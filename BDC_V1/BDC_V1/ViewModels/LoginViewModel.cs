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
        public ICommand LoginButtonOnClick { get; }

        [NotNull]
        public ICommand SelectConfigFileButtonOnClick { get; }

        [NotNull]
        public ICommand SelectQcFileButtonOnClick { get; }

        [NotNull]
        public ICommand SelectInspectorButtonOnClick { get; }

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

        public IReadOnlyCollection<string> LoginUserList => _userName;
        private readonly List<string> _userName = new List<string>();

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

        // **************** Class constructors ********************************************** //

        public LoginViewModel(MockValidUsers validUsers)
        {
            //_validUsers = validUsers;
            _userName.AddRange(validUsers.GetValidUsers());

            LoginButtonContent = "LOG IN";
            LabelContent = "Something to confirm LoginViewModel is properly bound.";

            LoginButtonOnClick            = new DelegateCommand(OnCmdLogin  );
            SelectConfigFileButtonOnClick = new DelegateCommand(OnConfigFile);
            SelectQcFileButtonOnClick     = new DelegateCommand(OnQcFile    );
            SelectInspectorButtonOnClick  = new DelegateCommand(OnInspector );
        }

        // **************** Class members *************************************************** //

        private void OnCmdLogin()
        {
            LoginSuccessful = true;
            DialogResultEx = true;

            // Insert a string literal into the Login button clicked event.
            // Normally, the PubSubEvent would be a derived class and the thing being
            // published would be an instantiation of an object with various properties
            // filled out. This simple short but is a proof of concept and should be replaced
            // in the real code.
            EventAggregator.GetEvent<PubSubEvent<string>>().Publish("Login clicked");

            //Publish event to close this window
            EventAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                .Publish(new CloseWindowEvent(typeof(LoginView).Name));

            if (! LoginSuccessful) Application.Current.Shutdown(-1);
            else
            {
                // Publish event to make the shell window visible
                EventAggregator.GetEvent<PubSubEvent<WindowVisibilityEvent>>()
                    .Publish(new WindowVisibilityEvent(typeof(ShellView).Name,
                        Visibility.Visible));
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
