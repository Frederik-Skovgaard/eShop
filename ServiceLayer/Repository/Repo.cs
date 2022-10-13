using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using ServiceLayer.Interface;

namespace ServiceLayer.Repository
{
    public class Repo : IRepo
    {
        private readonly  eShopContext _shopContext;

        public Repo(eShopContext shopContext)
        {
            _shopContext = shopContext;
        }


        #region Product

        public void AddProduct(Product product)
        {
            _shopContext.Products.Add(product);
            _shopContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _shopContext.Products.Update(product);
            _shopContext.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _shopContext.Update(product);
            _shopContext.SaveChanges();
        }

        public List<Product> GetProductByBrand(string brand) => _shopContext.Products.Where(x => x.Brand == brand).ToList();

        public List<Product> GetProducts() => _shopContext.Products.ToList();

        #endregion


        #region User

        public void AddUser(User user)
        {
            _shopContext.Users.Add(user);
            _shopContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _shopContext.Update(user);
            _shopContext.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _shopContext.Update(user);
            _shopContext.SaveChanges();
        }

        public List<User> GetNormalUsers() => _shopContext.Users.Where(x => x.RoleId == 3).ToList();

        public List<User> GetUsers() => _shopContext.Users.ToList();

        #endregion

        #region ProductUser

        public void AddProductUser(ProductUser productUser)
        {
            _shopContext.Add(productUser);
            _shopContext.SaveChanges();
        }

        public void UpdateProductUser(ProductUser productUser)
        {
            _shopContext.Update(productUser);
            _shopContext.SaveChanges();
        }

        public void DeleteProductUser(ProductUser productUser)
        {
            _shopContext.Update(productUser);
            _shopContext.SaveChanges();
        }

        #endregion

        #region UserInformation

        public void AddUserInformation(UserInformation userInformation)
        {
            _shopContext.Add(userInformation);
            _shopContext.SaveChanges();
        }

        public void UpdateUserInformation(UserInformation userInformation)
        {
            _shopContext.Update(userInformation);
            _shopContext.SaveChanges();
        }

        public void DeleteUserInformation(UserInformation userInformation)
        {
            _shopContext.Update(userInformation);
            _shopContext.SaveChanges();
        }

        #endregion

        #region Role

        public void AddRole(Role role)
        {
            _shopContext.Add(role);
            _shopContext.SaveChanges();
        }

        public void UpdateRole(Role role)
        {
            _shopContext.Update(role);
            _shopContext.SaveChanges();
        }

        public void DeleteRole(Role role)
        {
            _shopContext.Update(role);
            _shopContext.SaveChanges();
        }

        public List<Role> GetRoles() => _shopContext.Roles.ToList();

        #endregion

        #region Type

        public void AddType(Types types)
        {
            _shopContext.Add(types);
            _shopContext.SaveChanges();
        }

        public void UpdateType(Types types)
        {
            _shopContext.Update(types);
            _shopContext.SaveChanges();
        }

        public void DeleteType(Types types)
        {
            _shopContext.Update(types);
            _shopContext.SaveChanges();
        }

        public List<Types> GetTypes() => _shopContext.Types.ToList();

        #endregion

    }
}
