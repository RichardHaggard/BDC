using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IConfigInfo : INotifyPropertyChanged
    {
        string      FileName   { get; set; }
        IValidUsers ValidUsers { get; }
    }
}
