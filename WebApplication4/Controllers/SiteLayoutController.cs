using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class SiteLayoutController : SurfaceController
    {
        private const string PartilFolder = "~/Views/Partials/SiteLayout/";

        public ActionResult RenderIntro()
        {
            return PartialView($"{PartilFolder}_Intro.cshtml");
        }
        public ActionResult RenderTitleControls()
        {
            return PartialView($"{PartilFolder}_TitleControls.cshtml");
        }

        public ActionResult RenderFooter()
        {
            return PartialView($"{PartilFolder}_Footer.cshtml");
        }

        public ActionResult RenderHeader()
        {
            //List<NavigationListItem> nav = GetNavigationModelFromDatabase();
            var nav = GetObjectFromCache<List<NavigationListItem>>("mainNav", 5, GetNavigationModelFromDatabase);
            return PartialView($"{PartilFolder}_Header.cshtml", nav);
        }
        //public ActionResult RenderTopNavigation()
        //{
        //    List<NavigationListItem> nav = GetNavigationModelFromDatabase();
        //    return PartialView(PartilFolder + "_TopNavigation.cshtml", nav);
        //}
        private List<NavigationListItem> GetNavigationModelFromDatabase()
        {
            const int homePagePositionInPath = 1;
            var homePageId = int.Parse(CurrentPage.Path.Split(',')[homePagePositionInPath]);

            var homePage = Umbraco.Content(homePageId);
            var nav = new List<NavigationListItem>
            {
                new NavigationListItem(new NavigationLink(homePage.Url, homePage.Name))
            };
            nav.AddRange(GetChildNavigationList(homePage));
            return nav;
        }

        private List<NavigationListItem> GetChildNavigationList(IPublishedContent page)
        {
            var childPages = page.Children.Where("Visible");
            if (childPages == null)
            {
                return new List<NavigationListItem>();
            }

            if (!childPages.Any())
            {
                return new List<NavigationListItem>();
            }

            var result = new List<NavigationListItem>();

            foreach (var childPage in childPages)
            {
                var listItem = new NavigationListItem(new NavigationLink(childPage.Url, childPage.Name))
                {
                    Items = GetChildNavigationList(childPage)
                };

                result.Add(listItem);

            }

            return result;
        }

        private static T GetObjectFromCache<T>(string cacheItemName, int cacheTimeInMinutes, Func<T> objectSettingFunction)
        {
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = (T)cache[cacheItemName];

            if (cachedObject != null)
            {
                return cachedObject;
            }

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes)
            };
            cachedObject = objectSettingFunction();
            cache.Set(cacheItemName, cachedObject, policy);

            return cachedObject;
        }
    }
}
