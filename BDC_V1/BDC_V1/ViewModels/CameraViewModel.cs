using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDC_V1.Enumerations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CameraViewModel  : CloseableWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdCancelUndo  { get; }
        public ICommand CmdOkCommand   { get; }


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


        // **************** Class constructors ********************************************** //

        public CameraViewModel()
        {
            CmdCancelUndo  = new DelegateCommand(OnCancelUndo );
            CmdOkCommand   = new DelegateCommand(OnOkCommand  );
        }

        // **************** Class members *************************************************** //

        private void OnCancelUndo()
        {
            Result = EnumCommentResult.ResultCancelled;
            DialogResultEx = false;
        }

        private void OnOkCommand()
        {
            Result = EnumCommentResult.ResultSaveNow;
            DialogResultEx = true;
        }

    }
}
