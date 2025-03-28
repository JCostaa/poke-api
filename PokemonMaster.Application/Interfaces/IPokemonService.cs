using PokemonMaster.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonMaster.Application.Interfaces
{
    public interface IPokemonService
    {
        Task<List<PokemonDTO>> GetRandomPokemonsAsync(int count);
        Task<PokemonDTO?> GetPokemonByIdAsync(int id);
    }
}