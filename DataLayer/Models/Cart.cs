using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public List<ProductCart> ProductCarts { get; set; }
    }
}
