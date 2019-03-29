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

namespace BDC_V1.Classes
{
    public class BredInfoContainer : IGlobalContainer<IBredInfo>
    {
        [CanBeNull] 
        public IBredInfo GlobalValue
        {
            get => _globalValue ?? _defaultValue;
            set
            {
                if (value == null) value = _defaultValue;

                if (_globalValue != value)
                {
                    _globalValue = value;

                    EventAggregator.GetEvent<PubSubEvent<GlobalDataEvent>>()
                        .Publish(new GlobalDataEvent(typeof(IBredInfo), nameof(GlobalValue)));
                }
            }
        }
        private IBredInfo _globalValue;
        private readonly IBredInfo _defaultValue;

        public BredInfoContainer([CanBeNull] IBredInfo defaultValue = null)
        {
            _defaultValue = defaultValue;
        }
    }
}
