// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridData.IItemAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls.UI.PropertyGridData
{
  public interface IItemAccessor
  {
    string Name { get; }

    string DisplayName { get; }

    object Value { get; set; }

    string Description { get; }

    bool ReadOnly { get; }

    string Category { get; }

    AttributeCollection Attributes { get; }

    Type PropertyType { get; }

    PropertyDescriptor PropertyDescriptor { get; }

    UITypeEditor UITypeEditor { get; }

    TypeConverter TypeConverter { get; }
  }
}
