using Microsoft.AspNetCore.Mvc;
using PokemonMaster.Application.DTOs;
using PokemonMaster.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonMaster.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("random")]
        public async Task<ActionResult<List<PokemonDTO>>> GetRandomPokemons()
        {
            var pokemons = await _pokemonService.GetRandomPokemonsAsync(10);
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PokemonDTO>> GetPokemon(int id)
        {
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            
            if (pokemon == null)
                return NotFound();
                
            return Ok(pokemon);
        }
    }
}