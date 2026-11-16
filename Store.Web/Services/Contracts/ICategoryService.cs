using Store.Web.Models;

namespace Store.Web.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategories();
        Task<CategoryViewModel> GetCategoryById(int id);
        Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryViewModel);
        Task<CategoryViewModel> UpdateCategory(CategoryViewModel categoryViewModel);
        Task<bool> DeleteCategory(int id);
    }
}