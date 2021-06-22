using System.Web;
using System.Web.Optimization;

namespace OnlineShoppingStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/vendor/jquery").IncludeDirectory(
                "~/Contents/vendor/jquery", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/vendor/js").IncludeDirectory(
                "~/Contents/vendor/bootstrap", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Contents/css/shop-homepage.css",
                      "~/Content/bootstrap.css",
                      "~/Content/fontawsome.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/vendor/bootstrap").IncludeDirectory(
                @"~/Contents/vendor/bootstrap", "*.css", true));
        }
    }
}
