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
    ///     Chooses one of two values based on a static boolean parameter
    /// </summary>
    /// <remarks>
    ///     ConverterParameters cannot be bound.
    ///     Use MultiBoolToObjectConverter for a bound, non-static boolean
    /// </remarks>
    /// <returns>
    ///     Binding[0] if Parameter = True 
    ///     Binding[1] if Parameter = False
    /// </returns>
    /// <example>
    ///     <code>
    ///        <TextBox.Resources>
    ///             <Brush x:Key="EnabledBrush">White</Brush>
    ///             <Brush x:Key="DisabledBrush">LightGray</Brush>
    ///        </TextBox.Resources>
    ///        <TextBox.Background>
    ///             <MultiBinding Converter="{StaticResource MultiBoolToObjectConverter}"
    ///                             ConverterParameter="{StaticResource WhichBackground}">
    ///             <Binding Source="{StaticResource EnabledBrush}"  />
    ///             <Binding Source="{StaticResource DisabledBrush}" />
    ///             </MultiBinding>
    ///        </TextBox.Background>
    ///     </code>
    /// </example>
    public class BoolToObjectConverter : DependencyObject, IMultiValueConverter
    {
        /// <inheritdoc />
        /// <remarks>
        /// parameter == true  : Returns values[0] 
        /// parameter == false : Returns values[1] 
        /// </remarks>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values.Length >= 2) && (parameter is bool bWhich))
                return bWhich ? values[0] : values[1];

            return Binding.DoNothing;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
