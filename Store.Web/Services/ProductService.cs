using Store.Web.Models;
using Store.Web.Services.Contracts;
using System.Text;
using System.Text.Json;

namespace Store.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/products/";
        private readonly JsonSerializerOptions _jsonOptions;
        private ProductViewModel productViewModel;
        private IEnumerable<ProductViewModel> productsViewModel;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _jsonOptions = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            var client = _clientFactory.CreateClient("ProductApi");

            using (var response = await client.GetAsync(apiEndpoint))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    productsViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>
                        (apiResponse, _jsonOptions);
                }
                else
                {
                    return null;
                }
            }

            return productsViewModel;
        }

        public async Task<ProductViewModel> GetProductById(int id)
        {
            var client = _clientFactory.CreateClient("ProductApi");

            using (var response = await client.GetAsync($"{apiEndpoint}/{id}"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _jsonOptions);
                }
                else
                {
                    return null;
                }
            }

            return productViewModel;
        }

        public async Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel)
        {
            var client = _clientFactory.CreateClient("ProductApi");

            StringContent content = new StringContent(JsonSerializer.Serialize(productViewModel), 
                                                    Encoding.UTF8,
                                                    "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _jsonOptions);
                }
                else
                {
                    return null;
                }
            }

            return productViewModel;
        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            ProductViewModel productUpdated = new ProductViewModel();

            using (var response = await client.PutAsJsonAsync(apiEndpoint, productViewModel))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    productUpdated = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _jsonOptions);
                }
                else
                {
                    return null;
                }
            }

            return productUpdated;
        }

        public async Task<bool> DeleteProductbyId(int id)
        {
            var client = _clientFactory.CreateClient("ProductApi");

            using (var response = await client.DeleteAsync($"{apiEndpoint}/{id}"))
                return response.IsSuccessStatusCode;
        }
    }
}