using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            content.Append("\r\n/* Minification Error \r\n");
            content.Append(string.Join(" \r\n", errors));
            content.Append("\r\n Minification Error */\r\n");
            content.Append(file);
            return content.ToString();
        }
    }
}
