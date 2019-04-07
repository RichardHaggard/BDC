using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDC_V1.Enumerations;
using BDC_V1.Views;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CameraViewModel  : CloseableWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdCameraButton    { get; }
        public ICommand CmdCropPhoto       { get; }
        public ICommand CmdRotateClockwise { get; }
        public ICommand CmdRotateCounter   { get; }
        public ICommand CmdSizeStandard    { get; }
        public ICommand CmdSizeLarge       { get; }
        public ICommand CmdExtraSizeLarge  { get; }
        public ICommand CmdCancelUndo      { get; }
        public ICommand CmdOkCommand       { get; }

        /// <summary>
        /// EnumControlResult.ResultCancelled indicates cancellation.
        /// EnumControlResult.ResultDeferred  is defer result.
        /// EnumControlResult.ResultSaveNow   is save Comment now.
        /// </summary>
        public EnumControlResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        private EnumControlResult _result;


        // **************** Class constructors ********************************************** //

        public CameraViewModel()
        {
            CmdCameraButton    = new DelegateCommand(OnCameraButton   );
            CmdCropPhoto       = new DelegateCommand(OnCropPhoto      );
            CmdRotateClockwise = new DelegateCommand(OnRotateClockwise);
            CmdRotateCounter   = new DelegateCommand(OnRotateCounter  );
            CmdSizeStandard    = new DelegateCommand(OnSizeStandard   );
            CmdSizeLarge       = new DelegateCommand(OnSizeLarge      );
            CmdExtraSizeLarge  = new DelegateCommand(OnExtraSizeLarge );
            CmdCancelUndo      = new DelegateCommand(OnCancelUndo     );
            CmdOkCommand       = new DelegateCommand(OnOkCommand      );
        }

        // **************** Class members *************************************************** //

        private void OnCancelUndo()
        {
            Result = EnumControlResult.ResultCancelled;
            DialogResultEx = false;
        }

        private void OnOkCommand()
        {
            Result = EnumControlResult.ResultSaveNow;
            DialogResultEx = true;
        }

        private void OnCameraButton()
        {
            var view = new CameraView();
            view.ShowDialog();
        }

        private void OnCropPhoto      () { Debug.WriteLine("OnCropPhoto       is not implemented"); }
        private void OnRotateClockwise() { Debug.WriteLine("OnRotateClockwise is not implemented"); }
        private void OnRotateCounter  () { Debug.WriteLine("OnRotateCounter   is not implemented"); }
        private void OnSizeStandard   () { Debug.WriteLine("OnSizeStandard    is not implemented"); }
        private void OnSizeLarge      () { Debug.WriteLine("OnSizeLarge       is not implemented"); }
        private void OnExtraSizeLarge () { Debug.WriteLine("OnExtraSizeLarge  is not implemented"); }
    }
}
