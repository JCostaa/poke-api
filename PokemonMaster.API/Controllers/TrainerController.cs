using Microsoft.AspNetCore.Mvc;
using PokemonMaster.Application.DTOs;
using PokemonMaster.Application.Interfaces;

namespace PokemonMaster.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateTrainer(CreateTrainerDTO trainerDto)
        {
            var id = await _trainerService.CreateTrainerAsync(trainerDto);
            return CreatedAtAction(nameof(GetTrainer), new { id }, id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerDTO>> GetTrainer(int id)
        {
            var trainer = await _trainerService.GetTrainerAsync(id);
            
            if (trainer == null)
                return NotFound();
                
            return Ok(trainer);
        }

        [HttpPost("catch")]
        public async Task<ActionResult> CatchPokemon(CaughtPokemonDTO caughtPokemonDto)
        {
            try
            {
                await _trainerService.CatchPokemonAsync(caughtPokemonDto);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/pokemon")]
        public async Task<ActionResult<IEnumerable<PokemonDTO>>> GetCaughtPokemons(int id)
        {
            var pokemons = await _trainerService.GetCaughtPokemonsAsync(id);
            return Ok(pokemons);
        }
    }
}