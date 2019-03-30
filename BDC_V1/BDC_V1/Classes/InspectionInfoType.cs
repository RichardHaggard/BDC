using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class InspectionInfoType : IInspectionInfoType
    {
        public EnumInspectionType InspectionType { get; set; }
        public ObservableCollection<ImageSource> Images { get; }
        public string   Component         { get; set; }
        public string   Section           { get; set; }
        public string   Category          { get; set; }
        public string   ComponentType     { get; set; }
        public decimal  Quantity          { get; set; }
        public DateTime InspectionDate    { get; set; }
        public string   Note              { get; set; }
        public string   InspectionComment { get; set; }

        public InspectionInfoType()
        {
            Images = new ObservableCollection<ImageSource>();
        }
    }
}
