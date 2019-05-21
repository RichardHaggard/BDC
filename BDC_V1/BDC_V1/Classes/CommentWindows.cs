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
        [NotNull] public ICommand CmdDelete      { get; }

        public string HeaderText
        {
            get => _headerText;
            set => SetProperty(ref _headerText, value);
        }
        private string _headerText;

        public bool IsChanged
        {
            get => _isChanged;
            set => SetProperty(ref _isChanged, value);
        }
        private bool _isChanged;

        public string CommentText 
        {
            get => _commentText;
            set => SetProperty(ref _commentText, value, () => IsChanged = true);
        }
        private string _commentText;

        public string MikeOffBg
        {
            get => _mikeOffBg;
            set => SetProperty(ref _mikeOffBg, value);
        }
        private string _mikeOffBg = ConstBgActive;


        public string MikeOffBorderBrush
        {
            get => _mikeOffBorderBrush;
            set => SetProperty(ref _mikeOffBorderBrush, value);
        }
        private string _mikeOffBorderBrush = ConstBorderActive;


        public string MikeOnBg
        {
            get => _mikeOnBg;
            set => SetProperty(ref _mikeOnBg, value);
        }
        private string _mikeOnBg = ConstBgInactive;


        public string MikeOnBorderBrush
        {
            get => _mikeOnBorderBrush;
            set => SetProperty(ref _mikeOnBorderBrush, value);
        }
        private string _mikeOnBorderBrush = ConstBgInactive;


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
            CmdDelete      = new DelegateCommand(OnDelete     );   
        }

        // **************** Class members *************************************************** //

        protected override void OnCancelUndo()
        {
            // TODO: Properly determine if changes have been made.
            if ((! IsChanged) ||
                (BdcMessageBoxView.Show("Cancel changes?", "CANCEL CHANGES?", 
                     MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                 MessageBoxResult.Yes))
            {
                base.OnCancelUndo();
            }
        }

        protected virtual void OnReviewLater()
        {
            Result = EnumControlResult.ResultDeferred;
            DialogResultEx = true;
        }

        protected virtual void OnDelete()
        {
            if (BdcMessageBoxView.Show("Permanently delete comment?", "DELETE COMMENT?", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
            {
                Result = EnumControlResult.ResultDeleteItem;
                DialogResultEx = true;
            }
        }

        [NotNull]   protected abstract List<ICommentary> CommentaryList  { get; set; }
        [CanBeNull] protected abstract string            CopyWindowTitle { get; }
        protected virtual void OnCopy()
        {
            var view = new CopyCommentView
            {
                Owner = Application.Current.MainWindow
            };

            if (!(view.DataContext is CopyCommentViewModel model)) 
                throw new InvalidCastException("Invalid View Model");

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
