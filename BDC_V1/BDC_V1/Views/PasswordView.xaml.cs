using System.Windows;
using BDC_V1.Events;
using BDC_V1.ViewModels;
using Prism.Events;

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

            EventTypeAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                .Subscribe((item) =>
                {
                    if (item?.WindowName == this.GetType().Name)
                        Close();
                });
        }
    }
}
