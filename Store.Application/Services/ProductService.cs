using Store.Application.DTOs;
using Store.Application.Mappings;
using Store.Application.Services.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Store.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductProfile _productProfile;

        public ProductService(IProductRepository productRepository, ProductProfile productProfile)
        {
            _productRepository = productRepository;
            _productProfile = productProfile;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            IEnumerable<Product> products = await _productRepository.GetAll();

            return products.Select(c => _productProfile.MappingProductToProductDTO(c)).ToList();
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            Product Product = await _productRepository.GetById(id);

            return _productProfile.MappingProductToProductDTO(Product);
        }

        public async Task<ProductDTO> GetProduct(string search)
        {
            Expression<Func<Product, bool>> expression = p => p.Name == search;

            Product Product = await _productRepository.Get(expression);

            return _productProfile.MappingProductToProductDTO(Product);
        }

        public async Task<ProductDTO> AddProduct(ProductDTO productDTO)
        {
            Product Product = _productProfile.MappingProductDTOToProduct(productDTO);

            Product ProductCreated = await _productRepository.Create(Product);

            return _productProfile.MappingProductToProductDTO(ProductCreated);
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO productDTO)
        {
            Product Product = _productProfile.MappingProductDTOToProduct(productDTO);

            Product ProductUpdated = await _productRepository.Update(Product);

            return _productProfile.MappingProductToProductDTO(ProductUpdated);
        }

        public async Task RemoveProduct(int id)
        {
            Product Product = await _productRepository.GetById(id);

            if (Product is not null)
                await _productRepository.Delete(Product);
        }
    }
}