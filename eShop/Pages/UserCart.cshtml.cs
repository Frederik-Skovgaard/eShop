using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interface;
using System.ComponentModel.DataAnnotations;

namespace eShop.Pages
{
    public class UserCartModel : PageModel
    {
        private readonly IProductUser _ProductUser;
        private readonly IUser _User;
        private readonly IPaymentMethod _PaymentMethod;

        public UserCartModel(IProductUser productUser, IPaymentMethod paymentMethod, IUser user)
        {
            _ProductUser = productUser;
            _PaymentMethod = paymentMethod;
            _User = user;
        }


        //---------------------List porperties---------------------
        [BindProperty]
        public List<ProductUser> Products { get; set; }

        [BindProperty]
        public List<int> Quantitys { get; set; } = new List<int>();

        [BindProperty]
        public List<PaymentMethod> PaymentMethods { get; set; }

        //---------------------Properties for payment method---------------------
        [BindProperty, Required]
        public string PaymentName { get; set; }

        [BindProperty, Required, MaxLength(255, ErrorMessage ="Name is too long")]
        public string Name { get; set; }

        [BindProperty, Required, MaxLength(19), MinLength(16, ErrorMessage = "Must be atlest 16 digits long")]
        public int CreditNr { get; set; }

        [BindProperty, Required, MaxLength(2)]
        public int Month { get; set; }

        [BindProperty, Required, MaxLength(4)]
        public int Year { get; set; }

        [BindProperty, Required, MaxLength(4), MinLength(3, ErrorMessage = "Must be atlest 3 digits long")]
        public int CVV { get; set; }

        //---------------------UserInformation properties---------------------
        [BindProperty, Required, MaxLength(85, ErrorMessage = "Must be less then 85 characters")]
        public string Street { get; set; }

        [BindProperty, Required, MaxLength(85, ErrorMessage = "Must be less then 85 characters")]
        public string City { get; set; }

        [BindProperty, Required, MaxLength(4)]
        public int Zip { get; set; }

        //---------------------Properties---------------------

        public User User { get; set; }

        //---------------------Methods---------------------

        public void OnGet(int id)
        {
            Products = _ProductUser.GetProductcart(id);

            foreach (var item in Products)
            {
                Quantitys.Add(item.Quantity);
            }

            PaymentMethods = _PaymentMethod.GetPaymentMethods(id);
            User = _User.GetUser(id);
        }

        public IActionResult OnPostUpdate()
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            Products = _ProductUser.GetProductcart(id);

            List<int> ints = Quantitys;

            foreach (var prob in Products)
            {
                for (int i = 0; i < ints.Count(); i++)
                {
                    prob.Quantity = ints[i];
                    ints.Remove(ints[i]);
                    break;
                }

                if (prob.Quantity !<= 0) _ProductUser.UpdateEntit(prob); else _ProductUser.DeleteEntit(prob);
            }

            OnGet(id);
            return Page();
        }

        public IActionResult OnPostRemove(int id)
        {
            int userId = (int)HttpContext.Session.GetInt32("id");

            ProductUser p = _ProductUser.FindProductUserById(id, userId);

            if (p != null)
            {
                _ProductUser.DeleteEntit(p);
            }

            OnGet(userId);
            return Page();
        }

        public IActionResult OnPostCheckOut()
        {
            string date = $"{Year}/{Month}/1";
            int userId = (int)HttpContext.Session.GetInt32("id");
            OnGet(userId);

            if (User.PaymentMethod.Count != 0)
            {
                PaymentMethod payment = new()
                {
                    PaymentMethodName = PaymentName,
                    Name = Name,
                    AccountNr = CreditNr,
                    BackNr = CVV,
                    ExpirationDate = DateTime.Parse(date)
                };

                _PaymentMethod.AddEntity(payment);
            }
            if (User.UserInformationId != null)
            {
                UserInformation user = new()
                {
                    Street = Street,
                    City = City,
                    ZipCode = Zip
                };

                _ProductUser.AddEntity(user);
            }


            
            return RedirectToPage("/Success");
        }
    }
}
