﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.RuntimeLicense
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal class RuntimeLicense : DesignTimeLicense
  {
    internal RuntimeLicense(ILicenseKey licenseKey)
      : base(licenseKey)
    {
    }

    internal static RuntimeLicense CreateRuntimeLicense(ILicenseKey key)
    {
      return new RuntimeLicense(key);
    }
  }
}