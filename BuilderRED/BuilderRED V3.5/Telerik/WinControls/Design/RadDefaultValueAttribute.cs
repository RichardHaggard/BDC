// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Design.RadDefaultValueAttribute
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Reflection;

namespace Telerik.WinControls.Design
{
  public class RadDefaultValueAttribute : DefaultValueAttribute
  {
    private string propertyName;
    private Type objectType;

    public RadDefaultValueAttribute(string propertyName, Type objectType)
      : base((string) null)
    {
      this.propertyName = propertyName;
      this.objectType = objectType;
    }

    public override object Value
    {
      get
      {
        try
        {
          DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute) null;
          PropertyInfo property = this.objectType.GetProperty(this.propertyName);
          if ((object) property != null)
            defaultValueAttribute = (DefaultValueAttribute) Attribute.GetCustomAttribute((MemberInfo) property, typeof (DefaultValueAttribute), false);
          if (defaultValueAttribute != null)
          {
            if (defaultValueAttribute != this)
              return defaultValueAttribute.Value;
            throw new ArgumentException("Incorrect use of RadDefaultValueAttribute.", property.Name, (Exception) null);
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
