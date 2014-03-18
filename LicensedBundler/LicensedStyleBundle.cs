using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace LicensedBundler
{
    public class LicensedStyleBundle : Bundle
    {
        public LicensedStyleBundle(string virtualPath)
            : base(virtualPath)
        {
            this.Builder = new LicencedStyleBuilder();
        }

        public LicensedStyleBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        {
            this.Builder = new LicencedStyleBuilder();
        }
    }

    public class LicencedStyleBuilder : IBundleBuilder
    {
        public virtual string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            var content = new StringBuilder();
            content.Append(Common.AddHeader());
            foreach (var file in files)
            {
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file.VirtualFile.VirtualPath));
                CssSettings settings = new CssSettings();
                settings.CommentMode = Microsoft.Ajax.Utilities.CssComment.Important;
                var minifier = new Microsoft.Ajax.Utilities.Minifier();
                string readFile = Common.Read(f);
                string res = minifier.MinifyStyleSheet(readFile, settings);
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
