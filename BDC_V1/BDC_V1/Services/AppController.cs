﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using CommonServiceLocator;
using JetBrains.Annotations;
using Prism.Events;

namespace BDC_V1.Services
{
    public class AppController : IAppController
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        [CanBeNull]
        private IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                {
                    try
                    {
                        // ServiceLocator.Current may or may not be set. Wrap in a try/catch
                        // just in case.
                        _eventAggregator = ServiceLocator.Current.TryResolve<IEventAggregator>();
                    }
                    catch { }
                }
                return _eventAggregator;
            }
        }

        [CanBeNull]
        private IEventAggregator _eventAggregator;

        // **************** Class constructors ********************************************** //

        public AppController()
        {
            EventAggregator?.GetEvent<PubSubEvent<string>>().Subscribe(OnUserAction);
        }

        // **************** Class members *************************************************** //


        private static void OnUserAction( string userAction )
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
