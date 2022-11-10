using DataLayer;
using DataLayer.Models;
using ServiceLayer.Interface.Old;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.Old
{
    public class PaymentService : IPaymentMethod
    {
        private readonly eShopContext _shopContext;

        public PaymentService(eShopContext eShopContext)
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

        public PaymentMethod GetPaymentMethod(string name, string account) => _shopContext.PaymentMethods.Where(x => x.Name == name && x.AccountNr == account).FirstOrDefault();

    }
}
