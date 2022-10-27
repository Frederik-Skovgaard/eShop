using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using ServiceLayer.Repository;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace eShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUser _User;

        public IndexModel(IUser user)
        {
            _User = user;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPostLogin()
        {
            if (_User.LoginUser(Username, Password))
            {
                User user = _User.GetUserIdAndRole(Username, Password);
                HttpContext.Session.SetInt32("id", user.UserId);
                HttpContext.Session.SetInt32("role", user.RoleId);

                return Redirect("/HomePage");
            }
            else
                return Page();
        }

    }
}