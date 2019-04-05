using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class SystemElement :  BindableBase, ISystemElement
    {
        public EnumComponentTypes ComponentType
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

        public virtual bool? HasComments      => false;
        public virtual bool? HasImages        => false;
        public virtual bool? HasInspections   => false;
        public virtual bool? HasDetails       => false;
        public virtual bool? HasQaIssues      => false;
        public virtual bool? HasSubsystems    => false;
        public virtual bool  HasAnyComponents => false;
    }
}
