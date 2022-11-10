using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Model
{
    public class UserInformation
    {
        public int UserInformationId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }

        public User User { get; set; }
    }
}
