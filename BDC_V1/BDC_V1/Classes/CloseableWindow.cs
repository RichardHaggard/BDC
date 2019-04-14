using System.Windows.Input;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using Prism.Commands;

namespace BDC_V1.Classes
{
    public class CloseableWindow : ViewModelBase, ICloseableWindow
    {
        public ICommand CmdCancelUndo  { get; }
        public ICommand CmdOkCommand   { get; }

        /// <summary>
        /// Setting this will cause the dialog to close
        /// </summary>
        /// <remarks>
        /// Setting <see cref="CanClose"/> to false will prevent this from closing the dialog
        /// </remarks>
        public bool? DialogResultEx
        {
            get => _dialogResultEx;
            set
            {
                if (CanClose)
                {
                    SetProperty(ref _dialogResultEx, value);
                }
            }
        }
        private bool? _dialogResultEx;

        /// <summary>
        /// Setting this value to false will block the window from closing via <see cref="DialogResultEx"/>
        /// </summary>
        public bool CanClose
        {
            get => _canClose;
            set => SetProperty(ref _canClose, value);
        }
        private bool _canClose = true;

        /// <summary>
        /// If <see cref="DialogResultEx"/>  is true, this indicates what action is to follow
        /// </summary>
        /// <returns>
        /// <see cref="EnumControlResult.ResultDeleteItem"/> delete was selected.
        /// <see cref="EnumControlResult.ResultCancelled "/> indicates cancellation.
        /// <see cref="EnumControlResult.ResultDeferred  "/> is defer result.
        /// <see cref="EnumControlResult.ResultSaveNow   "/> is save Comment now.
        /// </returns>
        public virtual EnumControlResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        private EnumControlResult _result = EnumControlResult.ResultCancelled;

        // **************** Class constructors ********************************************** //

        protected CloseableWindow()
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
