using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IItemChecklist : INotifyPropertyChanged
    {
        string ItemName      { get; set; }
        bool   ItemIsChecked { get; set; }
    }
}
