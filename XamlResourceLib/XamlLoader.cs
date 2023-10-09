using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace XamlResourceLib
{
    public class XamlLoader
    {
        public static string Load(string name)
        {
            var rm = new ResourceManager("XamlResourceLib.g", Assembly.GetExecutingAssembly());
            var rset = rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var xamlText = GetStringForValue(rset.GetObject(name));
            return xamlText;
        }

        private static string GetHead(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                //stream.Length
                var buffer = new char[stream.Length];
                int nChars = reader.Read(buffer, 0, buffer.Length);
                string text = new String(buffer, 0, nChars);

                if (!reader.EndOfStream) text += "...";
                return text;
            }

        }

        private static string GetStringForValue(object value)
        {
            if (value == null) return "null";
            if (value is Stream) return "Stream: " + GetHead((Stream)value);
            return value.ToString();
        }
    }
}
