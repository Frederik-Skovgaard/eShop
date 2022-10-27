using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Areas.Administrator.Pages
{
    public class ViewUserModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("id") != 1)
            {
                return RedirectToPage("/HomePage", new { area = "" });
            }
            return Page();
        }
    }
}
