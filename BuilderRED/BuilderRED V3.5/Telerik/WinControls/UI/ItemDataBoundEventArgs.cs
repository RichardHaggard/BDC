// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ItemDataBoundEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ItemDataBoundEventArgs : EventArgs
  {
    private RadItem dataBoundItem;
    private object dataItem;

    public ItemDataBoundEventArgs(RadItem dataBoundItem, object dataItem)
    {
      this.dataBoundItem = dataBoundItem;
      this.dataItem = dataItem;
    }

    public RadItem DataBoundItem
    {
      get
      {
        return this.dataBoundItem;
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
