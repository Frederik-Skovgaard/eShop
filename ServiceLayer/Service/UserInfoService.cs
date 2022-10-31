using DataLayer;
using DataLayer.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class UserInfoService : IUserInfo
    {
        private readonly eShopContext _shopContext;

        public UserInfoService(eShopContext eShopContext)
        {
            _shopContext = eShopContext;
        }

        public UserInformation GetUserInfoByContext(string street, string city, int zip) => _shopContext.UserInformations.Where(x => x.Street == street && x.City == city && x.ZipCode == zip).FirstOrDefault();

    }
}
