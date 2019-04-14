using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BDC_V1.Classes;
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
            MessageBox.Show("Select Photo");
        }

        private void OnCmdUnlinkExistingPhoto()
        {
            DialogResultEx = true;
        }

    }
}
