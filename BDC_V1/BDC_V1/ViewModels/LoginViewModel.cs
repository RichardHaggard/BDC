// define this to use a password for login
#if !DEBUG
#define USE_PASSWORD
#endif

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Databases;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Mock_Data;
using BDC_V1.Services;
using BDC_V1.Utils;
using BDC_V1.Views;
using CommonServiceLocator;
using JetBrains.Annotations;
using Microsoft.Win32;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class LoginViewModel : CloseableResultsWindow
    {

        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull] public ICommand CmdCancel           { get; }
        [NotNull] public ICommand CmdLogin            { get; }
        [NotNull] public ICommand CmdSelectConfigFile { get; }
        [NotNull] public ICommand CmdSelectQcFile     { get; }
        [NotNull] public ICommand CmdSelectInspector  { get; }

        [NotNull] public IndexedCollection<IInspector> LoginUserList { get; } = 
            new IndexedCollection<IInspector>();
        
        public string ConfigurationFilename
        {
            get => _configurationFilename;
            set => SetProperty(ref _configurationFilename, value, () =>
            {
                RaisePropertyChanged(nameof(LoginButtonEnabled));
                Properties.Settings.Default.FileConfigData = value;
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
                Properties.Settings.Default.FileBredData = value;
                Properties.Settings.Default.Save();
            });
        }
        private string _bredFilename;

        public bool LoginButtonEnabled => (!string.IsNullOrEmpty(ConfigurationFilename) &&
                                           !string.IsNullOrEmpty(BredFilename) &&
                                           (LoginUserList.SelectedItem != null));

        public bool IsMoreEnabled
        {
            get => _isMoreEnabled;
            set => SetProperty(ref _isMoreEnabled, value);
        }
        private bool _isMoreEnabled;

        // **************** Class data members ********************************************** //


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

            GetConfigInfo(Properties.Settings.Default.FileConfigData, true);
            GetBredInfo  (Properties.Settings.Default.FileBredData         , true);

            LoginUserList.PropertyChanged += (e, i) =>
            {
                if (i.PropertyName == nameof(LoginUserList.SelectedItem))
                    RaisePropertyChanged(nameof(LoginButtonEnabled));
            };
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            return false;
        }

        // here is where we read in the global config info containing the list of valid users
        private bool GetConfigInfo(string fileName, bool isSilent = false)
        {
            // validate the BRED database file
            if (! ConfigDatabase.IsValidDatabase(fileName))
            {
                if (! isSilent)
                {
                    BdcMessageBoxView.Show(
                    "File \"" + fileName + "\" is not a valid Configuration file",
                    "INVALID CONFIGURATION FILE",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                }

                return false;
            }

            var inspectors = GetConfigInspectors(fileName)
                .Where(user => !user.Disabled)
                .OrderBy(item => item.LastFirst)
                .ToList();

            if (inspectors.Any())
            {
                LoginUserList.SelectedIndex = -1;
                LoginUserList.Collection.Clear();
                LoginUserList.Collection.AddRange(inspectors);
            }

            ConfigurationFilename = fileName;
            return true;
        }

        // here is where we read in the global BRED info
        private bool GetBredInfo(string fileName, bool isSilent = false)
        {
            // validate the BRED database file
            if (! BredDatabase.IsValidDatabase(fileName))
            {
                if (!isSilent)
                {
                    BdcMessageBoxView.Show(
                        "File \"" + fileName + "\" is not a valid BRED QC file",
                        "INVALID BRED QC FILE",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                return false;
            }

            // see if there are valid users
            var inspectors = GetBredInspectors(fileName)
                .Where(user => !user.Disabled)
                .OrderBy(item => item.LastFirst)
                .ToList();

            if (inspectors.Any())
            {
                IsMoreEnabled = true;

                // if there isn't any local config info, initially build the user list from the BRED users
                if (string.IsNullOrEmpty(ConfigurationFilename) || !LoginUserList.Collection.Any())
                {
                    LoginUserList.SelectedIndex = -1;
                    LoginUserList.Collection.Clear();
                    LoginUserList.Collection.AddRange(inspectors);
                }
            }

            BredFilename = fileName;
            return true;
        }

        private void OnCmdCancel()
        {
            DialogResultEx = false;
            Application.Current.Shutdown();
        }

        private void OnCmdLogin()
        {
            //var view = new PasswordView(new PasswordViewModel());
            ////view.ShowDialog();
            //view.ShowDialogInParent(true);

            //if (!(view.DataContext is PasswordViewModel viewModel))       
            //    throw new InvalidCastException("Invalid View Model");

            //if (viewModel.DialogResultEx != true) return;

            //if (_localValidUsers.ValidateUser(SelectedLoginUser, viewModel.UserPass))
            //{
            //    //LoginSuccessful = true;
            //    DialogResultEx  = true;

            //    //Publish event to close this window
            //    EventTypeAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
            //        .Publish(new CloseWindowEvent(typeof(LoginView).Name));
            //}

            //else if (MessageBox.Show("Invalid User / Password combination", "Cannot Validate",
            //             MessageBoxButton.OKCancel, MessageBoxImage.Hand)
            //         == MessageBoxResult.Cancel)
            //{
            //    //LoginSuccessful = false;
            //    DialogResultEx  = false;

            //    //Publish event to close this window
            //    EventTypeAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
            //        .Publish(new CloseWindowEvent(typeof(LoginView).Name));
            //}

            var configContainer = ServiceLocator.Current.TryResolve<ConfigInfoContainer>();
            Debug.Assert(configContainer != null);

            // for now load mock info and overwrite it with the real data
            var configInfo = new ConfigInfo {FileName = ConfigurationFilename};
            foreach (var inspector in LoginUserList.Collection)
                configInfo.ValidUsers.Add(inspector, inspector.Password);

            configContainer.GlobalValue = configInfo;

            var bredContainer = ServiceLocator.Current.TryResolve<BredInfoContainer>();
            Debug.Assert(bredContainer != null);

            // for now load mock info and overwrite it with the real data
            var bredInfo = new MockBredInfo {FileName = BredFilename};
            bredContainer.GlobalValue = bredInfo;

            DialogResultEx = true;
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
                DefaultExt       = "mdb",
                FilterIndex      = 1,
                Filter           = "Config files (*.mdb)|*.mdb|All files (*.*)|*.*"
            };

            if (openFileDlg.ShowDialog() == true)
            {
                GetConfigInfo(openFileDlg.FileName);
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
                GetBredInfo(openFileDlg.FileName);
            }
        }
        
        private void OnInspector()
        {
            var inspectorList = GetBredInspectors(BredFilename)
                .Where(user => !user.Disabled)
                .OrderBy(user => user.LastFirst)
                .ToList();
            if (! inspectorList.Any()) return;

            var view = new SelectUserView();
            if (!(view.DataContext is SelectUserViewModel model))
                throw new InvalidCastException("DataContext is not the proper model");

            model.Users.Collection.AddRange(inspectorList);
            if (view.ShowDialog() != true) return;

            var selectedInspector = model.Users.SelectedItem;
            if (selectedInspector == null) return;

            switch (model.Result)
            {
                case EnumControlResult.ResultDeleteItem:
                case EnumControlResult.ResultCancelled:
                case EnumControlResult.ResultDeferred:
                    break;

                case EnumControlResult.ResultSaveNow:
                    if (! LoginUserList.Collection.Contains(selectedInspector))
                        LoginUserList.Collection.Add(selectedInspector);

                    LoginUserList.SelectedItem = selectedInspector;
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException();
#endif
            }
        }

        private IEnumerable<IInspector> GetConfigInspectors([NotNull] string configFilename)
        {
            var inspectors = ConfigDatabase.GetInspectors(configFilename);
            return GetInspectors(inspectors);
        }

        private IEnumerable<IInspector> GetBredInspectors([NotNull] string bredFilename)
        {
            var inspectors = BredDatabase.GetInspectors(bredFilename);
            return GetInspectors(inspectors);
        }

        private IEnumerable<IInspector> GetInspectors([NotNull] DataTable inspectorsTable)
        {
            var inspectors = new List<IInspector>();

            if (inspectorsTable == null) throw new ArgumentNullException(nameof(inspectorsTable));
            if (inspectorsTable.Rows.Count > 0)
            {
                foreach (DataRow row in inspectorsTable.Rows)
                {
                    try
                    {
                        var firstName = row["Firstname"].ToString();
                        var lastName  = row["Lastname" ].ToString();

                        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                        {
                            var password = row["PasswordHashed"].ToString();

                            var strUserId = row["User_ID"].ToString();
                            if (! Guid.TryParse(strUserId, out var userId))
                                userId = new Guid();

                            var strPsdChgDate = row["PasswordChanged"].ToString();
                            if (! DateTime.TryParse(strPsdChgDate, out var psdChgDate))
                                psdChgDate = DateTime.Now;

                            var strDisabled = row["Disabled"].ToString();
                            if (! bool.TryParse(strDisabled, out var disabled))
                                disabled = false;

                            var inspector = new Inspector
                            {
                                UserId    = userId,
                                LastName  = lastName,
                                FirstName = firstName,
                                Password  = password,
                                Disabled  = disabled,
                                PasswordChanged = psdChgDate
                            };

                            if (!inspectors.Contains(inspector)) inspectors.Add(inspector);
                        }
                    }
                    catch (Exception e)
                    {
                        // ignore all exceptions
                        Debug.WriteLine(e);
                    }
                }
            }

            return inspectors;
        }

    }
}
