using DataLayer.Models;
using DataLayer;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.Service
{
    public class ProductuserService : IProductUser
    {
        private readonly eShopContext _shopContext;

        public ProductuserService(eShopContext eShopContext)
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

        public void DeleteEntit<T>(T entry) where T : class
        {
            _shopContext.Remove(entry);
            _shopContext.SaveChanges();
        }

        /// <summary>
        /// get all products inside of cart.
        /// </summary>
        /// <returns></returns>
        public List<ProductUser> GetProductcart(int id) => _shopContext.productUsers.Where(x => x.UserId == id).Include(x => x.Product).ToList();
        
        public async Task<int> GetProductAsync(int id) => await _shopContext.productUsers.Where(x => x.UserId == id).CountAsync();

        public ProductUser FindProductUserById(int prodId, int userId) => _shopContext.productUsers.Where(x => x.ProductId == prodId && x.UserId == userId).FirstOrDefault();
    }
}

