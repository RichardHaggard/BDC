using CommonServiceLocator;
using Prism.Events;

namespace BDC_V1.Events
{
    public static class EventTypeAggregator
    {
        private static IEventAggregator _eventAggregator;

        public static TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            if (_eventAggregator == null)
            {
                try
                {
                    _eventAggregator = ServiceLocator.Current.TryResolve<IEventAggregator>();
                }
                catch
                {
                    _eventAggregator = new Prism.Events.EventAggregator();
                }
            }

            return _eventAggregator.GetEvent<TEventType>();
        }
    }
}
