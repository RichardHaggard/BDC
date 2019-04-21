using BDC_V1.Classes;
using BDC_V1.Models;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using BDC_V1.Enumerations;
using BDC_V1.Views;
using JetBrains.Annotations;
using BDC_V1.Utils;

namespace BDC_V1.ViewModels
{
    public class PhotoManagementViewModel : CloseableResultsWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //


        public ICommand CmdAddAllPending       { get; }
        public ICommand CmdCapturePhoto        { get; }
        public ICommand CmdRemoveSelected      { get; }
        public ICommand CmdSelectPhoto         { get; }
        public ICommand CmdUnlinkExistingPhoto { get; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [CanBeNull]
        private IEventAggregator EventAggregator { get; }

        public PhotoModel PendingItem
        {
            get => _pendingItem;
            set => SetProperty(ref _pendingItem, value);
        }
        private PhotoModel _pendingItem;


        public ObservableCollection<PhotoModel> PendingList
        {
            get => _pendingList;
            set => SetProperty(ref _pendingList, value);
        }
        private ObservableCollection<PhotoModel> _pendingList = new ObservableCollection<PhotoModel>();


        public string PhotoType
        {
            get { return _PhotoType; }
            set
            {
                if (_PhotoType != value)
                {
                    _PhotoType = value;
                    RefreshTitle();
                }
            }
        }
        private string _PhotoType = "";

        public string Tab
        {
            get { return _Tab; }
            set
            {
                if (_Tab != value)
                {
                    _Tab = value;
                    RefreshTitle();
                }
            }
        }
        private string _Tab = "";


        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _title = "<TYPE> Photos of <IDENTITY of Facility, Section or Inspection>";


        // **************** Class constructors ********************************************** //

        public PhotoManagementViewModel(IEventAggregator eventAggregator = null)
        {
            EventAggregator = eventAggregator;

            CmdAddAllPending        = new DelegateCommand(OnCmdAddAllPending);
            CmdCapturePhoto         = new DelegateCommand(OnCmdCapturePhoto);
            CmdRemoveSelected       = new DelegateCommand(OnCmdRemoveSelected);
            CmdSelectPhoto          = new DelegateCommand(OnCmdSelectPhoto);
            CmdUnlinkExistingPhoto  = new DelegateCommand(OnCmdUnlinkExistingPhoto);

            PendingList.Add(new PhotoModel("EmeraldHils.jpg", "Emerald Hills", "3/13/2019"));
            PendingList.Add(new PhotoModel("FlamingoWater.jpg.jpg", "FlamingoWater.jpg", "3/12/2019"));
            PendingList.Add(new PhotoModel("th7.jpg", "Fans", "3/11/2019"));
            PendingItem = PendingList[0];
        }


        // **************** Class members *************************************************** //

        private void OnCmdAddAllPending()
        {
            MessageBox.Show("Add All Pending");
        }

        private void OnCmdCapturePhoto()
        {
            var view = new CameraView();
            if (! (view.DataContext is CameraViewModel model))
                throw new InvalidCastException(nameof(view.DataContext));

            //model.SourceImage = PendingItem.Filename TODO: not sure how to convert this
            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

            // TODO: Add / delete results from PendingList in the proper manner
            switch (model.Result)
            {
                case EnumControlResult.ResultDeleteItem:
                case EnumControlResult.ResultCancelled:
                    break;

                case EnumControlResult.ResultDeferred:
                case EnumControlResult.ResultSaveNow:
                    if (! string.IsNullOrEmpty(model.SourceImage?.ToString()))
                    {
                        var imageName = model.SourceImage.ToString();
                        var pm = new PhotoModel(imageName, 
                            Path.GetFileNameWithoutExtension(imageName), 
                            DateTime.Now.ToShortDateString());

                        PendingList.Add(pm);
                        PendingItem = pm;
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnCmdRemoveSelected()
        {
            MessageBox.Show("Remove Selected");
        }

        private void OnCmdSelectPhoto()
        {
            var dlg = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Image files (*.jpg)|*.jpg|(*.png)|*.png|All files (*.*)|*.*",
                Multiselect = false,
                Title = "Select an Image File"
            };

            if (true == dlg.ShowDialog())
            {
                var pm = new PhotoModel(dlg.FileName, Path.GetFileNameWithoutExtension(dlg.FileName), DateTime.Now.ToShortDateString());
                PendingList.Add(pm);
                PendingItem = pm;
            }
        }

        private void OnCmdUnlinkExistingPhoto()
        {
            DialogResultEx = true;
        }


        private void RefreshTitle()
        {
            Title = string.Format
                (
                "{0} Photos of {1}",
                string.IsNullOrEmpty(PhotoType) ? "<TYPE>"                                        : PhotoType,
                string.IsNullOrEmpty(Tab)       ? "<IDENTITY of Facility, Section or Inspection>" : Tab
                );
        }
    }
}
