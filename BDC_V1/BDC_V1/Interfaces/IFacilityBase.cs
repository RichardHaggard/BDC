using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IFacilityBase
    {
        [CanBeNull] 
        IComponentFacility LocalFacilityInfo { get; }

        [CanBeNull] 
        IList<IComponentFacility> Facilities { get; }

        int FacilityIndex { get; }
    }
}
