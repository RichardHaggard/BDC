﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Enumerations
{
    public enum EnumControlResult
    {
        ResultDeleteItem = -2,
        ResultCancelled = -1,
        ResultDeferred,
        ResultSaveNow
    }
}
