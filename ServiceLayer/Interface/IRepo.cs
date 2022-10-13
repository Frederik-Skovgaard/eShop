using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IRepo
    {
        #region Product

        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

        List<Product> GetProductByBrand(string brand);
        List<Product> GetProducts();

        #endregion

        #region User

        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);

        List<User> GetNormalUsers();
        List<User> GetUsers();

        #endregion

        #region ProductUser

        void AddProductUser(ProductUser productUser);
        void UpdateProductUser(ProductUser productUser);
        void DeleteProductUser(ProductUser productUser);

        #endregion

        #region UserInformation

        void AddUserInformation(UserInformation userInformation);
        void UpdateUserInformation(UserInformation userInformation);
        void DeleteUserInformation(UserInformation userInformation);

        #endregion

        #region Role

        void AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(Role role);

        List<Role> GetRoles();

        #endregion

        #region Type

        void AddType(Types types);
        void UpdateType(Types types);
        void DeleteType(Types types);

        List<Types> GetTypes();

        #endregion 

    }
}
