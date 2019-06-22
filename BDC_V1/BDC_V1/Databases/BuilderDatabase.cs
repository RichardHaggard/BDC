using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Databases
{
    public class BuilderDatabase : IBuilderDatabase
    {
        public const string ConnectionString = @"Driver={Microsoft Access Driver (*.mdb)};";

        [NotNull]   public string Filename { get; }
        [CanBeNull] public string Password { get; set; } = null;
        [CanBeNull] public string UserId   { get; set; } = null;

        public BuilderDatabase([NotNull] string filename, [CanBeNull] string password=null, [CanBeNull] string userId=null)
        {
            if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException(nameof(filename));
            Filename = filename;
            Password = password;
            UserId = userId;
        }

        public DataTable GetTableFromQuery(string query, IDictionary<string, object> parameters, CommandType commandType)
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));

            var dataTable = new DataTable();
            using (var conn = GetConnection()) 
            {
                //var adapter = new OdbcDataAdapter
                //{
                //    SelectCommand = new OdbcCommand("DROP TABLE " + tableName + ";", connection)
                //};
                //var builder = new OdbcCommandBuilder(adapter);

                //adapter.Fill(dataTable, tableName);

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

        public DataTable LoadDataSet(ref DataSet oDs, string sTableName, string query)
        {
            if (string.IsNullOrEmpty(sTableName)) throw new ArgumentNullException(nameof(sTableName));
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));

            var dataTable = oDs.Tables.Add(sTableName);
            using (var conn = GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;

                    using (var adapter = new OdbcDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public virtual OdbcConnection GetConnection()
        {
            return GetConnection(Filename, Password);
        }

        public virtual bool IsValidDatabase()
        {
            return true;
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        public static OdbcConnection GetConnection([NotNull] string sourceDb, [CanBeNull] string sPwd=null, [CanBeNull] string user=null)
        {
            var builder = new OdbcConnectionStringBuilder { Driver = "Microsoft Access Driver (*.mdb)" };
            builder.Add("Dbq", sourceDb);
            if (!string.IsNullOrEmpty(user)) builder.Add("Uid", user);
            if (!string.IsNullOrEmpty(sPwd)) builder.Add("Pwd", sPwd);

            return new OdbcConnection(builder.ConnectionString);
        }

        public static bool IsValidDatabase([NotNull] string filename)
        {
            return (!string.IsNullOrEmpty(filename) &&
                    File.Exists(filename));
        }
    }
}
