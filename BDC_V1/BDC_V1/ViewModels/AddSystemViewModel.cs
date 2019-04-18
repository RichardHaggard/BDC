using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class AddSystemViewModel : CloseableResultsWindow
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string Component
        {
            get => _component;
            set => SetProperty(ref _component, value);
        }
        private string _component;


        public ObservableCollection<string> Components
        {
            get => _components;
            set => SetProperty(ref _components, value);
        }
        private ObservableCollection<string> _components = new ObservableCollection<string>();


        // **************** Class constructors ********************************************** //

        public AddSystemViewModel()
        {
#if DEBUG
#warning Using MOCK data for AddNewComponentViewModel
            Components.AddRange(new []
            {
                "F10 SPECIAL CONSTRUCTION"         ,
                "F20 SELECTIVE BUILDING DEMOLITION",
                "G10 SITE PREPARATIONS"            ,
                "G90 OTHER SITE CONSTRUCTION"      ,
                "H30 COASTAL PROTECTION"           ,
                "H40 NAV DREDGING/RECLAMATION"     ,
                "H60 WATERFRONT DEMOLITION"        ,
                "H70 WATERFRONT ATFP"
            });
#endif
        }


        // **************** Class members *************************************************** //

    }
}
