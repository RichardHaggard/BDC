using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Interfaces;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class InventoryDetailsType : BindableBase, IInventoryDetailsType
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string Capacity
        {
            get => _capacity;
            set => SetProperty(ref _capacity, value);
        }
        private string _capacity;

        public string ControlTypeMake
        {
            get => _controlTypeMake;
            set => SetProperty(ref _controlTypeMake, value);
        }
        private string _controlTypeMake;

        public string CurrentSection
        {
            get => _currentSection;
            set => SetProperty(ref _currentSection, value);
        }
        private string _currentSection;

        public string DateManufactured
        {
            get => _dateManufactured;
            set => SetProperty(ref _dateManufactured, value);
        }
        private string _dateManufactured;

        public string DetailComment
        {
            get => _detailComment;
            set => SetProperty(ref _detailComment, value);
        }
        private string _detailComment;

        public string DetailIdNumber
        {
            get => _detailIdNumber;
            set => SetProperty(ref _detailIdNumber, value);
        }
        private string _detailIdNumber;

        public string DetailPhotosCount
        {
            get => _detailPhotosCount;
            set => SetProperty(ref _detailPhotosCount, value);
        }
        private string _detailPhotosCount;

        public string DetailSelector
        {
            get => _detailSelector;
            set => SetProperty(ref _detailSelector, value);
        }
        private string _detailSelector;

        public string EquipmentMake
        {
            get => _equipmentMake;
            set => SetProperty(ref _equipmentMake, value);
        }
        private string _equipmentMake;

        public string EquipmentType
        {
            get => _equipmentType;
            set => SetProperty(ref _equipmentType, value);
        }
        private string _equipmentType;

        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }
        private string _location;

        public string Manufacturer
        {
            get => _manufacturer;
            set => SetProperty(ref _manufacturer, value);
        }
        private string _manufacturer;

        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }
        private string _model;

        public string SerialNumber
        {
            get => _serialNumber;
            set => SetProperty(ref _serialNumber, value);
        }
        private string _serialNumber;

        public string WarrantyCompany
        {
            get => _warrantyCompany;
            set => SetProperty(ref _warrantyCompany, value);
        }
        private string _warrantyCompany;

        public string WarrantyCompany2
        {
            get => _warrantyCompany2;
            set => SetProperty(ref _warrantyCompany2, value);
        }
        private string _warrantyCompany2;

        public string WarrantyDate
        {
            get => _warrantyDate;
            set => SetProperty(ref _warrantyDate, value);
        }
        private string _warrantyDate;

        public string WarrantyDate2
        {
            get => _warrantyDate2;
            set => SetProperty(ref _warrantyDate2, value);
        }
        private string _warrantyDate2;

        public string YearInstalled
        {
            get => _yearInstalled;
            set => SetProperty(ref _yearInstalled, value);
        }
        private string _yearInstalled;

        public string InventoryDetails
        {
            get => _inventoryDetails;
            set => SetProperty(ref _inventoryDetails, value);
        }
        private string _inventoryDetails;

        public QuickObservableCollection<string> DetailSelectors { get; } = 
            new QuickObservableCollection<string>();

        public QuickObservableCollection<string> EquipmentMakes { get; } = 
            new QuickObservableCollection<string>();

        public QuickObservableCollection<string> Manufacturers { get; } = 
            new QuickObservableCollection<string>();

        public QuickObservableCollection<ImageSource> Images { get; } = 
            new QuickObservableCollection<ImageSource>();
    }

}
