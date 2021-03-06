﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Navigation;
using BDC_V1.Classes;
using BDC_V1.Databases;
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
using JetBrains.Annotations;

namespace BDC_V1
{
    public enum SystemOfMeasure
    {
        English = 1,
        Metric,
    }

    public class UserAccount
    {
        public Guid   ID        { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // templates to build the following paths and file names
        [CanBeNull]
        private static Dictionary<string, string> _decoderDictionary;   // working copy

        [NotNull]
        private static readonly ReadOnlyDictionary<string, string> MasterDictionary =
            new ReadOnlyDictionary<string, string>(
                new Dictionary<string, string>
                {
                    {nameof(BuilderDcPath)    , @"Cardno\<BuilderDcName>"},
                    {nameof(PathCalcTemplates), @"<PathBuilderDc>\CalculatorTemplates"},
                    {nameof(FileBuilderData)  , @"<PathBuilderDc>\<BuilderDcName>.mdb"},
                    {nameof(PathLogFiles)     , @"<PathUserAppData>\Logs"},
                    {nameof(PathProjectFolder), @"<PathUserFolder>\<Project>"},
                    {nameof(PathNamedProject) , @"<PathUserFolder>\<ProjectIdName>"},
                    {nameof(PathBredFiles)    , @"<PathNamedProject>\<BREDName>"},
                    {nameof(PathCalculators)  , @"<PathBredFiles>\Calculators"},
                    {nameof(PathPhotosRaw)	  , @"<PathBredFiles>\PhotosRaw"},
                    {nameof(PathPhotos)	      , @"<PathBredFiles>\Photos"},
                    {nameof(FileLookupData)   , @"<PathBuilderDc>\<LookupFilename>"},
                    {nameof(FileUserCache)	  , @"<PathBuilderDc>\<InspectorName>"},
                    {nameof(FileBredData)	  , @"<PathProjectFolder>\<BREDFilename>"},
                    {nameof(FileBredPackage)  , @"<PathProjectFolder>\<PackageFilename>"},
                    {nameof(FileConfigData)   , @"<PathProjectFolder>\<ConfigFilename>"},
                });

        // TODO: These names aren't quite working yet !!!
        // Paths that are constant for all users
        [NotNull] public static string BuilderDcName     { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string BuilderDcPath     { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string BuilderDcProgram  { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathUserAppData   { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathApplication   { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathUserApp       { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathProgramData   { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathBuilderDc     { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathCalcTemplates { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string FileBuilderData   { get => GetPath(); set => SetPath(value); }
                                         
        // paths that may vary per user  
        [NotNull] public static string PathLogFiles      { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathUserFolder    { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathProjectFolder { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathNamedProject  { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathBredFiles     { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathCalculators   { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathPhotosRaw	 { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PathPhotos	     { get => GetPath(); set => SetPath(value); }
                                        
        // files that may vary per user 
        [NotNull] public static string FileLookupData	 { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string FileUserCache	 { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string FileBredData	     { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string FileBredPackage   { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string FileConfigData	 { get => GetPath(); set => SetPath(value); }
                                               
        // Variables that change the whole lot 
        [NotNull] public static string LookupFilename    { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string InspectorName     { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string BREDFilename      { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string PackageFilename   { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string ConfigFilename    { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string Project           { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string ProjectIdName     { get => GetPath(); set => SetPath(value); }
        [NotNull] public static string BREDName          { get => GetPath(); set => SetPath(value); }


        // properties that mdUtility wants
        [NotNull] public static string CompanyName = "Cardno";
        [NotNull] public static string Title       = "BuilderDC";

        internal static string UserApplicationDataDirectory => 
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + 
            App.CompanyName + @"\" + 
            App.Title;

        internal static string CommonApplicationDataDirectory => 
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\" + 
            App.CompanyName + @"\" + 
            App.Title;

        public static UserAccount User        { get; set; }

        public static string PackageFileName  { get; set; }

        public static string CurrentInspector { get; set; }

        public static bool   LoggedIn         { get; set; }

        public static SystemOfMeasure Units   { get; set; }

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
            var window = Container.Resolve<ShellView>();

            // does this get rid of the start-up flash?
            window.Visibility = Visibility.Collapsed;

            return window;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // get the system folders
            var szApplication = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            // C:\Program Files (x86)\Cardno\BuilderDC\BDC.exe
            BuilderDcProgram = szApplication;

            // C:\Program Files (x86)\Cardno\BuilderDC
            PathApplication = System.IO.Path.GetDirectoryName(szApplication) ?? string.Empty;
            PathApplication = PathApplication.Replace(@"file:\", "");
            Debug.Assert(System.IO.Directory.Exists(PathApplication));

            // set the name of the program folder
            BuilderDcName = "BuilderDC";

            // C:\ProgramData
            PathProgramData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            Debug.Assert(System.IO.Directory.Exists(PathProgramData));

            // C:\Users\<UserName>\AppData\Roaming
            PathUserApp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Debug.Assert(System.IO.Directory.Exists(PathUserApp));

            // C:\Users\<UserName>\Documents
            // Do not use this, returns OneDrive folder if connected
            //PathUserFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            PathUserFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Documents";
            Debug.Assert(System.IO.Directory.Exists(PathUserFolder));

            // C:\ProgramData\Cardno\BuilderDC
            PathBuilderDc = PathProgramData + @"\" + BuilderDcPath;
            System.IO.Directory.CreateDirectory(PathBuilderDc);     // create it if it doesn't exist

            // C:\Users\<UserName>\AppData\Roaming\Cardno\BuilderDC
            PathUserAppData = PathUserApp + @"\" + BuilderDcPath;
            System.IO.Directory.CreateDirectory(PathUserAppData);     // create it if it doesn't exist

            PresentationTraceSources.Refresh();
            //PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());
            PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());
            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Warning | SourceLevels.Error;

            // does this get rid of the start-up flash?
            if (MainWindow != null)
            {
                MainWindow.Visibility = Visibility.Collapsed;

                if (MainWindow.DataContext is ShellViewModel shellViewModel)
                    shellViewModel.WindowVisibility = Visibility.Collapsed;
            }

            base.OnStartup(e);
        }

        internal static mdUtility MdUtility;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            switch (LoginUser())
            {
                case true: 
                    // this is a hack to get the toolbar menu to display at first showing
                    if (MainWindow is ShellView shellView)
                        shellView.ViewTabControl.SelectedIndex = 0;

                    MdUtility = new mdUtility();
                    return;

                case false:
                    Current.Shutdown(-1);
                    return;

                default:
                    throw new ApplicationException("Cannot obtain necessary models");
            }
        }

        public static bool? LoginUser()
        {
            if (Current.MainWindow?.DataContext is ShellViewModel shellViewModel)
            {
                shellViewModel.WindowVisibility = Visibility.Collapsed;

                //var loginView = new LoginView(new LoginViewModel());
                var loginView = new LoginView();
                if (loginView.DataContext is LoginViewModel loginViewModel)
                {
                    loginView.ShowDialog();
                    if (loginViewModel.DialogResultEx != true) return false;

                    // Publish event to make the shell window visible
                    shellViewModel.ConfigurationFilename = loginViewModel.ConfigurationFilename;
                    shellViewModel.BredFilename          = loginViewModel.BredFilename;
                    shellViewModel.SelectedLoginUser     = loginViewModel.LoginUserList.SelectedItem;

                    // Use the extension method in the WindowPlace class to retrieve this 
                    // window's previous position and display state, if any.
                    Current.MainWindow.SetPlacement(Settings.Default.PlacementShell, false);

                    shellViewModel.WindowVisibility = Visibility.Visible;
                    return true;
                }
            }

            return null;
        }

        private static string GetPath(
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            return (string.IsNullOrEmpty(propertyName) || 
                    (_decoderDictionary == null) || 
                    !_decoderDictionary.ContainsKey(propertyName))
                ? string.Empty 
                : _decoderDictionary[propertyName];
        }

        private static bool SetPath([NotNull] string value,
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return false;

            if ((_decoderDictionary != null) &&
                _decoderDictionary.ContainsKey(propertyName) &&
                (_decoderDictionary[propertyName] == value))
            {
                return false;
            }

            // build a list of all affected macros
            var hitList = new List<string>();

            if (_decoderDictionary == null)
            {
                _decoderDictionary = new Dictionary<string, string>(MasterDictionary);
                hitList.AddRange(_decoderDictionary.Keys);  // mark all changed
            }
            else
            {
                // find which macros will be affected
                hitList.Add(propertyName);

                for (var modified = true; modified; )
                {
                    modified = false;

                    foreach (var kvp in MasterDictionary
                        .Where(kvp => !hitList.Contains(kvp.Key)))
                    {
                        foreach (var key in hitList)
                        {
                            var macro = "<" + key + ">";

                            if (kvp.Value.Contains(macro))
                            {
                                hitList.Add(kvp.Key);
                                modified = true;
                                break;
                            }
                        }

                        if (modified) break;
                    }
                }

                foreach (var key in hitList
                    .Where(key => key != propertyName &&
                                  MasterDictionary.ContainsKey(key)))
                {
                    _decoderDictionary[key] = MasterDictionary[key];
                }
            }

            // modify the specified value
            _decoderDictionary[propertyName] = value;

            // make a copy of the hit list for reporting
            IReadOnlyList<string> notifyList = new List<string>(hitList);

            for (var mod = true; mod; )
            {
                mod = false;

                foreach (var key in hitList
                    .Where(key => !MasterDictionary.ContainsKey(key) ||
                                  _decoderDictionary[key] != MasterDictionary[key]))
                {
                    var macro = "<" + key + ">";

                    for (var modified = true; modified; )
                    {
                        modified = false;

                        foreach (var kvp in _decoderDictionary 
                            .Where(kvp => (kvp.Key != key) && 
                                          kvp.Value.Contains(macro)))
                        {
                            _decoderDictionary[kvp.Key] =
                                kvp.Value.Replace(macro, _decoderDictionary[key]);

                            mod = modified = true;
                            break;
                        }
                    }
                }
            }

            return true;
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
