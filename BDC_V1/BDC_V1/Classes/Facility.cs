using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class Facility : IFacility
    {
        public EnumConstType ConstType    { get; set; }
        public string   BuildingId        { get; set; }
        public string   BuildingName      { get; set; }
        public string   BuildingUse       { get; set; }
        public int      YearBuilt         { get; set; }
        public string   AlternateId       { get; set; }
        public string   AlternateIdSource { get; set; }
        public decimal  TotalArea         { get; set; }
        public decimal  Width             { get; set; }
        public decimal  Depth             { get; set; }
        public decimal  Height            { get; set; }
        public int      NumFloors         { get; set; }
        public string   FacilityComments  { get; set; }
        public IAddress Address           { get; set; }
        public IContact Contact           { get; set; }
        public IList<TreeNode> FacilityTreeNodes { get; } 
        public ObservableCollection<ImageSource> Images { get; }
        public ObservableCollection<IInspector>  Inspections { get; }

        public Facility()
        {
            Images            = new ObservableCollection<ImageSource>();
            Inspections       = new ObservableCollection<IInspector> ();
            FacilityTreeNodes = new List<TreeNode>();
        }
    }
}
