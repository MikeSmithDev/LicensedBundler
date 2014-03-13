using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LicensedBundler
{
    public static class Common
    {
        public static string Read(FileInfo file)
        {
            using (var r = file.OpenText())
            {
                return r.ReadToEnd();
            }
        }

        public static string PrependErrors(string file, ICollection<ContextError> errors)
        {
            var content = new StringBuilder();
            content.Append("/* ");
            content.Append("Minify Error").Append("\r\n");
            foreach (ContextError err in errors)
            {
                content.Append(err.ToString()).Append("\r\n");
            }
            content.Append(" */\r\n");
            content.Append("Minify Error").Append("\r\n");
            content.Append(file);
            return content.ToString();
        }
    }
}
