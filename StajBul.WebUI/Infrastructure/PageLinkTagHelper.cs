using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StajBul.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory UrlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.UrlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound] //
        public ViewContext ViewContext { get; set; }

        public AnnouncementListAndPaginationModel PageModel { get; set; } //14. satirda [HtmlTargetElement("div", Attributes = "page-model")] yaptigmiz icin on tarafta <div page-model ="MODEL"> yaparak gonderdigim model bu degiskenin icine gelcek
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //cagrildigi yere burada html ciktisi uretiyoruz
            var urlHelper = UrlHelperFactory.GetUrlHelper(ViewContext);

            var result = new TagBuilder("div");

            for(int i=1; i <= PageModel.PaginationModel.TotalPages(); i++) //Toplam sayfa kadar 1-2-3-4 diye link olusturcaz
            {
                var tag = new TagBuilder("a");
                if(PageModel.UserProfileViewModel == null)
                {
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { page = i, isIntern = PageModel.CategoryFilterModel.IsIntern, categoryId = PageModel.CategoryFilterModel.CategoryId, searchedWords = PageModel.CategoryFilterModel.SearchedWords });
                }
                else
                {
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { page = i, id = PageModel.CategoryFilterModel.id, username = PageModel.CategoryFilterModel.username});
                }
                tag.InnerHtml.Append(i.ToString());
                if(i == PageModel.PaginationModel.CurrentPage)
                {
                    tag.AddCssClass("btn btn-primary page-item my-2 mx-1");
                }
                else
                {
                    tag.AddCssClass("btn btn-outline-dark page-item my-2 mx-1");
                }
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }


    }
}
