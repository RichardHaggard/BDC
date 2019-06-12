// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.LicenseContextData
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.Licensing
{
  internal class LicenseContextData : ILicenseContextData
  {
    public LicenseContextData(LicenseContext context, Type type, bool allowExceptions)
    {
      this.Context = context;
      this.Type = type;
      this.AllowExceptions = allowExceptions;
    }

    public LicenseContext Context { get; set; }

    public Type Type { get; set; }

    public bool AllowExceptions { get; set; }
  }
}
