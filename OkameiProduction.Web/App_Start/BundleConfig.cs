using System.Web;
using System.Web.Optimization;

namespace OkameiProduction.Web
{
    public class BundleConfig
    {
        // バンドルの詳細については、https://go.microsoft.com/fwlink/?LinkId=301862 を参照してください
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/lib/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/lib/jquery.validate*"));

            // 開発と学習には、Modernizr の開発バージョンを使用します。次に、実稼働の準備が
            // 運用の準備が完了したら、https://modernizr.com のビルド ツールを使用し、必要なテストのみを選択します。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/lib/bootstrap.bundle.min.js",
            //          "~/Scripts/lib/SweetAlert2.js"
            //          ));

            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
                      "~/Scripts/lib/jquery-{version}.js",
                      "~/Scripts/lib/bootstrap.bundle.min.js",
                      "~/Scripts/lib/SweetAlert2.js",
                      "~/Scripts/lib/tilt.jquery.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/Menu").Include(
                      "~/Scripts/lib/jquery-{version}.js",
                      "~/Scripts/lib/bootstrap.bundle.min.js",
                      "~/Scripts/lib/SweetAlert2.js",
                      "~/Scripts/Common.js"
                      ));

            bundles.Add(new StyleBundle("~/Login/css").Include(
                      "~/Content/lib/bootstrap.min.css",
                      "~/Content/login.css"
                      ));

            bundles.Add(new StyleBundle("~/Menu/css").Include(
                      "~/Content/lib/bootstrap.min.css",
                      "~/Content/menu.css",
                      "~/Content/customize.css"
                      ));

        }
    }
}
