// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewVisualItemCreatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ListViewVisualItemCreatingEventArgs : EventArgs
  {
    private BaseListViewVisualItem visualItem;
    private ListViewType listViewType;
    private ListViewDataItem dataItem;

    public ListViewVisualItemCreatingEventArgs(
      BaseListViewVisualItem visualItem,
      ListViewType viewType,
      ListViewDataItem dataItem)
    {
      this.visualItem = visualItem;
      this.listViewType = viewType;
      this.dataItem = dataItem;
    }

    public ListViewDataItem DataItem
    {
      get
      {
        return this.dataItem;
      }
      set
      {
        this.dataItem = value;
      }
    }

    public ListViewType ListViewType
    {
      get
      {
        return this.listViewType;
      }
    }

    public BaseListViewVisualItem VisualItem
    {
      get
      {
        return this.visualItem;
      }
      set
      {
        this.visualItem = value;
      }
    }
  }
}
