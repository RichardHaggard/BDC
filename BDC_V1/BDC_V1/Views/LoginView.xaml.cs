﻿using BDC_V1.ViewModels;

namespace BDC_V1.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();

#if false
            EventTypeAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                .Subscribe((item) =>
                {
                    if (item?.WindowName==this.GetType().Name)
                        Close();
                });
#endif
        }

        public LoginView(LoginViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}
