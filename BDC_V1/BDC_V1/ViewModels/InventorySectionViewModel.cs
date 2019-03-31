using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.ViewModels
{
    public class InventorySectionViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull]
        public IInventorySectionType InventorySection { get; }

        [CanBeNull] 
        protected IFacility LocalFacilityInfo
        {
            get => _localFacilityInfo;
            private set
            {
                if (SetProperty(ref _localFacilityInfo, value))
                {
                    InventorySection.Images.Clear();
                    InventorySection.Images.AddRange(_localFacilityInfo?.Images);

                    // QuickObservableCollection should raise it's own notify
                    //RaisePropertyChanged(nameof(InventorySection));
                }
            }
        }
        [CanBeNull] private IFacility _localFacilityInfo;

        //protected override IConfigInfo LocalConfigInfo
        //{
        //    get => base.LocalConfigInfo;
        //    set
        //    {
        //        base.LocalConfigInfo = value;
        //    }
        //}

        protected override IBredInfo LocalBredInfo
        {
            get => base.LocalBredInfo;
            set
            {
                base.LocalBredInfo = value;
                LocalFacilityInfo = base.LocalBredInfo?.FacilityInfo;
            }
        }

        // **************** Class constructors ********************************************** //

        public InventorySectionViewModel()
        {
            InventorySection = new MockInventorySection();
        }


        // **************** Class members *************************************************** //

    }
}
