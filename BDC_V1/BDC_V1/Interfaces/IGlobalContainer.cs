using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IGlobalContainer<T>
    {
        [CanBeNull] 
        T GlobalValue { get; set; }
    }
}
