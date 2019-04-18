using System.Windows.Input;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using Prism.Commands;

namespace BDC_V1.Classes
{
    public abstract class CloseableResultsWindow : CloseableWindow, ICloseableResultsWindow
    {
        public ICommand CmdCancelUndo  { get; }
        public ICommand CmdOkCommand   { get; }

        /// <inheritdoc/>
        public virtual EnumControlResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        private EnumControlResult _result = EnumControlResult.ResultCancelled;

        // **************** Class constructors ********************************************** //

        protected CloseableResultsWindow()
        {
            CmdCancelUndo = new DelegateCommand(OnCancelUndo);
            CmdOkCommand  = new DelegateCommand(OnOkCommand );
        }

        // **************** Class members *************************************************** //

        protected virtual void OnCancelUndo()
        {
            Result = EnumControlResult.ResultCancelled;
            DialogResultEx = false;
        }

        protected virtual void OnOkCommand()
        {
            Result = EnumControlResult.ResultSaveNow;
            DialogResultEx = true;
        }

    }
}
