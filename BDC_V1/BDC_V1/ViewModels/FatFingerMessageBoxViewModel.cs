using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class FatFingerMessageBoxViewModel : CloseableWindow
    {
        public ICommand CmdNoButton     { get; }
        public ICommand CmdCancelButton { get; }
        public ICommand CmdYesOkCommand { get; }

        public ICommand CmdCloseWindow    { get; }
        public ICommand CmdToggleWindow   { get; }
        public ICommand CmdMinimizeWindow { get; }

        public string WindowTitle
        {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }
        private string _windowTitle;


        public string MessageText
        {
            get => _messageText;
            set => SetProperty(ref _messageText, value);
        }
        private string _messageText;

        public string OkYesButtonText 
        {
            get
            {
                switch (MessageButtons)
                {
                    case MessageBoxButton.OKCancel:
                    case MessageBoxButton.OK:
                        return "OK";

                    case MessageBoxButton.YesNoCancel:
                    case MessageBoxButton.YesNo:
                        return "Yes";

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Visibility NoButtonVisibility
        {
            get
            {
                switch (MessageButtons)
                {
                    case MessageBoxButton.OKCancel:
                    case MessageBoxButton.OK:
                        return Visibility.Collapsed;

                    case MessageBoxButton.YesNoCancel:
                    case MessageBoxButton.YesNo:
                        return Visibility.Visible;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Visibility CancelButtonVisibility
        {
            get
            {
                switch (MessageButtons)
                {
                    case MessageBoxButton.OK:
                    case MessageBoxButton.YesNo:
                        return Visibility.Collapsed;

                    case MessageBoxButton.YesNoCancel:
                    case MessageBoxButton.OKCancel:
                        return Visibility.Visible;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public ImageSource MessageIcon
        {
            get
            {
                switch (MessageImage)
                {
                    case MessageBoxImage.Hand:
                        return new BitmapImage(
                            new Uri(@"pack://application:,,,/Images/Remember (1).png"));

                    case MessageBoxImage.Question:
                        return new BitmapImage(
                            new Uri(@"pack://application:,,,/Images/Refresh.png"));

                    case MessageBoxImage.Exclamation:
                        return new BitmapImage(
                            new Uri(@"pack://application:,,,/Images/Flash-on-75_icons8.png"));

                    case MessageBoxImage.Asterisk:
                        return new BitmapImage(
                            new Uri(@"pack://application:,,,/Images/OK.png"));

                    case MessageBoxImage.None:
                        return null;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public MessageBoxButton MessageButtons
        {
            get => _messageButtons;
            set => SetPropertyFlagged(ref _messageButtons, value, new []
            {
                nameof(OkYesButtonText),
                nameof(NoButtonVisibility),
                nameof(CancelButtonVisibility)
            });
        }
        private MessageBoxButton _messageButtons;


        public MessageBoxImage MessageImage
        {
            get => _messageImage;
            set => SetPropertyFlagged(ref _messageImage, value, nameof(MessageIcon));
        }
        private MessageBoxImage _messageImage;


        public MessageBoxResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        private MessageBoxResult _result;


        // **************** Class constructors ********************************************** //

        public FatFingerMessageBoxViewModel()
        {
            CmdNoButton       = new DelegateCommand(OnNoButton      );
            CmdCancelButton   = new DelegateCommand(OnCancelButton  );
            CmdYesOkCommand   = new DelegateCommand(OnYesOkCommand  );
            CmdCloseWindow    = new DelegateCommand(OnCloseWindow   );
            CmdToggleWindow   = new DelegateCommand(OnToggleWindow  );
            CmdMinimizeWindow = new DelegateCommand(OnMinimizeWindow);
        }

        // **************** Class members *************************************************** //

        protected virtual void OnCloseWindow()
        {
            DialogResultEx = false;
            Result = MessageBoxResult.None;
        }

        protected virtual void OnToggleWindow()
        {
        }

        protected virtual void OnMinimizeWindow()
        {
        }

        protected virtual void OnNoButton()
        {
            DialogResultEx = true;
            Result = MessageBoxResult.No;
        }

        protected virtual void OnCancelButton()
        {
            DialogResultEx = true;
            Result = MessageBoxResult.Cancel;
        }

        protected virtual void OnYesOkCommand()
        {
            DialogResultEx = true;

            switch (MessageButtons)
            {
                case MessageBoxButton.OK:
                case MessageBoxButton.OKCancel:
                    Result = MessageBoxResult.OK;
                    break;

                case MessageBoxButton.YesNo:
                case MessageBoxButton.YesNoCancel:
                    Result = MessageBoxResult.Yes;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
