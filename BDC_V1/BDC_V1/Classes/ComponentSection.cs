using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    class ComponentSection : ComponentInventory, IComponentSection
    {
        public override EnumComponentTypes ComponentType => EnumComponentTypes.SectionType;

    }
}
