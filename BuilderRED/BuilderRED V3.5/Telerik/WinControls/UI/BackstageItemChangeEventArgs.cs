// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BackstageItemChangeEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class BackstageItemChangeEventArgs : EventArgs
  {
    private BackstageTabItem newItem;
    private BackstageTabItem oldItem;

    public BackstageItemChangeEventArgs(BackstageTabItem newItem, BackstageTabItem oldItem)
    {
      this.newItem = newItem;
      this.oldItem = oldItem;
    }

    public BackstageTabItem NewItem
    {
      get
      {
        return this.newItem;
      }
    }

    public BackstageTabItem OldItem
    {
      get
      {
        return this.oldItem;
      }
    }
  }
}
