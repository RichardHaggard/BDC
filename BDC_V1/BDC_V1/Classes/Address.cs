using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class Address : IAddress
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City    { get; set; }
        public string State   { get; set; }
        public string Zipcode { get; set; }
    }
}
