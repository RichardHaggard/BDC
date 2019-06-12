// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridTreeExpanderItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridTreeExpanderItem : GridExpanderItem
  {
    private GridTableElement tableElement;

    public GridTreeExpanderItem(GridTableElement tableElement)
    {
      this.tableElement = tableElement;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.tableElement != null)
        sizeF.Width = (float) this.tableElement.TreeLevelIndent;
      return sizeF;
    }
  }
}
