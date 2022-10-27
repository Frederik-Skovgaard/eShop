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
    public class UserService : IUser
    {
        private readonly eShopContext _shopContext;

        public UserService(eShopContext eShopContext)
        {
            _shopContext = eShopContext;
        }

        public void AddEntity<T>(T entry) where T : class
        {
            _shopContext.Add(entry);
            _shopContext.SaveChanges();
        }

        public void UpdateEntit<T>(T entry) where T : class
        {
            _shopContext.Update(entry);
            _shopContext.SaveChanges();
        }

        public List<User>
            GetNormalUsers() => _shopContext.Users.Where(x => x.RoleId == 3 && x.IsDeleted == false).ToList();

        public List<User> GetUsers() => _shopContext.Users.Where(x => x.IsDeleted == false)
            .Include(r => r.Role)
            .Include(i => i.UserInformation).ToList();

        public bool LoginUser(string user, string pas) => _shopContext.Users.Where(x => x.IsDeleted == false).Any(x => x.UserName == user && x.Password == pas);

        public User GetUserIdAndRole(string user, string pas) => _shopContext.Users.Where(x => x.UserName == user && x.Password == pas && x.IsDeleted == false).FirstOrDefault();

        public User GetUser(int id) => _shopContext.Users.Where(x => x.UserId == id && x.IsDeleted == false).Include(u => u.UserInformation).FirstOrDefault();

        public List<User> GetPaginatedResualt(List<User> list, int currentPage, int PageSize = 10) => list
            .OrderBy(d => d.UserId)
            .Skip((currentPage - 1) * PageSize)
            .Take(PageSize).ToList();
    }
}
