using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName  { get; set; }

        string FirstLast { get; }
        string ToString();
    }
}
