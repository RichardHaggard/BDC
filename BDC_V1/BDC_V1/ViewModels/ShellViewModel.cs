using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

        public ICommand CmdExit
        {
            get => _cmdExit;
            set => SetProperty(ref _cmdExit, value);
        }
        private ICommand _cmdExit;

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

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _title;

        // **************** Class constructors ********************************************** //

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ShellViewModel()
        {
            CmdExit = new DelegateCommand(OnCmdExit);
            InventoryTreeContent = "Inventory Tree <-> Inspection Tree";
            Title = @"Builder DC - My Documents\Project\Subfolder\BRED_HOOD_ABRAMS_E_11057.mdb";

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
