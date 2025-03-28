using PokemonMaster.Domain.Entities;

namespace PokemonMaster.Domain.Interfaces
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetRandomPokemonsAsync(int count);
        Task<Pokemon?> GetByIdAsync(int id);
    }
}