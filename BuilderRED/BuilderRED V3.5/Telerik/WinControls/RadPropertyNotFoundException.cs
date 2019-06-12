// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadPropertyNotFoundException
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class RadPropertyNotFoundException : ArgumentException
  {
    private readonly string propertyName;
    private readonly string typeName;

    internal RadPropertyNotFoundException(string propertyName, string typeName)
      : base(string.Format("No such property registered: {0}, {1}", (object) propertyName, (object) typeName))
    {
      this.propertyName = propertyName;
      this.typeName = typeName;
    }

    public string TypeName
    {
      get
      {
        return this.typeName;
      }
    }

    public string PropertyName
    {
      get
      {
        return this.propertyName;
      }
    }
  }
}
