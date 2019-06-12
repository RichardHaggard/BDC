// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridCustomGroupingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class PropertyGridCustomGroupingEventArgs : EventArgs
  {
    private bool handled = true;
    private PropertyGridTableElement propertyGridTable;
    private PropertyGridItem item;
    private object groupKey;

    public PropertyGridTableElement PropertyGridTable
    {
      get
      {
        return this.propertyGridTable;
      }
    }

    public PropertyGridItem Item
    {
      get
      {
        return this.item;
      }
    }

    public object GroupKey
    {
      get
      {
        return this.groupKey;
      }
      set
      {
        this.groupKey = value;
      }
    }

    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    public PropertyGridCustomGroupingEventArgs(
      PropertyGridTableElement table,
      PropertyGridItem item)
    {
      this.propertyGridTable = table;
      this.item = item;
    }
  }
}
