// Decompiled with JetBrains decompiler
// Type: BuilderRED.mdUtility
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using JetBrains.Annotations;
using Microsoft.Office.Interop.Access.Dao;

namespace BDC_V1.Databases
{
    internal sealed class mdUtility
    {
        private const string BootstrapPassword = "erdccerl";
        private const string LookupPassword    = "fidelity";
        private const string BredPassword      = "fidelity";

        private DataSet _mMasterSet = new DataSet();

        public static string LookupDatabase  => App.CommonApplicationDataDirectory + @"\Lookup.mdb";
        public static string BuilderDatabase => App.CommonApplicationDataDirectory + @"\BuilderDC.mdb";

        [NotNull]   internal string DatabasePath { get; set; }
        [NotNull]   internal static IBuilderDatabase DB => _mDbAccess ?? throw new InvalidOperationException();
        [CanBeNull] private  static IBuilderDatabase _mDbAccess;

        private static void ErrorHandler(Exception ex, string strModuleName, string strProcName)
        {
            Debug.WriteLine($"{strModuleName}:{strProcName} - {ex}");
        }

#if false
        internal bool Bootstrap(string dbName)
        {
            bool flag;
            DatabasePath = dbName;

            try
            {
                _mDbAccess?.Dispose();
                _mDbAccess = new BuilderDatabase(DatabasePath, BootstrapPassword);
                    
                //ConnectionProvider = (IAssessmentsServiceProvider)new BREDAssessmentServiceConnectionProvider("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DatabasePath + ";Jet OLEDB:BuilderDatabase Password=erdccerl;", PackageFileName);
                if (TableList(DatabasePath, BootstrapPassword) == null)
                    throw new ApplicationException("This builderDatabase is does not appear to be a valid BUILDER RED builderDatabase.\r\n" +
                                                   "Please make sure this file was downloaded from the matching version of BUILDER.");
                
                DeleteAllLinkedTables();
                var oTableNames = TableList(LookupDatabase, LookupPassword);
                if (oTableNames == null)
                    throw new ApplicationException("The critical builderDatabase named Lookup.mdb is not the most current version.\r\n" +
                                                   "Please check that Builder RED has been properly installed and that you have downloaded the latest Lookup.mdb builderDatabase.");
                
                DBLinker(LookupDatabase, oTableNames, LookupPassword);
                flag = true;
            }
            catch (Exception ex)
            {
                //ProjectData.SetProjectError(ex);
                ErrorHandler(ex, nameof(mdUtility), nameof(Bootstrap));
                flag = false;
                //ProjectData.ClearProjectError();
            }

            return flag;
        }
#endif

        internal DataTable get_MasterTable(string sTableName)
        {
            return _mMasterSet.Tables[sTableName];
        }

        internal string get_ConfigValue(string configName)
        {
            if (get_MasterTable("Configuration") == null)
                LoadMasterTable("Configuration", "Select * from Configuration");

            var dataRow = get_MasterTable("Configuration")
                .AsEnumerable()
                .FirstOrDefault<DataRow>(r => r["ConfigName"] == (object)configName);

            return ((dataRow != null) && (dataRow["ConfigValue"] == DBNull.Value))
                ? dataRow["ConfigValue"].ToString()
                : String.Empty;
        }

        private void ReloadDataAccess()
        {
            _mDbAccess?.Dispose();
            _mDbAccess = new BuilderDatabase(BuilderDatabase, BredPassword);

            ClearMasterSetTables();

            //PMFactory.SetParameters(10000, (object)new BuildingBRTypes(), RuntimeHelpers.GetObjectValue(new object()), str, IDBAccess.DBServerType.OleDB, false);
            //ConnectionProvider = (IAssessmentsServiceProvider)new BREDAssessmentServiceConnectionProvider(str, PackageFileName);
        }

        internal void LoadMasterTable(string sTableName, string sSql)
        {
            if (_mMasterSet.Tables.Contains(sTableName))
                _mMasterSet.Tables.Remove(get_MasterTable(sTableName));

            DB.LoadDataSet(ref _mMasterSet, sTableName, sSql);
        }

        public void ClearMasterSetTables()
        {
            _mMasterSet.Tables.Clear();
        }

        public mdUtility()
        {
            try
            {
                DatabasePath = BuilderDatabase;

                ReloadDataAccess();         // sets DB

                DeleteAllLinkedTables();
                ReloadDataAccess();         // resets DB

                var oTableNames = TableList(LookupDatabase, LookupPassword);
                if (oTableNames != null)
                {
                    DBLinker(LookupDatabase, oTableNames, LookupPassword);
                    ReloadDataAccess();
                }
                else
                {
                    MessageBox.Show(
                        "The critical builderDatabase named Lookup.mdb is not the most current version.\r\n" +
                        "Please check that Builder RED has been properly installed and that you have downloaded the latest Lookup.mdb builderDatabase.",
                        "Problem Encountered",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler(ex, nameof(mdUtility), nameof(mdUtility));
            }
        }

        public static void DeleteAllLinkedTables([NotNull] IBuilderDatabase builderDatabase)
        {
            try
            {
#if false
                var displayTypes = new List<string>
                {
                    //"MetaDataCollections",
                    //"DataSourceInformation",
                    //"DataTypes",
                    //"Restrictions",
                    //"ReservedWords",
                    //"Columns",
                    //"Indexes",
                    //"Procedures",
                    //"ProcedureColumns",
                    //"ProcedureParameters",
                    "Tables",
                    //"Views",
                };

                var tableNames = new List<string>();

                using (var connection = builderDatabase.GetConnection())
                {
                    try
                    {
                        connection.Open();

                        // Get list of schemas
                        var schemas = connection.GetSchema();
                        if (schemas != null)
                        {
                            for (var idx=0; idx < schemas.Columns.Count; idx++)
                            {
                                Debug.WriteIf(idx > 0, ", ");
                                Debug.Write(schemas.Columns[idx].ColumnName);   
                            }
                            Debug.WriteLine("");

                            foreach (DataRow row in schemas.Rows)
                            {
                                for (var idx=0; idx < schemas.Columns.Count; idx++)
                                {
                                    Debug.WriteIf(idx > 0, ", ");
                                    Debug.Write(row[idx]);   
                                }
                                Debug.WriteLine("");
                            }

                            foreach (DataRow schema in schemas.Rows)
                            {
                                if (!displayTypes.Contains(schema[0].ToString())) continue;

                                Debug.WriteLine("\r\nSchema:" + schema[0]);

                                var rows = connection.GetSchema(schema[0].ToString());
                                if (rows != null)
                                {
                                    for (var idx=0; idx < rows.Columns.Count; idx++)
                                    {
                                        Debug.WriteIf(idx > 0, ", ");
                                        Debug.Write(rows.Columns[idx].ColumnName);   
                                    }
                                    Debug.WriteLine("");

                                    foreach (DataRow row in rows.Rows)
                                    {
                                        for (var idx=0; idx < rows.Columns.Count; idx++)
                                        {
                                            Debug.WriteIf(idx > 0, ", ");
                                            Debug.Write(row[idx]);   
                                        }
                                        Debug.WriteLine("");

                                        tableNames.Add(row["TABLE_NAME"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
#endif
#if false
                foreach (var tableName in tableNames)
                {
                    try
                    {
                        Debug.WriteLine("\r\nTable:" + tableName);

                        var sysTables = builderDatabase.GetTableFromQuery("SELECT * FROM " + tableName);
                        for (var idx = 0; idx < sysTables.Columns.Count; idx++)
                        {
                            Debug.WriteIf(idx > 0, ", ");
                            Debug.Write(sysTables.Columns[idx].ColumnName);
                        }

                        Debug.WriteLine("");

                        foreach (DataRow row in sysTables.Rows)
                        {
                            for (var idx = 0; idx < sysTables.Columns.Count; idx++)
                            {
                                Debug.WriteIf(idx > 0, ", ");
                                Debug.Write(row[idx]);
                            }

                            Debug.WriteLine("");
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler(ex, nameof(mdUtility), nameof(DeleteAllLinkedTables));
                    }
                }
#endif
#if false
                using (var connection = builderDatabase.GetConnection())
                {
                    try
                    {
                        connection.Open();
                        var schema = connection.GetSchema(OdbcMetaDataCollectionNames.Tables);

                        for (var idx = 0; idx < schema.Columns.Count; idx++)
                        {
                            Debug.WriteIf(idx > 0, ", ");
                            Debug.Write(schema.Columns[idx].ColumnName);
                        }

                        Debug.WriteLine("");

                        foreach (DataRow row in schema.Rows)
                        {
                            for (var idx = 0; idx < schema.Columns.Count; idx++)
                            {
                                Debug.WriteIf(idx > 0, ", ");
                                Debug.Write(row[idx]);
                            }

                            Debug.WriteLine("");
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
#endif
#if false
                using (var connection = builderDatabase.GetConnection())
                {
                    try
                    {
                        connection.Open();

                        var command = new OdbcCommand("SHOW TABLES", connection);
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Debug.WriteLine(reader[0]);
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
#endif
#if false
                using (var connection = builderDatabase.GetConnection())
                {
                    try
                    {
                        var strCommand = "SELECT DISTINCTROW msysobjects.Name, msysobjects.Database, msysobjects.Connect " +
                                         "FROM msysobjects WHERE (((msysobjects.Type)=6 Or (msysobjects.Type) Like \"dsn*\")) ORDER BY msysobjects.Database;";
                        connection.Open();

                        var command = new OdbcCommand(strCommand, connection);
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Debug.WriteLine(reader[0]);
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
#endif
            }
            catch (Exception ex)
            {
                ErrorHandler(ex, nameof(mdUtility), nameof(DeleteAllLinkedTables));
            }
        }

        public static void DeleteAllLinkedTables(
            [NotNull]   string sourceDb, 
            [CanBeNull] string sPwd = null, 
            [CanBeNull] string user = null)
        {
            var database = new BuilderDatabase(sourceDb, sPwd, user);
            DeleteAllLinkedTables(database);
        }

        public void DeleteAllLinkedTables()
        {
            DeleteAllLinkedTables(DB);
        }

        public static IList<string> TableList([NotNull] IBuilderDatabase builderDatabase)
        {
            var tables = new List<string>();
            try
            {
                using (var connection = builderDatabase.GetConnection())
                {
                    try
                    {
                        connection.Open();

                        // Get list of user tables
                        var userTables = connection.GetSchema("Tables");
                        if (userTables != null)
                        {
                            foreach (DataRow row in userTables.Rows)
                            {
                                if (row["TABLE_Type"].ToString() == "TABLE") tables.Add(row["TABLE_NAME"].ToString());
                            }
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler(ex, nameof(mdUtility), nameof(TableList));
            }

            return tables;
        }

        public static IList<string> TableList(
            [NotNull]   string sourceDb,
            [CanBeNull] string sPwd = null,
            [CanBeNull] string user = null)
        {
            var database = new BuilderDatabase(sourceDb, sPwd, user);
            return TableList(database);
        }

        public IList<string> TableList()
        {
            return TableList(DB);
        }

        // ReSharper disable once InconsistentNaming
        private void DBLinker(
            [NotNull]   string sourceDb, 
            [NotNull]   IList<string> oTableNames, 
            [CanBeNull] string sPwd = null, 
            [CanBeNull] string user = null)
        {
            //((_Connection) connection)[] = mdUtility.DB.ConnectionString;
            //((_Connection) connection).Open("", "", "", -1);
            //Catalog instance1 = (Catalog) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000602-0000-0010-8000-00AA006D2EA4")));
            //instance1.ActiveConnection = (object) connection;

            try
            {
                //var dbEn = new DBEngine();
                //var db = dbEn.OpenDatabase(DB.Filename, null, false, "ODBC;PWD=fidelity");
                //var tbl = db.CreateTableDef("AAATestTable");
                //tbl.SourceTableName = "RO_Component";
                //tbl.Connect = "DSN=MS Access Database;DBQ=C:/ProgramData/Cardno/BuilderDC/BuilderDC.mdb;PWD=fidelity;";
                //db.TableDefs.Append(tbl);
                //db.TableDefs.Refresh();


                //var connectionString = @"MS Access;DATABASE=" + sourceDb + ";";
                //if (!string.IsNullOrEmpty(user)) connectionString += "Uid=" + user + ";";
                //if (!string.IsNullOrEmpty(sPwd)) connectionString += "Pwd=" + sPwd + ";";

                //using (var connection = DB.GetConnection())
                //{
                //    try
                //    {
                //        connection.Open();

                //        //foreach (var tableName in oTableNames)
                //        {
                //            using (var cmd = connection.CreateCommand())
                //            {
                //                cmd.CommandType = CommandType.Text;
                //                cmd.CommandText = "DROP TABLE " + "RO_Component" + ";";

                //                try
                //                {
                //                    cmd.ExecuteNonQuery();
                //                }
                //                catch (Exception ex)
                //                {
                //                    ErrorHandler(ex, nameof(mdUtility), nameof(DBLinker));
                //                }
                //            }
                //        }

                //        //foreach (var tableName in oTableNames)
                //        {
                //            using (var cmd = connection.CreateCommand())
                //            {
                //                cmd.CommandType = CommandType.Text;
                //                cmd.CommandText = "create table RO_Component engine=connect table_type=ODBC" +
                //                    "block_size=10 tabname='RO_Component'" +
                //                    "Connection='DSN=MS Access Database;DBQ=C:/ProgramData/Cardno/BuilderDC/BuilderDC.mdb;PWD=fidelity';";
                //                try
                //                {
                //                    cmd.ExecuteNonQuery();
                //                }
                //                catch (Exception ex)
                //                {
                //                    ErrorHandler(ex, nameof(mdUtility), nameof(DBLinker));
                //                }
                //            }
                //        }


                //    }
                //    finally
                //    {
                //        connection.Close();
                //    }
                //}
            }
            catch (Exception e)
            {
                ErrorHandler(e, nameof(mdUtility), nameof(DBLinker));
            }
        }
    }
}
