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
    /// Interaction logic for CpyInvView.xaml
    /// </summary>
    public partial class CpyInvView : Window
    {
        public CpyInvView()
        {
            InitializeComponent();
        }

        // singleton instance to block multiple instances 
        private static CpyInvView _instance;
        public static CpyInvView Instance => _instance ?? (_instance = new CpyInvView());
    }
}
