using System.Net.Http.Json;
using BlazorWebShop.Models;


namespace BlazorWebShop.Services
{
    public class ProductServiceAPI : IProductAPI
    {
        private readonly HttpClient _httpClient;

        public ProductServiceAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Product> CreateProductAsync(Product item)
        {
            var response = await _httpClient.PostAsJsonAsync<Product>("/api/Product/create", item);

            response.EnsureSuccessStatusCode();

            return item;
        }

        public async Task<bool> DeleteProdcutAsync(int id)
        {
            var resualt = await _httpClient.DeleteAsync($"/api/Product/delete?id={id}");
            return resualt.IsSuccessStatusCode;
        }

        public async Task<Product> UpdateProductAsync(Product item)
        {
            var response = await _httpClient.PutAsJsonAsync<Product>("/api/Product/update", item);

            if (response.IsSuccessStatusCode)
            {
                return item;
            }
            throw new KeyNotFoundException("Item does not exist");
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var item = await _httpClient.GetFromJsonAsync<Product>($"/api/Product/item/{id}");

            if (item != null)
            {
                return item;
            }
            throw new KeyNotFoundException("Item does not exist");
        }

        public async Task<List<Product>?> GetProductsAsync() => await _httpClient.GetFromJsonAsync<List<Product>>("/api/Product");

        public async Task<HttpResponseMessage> UploadImage(List<ImageFile> filesBase64) => await _httpClient.PostAsJsonAsync<List<ImageFile>>("/api/upload", filesBase64, System.Threading.CancellationToken.None);
    }
}
