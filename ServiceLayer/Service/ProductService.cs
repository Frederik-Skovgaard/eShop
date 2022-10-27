using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace ServiceLayer.Repository
{
    public class ProductService : IProduct
    {
        private readonly  eShopContext _shopContext;

        public ProductService(eShopContext eShopContext)
        {
            _shopContext = eShopContext;
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

        public List<Product> GetProducts() => _shopContext.Products.Where(x => x.IsDeleted == false)
            .Include(p => p.ProductUsers)
            .ThenInclude(u => u.User)
            .Include(t => t.Types).ToList();

        public Product FindProductById(int id) => _shopContext.Products
            .Where(x => x.ProductId == id).FirstOrDefault();

        public List<Product> GetPaginatedResualt(List<Product> list, int currentPage, int PageSize = 10) => list
            .OrderBy(d => d.ProductId)
            .Skip((currentPage - 1) * PageSize)
            .Take(PageSize).ToList();
    }
}
