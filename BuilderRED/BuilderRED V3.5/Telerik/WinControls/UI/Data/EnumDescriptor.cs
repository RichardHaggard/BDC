// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Data.EnumDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Reflection;

namespace Telerik.WinControls.UI.Data
{
  public class EnumDescriptor
  {
    private string description;
    private object value;

    public EnumDescriptor(Type source, object value)
    {
      Type enumType = source;
      string name = Enum.GetName(enumType, value);
      if (!string.IsNullOrEmpty(name))
      {
        FieldInfo field = enumType.GetField(name);
        if ((object) field != null)
        {
          DescriptionAttribute customAttribute = Attribute.GetCustomAttribute((MemberInfo) field, typeof (DescriptionAttribute)) as DescriptionAttribute;
          if (customAttribute != null)
            this.description = customAttribute.Description;
        }
        if (string.IsNullOrEmpty(this.description))
          this.description = name;
      }
      this.value = value;
    }

    public object Value
    {
      get
      {
        return this.value;
      }
    }

    public string Description
    {
      get
      {
        return this.description;
      }
      set
      {
        this.description = value;
      }
    }
  }
}
