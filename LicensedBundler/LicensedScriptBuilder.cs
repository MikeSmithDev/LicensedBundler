﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace LicensedBundler
{
    public class LicensedScriptBuilder: Bundle
    {
        public LicensedScriptBuilder(string virtualPath)
            : base(virtualPath)
        {
            this.Builder = new LicensedScriptBundleBuilder();
        }

        public LicensedScriptBuilder(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        {
            this.Builder = new LicensedScriptBundleBuilder();
        }
    }

    public class LicensedScriptBundleBuilder : IBundleBuilder
    {
        public virtual string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            var content = new StringBuilder();
            foreach (var file in files)
            {
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file.VirtualFile.VirtualPath));
                Microsoft.Ajax.Utilities.CodeSettings settings = new Microsoft.Ajax.Utilities.CodeSettings();
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
