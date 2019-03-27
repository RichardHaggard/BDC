using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using BDC_V1.Views;
using CommonServiceLocator;
using JetBrains.Annotations;
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
        public ICommand LoginCmd { get; }

        [NotNull]
        public ICommand SelectConfigFileCmd { get; }

        [NotNull]
        public ICommand SelectQcFileCmd { get; }

        [NotNull]
        public ICommand SelectInspectorCmd { get; }

        [CanBeNull]
        public bool? DialogResultEx
        {
            get => _dialogResultEx;
            set => SetProperty(ref _dialogResultEx, value);
        }
        private bool? _dialogResultEx;

        public string LoginButtonContent
        {
            get => _loginButtonContent;
            set => SetProperty(ref _loginButtonContent, value);
        }
        private string _loginButtonContent;

        public string LabelContent
        {
            get => _labelContent;
            set => SetProperty(ref _labelContent, value);
        }
        private string _labelContent;

        public bool LoginSuccessful 
        {
            get => _loginSuccessful;
            set => SetProperty(ref _loginSuccessful, value);
        }
        private bool _loginSuccessful;

        public IReadOnlyCollection<string> LoginUserList => _validUsers.GetValidUsers();

        public string SelectedLoginUser
        {
            get => _selectedLoginUser;
            set
            {
                if (LoginUserList.Contains(value))
                    SetProperty(ref _selectedLoginUser, value);
            }
        }
        private string _selectedLoginUser;

        public string ConfigurationFilename
        {
            get => _configurationFilename;
            set => SetProperty(ref _configurationFilename, value);
        }
        private string _configurationFilename;

        public string BredFilename
        {
            get => _bredFilename;
            set => SetProperty(ref _bredFilename, value);
        }
        private string _bredFilename;

        [NotNull]
        private readonly IValidUsers _validUsers;

        // **************** Class constructors ********************************************** //

        public LoginViewModel()
        {
            LoginCmd            = new DelegateCommand(OnCmdLogin  );
            SelectConfigFileCmd = new DelegateCommand(OnConfigFile);
            SelectQcFileCmd     = new DelegateCommand(OnQcFile    );
            SelectInspectorCmd  = new DelegateCommand(OnInspector );

            _validUsers = ServiceLocator.Current.TryResolve<IValidUsers>();
            if (_validUsers == null)
            {
                //Publish event to close this window
                EventAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                    .Publish(new CloseWindowEvent(typeof(LoginView).Name));

                MessageBox.Show("Error Obtaining Valid Users", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResultEx = false;
                return;
            }

            LoginButtonContent = "LOG IN";
            LabelContent = "Something to confirm LoginViewModel is properly bound.";
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

            var view = new PasswordView(new PasswordViewModel());
            view.ShowDialog();

            if (!(view.DataContext is PasswordViewModel viewModel)) return;
            if (viewModel.DialogResultEx != true) return;

            if (_validUsers.ValidateUser(SelectedLoginUser, viewModel.UserPass))
            {
                LoginSuccessful = true;
                DialogResultEx  = true;

                //Publish event to close this window
                EventAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                    .Publish(new CloseWindowEvent(typeof(LoginView).Name));
            }

            else if (MessageBox.Show("Invalid User / Password combination", "Cannot Validate",
                         MessageBoxButton.OKCancel, MessageBoxImage.Hand)
                     == MessageBoxResult.Cancel)
            {
                LoginSuccessful = false;
                DialogResultEx  = false;

                //Publish event to close this window
                EventAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                    .Publish(new CloseWindowEvent(typeof(LoginView).Name));
            }
        }

        private void OnConfigFile()
        {
            // TO BE IMPLEMENTED
        }

        private void OnQcFile()
        {
            // TO BE IMPLEMENTED
        }

        private void OnInspector()
        {
            MessageBox.Show("To be implemented");
        }

    }
}
