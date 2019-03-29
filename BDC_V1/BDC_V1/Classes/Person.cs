using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Classes
{
    public class Person : IPerson
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }

        public override string ToString()
        {
            return LastName + ", " + FirstName;
        }
    }
}
