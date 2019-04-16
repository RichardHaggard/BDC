using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BDC_V1.Converters
{
    /// <summary>
    ///     Chooses one of two values based on a non-static boolean parameter
    /// </summary>
    /// <returns>
    ///     Binding[1] if Binding[0] = True 
    ///     Binding[2] if Binding[0] = False
    /// </returns>
    /// <example>
    /// <code>
    ///    <TextBox.Resources>
    ///         <Brush x:Key="EnabledBrush">White</Brush>
    ///         <Brush x:Key="DisabledBrush">LightGray</Brush>
    ///    </TextBox.Resources>
    ///    <TextBox.Background>
    ///         <MultiBinding Converter="{StaticResource MultiBoolToObjectConverter}" >
    ///         <Binding Path="IsEnabled" RelativeSource="{RelativeSource Self}" 
    ///                         UpdateSourceTrigger="PropertyChanged" />
    ///         <Binding Source="{StaticResource EnabledBrush}"  />
    ///         <Binding Source="{StaticResource DisabledBrush}" />
    ///         </MultiBinding>
    ///    </TextBox.Background>
    /// </code>
    /// </example>
    public class MultiBoolToObjectConverter : DependencyObject, IMultiValueConverter
    {
        private static readonly BoolToObjectConverter BoolConverter = new BoolToObjectConverter();

        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values == null) || (values.Length < 3))
                throw new ArgumentOutOfRangeException(nameof(values), values?.Length, @"Too few items in array");

            return BoolConverter.Convert(new[] {values[1], values[2]}, targetType, values[0], culture);
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
           return BoolConverter.ConvertBack(value, targetTypes, parameter, culture);
        }
    }
}
