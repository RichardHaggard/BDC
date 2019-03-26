using System.Windows.Input;
using CommonServiceLocator;
using Prism.Commands;
using Prism.Events;

namespace BDC_V1.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdLogin
        {
            get { return _CmdLogin; }
            set { SetProperty(ref _CmdLogin, value); }
        }
        private ICommand _CmdLogin;


        public bool? DialogResultEx
        {
            get { return _DialogResultEx; }
            set { SetProperty(ref _DialogResultEx, value); }
        }
        private bool? _DialogResultEx;


        private IEventAggregator EventAggregator 
        { 
            get
            {
                if (_EventAggregator == null)
                {
                    try
                    {
                        _EventAggregator = ServiceLocator.Current.TryResolve<IEventAggregator>();
                    }
                    catch { }
                }
                return _EventAggregator;
            }
        }
        private IEventAggregator _EventAggregator;


        public string LoginButtonContent
        {
            get { return _LoginButtonContent; }
            set { SetProperty(ref _LoginButtonContent, value); }
        }
        private string _LoginButtonContent;


        public bool LoginSuccessful { get; set; }

        // **************** Class constructors ********************************************** //

        public LoginViewModel()
        {
            LoginButtonContent = "LOG IN";
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
            // filled out. This simple shortbut is a proof of concept and should be replaced
            // in the real code.
            if (EventAggregator != null)
                EventAggregator.GetEvent<PubSubEvent<string>>().Publish("Login clicked");
        }

    }
}
