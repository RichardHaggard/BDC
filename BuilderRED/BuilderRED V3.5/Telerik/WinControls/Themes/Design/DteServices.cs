// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Themes.Design.DteServices
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Reflection;
using System.Windows.Forms;

namespace Telerik.WinControls.Themes.Design
{
  public class DteServices
  {
    private static readonly Guid DesignTimeEnvironmentCLSID = new Guid(78062356, (short) 13033, (short) 18658, (byte) 155, (byte) 135, (byte) 166, (byte) 54, (byte) 3, (byte) 69, (byte) 79, (byte) 62);
    private const BindingFlags propFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
    private const BindingFlags mthdFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod;
    public System.Type EnvDteType;
    public object EnvDteInstance;
    public System.IServiceProvider ServiceProvider;
    public object EnvDteActiveDocument;
    public object EnvDteActiveProjectItem;
    public object EnvDteContainingProject;
    public object EnvDteContainingProjectProperties;
    public object EnvDteContainingProjectPath;
    public string EnvDteContainingProjectPathStr;

    public DteServices(System.IServiceProvider provider)
    {
      this.ServiceProvider = provider;
      this.EnvDteType = DteServices.GetEnvDteType();
      this.EnvDteInstance = this.GetEnvDteInstance();
      this.EnvDteActiveDocument = this.GetActiveDocument();
      this.EnvDteActiveProjectItem = this.GetActiveProjectItem();
      this.EnvDteContainingProject = this.GetActiveContainingProject();
      this.EnvDteContainingProjectProperties = this.GetActiveProjectProperties();
      this.EnvDteContainingProjectPath = this.GetActivePathAddIn();
      this.EnvDteContainingProjectPathStr = this.GetActiveProjectFullPath();
    }

    public object GetEnvDteInstance()
    {
      return this.ServiceProvider.GetService(this.EnvDteType);
    }

    public object GetActiveDocument()
    {
      object obj = (object) null;
      if (this.EnvDteInstance != null)
        obj = this.EnvDteType.InvokeMember("ActiveDocument", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, this.EnvDteInstance, new object[0]);
      return obj;
    }

    public string GetActiveDocumentPath()
    {
      string str = (string) null;
      object activeDocument = this.GetActiveDocument();
      if (activeDocument != null)
        str = (string) DteServices.GetPropertyValue("Path", activeDocument);
      return str;
    }

    public object GetActiveProjectItem()
    {
      object obj = (object) null;
      System.Type type = this.EnvDteInstance.GetType().Assembly.GetType("EnvDTE.ProjectItem");
      if ((object) type == null)
      {
        if (this.EnvDteActiveDocument != null)
          obj = this.EnvDteActiveDocument.GetType().InvokeMember("ProjectItem", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, this.EnvDteActiveDocument, new object[0]);
      }
      else
        obj = this.ServiceProvider.GetService(type);
      return obj;
    }

    public object GetActiveContainingProject()
    {
      object obj = (object) null;
      if (this.EnvDteActiveProjectItem != null)
        obj = this.EnvDteActiveProjectItem.GetType().InvokeMember("ContainingProject", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, this.EnvDteActiveProjectItem, new object[0]);
      return obj;
    }

    public object GetActiveProjectProperties()
    {
      object obj = (object) null;
      if (this.EnvDteContainingProject != null)
        obj = this.EnvDteContainingProject.GetType().InvokeMember("Properties", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, this.EnvDteContainingProject, new object[0]);
      return obj;
    }

    public object GetActivePathAddIn()
    {
      object obj = (object) null;
      if (this.EnvDteContainingProjectProperties != null)
        obj = this.EnvDteContainingProjectProperties.GetType().InvokeMember("Item", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, (Binder) null, this.EnvDteContainingProjectProperties, new object[1]
        {
          (object) "FullPath"
        });
      return obj;
    }

    public object GetProjectPropertyByName(string propertyName)
    {
      object obj = (object) null;
      if (this.EnvDteContainingProjectProperties != null)
        obj = this.EnvDteContainingProjectProperties.GetType().InvokeMember("Item", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, (Binder) null, this.EnvDteContainingProjectProperties, new object[1]
        {
          (object) propertyName
        });
      return obj;
    }

    public object GetProjectPropertyValue(string propertyName)
    {
      object projectPropertyByName = this.GetProjectPropertyByName(propertyName);
      object obj = (object) null;
      if (projectPropertyByName != null)
        obj = projectPropertyByName.GetType().InvokeMember("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, projectPropertyByName, new object[0]);
      return obj;
    }

    public object GetProjectPropertyValue(object propertyInstance)
    {
      object obj = (object) null;
      if (propertyInstance != null)
        obj = propertyInstance.GetType().InvokeMember("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, propertyInstance, new object[0]);
      return obj;
    }

    public string GetActiveProjectFullPath()
    {
      object obj = (object) null;
      if (this.EnvDteContainingProjectPath != null)
        obj = this.EnvDteContainingProjectPath.GetType().InvokeMember("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, this.EnvDteContainingProjectPath, new object[0]);
      return obj as string;
    }

    public string GetActiveProjectFullName()
    {
      object obj = (object) null;
      if (this.EnvDteContainingProject != null)
        obj = this.EnvDteContainingProject.GetType().InvokeMember("FullName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, this.EnvDteContainingProject, new object[0]);
      return obj as string;
    }

    public object GetProjectConfigurationPropertyValue(string propertyName)
    {
      object obj = (object) null;
      if (this.EnvDteContainingProject != null)
      {
        object propertyValue1 = DteServices.GetPropertyValue("ConfigurationManager", this.EnvDteContainingProject);
        if (propertyValue1 == null)
          return (object) null;
        object propertyValue2 = DteServices.GetPropertyValue("ActiveConfiguration", propertyValue1);
        if (propertyValue2 == null)
          return (object) null;
        object propertyValue3 = DteServices.GetPropertyValue("Properties", propertyValue2);
        if (propertyValue3 == null)
          return (object) null;
        object targetObject = DteServices.InvokeMethod("Item", propertyValue3, (object) propertyName);
        if (targetObject == null)
          return (object) null;
        obj = DteServices.GetPropertyValue("Value", targetObject);
      }
      return obj;
    }

    private static object GetPropertyValue(
      string memberName,
      object targetObject,
      params object[] invokationParams)
    {
      try
      {
        return targetObject.GetType().InvokeMember(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, targetObject, invokationParams);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Error getting value of property property " + memberName + " from object " + targetObject.ToString() + ": " + ex.ToString());
      }
      return (object) null;
    }

    private static object InvokeMethod(
      string memberName,
      object targetObject,
      params object[] invokationParams)
    {
      try
      {
        return targetObject.GetType().InvokeMember(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, (Binder) null, targetObject, invokationParams);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Error invoking method " + memberName + " from object " + targetObject.ToString() + ": " + ex.ToString());
      }
      return (object) null;
    }

    public static System.Type GetEnvDteType()
    {
      return System.Type.GetTypeFromCLSID(DteServices.DesignTimeEnvironmentCLSID, false);
    }

    public static object GetEnvDteInstance(System.IServiceProvider provider)
    {
      return provider.GetService(DteServices.GetEnvDteType());
    }

    public static object GetActiveDocument(object envdteInstance)
    {
      object obj = (object) null;
      if (envdteInstance != null)
        obj = DteServices.GetEnvDteType().InvokeMember("ActiveDocument", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, envdteInstance, new object[0]);
      return obj;
    }

    public static object GetActiveProjectItem(object activeDoc)
    {
      object obj = (object) null;
      if (activeDoc != null)
        obj = activeDoc.GetType().InvokeMember("ProjectItem", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, activeDoc, new object[0]);
      return obj;
    }

    public static object GetActiveContainingProject(object projectItem)
    {
      object obj = (object) null;
      if (projectItem != null)
        obj = projectItem.GetType().InvokeMember("ContainingProject", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, projectItem, new object[0]);
      return obj;
    }

    public static object GetActiveProjectProperties(object containingProject)
    {
      object obj = (object) null;
      if (containingProject != null)
        obj = containingProject.GetType().InvokeMember("Properties", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, containingProject, new object[0]);
      return obj;
    }

    public static object GetActivePathAddIn(object containingProjectProperties)
    {
      object obj = (object) null;
      if (containingProjectProperties != null)
        obj = containingProjectProperties.GetType().InvokeMember("Item", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, (Binder) null, containingProjectProperties, new object[1]
        {
          (object) "FullPath"
        });
      return obj;
    }

    public static string GetActiveProjectFullPath(object addinItem)
    {
      object obj = (object) null;
      if (addinItem != null)
        obj = addinItem.GetType().InvokeMember("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, addinItem, new object[0]);
      return obj as string;
    }

    public static string GetActiveProjectFullName(object containingProject)
    {
      object obj = (object) null;
      if (containingProject != null)
        obj = containingProject.GetType().InvokeMember("FullName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, containingProject, new object[0]);
      return obj as string;
    }
  }
}
