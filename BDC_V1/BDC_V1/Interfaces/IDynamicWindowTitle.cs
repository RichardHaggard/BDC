﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IDynamicReadOnlyWindowTitle
    {
        string WindowTitle { get; }
    }

    public interface IDynamicWindowTitle
    {
        string WindowTitle { get; set; }
    }
}
