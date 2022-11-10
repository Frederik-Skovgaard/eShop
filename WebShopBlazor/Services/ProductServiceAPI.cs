using WebShop.Model;
using System.Net.Http.Json;


namespace WebShop.Services
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
            ProductObject productObject = item.ProductItemObject();

            var response = await _httpClient.PostAsJsonAsync<ProductObject>("/api/Product/create", productObject);

            response.EnsureSuccessStatusCode();

            productObject = await response.Content.ReadFromJsonAsync<ProductObject>();
            return productObject.ProductItem();
        }

        public async Task<bool> DeleteProdcutAsync(int id)
        {
            var resualt = await _httpClient.DeleteAsync($"/api/Product/delete?id={id}");
            return resualt.IsSuccessStatusCode;
        }

        public async Task<Product> UpdateProductAsync(Product item)
        {
            ProductObject productObject = item.ProductItemObject();

            var response = await _httpClient.PutAsJsonAsync<ProductObject>("/api/Product/update", productObject);

            if (response.IsSuccessStatusCode)
            {
                productObject = await response.Content.ReadFromJsonAsync<ProductObject>();
                return productObject.ProductItem();
            }
            throw new KeyNotFoundException("Item does not exist");
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var item = await _httpClient.GetFromJsonAsync<ProductObject>($"/api/Product/item{id}");

            if (item != null)
            {
                return item.ProductItem();
            }
            throw new KeyNotFoundException("Item does not exist");
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var resualt = await _httpClient.GetFromJsonAsync<List<ProductObject>>("/api/Product");
            return resualt
                .AsQueryable()
                .MapProductItems()
                .ToList();
        }
    }

    public class ProductObject
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string? ImageUrl { get; set; }
        public int TypesId { get; set; }
        public bool IsDeleted { get; set; }
    }

    public static class ProductObjectMapper
    {
        public static ProductObject ProductItemObject(this Product item)
        {
            return new ProductObject
            {
                ProductId = item.ProductId,
                Name = item.Name,
                Price = item.Price,
                Brand = item.Brand,
                ImageUrl = item.ImageUrl,
                TypesId = item.TypesId,
                IsDeleted = item.IsDeleted
            };
        }

        public static Product ProductItem(this ProductObject item)
        {
            return new Product
            {
                ProductId = item.ProductId,
                Name = item.Name,
                Price = item.Price,
                Brand = item.Brand,
                ImageUrl = item.ImageUrl,
                TypesId = item.TypesId,
                IsDeleted = item.IsDeleted
            };
        }

        public static IQueryable<Product> MapProductItems(this IQueryable<ProductObject> items)
        {
            return items.Select(it => new Product
            {
                ProductId = it.ProductId,
                Name = it.Name,
                Price = it.Price,
                Brand = it.Brand,
                ImageUrl = it.ImageUrl,
                TypesId = it.TypesId,
                IsDeleted = it.IsDeleted
            });
        }
    }
}
