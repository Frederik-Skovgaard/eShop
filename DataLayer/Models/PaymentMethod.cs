using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public string Name { get; set; }
        public int AccountNr { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int BackNr { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
