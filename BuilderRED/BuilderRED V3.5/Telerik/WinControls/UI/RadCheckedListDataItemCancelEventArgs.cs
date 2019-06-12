// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckedListDataItemCancelEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class RadCheckedListDataItemCancelEventArgs : CancelEventArgs
  {
    private RadCheckedListDataItem item;

    public RadCheckedListDataItemCancelEventArgs(RadCheckedListDataItem item)
    {
      this.item = item;
    }

    public RadCheckedListDataItemCancelEventArgs(RadCheckedListDataItem item, bool cancel)
      : base(cancel)
    {
      this.item = item;
    }

    public RadCheckedListDataItem Item
    {
      get
      {
        return this.item;
      }
    }

    public RadListElement ListViewElement
    {
      get
      {
        return this.item.Owner;
      }
    }
  }
}
