using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.Interface;

namespace eShop.Areas.Administrator.Pages
{
    public class UserModel : PageModel
    {
        private readonly IUser _User;
        private readonly IRole _Role;

        public UserModel(IUser user, IRole role)
        {
            _User = user;
            _Role = role;
        }

        [BindProperty]
        public List<User> Users { get; set; } = new List<User>();

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

            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            User u = _User.GetUser(id);

            u.IsDeleted = true;
            _User.UpdateEntit(u);

            Users = _User.GetUsers();
            return Page();
        }
    }
}
