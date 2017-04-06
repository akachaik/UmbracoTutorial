using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core.Models;
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
            const int HOME_PAGE_POSITION_IN_PATH = 1;
            int homePageId = int.Parse(CurrentPage.Path.Split(',')[HOME_PAGE_POSITION_IN_PATH]);

            IPublishedContent homePage = Umbraco.Content(homePageId);
            List<NavigationListItem> nav = new List<NavigationListItem>();
            nav.Add(new NavigationListItem(new NavigationLink(homePage.Url, homePage.Name)));
            nav.AddRange(GetChildNavigationList(homePage));
            return nav;
        }

        private List<NavigationListItem> GetChildNavigationList(dynamic page)
        {
            List<NavigationListItem> listItems = null;
            var childPages = page.Children.Where("Visible");
            if (childPages != null && childPages.Any() && childPages.Count() > 0)
            {
                listItems = new List<NavigationListItem>();
                foreach (var childPage in childPages)
                {
                    NavigationListItem listItem = new NavigationListItem(new NavigationLink(childPage.Url, childPage.Name));
                    listItem.Items = GetChildNavigationList(childPage);
                    listItems.Add(listItem);
                }
            }
            else
            {
                listItems = new List<NavigationListItem>();
            }
            return listItems;
        }

        private static T GetObjectFromCache<T>(string cacheItemName, int cacheTimeInMinutes, Func<T> objectSettingFunction)
        {
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = (T)cache[cacheItemName];
            if (cachedObject == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);
                cachedObject = objectSettingFunction();
                cache.Set(cacheItemName, cachedObject, policy);
            }
            return cachedObject;
        }
    }
}
