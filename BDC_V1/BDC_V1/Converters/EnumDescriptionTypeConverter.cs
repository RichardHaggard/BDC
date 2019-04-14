using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Converters
{
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
