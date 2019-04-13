using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class ComponentBase : PropertyBase, IComponentBase
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

        /// <inheritdoc />
        public virtual EnumComponentTypes ComponentType
        {
            get => _componentType;
            set => SetProperty(ref _componentType, value);
        }
        private EnumComponentTypes _componentType;

        /// <inheritdoc />
        public string ComponentName
        {
            get => _componentName;
            set => SetProperty(ref _componentName, value);
        }
        private string _componentName;

        /// <inheritdoc />
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _description;

        /// <inheritdoc />
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }
        private bool _isActive = true;

        // *******************************************************
        // Queries to bring derived class features up front
        // These queries won't instantiate storage if it is on-demand
        // *******************************************************

        // *******************************************************
        // These queries are matched pairs on just this component and
        // this PLUS all it's children
        // *******************************************************

        // *******************************************************
        /// <inheritdoc />
        public bool HasQaIssues => HasInspectionIssues || HasInventoryIssues;

        /// <inheritdoc />
        public bool HasAnyQaIssues
        {
            get => _hasAnyQaIssues |= HasQaIssues;
            set => SetProperty(ref _hasAnyQaIssues, value);
        }
        private bool _hasAnyQaIssues;
        // *******************************************************

        // *******************************************************
        /// <inheritdoc />
        public virtual bool HasInspectionIssues
        {
            get => _hasInspectionIssues;
            set => SetProperty(ref _hasInspectionIssues, value, () =>
            {
                HasAnyInspectionIssues |= _hasInspectionIssues;
                RaisePropertyChanged(new [] {nameof(HasQaIssues), nameof(HasAnyQaIssues)});
            });
        }
        private bool _hasInspectionIssues;

        /// <inheritdoc />
        public bool HasAnyInspectionIssues
        {
            get => _hasAnyInspectionIssues;
            set => SetProperty(ref _hasAnyInspectionIssues, value);
        }
        private bool _hasAnyInspectionIssues;
        // *******************************************************

        // *******************************************************
        /// <inheritdoc />
        public virtual bool HasInventoryIssues
        {
            get => _hasInventoryIssues;
            set => SetProperty(ref _hasInventoryIssues, value, () =>
            {
                HasAnyInventoryIssues |= _hasInventoryIssues;
                RaisePropertyChanged(new [] {nameof(HasQaIssues), nameof(HasAnyQaIssues)});
            });
        }
        private bool _hasInventoryIssues;

        /// <inheritdoc />
        public bool HasAnyInventoryIssues
        {
            get => _hasAnyInventoryIssues;
            set => SetProperty(ref _hasAnyInventoryIssues, value);
        }
        private bool _hasAnyInventoryIssues;
        // *******************************************************

        // *******************************************************
        /// <inheritdoc />
        public virtual bool HasInspections
        {
            get => _hasInspections;
            set => SetProperty(ref _hasInspections, value, () =>
            {
                HasAnyInspections |= _hasInspections;
            });
        }
        private bool _hasInspections;

        /// <inheritdoc />
        public bool HasAnyInspections
        {
            get => _hasAnyInspections;
            set => SetProperty(ref _hasAnyInspections, value);
        }
        private bool _hasAnyInspections;
        // *******************************************************

        // *******************************************************
        // the rest of these are just for this component
        // *******************************************************

        /// <inheritdoc />
        public virtual bool HasComments
        {
            get => _hasComments;
            set => SetProperty(ref _hasComments, value);
        }
        private bool _hasComments;

        /// <inheritdoc />
        public virtual bool HasFacilityComments
        {
            get => _hasFacilityComments;
            set => SetProperty(ref _hasFacilityComments, value);
        }
        private bool _hasFacilityComments;

        /// <inheritdoc />
        public virtual bool HasInspectionComments
        {
            get => _hasInspectionComments;
            set => SetProperty(ref _hasInspectionComments, value);
        }
        private bool _hasInspectionComments;

        /// <inheritdoc />
        public virtual bool HasDetailComments
        {
            get => _hasDetailComments;
            set => SetProperty(ref _hasDetailComments, value);
        }
        private bool _hasDetailComments;

        /// <inheritdoc />
        public virtual bool HasImages
        {
            get => _hasImages;
            set => SetProperty(ref _hasImages, value);
        }
        private bool _hasImages;

        /// <inheritdoc />
        public bool HasComponents => Children.Any();

        // *******************************************************
        // Component children
        // *******************************************************

        // Methods to add children ??? TODO: must block direct additions!!!
        /// <inheritdoc />
        public ObservableCollection<ComponentBase> Children { get; }
        
        //PropertyCollection<IComponentBase>(ref _components, nameof(HasComponents));
        //[CanBeNull] private INotifyingCollection<IComponentBase> _components;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //

        public ComponentBase()
        {
            Children = new ObservableCollection<ComponentBase>();
            Children.CollectionChanged += (o, i) => { RaisePropertyChanged(nameof(HasComponents)); };
        }

        // **************** Class members *************************************************** //
        // Methods to add children ??? TODO: must block direct additions!!!

        private void OnChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(! (sender is IComponentBase child)) throw new InvalidCastException("object is not a child");

            switch (e.PropertyName)
            {
                case nameof(HasAnyQaIssues):         HasAnyQaIssues         = child.HasAnyQaIssues         | HasQaIssues        ; break;
                case nameof(HasAnyInspectionIssues): HasAnyInspectionIssues = child.HasAnyInspectionIssues | HasInspectionIssues; break;
                case nameof(HasAnyInventoryIssues):  HasAnyInventoryIssues  = child.HasAnyInventoryIssues  | HasInventoryIssues ; break;
                case nameof(HasAnyInspections):      HasAnyInspections      = child.HasAnyInspections      | HasInspections     ; break;
            }

            // does this kludge get things to update in the tree view?
            // nope!
            //RaisePropertyChanged(string.Empty);
        }

        // ******************************************************************* 
        // Add children access, the only proper way
        // ******************************************************************* 

        /// <inheritdoc />
        public virtual void AddChild(ComponentBase child)
        {
            child.PropertyChanged += OnChildPropertyChanged;
            Children.Add(child);
        }

        /// <inheritdoc />
        public virtual void AddChildren(IEnumerable<ComponentBase> children)
        {
            foreach (var child in children) AddChild(child);
        }

        // ******************************************************************* 
        // getters for the desired component children, these only work for unique keys
        // the find key is InventoryType+ComponentName by default,
        // Set the InventoryType = None to do a name-only search
        // should the keys not be unique, these will return the first found
        // ******************************************************************* 

        /// <inheritdoc />
        public IComponentBase GetComponent(EnumComponentTypes type, string name) =>
            TryGetComponent(type, name, out var val) ? val : null;

        /// <inheritdoc />
        public IComponentBase GetComponent(IComponentBase type) =>
            TryGetComponent(type.ComponentType, type.ComponentName, out var val) ? val : null;

        /// <inheritdoc />
        public bool TryGetComponent(IComponentBase type, out IComponentBase val) =>
            TryGetComponent(type.ComponentType, type.ComponentName, out val);

        /// <inheritdoc />
        /// <remarks>
        /// Recursive function
        /// </remarks>
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
                foreach (var item in Children)
                {
                    if (item.TryGetComponent(compType, compName, out val))
                        return true;
                }
            }

            return false;
        }

        // on the off chance that the keys are not unique, these return all of the matches
        /// <inheritdoc />
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
                foreach (var item in Children)
                {
                    itemList.AddRange(item.GetAnyComponent(compType, compName));
                }
            }

            return itemList;
        }
    }
}
