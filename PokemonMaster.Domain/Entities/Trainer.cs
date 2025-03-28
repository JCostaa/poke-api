namespace PokemonMaster.Domain.Entities
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string CPF { get; set; } = string.Empty;
        public List<CaughtPokemon> CaughtPokemons { get; set; } = new List<CaughtPokemon>();
    }
}