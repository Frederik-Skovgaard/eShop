using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface.Old
{
    public interface IRole
    {
        void DeleteEntit<T>(T entry) where T : class;

        List<Role> GetRoles();
        Role GetRoleById(int id);
    }
}
