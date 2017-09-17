using System.Web.Mvc;
using System.Web.Routing;

namespace oleksandrbachkai.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {            
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );            
            routes.MapRoute(
                "Default",
                "{*url}",
                new { controller = "Home", action = "Index" }
            );
        }
    }
}
