// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Design.RadDescriptionAttribute
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Reflection;

namespace Telerik.WinControls.Design
{
  public class RadDescriptionAttribute : DescriptionAttribute
  {
    private string name;
    private Type objectType;

    public RadDescriptionAttribute(string name, Type objectType)
      : base((string) null)
    {
      this.name = name;
      this.objectType = objectType;
    }

    public override string Description
    {
      get
      {
        try
        {
          DescriptionAttribute descriptionAttribute = (DescriptionAttribute) null;
          MemberInfo element = (MemberInfo) this.objectType.GetProperty(this.name);
          if ((object) element == null)
          {
            element = (MemberInfo) this.objectType.GetEvent(this.name);
            if ((object) element == null)
              element = (MemberInfo) this.objectType.GetMethod(this.name);
          }
          if ((object) element != null)
            descriptionAttribute = (DescriptionAttribute) Attribute.GetCustomAttribute(element, typeof (DescriptionAttribute));
          if (descriptionAttribute != null)
            return descriptionAttribute.Description;
        }
        catch (Exception ex)
        {
        }
        return base.Description;
      }
    }
  }
}
