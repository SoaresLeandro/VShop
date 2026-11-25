using Microsoft.AspNetCore.Identity;
using Store.Web.Models;
using Store.Web.Services.Contracts;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Store.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string _apiEndpoint = "/api/categories/";
        private IEnumerable<CategoryViewModel> _categoriesViewModel;
        private CategoryViewModel _categoryViewModel;
        private readonly JsonSerializerOptions _jsonOptions;

        public CategoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await client.GetAsync(_apiEndpoint))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    _categoriesViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>
                        (apiResponse, _jsonOptions);
                }
                else
                {
                    return null;
                }
            }

            return _categoriesViewModel;
        }

        public async Task<CategoryViewModel> GetCategoryById(int id, string token)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await client.GetAsync(_apiEndpoint + id))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    _categoryViewModel = await JsonSerializer.DeserializeAsync<CategoryViewModel>(apiResponse, _jsonOptions);
                }
                else
                {
                    return null;
                }
            }

            return _categoryViewModel;
        }

        public async Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryViewModel, string token)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            StringContent content = new StringContent(JsonSerializer.Serialize(categoryViewModel), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(_apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    _categoryViewModel = await JsonSerializer.DeserializeAsync<CategoryViewModel>(apiResponse, _jsonOptions);
                }
                else 
                {
                    return null;
                }
            }

            return _categoryViewModel;
        }

        public async Task<CategoryViewModel> UpdateCategory(CategoryViewModel categoryViewModel, string token)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CategoryViewModel categoryUpdated = new CategoryViewModel();

            using(var response = await client.PutAsJsonAsync(_apiEndpoint, categoryViewModel))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    categoryUpdated = await JsonSerializer.DeserializeAsync<CategoryViewModel>(apiResponse, _jsonOptions);
                }
                else
                {
                    return null;
                }
            }

            return categoryUpdated;
        }

        public async Task<bool> DeleteCategory(int id, string token)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await client.DeleteAsync(_apiEndpoint + id))
                return response.IsSuccessStatusCode;
        }
    }
}