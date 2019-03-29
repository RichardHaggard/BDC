using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.ViewModels
{
    public class FacilityViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        //public string AltId
        //{
        //    get { return _AltId; }
        //    set { SetProperty(ref _AltId, value); }
        //}
        //private string _AltId;


        //public string AltIdSource
        //{
        //    get { return _AltIdSource; }
        //    set { SetProperty(ref _AltIdSource, value); }
        //}
        //private string _AltIdSource;


        //public string BuildingIdLong
        //{
        //    get { return _BuildingIdLong; }
        //    set { SetProperty(ref _BuildingIdLong, value); }
        //}
        //private string _BuildingIdLong;


        //public string BuildingIdShort
        //{
        //    get { return _BuildingIdShort; }
        //    set { SetProperty(ref _BuildingIdShort, value); }
        //}
        //private string _BuildingIdShort;


        //public string BuildingUse
        //{
        //    get { return _BuildingUse; }
        //    set { SetProperty(ref _BuildingUse, value); }
        //}
        //private string _BuildingUse;


        //public string City
        //{
        //    get { return _City; }
        //    set { SetProperty(ref _City, value); }
        //}
        //private string _City;


        //public string ConstType
        //{
        //    get { return _ConstType; }
        //    set { SetProperty(ref _ConstType, value); }
        //}
        //private string _ConstType;


        //public string Depth
        //{
        //    get { return _Depth; }
        //    set { SetProperty(ref _Depth, value); }
        //}
        //private string _Depth;


        //public string EMail
        //{
        //    get { return _EMail; }
        //    set { SetProperty(ref _EMail, value); }
        //}
        //private string _EMail;


        //public string FacilityComment
        //{
        //    get { return _FacilityComment; }
        //    set { SetProperty(ref _FacilityComment, value); }
        //}
        //private string _FacilityComment;


        //public string FloorHeight
        //{
        //    get { return _FloorHeight; }
        //    set { SetProperty(ref _FloorHeight, value); }
        //}
        //private string _FloorHeight;


        //public string Name
        //{
        //    get { return _Name; }
        //    set { SetProperty(ref _Name, value); }
        //}
        //private string _Name;


        //public string NoFloors
        //{
        //    get { return _NoFloors; }
        //    set { SetProperty(ref _NoFloors, value); }
        //}
        //private string _NoFloors;


        //public string Phone
        //{
        //    get { return _Phone; }
        //    set { SetProperty(ref _Phone, value); }
        //}
        //private string _Phone;


        //public string PhotoQty
        //{
        //    get { return _PhotoQty; }
        //    set { SetProperty(ref _PhotoQty, value); }
        //}
        //private string _PhotoQty = "Qty: 2";


        //public string PocEmail
        //{
        //    get { return _PocEmail; }
        //    set { SetProperty(ref _PocEmail, value); }
        //}
        //private string _PocEmail;


        //public string PocName
        //{
        //    get { return _PocName; }
        //    set { SetProperty(ref _PocName, value); }
        //}
        //private string _PocName;


        //public string PocPhone
        //{
        //    get { return _PocPhone; }
        //    set { SetProperty(ref _PocPhone, value); }
        //}
        //private string _PocPhone;


        //public string Quantity
        //{
        //    get { return _Quantity; }
        //    set { SetProperty(ref _Quantity, value); }
        //}
        //private string _Quantity;


        //public string StateProv
        //{
        //    get { return _StateProv; }
        //    set { SetProperty(ref _StateProv, value); }
        //}
        //private string _StateProv;


        //public string Street
        //{
        //    get { return _Street; }
        //    set { SetProperty(ref _Street, value); }
        //}
        //private string _Street;


        //public string Width
        //{
        //    get { return _Width; }
        //    set { SetProperty(ref _Width, value); }
        //}
        //private string _Width;


        //public string YrBuilt
        //{
        //    get { return _YrBuilt; }
        //    set { SetProperty(ref _YrBuilt, value); }
        //}
        //private string _YrBuilt;


        //public string ZipPostCode
        //{
        //    get { return _ZipPostCode; }
        //    set { SetProperty(ref _ZipPostCode, value); }
        //}
        //private string _ZipPostCode;

        public IFacility LocalFacilityInfo
        {
            get => _localFacilityInfo;
            set => SetProperty(ref _localFacilityInfo, value);
        }
        private IFacility _localFacilityInfo;

        //protected override IConfigInfo LocalConfigInfo
        //{
        //    get => base.LocalConfigInfo;
        //    set => base.LocalConfigInfo = value;
        //}

        protected override IBredInfo LocalBredInfo
        {
            get => base.LocalBredInfo;
            set
            {
                base.LocalBredInfo = value;
                LocalFacilityInfo = base.LocalBredInfo?.FacilityInfo;
            }
        }

        // **************** Class constructors ********************************************** //

        public FacilityViewModel()
        {
            //BuildingIdShort = "ARMRY";
            //BuildingIdLong  = "National Guard Readiness Center";
            //BuildingUse     = "17180 - ARNG ARMORY";
            //ConstType       = "Permanent";
            //Quantity = "87,840.00";
            //Width = "500.0";
            //NoFloors = "1";
            //YrBuilt = "2007";
            //Depth = "175.7";
            //FloorHeight = "8.0";
            //AltId = "350939";
            //AltIdSource = "hqiis";
            //FacilityComment = "[Brian Rupert 01/08/19 11:38 AM] No A2D and D10 systems present. Could not gain access to Supply RM C342.";
            //Street = "4500 Silverado Ranch Blvd";
            //City = "Las Vegas";
            //StateProv = "NV";
            //ZipPostCode = "89139-8366";
            //PocName = "Robert Murphy";
            //PocPhone = "555-123-4567 X201";
            //PocEmail = "robert.murphy@somedomain.mil";
        }


        // **************** Class members *************************************************** //

    }
}
