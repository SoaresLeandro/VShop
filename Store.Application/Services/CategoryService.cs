using Store.Application.DTOs;
using Store.Application.Mappings;
using Store.Application.Services.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Store.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryProfile _categoryProfile;

        public CategoryService(ICategoryRepository categoryRepository, CategoryProfile categoryProfile)
        {
            _categoryRepository = categoryRepository;
            _categoryProfile = categoryProfile;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAll();

            return categories.Select(c => _categoryProfile.MappingCategoryToCategoryDTO(c)).ToList();
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesWithProducts()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetWithProducts();

            return categories.Select(c => _categoryProfile.MappingCategoryToCategoryDTO(c)).ToList();
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            Category category = await _categoryRepository.GetById(id);

            return _categoryProfile.MappingCategoryToCategoryDTO(category);
        }

        public async Task<CategoryDTO> GetCategory(string search)
        {
            Expression<Func<Category, bool>> expression = c => c.Name == search;

            Category category = await _categoryRepository.Get(expression);

            return _categoryProfile.MappingCategoryToCategoryDTO(category);
        }

        public async Task<CategoryDTO> AddCategory(CategoryDTO categoryDto)
        {
            Category category = _categoryProfile.MappingCategoryDTOToCategory(categoryDto);

            Category CategoryCreated = await _categoryRepository.Create(category);

            return _categoryProfile.MappingCategoryToCategoryDTO(CategoryCreated);
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO categoryDto)
        {
            Category category = _categoryProfile.MappingCategoryDTOToCategory(categoryDto);

            Category CategoryUpdated = await _categoryRepository.Update(category);

            return _categoryProfile.MappingCategoryToCategoryDTO(CategoryUpdated);
        }

        public async Task RemoveCategory(int id)
        {
            Category category = await _categoryRepository.GetById(id);

            if(category is not null)
                await _categoryRepository.Delete(category);
        }
    }
}