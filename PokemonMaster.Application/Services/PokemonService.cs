using PokemonMaster.Application.DTOs;
using PokemonMaster.Application.Interfaces;
using PokemonMaster.Domain.Entities;
using PokemonMaster.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonMaster.Application.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<List<PokemonDTO>> GetRandomPokemonsAsync(int count)
        {
            var pokemons = await _pokemonRepository.GetRandomPokemonsAsync(count);
            return pokemons.Select(MapToDto).ToList();
        }

        public async Task<PokemonDTO?> GetPokemonByIdAsync(int id)
        {
            var pokemon = await _pokemonRepository.GetByIdAsync(id);
            return pokemon != null ? MapToDto(pokemon) : null;
        }

        private PokemonDTO MapToDto(Pokemon pokemon)
        {
            return new PokemonDTO
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                SpriteBase64 = pokemon.SpriteBase64,
                Evolutions = pokemon.Evolutions.Select(e => new PokemonEvolutionDTO
                {
                    Id = e.EvolutionId,
                    Name = e.Name,
                    SpriteBase64 = e.SpriteBase64
                }).ToList()
            };
        }
    }
}