﻿using System;
using System.Collections.Generic;
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
        public int CardId { get; set; }
        public int UserInformationId { get; set; }
        public int RoleId { get; set; }

        public Cart Cart { get; set; }
        public UserInformation UserInformation { get; set; }
        public Role Role { get; set; }
    }
}
