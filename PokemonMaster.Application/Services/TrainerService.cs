using PokemonMaster.Application.DTOs;
using PokemonMaster.Application.Interfaces;
using PokemonMaster.Domain.Entities;
using PokemonMaster.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonMaster.Application.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IPokemonRepository _pokemonRepository;

        public TrainerService(
            ITrainerRepository trainerRepository, 
            IPokemonRepository pokemonRepository)
        {
            _trainerRepository = trainerRepository;
            _pokemonRepository = pokemonRepository;
        }

        public async Task<TrainerDTO> CreateTrainerAsync(CreateTrainerDTO trainerDto)
        {
            var trainer = new Trainer
            {
                Name = trainerDto.Name,
                Age = trainerDto.Age,
                CPF = trainerDto.CPF
            };

            int id = await _trainerRepository.CreateTrainerAsync(trainer);
            
            return new TrainerDTO
            {
                Id = id,
                Name = trainer.Name,
                Age = trainer.Age,
                CPF = trainer.CPF
            };
        }

        public async Task<TrainerDTO?> GetTrainerAsync(int id)
        {
            var trainer = await _trainerRepository.GetTrainerAsync(id);
            
            if (trainer == null) 
                return null;
                
            return new TrainerDTO
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Age = trainer.Age,
                CPF = trainer.CPF
            };
        }

        public async Task CatchPokemonAsync(CaughtPokemonDTO caughtPokemonDto)
        {
            var pokemon = await _pokemonRepository.GetByIdAsync(caughtPokemonDto.PokemonId);
            if (pokemon == null)
                throw new KeyNotFoundException($"Pokemon with ID {caughtPokemonDto.PokemonId} not found");

            var caughtPokemon = new CaughtPokemon
            {
                TrainerId = caughtPokemonDto.TrainerId,
                PokemonId = caughtPokemonDto.PokemonId,
                CaughtDate = DateTime.UtcNow
            };

            await _trainerRepository.AddCaughtPokemonAsync(caughtPokemon);
        }

        public async Task<IEnumerable<PokemonDTO>> GetCaughtPokemonsAsync(int trainerId)
        {
            var caughtPokemons = await _trainerRepository.GetCaughtPokemonsAsync(trainerId);
            
            var result = new List<PokemonDTO>();
            foreach (var caught in caughtPokemons)
            {
                var pokemon = await _pokemonRepository.GetByIdAsync(caught.PokemonId);
                if (pokemon != null)
                {
                    result.Add(new PokemonDTO
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
                    });
                }
            }
            
            return result;
        }
    }
}