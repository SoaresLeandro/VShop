using Store.Application.DTOs;
using Store.Domain.Entities;

namespace Store.Application.Mappings
{
    public class CategoryProfile
    {
        public CategoryDTO MappingCategoryToCategoryDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products
            };
        }

        public Category MappingCategoryDTOToCategory(CategoryDTO categoryDTO)
        {
            return new Category 
            { 
                Id = categoryDTO.Id,
                Name = categoryDTO.Name, 
                Products = categoryDTO.Products 
            };
        }
    }
}