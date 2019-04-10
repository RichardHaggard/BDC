using BDC_V1.Events;
using BDC_V1.Interfaces;
using JetBrains.Annotations;
using Prism.Events;

namespace BDC_V1.Classes
{
    public class BredInfoContainer : IGlobalContainer<IBredInfo>
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

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

                    EventTypeAggregator.GetEvent<PubSubEvent<GlobalDataEvent>>()
                        .Publish(new GlobalDataEvent(typeof(IBredInfo), nameof(GlobalValue)));
                }
            }
        }
        private IBredInfo _globalValue;
        private readonly IBredInfo _defaultValue;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //

        public BredInfoContainer([CanBeNull] IBredInfo defaultValue = null)
        {
            _defaultValue = defaultValue;
        }

        // **************** Class members *************************************************** //

    }
}
