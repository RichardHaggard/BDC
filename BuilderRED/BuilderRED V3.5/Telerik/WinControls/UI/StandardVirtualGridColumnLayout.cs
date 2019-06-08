// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StandardVirtualGridColumnLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class StandardVirtualGridColumnLayout : BaseVirtualGridColumnLayout
  {
    private int resizedColumn = int.MinValue;

    public override void CalculateColumnWidths(SizeF availableSize)
    {
    }

    public override void StartColumnResize(int column)
    {
      this.resizedColumn = column;
    }

    public override bool ResizeColumn(int delta)
    {
      int width = this.TableElement.GetColumnWidth(this.resizedColumn) + delta;
      this.TableElement.SetColumnWidth(this.resizedColumn, width);
      return width == this.TableElement.GetColumnWidth(this.resizedColumn);
    }

    public override void EndResizeColumn()
    {
      this.resizedColumn = int.MinValue;
    }

    public override void ResetCache()
    {
      this.resizedColumn = int.MinValue;
    }
  }
}
