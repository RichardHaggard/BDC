using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BDC_V1.ViewModels;
using JetBrains.Annotations;
using SystemCommands = Microsoft.Windows.Shell.SystemCommands;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for FatFingerMessageBoxView.xaml
    /// </summary>
    public partial class FatFingerMessageBoxView : Window
    {
        protected FatFingerMessageBoxView()
        {
            InitializeComponent();
        }

        public static MessageBoxResult Show(
            [NotNull] string messageBoxText,
            [NotNull] string caption,
            MessageBoxButton button = MessageBoxButton.OK, 
            MessageBoxImage  icon = MessageBoxImage.None)
        {
            var view = new FatFingerMessageBoxView();
            if (! (view.DataContext is FatFingerMessageBoxViewModel model))
                throw new InvalidCastException(@"Invalid DataContext");

            model.WindowTitle = caption;
            model.Message = messageBoxText;
            model.MessageButtons = button;
            model.MessageImage = icon;

            return view.ShowDialog().Equals(true) 
                ? model.Result 
                : MessageBoxResult.None;
        }

        private void MinimizeWindow_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            SystemCommands.MinimizeWindow(GetWindow(this));
        }

        private void RestoreWindow_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var w = GetWindow(this);
            Debug.Assert(w != null, nameof(w) + " != null");

            if (w.WindowState == WindowState.Maximized)
                SystemCommands.RestoreWindow(w);
            else
                SystemCommands.MaximizeWindow(w);
        }
    }
}
