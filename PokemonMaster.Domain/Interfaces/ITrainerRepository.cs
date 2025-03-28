using PokemonMaster.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonMaster.Domain.Interfaces
{
    public interface ITrainerRepository
    {
        Task<int> CreateTrainerAsync(Trainer trainer);
        Task<Trainer?> GetTrainerAsync(int id);
        Task AddCaughtPokemonAsync(CaughtPokemon caughtPokemon);
        Task<List<CaughtPokemon>> GetCaughtPokemonsAsync(int trainerId);
    }
}