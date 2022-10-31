using DataLayer.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Interface;

namespace ServiceLayer.Service
{
    public class TypeService : IType
    {
        private readonly eShopContext _shopContext;

        public TypeService(eShopContext eShopContext)
        {
            _shopContext = eShopContext;
        }

        public void DeleteEntit<T>(T entry) where T : class
        {
            _shopContext.Remove(entry);
            _shopContext.SaveChanges();
        }

        public List<Types> GetTypes() => _shopContext.Types.ToList();

        public Types FindTpyeById(int id) => _shopContext.Types.Where(x => x.TypesId == id).FirstOrDefault();
    }
}
