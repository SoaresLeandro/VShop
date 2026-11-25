using Store.Web.Models;

namespace Store.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts(string token);
        Task<ProductViewModel> GetProductById(int id, string token);
        Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel, string token);
        Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel, string token);
        Task<bool> DeleteProductbyId(int id, string token);
    }
}