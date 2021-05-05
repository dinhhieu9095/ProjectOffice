using System.Text;
using System.Web;
using System.Web.Optimization;

namespace DaiPhatDat.Module.Task.Web
{
    public class BundleConfig
    {
        //public const string Templates =
        //  "~/bundles/task/templates";
        //// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterAngulartJs(bundles);
            //var templates = new PartialsTransform("TaskApp", Templates)
            //    .IncludeDirectory("~/TaskApp/Templates/", "*.html");

            //bundles.Add(templates);
        }
        private static void RegisterAngulartJs(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/angularJs").Include(
                "~/Areas/Task/Content/angularjs/Documentation-menu.js",
                "~/Areas/Task/Content/angularjs/angular.min.js",
                 "~/Areas/Task/Content/angularjs/angular-chosen.min.js",
                //"~/Areas/Task/Content/angularjs/highcharts.js",
                //"~/Areas/Task/Content/angularjs/highstock.js",
                //  "~/Areas/Task/Content/angularjs/exporting.js,
                "~/Areas/Task/Content/angularjs/libs/js/angular-sanitize.min.js",
                "~/Areas/Task/Content/angularjs/paging.js",
                "~/Areas/Task/Content/angularjs/libs/js/ui-bootstrap-tpls-2.5.0.min.js",
                "~/Areas/Task/Content/angularjs/libs/js/angular-toastr.tpls.min.js",
                 "~/Areas/Task/Content/angularjs/libs/js/ng-file-upload-shim.min.js",
                 "~/Areas/Task/Content/angularjs/libs/js/ng-file-upload.min.js",
                "~/Areas/Task/Content/angularjs/libs/js/object-to-form-data.js",
                 "~/Areas/Task/Content/angularjs/libs/js/date.js",
                "~/Areas/Task/Content/angularjs/libs/js/aes.js",
                "~/Areas/Task/Content/angularjs/libs/js/pbkdf2.js",
                "~/Areas/Task/Content/angularjs/libs/js/angular.dcb-img-fallback.min.js",
                "~/Areas/Task/Content/angularjs/Tables/angular-scrollable-table.js",
                "~/Areas/Task/Content/angularjs/app/SurePortalApp.js",
                 "~/Areas/Task/Content/angularjs/directives/validate.js",
                "~/Areas/Task/Content/angularjs/directives/icheck.js",
                "~/Areas/Task/Content/angularjs/directives/rw-money-mask.js",
                 "~/Areas/Task/Content/angularjs/global.js",
                "~/Areas/Task/Content/angularjs/directives/mask.js"
                ));
        }

    }
    public class PartialsTransform : IBundleTransform
    {
        private readonly string _moduleName;

        public PartialsTransform(string moduleName)
        {
            _moduleName = moduleName;
        }

        public void Process(
            BundleContext context, BundleResponse response)
        {
            var strBundleResponse = new StringBuilder();
            strBundleResponse.AppendFormat(
                "angular.module('{0}')", _moduleName);
            strBundleResponse.Append(
                ".run(['$templateCache', function(t) {");

            foreach (var file in response.Files)
            {
                var content = file.ApplyTransforms();
                content = content
                    .Replace("'", "\\'")
                    .Replace("\r", "")
                    .Replace("\n", "");
                var path = file.IncludedVirtualPath
                    .Replace("~", "");
                strBundleResponse.AppendFormat(
                    "t.put('{0}','{1}');", path, content);
            }

            strBundleResponse.Append("}]);");

            response.Files = new BundleFile[] { };
            response.Content = strBundleResponse.ToString();
            response.ContentType = "text/javascript";
        }
    }
    public class PartialsBundle : Bundle
    {
        public PartialsBundle(string moduleName, string virtualPath)
            : base(virtualPath, new[] { new PartialsTransform(moduleName) })
        {
        }
    }
}
