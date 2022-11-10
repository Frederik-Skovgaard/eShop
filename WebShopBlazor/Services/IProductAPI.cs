using WebShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Services
{
    public interface IProductAPI
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);

        Task<Product> UpdateProductAsync(Product item);
        Task<bool> DeleteProdcutAsync(int id);
        Task<Product> CreateProductAsync(Product item);
    }
}
