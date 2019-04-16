using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Converters
{
    /// <summary>
    ///     Converts an Enum value into it's Description name,
    ///     This is a version of the GetEnumDescription extension that
    ///     is targeted for use in XAML.
    /// </summary>
    /// <remarks>
    ///     I'm not sure if this works or is even necessary.
    /// </remarks>
    public class EnumDescriptionTypeConverter : EnumConverter
    {
        public EnumDescriptionTypeConverter(Type type)
            : base(type)
        {
        }

        public override object ConvertTo(
            ITypeDescriptorContext context, 
            System.Globalization.CultureInfo culture, 
            object value, 
            Type destinationType)
        {
            if (destinationType != typeof(string)) 
                return base.ConvertTo(context, culture, value, destinationType);

            if (value == null) return string.Empty;
            var valStr = value.ToString();

            var fi = value.GetType().GetField(valStr);
            if (fi == null) return string.Empty;

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((attributes.Length > 0) && (!string.IsNullOrEmpty(attributes[0].Description))) 
                ? attributes[0].Description 
                : valStr;
        }
    }
}
