﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class ConfigInfo : IConfigInfo
    {
        public string      FileName { get; set; }
        public IValidUsers ValidUsers { get; protected set; } = new ValidUsers();
    }
}
