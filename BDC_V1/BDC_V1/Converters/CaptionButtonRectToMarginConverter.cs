/**************************************************************************\
    Copyright Microsoft Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using System.Windows;
using System.Windows.Data;

namespace BDC_V1.Converters
{

    public class CaptionButtonRectToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ( value is Rect captionLocation)
            {
                return new Thickness(0, captionLocation.Top + 6, -captionLocation.Right, 0);
            }

            return new Thickness();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
