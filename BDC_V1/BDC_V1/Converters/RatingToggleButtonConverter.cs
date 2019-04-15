using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using BDC_V1.Enumerations;
using Prism.Interactivity.DefaultPopupWindows;

namespace BDC_V1.Converters
{
    public class RatingToggleButtonConverter : DependencyObject, IMultiValueConverter
    {
        private static readonly EnumBooleanConverter          EnumConverter = new EnumBooleanConverter();
        private static readonly RatingToRatingColorConverter ColorConverter = new RatingToRatingColorConverter();

        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // check for a valid object count
            if ((values == null) || (values.Length < 3)) 
                throw new ArgumentOutOfRangeException(nameof(values.Length), values?.Length, @"Too few arguments passed in");

            // brush color array --> one way to source
            if (!(values[0] is RadioButton button))     // <Binding RelativeSource="{RelativeSource Self}"/>
                throw new InvalidCastException($"values[0] ({values[0].GetType()}) is not a {typeof(RadioButton)}");

            // brush color array --> one way to source
            // 
            //  <x:Array Type="{x:Type sys:String}">
            //      <sys:String>White</sys:String>  <!-- Green, !IsEnabled, Foreground -->
            //      <sys:String>Black</sys:String>  <!-- Green, !IsEnabled, Background -->
            //
            //      <sys:String>#FEDD00</sys:String>
            //      ...
            //  </x:Array>
            // 
            if (!(values[1] is string[] colorBrushes) ||    // [3 colors Green,Amber,Red] * 
                (colorBrushes.Length != 24))                // [4 states !IsEnabled,!IsSelected & None, !IsSelected & Any, IsSelected] *
            {                                               // [2 brushes Foreground, Background]
                throw new InvalidCastException($"values[1] ({values[1].GetType()}) is not a {typeof(EnumRatingType)}");
            }

            // current value of the enum --> one way to source
            if (!(values[2] is EnumRatingType rating))
                throw new InvalidCastException($"values[2] ({values[2].GetType()}) is not a {typeof(EnumRatingType)}");

            // the string value of the EnumRatingType for this button
            if (!(parameter is string targetStr))
                throw new InvalidCastException($"parameter ({parameter.GetType()}) is not a {typeof(bool)}");

            // is it a valid value for this Enum
            if (! Enum.TryParse<EnumRatingType>(targetStr, true, out var targetEnum))
                throw new InvalidEnumArgumentException(nameof(targetStr));
           
            var selected = false;
            int stateIdx = 0;   // Disabled
            if (button.IsEnabled.Equals(true))
            {
                // is the enum == the button desired value (i.e. selected)
                // ReSharper disable once AssignmentInConditionalExpression
                if (selected = (EnumConverter.Convert(rating, targetType, targetStr, culture) as bool?).Equals(true))
                    stateIdx = 3;

                // is the enum == None
                else if ((EnumConverter.Convert(rating, targetType, "None", culture) as bool?).Equals(true))
                    stateIdx = 1;

                // Enabled && !Selected && !None
                else
                    stateIdx = 2;
            }

            var colorIdx = 0;
            if (ColorConverter.Convert(targetEnum, typeof(EnumRatingColors), null, culture) is EnumRatingColors color)
            {
                switch (color)
                {
                    case EnumRatingColors.Green: colorIdx = 0; break;
                    case EnumRatingColors.Amber: colorIdx = 1; break;
                    case EnumRatingColors.Red  : colorIdx = 2; break;

                    case EnumRatingColors.None:
                    case EnumRatingColors.Yellow:
                    default:
                        throw new ArgumentOutOfRangeException(nameof(color), color, @"Invalid color returned");
                }
            }

            // now convert this to an array index
            var index = (colorIdx * 8) + (stateIdx * 2);

            if ((index + 1) >= colorBrushes.Length)
                throw new ArgumentOutOfRangeException(nameof(index), index + 1, @"Invalid calculated index");

            button.Foreground = (SolidColorBrush) new BrushConverter().ConvertFromString(colorBrushes[index + 0]);
            button.Background = (SolidColorBrush) new BrushConverter().ConvertFromString(colorBrushes[index + 1]);

            return selected;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if ((targetTypes == null) || (targetTypes.Length < 3)) 
                throw new ArgumentOutOfRangeException(nameof(targetTypes.Length), targetTypes?.Length, @"Too few arguments passed in");

            if (targetTypes[2] != typeof(EnumRatingType))
                throw new InvalidCastException($"targetTypes[2] ({targetTypes[2]}) is not a {typeof(EnumRatingType)}");

            var retval = Enumerable.Repeat(DependencyProperty.UnsetValue, targetTypes.Length).ToArray();
            retval[2] = value;

            return retval;
        }
    }
}
