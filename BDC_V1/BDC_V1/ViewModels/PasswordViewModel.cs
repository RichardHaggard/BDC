using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Events;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Events;

namespace BDC_V1.ViewModels
{
    public class PasswordViewModel : ViewModelBase
    {
        [NotNull]
        public ICommand ValidateCmd { get; }

        public string UserPass
        {
            get => _userPass;
            set => SetPropertyFlagged(ref _userPass, value, nameof(ValidateButtonEnabled));
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
            RegionManagerName = "PasswordItemControl";

            ValidateCmd = new DelegateCommand(OnCmdVerify);
        }

        protected override bool GetRegionManager()
        {
            return false;
        }

        private void OnCmdVerify()
        {
            DialogResultEx = true;

            //Publish event to close this window
            EventTypeAggregator.GetEvent<PubSubEvent<CloseWindowEvent>>()
                .Publish(new CloseWindowEvent(typeof(PasswordView).Name));
        }
    }
}
