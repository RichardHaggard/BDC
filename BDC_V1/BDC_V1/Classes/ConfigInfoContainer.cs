using BDC_V1.Events;
using BDC_V1.Interfaces;
using JetBrains.Annotations;
using Prism.Events;

namespace BDC_V1.Classes
{
    public class ConfigInfoContainer : IGlobalContainer<IConfigInfo>
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

        public IConfigInfo GlobalValue
        {
            get => _globalValue ?? _defaultValue;
            set
            {
                if (value == null) value = _defaultValue;

                if (_globalValue != value)
                {
                    _globalValue = value;

                    EventTypeAggregator.GetEvent<PubSubEvent<GlobalDataEvent>>()
                        .Publish(new GlobalDataEvent(typeof(IConfigInfo), nameof(GlobalValue)));
                }
            }
        }
        private IConfigInfo _globalValue;
        private readonly IConfigInfo _defaultValue;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //

        public ConfigInfoContainer([CanBeNull] IConfigInfo defaultValue = null)
        {
            _defaultValue = defaultValue;
        }

        // **************** Class members *************************************************** //

    }
}
