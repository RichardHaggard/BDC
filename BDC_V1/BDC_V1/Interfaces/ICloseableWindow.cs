using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface ICloseableWindow
    {
        bool? DialogResultEx { get; set; }
        bool CanClose        { get; set; }
    }
}
