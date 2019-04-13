using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class ComponentInventory : ComponentBase, IComponentInventory
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

        public override EnumComponentTypes ComponentType => EnumComponentTypes.InventoryType;

        public IInventorySection Section { get; } = new InventorySection();
        public IInventoryDetail  Detail  { get; } = new InventoryDetail ();

        // Images
        public override bool HasImages => (base.HasImages = Section.HasImages || Detail.HasImages);

        // Inspection Issues
        public override bool HasInspectionIssues => (base.HasInspectionIssues = InspectionIssues.HasItems);
        public INotifyingCollection<IIssueInspection> InspectionIssues => 
            PropertyCollection<IIssueInspection>(ref _inspectionIssues, nameof(HasInspectionIssues));
        [CanBeNull] private INotifyingCollection<IIssueInspection> _inspectionIssues;

        // Inventory Issues
        public override bool HasInventoryIssues => (base.HasInventoryIssues = InventoryIssues.HasItems);
        public INotifyingCollection<IIssueInventory> InventoryIssues => 
            PropertyCollection<IIssueInventory>(ref _inventoryIssues, nameof(HasInventoryIssues));
        [CanBeNull] private INotifyingCollection<IIssueInventory> _inventoryIssues;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //


        // **************** Class members *************************************************** //

    }
}
