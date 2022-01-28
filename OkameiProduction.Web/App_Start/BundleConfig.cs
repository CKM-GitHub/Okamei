using System.Web;
using System.Web.Optimization;

namespace OkameiProduction.Web
{
    public class BundleConfig
    {
        // バンドルの詳細については、https://go.microsoft.com/fwlink/?LinkId=301862 を参照してください
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jquery-{version}.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/lib/bootstrap.bundle.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/menu").Include(
                        "~/Scripts/lib/SweetAlert2.js",
                        "~/Scripts/Common.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                        "~/Scripts/lib/SweetAlert2.js",
                        "~/Scripts/lib/tilt.jquery.js"
                        ));


            bundles.Add(new StyleBundle("~/Content/loginCss").Include(
                        "~/Content/lib/bootstrap.css",
                        "~/Content/login.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/Css").Include(
                        "~/Content/lib/bootstrap.css",
                        "~/Content/menu.css",
                        "~/Content/customize.css"
                        ));

        }
    }
}
