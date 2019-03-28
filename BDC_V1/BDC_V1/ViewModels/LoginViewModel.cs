// define this to use a password for login
#if !DEBUG
#define USE_PASSWORD
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using BDC_V1.Utils;
using BDC_V1.Views;
using CommonServiceLocator;
using JetBrains.Annotations;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using EventAggregator = BDC_V1.Events.EventAggregator;

namespace BDC_V1.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // this causes exceptions within the XamlParser
        //private readonly IValidUsers _validUsers;

        // **************** Class properties ************************************************ //

        [NotNull]
        public ICommand CmdLogin { get; }

        [NotNull]
        public ICommand CmdSelectConfigFile { get; }

        [NotNull]
        public ICommand CmdSelectQcFile { get; }

        [NotNull]
        public ICommand CmdSelectInspector { get; }

        [CanBeNull]
        public bool? DialogResultEx
        {
            get => _dialogResultEx;
            set => SetProperty(ref _dialogResultEx, value);
        }
        private bool? _dialogResultEx;

        public IReadOnlyCollection<IPerson> LoginUserList => _validUsers.GetValidUsers();

        public IPerson SelectedLoginUser
        {
            get => _selectedLoginUser;
            set
            {
                if (_selectedLoginUser != value)
                {
                    if (LoginUserList.Contains(value))
                    {
                        SetProperty(ref _selectedLoginUser, value);
                        RaisePropertyChanged(nameof(LoginButtonEnabled));
                    }
                }
            }
        }
        private IPerson _selectedLoginUser;

        public string ConfigurationFilename
        {
            get => _configurationFilename;
            set
            {
                if (_configurationFilename != value)
                {
                    SetProperty(ref _configurationFilename, value);
                    RaisePropertyChanged(nameof(LoginButtonEnabled));
                }
            }
        }
        private string _configurationFilename;

        public string BredFilename
        {
            get => _bredFilename;
            set
            {
                if (_bredFilename != value)
                {
                    SetProperty(ref _bredFilename, value);
                    RaisePropertyChanged(nameof(LoginButtonEnabled));
                }
            }
        }
        private string _bredFilename;

        public bool LoginButtonEnabled => (!string.IsNullOrEmpty(ConfigurationFilename) &&
                                           !string.IsNullOrEmpty(BredFilename) &&
                                           (SelectedLoginUser != null) &&
                                           LoginUserList.Contains(SelectedLoginUser));

        [NotNull]
        public BitmapSource CompanyLogo { get; }

        [NotNull]
        private readonly IValidUsers _validUsers;

        // **************** Class constructors ********************************************** //

        public LoginViewModel()
        {
            // NOTE: Passing an interface to the constructor causes runtime problems with XamlParser
            //       This is stupid!

            //LoginButtonContent = "LOG IN";
            //LabelContent = "Something to confirm LoginViewModel is properly bound.";

            // build the button commands
            CmdLogin            = new DelegateCommand(OnCmdLogin  );
            CmdSelectConfigFile = new DelegateCommand(OnConfigFile);
            CmdSelectQcFile     = new DelegateCommand(OnQcFile    );
            CmdSelectInspector  = new DelegateCommand(OnInspector );

            //Build the company logo, make the background color (White) transparent
            var bitmapUri = new Uri(@"pack://application:,,,/Resources/CardnoLogo.bmp");
            var bitmapImage = new BitmapImage(bitmapUri);
            
            // make the background color transparent
            var bmp = bitmapImage.ToBitmap();
            bmp.MakeTransparent(bmp.GetPixel(1, 1));
            CompanyLogo = bmp.ToBitmapSource();

            // get the valid users class from the application services
            _validUsers = ServiceLocator.Current.TryResolve<IValidUsers>();
            if (_validUsers == null)
            {
                MessageBox.Show("Error Obtaining Valid Users", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _validUsers = new MockValidUsers();
            }

        #if DEBUG
            SelectedLoginUser     = _validUsers?.GetValidUsers().ElementAt(1);
            ConfigurationFilename = @"My Documents\Project\Subfolder\BRED_HOOD_ABRAMS_E_11057.cfg";
            BredFilename          = @"My Documents\Project\Subfolder\BRED_HOOD_ABRAMS_E_11057.mdb";
        #endif
        }

        // **************** Class members *************************************************** //

        private void OnCmdLogin()
        {
            // Insert a string literal into the Login button clicked event.
            // Normally, the PubSubEvent would be a derived class and the thing being
            // published would be an instantiation of an object with various properties
            // filled out. This simple short but is a proof of concept and should be replaced
            // in the real code.
            //EventAggregator.GetEvent<PubSubEvent<string>>().Publish("Login clicked");

        #if USE_PASSWORD
            var view = new PasswordView(new PasswordViewModel());
            view.ShowDialog();

            if (!(view.DataContext is PasswordViewModel viewModel)) return;
            if (viewModel.DialogResultEx != true) return;

            if (_validUsers.ValidateUser(SelectedLoginUser, viewModel.UserPass))
            {
                //LoginSuccessful = true;
                DialogResultEx  = true;

                //Publish event to close this window
                EventAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                    .Publish(new CloseWindowEvent(typeof(LoginView).Name));
            }

            else if (MessageBox.Show("Invalid User / Password combination", "Cannot Validate",
                         MessageBoxButton.OKCancel, MessageBoxImage.Hand)
                     == MessageBoxResult.Cancel)
            {
                //LoginSuccessful = false;
                DialogResultEx  = false;

                //Publish event to close this window
                EventAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                    .Publish(new CloseWindowEvent(typeof(LoginView).Name));
            }
        #else
            DialogResultEx = true;

            //Publish event to close this window
            EventAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                .Publish(new CloseWindowEvent(typeof(LoginView).Name));
        #endif
        }

        private void OnConfigFile()
        {
            var openFileDlg = new OpenFileDialog
            {
                ReadOnlyChecked = true,
                Multiselect = false,
                ShowReadOnly = false,
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true,
                DefaultExt = "cfg",
                Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*",
                FilterIndex = 2,
                FileName = ConfigurationFilename,
                Title = "Configuration File"
            };

            if (openFileDlg.ShowDialog() == true)
                ConfigurationFilename = openFileDlg.FileName;
        }

        private void OnQcFile()
        {
            var openFileDlg = new OpenFileDialog
            {
                ReadOnlyChecked = true,
                Multiselect = false,
                ShowReadOnly = false,
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true,
                DefaultExt = "mdb",
                Filter = "mdb files (*.mdb)|*.mdb|All files (*.*)|*.*",
                FilterIndex = 2,
                FileName = BredFilename,
                Title = "BRED QC File"
            };

            if (openFileDlg.ShowDialog() == true)
                BredFilename = openFileDlg.FileName;
        }
        
        private void OnInspector()
        {
            MessageBox.Show("To be implemented");
        }

    }
}
