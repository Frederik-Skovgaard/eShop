using DataLayer.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Interface;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.Service
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
