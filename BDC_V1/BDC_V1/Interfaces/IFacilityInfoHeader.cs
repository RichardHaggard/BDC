using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IFacilityInfoHeader : INotifyPropertyChanged
    {
        uint   BuildingIdNumber { get; set; }
        string BuildingName     { get; set; }
    }
}
