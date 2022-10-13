using DataLayer;
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
        void AddEntity<T>(T entry) where T : class;
        void UpdateEntit<T>(T entry) where T : class;


        //Product
        List<Product> GetProductByBrand(string brand);
        List<Product> GetProducts();
        Product FindById(int id);


        //User
        List<User> GetNormalUsers();
        List<User> GetUsers();


        //Role
        List<Role> GetRoles();


        //Types
        List<Types> GetTypes();


    }
}
