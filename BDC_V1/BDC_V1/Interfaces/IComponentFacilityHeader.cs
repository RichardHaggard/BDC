using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    /// <inheritdoc cref="IComponentBase"/>
    public interface IComponentFacilityHeader : IComponentBase, INotifyPropertyChanged
    {
        string BuildingId       { get; set; }
        uint   BuildingIdNumber { get; set; }
        string BuildingName     { get; set; }
    }
}
