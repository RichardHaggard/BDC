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

        [NotNull] public ICommand CmdAddAllPending       { get; }
        [NotNull] public ICommand CmdCapturePhoto        { get; }
        [NotNull] public ICommand CmdRemoveSelected      { get; }
        [NotNull] public ICommand CmdSelectPhoto         { get; }
        [NotNull] public ICommand CmdUnlinkExistingPhoto { get; }

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
        private ObservableCollection<PhotoModel> _pendingList = 
            new ObservableCollection<PhotoModel>();


        public string PhotoType
        {
            get => _photoType;
            set => SetProperty(ref _photoType, value, RefreshTitle);
        }
        private string _photoType = "";

        public string Tab
        {
            get => _tab;
            set => SetProperty(ref _tab, value, RefreshTitle);
        }
        private string _tab = "";


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
        }


        // **************** Class members *************************************************** //

        private void OnCmdAddAllPending()
        {
            BdcMessageBoxView.Show("Add All Pending", "NOT IMPLEMENTED");
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
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException();
#endif
            }
        }

        private void OnCmdRemoveSelected()
        {
            BdcMessageBoxView.Show("Remove Selected", "NOT IMPLEMENTED");
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
                var pm = new PhotoModel(dlg.FileName, 
                    Path.GetFileNameWithoutExtension(dlg.FileName), 
                    DateTime.Now.ToShortDateString());

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
