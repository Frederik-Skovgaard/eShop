using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.Interface;

namespace eShop.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProduct _Product;
        private readonly IType _Type;
        private readonly IProductUser _ProductUser;

        public IndexModel(IProduct product, IType type, IProductUser productUser)
        {
            _Product = product;
            _Type = type;
            _ProductUser = productUser;
        }


        //------------------------------------Properties------------------------------------
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? GenreType { get; set; }
        public SelectList? GenreBrand { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? TypeGenre { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BrandGenre { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 3;

        public int totalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<Product> Data { get; set; }

        public bool IsTrue { get; set; }

        [BindProperty]
        public int ID { get; set; }

        //------------------------------------Methods------------------------------------

        public void OnGet(bool order)
        {
            var genreType = from m in _Product.GetProducts()
                             orderby m.Name
                             select m.Types.Name;

            var genreBrand = from m in _Product.GetProducts()
                             orderby m.Brand
                             select m.Brand;

            var products = from m in _Product.GetProducts()
                           select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                IsTrue = true;
                products = products.Where(s => s.Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(TypeGenre))
            {
                IsTrue = true;
                products = products.Where(s => _Type.FindTpyeById(s.TypesId).Name == TypeGenre);
            }

            if (!string.IsNullOrEmpty(BrandGenre))
            {
                IsTrue = true;
                products = products.Where(s => s.Brand == BrandGenre);
            }

            GenreType = new SelectList(genreType.Distinct().ToList());
            GenreBrand = new SelectList(genreBrand.Distinct().ToList());

            var i = order == true ? Data = _Product.GetPaginatedResualt(products.ToList(), CurrentPage, PageSize).OrderByDescending(x => x.ProductId).ToList() : Data = _Product.GetPaginatedResualt(products.ToList(), CurrentPage, PageSize).OrderBy(x => x.ProductId).ToList();
            var _ = IsTrue == true ? Count = products.Count() : Count = _Product.GetProducts().Count();

            IsTrue = false;
        }

        public IActionResult OnPostOrder()
        {
            OnGet(false);
            return Page();
        }

        public IActionResult OnPostOrderDescending()
        {
            OnGet(true);
            return Page();
        }

        public IActionResult OnPostAdd(int id)
        {
            int userId = (int)HttpContext.Session.GetInt32("id");

            ProductUser prodUser = _ProductUser.FindProductUserById(id, userId);

            if (prodUser != null)
            {
                prodUser.Quantity = prodUser.Quantity += 1;
                _ProductUser.UpdateEntit(prodUser);
            }
            else
            {
                ProductUser p = new()
                {
                    UserId = userId,
                    ProductId = id,
                    Quantity = 1
                };

                _ProductUser.AddEntity(p);
            }
                
            OnGet(false);
            return Page();
        }

        public IActionResult OnPostFilter()
        {
            OnGet(false);
            return Page();
        }


    }
}
