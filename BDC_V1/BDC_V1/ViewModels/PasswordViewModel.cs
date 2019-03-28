using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDC_V1.Events;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Events;
using EventAggregator = BDC_V1.Events.EventAggregator;

namespace BDC_V1.ViewModels
{
    public class PasswordViewModel : ViewModelBase
    {
        [NotNull]
        public ICommand ValidateCmd { get; }

        public string UserPass
        {
            get => _userPass;
            set
            {
                if (_userPass != value)
                {
                    SetProperty(ref _userPass, value);
                    RaisePropertyChanged(nameof(ValidateButtonEnabled));
                }
            }
        }
        private string _userPass;

        [CanBeNull]
        public bool? DialogResultEx
        {
            get => _dialogResultEx;
            set => SetProperty(ref _dialogResultEx, value);
        }
        private bool? _dialogResultEx;

        public bool ValidateButtonEnabled => !string.IsNullOrEmpty(UserPass);

        public PasswordViewModel()
        {
            ValidateCmd = new DelegateCommand(OnCmdVerify);
        }

        private void OnCmdVerify()
        {
            DialogResultEx = true;

            //Publish event to close this window
            EventAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                .Publish(new CloseWindowEvent(typeof(PasswordView).Name));
        }
    }
}
