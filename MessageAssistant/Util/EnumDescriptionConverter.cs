using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MessageAssistant.Util
{
    public class EnumDescriptionConverter<T> where T : struct
    {
        private static readonly Dictionary<T, string> SToString =
            new Dictionary<T, string>();

        private static readonly Dictionary<string, T> SToValue =
            new Dictionary<string, T>();

        static EnumDescriptionConverter()
        {
            System.Diagnostics.Debug.Assert(typeof(T).IsEnum,
              "The custom enum class must be used with an enum type.");

            foreach (T item in Enum.GetValues(typeof(T)))
            {
                string description = GetDescriptionInner(item);
                SToString[item] = description;
                SToValue[description] = item;
            }
        }

        private static string GetDescriptionInner(T optionValue)
        {
            var optionDescription = optionValue.ToString();
            var optionInfo = typeof(T).GetField(optionDescription);
            if (Attribute.IsDefined(optionInfo, typeof(DescriptionAttribute)))
            {
                var attribute =
                  (DescriptionAttribute)Attribute.
                     GetCustomAttribute(optionInfo, typeof(DescriptionAttribute));
                return attribute.Description;
            }
            return optionDescription;
        }

        public static string GetDescription(T val)
        {
            if (SToString.ContainsKey(val))
            {
                return SToString[val];
            }
            return string.Empty;
        }

        public static T GetValue(string str)
        {
            if (SToValue.ContainsKey(str))
            {
                return SToValue[str];
            }

            return default(T);
        }

        public static List<T> GetValues()
        {
            List<T> result = new List<T>();
            foreach (var item in SToString)
            {
                result.Add(item.Key);
            }
            return result;
        }

        public static List<String> GetDescriptions()
        {
            List<string> result = new List<string>();
            foreach (var item in SToValue)
            {
                result.Add(item.Key);
            }
            return result;
        }
    }
}
