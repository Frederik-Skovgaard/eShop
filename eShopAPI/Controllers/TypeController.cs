using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace eShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeController : Controller
    {
        private readonly IType _Type;

        public TypeController(IType type)
        {
            _Type = type;
        }

        /// <summary>
        /// GET: List of products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Types> GetItems() => _Type.GetTypes();

        [HttpGet]
        [Route("item/{id}")]
        public IActionResult GetItemById(int id)
        {
            var prop = _Type.FindTpyeById(id);

            if (prop == null)
            {
                return NotFound();
            }

            return Ok(prop);
        }
    }
}
