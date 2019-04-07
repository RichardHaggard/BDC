using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IComponentBase : INotifyPropertyChanged
    {
        // hopefully unique key for all components
        EnumComponentTypes ComponentType { get; }
        string             ComponentName { get; }
        string             Description   { get; }

        // Queries to bring derived class features up front
        // These queries won't instantiate storage if it is on-demand
        // checks and returns for the selected component only
        bool HasFacilityComments    { get; }
        bool HasComments            { get; }
        bool HasInspectionComments  { get; }
        bool HasImages              { get; }
        bool HasInspections         { get; }
        bool HasDetailComments      { get; }
        bool HasQaIssues            { get; }
        bool HasInspectionIssues    { get; }
        bool HasInventoryIssues     { get; }
        bool HasComponents          { get; }

        // These queries will check the selected component and all of it's children
        bool HasAnyQaIssues         { get; }
        bool HasAnyInspectionIssues { get; }
        bool HasAnyInventoryIssues  { get; }
        bool HasAnyInspections      { get; }

        // child components
        [NotNull] INotifyingCollection<IComponentBase> Components { get; }

        // getters for the desired component children, these only work for unique keys
        // the find key is InventoryType+ComponentName by default,
        // Set the InventoryType = None to do a name-only search
        // should the keys not be unique, these will return the first found
        bool TryGetComponent(IComponentBase    component, out IComponentBase val);
        bool TryGetComponent(EnumComponentTypes compType, string compName, out IComponentBase val);

        [CanBeNull] IComponentBase GetComponent(EnumComponentTypes compType, string compName);
        [CanBeNull] IComponentBase GetComponent(IComponentBase component);

        // on the off chance that the keys are not unique, these return all of the matches
        [NotNull] IEnumerable<IComponentBase> GetAnyComponent(IComponentBase component);
        [NotNull] IEnumerable<IComponentBase> GetAnyComponent(EnumComponentTypes compType, string compName);
    }
}
