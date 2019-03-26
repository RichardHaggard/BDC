using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get { return _CmdExit; }
            set { SetProperty(ref _CmdExit, value); }
        }
        private ICommand _CmdExit;

        public Visibility WindowVisibility
        {
            get => _windowVisibility;
            set => SetProperty(ref _windowVisibility, value);
        }
        private Visibility _windowVisibility;

        public string InventoryTreeContent
        {
            get { return _InventoryTreeContent; }
            set { SetProperty(ref _InventoryTreeContent, value); }
        }
        private string _InventoryTreeContent;


        public string Title
        {
            get { return _Title; }
            set { SetProperty(ref _Title, value); }
        }
        private string _Title;

        // **************** Class constructors ********************************************** //

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ShellViewModel()
        {
            CmdExit = new DelegateCommand(OnCmdExit);
            InventoryTreeContent = "Inventory Tree <-> Inspection Tree";
            Title = @"Builder DC - My Documents\Project\Subfolder\BRED_HOOD_ABRAMS_E_11057.mdb";
        }


        // **************** INavigationAware interface implementation *********************** //

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoginViewModel ViewModel = new LoginViewModel();
            LoginView view = new LoginView(ViewModel);
            view.ShowDialog();

            if (!ViewModel.LoginSuccessful)
                App.Current.Shutdown();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }



        // **************** Class members *************************************************** //

        public void OnCmdExit()
        {
            App.Current.Shutdown();
        }
    }
}
