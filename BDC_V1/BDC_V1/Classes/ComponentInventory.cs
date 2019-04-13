using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        public override bool HasImages => Section.HasImages || Detail.HasImages;

        // Inspection Issues
        public override bool HasInspectionIssues => InspectionIssues.Any();
        public ObservableCollection<IssueInspection> InspectionIssues { get; } =
            new ObservableCollection<IssueInspection>();

        // Inventory Issues
        public override bool HasInventoryIssues => InventoryIssues.Any();
        public ObservableCollection<IssueInventory> InventoryIssues { get; } =
            new ObservableCollection<IssueInventory>();

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //

        public ComponentInventory()
        {
            InspectionIssues.CollectionChanged += (o, e) => 
                RaisePropertyChanged(new []
                {
                    nameof(HasInspectionIssues   ), 
                    nameof(HasAnyInspectionIssues)
                });

            InventoryIssues.CollectionChanged += (o, e) => 
                RaisePropertyChanged(new []
                {
                    nameof(HasInventoryIssues    ), 
                    nameof(HasAnyInventoryIssues )
                });

            Section.PropertyChanged += OnItemPropertyChanged;
            Detail .PropertyChanged += OnItemPropertyChanged;
        }

        private void OnItemPropertyChanged(object o, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HasImages)) 
                RaisePropertyChanged(e.PropertyName);
        }

        // **************** Class members *************************************************** //

    }
}
