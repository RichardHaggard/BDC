// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ItemDataBindingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ItemDataBindingEventArgs : EventArgs
  {
    private RadItem dataBindingItem;
    private object dataItem;
    private RadItem newBindingItem;

    public ItemDataBindingEventArgs(RadItem dataBindingItem, object dataItem)
    {
      this.dataBindingItem = dataBindingItem;
      this.dataItem = dataItem;
    }

    public RadItem DataBindingItem
    {
      get
      {
        return this.dataBindingItem;
      }
    }

    public RadItem NewBindingItem
    {
      get
      {
        return this.newBindingItem;
      }
      set
      {
        this.newBindingItem = value;
      }
    }

    public object DataItem
    {
      get
      {
        return this.dataItem;
      }
    }
  }
}
