using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interface;
using System.ComponentModel.DataAnnotations;

namespace eShop.Areas.Administrator.Pages
{
    public class ProductEditModel : PageModel
    {
        private readonly IProduct _Procut;
        private readonly IType _Type;

        public ProductEditModel(IProduct product, IType type)
        {
            _Procut = product;
            _Type = type;
        }

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


            Types = _Type.GetTypes();

            return Page();
        }

        public IActionResult OnPostUpdate()
        {
            Product prob = new()
            {
                Name = Name,
                Price = Price,
                Brand = Brand,
                TypesId = TypeId
            };

            _Procut.UpdateEntit(prob);

            return RedirectToPage("/Product");
        }
    }
}
