using System.Web;
using System.Web.Optimization;

namespace iPede.Site
{
    public class BundleConfig
    {
        // Para obter mais informações sobre agrupamento, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender com ela. Após isso, quando você estiver
            // pronto para produção, use a ferramenta de compilação em http://modernizr.com para selecionar somente os testes que você precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/validate-fix.js"));


            bundles.Add(new ScriptBundle("~/bundles/file-upload").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
                        "~/Scripts/jQuery.FileUpload/jquery.fileupload.js"));

            bundles.Add(new StyleBundle("~/Content/jQuery.FileUpload/css/file-upload").Include(
                      "~/Content/jQuery.FileUpload/css/jquery.fileupload.css")); 

            #region Site
            #region Script
            //Geral
            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                //Custom js
                        "~/Scripts/main.js"));

            //Home page
            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                      "~/Scripts/fwslider.js"));

            //Product
            bundles.Add(new ScriptBundle("~/bundles/product-js").Include(
                    "~/Scripts/jquery.etalage.min.js",
                    "~/Scripts/product-page.js"));
            #endregion

            #region Style

            bundles.UseCdn = true;

            //Geral
            bundles.Add(new StyleBundle("~/Content/cdn-font-css", "http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800")
                .Include("~/Content/cdn-font.css"));

            bundles.Add(new StyleBundle("~/Content/site-css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-responsive.css",
                      "~/Content/site.css",
                      "~/Content/misc.css"));

            //Home
            bundles.Add(new StyleBundle("~/Content/home-css").Include(
                      "~/Content/fwslider.css",
                      "~/Content/style-home.css"));

            bundles.Add(new StyleBundle("~/Content/product-css").Include(
                      "~/Content/etalage.css"));

            bundles.Add(new StyleBundle("~/Content/cart-css").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/cart.css")); 
 
            #endregion

            #endregion

            #region Admin
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                            "~/Scripts/jquery-{version}.js",
                            "~/Scripts/bootstrap.js",
                            "~/Scripts/respond.js",
                            "~/Scripts/fontawesome-iconpicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin-suggested").Include(
                             "~/Scripts/admin-suggested.js"));


            bundles.Add(new StyleBundle("~/Content/admin-css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-responsive.css",
                      "~/Content/site.css",
                      "~/Content/fontawesome-iconpicker.min.css")); 
            #endregion
            
            // Definir EnableOptimizations como false para depuração. Para obter mais informações,
            // visite http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
