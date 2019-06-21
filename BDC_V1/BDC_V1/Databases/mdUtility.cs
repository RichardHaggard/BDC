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

namespace BDC_V1.Databases
{
    internal sealed class mdUtility
    {
        private static DataSet _mMasterSet = new DataSet();

        internal static string DatabasePath { get; set; }

        public static string LookupDatabase => App.CommonApplicationDataDirectory + @"\lookup.mdb";
        public static string BredDatabase   => App.CommonApplicationDataDirectory + @"\bred.mdb";

        public const string BootstrapPassword = "erdccerl";
        public const string LookupPassword    = "fidelity";
        public const string BredPassword      = "fidelity";

        // ReSharper disable once InconsistentNaming
        public static IList<string> BREDTableList
        {
            get => _mBredTableList ?? (_mBredTableList = TableList(DatabasePath, BootstrapPassword));
            set => _mBredTableList = value;
        }
        private static IList<string> _mBredTableList;

        [NotNull] internal static Database DB => _mDbAccess ?? throw new InvalidOperationException();
        [CanBeNull] private static Database _mDbAccess;

        public static void ErrorHandler(Exception ex, string strModuleName, string strProcName)
        {
        }

        internal static bool Bootstrap(string dbName)
        {
            bool flag;
            DatabasePath = dbName;

            try
            {
                _mDbAccess?.Dispose();
                _mDbAccess = new Database(DatabasePath, BootstrapPassword);
                    
                //ConnectionProvider = (IAssessmentsServiceProvider)new BREDAssessmentServiceConnectionProvider("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DatabasePath + ";Jet OLEDB:Database Password=erdccerl;", PackageFileName);
                if (TableList(DatabasePath, BootstrapPassword) == null)
                    throw new ApplicationException("This database is does not appear to be a valid BUILDER RED database.\r\n" +
                                                   "Please make sure this file was downloaded from the matching version of BUILDER.");
                
                DeleteAllLinkedTables(DatabasePath, BootstrapPassword);
                var oTableNames = TableList(LookupDatabase, LookupPassword);
                if (oTableNames == null)
                    throw new ApplicationException("The critical database named Lookup.mdb is not the most current version.\r\n" +
                                                   "Please check that Builder RED has been properly installed and that you have downloaded the latest Lookup.mdb database.");
                
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

        internal static DataTable get_MasterTable(string sTableName)
        {
            return _mMasterSet.Tables[sTableName];
        }

        internal static string get_ConfigValue(string configName)
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

        public static void ReloadDataAccess()
        {
            _mDbAccess?.Dispose();
            _mDbAccess = new Database(BredDatabase, BredPassword);

            //PMFactory.SetParameters(10000, (object)new BuildingBRTypes(), RuntimeHelpers.GetObjectValue(new object()), str, IDBAccess.DBServerType.OleDB, false);
            //ConnectionProvider = (IAssessmentsServiceProvider)new BREDAssessmentServiceConnectionProvider(str, PackageFileName);
        }

        internal static void LoadMasterTable(string sTableName, string sSQL)
        {
            if (_mMasterSet.Tables.Contains(sTableName))
                _mMasterSet.Tables.Remove(get_MasterTable(sTableName));

            DB.LoadDataSet(ref _mMasterSet, sTableName, sSQL);
        }

        public static void ClearMasterSetTables()
        {
            _mMasterSet.Tables.Clear();
        }

        public mdUtility()
        {
            try
            {
                DeleteAllLinkedTables(BredDatabase, BredPassword);
                ReloadDataAccess();

                var oTableNames = TableList(LookupDatabase, LookupPassword);
                if (oTableNames != null)
                {
                    DBLinker(LookupDatabase, oTableNames, LookupPassword);
                    ReloadDataAccess();
                }
                else
                {
                    MessageBox.Show(
                        "The critical database named Lookup.mdb is not the most current version.\r\n" +
                        "Please check that Builder RED has been properly installed and that you have downloaded the latest Lookup.mdb database.",
                        "Problem Encountered",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                //ProjectData.SetProjectError(ex);
                ErrorHandler(ex, nameof(mdUtility), "Main");
                //Application.Exit();
                //ProjectData.ClearProjectError();
            }
        }

        public static void DeleteAllLinkedTables([NotNull] string sDbPath, [CanBeNull] string sPwd = null)
        {
        }

        public static IList<string> TableList([NotNull] string sDbPath, [CanBeNull] string sPwd = null)
        {
            var connectionString = Database.ConnectionString + "Dbq=" + sDbPath + ";";
            if (!string.IsNullOrEmpty(sPwd)) connectionString += "Pwd=" + sPwd + ";";

            var tables = new List<string>();
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    // Get list of user tables
                    var userTables = connection.GetSchema("Tables");
                    if (userTables != null)
                    {
                        foreach (DataRow row in userTables.Rows)
                        {
                            tables.Add(row["TABLE_NAME"].ToString());
                            //row["TABLE_CAT"] 
                            //row["TABLE_SCHEM"]
                            //row["TABLE_NAME"]
                            //row["TABLE_Type"]
                            //row["REMARKS"]
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return tables;
        }

        // ReSharper disable once InconsistentNaming
        public static void DBLinker(string sourceDb, IList<string> oTableNames, string sPwd = null)
        {
            var connectionString = Database.ConnectionString + "Dbq=" + sourceDb + ";";
            if (!string.IsNullOrEmpty(sPwd)) connectionString += "Pwd=" + sPwd + ";";

            //((_Connection) connection)[] = mdUtility.DB.ConnectionString;
            //((_Connection) connection).Open("", "", "", -1);
            //Catalog instance1 = (Catalog) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000602-0000-0010-8000-00AA006D2EA4")));
            //instance1.ActiveConnection = (object) connection;

            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    foreach (var tableName in oTableNames)
                    {
                        //instance1[].Delete(tableName);

                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "DROP TABLE " + tableName + ";";
                            cmd.CommandType = CommandType.Text;

                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }

                        //Table table = (Table) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000609-0000-0010-8000-00AA006D2EA4")));
                        //table.Name = str;
                        //table.ParentCatalog = instance1;
                        //table.Properties[(object) "Jet OLEDB:Create Link"][] = (object) true;
                        //table.Properties[(object) "Jet OLEDB:Link Datasource"][] = (object) SourceDB;
                        //table.Properties[(object) "Jet OLEDB:Link Provider String"][] = (object) ("MS Access;Pwd=" + strPwd);
                        //table.Properties[(object) "Jet OLEDB:Remote Table Name"][] = (object) str;
                        //instance1[].Append((object) instance2);


                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
