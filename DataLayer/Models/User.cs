using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }

        public bool IsDeleted { get; set; } = false;

        public int? UserInformationId { get; set; }
        public int RoleId { get; set; }

        public List<ProductUser> ProductUsers { get; set; }
        public List<PaymentMethodUser> PaymentMethod { get; set; }

        public UserInformation UserInformation { get; set; }
        public Role Role { get; set; }
    }
}
