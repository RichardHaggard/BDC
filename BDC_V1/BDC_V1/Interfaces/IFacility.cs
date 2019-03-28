using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Enumerations;

namespace BDC_V1.Interfaces
{
    public interface IFacility
    {
        EnumConstType ConstType              { get; set; }
        string   BuildingId                  { get; set; }
        string   BuildingName                { get; set; }
        string   BuildingUse                 { get; set; }
        int      YearBuilt                   { get; set; }
        string   AlternateId                 { get; set; }
        string   AlternateIdSource           { get; set; }
        decimal  TotalArea                   { get; set; }
        decimal  Width                       { get; set; }
        decimal  Depth                       { get; set; }
        decimal  Height                      { get; set; }
        int      NumFloors                   { get; set; }
        string   FacilityComments            { get; set; }
        IAddress Address                     { get; set; }
        IContact Contact                     { get; set; }
        IEnumerable<ImageSource> Images      { get; set; }
        IEnumerable<IInspector>  Inspections { get; set; }
    }
}
