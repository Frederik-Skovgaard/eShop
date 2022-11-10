using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface.Old
{
    public interface IProductUser
    {

        void AddEntity<T>(T entry) where T : class;


        void UpdateEntit<T>(T entry) where T : class;

        void DeleteEntit<T>(T entry) where T : class;

        List<ProductUser> GetProductcart(int id);

        Task<int> GetProductAsync(int id);

        ProductUser FindProductUserById(int prodId, int userId);
    }
}
