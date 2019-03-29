using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Events
{
    public class GlobalDataEvent
    {
        public Type   GlobalType { get; }
        public string GlobalName { get; }

        public GlobalDataEvent(Type t, string n)
        {
            GlobalType = t;
            GlobalName = n;
        }
    }
}
