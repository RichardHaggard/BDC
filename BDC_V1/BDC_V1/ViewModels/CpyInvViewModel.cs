using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CpyInvViewModel : CloseableWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdCancelButton { get; }
        public ICommand CmdCopyButton   { get; }

        /// <summary>
        /// EnumCommentResult.ResultCancelled indicates cancellation.
        /// EnumCommentResult.ResultDeferred  is defer result.
        /// EnumCommentResult.ResultSaveNow   is save Comment now.
        /// </summary>
        public EnumCommentResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        private EnumCommentResult _result;

        [NotNull]
        public IList<IItemChecklist> ListOfSystems { get; } = new List<IItemChecklist>();

        // **************** Class constructors ********************************************** //

        public CpyInvViewModel()
        {
            CmdCancelButton = new DelegateCommand(OnCancelButton);
            CmdCopyButton   = new DelegateCommand(OnCopyButton  );

            ListOfSystems.Add(new ItemChecklist() {ItemName = "C10 - INTERIOR CONSTRUCTION", ItemIsChecked = false});
            ListOfSystems.Add(new ItemChecklist() {ItemName = "C20 - STAIRS"               , ItemIsChecked = false});
            ListOfSystems.Add(new ItemChecklist() {ItemName = "C30 - INTERIOR FINISHES"    , ItemIsChecked = false});
        }

        // **************** Class members *************************************************** //

        private void OnCancelButton()
        {
            Result = EnumCommentResult.ResultCancelled;
            DialogResultEx = false;
        }

        private void OnCopyButton()
        {
            Result = EnumCommentResult.ResultSaveNow;
            DialogResultEx = true;
        }


    }
}
