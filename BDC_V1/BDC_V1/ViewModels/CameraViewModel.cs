using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Views;
using JetBrains.Annotations;
using Microsoft.Win32;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class CameraViewModel  : CloseableWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull] public ICommand CmdCameraButton    { get; }
        [NotNull] public ICommand CmdCropPhoto       { get; }
        [NotNull] public ICommand CmdRotateClockwise { get; }
        [NotNull] public ICommand CmdRotateCounter   { get; }
        [NotNull] public ICommand CmdSizeStandard    { get; }
        [NotNull] public ICommand CmdSizeLarge       { get; }
        [NotNull] public ICommand CmdExtraSizeLarge  { get; }
        [NotNull] public ICommand CmdCancelUndo      { get; }
        [NotNull] public ICommand CmdOkCommand       { get; }
        [NotNull] public ICommand CmdDeleteCommand   { get; }

        [CanBeNull] 
        public ImageSource SourceImage
        {
            get => _sourceImage ?? (_sourceImage = new BitmapImage());
            set => SetProperty(ref _sourceImage, value);
        }
        private ImageSource _sourceImage;


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
            CmdDeleteCommand   = new DelegateCommand(OnDeleteCommand  );

#if DEBUG
#warning Using MOCK data for CameraViewModel
            SourceImage = new BitmapImage(new Uri(@"pack://application:,,,/Images/Reactor.png"));
#endif
        }

        // **************** Class members *************************************************** //

        // currently not implemented
        private void OnDeleteCommand()
        {
            Result = EnumControlResult.ResultDeleteItem;
            DialogResultEx = true;
        }

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

        private string PictureFilename { get; set; }
        private void OnCameraButton()
        {
            var picFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            var openFileDlg = new OpenFileDialog
            {
                Title            = "Open Image",
                FileName         = PictureFilename,
                InitialDirectory = picFolder,
                ReadOnlyChecked  = true,
                Multiselect      = false,
                ShowReadOnly     = false,
                AddExtension     = true,
                CheckFileExists  = true,
                CheckPathExists  = true,
                RestoreDirectory = true,
                Filter = "All files (*.*)|*.*" +
                         "|bmp files (*.bmp)|*.bmp" +
                         "|jpg files (*.jpg)|*.jpg" +
                         "|gif files (*.gif)|*.gif" +
                         "|png files (*.png)|*.png" +
                         "|tif files (*.tif)|*.tif" +
                         "|tiff files (*.tiff)|*.tiff"
            };

            if ((openFileDlg.ShowDialog() == true) && !string.IsNullOrEmpty(openFileDlg.FileName))
            {
                PictureFilename = openFileDlg.FileName;

                try
                {
                    SourceImage = new BitmapImage(new Uri(PictureFilename));
                }
                catch
                {
                    MessageBox.Show(
                        "Selected file \"" + PictureFilename + "\" failed to load, try again.",
                        "IMAGE ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OnCropPhoto      () { Debug.WriteLine("OnCropPhoto       is not implemented"); }
        private void OnRotateClockwise() { Debug.WriteLine("OnRotateClockwise is not implemented"); }
        private void OnRotateCounter  () { Debug.WriteLine("OnRotateCounter   is not implemented"); }
        private void OnSizeStandard   () { Debug.WriteLine("OnSizeStandard    is not implemented"); }
        private void OnSizeLarge      () { Debug.WriteLine("OnSizeLarge       is not implemented"); }
        private void OnExtraSizeLarge () { Debug.WriteLine("OnExtraSizeLarge  is not implemented"); }
    }
}
