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
using System.Windows.Threading;
using BDC_V1.ViewModels;
using JetBrains.Annotations;
using SystemCommands = Microsoft.Windows.Shell.SystemCommands;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for BdcMessageBoxView.xaml
    /// </summary>
    public partial class BdcMessageBoxView : Window
    {
        protected BdcMessageBoxView()
        {
            InitializeComponent();
        }

        public static MessageBoxResult Show(
            [NotNull] string messageBoxText,
            [NotNull] string caption,
            MessageBoxButton button = MessageBoxButton.OK, 
            MessageBoxImage  icon   = MessageBoxImage.None)
        {
            var view = new BdcMessageBoxView();
            if (! (view.DataContext is BdcMessageBoxViewModel model))
                throw new InvalidCastException(@"Invalid DataContext");

            model.WindowTitle    = caption;
            model.MessageText    = messageBoxText;
            model.MessageButtons = button;
            model.MessageImage   = icon;

            return view.ShowDialog().Equals(true) 
                ? model.Result 
                : MessageBoxResult.None;
        }
    }
}
