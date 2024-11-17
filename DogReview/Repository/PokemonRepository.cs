using DogReview.Data;
using DogReview.Interfaces;
using DogReview.Models;

namespace DogReview.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)//many-to-many-relationship
        {
            var owner = _context.owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _context.categories.Where(o => o.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = owner,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };

            _context.Add(pokemonCategory);
            _context.pokemons.Add(pokemon);
            return Saved();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.pokemons.Remove(pokemon);
            return Saved();
        }

        //search by Id
        public Pokemon GetPokemon(int id)
        {
            return _context.pokemons.Where(p => p.Id == id).FirstOrDefault();
        }
        //search by Name
        public Pokemon GetPokemon(string name)
        {
            return _context.pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.reviews.Where(p => p.Pokemon.Id == pokeId);

            if(review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.pokemons.Any(p => p.Id ==  pokeId);
        }

        public bool Saved()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _context.pokemons.Update(pokemon);
            return Saved();
        }
    }
}
