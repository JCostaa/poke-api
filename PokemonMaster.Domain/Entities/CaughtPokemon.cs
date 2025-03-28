using System;

namespace PokemonMaster.Domain.Entities
{
    public class CaughtPokemon
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int TrainerId { get; set; }
        public DateTime CaughtDate { get; set; }
        public string Nickname { get; set; } = string.Empty;
        
        public Trainer? Trainer { get; set; }
    }
}