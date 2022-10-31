using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interface;
using ServiceLayer.Service;
using System.ComponentModel.DataAnnotations;

namespace eShop.Pages
{
    public class UserCartModel : PageModel
    {
        private readonly IProductUser _ProductUser;
        private readonly IUser _User;
        private readonly IPaymentMethodsUser _PaymentMethodUser;
        private readonly IPaymentMethod _PaymentMethod;

        private readonly IMail _Mail;

        public UserCartModel(IProductUser productUser, IPaymentMethodsUser paymentMethodsUser, IUser user, IPaymentMethod paymentMethod, IMail mail)
        {
            _ProductUser = productUser;
            _PaymentMethod = paymentMethod;
            _User = user;
            _PaymentMethod = paymentMethod;
            _PaymentMethodUser = paymentMethodsUser;
            _Mail = mail;
        }


        [BindProperty]
        public string NummbersOnly { get; set; } = "return (event.charCode !=8 && event.charCode ==0 || (event.charCode >= 48 && event.charCode <= 57));";


        //---------------------List porperties---------------------

        [BindProperty]
        public List<ProductUser> Products { get; set; }

        [BindProperty]
        public List<int> Quantitys { get; set; } = new List<int>();

        [BindProperty]
        public List<PaymentMethodUser> PaymentMethods { get; set; }

        //---------------------Properties for payment method---------------------
        [BindProperty, Required]
        public string PaymentName { get; set; }

        [BindProperty, Required, MaxLength(255, ErrorMessage ="Name is too long")]
        public string Name { get; set; }

        [BindProperty, Required, MaxLength(19), MinLength(16, ErrorMessage = "Must be atlest 16 digits long")]
        public string CreditNr { get; set; }

        [BindProperty, Required]
        public int Month { get; set; }

        [BindProperty, Required]
        public int Year { get; set; }

        [BindProperty, Required]
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

            PaymentMethods = _PaymentMethodUser.GetPaymentMethods(id);
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

                if (prob.Quantity <= 0) _ProductUser.DeleteEntit(prob); else  _ProductUser.UpdateEntit(prob);
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

            if (User.PaymentMethod == null)
            {
                PaymentMethod payment = new()
                {
                    PaymentMethodName = PaymentName,
                    Name = Name,
                    AccountNr = CreditNr,
                    BackNr = Convert.ToInt32(CVV),
                    ExpirationDate = DateTime.Parse(date)
                };

                _User.AddEntity(payment);

                var p = _PaymentMethod.GetPaymentMethod(Name, CreditNr);

                PaymentMethodUser payUser = new()
                {
                    PaymentMethodId = p.PaymentMethodId,
                    UserId = userId
                };

                _User.AddEntity(payUser);                
            }


            if (User.UserInformationId == null)
            {
                UserInformation user = new()
                {
                    Street = Street,
                    City = City,
                    ZipCode = Convert.ToInt32(Zip)
                };

                _ProductUser.AddEntity(user);
            }

            foreach (var item in _ProductUser.GetProductcart(userId))
            {
                _ProductUser.DeleteEntit(item);
            }

            _Mail.SendCheck("frederik.skovgaard@gmail.com");

            return RedirectToPage("/Success");
        }
    }
}
