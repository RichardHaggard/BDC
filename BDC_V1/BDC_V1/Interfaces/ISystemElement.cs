using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Enumerations;

namespace BDC_V1.Interfaces
{
    public interface ISystemElement
    {
        EnumComponentTypes ComponentType { get; }
        string             ComponentName { get; }

        bool? HasComments      { get; }
        bool? HasImages        { get; }
        bool? HasInspections   { get; }
        bool? HasDetails       { get; }
        bool? HasQaIssues      { get; }
        bool? HasSubsystems    { get; }
        bool  HasAnyComponents { get; }
    }

}
