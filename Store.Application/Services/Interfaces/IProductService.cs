using Store.Application.DTOs;

namespace Store.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int id);
        Task<ProductDTO> GetProduct(string search);
        Task<ProductDTO> AddProduct(ProductDTO ProductDTO);
        Task<ProductDTO> UpdateProduct(ProductDTO ProductDTO);
        Task RemoveProduct(int id);
    }
}