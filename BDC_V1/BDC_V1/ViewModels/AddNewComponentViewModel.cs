using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.ViewModels
{
    public class AddNewComponentViewModel : ViewModelBase
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


        public List<string> Components
        {
            get => _components;
            set => SetProperty(ref _components, value);
        }
        private List<string> _components = new List<string>();


        // **************** Class constructors ********************************************** //

        public AddNewComponentViewModel()
        {
            Components.Add("G2010 ROADWAYS");
            Components.Add("G2020 PARKING LOTS");
            Components.Add("G2030 PEDESTRIAN PAVING");
            Components.Add("G2040 SITE DEVELOPMENT");
            Components.Add("G2050 LANDSCAPING");
            Components.Add("G2060 AIRFIELD PACING");
        }


        // **************** Class members *************************************************** //


    }
}
