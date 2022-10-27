using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace eShop.TagHelpers
{
    public class TotalPriceProb : TagHelper
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "TotalPriceProb";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat($"Total {Price * Quantity} Kr.");

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }
}
