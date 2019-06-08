// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.DesignTimeLicense
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.Licensing
{
  internal class DesignTimeLicense : License, ISerialKeyLicense
  {
    private readonly string _key;

    internal DesignTimeLicense(ILicenseKey licenseKey)
    {
      this._key = licenseKey.Key;
    }

    public override string LicenseKey
    {
      get
      {
        return this._key;
      }
    }

    public override sealed void Dispose()
    {
      this.Dispose(true);
    }

    internal static DesignTimeLicense CreateDesigntimeLicense(ILicenseKey key)
    {
      return new DesignTimeLicense(key);
    }

    protected virtual void Dispose(bool disposing)
    {
    }
  }
}
