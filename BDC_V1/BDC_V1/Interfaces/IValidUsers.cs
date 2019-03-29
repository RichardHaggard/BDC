using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IValidUsers
    {
        IReadOnlyCollection<IPerson> GetValidUsers { get; }
        bool ValidateUser(IPerson userName, string password);
    }
}
