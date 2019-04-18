using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class FacilityInfoHeader  : PropertyBase, IFacilityInfoHeader
    {
        /// <inheritdoc />
        public uint BuildingIdNumber
        {
            get => _buildingIdNumber;
            set => SetProperty(ref _buildingIdNumber, value);
        }
        private uint _buildingIdNumber;

        /// <inheritdoc />
        public string BuildingName
        {
            get => _buildingName;
            set => SetProperty(ref _buildingName, value);
        }
        private string _buildingName;
    }
}
