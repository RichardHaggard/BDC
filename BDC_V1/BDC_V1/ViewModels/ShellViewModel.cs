using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Views;
using Prism.Regions;

namespace BDC_V1.ViewModels
{
    public class ShellViewModel : ViewModelBase, INavigationAware
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string LabelContent
        {
            get { return _LabelContent; }
            set
            {
                if (_LabelContent != value)
                {
                    _LabelContent = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _LabelContent;


        // **************** Class constructors ********************************************** //

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ShellViewModel()
        {
            LabelContent = "Something to confirm ShellViewModel is properly bound.";
        }

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

        // **************** INavigationAware interface implementation *********************** //



        // **************** Class members *************************************************** //

    }
}
