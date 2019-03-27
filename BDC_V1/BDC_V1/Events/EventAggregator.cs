using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using JetBrains.Annotations;
using Prism.Events;

namespace BDC_V1.Events
{
    public static class EventAggregator
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
