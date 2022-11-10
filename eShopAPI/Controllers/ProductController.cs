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
        private readonly IProduct  _Product;

        public ProductController(IProduct product)
        {
            _Product = product;
        }


        /// <summary>
        /// GET: List of products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Product> GetItems() => _Product.GetProducts();

        /// <summary>
        /// GET: Find product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("item/{id}")]
        public IActionResult GetItemById(int id)
        {
            var prop = _Product.FindProductById(id);

            if (prop == null)
            {
                return NotFound();
            }

            return Ok(prop);
        }


        /// <summary>
        /// POST: Creates a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public IActionResult Create(Product product)
        {
            try
            {
                product = _Product.AddProduct(product);
                return Created($"/api/{product.ProductId}", product);
            }
            catch (Exception) { return StatusCode(400); }
        }

        /// <summary>
        /// PUT: Update product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public IActionResult Update(Product product)
        {
            if (product.ProductId == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, "Id is null or 0");

            try
            {
                _Product.UpdateEntit(product);
                return Accepted($"item({product.ProductId}", product);
            }
            catch (Exception) { return NotFound(); }
        }

        /// <summary>
        /// DELETE: Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
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
