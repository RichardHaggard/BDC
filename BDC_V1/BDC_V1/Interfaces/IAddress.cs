using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IAddress : INotifyPropertyChanged
    {
        string Street1 { get; set; }
        string Street2 { get; set; }
        string City    { get; set; }
        string State   { get; set; }
        string Zipcode { get; set; }
    }
}
