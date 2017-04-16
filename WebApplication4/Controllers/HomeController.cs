using System.Web.Mvc;
using Umbraco.Web.Mvc;
using WebApplication4.Models;

using System.Collections.Generic;
using Umbraco.Web;
using Umbraco.Core.Models;

using System.Linq;
using Archetype.Models;



namespace WebApplication4.Controllers
{
    public class HomeController : SurfaceController
    {
        private const string PartialViewFolder = "~/Views/Partials/Home/";

        public ActionResult RenderFeatured()
        {
            var model = new List<FeaturedItem>();
            var homePage = Umbraco.TypedContentAtRoot().First();
            var featuredItems = homePage.GetPropertyValue<ArchetypeModel>("featuredItems");

            foreach (var item in featuredItems)
            {
                var imageId = item.GetValue<int>("image");
                var mediaItem = Umbraco.Media(imageId);
                string imageUrl = mediaItem.Url;

                var pageId = item.GetValue<int>("page");
                var linkedToPage = Umbraco.TypedContent(pageId);
                var linkUrl = linkedToPage.Url;

                model.Add(new FeaturedItem
                {
                    Name = item.GetValue<string>("name"),
                    Category = item.GetValue<string>("category"),
                    ImageUrl = imageUrl,
                    LinkUrl = linkUrl
                });
            }

            return PartialView(PartialViewFolder + "_Featured.cshtml", model);
        }
        public ActionResult RenderServices()
        {
            return PartialView(PartialViewFolder + "_Services.cshtml");
        }

        public ActionResult RenderBlog()
        {
            return PartialView(PartialViewFolder + "_Blog.cshtml");
        }

        public ActionResult RenderClients()
        {
            return PartialView(PartialViewFolder + "_Clients.cshtml");
        }
    }
}
