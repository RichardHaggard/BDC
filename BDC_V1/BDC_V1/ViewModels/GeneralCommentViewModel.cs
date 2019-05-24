using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class GeneralCommentViewModel : CommentWindows
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull] public ICommand CmdDistressed { get; }

        public string WindowTitle
        {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }
        private string _windowTitle = "INSPECTION COMMENT";

        public bool IsCopyEnabled
        {
            get => _isCopyEnabled;
            set => SetProperty(ref _isCopyEnabled, value);
        }
        private bool _isCopyEnabled;

        public bool IsDistressedEnabled
        {
            get => _isDistressedEnabled;
            set => SetProperty(ref _isDistressedEnabled, value);
        }
        private bool _isDistressedEnabled = true;

        public EnumRepairType RepairType
        {
            get => IsDistressedEnabled ? _repairType : EnumRepairType.None;
            set => SetProperty(ref _repairType, value);
        }
        private EnumRepairType _repairType = EnumRepairType.None;

        // **************** Class constructors ********************************************** //

        public GeneralCommentViewModel()
        {
            CmdDistressed = new DelegateCommand(OnDistressed);
            HeaderText = "Inspection Comment on 11507 - FL1 - D302001 BOILERS - <Inspection Date>";
        }

        // **************** Class members *************************************************** //

        public override IList<ICommentary> CommentaryList
        {
            get => _commentaryList ?? (_commentaryList = new List<ICommentary>());
            set => SetProperty(ref _commentaryList, value);
        }
        private IList<ICommentary> _commentaryList;

        protected override string CopyWindowTitle => "COPY INSPECTION COMMENT";

        private void OnDistressed()
        {
            if (! IsDistressedEnabled) return;

            var view = new DistressPopupView();
            if (! (view.DataContext is DistressPopupViewModel model))
                throw new ApplicationException("view model is not DistressPopupViewModel");

            if (view.ShowDialog() == true)
            {
                switch (model.Result)
                {
                    case EnumControlResult.ResultDeleteItem:
                    case EnumControlResult.ResultDeferred:
                    case EnumControlResult.ResultCancelled:
                        break;

                    case EnumControlResult.ResultSaveNow:
                        break;
#if DEBUG
                    default:
                        throw new ArgumentOutOfRangeException();
#endif
                }
            }
        }

    }
}
