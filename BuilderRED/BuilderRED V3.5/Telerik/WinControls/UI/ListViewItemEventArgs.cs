﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewItemEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ListViewItemEventArgs : EventArgs
  {
    private ListViewDataItem item;

    public ListViewItemEventArgs(ListViewDataItem item)
    {
      this.item = item;
    }

    public ListViewDataItem Item
    {
      get
      {
        return this.item;
      }
    }

    public RadListViewElement ListViewElement
    {
      get
      {
        return this.item.Owner;
      }
    }
  }
}
