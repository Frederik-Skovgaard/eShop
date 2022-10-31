using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class PaymentMethodUser
    {
        public int PaymentMethodId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
