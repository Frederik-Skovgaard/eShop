using DataLayer.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace eShop.TagHelpers
{
    public class TotalPrice : TagHelper
    {
        public List<ProductUser> ProductUsers { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            decimal price = 0;
            output.TagName = "TotalPrice";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();

            foreach (var item in ProductUsers)
            {
                price += (item.Product.Price * item.Quantity);
            }

            sb.AppendFormat($"Total {price} Kr.");

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }
}
