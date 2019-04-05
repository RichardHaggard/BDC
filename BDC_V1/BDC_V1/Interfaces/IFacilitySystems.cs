using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Enumerations;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IFacilitySystems : ISystemElement
    {
        bool TryGet(EnumComponentTypes type, string name, out IFacilitySystems val);

        [CanBeNull]
        IFacilitySystems Get(EnumComponentTypes type, string name);

        [CanBeNull]
        IFacilitySystems Get(ISystemElement type);

        ObservableCollection<ISystemElement> SubSystems { get; }
    }
}
