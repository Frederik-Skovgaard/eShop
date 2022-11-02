using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using DataLayer.Models;

namespace eShopAPI.Formatters
{
    public class VcardOutputFormatter : TextOutputFormatter
    {
        public VcardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            return typeof(Product).IsAssignableFrom(type) ||
                typeof(IEnumerable<Product>).IsAssignableFrom(type);
        }

        public override async Task WriteResponseBodyAsync(
            OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<VcardOutputFormatter>>();
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Product> products)
            {
                foreach (var product in products)
                {
                    FormatVcard(buffer, product, logger);
                }
            }
            else
            {
                FormatVcard(buffer, (Product)context.Object, logger);
            }

            await httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatVcard(
            StringBuilder buffer, Product product, ILogger logger)
        {
            buffer.AppendLine("BEGIN:PRODUCT");
            buffer.AppendLine($"ID:{product.ProductId}");
            buffer.AppendLine($"Name:{product.Name}");
            buffer.AppendLine($"Price:{product.Price}");
            buffer.AppendLine($"Brand:{product.Brand}");
            buffer.AppendLine($"ImageUrl:{product.ImageUrl}");
            buffer.AppendLine($"IsDeleted:{product.IsDeleted}");
            buffer.AppendLine("END:PRODUCT");

            logger.LogInformation("Writing {Name} {Brand}",
                product.Name, product.Brand);
        }

    }
}
