using DataLayer.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface.Old;

namespace ServiceLayer.Service.Old
{
    public class PaymentMethoUser : IPaymentMethodsUser
    {
        private readonly eShopContext _shopContext;

        public PaymentMethoUser(eShopContext eShopContext)
        {
            _shopContext = eShopContext;
        }

        public List<PaymentMethodUser> GetPaymentMethods(int id) => _shopContext.PaymentMethodUsers.Where(x => x.UserId == id)
            .Include(x => x.PaymentMethod)
            .Include(x => x.User).ToList();


    }
}
