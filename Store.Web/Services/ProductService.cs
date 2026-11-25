using Store.Web.Models;
using Store.Web.Services.Contracts;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Store.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/products";
        private readonly JsonSerializerOptions _jsonOptions;
        private ProductViewModel productViewModel;
        private IEnumerable<ProductViewModel> productsViewModel;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _jsonOptions = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts(string token)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        public async Task<ProductViewModel> GetProductById(int id, string token)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        public async Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel, string token)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel, string token)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        public async Task<bool> DeleteProductbyId(int id, string token)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await client.DeleteAsync($"{apiEndpoint}/{id}"))
                return response.IsSuccessStatusCode;
        }
    }
}