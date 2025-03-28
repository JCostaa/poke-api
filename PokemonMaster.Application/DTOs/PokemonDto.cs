using System.Collections.Generic;

namespace PokemonMaster.Application.DTOs
{
    public class PokemonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SpriteBase64 { get; set; } = string.Empty;
        public List<PokemonEvolutionDTO> Evolutions { get; set; } = new List<PokemonEvolutionDTO>();
    }

    public class PokemonEvolutionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SpriteBase64 { get; set; } = string.Empty;
    }
}