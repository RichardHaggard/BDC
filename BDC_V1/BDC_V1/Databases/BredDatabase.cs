using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Databases
{
    public class BredDatabase : Database
    {
        public BredDatabase(string bredFilename) 
            : base(bredFilename)
        {
        }

        [NotNull] public DataTable GetInspectors()
        {
            return base.GetTableFromQuery("SELECT * FROM [UserAccount] ORDER BY LastName, FirstName", null, CommandType.Text);
        }

    }
}
