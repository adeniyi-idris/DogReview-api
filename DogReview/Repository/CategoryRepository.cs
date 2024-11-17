using DogReview.Data;
using DogReview.Interfaces;
using DogReview.Models;

namespace DogReview.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExist(int id)
        {
            return _context.categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            // Change Tracker
            _context.categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.categories.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
           return _context.categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int CategoryId)
        {
            return _context.pokemonCategories.Where(c => c.CategoryId == CategoryId).Select(e => e.Pokemon).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true: false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.categories.Update(category);
            return Save();
        }
    }
}
