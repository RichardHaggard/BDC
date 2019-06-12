// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.DefaultLicense
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.Licensing
{
  internal class DefaultLicense : License, ISerialKeyLicense
  {
    private const string DefaultLicenseKey = "default@telerik.com";

    internal DefaultLicense(ILicenseKey key)
    {
    }

    public override string LicenseKey
    {
      get
      {
        return "default@telerik.com";
      }
    }

    public override sealed void Dispose()
    {
      this.Dispose(true);
    }

    internal static DefaultLicense CreateDefaultLicense(ILicenseKey key)
    {
      return new DefaultLicense(key);
    }

    protected virtual void Dispose(bool disposing)
    {
    }
  }
}
