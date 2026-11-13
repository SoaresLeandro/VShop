using Store.Application.DTOs;

namespace Store.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<IEnumerable<CategoryDTO>> GetCategoriesWithProducts();
        Task<CategoryDTO> GetCategoryById(int id);
        Task<CategoryDTO> GetCategory(string search);
        Task<CategoryDTO> AddCategory(CategoryDTO categoryDto);
        Task<CategoryDTO> UpdateCategory(CategoryDTO categoryDto);
        Task RemoveCategory(int id);
    }
}