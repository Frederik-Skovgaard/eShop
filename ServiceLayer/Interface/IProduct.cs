using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IProduct
    {
        //CRUD
        void AddEntity<T>(T entry) where T : class;
        void UpdateEntit<T>(T entry) where T : class;


        //Product
        List<Product> GetProductByBrand(string brand);
        List<Product> GetProducts();
        Product FindProductById(int id);

        List<Product> GetPaginatedResualt(List<Product> list, int currentPage, int PageSize = 10);

        

    }
}
