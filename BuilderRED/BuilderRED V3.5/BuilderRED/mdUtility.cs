// Decompiled with JetBrains decompiler
// Type: BuilderRED.mdUtility
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ADODB;
using ADOX;
using BuilderRED.My;
using ERDC.CERL.SMS.Libraries.Business.BuildingBR;
using ERDC.CERL.SMS.Libraries.Business.E2F;
using ERDC.CERL.SMS.Libraries.Data.DataAccess;
using ERDC.CERL.SMS.Libraries.Portable.Assessments;
using ERDC.CERL.SMS.Libraries.Security.Builder;
using ERDC.CERL.SMS.Libraries.Service.Managers.Assessments;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BuilderRED
{
  [StandardModule]
  internal sealed class mdUtility
  {
    private static DataSet m_MasterSet = new DataSet();
    private static object m_bUniformat = (object) null;
    private const string MOD_NAME = "mdUtility";
    public const int SPRINKLER_CMC_ID = 5061;
    private static string m_strDBPath;
    private static DBAccess m_DBAccess;
    public static frmMain fMainForm;
    public static frmWorkItems fWorkItems;
    public static string LookupDataBaseName;
    public static bool UseEnergyForm;
    public static bool UseMissingComponentsForm;
    public static bool UseADAForm;
    public static bool UseSprinklerButton;
    public static bool UseSeismicForm;
    public static bool UseCoverage;
    public static float ScaleFactor;
    private static mdUtility.SystemofMeasure m_uUnits;
    private static bool bGeneratorInitialized;
    public static string HelpFilePath;
    private static string m_strCurrentInspector;
    private static bool m_LoggedIn;
    private static IAssessmentsServiceProvider m_ConnectionProvider;
    private static mdUtility.UserAccount m_User;
    private static ArrayList m_BREDTableList;

    public static bool LoggedIn
    {
      get
      {
        return mdUtility.m_LoggedIn;
      }
      set
      {
        mdUtility.m_LoggedIn = value;
      }
    }

    public static string PackageFileName { get; set; }

    public static IAssessmentsServiceProvider ConnectionProvider
    {
      get
      {
        return mdUtility.m_ConnectionProvider;
      }
      set
      {
        AssessmentsImagePackage assessmentsClient = (AssessmentsImagePackage) value.AssessmentsClient;
        if (mdUtility.User != null)
        {
          ERDC.CERL.SMS.Libraries.Business.E2F.UserAccount user = new ERDC.CERL.SMS.Libraries.Business.E2F.UserAccount(mdUtility.User.ID);
          assessmentsClient.AssessmentsClient.SetExplicitUser(user);
        }
        AssessmentsServiceConnectionProvider.SetProvider(value);
        mdUtility.m_ConnectionProvider = value;
      }
    }

    public static mdUtility.UserAccount User
    {
      get
      {
        return mdUtility.m_User;
      }
      set
      {
        mdUtility.m_User = value;
        if (mdUtility.m_ConnectionProvider == null || mdUtility.m_User == null)
          return;
        AssessmentsImagePackage assessmentsClient = (AssessmentsImagePackage) mdUtility.m_ConnectionProvider.AssessmentsClient;
        ERDC.CERL.SMS.Libraries.Business.E2F.UserAccount byId;
        using (IPersistenceManager PM = PMFactory.Create(false, true, (IBuilderPrincipalProvider) null))
          byId = ERDC.CERL.SMS.Libraries.Business.E2F.UserAccount.GetByID(PM, new Guid?(mdUtility.User.ID));
        assessmentsClient.AssessmentsClient.SetExplicitUser(byId);
      }
    }

    public static string strCurrentInspector
    {
      get
      {
        return mdUtility.m_strCurrentInspector;
      }
      set
      {
        mdUtility.m_strCurrentInspector = value;
      }
    }

    public static void Errorhandler(Exception ex, string strModuleName, string strProcName)
    {
      StreamWriter streamWriter;
      try
      {
        if (!Directory.Exists(mdUtility.UserApplicationDataDirectory))
          Directory.CreateDirectory(mdUtility.UserApplicationDataDirectory);
        streamWriter = new StreamWriter(mdUtility.UserApplicationDataDirectory + "\\BREDErrors.log", true);
        streamWriter.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
        int num = (int) Interaction.MsgBox((object) ("An Error has occurred.  If the problem continues, please contact technical support\r\nModule: " + strModuleName + "\r\nProcedure: " + strProcName + "\r\nAdditional Information:\r\n" + ex.Message + "\r\nStack Trace:\r\n" + ex.StackTrace), MsgBoxStyle.OkOnly, (object) "Errors Encountered");
        try
        {
          mdUtility.fMainForm.Cursor = Cursors.Default;
        }
        catch (Exception ex1)
        {
          ProjectData.SetProjectError(ex1);
          ProjectData.ClearProjectError();
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        int num = (int) MessageBox.Show("Log exception:\r\n" + ex1.Message + "\r\nOriginal Exception:\r\n" + ex.Message, "Unhandled errors encountered", MessageBoxButtons.OK);
        ProjectData.ClearProjectError();
      }
      finally
      {
        if (!Information.IsNothing((object) streamWriter))
          streamWriter.Close();
      }
    }

    internal static DBAccess DB
    {
      get
      {
        return mdUtility.m_DBAccess;
      }
    }

    internal static string UserApplicationDataDirectory
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + MyProject.Application.Info.CompanyName + "\\" + MyProject.Application.Info.Title;
      }
    }

    internal static string CommonApplicationDataDirectory
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + MyProject.Application.Info.CompanyName + "\\" + MyProject.Application.Info.Title;
      }
    }

    internal static string DatabasePath
    {
      get
      {
        return mdUtility.m_strDBPath;
      }
      set
      {
        mdUtility.m_strDBPath = value;
      }
    }

    internal static bool Bootstrap(string DBName)
    {
      bool flag;
      try
      {
        if (mdUtility.m_DBAccess != null)
        {
          mdUtility.m_DBAccess.Dispose();
          mdUtility.m_DBAccess = (DBAccess) null;
        }
        mdUtility.m_strDBPath = DBName;
        mdUtility.m_DBAccess = new DBAccess(IDBAccess.DBServerType.OleDB, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdUtility.m_strDBPath + ";Jet OLEDB:Database Password=erdccerl;");
        mdUtility.ConnectionProvider = (IAssessmentsServiceProvider) new BREDAssessmentServiceConnectionProvider("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdUtility.m_strDBPath + ";Jet OLEDB:Database Password=erdccerl;", mdUtility.PackageFileName);
        if (mdUtility.TableList(mdUtility.m_strDBPath, "erdccerl") == null)
          throw new ApplicationException("This database is does not appear to be a valid BUILDER RED database.  \r\nPlease make sure this file was downloaded from the matching version of BUILDER.");
        mdUtility.DeleteAllLinkedTables(mdUtility.m_strDBPath, "erdccerl");
        ArrayList oTableNames = mdUtility.TableList(mdUtility.CommonApplicationDataDirectory + "\\lookup.mdb", "fidelity");
        if (oTableNames == null)
          throw new ApplicationException("The critical database named Lookup.mdb is not the most current version.\rPlease check that Builder RED has been properly installed and that you have downloaded the latest Lookup.mdb database.");
        string SourceDB = mdUtility.CommonApplicationDataDirectory + "\\lookup.mdb";
        mdUtility.DBLinker(ref SourceDB, oTableNames, "fidelity");
        flag = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (mdUtility), nameof (Bootstrap));
        flag = false;
        ProjectData.ClearProjectError();
      }
      return flag;
    }

    internal static DataTable get_MstrTable(string sTableName)
    {
      return mdUtility.m_MasterSet.Tables[sTableName];
    }

    internal static string get_ConfigValue(string ConfigName)
    {
      if (mdUtility.get_MstrTable("Configuration") == null)
        mdUtility.LoadMstrTable("Configuration", "Select * from Configuration");
      DataRow dataRow = mdUtility.get_MstrTable("Configuration").AsEnumerable().FirstOrDefault<DataRow>((Func<DataRow, bool>) (r => Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(r[nameof (ConfigName)], (object) ConfigName, false)));
      return Conversions.ToString(dataRow == null || Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRow[nameof (ConfigValue)])) ? (object) "" : dataRow[nameof (ConfigValue)]);
    }

    public static void ReloadDataAccess()
    {
      string str = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdUtility.CommonApplicationDataDirectory + "\\BRED.MDB;Jet OLEDB:Database Password=fidelity;";
      mdUtility.m_DBAccess = new DBAccess(IDBAccess.DBServerType.OleDB, str);
      PMFactory.SetParameters(10000, (object) new BuildingBRTypes(), RuntimeHelpers.GetObjectValue(new object()), str, IDBAccess.DBServerType.OleDB, false);
      mdUtility.ConnectionProvider = (IAssessmentsServiceProvider) new BREDAssessmentServiceConnectionProvider(str, mdUtility.PackageFileName);
    }

    internal static void LoadMstrTable(string sTableName, string sSQL)
    {
      if (mdUtility.m_MasterSet.Tables.Contains(sTableName))
        mdUtility.m_MasterSet.Tables.Remove(mdUtility.get_MstrTable(sTableName));
      mdUtility.DB.LoadDataSet(ref mdUtility.m_MasterSet, sTableName, sSQL);
    }

    public static void ClearMstrSetTables()
    {
      mdUtility.m_MasterSet.Tables.Clear();
    }

    [STAThread]
    public static void Main()
    {
      try
      {
        mdUtility.HelpFilePath = Application.StartupPath + "\\BRED.chm";
        Application.EnableVisualStyles();
        frmSplash frmSplash = new frmSplash();
        frmSplash.Show();
        frmSplash.Refresh();
        mdUtility.DeleteAllLinkedTables(mdUtility.CommonApplicationDataDirectory + "\\bred.mdb", "fidelity");
        mdUtility.ReloadDataAccess();
        ArrayList oTableNames = mdUtility.TableList(mdUtility.CommonApplicationDataDirectory + "\\lookup.mdb", "fidelity");
        if (oTableNames != null)
        {
          string SourceDB = mdUtility.CommonApplicationDataDirectory + "\\lookup.mdb";
          mdUtility.DBLinker(ref SourceDB, oTableNames, "fidelity");
          mdUtility.ReloadDataAccess();
          mdUtility.fMainForm = new frmMain();
          frmSplash.Close();
          VBMath.Randomize();
        }
        else
        {
          int num = (int) Interaction.MsgBox((object) "The critical database named Lookup.mdb is not the most current version.\rPlease check that Builder RED has been properly installed and that you have downloaded the latest Lookup.mdb database.", MsgBoxStyle.Critical, (object) "Problem Encountered");
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (mdUtility), nameof (Main));
        Application.Exit();
        ProjectData.ClearProjectError();
      }
      if (mdUtility.fMainForm == null)
        return;
      Application.Run((Form) mdUtility.fMainForm);
    }

    private static void DeleteAllLinkedTables(string sDBPath, string sPwd = "")
    {
      Connection connection = (Connection) new ConnectionClass();
      try
      {
        ((_Connection) connection)[] = "Provider=Microsoft.Jet.OLEDB.4.0; Jet OLEDB:Database Password='" + sPwd + "'; Data Source=" + sDBPath;
        ((_Connection) connection).Open("", "", "", -1);
        // ISSUE: variable of a compiler-generated type
        Catalog instance = (Catalog) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000602-0000-0010-8000-00AA006D2EA4")));
        instance.ActiveConnection = (object) connection;
        int num = checked (instance[].Count - 1);
        while (num >= 0)
        {
          // ISSUE: variable of a compiler-generated type
          Table table = instance[][(object) num];
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(table.Type, "LINK", false) == 0)
          {
            try
            {
              // ISSUE: reference to a compiler-generated method
              instance[].Delete((object) table.Name);
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ProjectData.ClearProjectError();
            }
          }
          checked { num += -1; }
        }
      }
      finally
      {
        if (connection != null)
          ((_Connection) connection).Close();
      }
    }

    public static ArrayList BREDTableList
    {
      get
      {
        if (mdUtility.m_BREDTableList == null)
          mdUtility.m_BREDTableList = mdUtility.TableList(mdUtility.DatabasePath, "erdccerl");
        return mdUtility.m_BREDTableList;
      }
      set
      {
        mdUtility.m_BREDTableList = value;
      }
    }

    public static ArrayList TableList(string sDBPath, string sPwd = "")
    {
      ArrayList arrayList = new ArrayList();
      string Left = "";
      Connection connection = (Connection) new ConnectionClass();
      // ISSUE: variable of a compiler-generated type
      Catalog catalog = (Catalog) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000602-0000-0010-8000-00AA006D2EA4")));
      try
      {
        connection = (Connection) new ConnectionClass();
        ((_Connection) connection)[] = "Provider=Microsoft.Jet.OLEDB.4.0; Jet OLEDB:Database Password='" + sPwd + "'; Data Source=" + sDBPath;
        ((_Connection) connection).Open("", "", "", -1);
        // ISSUE: variable of a compiler-generated type
        Catalog instance = (Catalog) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000602-0000-0010-8000-00AA006D2EA4")));
        instance.ActiveConnection = (object) connection;
        try
        {
          foreach (Table table in instance[])
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(table.Type, "TABLE", false) == 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.UCase(Microsoft.VisualBasic.Strings.Left(table.Name, 6)), "APPVER", false) == 0)
                Left = table.Name;
              arrayList.Add((object) table.Name);
            }
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "", false) <= 0U)
          return (ArrayList) null;
        ((_Recordset) new RecordsetClass()).Open((object) ("SELECT * FROM " + Left), RuntimeHelpers.GetObjectValue(instance.ActiveConnection), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockUnspecified, -1);
        if (mdUtility.DB.GetDataTable("SELECT * FROM [AppVer_BRED] WHERE [Version_Major]=" + Conversions.ToString(MyProject.Application.Info.Version.Major)).Rows.Count > 0)
          return arrayList;
        return (ArrayList) null;
      }
      finally
      {
        if (connection != null)
          ((_Connection) connection).Close();
        catalog = (Catalog) null;
      }
    }

    public static void DBLinker(ref string SourceDB, ArrayList oTableNames, string strPwd = "")
    {
      Connection connection = (Connection) new ConnectionClass();
      try
      {
        connection = (Connection) new ConnectionClass();
        ((_Connection) connection)[] = mdUtility.DB.ConnectionString;
        ((_Connection) connection).Open("", "", "", -1);
        // ISSUE: variable of a compiler-generated type
        Catalog instance1 = (Catalog) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000602-0000-0010-8000-00AA006D2EA4")));
        instance1.ActiveConnection = (object) connection;
        try
        {
          foreach (object oTableName in oTableNames)
          {
            string str = Conversions.ToString(oTableName);
            try
            {
              // ISSUE: reference to a compiler-generated method
              instance1[].Delete((object) str);
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ProjectData.ClearProjectError();
            }
            // ISSUE: variable of a compiler-generated type
            Table instance2 = (Table) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000609-0000-0010-8000-00AA006D2EA4")));
            // ISSUE: variable of a compiler-generated type
            Table table = instance2;
            table.Name = str;
            table.ParentCatalog = instance1;
            table.Properties[(object) "Jet OLEDB:Create Link"][] = (object) true;
            table.Properties[(object) "Jet OLEDB:Link Datasource"][] = (object) SourceDB;
            table.Properties[(object) "Jet OLEDB:Link Provider String"][] = (object) ("MS Access;Pwd=" + strPwd);
            table.Properties[(object) "Jet OLEDB:Remote Table Name"][] = (object) str;
            // ISSUE: reference to a compiler-generated method
            instance1[].Append((object) instance2);
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      finally
      {
        if (connection != null)
          ((_Connection) connection).Close();
      }
    }

    public static void CopyDBQueries(ref DBAccess SourceDBAccess)
    {
      Connection connection1;
      Connection connection2;
      try
      {
        ConnectionClass connectionClass1 = new ConnectionClass();
        ((_Connection) connectionClass1)[] = mdUtility.DB.ConnectionString;
        connection1 = (Connection) connectionClass1;
        ((_Connection) connection1).Open("", "", "", -1);
        // ISSUE: variable of a compiler-generated type
        Catalog instance1 = (Catalog) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000602-0000-0010-8000-00AA006D2EA4")));
        instance1.ActiveConnection = (object) connection1;
        ConnectionClass connectionClass2 = new ConnectionClass();
        ((_Connection) connectionClass2)[] = SourceDBAccess.ConnectionString;
        connection2 = (Connection) connectionClass2;
        ((_Connection) connection2).Open("", "", "", -1);
        // ISSUE: variable of a compiler-generated type
        Catalog instance2 = (Catalog) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000602-0000-0010-8000-00AA006D2EA4")));
        instance2.ActiveConnection = (object) connection2;
        try
        {
          foreach (ADOX.View view in instance2.Views)
          {
            try
            {
              // ISSUE: reference to a compiler-generated method
              instance1.Views.Delete((object) view.Name);
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ProjectData.ClearProjectError();
            }
            // ISSUE: variable of a compiler-generated type
            Views views = instance1.Views;
            string name = view.Name;
            ADODB.Command command1 = (ADODB.Command) new CommandClass();
            ((_Command) command1).CommandText = Conversions.ToString(NewLateBinding.LateGet(view[], (System.Type) null, "CommandText", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null));
            ADODB.Command command2 = command1;
            // ISSUE: reference to a compiler-generated method
            views.Append(name, (object) command2);
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        try
        {
          foreach (Procedure procedure in instance2.Procedures)
          {
            try
            {
              if (!procedure.Name.StartsWith("~TMP"))
              {
                // ISSUE: reference to a compiler-generated method
                instance1.Views.Delete((object) procedure.Name);
              }
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ProjectData.ClearProjectError();
            }
            if (!procedure.Name.StartsWith("~TMP"))
            {
              // ISSUE: variable of a compiler-generated type
              Views views = instance1.Views;
              string name = procedure.Name;
              ADODB.Command command1 = (ADODB.Command) new CommandClass();
              ((_Command) command1).CommandText = Conversions.ToString(NewLateBinding.LateGet(procedure[], (System.Type) null, "CommandText", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null));
              ADODB.Command command2 = command1;
              // ISSUE: reference to a compiler-generated method
              views.Append(name, (object) command2);
            }
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
      finally
      {
        if (connection1 != null)
          ((_Connection) connection1).Close();
        if (connection2 != null)
          ((_Connection) connection2).Close();
      }
    }

    public static void DeleteInspectionBySection(string strSectionID)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM Inspection_Data WHERE [insp_data_sec_id]={" + strSectionID + "}");
      try
      {
        foreach (DataRow row1 in dataTable1.Rows)
        {
          DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT SAMP_DATA_ID FROM Sample_Data WHERE [samp_data_insp_data_id]={" + row1["INSP_DATA_ID"].ToString() + "}");
          try
          {
            foreach (DataRow row2 in dataTable2.Rows)
              Sample.DeleteSample(row2["SAMP_DATA_ID"].ToString());
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row1["BRED_Status"], (object) "N", false))
            row1["BRED_Status"] = (object) "D";
          else
            row1.Delete();
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.DB.SaveDataTable(ref dataTable1, "SELECT * FROM Inspection_Data WHERE [insp_data_sec_id]={" + strSectionID + "}");
    }

    public static mdUtility.SystemofMeasure Units
    {
      get
      {
        return mdUtility.m_uUnits;
      }
      set
      {
        mdUtility.fMainForm.miUnits.Checked = value == mdUtility.SystemofMeasure.umEnglish;
        mdUtility.m_uUnits = value;
      }
    }

    private static bool HasTable(ref Connection conCurr, string strTableName)
    {
      // ISSUE: variable of a compiler-generated type
      Table table;
      bool flag;
      try
      {
        // ISSUE: variable of a compiler-generated type
        Catalog instance = (Catalog) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00000602-0000-0010-8000-00AA006D2EA4")));
        instance.ActiveConnection = (object) conCurr;
        table = instance[][(object) strTableName];
        flag = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        flag = false;
        ProjectData.ClearProjectError();
      }
      finally
      {
        table = (Table) null;
      }
      return flag;
    }

    public static string GetUniqueID()
    {
      return Guid.NewGuid().ToString();
    }

    public static double GetPercent(
      string strInspectionID,
      string strSampleID,
      double dAdditionalQty)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT [SEC_QTY] FROM [Component_Section] WHERE [SEC_ID]={" + Inspection.Section(ref strInspectionID) + "}");
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT Sum([samp_data_qty]) FROM [Sample_Data] WHERE [samp_data_id]<>{" + strSampleID + "} AND [samp_data_insp_data_id]={" + strInspectionID + "}");
      DataTable dataTable3 = mdUtility.DB.GetDataTable("SELECT UOM_CONV FROM [Sample Info] WHERE [samp_data_id]={" + strSampleID + "}");
      if (dataTable1.Rows.Count > 0 && dataTable2.Rows.Count > 0 && dataTable3.Rows.Count > 0)
      {
        double num1 = !mdUtility.fMainForm.miUnits.Checked ? dAdditionalQty : Conversions.ToDouble(Interaction.IIf(dAdditionalQty != 0.0, Microsoft.VisualBasic.CompilerServices.Operators.DivideObject((object) dAdditionalQty, dataTable3.Rows[0]["UOM_CONV"]), (object) 0));
        double num2 = Conversions.ToDouble(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable1.Rows[0]["SEC_QTY"]), (object) 0));
        if (num2 != 0.0)
          return Conversions.ToDouble(NewLateBinding.LateGet((object) null, typeof (Math), "Round", new object[2]{ Microsoft.VisualBasic.CompilerServices.Operators.DivideObject(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable2.Rows[0][0]), (object) 0), (object) num1), (object) num2), (object) 2 }, (string[]) null, (System.Type[]) null, (bool[]) null));
      }
      return 0.0;
    }

    public static string NewRandomNumberString()
    {
      return Conversions.ToString(checked ((long) Math.Round(unchecked (1000000000.0 * (double) VBMath.Rnd() + 1.0))));
    }

    public static bool UseUniformat
    {
      get
      {
        if (mdUtility.m_bUniformat == null)
        {
          DataTable dataTable = mdUtility.DB.GetDataTable("SELECT * FROM Configuration where [ConfigName] = 'UseUniformat'");
          if (dataTable.Rows.Count > 0)
            mdUtility.m_bUniformat = (object) Conversions.ToBoolean(dataTable.Rows[0]["ConfigValue"]);
        }
        return Conversions.ToBoolean(mdUtility.m_bUniformat);
      }
    }

    public static double MetricConversionFactor(long lCMC)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT * FROM qryCMC_UOM where [CMC_ID] = " + Conversions.ToString(lCMC));
      if (dataTable.Rows.Count > 0)
        return Conversions.ToDouble(dataTable.Rows[0]["UOM_CONV"]);
      return 1.0;
    }

    public static string UOMforCMC(long lCMC)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT UOM_ENG_UNIT_ABBR, UOM_MET_UNIT_ABBR FROM qryCMC_UOM where [CMC_ID]=" + Conversions.ToString(lCMC));
      if (dataTable.Rows.Count <= 0)
        return "";
      if (mdUtility.fMainForm.miUnits.Checked)
        return Conversions.ToString(dataTable.Rows[0]["UOM_ENG_UNIT_ABBR"]);
      return Conversions.ToString(dataTable.Rows[0]["UOM_MET_UNIT_ABBR"]);
    }

    public static string FormatDoubleForDisplay(ref double dblTemp)
    {
      return Microsoft.VisualBasic.Strings.Format((object) dblTemp, "#,###.###");
    }

    public static double AreaConvertDBToDisplay(double dArea)
    {
      return !mdUtility.fMainForm.miUnits.Checked ? dArea : dArea * 10.76386871;
    }

    public static double AreaConvertDisplayToDB(double dArea)
    {
      return !mdUtility.fMainForm.miUnits.Checked ? dArea : dArea / 10.76386871;
    }

    public static void LogImageLink(
      string guid,
      string inventoryClass,
      string inventoryDesc,
      string heirarchy = "")
    {
      try
      {
        File.AppendAllText(mdUtility.CommonApplicationDataDirectory + "\\ImageLinkLog.txt", guid + "|" + inventoryClass + "|" + DateAndTime.Now.ToString() + "|(" + mdUtility.User.FirstName + " " + mdUtility.User.LastName + ")|(" + heirarchy + ")\r\n");
        int num = (int) Interaction.MsgBox((object) ("Image Link timestamp has been logged for - \r\n" + inventoryDesc + " at " + DateAndTime.Now.ToShortTimeString().ToString()), MsgBoxStyle.Information, (object) "Image Link Logged");
      }
      catch (IOException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        int num = (int) Interaction.MsgBox((object) "Log Failed. Log file is being used in another process", MsgBoxStyle.Exclamation, (object) "Log Failed!");
        ProjectData.ClearProjectError();
      }
      finally
      {
      }
    }

    public static string GetBreadCrumbString(string strObjectName, string strID, string strkey = "")
    {
      string str = "";
      string Left = strObjectName;
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Building", false) != 0)
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Location", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "System", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Component", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Section", false) != 0)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Inspection", false) == 0)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.CurrentSystem, "", false) == 0)
                    str = mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentLocation).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentComp).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentSection).Text;
                  else
                    str = mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentSystem).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentComp).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentSection).Text;
                }
              }
              else
                str = mdUtility.fMainForm.tvInventory.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text + " - " + mdUtility.fMainForm.tvInventory.GetNodeByKey(mdUtility.fMainForm.CurrentSystem).Text + " - " + mdUtility.fMainForm.tvInventory.GetNodeByKey(mdUtility.fMainForm.CurrentComp).Text + " - " + mdUtility.fMainForm.tvInventory.GetNodeByKey(mdUtility.fMainForm.CurrentSection).Text;
            }
            else if (mdUtility.fMainForm.Mode == frmMain.ProgramMode.pmInspection)
              str = mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(strkey).Parent.Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(strkey).Text;
            else
              str = mdUtility.fMainForm.tvInventory.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text + " - " + mdUtility.fMainForm.tvInventory.GetNodeByKey(mdUtility.fMainForm.CurrentSystem).Text + " - " + mdUtility.fMainForm.tvInventory.GetNodeByKey(mdUtility.fMainForm.CurrentComp).Text;
          }
          else
            str = mdUtility.fMainForm.Mode != frmMain.ProgramMode.pmInspection ? mdUtility.fMainForm.tvInventory.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text + " - " + mdUtility.fMainForm.tvInventory.GetNodeByKey(mdUtility.fMainForm.CurrentSystem).Text : mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentSystem).Text;
        }
        else if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.CurrentSystem, "", false) > 0U)
          str = mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentSystem).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentComp).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentSection).Text;
        else
          str = mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text + " - " + mdUtility.fMainForm.tvInspection.GetNodeByKey(strkey).Text;
      }
      else
        str = mdUtility.fMainForm.Mode != frmMain.ProgramMode.pmInventory ? mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text : mdUtility.fMainForm.tvInventory.GetNodeByKey(mdUtility.fMainForm.CurrentBldg).Text;
      return str;
    }

    public static int GetDropDownWidth(
      DataTable datasource,
      string columnWithStringsToMeasure,
      ComboBox cbo,
      Form frm)
    {
      Graphics graphics = frm.CreateGraphics();
      SizeF size = (SizeF) cbo.Size;
      try
      {
        foreach (DataRow row in datasource.Rows)
        {
          SizeF sizeF = graphics.MeasureString(Conversions.ToString(row[columnWithStringsToMeasure]), cbo.Font);
          if ((double) sizeF.Width > (double) size.Width)
            size.Width = sizeF.Width;
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      return checked ((int) Math.Round((double) size.Width));
    }

    private static object Database()
    {
      throw new NotImplementedException();
    }

    public enum SystemofMeasure
    {
      umEnglish = 1,
      umMetric = 2,
    }

    public class UserAccount
    {
      private Guid m_ID;
      private string m_FirstName;
      private string m_LastName;

      public Guid ID
      {
        get
        {
          return this.m_ID;
        }
        set
        {
          this.m_ID = value;
        }
      }

      public string FirstName
      {
        get
        {
          return this.m_FirstName;
        }
        set
        {
          this.m_FirstName = value;
        }
      }

      public string LastName
      {
        get
        {
          return this.m_LastName;
        }
        set
        {
          this.m_LastName = value;
        }
      }
    }

    public class BREDUnitOfMeasurePreferenceProvider : IUnitSystemPreferenceProvider
    {
      public UnitsSystem GetUnitsSystem()
      {
        return mdUtility.fMainForm.miUnits.Checked ? UnitsSystem.English : UnitsSystem.Metric;
      }
    }
  }
}
