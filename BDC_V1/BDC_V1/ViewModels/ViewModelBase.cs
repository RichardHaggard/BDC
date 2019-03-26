using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommonServiceLocator;
using JetBrains.Annotations;
using Prism.Events;
using Prism.Mvvm;

namespace BDC_V1.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //


        // **************** Class Events ************************************************ //

        [CanBeNull]
        protected IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                {
                    try
                    {
                        _eventAggregator = ServiceLocator.Current.TryResolve<IEventAggregator>();
                    }
                    catch { }
                }

                return _eventAggregator;
            }
        }

        [CanBeNull]
        private IEventAggregator _eventAggregator;

        // **************** Class properties ************************************************ //

        // **************** Class constructors ********************************************** //

        // **************** Class members *************************************************** //

    }
}
