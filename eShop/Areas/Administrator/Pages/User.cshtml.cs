using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServiceLayer.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace eShop.Areas.Administrator.Pages
{
    public class UserModel : PageModel
    {
        private readonly IUser _User;
        private readonly IRole _Role;
        private readonly IUserInfo _Info;

        public UserModel(IUser user, IRole role, IUserInfo info)
        {
            _User = user;
            _Role = role;
            _Info = info;
        }

        //---------------------Collection Properties---------------------

        [BindProperty]
        public List<User> Users { get; set; } = new List<User>();

        [BindProperty]
        public List<Role> Roles { get; set; } = new List<Role>();

        //---------------------Create Properties---------------------

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


        //---------------------Search/Pagination---------------------   

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? RoleGenere { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int totalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));


        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("id") != 1)
            {
                return RedirectToPage("/HomePage", new { area = "" });
            }

            var genreQuery = from m in _User.GetUsers()
                             orderby m.Role.Name
                             select m.Role.Name;

            var users = from m in _User.GetUsers()
                           select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                users = users.Where(s => s.UserName.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(RoleGenere))
            {
                users = users.Where(s => _Role.GetRoleById(s.RoleId).Name == RoleGenere);
            }

            Genres = new SelectList(genreQuery.Distinct().ToList());

            Users = _User.GetPaginatedResualt(users.ToList(), CurrentPage, PageSize);
            Count = users.Count();

            Roles = _Role.GetRoles();

            return Page();
        }

        //---------------------Methods---------------------  

        public IActionResult OnPostCreate(string text)
        {
            if (text != "Role")
            {
                User i = new()
                {
                    UserName = UserName,
                    Password = Password,
                    Email = Email,
                    RoleId = RoleId
                };

                if (!string.IsNullOrWhiteSpace(Street) && !string.IsNullOrWhiteSpace(City) && Zip != 0)
                {
                    UserInformation u = new()
                    {
                        Street = Street,
                        City = City,
                        ZipCode = Zip
                    };

                    _User.AddEntity(u);

                    u = _Info.GetUserInfoByContext(Street, City, Zip);

                    i.UserInformationId = u.UserInformationId;
                }
              
                _User.AddEntity(i);
            }
            else
            {
                Role i = new()
                {
                    Name = Name
                };
                _User.AddEntity(i);
            }

            Roles = _Role.GetRoles();
            Users = _User.GetUsers();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            User u = _User.GetUser(id);

            u.IsDeleted = true;
            _User.UpdateEntit(u);

            Roles = _Role.GetRoles();
            Users = _User.GetUsers();
            return Page();
        }

        public IActionResult OnPostDeleteRole(int id)
        {
            Role r = _Role.GetRoleById(id);

            _Role.DeleteEntit(r);

            Roles = _Role.GetRoles();
            Users = _User.GetUsers();
            return Page();
        }

        public IActionResult OnPostFilter()
        {
            OnGet();
            return Page();
        }

    }
}
