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

        public string BuildingId
        {
            get => ComponentName;
            set => ComponentName = value;
        }

        public uint BuildingIdNumber
        {
            get => _buildingIdNumber;
            set => SetProperty(ref _buildingIdNumber, value);
        }
        private uint _buildingIdNumber;

        public string BuildingName
        {
            get => _buildingName;
            set => SetProperty(ref _buildingName, value);
        }
        private string _buildingName;

    }
}
