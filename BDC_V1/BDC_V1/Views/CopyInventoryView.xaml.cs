using System;
using System.Collections.Generic;
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

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for CopyInventoryView.xaml
    /// </summary>
    public partial class CopyInventoryView : Window
    {
        public CopyInventoryView()
        {
            InitializeComponent();
        }

        // singleton instance to block multiple instances 
        private static CopyInventoryView _instance;
        public static CopyInventoryView Instance => _instance ?? (_instance = new CopyInventoryView());

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
