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

        public string Capacity
        {
            get => _Capacity;
            set => SetProperty(ref _Capacity, value);
        }
        private string _Capacity;

        public string ControlTypeMake
        {
            get => _ControlTypeMake;
            set => SetProperty(ref _ControlTypeMake, value);
        }
        private string _ControlTypeMake;


        public string CurrentSection
        {
            get => _currentSection;
            set => SetProperty(ref _currentSection, value);
        }
        private string _currentSection;


        public string DateManufactured
        {
            get => _DateManufactured;
            set => SetProperty(ref _DateManufactured, value);
        }
        private string _DateManufactured;


        public string DetailComment
        {
            get => _DetailComment;
            set => SetProperty(ref _DetailComment, value);
        }
        private string _DetailComment;


        public string DetailIdNumber
        {
            get => _detailIdNumber;
            set => SetProperty(ref _detailIdNumber, value);
        }
        private string _detailIdNumber;


        public string DetailPhotosCount
        {
            get => _DetailPhotosCount;
            set => SetProperty(ref _DetailPhotosCount, value);
        }
        private string _DetailPhotosCount;


        public string DetailSelector
        {
            get => _DetailSelector;
            set => SetProperty(ref _DetailSelector, value);
        }
        private string _DetailSelector;

        public List<string> DetailSelectors { get; } = new List<string>();


        public string EquipmentMake
        {
            get => _EquipmentMake;
            set => SetProperty(ref _EquipmentMake, value);
        }
        private string _EquipmentMake;

        public List<string> EquipmentMakes { get; } = new List<string>();


        public string EquipmentType
        {
            get => _EquipmentType;
            set => SetProperty(ref _EquipmentType, value);
        }
        private string _EquipmentType;

        public string Location
        {
            get => _Location;
            set => SetProperty(ref _Location, value);
        }
        private string _Location;


        public string Manufacturer
        {
            get => _Manufacturer;
            set => SetProperty(ref _Manufacturer, value);
        }
        private string _Manufacturer;


        public List<string> Manufacturers { get; } = new List<string>();


        public string Model
        {
            get => _Model;
            set => SetProperty(ref _Model, value);
        }
        private string _Model;


        public string SerialNumber
        {
            get => _SerialNumber;
            set => SetProperty(ref _SerialNumber, value);
        }
        private string _SerialNumber;

        public string WarrantyCompany
        {
            get => _WarrantyCompany;
            set => SetProperty(ref _WarrantyCompany, value);
        }
        private string _WarrantyCompany;

        public string WarrantyCompany2
        {
            get => _WarrantyCompany2;
            set => SetProperty(ref _WarrantyCompany2, value);
        }
        private string _WarrantyCompany2;

        public string WarrantyDate
        {
            get => _WarrantyDate;
            set => SetProperty(ref _WarrantyDate, value);
        }
        private string _WarrantyDate;

        public string WarrantyDate2
        {
            get => _WarrantyDate2;
            set => SetProperty(ref _WarrantyDate2, value);
        }
        private string _WarrantyDate2;

        public string YearInstalled
        {
            get => _YearInstalled;
            set => SetProperty(ref _YearInstalled, value);
        }
        private string _YearInstalled;


        // **************** Class constructors ********************************************** //

        public InventoryDetailsViewModel()
        {
            CurrentSection = "11057 - EAST BAY - D501003 INTERIOR DISTRIBUTION SERVICES dry-type, 480V primary 120/208V secondary, 225kVA";

            DetailSelectors.Add("FL2 - TL12412C - GE - Rm207 - DAMAGED - The nameplate on component was missing ...");
            DetailSelectors.Add("AB3 - GH504xx - AP - Rm111 - DAMAGE - Scratched and defaced");
            DetailSelectors.Add("XY8 - OP583C - PD - Rm456 - SOMET - In very poor taste");

            DetailSelector = "FL2 - TL12412C - GE - Rm207 - DAMAGED - The nameplate on component was missing ...";

            DetailIdNumber = "N/A";

            Location = "Rm 107";

            Model = "TL12412C";

            SerialNumber = "";

            Manufacturers.Add("GE");
            Manufacturers.Add("Westinghouse");
            Manufacturers.Add("Craftsman");
            Manufacturers.Add("Whilrpool");
            Manufacturer = "GE";

            Capacity = "200 amp";

            EquipmentType = "MLO Panel";

            EquipmentMakes.Add("GE");
            EquipmentMakes.Add("Westinghouse");
            EquipmentMakes.Add("Craftsman");
            EquipmentMakes.Add("Whilrpool");
            EquipmentMake = "GE";

            DateManufactured = DateTime.Now.ToShortDateString();

            YearInstalled = "2007";

            ControlTypeMake = "N/A";

            WarrantyCompany = "";
            WarrantyDate = "";
            WarrantyCompany2 = "";
            WarrantyDate2 = "";

            DetailComment = "[Kurt Benson on 1/17/2019 10:13:03 AM]\n" +
            "The nameplate on the component was missing certain Section Detail fields. Section Detail fields have been populated and fields with NA represent data not found.";
        }


        // **************** Class members *************************************************** //


    }
}
