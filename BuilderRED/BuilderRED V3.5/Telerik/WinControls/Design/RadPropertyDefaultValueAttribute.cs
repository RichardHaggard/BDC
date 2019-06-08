// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Design.RadPropertyDefaultValueAttribute
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.Design
{
  public class RadPropertyDefaultValueAttribute : DefaultValueAttribute
  {
    private string propertyName;
    private Type ownerType;

    public RadPropertyDefaultValueAttribute(string propertyName, Type ownerType)
      : base((string) null)
    {
      this.propertyName = propertyName;
      this.ownerType = ownerType;
    }

    public override object Value
    {
      get
      {
        try
        {
          RadObjectType radObjectType = RadObjectType.FromSystemType(this.ownerType);
          RadProperty registeredRadProperty = RadTypeResolver.Instance.GetRegisteredRadProperty(this.ownerType, this.propertyName);
          if (registeredRadProperty != null)
          {
            if (radObjectType != null)
              return registeredRadProperty.GetMetadata(radObjectType).DefaultValue;
          }
        }
        catch (Exception ex)
        {
        }
        return base.Value;
      }
    }
  }
}
