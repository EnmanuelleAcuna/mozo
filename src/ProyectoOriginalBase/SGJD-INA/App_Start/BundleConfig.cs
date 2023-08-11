using System.Web.Optimization;

namespace SGJD_INA {
    public class BundleConfig {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection Bundles) {
            // Set EnableOptimizations to false for debugging. For more information visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;

            // Use the development version of Modernizr to develop with and learn from. Then, when you're ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            Bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            // JQuery
            Bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            // JQuery Validate
            Bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            // JQuery AJAX Unobtrusive
            Bundles.Add(new ScriptBundle("~/bundles/jqueryajaxunobtrusive").Include("~/Scripts/jquery.unobtrusive*"));

            // JQuery UI
            Bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));
            Bundles.Add(new StyleBundle("~/Content/jqueryui").Include("~/Content/themes/base/all.css").IncludeDirectory("~/Content/themes/base", "*.css"));

            // JQuery DataTables https://datatables.net/
            // Scripts
            Bundles.Add(new ScriptBundle("~/bundles/jquerydatatable").Include("~/Content/DataTables/JS/jquery.dataTables.min.js"));
            Bundles.Add(new ScriptBundle("~/bundles/jquerydatatableresponsive").Include("~/Content/DataTables/Responsive/JS/dataTables.responsive.min.js"));

            Bundles.Add(new ScriptBundle("~/bundles/jquerydatatablebuttons").Include("~/Content/DataTables/Buttons-1.6.2/JS/dataTables.buttons.min.js"));
            Bundles.Add(new ScriptBundle("~/bundles/jquerydatatablebuttonshtml").Include("~/Content/DataTables/Buttons-1.6.2/JS/buttons.html5.min.js"));
            Bundles.Add(new ScriptBundle("~/bundles/jquerydatatablepdfzip").Include("~/Content/DataTables/JSZip-2.5.0/jszip.min.js"));
            Bundles.Add(new ScriptBundle("~/bundles/jquerydatatablepdfmake").Include("~/Content/DataTables/pdfmake-0.1.36/pdfmake.min.js"));
            Bundles.Add(new ScriptBundle("~/bundles/jquerydatatablepdfmakefonts").Include("~/Content/DataTables/pdfmake-0.1.36/vfs_fonts.js"));
            Bundles.Add(new ScriptBundle("~/bundles/jquerydatatablebuttonsprint").Include("~/Content/DataTables/Buttons-1.6.2/JS/buttons.print.min.js"));
            Bundles.Add(new ScriptBundle("~/bundles/jquerydatatablebuttonflash").Include("~/Content/DataTables/Buttons-1.6.2/JS/buttons.flash.min.js"));

            // Estilos
            Bundles.Add(new StyleBundle("~/Content/jquerydatatable").Include("~/Content/DataTables/CSS/jquery.dataTables.min.css"));
            Bundles.Add(new StyleBundle("~/Content/jquerydatatableresponsive").Include("~/Content/DataTables/Responsive/CSS/responsive.dataTables.min.css"));
            Bundles.Add(new StyleBundle("~/Content/jquerydatatablebuttons").Include("~/Content/DataTables/Buttons-1.6.2/CSS/buttons.dataTables.min.css"));

            // MaterializeCSS https://materializecss.com/
            Bundles.Add(new ScriptBundle("~/bundles/materialize").Include("~/Content/MaterializeCSS/JS/materialize.min.js"));
            Bundles.Add(new StyleBundle("~/Content/materialize").Include("~/Content/MaterializeCSS/CSS/materialize.min.css"));
            Bundles.Add(new StyleBundle("~/Content/material-icons").Include("~/Content/MaterializeCSS/CSS/material-icons.css"));

            // jQuery input mask https://github.com/RobinHerbots/InputMask
            Bundles.Add(new ScriptBundle("~/bundles/jqueryinputmask").Include("~/Scripts/jquery.inputmask.min.js"));

            // Font Awesome https://use.fontawesome.com/releases/v5.11.2/css/all.css
            Bundles.Add(new StyleBundle("~/Content/faicons").Include("~/Content/FontAwesome/CSS/all.min.css", new CssRewriteUrlTransform()));
            //Bundles.Add(new StyleBundle("~/Content/fontawesome").Include("~/Content/FontAwesome/CSS/all.css"));
            //Bundles.Add(new StyleBundle("~/Content/fontawesome").Include("~/Content/FontAwesome/CSS/all.css", new CssRewriteUrlTransform()));

            // NormalizeCSS
            Bundles.Add(new StyleBundle("~/Content/normalize").Include("~/Content/normalize.css"));

            // UserWay (PlugIn de accesibilidad)
            Bundles.Add(new ScriptBundle("~/bundles/userway").Include("~/Scripts/userway.js"));

            // QuillJS
            Bundles.Add(new ScriptBundle("~/bundles/quill").Include("~/Content/QuillJS/JS/quill.min.js", "~/Content/QuillJS/JS/_quilljs.js"));
            Bundles.Add(new ScriptBundle("~/bundles/quillextra").Include("~/Content/QuillJS/JS/_scriptseditor.js"));
            Bundles.Add(new StyleBundle("~/Content/quill").Include("~/Content/QuillJS/CSS/quill.snow.css"));

            // JQuery Confirm
            Bundles.Add(new ScriptBundle("~/bundles/jqueryconfirm").Include("~/Content/JQuery-Confirm/JS/jquery-confirm.min.js"));
            Bundles.Add(new StyleBundle("~/Content/jqueryconfirm").Include("~/Content/JQuery-Confirm/CSS/jquery-confirm.min.css"));

            // Helpers CSS
            Bundles.Add(new StyleBundle("~/Content/helpers").Include("~/Content/_helpers.css"));

            // Firma digital
            Bundles.Add(new ScriptBundle("~/bundles/firmadigital").Include("~/Scripts/_firmadigital.js"));

            Bundles.Add(new StyleBundle("~/Content/estilosimpresion").Include(
                "~/Content/MaterializeCSS/CSS/_materializecss.css",
                "~/Content/_estilosimpresion.css"));

            Bundles.Add(new StyleBundle("~/Content/customcss").Include(
                "~/Content/_global.css",
                "~/Content/MaterializeCSS/CSS/_materializecss.css",
                "~/Content/DataTables/CSS/_jquerydatatables.css",
                "~/Content/_jqueryui.css",
                "~/Content/_pageloader.css",
                "~/Content/_login.css",
                "~/Content/_oops.css",
                "~/Content/_site.css"));

            Bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                "~/Scripts/_funciones.js",
                "~/Scripts/_notificaciones.js",
                "~/Scripts/_modals.js",
                "~/Scripts/_datepickers.js",
                "~/Scripts/_scriptseditor.js"));

            // JQuery Swapsie PlugIn http://biostall.com/swap-and-re-order-divs-smoothly-using-jquery-swapsie-plugin/
            //Bundles.Add(new ScriptBundle("~/bundles/swapsies").Include("~/Scripts/swapsies.js"));

            // Librería para gráficos ChartJS
            Bundles.Add(new ScriptBundle("~/bundles/chart").Include("~/Scripts/chart.min.js"));
        }
    }
}