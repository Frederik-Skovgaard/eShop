using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace ServiceLayer.Service
{
    public class ProductService : IProduct
    {
        private readonly eShopContext _shopContext;

        public ProductService(eShopContext eShopContext)
        {
            _shopContext = eShopContext;
        }

        public Product AddProduct(Product prop)
        {
            _shopContext.Add(prop);
            _shopContext.SaveChanges();

            return _shopContext.Products.Where(x => x.Name == prop.Name && x.Price == prop.Price).FirstOrDefault();
        }

        public void AddEntity<T>(T entry) where T : class
        {
            _shopContext.Add(entry);
            _shopContext.SaveChanges();

        }

        public void UpdateEntit<T>(T entry) where T : class
        {
            _shopContext.Update(entry);
            _shopContext.SaveChanges();
        }


        public List<Product> GetProductByBrand(string brand) => _shopContext.Products.Where(x => x.Brand == brand).ToList();

        public List<Product> GetProducts() => _shopContext.Products.Where(x => x.IsDeleted == false).ToList();

        public Product FindProductById(int id) => _shopContext.Products.Where(x => x.ProductId == id).FirstOrDefault();

        public List<Product> GetPaginatedResualt(List<Product> list, int currentPage, int PageSize = 10) => list
            .OrderBy(d => d.ProductId)
            .Skip((currentPage - 1) * PageSize)
            .Take(PageSize).ToList();

        public List<Product> GetProductAPI() => _shopContext.Products.Where(x => x.IsDeleted == false).ToList();
    }
}
