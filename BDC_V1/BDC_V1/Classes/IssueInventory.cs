using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class IssueInventory : PropertyBase, IIssueInventory
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

        public bool HasInventoryComments => InventoryComments.HasItems;
        public INotifyingCollection<ICommentInventory> InventoryComments => 
            PropertyCollection<ICommentInventory>(ref _inventoryComments, nameof(HasInventoryComments));
        [CanBeNull] private INotifyingCollection<ICommentInventory> _inventoryComments;
    }
}
