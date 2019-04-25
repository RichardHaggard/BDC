using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class ComponentFacilityHeader : ComponentBase, IComponentFacilityHeader
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        public override EnumComponentTypes ComponentType => EnumComponentTypes.FacilityType;

        public override string ComponentName => $@"{BuildingIdNumber} - {BuildingName}";

        public string BuildingId
        {
            get => base.ComponentName;
            set => base.ComponentName = value;
        }

        public uint BuildingIdNumber
        {
            get => _buildingIdNumber;
            set => SetPropertyFlagged(ref _buildingIdNumber, value, nameof(ComponentName));
        }
        private uint _buildingIdNumber;

        public string BuildingName
        {
            get => _buildingName;
            set => SetPropertyFlagged(ref _buildingName, value, nameof(ComponentName));
        }
        private string _buildingName;

    }
}
