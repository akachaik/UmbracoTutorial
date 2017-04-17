using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Umbraco.Web;
using Umbraco.Web.Mvc;

using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class BlogController : SurfaceController
    {
        private const string PartialViewFolder = "~/Views/Partials/Blog/";

        public ActionResult RenderPostList()
        {
            var model = new List<BlogPreview>();
            var blogPage = Umbraco.TypedContentSingleAtXPath("//blog");

            foreach (var item in blogPage.Children.OrderByDescending(x => x.UpdateDate))
            {
                var imageId = item.GetPropertyValue<int>("articleImage");
                var mediaItem = Umbraco.Media(imageId);
                string imageUrl = mediaItem.Url;

                model.Add(new BlogPreview
                {
                    Name = item.Name,
                    Introduction = item.GetPropertyValue<string>("articleIntro"),
                    ImageUrl = imageUrl,
                    LinkUrl = item.Url
                });
            }

            return PartialView(PartialViewFolder + "_PostList.cshtml", model);

        }
    }
}
