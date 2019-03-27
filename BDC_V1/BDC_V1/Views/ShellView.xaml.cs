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
using BDC_V1.Events;
using BDC_V1.ViewModels;
using CommonServiceLocator;
using JetBrains.Annotations;
using Prism.Events;
using EventAggregator = BDC_V1.Events.EventAggregator;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();

            //EventAggregator.GetEvent<PubSubEvent<WindowVisibilityEvent>>()
            //    .Subscribe((item) =>
            //{
            //    if ((item == null) || (item.WindowName != this.GetType().Name)) return;
            //    switch (item.WindowVisibility)
            //    {
            //        case Visibility.Hidden : Hide(); break;
            //        case Visibility.Visible: Show(); break;
            //    }
            //});
        }
    }
}
