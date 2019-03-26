using System.Windows;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using BDC_V1.ViewModels;
using BDC_V1.Views;
using Prism.Ioc;

namespace BDC_V1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //LoginViewModel ViewModel = new LoginViewModel();
            //LoginView view = new LoginView( ViewModel );
            //view.ShowDialog();

            //if (ViewModel.LoginSuccessful)
            //{
                base.OnStartup(e);
                //MainWindow.Show();
            //}
            //else
            //    MainWindow.Close();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Previous to Prism version 7, this happened automatically. Not anymore.
            base.ConfigureServiceLocator();

            containerRegistry.RegisterInstance<IAppController>(new AppController());
        }
    }
}
