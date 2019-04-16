using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class FacilityBase : PropertyBase, IFacilityBase
    {
        public virtual IComponentFacility LocalFacilityInfo => 
            ((FacilityIndex >= 0) && (FacilityIndex < Facilities?.Count)) 
                ? Facilities?[FacilityIndex] 
                : null;

        // ??? KLUDGE ???
        public virtual int FacilityIndex
        {
            get => Math.Max(Math.Min(_facilityIndex, Facilities?.Count ?? -1), -1);
            set => SetPropertyFlagged(ref _facilityIndex, Math.Max(-1, value), nameof(LocalFacilityInfo));
        }
        private int _facilityIndex = -1;

        public IList<IComponentFacility> Facilities
        {
            get => _facilities;
            set => SetPropertyFlagged(ref _facilities, value, new []
            {
                nameof(FacilityIndex),
                nameof(LocalFacilityInfo)
            });
        }
        private IList<IComponentFacility> _facilities;
    }
}
