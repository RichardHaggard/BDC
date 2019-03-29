using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using JetBrains.Annotations;
using Prism.Events;
using EventAggregator = BDC_V1.Events.EventAggregator;

namespace BDC_V1.Interfaces
{
    public interface IGlobalContainer<T>
    {
        [CanBeNull] 
        T GlobalValue { get; set; }
    }
}
