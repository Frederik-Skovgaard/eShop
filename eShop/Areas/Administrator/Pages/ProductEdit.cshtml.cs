using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interface;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace eShop.Areas.Administrator.Pages
{
    public class ProductEditModel : PageModel
    {
        private readonly IProduct _Procut;
        private readonly IType _Type;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public ProductEditModel(IProduct product, IType type, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _Procut = product;
            _Type = type;
            _environment = environment;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        //---------------------Collection Properties---------------------

        [BindProperty]
        public List<Types> Types { get; set; } = new List<Types>();

        //---------------------Edit Properties---------------------

        [BindProperty, Required]
        public string Name { get; set; }

        [BindProperty, Required]
        public decimal Price { get; set; }

        [BindProperty, Required]
        public string Brand { get; set; }

        [BindProperty, Required]
        public int TypeId { get; set; }

        [BindProperty]
        public string ImageUrl { get; set; }


        [BindProperty]
        public int ID { get; set; }

        [BindProperty]
        public string NummbersOnly { get; set; } = "return (event.charCode !=8 && event.charCode ==0 || (event.charCode >= 48 && event.charCode <= 57));";
        

        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetInt32("id") != 1)
            {
                return RedirectToPage("/HomePage", new { area = "" });
            }

            Product prob = _Procut.FindProductById(id);

            Name = prob.Name;
            Price = prob.Price;
            Brand = prob.Brand;
            TypeId = prob.TypesId;
            ImageUrl = prob.ImageUrl;
            ID = id;

            Types = _Type.GetTypes();

            return Page();
        }

        public async Task<IActionResult> OnPostUpdate(int id)
        {
            var file = Path.Combine(_environment.ContentRootPath, "wwwroot\\Images", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            Product prob = _Procut.FindProductById(ID);

            prob.Name = Name;
            prob.Price = Price;
            prob.Brand = Brand;
            prob.TypesId = TypeId;
            prob.ImageUrl = $"Images/{Upload.FileName}";

            _Procut.UpdateEntit(prob);

            return RedirectToPage("/Product");
        }
    }
}
