using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class BredInfo : IBredInfo
    {
        public string    FileName { get; set; }
        public IFacility FacilityInfo { get; protected set; } = new Facility();
    }
}
