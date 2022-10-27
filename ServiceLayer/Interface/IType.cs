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
        List<Types> GetTypes();
        Types FindTpyeById(int id);
    }
}
