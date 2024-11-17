using DogReview.Models;

namespace DogReview.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Pokemon> GetPokemonByCategory(int CategoryId);
        bool CategoryExist(int id);//does not return data, it only check if it exist
        bool CreateCategory(Category category);//create
        bool UpdateCategory(Category category);//update
        bool DeleteCategory(Category category);
        bool Save();

    }
}
