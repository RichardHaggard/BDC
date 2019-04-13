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

        /// <summary>
        /// The component type
        /// </summary>
        EnumComponentTypes ComponentType { get; }

        /// <summary>
        /// The display name of this component
        /// </summary>
        string ComponentName { get; }

        /// <summary>
        /// User-centric description of this component
        /// </summary>
        string  Description { get; }

        /// <summary>
        /// State used by the filter to control this display of this item
        /// on the component tree
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Are any of the children active
        /// </summary>
        bool AreAnyActive { get; }

        // Queries to bring derived class features up front
        // These queries won't instantiate storage if it is on-demand
        // checks and returns for the selected component only

        /// <summary>
        /// Returns the state of Qa Issues for just this component
        /// </summary>
        bool HasQaIssues { get; }

        /// <summary>
        /// Returns the state of Qa Issues for this component and it's children
        /// </summary>
        bool HasAnyQaIssues { get; }

        /// <summary>
        /// Returns the state of Inspection Issues for just this component
        /// </summary>
        bool HasInspectionIssues { get; }

        /// <summary>
        /// Returns the state of Inspection Issues for this component and it's children
        /// </summary>
        bool HasAnyInspectionIssues { get; }

        /// <summary>
        /// Returns the state of Inventory Issues for just this component
        /// </summary>
        bool HasInventoryIssues { get; }

        /// <summary>
        /// Returns the state of Inventory Issues for this component and it's children
        /// </summary>
        bool HasAnyInventoryIssues { get; }

        /// <summary>
        /// Returns the state of Inspections for just this component
        /// </summary>
        bool HasInspections { get; }

        /// <summary>
        /// Returns the state of Inspections for this component and it's children
        /// </summary>
        bool HasAnyInspections { get; }

        /// <summary>
        /// Returns the state of Comments for just this component
        /// </summary>
        bool HasComments { get; }

        /// <summary>
        /// Returns the state of Facility Comments for just this component
        /// </summary>
        bool HasFacilityComments { get; }

        /// <summary>
        /// Returns the state of Inspection Comments for just this component
        /// </summary>
        bool HasInspectionComments { get; }

        /// <summary>
        /// Returns the state of Detail Comments for just this component
        /// </summary>
        bool HasDetailComments { get; }

        /// <summary>
        /// Returns the state of Images for just this component
        /// </summary>
        bool HasImages { get; }

        /// <summary>
        /// Returns true if there are any children
        /// </summary>
        bool HasChildren { get; }

        /// <summary>
        /// Component children of this one, Blocks external collection changes
        /// </summary>
        [NotNull] ReadOnlyObservableCollection<ComponentBase> Children { get; }

        // getters for the desired component children, these only work for unique keys
        // the find key is InventoryType+ComponentName by default,
        // Set the InventoryType = None to do a name-only search
        // should the keys not be unique, these will return the first found

        /// <summary>
        /// Tries to find and return the child specified by <paramref name="component"/>
        /// </summary>
        /// <remarks>
        /// Just the <paramref name="component"/> ComponentType and ComponentName are used as search parameters
        /// </remarks>
        /// <param name="component">Description of the child to find</param>
        /// <param name="val">Reference to the found component, null if not found</param>
        /// <returns>true - the <paramref name="component"/> was found and <paramref name="val"/> is it</returns>
        bool TryGetComponent(IComponentBase component, out IComponentBase val);

        /// <summary>
        ///     Tries to find and return the child specified by
        ///     <paramref name="compType"/> & <paramref name="compName"/>
        /// </summary>
        /// <param name="compType">Component type of the child to find</param>
        /// <param name="compName">Component name of the child to find</param>
        /// <param name="val">Reference to the found component, null if not found</param>
        /// <returns>
        ///     true - the <paramref name="compType"/> & <paramref name="compName"/> was found
        ///     and <paramref name="val"/> is it
        /// </returns>
        bool TryGetComponent(EnumComponentTypes compType, string compName, out IComponentBase val);

        /// <summary>
        /// Tries to find and return the child specified by <paramref name="component"/>
        /// </summary>
        /// <remarks>
        /// Just the <paramref name="component"/> ComponentType and ComponentName are used as search parameters
        /// </remarks>
        /// <param name="component">Description of the child to find</param>
        /// <returns> != null => the <paramref name="component"/> was found and this is it</returns>
        [CanBeNull] IComponentBase GetComponent(IComponentBase component);

        /// <summary>
        ///     Returns the child specified by
        ///     <paramref name="compType"/> & <paramref name="compName"/>
        /// </summary>
        /// <param name="compType">Component type of the child to find</param>
        /// <param name="compName">Component name of the child to find</param>
        /// <returns> != null => the <paramref name="compType"/> & <paramref name="compName"/> was found</returns>
        [CanBeNull] IComponentBase GetComponent(EnumComponentTypes compType, string compName);

        /// <summary>
        /// Tries to find and return the children specified by <paramref name="component"/>
        /// </summary>
        /// <remarks>
        /// Just the <paramref name="component"/> ComponentType and ComponentName are used as search parameters
        /// </remarks>
        /// <param name="component">Description of the child to find</param>
        /// <returns> list of children found matching <paramref name="component"/></returns>
        [NotNull] IEnumerable<IComponentBase> GetAnyComponent(IComponentBase component);

        /// <summary>
        ///     Returns the children specified by
        ///     <paramref name="compType"/> & <paramref name="compName"/>
        /// </summary>
        /// <param name="compType">Component type of the child to find</param>
        /// <param name="compName">Component name of the child to find</param>
        /// <returns> list of children found matching <paramref name="compType"/> & <paramref name="compName"/></returns>
        [NotNull] IEnumerable<IComponentBase> GetAnyComponent(EnumComponentTypes compType, string compName);

        /// <summary>
        /// Add a child, the only proper way
        /// </summary>
        /// <param name="child"></param>
        void AddChild(ComponentBase child);

        /// <summary>
        /// Add children, the only proper way
        /// </summary>
        /// <param name="children"></param>
        void AddChildren(IEnumerable<ComponentBase> children);
    }
}
