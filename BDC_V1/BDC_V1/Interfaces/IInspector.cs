using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IInspector : IPerson, IComparable<IInspector>
    {
        Guid?              UserId          { get; set; }
        [NotNull] string   UserName        { get; set; }
        [NotNull] DateTime PasswordChanged { get; set; }
        [NotNull] string   Password        { get; set; }
        bool               Disabled        { get; set; } 
    }
}
