﻿using Prism.Mvvm;

namespace BDC_V1.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //


        // **************** Class members *************************************************** //

        [CanBeNull]
        private IEventAggregator _eventAggregator;

        // **************** Class properties ************************************************ //

        // **************** Class constructors ********************************************** //

        // **************** Class members *************************************************** //

    }
}
