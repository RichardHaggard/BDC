using System.Diagnostics;
using System.Linq;
using BDC_V1.Classes;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using CommonServiceLocator;
using JetBrains.Annotations;
using Prism.Events;
using Prism.Mvvm;
using EventAggregator = BDC_V1.Events.EventAggregator;

namespace BDC_V1.ViewModels
{
    public class ViewModelBase : BindableBase
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
        [CanBeNull] private IConfigInfo _configInfo;

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
        [CanBeNull] private IBredInfo _bredInfo;

        // **************** Class constructors ********************************************** //

        public ViewModelBase()
        {
            // subscribe to updates of the global information
            // it get's updated when the config file is opened at LoginView or when a new file->open occurs
            Events.EventAggregator.GetEvent<PubSubEvent<GlobalDataEvent>>()
                .Subscribe((item) =>
                {
                    if ((item.GlobalType == typeof(IConfigInfo)) &&
                        (item.GlobalName == "GlobalValue"))
                    {
                        LocalConfigInfo = GetConfigInfo();
                    }
                });

            EventAggregator.GetEvent<PubSubEvent<GlobalDataEvent>>()
                .Subscribe((item) =>
                {
                    if ((item.GlobalType == typeof(IBredInfo)) &&
                        (item.GlobalName == "GlobalValue"))
                    {
                        LocalBredInfo = GetBredInfo();
                    }
                });
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

    }
}
