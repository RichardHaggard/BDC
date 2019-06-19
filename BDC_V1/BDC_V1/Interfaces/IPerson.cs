using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IPerson : INotifyPropertyChanged, IComparable<IPerson>
    {
        [NotNull] string LastName  { get; set; }
        [NotNull] string FirstName { get; set; }

        [NotNull] string FirstLast { get; }
        [NotNull] string LastFirst { get; }
    }
}
