using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Model
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public string Name { get; set; }
        public string AccountNr { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int BackNr { get; set; }

        public List<PaymentMethodUser> PaymentMethodUsers { get; set; }
    }
}
