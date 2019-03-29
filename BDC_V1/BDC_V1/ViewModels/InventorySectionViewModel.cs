using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BDC_V1.ViewModels
{
    public class InventorySectionViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        public string AddCurrentInspector
        {
            get { return _AddCurrentInspector; }
            set { SetProperty(ref _AddCurrentInspector, value); }
        }
        private string _AddCurrentInspector;


        public string ComponentType
        {
            get { return _ComponentType; }
            set { SetProperty(ref _ComponentType, value); }
        }
        private string _ComponentType;


        public List<string> ComponentTypes
        {
            get {return _ComponentTypes;}
        }
        private List<string> _ComponentTypes = new List<string>();


        public string Date
        {
            get { return _Date; }
            set { SetProperty(ref _Date, value); }
        }
        private string _Date;


        public string Dcr
        {
            get { return _Dcr; }
            set { SetProperty(ref _Dcr, value); }
        }
        private string _Dcr;


        public List<string> Dcrs
        {
            get {return _Dcrs;}
        }
        private List<string> _Dcrs = new List<string>();


        public string EquipmentCategory
        {
            get { return _EquipmentCategory; }
            set { SetProperty(ref _EquipmentCategory, value); }
        }
        private string _EquipmentCategory;


        public List<string> EquipmentCategorys
        {
            get {return _EquipmentCategorys;}
        }
        private List<string> _EquipmentCategorys = new List<string>();


        public bool PaintedIsChecked
        {
            get { return _PaintedIsChecked; }
            set
            {
                if (SetProperty( ref _PaintedIsChecked, value ))
                    YearPcVisibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }
        private bool _PaintedIsChecked = false;


        public string PcRating
        {
            get { return _PcRating; }
            set { SetProperty(ref _PcRating, value); }
        }
        private string _PcRating = "";


        public List<string> PcRatings
        {
            get { return _PcRatings; }
        }
        private List<string> _PcRatings = new List<string>();


        public string PcType
        {
            get { return _PcType; }
            set { SetProperty(ref _PcType, value); }
        }
        private string _PcType = "";


        public string Quantity
        {
            get { return _Quantity; }
            set { SetProperty(ref _Quantity, value); }
        }
        private string _Quantity = "";


        public List<string> PcTypes
        {
            get { return _PcTypes; }
        }
        private List<string> _PcTypes = new List<string>();


        public string SectionComment
        {
            get { return _SectionComment; }
            set { SetProperty(ref _SectionComment, value); }
        }
        private string _SectionComment;

        public string SectionName
        {
            get { return _SectionName; }
            set { SetProperty(ref _SectionName, value); }
        }
        private string _SectionName;


        public List<string> SectionNames
        {
            get {return _SectionNames;}
        }
        private List<string> _SectionNames = new List<string>();


        public string YearPc
        {
            get { return _YearPc; }
            set { SetProperty(ref _YearPc, value); }
        }
        private string _YearPc;


        public Visibility YearPcVisibility
        {
            get { return _YearPcVisibility; }
            set { SetProperty(ref _YearPcVisibility, value); }
        }
        private Visibility _YearPcVisibility;


        // **************** Class constructors ********************************************** //

        public InventorySectionViewModel()
        {
            SectionNames.Add("Heating System");
            SectionName = "Heating System";

            EquipmentCategorys.Add("D302001 BOILERS");
            EquipmentCategory = "D302001 BOILERS";

            ComponentTypes.Add("Permanent");
            ComponentType = "ComponentTypes";

            Quantity = "2.00";

            YearPc = "2007";

            PcTypes.Add("Heat-Resist 400 degF Enml");
            PcTypes.Add("Moderate 300 degF Enml");
            PcTypes.Add("Extreme 1400 degF Enml");
            PcType = "Heat-Resist 400 degF Enml";

            SectionComment = "[Jane Doe on 1/18/2018 6:19:55 PM]\nThis unit was in a locked room and not visible.\n(Text box large enough for STAMP on line 1 and at least 3 lines of actual comment.)";

            Date = DateTime.Now.ToShortDateString();

            Dcrs.Add("D1");
            Dcrs.Add("D2");
            Dcrs.Add("D3");
            Dcr = "D2";

            PcRatings.Add("PcRating1");
            PcRatings.Add("PcRating2");
            PcRatings.Add("PcRating3");
            PcRating = "PcRating2";

        }


        // **************** Class members *************************************************** //


    }
}
