using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Databases
{
    public interface IDatabase
    {
        [NotNull] string Filename { get; }

        [NotNull] DataTable GetTableFromQuery([NotNull] string query, [CanBeNull] IDictionary<string, object> parameters, CommandType commandType);

        [CanBeNull] object GetSingleObjectFromQuery([NotNull] string query, [CanBeNull] IDictionary<string, object> parameters, CommandType commandType);

        int ExecuteNonQuery([NotNull] string query, [CanBeNull] IDictionary<string, object> parameters, CommandType commandType);
    }
}
