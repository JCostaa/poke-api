using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using PokemonMaster.Domain.Entities;

namespace PokemonMaster.Infrastructure.ExternalServices
{
    public class PokemonApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public PokemonApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }

        public async Task<Pokemon?> GetPokemonByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<PokemonApiResponse>($"pokemon/{id}", _options);
                if (response == null) return null;

                var speciesResponse = await _httpClient.GetFromJsonAsync<SpeciesResponse>(response.Species.Url, _options);
                
                var pokemon = new Pokemon
                {
                    Id = response.Id,
                    Name = response.Name,
                    SpriteUrl = response.Sprites.FrontDefault,
                    SpriteBase64 = await GetImageAsBase64(response.Sprites.FrontDefault)
                };

                // Get evolution chain
                if (speciesResponse?.EvolutionChain?.Url != null)
                {
                    var evolutionResponse = await _httpClient.GetFromJsonAsync<EvolutionChainResponse>(speciesResponse.EvolutionChain.Url, _options);
                    if (evolutionResponse != null)
                    {
                        await ExtractEvolutionsAsync(evolutionResponse.Chain, pokemon);
                    }
                }

                return pokemon;
            }
            catch (Exception)
            {
                // Log exception
                return null;
            }
        }

        private async Task ExtractEvolutionsAsync(ChainLink chain, Pokemon pokemon)
        {
            if (chain?.EvolvesTo == null || !chain.EvolvesTo.Any()) return;

            foreach (var evolution in chain.EvolvesTo)
            {
                // Get the ID from the species URL
                var url = evolution.Species.Url;
                var id = int.Parse(url.Split('/').Where(x => !string.IsNullOrEmpty(x)).Last());
                
                // Get detailed Pokemon info including sprite
                var pokemonInfo = await _httpClient.GetFromJsonAsync<PokemonApiResponse>($"pokemon/{id}", _options);
                if (pokemonInfo != null)
                {
                    pokemon.Evolutions.Add(new PokemonEvolution
                    {
                        PokemonId = pokemon.Id,
                        EvolutionId = id,
                        Name = evolution.Species.Name,
                        SpriteUrl = pokemonInfo.Sprites.FrontDefault,
                        SpriteBase64 = await GetImageAsBase64(pokemonInfo.Sprites.FrontDefault)
                    });
                }

                // Recursive call to get next evolution
                await ExtractEvolutionsAsync(evolution, pokemon);
            }
        }

        private async Task<string> GetImageAsBase64(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return string.Empty;

            try
            {
                var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
                return Convert.ToBase64String(imageBytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<List<Pokemon>> GetRandomPokemonsAsync(int count)
        {
            // PokeAPI has over 1000 Pokemon, let's limit to that
            var random = new Random();
            var randomIds = Enumerable.Range(1, 1000)
                .OrderBy(_ => random.Next())
                .Take(count)
                .ToList();

            var tasks = randomIds.Select(id => GetPokemonByIdAsync(id));
            var pokemons = await Task.WhenAll(tasks);
            
            return pokemons.Where(p => p != null).ToList()!;
        }
    }

    // Models to deserialize API responses
    public class PokemonApiResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public NamedApiResource Species { get; set; } = new();
        public SpriteInfo Sprites { get; set; } = new();
    }

    public class SpriteInfo
    {
        public string FrontDefault { get; set; } = string.Empty;
    }

    public class SpeciesResponse
    {
        public NamedApiResource EvolutionChain { get; set; } = new();
    }

    public class EvolutionChainResponse
    {
        public ChainLink Chain { get; set; } = new();
    }

    public class ChainLink
    {
        public NamedApiResource Species { get; set; } = new();
        public List<ChainLink> EvolvesTo { get; set; } = new();
    }

    public class NamedApiResource
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}