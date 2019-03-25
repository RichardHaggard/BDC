using System.Windows;

namespace BDC_V1.Utils
{
    public static class DialogCloserExtension
    {
        /// <summary>
        /// This is an extension property. It is added to the Window
        /// inthe Window's XAML and bound to a property to the Window's
        /// DataContext. When the property changes in the DataContext
        /// that change will be passed back to this property.
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
            "DialogResult",
            typeof( bool? ),
            typeof( DialogCloserExtension ),
            new PropertyMetadata( DialogResultChanged ) );

        /// <summary>
        /// Handle notification that the DialogResultProperty has changed
        /// by passing that value on to the Window's DialogResult. As a
        /// side effect of setting the Window's own native property the
        /// Window will close.
        /// </summary>
        /// <param name="d">The target Window.</param>
        /// <param name="e">Contains the value to be written to the Window's DialogResult.</param>
        private static void DialogResultChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var window = d as Window;
            if (window != null && window.IsVisible)
            {
                window.DialogResult = e.NewValue as bool?;
            }
        }


        /// <summary>
        /// Returns the supplied Window's DialogResult property.
        /// </summary>
        /// <param name="target">Window that owns the attached property.</param>
        /// <returns>
        /// The Window's attached property is returned as a nullable bool.
        /// </returns>
        public static bool? GetDialogResult( Window target )
        {
            bool? bReturn = false;

            try
            {
                if (target != null)
                {
                    bReturn = target.GetValue( DialogResultProperty ) as bool?;
                }
            }
            catch
            {
                // Just eat the exception so that the application does not crash.
            }

            return bReturn;
        }


        /// <summary>
        /// Set a value into the Windows attached DialogResultProperty.
        /// </summary>
        /// <param name="target">Window that owns the property.</param>
        /// <param name="value">Value to be written into the property.</param>
        public static void SetDialogResult( Window target, bool? value )
        {
            target.SetValue( DialogResultProperty, value );
        }
    }
}
