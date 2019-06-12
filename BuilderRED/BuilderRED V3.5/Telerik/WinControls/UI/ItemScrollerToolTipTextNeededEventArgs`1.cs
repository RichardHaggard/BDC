// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ItemScrollerToolTipTextNeededEventArgs`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ItemScrollerToolTipTextNeededEventArgs<T> : ToolTipTextNeededEventArgs
  {
    private int itemIndex;
    private T item;

    public int ItemIndex
    {
      get
      {
        return this.itemIndex;
      }
    }

    public T Item
    {
      get
      {
        return this.item;
      }
    }

    public ItemScrollerToolTipTextNeededEventArgs(
      ToolTip toolTip,
      int itemIndex,
      T item,
      string tooltipText)
      : base(toolTip, tooltipText)
    {
      this.itemIndex = itemIndex;
      this.item = item;
    }
  }
}
