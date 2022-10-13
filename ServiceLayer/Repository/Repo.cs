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
    public class Repo : IRepo
    {
        private readonly  eShopContext _shopContext;

        public Repo()
        {
            _shopContext = new eShopContext();
        }

        /// <summary>
        /// Adds an entity to database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        public void AddEntity<T>(T entry) where T : class
        {
            _shopContext.Add(entry);
            _shopContext.SaveChanges();
        }

        /// <summary>
        /// Updates an entity in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        public void UpdateEntit<T>(T entry) where T : class
        {
            _shopContext.Update(entry);
            _shopContext.SaveChanges();
        }


        #region Product

        public List<Product> GetProductByBrand(string brand) => _shopContext.Products.Where(x => x.Brand == brand).ToList();

        public List<Product> GetProducts() => _shopContext.Products.Where(x => x.IsDeleted == false)
            .Include(p => p.ProductUsers)
            .ThenInclude(u => u.User)
            .Include(t => t.Types).ToList();

        public Product FindProductById(int id) => _shopContext.Products.Where(x => x.ProductId == id).FirstOrDefault();


        #endregion

        #region User

        public List<User>
            GetNormalUsers() => _shopContext.Users.Where(x => x.RoleId == 3).ToList();

        public List<User> GetUsers() => _shopContext.Users.Where(x => x.IsDeleted == false)
            .Include(r => r.Role)
            .Include(i => i.UserInformation).ToList();

        #endregion


        //Role
        public List<Role> GetRoles() => _shopContext.Roles.ToList();


        //Types
        public List<Types> GetTypes() => _shopContext.Types.ToList();

        public Types FindTpyeById(int id) => _shopContext.Types.Where(x => x.TypesId == id).FirstOrDefault();

    }
}
