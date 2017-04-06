using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace WebApplication4.Controllers
{
    public class HomeController : SurfaceController
    {
        private const string PartialViewFolder = "~/Views/Partials/Home/";

        public ActionResult RenderFeatured()
        {
            return PartialView(PartialViewFolder + "_Featured.cshtml");
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
