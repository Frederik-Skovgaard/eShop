using BlazorWebShop;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace eShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment env;

        public UploadController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpPost]
        public async Task Post([FromBody] ImageFile[] files)
        {
            foreach (var file in files)
            {
                var buf = Convert.FromBase64String(file.base64data);
                await System.IO.File.WriteAllBytesAsync(env.ContentRootPath + Path.DirectorySeparatorChar + Guid.NewGuid().ToString("N") + "-" + file.fileName, buf);
            }
        }
    }
}
