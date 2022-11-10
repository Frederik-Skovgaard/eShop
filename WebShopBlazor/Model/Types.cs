using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Model
{
    public class Types
    {
        public int TypesId { get; set; }
        public string Name { get; set; }

        public List<Product> products { get; set; }
    }
}
