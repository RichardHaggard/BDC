﻿using BDC_V1.Enumerations;
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
        public override bool HasImages => Section.HasImages || Detail.HasImages;

        // Inspection Issues
        public override bool HasInspectionIssues => InspectionIssues.HasItems;
        public INotifyingCollection<IIssueInspection> InspectionIssues => 
            PropertyCollection<IIssueInspection>(ref _inspectionIssues, new []
            {
                nameof(HasInspectionIssues),
                nameof(HasAnyInspectionIssues),
                nameof(HasQaIssues),
                nameof(HasAnyQaIssues)
            });
        [CanBeNull] private INotifyingCollection<IIssueInspection> _inspectionIssues;

        // Inventory Issues
        public override bool HasInventoryIssues => InventoryIssues.HasItems;
        public INotifyingCollection<IIssueInventory> InventoryIssues => 
            PropertyCollection<IIssueInventory>(ref _inventoryIssues, new []
            {
                nameof(HasInventoryIssues),
                nameof(HasAnyInventoryIssues),
                nameof(HasQaIssues),
                nameof(HasAnyQaIssues)
            });
        [CanBeNull] private INotifyingCollection<IIssueInventory> _inventoryIssues;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //


        // **************** Class members *************************************************** //

    }
}
