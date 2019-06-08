// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridDetailViewRowBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridDetailViewRowBehavior : GridRowBehavior
  {
    protected override bool SelectPositionOnMouseDownLeft(
      MouseEventArgs e,
      GridRowElement rowElement,
      GridCellElement cellElement)
    {
      return false;
    }

    protected override bool OnMouseUpLeft(MouseEventArgs e)
    {
      return false;
    }

    public override bool CanResizeRow(Point currentLocation, GridRowElement rowElement)
    {
      if (!this.GridViewElement.UseScrollbarsInHierarchy || rowElement == null)
        return false;
      Rectangle boundingRectangle = rowElement.ControlBoundingRectangle;
      if (currentLocation.Y < boundingRectangle.Bottom - 2 || currentLocation.Y > boundingRectangle.Bottom + 2)
        return false;
      GridViewRowInfo rowInfo = rowElement.RowInfo;
      return true;
    }
  }
}
