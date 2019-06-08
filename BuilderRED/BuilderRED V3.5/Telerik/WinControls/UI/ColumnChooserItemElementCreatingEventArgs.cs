// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnChooserItemElementCreatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ColumnChooserItemElementCreatingEventArgs : EventArgs
  {
    private ColumnChooserItem itemElement;
    private GridViewColumn column;
    private RadGridViewElement gridViewElement;

    public ColumnChooserItemElementCreatingEventArgs(
      ColumnChooserItem itemElement,
      GridViewColumn column,
      RadGridViewElement gridViewElement)
    {
      this.itemElement = itemElement;
      this.column = column;
      this.gridViewElement = gridViewElement;
    }

    public ColumnChooserItem ItemElement
    {
      get
      {
        return this.itemElement;
      }
      set
      {
        this.itemElement = value;
      }
    }

    public GridViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.gridViewElement;
      }
    }
  }
}
