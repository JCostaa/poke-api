using Microsoft.EntityFrameworkCore;
using PokemonMaster.Domain.Entities;
using PokemonMaster.Domain.Interfaces;
using PokemonMaster.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonMaster.Infrastructure.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTrainerAsync(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return trainer.Id;
        }

        public async Task<Trainer?> GetTrainerAsync(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public async Task AddCaughtPokemonAsync(CaughtPokemon caughtPokemon)
        {
            _context.CaughtPokemons.Add(caughtPokemon);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CaughtPokemon>> GetCaughtPokemonsAsync(int trainerId)
        {
            return await _context.CaughtPokemons
                .Where(cp => cp.TrainerId == trainerId)
                .ToListAsync();
        }
    }
}