using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class BredInfo : PropertyBase, IBredInfo
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }
        private string _fileName;

        public bool HasFacilities => FacilityInfo.HasItems;
        public INotifyingCollection<IComponentFacility> FacilityInfo =>
            PropertyCollection<IComponentFacility>(ref _facilityInfo, nameof(HasFacilities));
        [CanBeNull] private INotifyingCollection<IComponentFacility> _facilityInfo;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //


        // **************** Class members *************************************************** //

    }
}
