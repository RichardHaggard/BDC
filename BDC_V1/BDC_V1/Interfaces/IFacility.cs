using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Enumerations;

namespace BDC_V1.Interfaces
{
    public interface IFacility : IFacilitySystems
    {
        EnumConstType ConstType    { get; set; }
        string   BuildingId        { get; set; }
        uint     BuildingIdNumber  { get; set; }
        string   BuildingName      { get; set; }
        string   BuildingUse       { get; set; }
        int      YearBuilt         { get; set; }
        string   AlternateId       { get; set; }
        string   AlternateIdSource { get; set; }
        decimal  TotalArea         { get; set; }
        decimal  Width             { get; set; }
        decimal  Depth             { get; set; }
        decimal  Height            { get; set; }
        int      NumFloors         { get; set; }
        IAddress Address           { get; set; }
        IContact Contact           { get; set; }

        ObservableCollection<IComment>    FacilityComments { get; }
        ObservableCollection<ImageSource> Images           { get; }
        ObservableCollection<IInspection> Inspections      { get; }
    }
}
