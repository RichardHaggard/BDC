using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using CommonServiceLocator;
using JetBrains.Annotations;
using Prism.Events;
using Prism.Regions;

namespace BDC_V1.Classes
{
    public abstract class ViewModelBase : PropertyBase, INotifyPropertyChanged
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class members *************************************************** //

        // **************** Class properties ************************************************ //

        [CanBeNull]
        protected virtual IConfigInfo LocalConfigInfo
        {
            get => _configInfo;
            set
            {
                if (_configInfo != value)
                {
                    Debug.Assert(value != null);
                    _configInfo = value;
                }
            }
        }
        private IConfigInfo _configInfo;

        [CanBeNull] 
        protected virtual IBredInfo LocalBredInfo
        {
            get => _bredInfo;
            set
            {
                if (_bredInfo != value)
                {
                    Debug.Assert(value != null);
                    _bredInfo = value;
                }
            }
        }
        private IBredInfo _bredInfo;

        [NotNull]
        public ICommand ViewActivated { get; }

        [CanBeNull]
        protected IRegionManager RegionManager
        {
            get => this._regionManager;
            set => SetProperty(ref _regionManager, value);
        } 
        private IRegionManager _regionManager;

        protected string RegionManagerName { get; set; }

        // **************** Class constructors ********************************************** //

        protected ViewModelBase()
        {
            // subscribe to updates of the global information
            // it get's updated when the config file is opened at LoginView or when a new file->open occurs
            Events.EventTypeAggregator.GetEvent<PubSubEvent<GlobalDataEvent>>()
                .Subscribe((item) =>
                {
                    if ((item.GlobalType == typeof(IConfigInfo)) &&
                        (item.GlobalName == "GlobalValue"))
                    {
                        LocalConfigInfo = GetConfigInfo();
                    }
                });

            EventTypeAggregator.GetEvent<PubSubEvent<GlobalDataEvent>>()
                .Subscribe((item) =>
                {
                    if ((item.GlobalType == typeof(IBredInfo)) &&
                        (item.GlobalName == "GlobalValue"))
                    {
                        LocalBredInfo = GetBredInfo();
                    }
                });

            ViewActivated = new RelayCommand(ViewActivatedEventHandler);         
        }

        // **************** Class members *************************************************** //

        // here is where we read in the global config info containing the list of valid users
        [NotNull]
        protected virtual IConfigInfo GetConfigInfo()
        {
            var container = ServiceLocator.Current.TryResolve<ConfigInfoContainer>();
            Debug.Assert(container?.GlobalValue != null);
            return container.GlobalValue;
        }

        // here is where we read in the global BRED info
        [NotNull]
        protected virtual IBredInfo GetBredInfo()
        {
            var container = ServiceLocator.Current.TryResolve<BredInfoContainer>();
            Debug.Assert(container?.GlobalValue != null);
            return container.GlobalValue;
        }

        protected virtual void ViewActivatedEventHandler(object sender, object e)
        {
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            GetRegionManager();
        }

        protected virtual bool GetRegionManager()
        {
            // if the manager name hasn't been set, return
            if (string.IsNullOrEmpty(RegionManagerName)) return false;

            // find the region manager for this ViewModel
            if ((RegionManager == null) ||
                !RegionManager.Regions.ContainsRegionWithName(RegionManagerName))
            {
                return false;
            }

            // always start with a clean slate
            var viewsCollections = RegionManager.Regions[RegionManagerName].ActiveViews;
            if (viewsCollections != null)
            {
                foreach (var region in viewsCollections)
                {
                    if (region is ItemsControl imageItems)
                    {
                        if (imageItems.ItemsSource is IList borderCollie)
                            borderCollie.Clear();

                        imageItems.ItemsSource = null;
                    }

                    RegionManager.Regions[RegionManagerName].Remove(region);
                }
            }

            return true;
        }
    }
}
