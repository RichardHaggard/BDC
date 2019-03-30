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
            get => _addCurrentInspector;
            set => SetProperty(ref _addCurrentInspector, value);
        }
        private string _addCurrentInspector;


        public string ComponentType
        {
            get => _componentType;
            set => SetProperty(ref _componentType, value);
        }
        private string _componentType;


        public List<string> ComponentTypes { get; } = new List<string>();


        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        private string _date;


        public string Dcr
        {
            get => _dcr;
            set => SetProperty(ref _dcr, value);
        }
        private string _dcr;


        public List<string> Dcrs { get; } = new List<string>();


        public string EquipmentCategory
        {
            get => _equipmentCategory;
            set => SetProperty(ref _equipmentCategory, value);
        }
        private string _equipmentCategory;


        public List<string> EquipmentCategorys { get; } = new List<string>();


        public bool PaintedIsChecked
        {
            get => _paintedIsChecked;
            set
            {
                if (SetProperty( ref _paintedIsChecked, value ))
                    YearPcVisibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }
        private bool _paintedIsChecked = false;


        public string PcRating
        {
            get => _pcRating;
            set => SetProperty(ref _pcRating, value);
        }
        private string _pcRating = "";


        public List<string> PcRatings { get; } = new List<string>();


        public string PcType
        {
            get => _pcType;
            set => SetProperty(ref _pcType, value);
        }
        private string _pcType = "";


        public string Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        private string _quantity = "";


        public List<string> PcTypes { get; } = new List<string>();


        public string SectionComment
        {
            get => _sectionComment;
            set => SetProperty(ref _sectionComment, value);
        }
        private string _sectionComment;

        public string SectionName
        {
            get => _sectionName;
            set => SetProperty(ref _sectionName, value);
        }
        private string _sectionName;


        public List<string> SectionNames { get; } = new List<string>();


        public string YearInstalledRenewed
        {
            get => _yearInstalledRenewed;
            set => SetProperty(ref _yearInstalledRenewed, value);
        }
        private string _yearInstalledRenewed;


        public string YearPc
        {
            get => _yearPc;
            set => SetProperty(ref _yearPc, value);
        }
        private string _yearPc;


        public Visibility YearPcVisibility
        {
            get => _yearPcVisibility;
            set => SetProperty(ref _yearPcVisibility, value);
        }
        private Visibility _yearPcVisibility = Visibility.Hidden;


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
