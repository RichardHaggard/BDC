using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Enumerations;

namespace BDC_V1.Interfaces
{
    public interface IInspectionInfoType
    {
        EnumInspectionType InspectionType { get; set; }
        QuickObservableCollection<ImageSource> Images   { get; }
        string   Component         { get; set; }
        string   Section           { get; set; }
        string   Category          { get; set; }
        string   ComponentType     { get; set; }
        decimal  Quantity          { get; set; }
        DateTime InspectionDate    { get; set; }
        string   Note              { get; set; }
        string   InspectionComment { get; set; }
    }
}
