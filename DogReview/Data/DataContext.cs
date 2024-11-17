using DogReview.Models;
using Microsoft.EntityFrameworkCore;

namespace DogReview.Data
{
    public class DataContext: DbContext
    {
        public DbSet<Category> categories { get; set; }

        public DbSet<Country> countries { get; set; }

        public DbSet<Owner> owners{ get; set; }

        public DbSet<Pokemon> pokemons { get; set; }

        public DbSet<PokemonOwner> pokemonOwners { get; set; }

        public DbSet<PokemonCategory> pokemonCategories { get; set; }   

        public DbSet<Review> reviews { get; set; }

        public  DbSet<Reviewer> reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modebuilder)
        {
            //base.OnModelCreating(builder);
            modebuilder.Entity<PokemonCategory>()
                .HasKey(pc => new { pc.PokemonId, pc.CategoryId });
            modebuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(pc => pc.PokemonId);
            modebuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(pc => pc.PokemonId);

            modebuilder.Entity<PokemonOwner>()
               .HasKey(po => new { po.PokemonId, po.OwnerId });
            modebuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(po => po.PokemonId);
            modebuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Owner)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(po => po.OwnerId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DogReview;Trusted_Connection=True;ConnectRetryCount=0");
        }
    }
}
