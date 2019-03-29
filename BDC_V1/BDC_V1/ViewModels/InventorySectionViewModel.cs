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


        public string PcType
        {
            get { return _PcType; }
            set { SetProperty(ref _PcType, value); }
        }
        private string _PcType = "";


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

            YearPc = "2007";

            PcTypes.Add("Heat-Resist 400 degF Enml");
            PcType = "Heat-Resist 400 degF Enml";

            SectionComment = "[Jane Doe on 1/18/2018 6:19:55 PM]\nThis unit was in a locked room and not visible.\n(Text box large enough for STAMP on line 1 and at least 3 lines of actual comment.)";
        }


        // **************** Class members *************************************************** //


    }
}
