using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IBredInfo
    {
        string FileName { get; set; }

        bool? HasFacilities { get; }

        [NotNull]
        IFacilitySystems FacilityInfo { get; }
    }
}
