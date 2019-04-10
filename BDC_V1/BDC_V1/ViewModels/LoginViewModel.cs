// define this to use a password for login
#if !DEBUG
#define USE_PASSWORD
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Mock_Data;
using BDC_V1.Services;
using BDC_V1.Utils;
using CommonServiceLocator;
using JetBrains.Annotations;
using Microsoft.Win32;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class LoginViewModel : CloseableWindow
    {

        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull]
        public ICommand CmdCancel { get; }


        [NotNull]
        public ICommand CmdLogin { get; }

        
        [NotNull]
        public ICommand CmdSelectConfigFile { get; }

        
        [NotNull]
        public ICommand CmdSelectQcFile { get; }

        
        [NotNull]
        public ICommand CmdSelectInspector { get; }
        
        [CanBeNull]
        public IReadOnlyCollection<IPerson> LoginUserList => LocalValidUsers?.Users;

        
        [CanBeNull]
        public IPerson SelectedLoginUser
        {
            get => _selectedLoginUser;
            set
            {
                if ((value == null) || ((LoginUserList == null) || LoginUserList.Contains(value)))
                    SetPropertyFlagged(ref _selectedLoginUser, value, nameof(LoginButtonEnabled));
            }
        }
        private IPerson _selectedLoginUser;

        
        public string ConfigurationFilename
        {
            get => _configurationFilename;
            set => SetProperty(ref _configurationFilename, value, () =>
            {
                RaisePropertyChanged(nameof(LoginButtonEnabled));
                Properties.Settings.Default.ConfigurationFilename = value;
                Properties.Settings.Default.Save();
            });
        }
        private string _configurationFilename;

        
        public string BredFilename
        {
            get => _bredFilename;
            set => SetProperty(ref _bredFilename, value, () =>
            {
                RaisePropertyChanged(nameof(LoginButtonEnabled));
                Properties.Settings.Default.BredFilename = value;
                Properties.Settings.Default.Save();
            });
        }
        private string _bredFilename;

        
        public bool LoginButtonEnabled => (!string.IsNullOrEmpty(ConfigurationFilename) &&
                                           !string.IsNullOrEmpty(BredFilename) &&
                                           (SelectedLoginUser != null) && (LoginUserList != null) &&
                                           LoginUserList.Contains(SelectedLoginUser));

        //[NotNull]
        //public BitmapSource CompanyLogo { get; }


        // **************** Class data members ********************************************** //

        // this causes exceptions within the XamlParser
        //private readonly IValidUsers _localValidUsers;

        [CanBeNull]
        protected IValidUsers LocalValidUsers
        {
            get => _localValidUsers;
            set
            {
                if (_localValidUsers != value)
                {
                    Debug.Assert(value != null);
                    _localValidUsers = value;

                    // make sure the current user is still valid
                    if ((SelectedLoginUser == null) || !_localValidUsers.Users.Contains(SelectedLoginUser))
                        SelectedLoginUser = new Person();

                    RaisePropertyChanged(nameof(LoginUserList));
                    RaisePropertyChanged(nameof(LoginButtonEnabled));
                }
            }
        }
        private IValidUsers _localValidUsers;

        protected override IConfigInfo LocalConfigInfo
        {
            get => base.LocalConfigInfo;
            set
            {
                base.LocalConfigInfo = value;
                ConfigurationFilename = base.LocalConfigInfo?.FileName;
                LocalValidUsers       = base.LocalConfigInfo?.ValidUsers;
            }
        }

        protected override IBredInfo LocalBredInfo
        {
            get => base.LocalBredInfo;
            set
            {
                base.LocalBredInfo = value;
                BredFilename = base.LocalBredInfo?.FileName;
            }
        }

        // **************** Class constructors ********************************************** //

        public LoginViewModel()
        {
            RegionManagerName = "LoginItemControl";

            // NOTE: Passing an interface to the constructor causes runtime problems with XamlParser
            //       This is stupid!

            // build the button commands
            CmdCancel           = new DelegateCommand(OnCmdCancel );
            CmdLogin            = new DelegateCommand(OnCmdLogin  );
            CmdSelectConfigFile = new DelegateCommand(OnConfigFile);
            CmdSelectQcFile     = new DelegateCommand(OnQcFile    );
            CmdSelectInspector  = new DelegateCommand(OnInspector );

            //Build the company logo, make the background color (White) transparent
            //CompanyLogo = MakeBitmapTransparent.MakeTransparent(@"pack://application:,,,/Resources/CardnoLogo.bmp");

#if DEBUG
#warning Using MOCK data for LoginViewModel
            GetConfigInfo(@"This_is_a_fake_config_file.cfg");
            GetBredInfo(@"My Documents\ProjectName\Subfolder\BRED_HOOD_ABRAMS_E_11057.mdb");
#endif
            ConfigurationFilename = Properties.Settings.Default.ConfigurationFilename;
            BredFilename          = Properties.Settings.Default.BredFilename;
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            return false;
        }

        // here is where we read in the global config info containing the list of valid users
        private static void GetConfigInfo(string fileName)
        {
            var container = ServiceLocator.Current.TryResolve<ConfigInfoContainer>();
            Debug.Assert(container != null);

            container.GlobalValue = new MockConfigInfo {FileName = fileName};
        }

        // here is where we read in the global BRED info
        private static void GetBredInfo(string fileName)
        {
            var container = ServiceLocator.Current.TryResolve<BredInfoContainer>();
            Debug.Assert(container != null);

            container.GlobalValue = new MockBredInfo {FileName = fileName};
        }

        private void OnCmdCancel()
        {
            DialogResultEx = false;
            Application.Current.Shutdown();
        }

        private void OnCmdLogin()
        {
            // Insert a string literal into the Login button clicked event.
            // Normally, the PubSubEvent would be a derived class and the thing being
            // published would be an instantiation of an object with various properties
            // filled out. This simple short but is a proof of concept and should be replaced
            // in the real code.
            //EventTypeAggregator.GetEvent<PubSubEvent<string>>().Publish("Login clicked");

        #if USE_PASSWORD
            var view = new PasswordView(new PasswordViewModel());
            view.ShowDialog();

            if (!(view.DataContext is PasswordViewModel viewModel)) return;
            if (viewModel.DialogResultEx != true) return;

            if (_localValidUsers.ValidateUser(SelectedLoginUser, viewModel.UserPass))
            {
                //LoginSuccessful = true;
                DialogResultEx  = true;

                //Publish event to close this window
                EventTypeAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                    .Publish(new CloseWindowEvent(typeof(LoginView).Name));
            }

            else if (MessageBox.Show("Invalid User / Password combination", "Cannot Validate",
                         MessageBoxButton.OKCancel, MessageBoxImage.Hand)
                     == MessageBoxResult.Cancel)
            {
                //LoginSuccessful = false;
                DialogResultEx  = false;

                //Publish event to close this window
                EventTypeAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                    .Publish(new CloseWindowEvent(typeof(LoginView).Name));
            }
        #else
            DialogResultEx = true;

        #if false
            //Publish event to close this window
            EventTypeAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                .Publish(new CloseWindowEvent(typeof(LoginView).Name));
        #endif
        #endif
        }


        private void OnConfigFile()
        {
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var openFileDlg = new OpenFileDialog
            {
                Title            = "Configuration File",
                FileName         = ConfigurationFilename,
                InitialDirectory = docFolder,
                ReadOnlyChecked  = true,
                Multiselect      = false,
                ShowReadOnly     = false,
                AddExtension     = true,
                CheckFileExists  = true,
                CheckPathExists  = true,
                RestoreDirectory = true,
                DefaultExt       = "cfg",
                FilterIndex      = 1,
                Filter           = "Config files (*.cfg)|*.cfg|All files (*.*)|*.*"
            };

            if (openFileDlg.ShowDialog() == true)
            {
                ConfigurationFilename = openFileDlg.FileName;
                GetConfigInfo(ConfigurationFilename);
            }
        }

        private void OnQcFile()
        {
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var openFileDlg = new OpenFileDialog
            {
                Title            = "BRED QC File",
                FileName         = BredFilename,
                InitialDirectory = docFolder,
                ReadOnlyChecked  = true,
                Multiselect      = false,
                ShowReadOnly     = false,
                AddExtension     = true,
                CheckFileExists  = true,
                CheckPathExists  = true,
                RestoreDirectory = true,
                DefaultExt       = "mdb",
                FilterIndex      = 1,
                Filter           = "BRED files (*.mdb)|*.mdb|All files (*.*)|*.*"
            };

            if (openFileDlg.ShowDialog() == true)
            {
                BredFilename = openFileDlg.FileName;
                GetBredInfo(BredFilename);
            }
        }
        
        private void OnInspector()
        {
            MessageBox.Show("To be implemented");
        }

    }
}
