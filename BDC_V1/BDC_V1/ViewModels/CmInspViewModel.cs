using System;
using System.Diagnostics;
using System.Windows.Input;
using BDC_V1.Enumerations;
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

        public ICommand CmdMicOn       { get; }
        public ICommand CmdMicOff      { get; }
        public ICommand CmdCopy        { get; }
        public ICommand CmdSpellCheck  { get; }
        public ICommand CmdCancelUndo  { get; }
        public ICommand CmdReviewLater { get; }
        public ICommand CmdOkCommand   { get; }
        public ICommand CmdDistressed  { get; }

        /// <summary>
        /// Setting this will cause the dialog to close
        /// </summary>
        public bool? DialogResultEx
        {
            get => _dialogResultEx;
            set => SetProperty(ref _dialogResultEx, value);
        }
        private bool? _dialogResultEx;

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
            get => _comment;
            set => SetProperty(ref _comment, value);
        }
        private string _comment;

        public bool IsRepairType
        {
            get => _isRepairType;
            set => SetProperty(ref _isRepairType, value);
        }
        private bool _isRepairType;

        public bool IsReplacement
        {
            get => _isReplacement;
            set => SetProperty(ref _isReplacement, value);
        }
        private bool _isReplacement;

        public bool IsNoRecommendation
        {
            get => _isNoRecommendation;
            set => SetProperty(ref _isNoRecommendation, value);
        }
        private bool _isNoRecommendation;

        public EnumRepairType RepairType
        {
            get
            {
                if (IsRepairType ) return EnumRepairType.Repair;
                if (IsReplacement) return EnumRepairType.Replace;
                return EnumRepairType.None;
            }

            set
            {
                switch (value)
                {
                    case EnumRepairType.None:
                        IsNoRecommendation = true;
                        break;
                    case EnumRepairType.Repair:
                        IsRepairType = true;
                        break;
                    case EnumRepairType.Replace:
                        IsReplacement = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

        /// <summary>
        /// EnumCommentResult.ResultCancelled indicates cancellation.
        /// EnumCommentResult.ResultDeferred  is defer result.
        /// EnumCommentResult.ResultSaveNow   is save Comment now.
        /// </summary>
        public EnumCommentResult Result { get; set; }

        // **************** Class constructors ********************************************** //

        public CmInspViewModel()
        {
            CmdMicOn       = new DelegateCommand(OnMicOn      );
            CmdMicOff      = new DelegateCommand(OnMicOff     );
            CmdCopy        = new DelegateCommand(OnCopy       );
            CmdSpellCheck  = new DelegateCommand(OnSpellCheck );
            CmdCancelUndo  = new DelegateCommand(OnCancelUndo );
            CmdReviewLater = new DelegateCommand(OnReviewLater);
            CmdOkCommand   = new DelegateCommand(OnOkCommand  );
            CmdDistressed  = new DelegateCommand(OnDistressed );

            HeaderText1 = "<TYPE> Comments for <IDENTIFIER>";
            HeaderText2 = "<IDENTIFIER CONTINUED>";
            CommentText = "DAMAGED - All the wood doors have 70% severe moisture damage.  CRACKED - All of the doors have 65% severe cracking and splintering.";

            RepairType = EnumRepairType.Replace;
        }

        // **************** Class members *************************************************** //

        private void OnCancelUndo()
        {
            Result = EnumCommentResult.ResultCancelled;
            DialogResultEx = false;
        }

        private void OnReviewLater()
        {
            Result = EnumCommentResult.ResultDeferred;
            DialogResultEx = false;
        }

        private void OnOkCommand()
        {
            Result = EnumCommentResult.ResultSaveNow;
            DialogResultEx = true;
        }

        private void OnMicOn      () { Debug.WriteLine("OnMicOn      not implemented"); }
        private void OnMicOff     () { Debug.WriteLine("OnMicOff     not implemented"); }
        private void OnCopy       () { Debug.WriteLine("OnCopy       not implemented"); }
        private void OnSpellCheck () { Debug.WriteLine("OnSpellCheck not implemented"); }
        private void OnDistressed () { Debug.WriteLine("OnDistressed not implemented"); }
    }
}
