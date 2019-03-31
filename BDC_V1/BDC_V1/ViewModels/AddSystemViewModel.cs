using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.ViewModels
{
    public class AddSystemViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string Component
        {
            get { return _Component; }
            set { SetProperty(ref _Component, value); }
        }
        private string _Component;


        public List<string> Components
        {
            get { return _Components; }
            set { SetProperty(ref _Components, value); }
        }
        private List<string> _Components = new List<string>();


        // **************** Class constructors ********************************************** //

        public AddSystemViewModel()
        {
            Components.Add("F10 SPECIAL CONSTRUCTION");
            Components.Add("F20 SELECTIVE BUILDING DEMOLITION");
            Components.Add("G10 SITE PREPARATIONS");
            Components.Add("G90 OTHER SITE CONSTRUCTION");
            Components.Add("H30 COASTAL PROTECTION");
            Components.Add("H40 NAV DREDGING/RECLAMATION");
            Components.Add("H60 WATERFRONT DEMOLITION");
            Components.Add("H70 WATERFRONT ATFP");
        }


        // **************** Class members *************************************************** //
    }
}
