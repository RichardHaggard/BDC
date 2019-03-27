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
using BDC_V1.Events;
using BDC_V1.ViewModels;
using Prism.Events;
using EventAggregator = BDC_V1.Events.EventAggregator;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for PasswordView.xaml
    /// </summary>
    public partial class PasswordView : Window
    {
        public PasswordView(PasswordViewModel viewModel)
        {
            DataContext = viewModel;

            InitializeComponent();

            EventAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                .Subscribe((item) =>
                {
                    if (item?.WindowName == this.GetType().Name)
                        Close();
                });
        }
    }
}
