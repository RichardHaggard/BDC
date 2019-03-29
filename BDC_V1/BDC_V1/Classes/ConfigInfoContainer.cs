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
    public class ConfigInfoContainer : IGlobalContainer<IConfigInfo>
    {
        [CanBeNull] 
        public IConfigInfo GlobalValue
        {
            get => _globalValue ?? _defaultValue;
            set
            {
                if (value == null) value = _defaultValue;

                if (_globalValue != value)
                {
                    _globalValue = value;

                    EventAggregator.GetEvent<PubSubEvent<GlobalDataEvent>>()
                        .Publish(new GlobalDataEvent(typeof(IConfigInfo), nameof(GlobalValue)));
                }
            }
        }
        private IConfigInfo _globalValue;
        private readonly IConfigInfo _defaultValue;

        public ConfigInfoContainer([CanBeNull] IConfigInfo defaultValue = null)
        {
            _defaultValue = defaultValue;
        }
    }
}
