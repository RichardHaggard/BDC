using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using BDC_V1.ViewModels;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.Classes
{
    public abstract class CommentWindows : CloseableResultsWindow
    {
        // **************** Class enumerations ********************************************** //

        private const string ConstBgActive     = "White";
        private const string ConstBgInactive   = "Transparent";
        private const string ConstBorderActive = "Black";

        // **************** Class properties ************************************************ //

        [NotNull] public ICommand CmdMicOn       { get; }
        [NotNull] public ICommand CmdMicOff      { get; }
        [NotNull] public ICommand CmdCopy        { get; }
        [NotNull] public ICommand CmdSpellCheck  { get; }
        [NotNull] public ICommand CmdReviewLater { get; }


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

        public string MikeOffBg
        {
            get { return _MikeOffBg; }
            set => SetProperty(ref _MikeOffBg, value);
        }
        private string _MikeOffBg = ConstBgInactive;


        public string MikeOffBorderBrush
        {
            get { return _MikeOffBorderBrush; }
            set => SetProperty(ref _MikeOffBorderBrush, value);
        }
        private string _MikeOffBorderBrush = ConstBgInactive;


        public string MikeOnBg
        {
            get { return _MikeOnBg; }
            set => SetProperty(ref _MikeOnBg, value);
        }
        private string _MikeOnBg = ConstBgActive;


        public string MikeOnBorderBrush
        {
            get { return _MikeOnBorderBrush; }
            set => SetProperty(ref _MikeOnBorderBrush, value);
        }
        private string _MikeOnBorderBrush = ConstBorderActive;


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

        [NotNull]   protected abstract List<ICommentary> CommentaryList  { get; set; }
        [CanBeNull] protected abstract string            CopyWindowTitle { get; }
        protected virtual void OnCopy()
        {
            var view = new CopyCommentView();
            if (!(view.DataContext is CopyCommentViewModel model)) 
                throw new InvalidCastException("Invalid View Model");
            view.Owner = Application.Current.MainWindow;

            model.WindowTitle = (! string.IsNullOrEmpty(CopyWindowTitle)) 
                ? CopyWindowTitle 
                : "SELECT COMMENT TO COPY…";

            model.UnFilteredCommentary.Clear();
            model.FacilityBaseInfo = FacilityBaseInfo;
            model.UnFilteredCommentary.AddRange(CommentaryList);

            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

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

        protected virtual void OnMicOn() 
        {
            MikeOffBg          = ConstBgInactive;
            MikeOffBorderBrush = ConstBgInactive;
            MikeOnBg           = ConstBgActive;
            MikeOnBorderBrush  = ConstBorderActive;
        }


        protected virtual void OnMicOff() 
        {
            MikeOffBg          = ConstBgActive;
            MikeOffBorderBrush = ConstBorderActive;
            MikeOnBg           = ConstBgInactive;
            MikeOnBorderBrush  = ConstBgInactive;
        }


        protected virtual void OnSpellCheck() { Debug.WriteLine("OnSpellCheck not implemented"); }
    }
}
