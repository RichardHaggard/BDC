using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class BredInfo : BindableBase, IBredInfo
    {
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }
        private string _fileName;

        public bool? HasFacilities => _facilityInfo?.HasSubsystems;

        public IFacilitySystems FacilityInfo =>
            _facilityInfo ?? (_facilityInfo = new FacilitySystems()
            {
                ComponentName = "Top Level",
                ComponentType = EnumComponentTypes.None
            });

       private FacilitySystems _facilityInfo;
    }
}
