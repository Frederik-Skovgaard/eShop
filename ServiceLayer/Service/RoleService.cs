using DataLayer.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Interface;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.Service
{
    public class RoleService : IRole
    {
        private readonly eShopContext _shopContext;

        public RoleService(eShopContext eShopContext)
        {
            _shopContext = eShopContext;
        }

        public List<Role> GetRoles() => _shopContext.Roles.ToList();

        public Role GetRoleById(int id) => _shopContext.Roles.Where(x => x.RoleId == id).FirstOrDefault();

       
    }
}
