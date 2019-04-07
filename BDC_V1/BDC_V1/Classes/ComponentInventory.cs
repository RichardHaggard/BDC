using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class ComponentInventory : ComponentBase, IComponentInventory
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

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
                nameof(HasAnyInspectionIssues)
            });
        [CanBeNull] private INotifyingCollection<IIssueInspection> _inspectionIssues;

        // Inventory Issues
        public override bool HasInventoryIssues => InventoryIssues.HasItems;
        public INotifyingCollection<IIssueInventory> InventoryIssues => 
            PropertyCollection<IIssueInventory>(ref _inventoryIssues, new []
            {
                nameof(HasInventoryIssues),
                nameof(HasAnyInventoryIssues)
            });
        [CanBeNull] private INotifyingCollection<IIssueInventory> _inventoryIssues;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //


        // **************** Class members *************************************************** //

    }
}
