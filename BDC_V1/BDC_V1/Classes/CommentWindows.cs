using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.ViewModels;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.Classes
{
    public abstract class CommentWindows : CloseableWindow
    {
        // **************** Class properties ************************************************ //

        public ICommand CmdMicOn       { get; }
        public ICommand CmdMicOff      { get; }
        public ICommand CmdCopy        { get; }
        public ICommand CmdSpellCheck  { get; }
        public ICommand CmdReviewLater { get; }

        public string HeaderText
        {
            get => _headerText;
            set => SetProperty(ref _headerText, value);
        }
        private string _headerText;

        public string CommentText 
        {
            get => _commentText;
            set => SetProperty(ref _commentText, value);
        }
        private string _commentText;

        public IFacilityBase FacilityBaseInfo
        {
            get => _facilityBaseInfo;
            set => SetProperty(ref _facilityBaseInfo, value);
        }
        private IFacilityBase _facilityBaseInfo;

        // **************** Class constructors ********************************************** //

        protected CommentWindows()
        {
            CmdMicOn       = new DelegateCommand(OnMicOn      );
            CmdMicOff      = new DelegateCommand(OnMicOff     );
            CmdCopy        = new DelegateCommand(OnCopy       );
            CmdSpellCheck  = new DelegateCommand(OnSpellCheck );
            CmdReviewLater = new DelegateCommand(OnReviewLater);
        }

        // **************** Class members *************************************************** //

        protected virtual void OnReviewLater()
        {
            Result = EnumControlResult.ResultDeferred;
            DialogResultEx = false;
        }

        [NotNull]   protected abstract List<Commentary> CommentaryList  { get; set; }
        [CanBeNull] protected abstract string           CopyWindowTitle { get; }
        protected virtual void OnCopy()
        {
            var view = new CopyCommentView();
            if (!(view.DataContext is CopyCommentViewModel model)) 
                throw new InvalidCastException("Invalid View Model");

            model.WindowTitle = (! string.IsNullOrEmpty(CopyWindowTitle)) 
                ? CopyWindowTitle 
                : "SELECT COMMENT TO COPY…";

            model.UnFilteredCommentary.Clear();
            model.FacilityBaseInfo = FacilityBaseInfo;
            model.UnFilteredCommentary.AddRange(CommentaryList);

            if (view.ShowDialog() != true) return;

            switch (model.Result)
            {
                case EnumControlResult.ResultDeleteItem:
                case EnumControlResult.ResultCancelled:
                case EnumControlResult.ResultDeferred:
                    break;

                case EnumControlResult.ResultSaveNow:
                    CommentaryList.Clear();
                    CommentaryList.AddRange(model.UnFilteredCommentary);
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException();
#endif
            }
        }

        protected virtual void OnMicOn     () { Debug.WriteLine("OnMicOn      not implemented"); }
        protected virtual void OnMicOff    () { Debug.WriteLine("OnMicOff     not implemented"); }
        protected virtual void OnSpellCheck() { Debug.WriteLine("OnSpellCheck not implemented"); }
    }
}
