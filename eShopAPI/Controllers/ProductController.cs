using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace eShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProduct _Product;

        public ProductController(IProduct product)
        {
            _Product = product;
        }


        /// <summary>
        /// GET: List of products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var list = _Product.GetProductAPI();

                if (list is null)
                {
                    return NotFound();
                }
                return Ok(list);
            }
            catch (Exception) { return StatusCode(400); }
        }

        /// <summary>
        /// POST: Creates a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                _Product.AddEntity(product);
                return Ok($"Product {product.Name} has been created...");
            }
            catch (Exception) { return StatusCode(400); }
        }

        /// <summary>
        /// PUT: Update product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Edit(Product product)
        {
            if (product.ProductId == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, "Id is null or 0");

            try
            {
                _Product.UpdateEntit(product);
                return Ok($"Product {product.Name} has been updated...");
            }
            catch (Exception) { return StatusCode(400); }
        }

        /// <summary>
        /// DELETE: Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (id == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, "Id is null or 0");
           
            try
            {
                var p = _Product.FindProductById(id);

                p.IsDeleted = true;
                _Product.UpdateEntit(p);

                return Ok($"Product {p.Name} has been deleted...");
            }
            catch (Exception) { return StatusCode(400); }
        }
    }
}
