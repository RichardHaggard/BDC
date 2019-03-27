using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Events
{
    public class CloseWindowEvent
    {
        public string WindowName { get; }

        public CloseWindowEvent([NotNull] string windowName)
        {
            WindowName = windowName;
        }
    }
}
