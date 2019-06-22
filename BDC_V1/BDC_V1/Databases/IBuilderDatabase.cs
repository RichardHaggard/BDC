using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Databases
{
    public interface IBuilderDatabase : IDisposable
    {
        [NotNull]   string Filename { get; }
        [CanBeNull] string Password { get; set; }
        [CanBeNull] string UserId   { get; set; }

        [NotNull] 
        DataTable GetTableFromQuery([NotNull] string query, [CanBeNull] IDictionary<string, object> parameters=null, CommandType commandType=CommandType.Text);

        [CanBeNull] 
        object GetSingleObjectFromQuery([NotNull] string query, [CanBeNull] IDictionary<string, object> parameters, CommandType commandType);

        int ExecuteNonQuery([NotNull] string query, [CanBeNull] IDictionary<string, object> parameters, CommandType commandType);

        [NotNull] 
        DataTable LoadDataSet(ref DataSet oDs, [NotNull] string sTableName, [NotNull] string query);
        
        bool IsValidDatabase();

        [NotNull]
        OdbcConnection GetConnection();
    }
}
