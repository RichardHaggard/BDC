// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridRowDragDropBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridRowDragDropBehavior : GridDragDropBehavior
  {
    public override RadImageShape DragHint
    {
      get
      {
        return this.GridViewElement.TableElement.RowDragHint;
      }
    }

    public override Point GetDragHintLocation(ISupportDrop dropTarget, Point mousePosition)
    {
      GridRowElement gridRowElement = dropTarget as GridRowElement;
      Rectangle screen = this.GridViewElement.ElementTree.Control.RectangleToScreen(gridRowElement.ControlBoundingRectangle);
      Size size = this.DragHint.Image.Size;
      Padding margins = this.DragHint.Margins;
      int num = RadGridViewDragDropService.IsDroppedAtTop(gridRowElement.PointFromScreen(mousePosition), gridRowElement.Size.Height) ? screen.Y : screen.Bottom;
      return new Point(screen.X - margins.Left, num - size.Height / 2);
    }

    public override Size GetDragHintSize(ISupportDrop dropTarget)
    {
      int height = this.DragHint.Image.Size.Height;
      int width = this.GridViewElement.Size.Width;
      GridRowElement gridRowElement = dropTarget as GridRowElement;
      if (gridRowElement != null)
        width = gridRowElement.Size.Width;
      return new Size(width, height);
    }
  }
}
