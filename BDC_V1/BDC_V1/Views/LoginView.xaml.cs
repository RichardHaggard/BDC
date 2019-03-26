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

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView
    {
        [CanBeNull]
        private IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                {
                    try
                    {
                        _eventAggregator = ServiceLocator.Current.TryResolve<IEventAggregator>();
                    }
                    catch { }
                }

                return _eventAggregator;
            }
        }

        [CanBeNull]
        private IEventAggregator _eventAggregator;

        public LoginView()
        {
            InitializeComponent();
        }

        public LoginView( LoginViewModel viewModel )
            : this()
        {
            DataContext = viewModel;

            EventAggregator?.GetEvent<PubSubEvent<CloseWindowEvent>>()
                .Subscribe((item) =>
                {
                    if (item?.WindowName==this.GetType().Name)
                        Close();
                });
        }
    }
}
