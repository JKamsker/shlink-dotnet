using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShlinkDotnet.Extensions
{
    internal static class EnumExtensions
    {
        public static string ConvertToString<T>(this T value, System.Globalization.CultureInfo? cultureInfo = null) where T : Enum
        {
            cultureInfo ??= System.Globalization.CultureInfo.InvariantCulture;
            string name = System.Enum.GetName(value.GetType(), value);
            if (name != null)
            {
                var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                if (field != null)
                {
                    var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                        as System.Runtime.Serialization.EnumMemberAttribute;
                    if (attribute != null)
                    {
                        return attribute.Value != null ? attribute.Value : name;
                    }
                }
            }
            return System.Convert.ToString(value, cultureInfo);
        }
    }
}
