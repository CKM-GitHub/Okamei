using System.Web;
using System.Web.Optimization;

namespace OkameiProduction.Web
{
    public class BundleConfig
    {
        // バンドルの詳細については、https://go.microsoft.com/fwlink/?LinkId=301862 を参照してください
        public static void RegisterBundles(BundleCollection bundles)
        {
            //js
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jquery-{version}.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/lib/bootstrap.bundle.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/SweetAlert2.js",
                        "~/Scripts/Common.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                        "~/Scripts/SweetAlert2.js",
                        "~/Scripts/tilt.jquery.js"
                        ));

            //css
            bundles.Add(new StyleBundle("~/Content/lib/css").Include(
                        "~/Content/lib/bootstrap.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/login/Css").Include(
                        "~/Content/login.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/common/Css").Include(
                        "~/Content/menu.css",
                        "~/Content/customize.css",
                        "~/Content/inputsize.css"
                        ));

        }
    }
}
