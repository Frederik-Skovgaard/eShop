using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.Areas.Administrator.Pages
{
    public class UserEditModel : PageModel
    {
        private readonly IUser _User;
        private readonly IRole _Role;
        private readonly IUserInfo _Info;

        public UserEditModel(IUser user, IRole role, IUserInfo info)
        {
            _User = user;
            _Role = role;
            _Info = info;
        }

        [BindProperty]
        public List<Role> Roles { get; set; } = new List<Role>();

        [BindProperty]
        public int ID { get; set; }

        [BindProperty]
        public string NummbersOnly { get; set; } = "return (event.charCode !=8 && event.charCode ==0 || (event.charCode >= 48 && event.charCode <= 57));";

        //---------------------Properties---------------------

        [BindProperty, Required]
        public string UserName { get; set; }

        [BindProperty, Required]
        public string Password { get; set; }

        [BindProperty, Required]
        public string Email { get; set; }

        [BindProperty, Required]
        public int RoleId { get; set; }

        [BindProperty, Required, MaxLength(85, ErrorMessage = "Must be less then 85 characters")]
        public string Street { get; set; }

        [BindProperty, Required, MaxLength(85, ErrorMessage = "Must be less then 85 characters")]
        public string City { get; set; }

        [BindProperty, Required]
        public int Zip { get; set; }


        [BindProperty, Required]
        public string Name { get; set; }

        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetInt32("id") != 1)
            {
                return RedirectToPage("/HomePage", new { area = "" });
            }

            User user = _User.GetUser(id);

            UserName = user.UserName;
            Password = user.Password;
            Email = user.Email;
            RoleId = user.RoleId;

            if (user.UserInformationId != null)
            {
                Street = user.UserInformation.Street;
                City = user.UserInformation.City;
                Zip = user.UserInformation.ZipCode;
            }
            else
            {
                Street = "Inoformation not disclosed";
                City = "Inoformation not disclosed";
                Zip = 0000;
            }

            ID = id;

            Roles = _Role.GetRoles();

            return Page();
        }

        public IActionResult OnPostUpdate(int id)
        {
            User user = _User.GetUser(id);

            user.UserName = UserName;
            user.Password = Password;
            user.Email = Email;
            user.RoleId = RoleId;
            user.UserInformation.Street = Street;
            user.UserInformation.City = City;
            user.UserInformation.ZipCode = Zip;

            _User.UpdateEntit(user);

            return RedirectToPage("/User");
        }
    }
}
