using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        [AllowNull]
        public string ImageUrl { get; set; }

        public int TypesId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public List<ProductUser> ProductUsers { get; set; }
        public Types Types { get; set; }
    }
}
