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

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for CmInspView.xaml
    /// </summary>
    public partial class CmInspView : Window
    {
        public CmInspView()
        {
            InitializeComponent();

            EventAggregator.GetEvent<Prism.Events.PubSubEvent<CloseWindowEvent>>()
                .Subscribe((item) =>
                {
                    if (item?.WindowName==this.GetType().Name)
                        Close();
                });
        }
    }
}
