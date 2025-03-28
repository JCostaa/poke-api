namespace PokemonMaster.Application.DTOs
{
    public class TrainerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string CPF { get; set; } = string.Empty;
    }

    public class CreateTrainerDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string CPF { get; set; } = string.Empty;
    }

    public class CaughtPokemonDTO
    {
        public int PokemonId { get; set; }
        public int TrainerId { get; set; }
        public string? Nickname { get; set; }
    }

    public class TrainerPokemonDTO
    {
        public int Id { get; set; }
        public PokemonDTO Pokemon { get; set; } = new PokemonDTO();
        public DateTime CaughtDate { get; set; }
        public string Nickname { get; set; } = string.Empty;
    }
}