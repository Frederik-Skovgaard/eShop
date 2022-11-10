using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface.Old
{
    public interface IUserInfo
    {
        UserInformation GetUserInfoByContext(string street, string city, int zip);
    }
}
