using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MessageAssistant.Util
{
    public static class ExtendFunction
    {
        public static String GetAttributeEx(this XmlElement e, String name)
        {
            String str = e.GetAttribute(name);
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentException($"{e.OuterXml} does not has attribute {name}");
            }
            return str.Trim().ToLower();
        }

        public static String GetAttributeEx(this XmlElement e, String name, String val)
        {
            String str = e.GetAttribute(name);
            if (String.IsNullOrEmpty(str))
            {
                return val;
            }
            return str.Trim().ToLower();
        }

        public static int GetAttributeInt(this XmlElement e, String name)
        {
            String str = e.GetAttribute(name);
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentException($"{e.OuterXml} does not has attribute {name}");
            }
            int baseIndex = str.ToLower().Trim().StartsWith("0x") ? 16 : 10;
            return Convert.ToInt32(str, baseIndex);
        }

        public static int GetAttributeInt(this XmlElement e, String name, int val)
        {
            String str = e.GetAttribute(name);
            if (String.IsNullOrEmpty(str))
            {
                return val;
            }
            int baseIndex = str.ToLower().Trim().StartsWith("0x") ? 16 : 10;
            return Convert.ToInt32(str, baseIndex);
        }

        public static double GetAttributeDouble(this XmlElement e, String name)
        {
            String str = e.GetAttribute(name);
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentException($"{e.OuterXml} does not has attribute {name}");
            }
            return Convert.ToDouble(str);
        }

        public static double GetAttributeDouble(this XmlElement e, String name, double val)
        {
            String str = e.GetAttribute(name);
            if (String.IsNullOrEmpty(str))
            {
                return val;
            }
            return Convert.ToDouble(str);
        }
    }
}
