using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interface;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace eShop.Pages
{
    public class ViewProductModel : PageModel
    {
        private readonly IProduct _Procut;
        private readonly IType _Type;

        public ViewProductModel(IProduct product, IType type)
        {
            _Procut = product;
            _Type = type;
        }

        [BindProperty, Required]
        public string ImgUrl { get; set; }

        [BindProperty, Required]
        public string Name { get; set; }

        [BindProperty, Required]
        public decimal Price { get; set; }

        [BindProperty, Required]
        public string Brand { get; set; }

        [BindProperty, Required]
        public Types Type { get; set; }

        public IActionResult OnGet(int id)
        {
            Product prob = _Procut.FindProductById(id);

            ImgUrl = prob.ImageUrl;
            Name = prob.Name;
            Price = prob.Price;
            Brand = prob.Brand;
            Type = prob.Types;

            return Page();
        }


    }
}
