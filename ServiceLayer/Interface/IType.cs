using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IType
    {
        void DeleteEntit<T>(T entry) where T : class;

        List<Types> GetTypes();
        Types FindTpyeById(int id);
    }
}
