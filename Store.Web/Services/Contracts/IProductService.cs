using Store.Web.Models;

namespace Store.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProductById(int id);
        Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel);
        Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel);
        Task<bool> DeleteProductbyId(int id);
    }
}