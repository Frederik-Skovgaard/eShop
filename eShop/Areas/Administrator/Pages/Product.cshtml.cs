using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interface;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace eShop.Areas.Administrator.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProduct _Procut;
        private readonly IType _Type;

        public ProductModel(IProduct product, IType type)
        {
            _Procut = product;
            _Type = type;
        }

        //---------------------Collection Properties---------------------

        [BindProperty]
        public List<Product> Products { get; set; } = new List<Product>();

        [BindProperty]
        public List<Types> Types { get; set; } = new List<Types>();

        //---------------------Create Properties---------------------

        [BindProperty, Required]
        public string Name { get; set; }

        [BindProperty, Required]
        public decimal Price { get; set; }

        [BindProperty, Required]
        public string Brand { get; set; }

        [BindProperty, Required]
        public int TypeId { get; set; }

        [BindProperty, Required]
        public string TypeName { get; set; }

        //---------------------Search/Pagination---------------------        

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? TypeGenre { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int totalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));


        //---------------------Method---------------------

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("id") != 1)
            {
                return RedirectToPage("/HomePage", new { area = "" });
            }

            var genreQuery = from m in _Procut.GetProducts()
                             orderby m.Name
                             select m.Name;

            var products = from m in _Procut.GetProducts()
                           select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(TypeGenre))
            {
                products = products.Where(s => _Type.FindTpyeById(s.TypesId).Name == TypeGenre);
            }

            Genres = new SelectList(genreQuery.Distinct().ToList());

            Products = _Procut.GetPaginatedResualt(products.ToList(), CurrentPage, PageSize);
            Count = products.Count();

            Types = _Type.GetTypes();

            return Page();
        }

        public IActionResult OnPostCreate(string text)
        {
            if (text != "Type")
            {
                Product p = new()
                {
                    Name = Name,
                    Price = Price,
                    Brand = Brand,
                    TypesId = TypeId
                };
                _Procut.AddEntity(p);
            }
            else
            {
                Types p = new()
                {
                    Name = TypeName
                };
                _Procut.AddEntity(p);
            }       

            Types = _Type.GetTypes();
            Products = _Procut.GetProducts();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            Product p = _Procut.FindProductById(id);

            p.IsDeleted = true;
            _Procut.UpdateEntit(p);

            Types = _Type.GetTypes();
            Products = _Procut.GetProducts();
            return Page();
        }
    }
}
