using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Models;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;

namespace BDC_V1.ViewModels
{
    public class PhotoManagementViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //


        public ICommand CmdAddAllPending
        {
            get { return _CmdAddAllPending; }
            set { SetProperty(ref _CmdAddAllPending, value); }
        }
        private ICommand _CmdAddAllPending;


        public ICommand CmdCapturePhoto
        {
            get { return _CmdCapturePhoto; }
            set { SetProperty(ref _CmdCapturePhoto, value); }
        }
        private ICommand _CmdCapturePhoto;


        public ICommand CmdRemoveSelected
        {
            get { return _CmdRemoveSelected; }
            set { SetProperty(ref _CmdRemoveSelected, value); }
        }
        private ICommand _CmdRemoveSelected;


        public ICommand CmdSelectPhoto
        {
            get { return _CmdSelectPhoto; }
            set { SetProperty(ref _CmdSelectPhoto, value); }
        }
        private ICommand _CmdSelectPhoto;


        public ICommand CmdUnlinkExistingPhoto
        {
            get { return _CmdUnlinkExistingPhoto; }
            set { SetProperty(ref _CmdUnlinkExistingPhoto, value); }
        }
        private ICommand _CmdUnlinkExistingPhoto;


        public bool? DialogResultEx
        {
            get { return _DialogResultEx; }
            set { SetProperty(ref _DialogResultEx, value); }
        }
        private bool? _DialogResultEx;


        private IEventAggregator EventAggregator { get; set; }


        public PhotoModel PendingItem
        {
            get { return _PendingItem; }
            set { SetProperty(ref _PendingItem, value); }
        }
        private PhotoModel _PendingItem;


        public ObservableCollection<PhotoModel> PendingList
        {
            get { return _PendingList; }
            set { SetProperty(ref _PendingList, value); }
        }
        private ObservableCollection<PhotoModel> _PendingList = new ObservableCollection<PhotoModel>();


        public string Title
        {
            get { return _Title; }
            set { SetProperty(ref _Title, value); }
        }
        private string _Title = "<TYPE> Photos of <IDENTITY of Facility, Sectin or Inspection>";


        // **************** Class constructors ********************************************** //

        public PhotoManagementViewModel()
        {
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


        public PhotoManagementViewModel(IEventAggregator eventAggregator )
        : this()
        {
            EventAggregator = eventAggregator;
        }



        // **************** Class members *************************************************** //

        private void OnCmdAddAllPending()
        {
            MessageBox.Show("Add All Pending");
        }

        private void OnCmdCapturePhoto()
        {
            MessageBox.Show("Capture Photo");
        }

        private void OnCmdRemoveSelected()
        {
            MessageBox.Show("Remove Selected");
        }

        private void OnCmdSelectPhoto()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = "Image files (*.jpg)|*.jpg|(*.png)|*.png|All files (*.*)|*.*";
            dlg.Multiselect = false;
            dlg.Title = "Select an Image File";
            if (true == dlg.ShowDialog() )
            {
                PhotoModel pm = new PhotoModel(dlg.FileName, Path.GetFileNameWithoutExtension(dlg.FileName), DateTime.Now.ToShortDateString());
                PendingList.Add(pm);
                PendingItem = pm;
            }
        }

        private void OnCmdUnlinkExistingPhoto()
        {
            DialogResultEx = true;
        }

    }
}
