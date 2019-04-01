using System.Windows.Input;
using BDC_V1.Events;
using BDC_V1.Views;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CmInspViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdCancel { get; }
        public ICommand CmdDefer { get; }
        public ICommand CmdOK{ get; }

        public string Comment
        {
            get { return _Comment; }
            set { SetProperty(ref _Comment, value); }
        }
        private string _Comment;


        public bool? DialogResultEx
        {
            get { return _dialogResultEx; }
            set { SetProperty(ref _dialogResultEx, value); }
        }
        private bool? _dialogResultEx;


        /// <summary>
        /// A return of -1 indicates cancellation.
        /// 0 is defer result.
        /// 1 is save Comment now.
        /// </summary>
        public int Result { get; set; }


        // **************** Class constructors ********************************************** //

        public CmInspViewModel()
        {
            CmdCancel= new DelegateCommand(OnCmdCancel);
            CmdDefer = new DelegateCommand(OnCmdDefer);
            CmdOK    = new DelegateCommand(OnCmdOK);
            Comment  = "DAMAGED - All the wood doors have 70% severe moisture damage.  CRACKED - All of the doors have 65% severe cracking and splintering.";
        }


        // **************** Class members *************************************************** //

        private void OnCmdCancel()
        {
            Result         = -1;
            DialogResultEx = false;
        }


        private void OnCmdDefer()
        {
            Result         = 0;
            DialogResultEx = false;
        }


        private void OnCmdOK()
        {
            Result         = 1;
            DialogResultEx = true;
        }
    }
}
