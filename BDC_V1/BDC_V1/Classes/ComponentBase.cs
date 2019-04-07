using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class ComponentBase :  PropertyBase, IComponentBase
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

        public virtual EnumComponentTypes ComponentType
        {
            get => _componentType;
            set => SetProperty(ref _componentType, value);
        }
        private EnumComponentTypes _componentType;

        public string ComponentName
        {
            get => _componentName;
            set => SetProperty(ref _componentName, value);
        }
        private string _componentName;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _description;

        // Queries to bring derived class features up front
        // These queries won't instantiate storage if it is on-demand
        // checks and returns for the selected component only
        public virtual bool HasFacilityComments   => false;
        public virtual bool HasComments           => false;
        public virtual bool HasInspectionComments => false;
        public virtual bool HasImages             => false;
        public virtual bool HasInspections        => false;
        public virtual bool HasDetailComments     => false;
        public virtual bool HasQaIssues           => HasInspectionIssues || HasInventoryIssues;
        public virtual bool HasInspectionIssues   => false;
        public virtual bool HasInventoryIssues    => false;
        
        // These queries will check the selected component and all of it's children
        public virtual bool HasAnyQaIssues         => HasQaIssues         || Components.Any(item => item.HasAnyQaIssues);
        public virtual bool HasAnyInspectionIssues => HasInspectionIssues || Components.Any(item => item.HasAnyInspectionIssues);
        public virtual bool HasAnyInventoryIssues  => HasInventoryIssues  || Components.Any(item => item.HasAnyInventoryIssues);
        public virtual bool HasAnyInspections      => HasInspections      || Components.Any(item => item.HasAnyInspections);

        // Subsystems
        public bool HasComponents => Components.HasItems;
        public INotifyingCollection<IComponentBase> Components => 
            PropertyCollection<IComponentBase>(ref _components, nameof(HasComponents));
        [CanBeNull] private INotifyingCollection<IComponentBase> _components;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //


        // **************** Class members *************************************************** //
        // getters for the desired component children, these only work for unique keys
        // the find key is InventoryType+ComponentName by default,
        // Set the InventoryType = None to do a name-only search
        // should the keys not be unique, these will return the first found

        public IComponentBase GetComponent(EnumComponentTypes type, string name) =>
            TryGetComponent(type, name, out var val) ? val : null;

        public IComponentBase GetComponent(IComponentBase type) =>
            TryGetComponent(type, out var val) ? val : null;

        public bool TryGetComponent(IComponentBase type, out IComponentBase val) =>
            TryGetComponent(type.ComponentType, type.ComponentName, out val);

        // recursive search function
        public bool TryGetComponent(EnumComponentTypes compType, string compName, out IComponentBase val)
        {
            val = null;

            if (((ComponentType == compType) || (EnumComponentTypes.None == compType)) &&
                 (ComponentName == compName))
            {
                val = this;
                return true;
            }

            if (HasComponents)
            {
                foreach (var item in Components)
                {
                    if (item.TryGetComponent(compType, compName, out val))
                        return true;
                }
            }

            return false;
        }

        // on the off chance that the keys are not unique, these return all of the matches
        public IEnumerable<IComponentBase> GetAnyComponent(IComponentBase component) =>
            GetAnyComponent(component.ComponentType, component.ComponentName);

        public IEnumerable<IComponentBase> GetAnyComponent(EnumComponentTypes compType, string compName)
        {
            var itemList = new List<IComponentBase>();

            if (((ComponentType == compType) || (EnumComponentTypes.None == compType)) &&
                (ComponentName == compName))
            {
                itemList.Add(this);
            }

            if (HasComponents)
            {
                foreach (var item in Components)
                {
                    itemList.AddRange(item.GetAnyComponent(compType, compName));
                }
            }

            return itemList;
        }
    }
}
