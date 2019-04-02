using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IItemChecklist
    {
        string ItemName      { get; set; }
        bool   ItemIsChecked { get; set; }
    }
}
