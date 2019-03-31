using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Events
{
    class TabChangeEvent
    {
        public string TabControlName { get; }
        public string TabItemName    { get; }

        public TabChangeEvent(string tabControl, string tabItem)
        {
            TabControlName = tabControl;
            TabItemName    = tabItem;
        }
    }
}
