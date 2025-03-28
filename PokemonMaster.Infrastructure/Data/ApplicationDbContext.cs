using Microsoft.EntityFrameworkCore;
using PokemonMaster.Domain.Entities;

namespace PokemonMaster.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<CaughtPokemon> CaughtPokemons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Trainer entity
            modelBuilder.Entity<Trainer>()
                .HasKey(t => t.Id);
            
            modelBuilder.Entity<Trainer>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            modelBuilder.Entity<Trainer>()
                .Property(t => t.CPF)
                .IsRequired()
                .HasMaxLength(14);

            // Configure CaughtPokemon entity
            modelBuilder.Entity<CaughtPokemon>()
                .HasKey(cp => cp.Id);
            
            modelBuilder.Entity<CaughtPokemon>()
                .HasOne(cp => cp.Trainer)
                .WithMany(t => t.CaughtPokemons)
                .HasForeignKey(cp => cp.TrainerId);
        }
    }
}