using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CmViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdMicOn       { get; }
        public ICommand CmdMicOff      { get; }
        public ICommand CmdCopy        { get; }
        public ICommand CmdSpellCheck  { get; }
        public ICommand CmdCancelUndo  { get; }
        public ICommand CmdReviewLater { get; }
        public ICommand CmdOkCommand   { get; }

        public string HeaderText1
        {
            get => _headerText1;
            set => SetProperty(ref _headerText1, value);
        }
        private string _headerText1;

        public string HeaderText2
        {
            get => _headerText2;
            set => SetProperty(ref _headerText2, value);
        }
        private string _headerText2;

        public string CommentText 
        {
            get => _commentText;
            set => SetProperty(ref _commentText, value);
        }
        private string _commentText;

        // **************** Class constructors ********************************************** //
        
        public CmViewModel()
        {
            CmdMicOn       = new DelegateCommand(OnMicOn      );
            CmdMicOff      = new DelegateCommand(OnMicOff     );
            CmdCopy        = new DelegateCommand(OnCopy       );
            CmdSpellCheck  = new DelegateCommand(OnSpellCheck );
            CmdCancelUndo  = new DelegateCommand(OnCancelUndo );
            CmdReviewLater = new DelegateCommand(OnReviewLater);
            CmdOkCommand   = new DelegateCommand(OnOkCommand  );

            HeaderText1 = "<TYPE> Comments for <IDENTIFIER>";
            HeaderText2 = "<IDENTIFIER CONTINUED>";
            CommentText = "This is a comment";
        }

        // **************** Class members *************************************************** //

        private void OnMicOn      () { Debug.WriteLine("OnMicOn       not implemented"); }
        private void OnMicOff     () { Debug.WriteLine("OnMicOff      not implemented"); }
        private void OnCopy       () { Debug.WriteLine("OnCopy        not implemented"); }
        private void OnSpellCheck () { Debug.WriteLine("OnSpellCheck  not implemented"); }
        private void OnCancelUndo () { Debug.WriteLine("OnCancelUndo  not implemented"); }
        private void OnReviewLater() { Debug.WriteLine("OnReviewLater not implemented"); }
        private void OnOkCommand  () { Debug.WriteLine("OnOkCommand   not implemented"); }
    }
}
