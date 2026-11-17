using Store.Application.DTOs;
using Store.Domain.Entities;

namespace Store.Application.Mappings
{
    public class ProductProfile
    {
        public ProductDTO MappingProductToProductDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageURL = product.ImageURL,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name
            };
        }

        public Product MappingProductDTOToProduct(ProductDTO productDTO)
        {
            return new Product
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                ImageURL = productDTO.ImageURL,
                CategoryId = productDTO.CategoryId
            };
        }
    }
}