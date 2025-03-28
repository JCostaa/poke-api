using PokemonMaster.Domain.Entities;
using PokemonMaster.Domain.Interfaces;
using PokemonMaster.Infrastructure.ExternalServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonMaster.Infrastructure.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonApiClient _pokemonApiClient;

        public PokemonRepository(PokemonApiClient pokemonApiClient)
        {
            _pokemonApiClient = pokemonApiClient;
        }

        public async Task<List<Pokemon>> GetRandomPokemonsAsync(int count)
        {
            var pokemons = await _pokemonApiClient.GetRandomPokemonsAsync(count);
            return pokemons.ToList();
        }

        public async Task<Pokemon?> GetByIdAsync(int id)
        {
            return await _pokemonApiClient.GetPokemonByIdAsync(id);
        }
    }
}