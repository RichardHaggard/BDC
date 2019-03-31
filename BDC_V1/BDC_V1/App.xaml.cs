using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using BDC_V1.Classes;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using BDC_V1.ViewModels;
using BDC_V1.Views;
using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Unity;
using Prism.Events;
using BDC_V1.Utils;
using BDC_V1.Properties;

namespace BDC_V1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Previous to Prism version 7, this happened automatically. Not anymore.
            base.ConfigureServiceLocator();

            containerRegistry.RegisterInstance<IAppController>     (new AppController());
            containerRegistry.RegisterInstance<IEventAggregator>   (new Prism.Events.EventAggregator());
            containerRegistry.RegisterInstance<BredInfoContainer>  (new BredInfoContainer  ());
            containerRegistry.RegisterInstance<ConfigInfoContainer>(new ConfigInfoContainer());
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            PresentationTraceSources.Refresh();
            //PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());
            PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());
            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Warning | SourceLevels.Error;

            base.OnStartup(e);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (MainWindow == null) return;

            if (MainWindow.DataContext is ShellViewModel shellViewModel)
            {
                shellViewModel.WindowVisibility = Visibility.Hidden;

                var loginView = new LoginView(new LoginViewModel());
                loginView.ShowDialog();

                if (loginView.DataContext is LoginViewModel loginViewModel)
                {
                    if (loginViewModel.DialogResultEx != true)
                    {
                        Application app = Application.Current;
                        if (app != null)
                            app.Shutdown(-1);
                        return;
                    }

                    // Publish event to make the shell window visible
                    shellViewModel.ConfigurationFilename = loginViewModel.ConfigurationFilename;
                    shellViewModel.BredFilename          = loginViewModel.BredFilename;
                    shellViewModel.SelectedLoginUser     = loginViewModel.SelectedLoginUser;

                    // Use the extension method in the WindowPlace class to retrieve this 
                    // window's previous position and display state, if any.
                    MainWindow.SetPlacement(Settings.Default.PlacementShell, false);

                    shellViewModel.WindowVisibility = Visibility.Visible;

                    // this is a hack to get the toolbar menu to display at first showing
                    if (MainWindow is ShellView shellView)
                    {
                        shellView.ViewTabControl.SelectedIndex = 0;
                    }
                    return;
                }
            }

            MessageBox.Show("Cannot obtain necessary models", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown(-1);
        }
    }

    public class DebugTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            Debug.Write(message);
        }

        public override void WriteLine(string message)
        {
            Debug.WriteLine(message);
            //Debugger.Break();
        }
    }
}
