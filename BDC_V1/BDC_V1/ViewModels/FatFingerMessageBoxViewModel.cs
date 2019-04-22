using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class FatFingerMessageBoxViewModel : CloseableWindow
    {
        [NotNull] public ICommand CmdNoButton     { get; }
        [NotNull] public ICommand CmdCancelButton { get; }
        [NotNull] public ICommand CmdYesOkCommand { get; }

        [NotNull] public ICommand CmdCloseWindow    { get; }
        [NotNull] public ICommand CmdToggleWindow   { get; }
        [NotNull] public ICommand CmdMinimizeWindow { get; }

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
                    case MessageBoxImage.Hand:        return SystemIconToBitmap(SystemIcons.Hand);
                    case MessageBoxImage.Question:    return SystemIconToBitmap(SystemIcons.Question);
                    case MessageBoxImage.Exclamation: return SystemIconToBitmap(SystemIcons.Exclamation);
                    case MessageBoxImage.Asterisk:    return SystemIconToBitmap(SystemIcons.Asterisk);
                    case MessageBoxImage.None:        return null;
                    default:                          return null;
                }
            }
        }

        public ImageSource ApplicationIcon
        {
            get => _applicationIcon ?? (_applicationIcon = SystemIconToBitmap(SystemIcons.Application));
            set => SetProperty(ref _applicationIcon, value);
        }
        [CanBeNull] private ImageSource _applicationIcon;

        [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
        private BitmapSource SystemIconToBitmap(Icon systemIcon)
        {
            return Imaging.CreateBitmapSourceFromHIcon(
                systemIcon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
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
