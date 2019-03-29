using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class Inspector : IInspector
    {
        public IPerson  InspectorName  { get; set; }
        public DateTime InspectionDate { get; set; }
    }
}
