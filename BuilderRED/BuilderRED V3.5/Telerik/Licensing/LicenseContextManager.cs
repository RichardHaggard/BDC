// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.LicenseContextManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Reflection;

namespace Telerik.Licensing
{
  internal class LicenseContextManager : ILicenseManager
  {
    private readonly ILicenseContextData _contextData;
    private readonly bool _licensingEnabled;

    public LicenseContextManager(ILicenseContextData data)
    {
      this._contextData = data;
      this._licensingEnabled = false;
    }

    public bool LicensingEnabled
    {
      get
      {
        return this._licensingEnabled;
      }
    }

    public ILicenseContextData ContextData
    {
      get
      {
        return this._contextData;
      }
    }

    public void SaveLicenseKey(Type type, ILicenseKey key)
    {
      if (!this.LicensingEnabled)
        return;
      this.ContextData.Context.SetSavedLicenseKey(type, key.Key);
    }

    public ILicenseKey ExtractLicenseKey(Type type)
    {
      if (this.LicensingEnabled)
        return (ILicenseKey) new RuntimeKey(this.ContextData.Context.GetSavedLicenseKey(type, this.FindLicenseAssembly()));
      return (ILicenseKey) new DefaultKey();
    }

    private Assembly FindLicenseAssembly()
    {
      foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        if (assembly.FullName.Contains("App_Licenses"))
          return assembly;
      }
      return (Assembly) null;
    }
  }
}
