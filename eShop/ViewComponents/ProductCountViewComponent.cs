using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace eShop.ViewComponents
{
    public class ProductCountViewComponent : ViewComponent
    {
        private readonly IProductUser _ProductUser;

        public ProductCountViewComponent(IProductUser productUser)
        {
            _ProductUser = productUser;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _ProductUser.GetProductAsync((int)HttpContext.Session.GetInt32("id")));
    }
}
