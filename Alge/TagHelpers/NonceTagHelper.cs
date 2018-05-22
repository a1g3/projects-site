using Alge.Interfaces.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Alge.TagHelpers
{
    [HtmlTargetElement("script", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("style", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("link", Attributes = "asp-add-nonce")]
    public class NonceTagHelper : TagHelper
    {
        private readonly INonceService NonceService;
        [HtmlAttributeName("asp-add-nonce")]
        public bool AddNonce { get; set; }

        public NonceTagHelper(INonceService nonceService)
        {
            NonceService = nonceService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (AddNonce) { output.Attributes.Add("nonce", NonceService.GetNonce()); }
            base.Process(context, output);
        }
    }
}
