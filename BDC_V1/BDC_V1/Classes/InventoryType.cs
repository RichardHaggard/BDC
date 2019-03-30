using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class InventoryType : BindableBase, IInventoryType
    {
        public string FacilityId
        {
            get => _facilityId;
            set => SetProperty(ref _facilityId, value);
        }

        private string _facilityId;

        public string SystemId
        {
            get => _systemId;
            set => SetProperty(ref _systemId, value);
        }

        private string _systemId;

        public string ComponentId
        {
            get => _componentId;
            set => SetProperty(ref _componentId, value);
        }

        private string _componentId;

        public string TypeName
        {
            get => _typeName;
            set => SetProperty(ref _typeName, value);
        }

        private string _typeName;

        public string SectionName
        {
            get => _sectionName;
            set => SetProperty(ref _sectionName, value);
        }

        private string _sectionName;

        public string InventIssue
        {
            get => _inventIssue;
            set => SetProperty(ref _inventIssue, value);
        }

        private string _inventIssue;
    }
}
