using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string _baseUrl = "api/v1/product";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Error: {response.StatusCode}, {response.ReasonPhrase}"
                );
            }

            var product = await response.Content.ReadFromJsonAsync<Product>();

            if (product == null)
            {
                throw new Exception("Failed to deserialize the response into a product.");
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Error: {response.StatusCode}, {response.ReasonPhrase}"
                );
            }

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            if (products == null)
            {
                throw new Exception("Failed to deserialize the response into a list of products.");
            }

            return products;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJson(_baseUrl, product);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Error: {response.StatusCode}, {response.ReasonPhrase}"
                );
            }
            var newProduct = await response.Content.ReadFromJsonAsync<Product>();

            if (newProduct == null)
            {
                throw new Exception("Failed to deserialize the response into a product.");
            }

            return newProduct;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJson($"{_baseUrl}/{product.Id}", product);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Error: {response.StatusCode}, {response.ReasonPhrase}"
                );
            }
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Error: {response.StatusCode}, {response.ReasonPhrase}"
                );
            }
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
