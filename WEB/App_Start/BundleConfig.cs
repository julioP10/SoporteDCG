using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace WEB
{
    public class UserDefinedBundleOrderer : IBundleOrderer
    {
        public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Dist/Generics/js/modernizr-2.6.2.js", "~/Dist/Generics/js/jquery-3.1.1.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                        "~/Dist/Generics/js/ajax.js"));
            bundles.Add(new ScriptBundle("~/bundles/dataTable").Include(
                        "~/Dist/Generics/js/simpledataTable.js"));

            // App SCRIPTS AND CSS
            ScriptBundle libsJSApp = new ScriptBundle("~/bundles/jsApp");
            libsJSApp.Orderer = new UserDefinedBundleOrderer();
            libsJSApp.Include(
                 "~/Dist/Generics/js/bootstrap.min.js",
                 "~/Dist/Generics/js/jquery-ui.min.js",
                 "~/Dist/Generics/js/perfect-scrollbar.min.js",
                 "~/Dist/App/Js/Script.js",
                 "~/Dist/Generics/js/bootstrap-switch-tags.js",
                 "~/Dist/Generics/js/sweetalert2.js",
                 "~/Dist/Generics/js/es6-promise-auto.min.js",
                 "~/Dist/App/Js/Page.js"
            );
            bundles.Add(libsJSApp);

            bundles.Add(new StyleBundle("~/Content/cssApp").Include(
               "~/Dist/Generics/css/bootstrap.min.css",
               "~/Dist/Generics/css/font-awesome.min.css",
               "~/Dist/Generics/css/themify-icons.css",
               "~/Dist/Generics/css/Loader.css",
               "~/Dist/Generics/css/animate.css",
               "~/Dist/App/Css/Style.css"
               ));


            BundleTable.EnableOptimizations = true;
        }
    }
}
