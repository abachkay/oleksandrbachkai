using System.Web.Optimization;

namespace oleksandrbachkai.App_Start
{
    public class BundleConfig
    {        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery/jquery-{version}.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/libs/bootstrap/bootstrap.js",
                      "~/Scripts/libs/bootstrap/respond.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                     "~/Scripts/libs/angular/angular.js"                   
            ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/Scripts/app/app.module.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/styles/bootstrap/bootstrap.css",
                      "~/Content/styles/angular/angular-material.css",
                      "~/Content/styles/Site.css"
            ));
        }
    }
}
