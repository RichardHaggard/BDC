﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class Contact : IContact
    {
        public IPerson Name { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
    }
}
