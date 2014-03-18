using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace LicensedBundler
{
    public class LicensedScriptBundle: Bundle
    {
        public LicensedScriptBundle(string virtualPath)
            : base(virtualPath)
        {
            this.Builder = new LicensedScriptBuilder();
        }

        public LicensedScriptBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        {
            this.Builder = new LicensedScriptBuilder();
        }
    }

    public class LicensedScriptBuilder : IBundleBuilder
    {
        public virtual string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            var content = new StringBuilder();
            content.Append(Common.AddHeader());
            foreach (var file in files)
            {
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file.VirtualFile.VirtualPath));
                CodeSettings settings = new CodeSettings();
                settings.RemoveUnneededCode = true;
                settings.StripDebugStatements = true;
                settings.PreserveImportantComments = true;
                settings.TermSemicolons = true;
                var minifier = new Microsoft.Ajax.Utilities.Minifier();
                string readFile = Common.Read(f);
                string res = minifier.MinifyJavaScript(readFile, settings);
                if (minifier.ErrorList.Count > 0)
                {
                    content.Insert(0, Common.PrependErrors(readFile, minifier.ErrorList));
                }
                else
                {
                    content.Append(res);
                }
            }

            return content.ToString();
        }
    } 
}
