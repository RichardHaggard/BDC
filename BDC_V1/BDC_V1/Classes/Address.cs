using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class Address : BindableBase, IAddress
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

        public string Street1
        {
            get => _street1;
            set => SetProperty(ref _street1, value);
        }
        private string _street1;

        public string Street2
        {
            get => _street2;
            set => SetProperty(ref _street2, value);
        }
        private string _street2;

        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }
        private string _city;

        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }
        private string _state;

        public string Zipcode
        {
            get => _zipcode;
            set => SetProperty(ref _zipcode, value);
        }
        private string _zipcode;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //


        // **************** Class members *************************************************** //
    }
}
