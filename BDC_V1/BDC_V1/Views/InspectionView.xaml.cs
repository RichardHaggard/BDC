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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BDC_V1.ViewModels;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for InspectionView.xaml
    /// </summary>
    public partial class InspectionView : UserControl
    {
        public InspectionView()
        {
            InitializeComponent();
        }

        InspectionViewModel ViewModel
        {
            set => DataContext = value;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
