using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BDC_V1.Converters
{
    /// <summary>
    ///     Compares an enumeration to the static enum in parameter
    /// </summary>
    /// <returns>
    ///     True = It's a match
    ///     False - Not a match
    /// </returns>
    /// <example>
    ///     <code>
    ///         IsChecked="{Binding Path=FilterSource,
    ///             Converter={StaticResource ResourceKey=EnumBooleanConverter},
    ///             ConverterParameter=BredFilter,
    ///             Mode=TwoWay}" />
    ///     </code>
    /// </example>
    public class EnumBooleanConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((parameter is string parameterString) && (value != null))
            {
                var type = value.GetType();
                if ((type.IsEnum) && Enum.IsDefined(type, value))
                {
                    var parameterValue = Enum.Parse(type, parameterString);
                    return parameterValue.Equals(value);
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string parameterString)
                return Enum.Parse(targetType, parameterString);

            return DependencyProperty.UnsetValue;
        }
    }
}
