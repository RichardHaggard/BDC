﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IBredInfo : INotifyPropertyChanged
    {
        string FileName { get; set; }

        bool HasFacilities { get; }

        [NotNull]
        INotifyingCollection<IComponentFacility> FacilityInfo { get; }
    }
}
