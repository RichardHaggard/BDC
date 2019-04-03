using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Enumerations;

namespace BDC_V1.Interfaces
{
    public interface IFilterItem
    {
        string PropertyName     { get; set; }
        EnumFilterOps Operation { get; set; }
        object Value            { get; set ; }
    }
}
