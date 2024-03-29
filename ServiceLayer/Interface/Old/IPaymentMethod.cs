﻿using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface.Old
{
    public interface IPaymentMethod
    {
        //CRUD
        void AddEntity<T>(T entry) where T : class;
        void UpdateEntit<T>(T entry) where T : class;

        PaymentMethod GetPaymentMethod(string name, string account);
    }
}
