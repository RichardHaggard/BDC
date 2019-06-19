using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Databases
{
    public class Database : IDatabase
    {
        // Lookup.xxx.mdb pass=fidelity
        // ???.mdb pass=erdccerl

        const string ConnectionString = @"Driver={Microsoft Access Driver (*.mdb)};";

        public string Filename { get; }
        public string Password { get; set; }

        public Database([NotNull] string filename)
        {
            if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException(nameof(filename));
            Filename = filename;
        }

        public DataTable GetTableFromQuery(string query, IDictionary<string, object> parameters, CommandType commandType)
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));

            var dataTable = new DataTable();
            using (var conn = GetConnection()) 
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = commandType;

                    if (parameters != null) 
                    {
                        foreach (var parameter in parameters) 
                            cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    using (var adapter = new OdbcDataAdapter(cmd)) 
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public object GetSingleObjectFromQuery(string query, IDictionary<string, object> parameters, CommandType commandType)
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));

            object value = null;
            using (var conn = GetConnection()) 
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = commandType;

                    if (parameters != null) 
                    {
                        foreach (var parameter in parameters) 
                            cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)) 
                    {
                        while (reader.Read()) 
                        {
                            value = reader.GetValue(0);
                        }
                    }
                }
            }

            return value;
        }

        public int ExecuteNonQuery(string query, IDictionary<string, object> parameters, CommandType commandType)
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));

            var value = 1;
            using (var conn = GetConnection()) 
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = commandType;

                    if (parameters != null) 
                    {
                        foreach (var parameter in parameters) 
                            cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    try
                    {
                        cmd.Connection.Open();
                        value = cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        cmd.Connection.Close();
                    }
                }
            }

            return value;
        }

        [NotNull] private OdbcConnection GetConnection()
        {
            var connectionString = ConnectionString + "Dbq=" + Filename + ";";
            if (!string.IsNullOrEmpty(Password)) connectionString += "Password=" + Password + ";";

            return new OdbcConnection(connectionString);
        }

        public virtual bool IsValidDatabase()
        {
            return true;
        }

        public static bool IsValidDatabase([NotNull] string filename)
        {
            return (!string.IsNullOrEmpty(filename) &&
                    File.Exists(filename));
        }

    }
}
