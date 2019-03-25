using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using CommonServiceLocator;
using Prism.Events;

namespace BDC_V1.Services
{
    public class AppController : IAppController
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        private IEventAggregator EventAggregator
        {
            get
            {
                if (_EventAggregator == null)
                {
                    try
                    {
                        // ServiceLocator.Current may or may not be set. Wrap in a try/catch
                        // just in case.
                        _EventAggregator = ServiceLocator.Current.TryResolve<IEventAggregator>();
                    }
                    catch { }
                }
                return _EventAggregator;
            }
        }
        private IEventAggregator _EventAggregator = null;


        // **************** Class constructors ********************************************** //

        public AppController()
        {
            if (EventAggregator != null)
                EventAggregator.GetEvent<PubSubEvent<string>>().Subscribe(OnUserAction);
        }

        // **************** Class members *************************************************** //


        private void OnUserAction( string userAction )
        {
            // Normally, arguments from an external agency should be validated before being
            // utilized. This, however, is just proof of concept and need not have extensive
            // error handling built in.

            switch ( userAction )
            {
                case "Login clicked":
                    {
                        break;
                    }
            }
        }
    }
}
