using System.Collections.Generic;

namespace PokemonMaster.Domain.Entities
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SpriteUrl { get; set; } = string.Empty;
        public string SpriteBase64 { get; set; } = string.Empty;
        public List<PokemonEvolution> Evolutions { get; set; } = new List<PokemonEvolution>();
    }
}