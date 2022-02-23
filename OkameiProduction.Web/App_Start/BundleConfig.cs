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
            bundles.Add(new ScriptBundle("~/bundles/lib/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/lib/jquery").Include(
                        "~/Scripts/lib/jquery-{version}.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/lib/bootstrap").Include(
                        "~/Scripts/lib/bootstrap.bundle.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                        "~/Scripts/SweetAlert2.js",
                        "~/Scripts/tilt.jquery.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/SweetAlert2.js",
                        "~/Scripts/typeahead.js",
                        "~/Scripts/Common.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/InputBukkenShousai").Include(
                        "~/Scripts/InputBukkenShousai.js",
                        "~/Scripts/InputBukkenShousaiHiuchi.js",
                        "~/Scripts/InputBukkenShousaiTekakou.js",
                        "~/Scripts/ModalForm.js"
                        ));

            //css
            //bundles.Add(new StyleBundle("~/Content/lib/Css").Include(
            //            "~/Content/lib/bootstrap.css"
            //            ));
            bundles.Add(new StyleBundle("~/Content/logincss").Include(
                        "~/Content/login.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/sitecss").Include(
                        "~/Content/typeahead.css",
                        "~/Content/menu.css",
                        "~/Content/customize.css",
                        "~/Content/inputsize.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/InputBukkenShousaiCss").Include(
                        "~/Content/InputBukkenShousai.css",
                        "~/Content/InputBukkenShousaiHiuchi.css",
                        "~/Content/InputBukkenShousaiTekakou.css",
                        "~/Content/ModalForm.css"
                        ));

        }
    }
}
