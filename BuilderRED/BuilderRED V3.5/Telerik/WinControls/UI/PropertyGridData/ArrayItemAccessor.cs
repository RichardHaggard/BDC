// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridData.ArrayItemAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI.PropertyGridData
{
  public class ArrayItemAccessor : ItemAccessor
  {
    private int index;

    public ArrayItemAccessor(PropertyGridItem owner, int index)
      : base(owner)
    {
      this.index = index;
    }

    public override object Value
    {
      get
      {
        return ((Array) this.Owner.GetValueOwner()).GetValue(this.index);
      }
      set
      {
        ((Array) this.Owner.GetValueOwner()).SetValue(value, this.index);
      }
    }

    public override Type PropertyType
    {
      get
      {
        return ((PropertyGridItem) this.Owner.Parent).PropertyType.GetElementType();
      }
    }
  }
}
