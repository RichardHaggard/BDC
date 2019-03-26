using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BDC_V1.Views;
using Prism.Regions;

namespace BDC_V1.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string LabelContent
        {
            get => _labelContent;
            set => SetProperty(ref _labelContent, value);
        }
        private string _labelContent;

        public Visibility WindowVisibility
        {
            get => _windowVisibility;
            set => SetProperty(ref _windowVisibility, value);
        }
        private Visibility _windowVisibility;

        // **************** Class constructors ********************************************** //

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ShellViewModel()
        {
            LabelContent = "Something to confirm ShellViewModel is properly bound.";
            WindowVisibility = Visibility.Hidden;
        }

        // **************** Class members *************************************************** //

    }
}
