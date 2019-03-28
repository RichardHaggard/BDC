using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IContact
    {
        IPerson Name { get; set; } 
        string Phone { get; set; }
        string EMail { get; set; }
    }
}
