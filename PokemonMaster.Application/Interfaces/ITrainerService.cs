using PokemonMaster.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonMaster.Application.Interfaces
{
    public interface ITrainerService
    {
        Task<TrainerDTO> CreateTrainerAsync(CreateTrainerDTO trainerDto);
        Task<TrainerDTO?> GetTrainerAsync(int id);
        Task CatchPokemonAsync(CaughtPokemonDTO caughtPokemonDto);
        Task<IEnumerable<PokemonDTO>> GetCaughtPokemonsAsync(int trainerId);
    }
}