using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BDC_V1.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BDC_V1.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdExit { get; }

        public Visibility WindowVisibility
        {
            get => _windowVisibility;
            set => SetProperty(ref _windowVisibility, value);
        }
        private Visibility _windowVisibility;

        public string InventoryTreeContent
        {
            get => _inventoryTreeContent;
            set => SetProperty(ref _inventoryTreeContent, value);
        }
        private string _inventoryTreeContent;

        public string Title => @"Builder DC - " + ConfigurationFilename;

        public string SelectedLoginUser
        {
            get => _selectedLoginUser;
            set => SetProperty(ref _selectedLoginUser, value);
        }
        private string _selectedLoginUser;

        public string ConfigurationFilename
        {
            get => _configurationFilename;
            set
            {
                if (_configurationFilename != value)
                {
                    SetProperty(ref _configurationFilename, value);
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }
        private string _configurationFilename;

        public string BredFilename
        {
            get => _bredFilename;
            set => SetProperty(ref _bredFilename, value);
        }
        private string _bredFilename;

        // **************** Class constructors ********************************************** //

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ShellViewModel()
        {
            CmdExit = new DelegateCommand(OnCmdExit);
            InventoryTreeContent = "Inventory Tree <-> Inspection Tree";

            // 'this' is your UI element
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                WindowVisibility = Visibility.Visible;
            else
                WindowVisibility = Visibility.Collapsed;
        }

        // **************** Class members *************************************************** //

        public void OnCmdExit()
        {
            App.Current.Shutdown();
        }
    }
}
