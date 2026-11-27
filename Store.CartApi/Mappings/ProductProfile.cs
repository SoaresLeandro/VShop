using Store.CartApi.DTOs;
using Store.CartApi.Models;

namespace Store.CartApi.Mappings;

public class ProductProfile
{
    public static ProductDTO MappingProductToProductDTO(Product product)
    {
        return new ProductDTO()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CategoryName = product.CategoryName,
            ImageURL = product.ImageURL,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public static Product MappingProductDTOToProduct(ProductDTO productDTO)
    {
        return new Product()
        {
            Id = productDTO.Id,
            Name = productDTO.Name,
            Description = productDTO.Description,
            CategoryName = productDTO.CategoryName,
            ImageURL = productDTO.ImageURL,
            Price = productDTO.Price,
            Stock = productDTO.Stock
        };
    }
}