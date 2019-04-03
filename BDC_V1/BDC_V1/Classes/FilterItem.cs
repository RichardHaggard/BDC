﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class FilterItem : IFilterItem
    {
        public string PropertyName     { get; set; }
        public EnumFilterOps Operation { get; set; }
        public object Value            { get; set; }
    }
}
