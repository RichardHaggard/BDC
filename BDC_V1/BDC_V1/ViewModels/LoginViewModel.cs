using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BDC_V1.Events;
using BDC_V1.Views;
using CommonServiceLocator;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace BDC_V1.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull]
        public ICommand CmdLogin { get; }

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

        // **************** Class constructors ********************************************** //

        public LoginViewModel()
        {
            LoginButtonContent = "LOG IN";
            LabelContent = "Something to confirm LoginViewModel is properly bound.";

            CmdLogin = new DelegateCommand(OnCmdLogin);
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
            EventAggregator?.GetEvent<PubSubEvent<string>>().Publish("Login clicked");

            //Publish event to close this window
            EventAggregator?.GetEvent<PubSubEvent<CloseWindowEvent>>()
                .Publish(new CloseWindowEvent(typeof(LoginView).Name));

            if (! LoginSuccessful) Application.Current.Shutdown(-1);
            else
            {
                // Publish event to make the shell window visible
                EventAggregator?.GetEvent<PubSubEvent<WindowVisibilityEvent>>()
                    .Publish(new WindowVisibilityEvent(typeof(ShellView).Name,
                        Visibility.Visible));
            }
        }
    }
}
