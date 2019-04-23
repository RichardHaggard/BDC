using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BDC_V1.Converters
{
    public class DiagnosticConverter : DependencyObject, IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("Diagnostic.Convert: Target:{0}", targetType);
            Debug.WriteLine("\tParameter={0}:\"{1}\"", 
                parameter?.GetType().ToString() ?? "null", 
                parameter?.ToString() ?? "null");

            Debug.WriteLine("\t{0} values", values.Length);
            for (var index=0; index < values.Length; index++)
                Debug.WriteLine("\t\t[{0}]={1}:\"{2}\"", index, 
                    values[index]?.GetType().ToString() ?? "null", 
                    values[index]?.ToString() ?? "null");

            return Binding.DoNothing;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("Diagnostic.ConvertBack: Source:{0}:\"{1}\"", 
                value?.GetType().ToString() ?? "null", 
                value?.ToString() ?? "null");

            Debug.WriteLine("\tParameter={0}:\"{1}\"", 
                parameter?.GetType().ToString() ?? "null", 
                parameter?.ToString() ?? "null");

            Debug.WriteLine("\t{0} targets", targetTypes.Length);
            for (var index=0; index < targetTypes.Length; index++)
                Debug.WriteLine("\t\t[{0}]={1}", index, 
                    targetTypes[index]?.ToString() ?? "null");

            return Enumerable.Repeat<object>(Binding.DoNothing, targetTypes.Length).ToArray();
        }
    }
}
