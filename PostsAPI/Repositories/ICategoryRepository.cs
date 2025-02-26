using PostsAPI.Models;

namespace PostsAPI.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
    }
}
