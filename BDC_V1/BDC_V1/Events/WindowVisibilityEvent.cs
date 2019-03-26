using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JetBrains.Annotations;

namespace BDC_V1.Events
{
    public class WindowVisibilityEvent
    {
        public string     WindowName       { get; }
        public Visibility WindowVisibility { get; }

        public WindowVisibilityEvent([NotNull] string windowName, Visibility visibility)
        {
            WindowName = windowName;
            WindowVisibility = visibility;
        }
    }
}
