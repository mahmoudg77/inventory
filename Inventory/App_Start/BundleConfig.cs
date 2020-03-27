using System.Web;
using System.Web.Optimization;

namespace Inventory
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/scripts").Include(
            "~/Content/plugins/jquery/jquery.min.js",
            "~/Content/dist/js/adminlte.min.js",
            "~/Content/plugins/bootstrap/js/bootstrap.bundle.min.js",
            "~/Content/plugins/datatables/jquery.dataTables.js",
            "~/Content/plugins/datatables-bs4/js/dataTables.bootstrap4.js"));


            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      "~/Content/dist/css/adminlte.min.css",
                      "~/Content/plugins/fontawesome-free/css/all.min.css",
                      "~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                      "~/Content/dist/css/ionicons.min.css",
                      "~/Content/plugins/datatables-bs4/css/dataTables.bootstrap4.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


        }
    }
}
