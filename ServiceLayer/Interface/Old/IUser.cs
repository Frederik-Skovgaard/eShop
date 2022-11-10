using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface.Old
{
    public interface IUser
    {
        void AddEntity<T>(T entry) where T : class;
        void UpdateEntit<T>(T entry) where T : class;

        List<User> GetNormalUsers();
        List<User> GetUsers();

        bool LoginUser(string user, string pas);
        User GetUserIdAndRole(string user, string pas);

        User GetUser(int id);



        List<User> GetPaginatedResualt(List<User> list, int currentPage, int PageSize = 10);
    }
}
