using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            set => SetPropertyFlagged(ref _isActive, value,
                nameof(AreAnyActive));
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
        public virtual bool HasInspectionIssues
        {
            get => _hasInspectionIssues;
            set => SetPropertyFlagged(ref _hasInspectionIssues, value, new [] 
            {
                nameof(HasQaIssues), 
                nameof(HasAnyQaIssues), 
                nameof(HasAnyInspectionIssues)
            });
        }
        private bool _hasInspectionIssues;

        /// <inheritdoc />
        public virtual bool HasInventoryIssues
        {
            get => _hasInventoryIssues;
            set => SetPropertyFlagged(ref _hasInventoryIssues, value, new [] 
            {
                nameof(HasQaIssues), 
                nameof(HasAnyQaIssues), 
                nameof(HasAnyInventoryIssues)
            });
        }
        private bool _hasInventoryIssues;

        /// <inheritdoc />
        public virtual bool HasInspections
        {
            get => _hasInspections;
            set => SetPropertyFlagged(ref _hasInspections, value, 
                nameof(HasAnyInspections));
        }
        private bool _hasInspections;

        // *******************************************************
        // Derived properties
        // *******************************************************

        // I don't see these being overridden
        public bool HasAnyQaIssues         => HasQaIssues         | Children.Any(item => item.HasAnyQaIssues        );       
        public bool HasAnyInspectionIssues => HasInspectionIssues | Children.Any(item => item.HasAnyInspectionIssues); 
        public bool HasAnyInventoryIssues  => HasInventoryIssues  | Children.Any(item => item.HasAnyInventoryIssues ); 
        public bool HasAnyInspections      => HasInspections      | Children.Any(item => item.HasAnyInspections     );

        // this one could possibly need to be overridden
        public virtual bool AreAnyActive => IsActive | Children.Any(item => item.AreAnyActive);

        // these should be overridden in any class that implements the specified collection
        public virtual bool HasComments           => false;
        public virtual bool HasFacilityComments   => false;
        public virtual bool HasInspectionComments => false;
        public virtual bool HasDetailComments     => false;
        public virtual bool HasImages             => false;

        // should not be overridden
        public bool HasChildren => Children.Any();

        // *******************************************************
        // Component children
        // *******************************************************

        // TODO: must block direct additions!!!
        /// <inheritdoc />
        public ReadOnlyObservableCollection<ComponentBase> Children { get; } 

        // ReSharper disable once InconsistentNaming
        private ObservableCollection<ComponentBase> _children { get; }

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //

        public ComponentBase()
        {
            _children = new ObservableCollection<ComponentBase>();
            _children.CollectionChanged += (o, e) => RaisePropertyChanged(new[]
            {
                nameof(HasChildren           ),
                nameof(HasAnyQaIssues        ),
                nameof(HasAnyInspectionIssues),
                nameof(HasAnyInventoryIssues ),
                nameof(HasAnyInspections     ),
                nameof(AreAnyActive          )
            });

            Children = new ReadOnlyObservableCollection<ComponentBase>(_children);
        }

        // **************** Class members *************************************************** //

        private void OnChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(! (sender is IComponentBase child)) 
                throw new InvalidCastException("object is not a child");

            switch (e.PropertyName)
            {
                case nameof(HasAnyQaIssues        ):         
                case nameof(HasAnyInspectionIssues): 
                case nameof(HasAnyInventoryIssues ):  
                case nameof(HasAnyInspections     ):
                case nameof(AreAnyActive          ):
                    RaisePropertyChanged(e.PropertyName);
                    break;

                //case nameof(IsActive) :
                //    RaisePropertyChanged(nameof(AreAnyActive));
                //    break;
            }

            // does this kludge get things to update in the tree view?
            // nope!
            //RaisePropertyChanged(string.Empty);
        }

        // ******************************************************************* 
        // Add children access, the only proper way
        //
        // Methods to add children ??? TODO: must block direct additions!!!
        // ******************************************************************* 

        /// <inheritdoc />
        public virtual void AddChild(ComponentBase child)
        {
            child.PropertyChanged += OnChildPropertyChanged;
            _children.Add(child);
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

            if (HasChildren)
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

            if (HasChildren)
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
