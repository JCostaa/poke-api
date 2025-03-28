namespace PokemonMaster.Domain.Entities
{
    public class PokemonEvolution
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int EvolutionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SpriteUrl { get; set; } = string.Empty;
        public string SpriteBase64 { get; set; } = string.Empty;
    }
}