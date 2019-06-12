// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.LicenseFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.Licensing
{
  internal static class LicenseFactory
  {
    public static License CreateLicense(ILicenseKey key)
    {
      if (key is DesignTimeKey)
        return (License) DesignTimeLicense.CreateDesigntimeLicense(key);
      if (key is RuntimeKey)
        return (License) RuntimeLicense.CreateRuntimeLicense(key);
      return (License) DefaultLicense.CreateDefaultLicense(key);
    }
  }
}
