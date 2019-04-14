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

        public ICommand CmdDistressed { get; }

        public string WindowTitle
        {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }
        private string _windowTitle = "INSPECTION COMMENTS";

        public bool IsDistressedEnabled
        {
            get => _isDistressedEnabled;
            set => SetProperty(ref _isDistressedEnabled, value);
        }
        private bool _isDistressedEnabled = true;


        // TODO: Move these properties into a separate interface / class ???

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

            HeaderText = WindowTitle + "\n" +
                         "This is a two-line auto-wrap text field";
        }

        // **************** Class members *************************************************** //

        protected override List<Commentary> CommentaryList
        {
            get => _commentaryList ?? (_commentaryList = new List<Commentary>());
            set => SetProperty(ref _commentaryList, value);
        }
        private List<Commentary> _commentaryList = new List<Commentary>();

        protected override string CopyWindowTitle => "COPY INSPECTION COMMENT";

        private void OnDistressed()
        {
            if (! IsDistressedEnabled) return;

            Debug.WriteLine("OnDistressed not implemented");
        }

    }
}
