using Archetype.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using WebApplication4.Models;

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
            var homePage = Umbraco.TypedContentAtRoot().First();
            var model = new LatestBlogPosts()
            {
                Title = homePage.GetPropertyValue<string>("latestBlogPostsTitle"),
                Introduction = homePage.GetPropertyValue<string>("latestBlogPostsIntroduction")
            };

            return PartialView(PartialViewFolder + "_Blog.cshtml", model);
        }

        public ActionResult RenderTestimonials()
        {
            var homePage = Umbraco.TypedContentAtRoot().First();

            var quotes = new List<TestimonialQuote>();
            var testimonialsList = homePage.GetPropertyValue<ArchetypeModel>("testimonialList");
            foreach (var quote in testimonialsList)
            {
                quotes.Add(new TestimonialQuote
                {
                    Name = quote.GetValue<string>("name"),
                    Quote =  quote.GetValue<string>("quote")
                });
            }

            var model = new Testimonials
            {
                Title = homePage.GetPropertyValue<string>("testimonialsTitle"),
                Introduction = homePage.GetPropertyValue<string>("testimonialsIntroduction"),
                Quotes = quotes
            };

            return PartialView(PartialViewFolder + "_Testimonials.cshtml", model);
        }
    }
}
