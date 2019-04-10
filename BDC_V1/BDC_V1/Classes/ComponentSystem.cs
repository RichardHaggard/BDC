using BDC_V1.Enumerations;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class ComponentSystem : ComponentInventory, IComponentSystem
    {
        public override EnumComponentTypes ComponentType => EnumComponentTypes.SystemType;

    }
}
