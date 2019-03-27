using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IValidUsers
    {
        IReadOnlyCollection<string> GetValidUsers();
        bool ValidateUser(string userName, string password);
    }
}
