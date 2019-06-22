﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewItemValidatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class ListViewItemValidatingEventArgs : ListViewItemCancelEventArgs
  {
    private object oldValue;
    private object newValue;

    public ListViewItemValidatingEventArgs(
      BaseListViewVisualItem visualItem,
      object oldValue,
      object newValue)
      : base(visualItem.Data)
    {
      this.oldValue = oldValue;
      this.newValue = newValue;
    }

    public object OldValue
    {
      get
      {
        return this.oldValue;
      }
    }

    public object NewValue
    {
      get
      {
        return this.newValue;
      }
      set
      {
        this.newValue = value;
      }
    }
  }
}