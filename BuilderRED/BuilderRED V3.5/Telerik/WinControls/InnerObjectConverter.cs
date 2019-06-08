// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.InnerObjectConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls
{
  public class InnerObjectConverter : ReferenceConverter
  {
    public InnerObjectConverter(Type innerObjectType)
      : base(innerObjectType)
    {
    }

    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(value, attributes);
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
      return true;
    }
  }
}
