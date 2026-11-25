using Store.Web.Models;

namespace Store.Web.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token);
        Task<CategoryViewModel> GetCategoryById(int id, string token);
        Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryViewModel, string token);
        Task<CategoryViewModel> UpdateCategory(CategoryViewModel categoryViewModel, string token);
        Task<bool> DeleteCategory(int id, string token);
    }
}