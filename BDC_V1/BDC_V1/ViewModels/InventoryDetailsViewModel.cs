using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.ViewModels
{
    public class InventoryDetailsViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string CurrentSection
        {
            get { return _CurrentSection; }
            set { SetProperty(ref _CurrentSection, value); }
        }
        private string _CurrentSection;


        public string DetailSelector
        {
            get { return _DetailSelector; }
            set { SetProperty(ref _DetailSelector, value); }
        }
        private string _DetailSelector;


        public List<string> DetailSelectors
        {
            get { return _DetailSelectors; }
        }
        private List<string> _DetailSelectors = new List<string>();

        // **************** Class constructors ********************************************** //

        public InventoryDetailsViewModel()
        {
            CurrentSection = "11057 - EAST BAY - D501003 INTERIOR DISTRIBUTION SERVICES dry-type, 480V primary 120/208V secondary, 225kVA";

            DetailSelectors.Add("FL2 - TL12412C - GE - Rm207 - DAMAGED - The nameplate on component was missing ...");
            DetailSelectors.Add("AB3 - GH504xx - AP - Rm111 - DAMAGE - Scratched and defaced");
            DetailSelectors.Add("XY8 - OP583C - PD - Rm456 - SOMET - In very poor taste");

            DetailSelector = "FL2 - TL12412C - GE - Rm207 - DAMAGED - The nameplate on component was missing ...";

        }


        // **************** Class members *************************************************** //


    }
}
