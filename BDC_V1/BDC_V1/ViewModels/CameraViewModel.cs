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
    public class CameraViewModel  : CloseableResultsWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public bool FlashOn
        {
            get { return _FlashOn; }
            set
            {
                if (SetProperty(ref _FlashOn, value))
                {
                    FlashBg   = value  ? "wHITE" : "Transparent";
                    NoFlashBg = !value ? "wHITE" : "Transparent";
                }
            }
        }
        private bool _FlashOn = true;


        [NotNull] public ICommand CmdCameraButton    { get; }
        [NotNull] public ICommand CmdCropPhoto       { get; }
        [NotNull] public ICommand CmdFlashOff        { get; }
        [NotNull] public ICommand CmdFlashOn         { get; }
        [NotNull] public ICommand CmdRotateClockwise { get; }
        [NotNull] public ICommand CmdRotateCounter   { get; }
        [NotNull] public ICommand CmdSizeStandard    { get; }
        [NotNull] public ICommand CmdSizeLarge       { get; }
        [NotNull] public ICommand CmdExtraSizeLarge  { get; }
        [NotNull] public ICommand CmdDeleteCommand   { get; }


        public string FlashBg
        {
            get { return _FlashBg; }
            set { SetProperty(ref _FlashBg, value); }
        }
        private string _FlashBg = "White";


        public string NoFlashBg
        {
            get { return _NoFlashBg; }
            set { SetProperty(ref _NoFlashBg, value); }
        }
        private string _NoFlashBg = "Transparent";


        [CanBeNull] 
        public ImageSource SourceImage
        {
            get => _sourceImage ?? (_sourceImage = new BitmapImage());
            set => SetProperty(ref _sourceImage, value);
        }
        private ImageSource _sourceImage;


        // **************** Class constructors ********************************************** //

        public CameraViewModel()
        {
            CmdCameraButton    = new DelegateCommand(OnCameraButton   );
            CmdFlashOff        = new DelegateCommand(OnCmdFlashOff    );
            CmdFlashOn         = new DelegateCommand(OnCmdFlashOn     );
            CmdCropPhoto       = new DelegateCommand(OnCropPhoto      );
            CmdRotateClockwise = new DelegateCommand(OnRotateClockwise);
            CmdRotateCounter   = new DelegateCommand(OnRotateCounter  );
            CmdSizeStandard    = new DelegateCommand(OnSizeStandard   );
            CmdSizeLarge       = new DelegateCommand(OnSizeLarge      );
            CmdExtraSizeLarge  = new DelegateCommand(OnExtraSizeLarge );
            CmdDeleteCommand   = new DelegateCommand(OnDeleteCommand  );

#if DEBUG
            SourceImage = new BitmapImage(new Uri(@"pack://application:,,,/Images/Reactor.png"));
#endif
        }

        // **************** Class members *************************************************** //

        private void OnCmdFlashOff()
        {
            FlashOn = false;
        }

        private void OnCmdFlashOn()
        {
            FlashOn = true;
        }


        // currently not implemented
        private void OnDeleteCommand()
        {
            Result = EnumControlResult.ResultDeleteItem;
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
