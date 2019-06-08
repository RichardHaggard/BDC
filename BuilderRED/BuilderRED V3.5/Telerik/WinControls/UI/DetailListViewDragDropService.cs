// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DetailListViewDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DetailListViewDragDropService : RadDragDropService
  {
    private ListViewDetailColumn draggedColumn;
    private DetailListViewElement owner;
    private RadLayeredWindow dragHintWindow;

    public DetailListViewDragDropService(DetailListViewElement owner)
    {
      this.owner = owner;
    }

    protected override void HandleMouseMove(Point mousePos)
    {
      ISupportDrop dropTarget = this.DropTarget;
      base.HandleMouseMove(mousePos);
      if (dropTarget != this.DropTarget && this.dragHintWindow == null && this.DropTarget.AllowDrop)
        this.PrepareDragHint();
      this.UpdateDragHintLocation(mousePos);
    }

    protected override void PerformStart()
    {
      base.PerformStart();
      DetailListViewCellElement context = this.Context as DetailListViewCellElement;
      if (context == null)
      {
        this.Stop(false);
      }
      else
      {
        this.draggedColumn = context.Data;
        this.PrepareDragHint();
      }
    }

    protected override void PerformStop()
    {
      base.PerformStop();
      this.DisposeDragHint();
    }

    protected override void OnPreviewDragOver(RadDragOverEventArgs e)
    {
      DetailListViewCellElement hitTarget = e.HitTarget as DetailListViewCellElement;
      DetailListViewCellElement dragInstance = e.DragInstance as DetailListViewCellElement;
      if (hitTarget != null && dragInstance != null)
      {
        ListViewDetailColumn data1 = hitTarget.Data;
        ListViewDetailColumn data2 = dragInstance.Data;
        if (data1 == null || data2 == null || data1.Owner != data2.Owner)
          e.CanDrop = false;
      }
      base.OnPreviewDragOver(e);
      if (e.CanDrop)
        return;
      this.DisposeDragHint();
    }

    protected override void OnPreviewDragDrop(RadDropEventArgs e)
    {
      DetailListViewCellElement hitTarget = e.HitTarget as DetailListViewCellElement;
      if (hitTarget == null)
      {
        base.OnPreviewDragDrop(e);
      }
      else
      {
        ListViewDetailColumn data = hitTarget.Data;
        int num = this.owner.Owner.Columns.IndexOf(this.draggedColumn);
        int index = e.DropLocation.X <= hitTarget.Size.Width / 2 ^ this.owner.RightToLeft ? this.owner.Owner.Columns.IndexOf(data) : this.owner.Owner.Columns.IndexOf(data) + 1;
        this.owner.Owner.Columns.Remove(this.draggedColumn);
        if (num < index)
          --index;
        this.owner.Owner.Columns.Insert(index, this.draggedColumn);
        this.owner.InvalidateMeasure(true);
        base.OnPreviewDragDrop(e);
      }
    }

    protected override bool IsDropTargetValid(ISupportDrop dropTarget)
    {
      DetailListViewCellElement listViewCellElement = dropTarget as DetailListViewCellElement;
      if (listViewCellElement != null)
        return listViewCellElement.Data != this.draggedColumn;
      return base.IsDropTargetValid(dropTarget);
    }

    protected virtual void UpdateDragHintLocation(Point mousePosition)
    {
      if (!this.CanCommit)
      {
        if (this.dragHintWindow == null)
          return;
        this.dragHintWindow.Visible = false;
      }
      else
      {
        RadElement dropTarget = this.DropTarget as RadElement;
        Rectangle screen1 = this.owner.ElementTree.Control.RectangleToScreen(dropTarget.ControlBoundingRectangle);
        Rectangle screen2 = this.owner.ElementTree.Control.RectangleToScreen(this.owner.ColumnContainer.ControlBoundingRectangle);
        Size size = this.owner.ColumnDragHint != null ? this.owner.ColumnDragHint.Image.Size : Size.Empty;
        Padding padding = this.owner.ColumnDragHint != null ? this.owner.ColumnDragHint.Margins : Padding.Empty;
        this.dragHintWindow.ShowWindow(new Point((dropTarget.PointFromScreen(mousePosition).X < dropTarget.Size.Width / 2 ? screen1.X : screen1.Right) - size.Width / 2, screen2.Y - padding.Top));
      }
    }

    protected virtual void PrepareDragHint()
    {
      this.dragHintWindow = new RadLayeredWindow();
      if (this.owner.ColumnDragHint == null)
        return;
      Size dragHintSize = this.GetDragHintSize(this.DropTarget);
      Bitmap bitmap = new Bitmap(dragHintSize.Width, dragHintSize.Height);
      Graphics g = Graphics.FromImage((Image) bitmap);
      this.owner.ColumnDragHint.Paint(g, new RectangleF(PointF.Empty, (SizeF) dragHintSize));
      g.Dispose();
      this.dragHintWindow.BackgroundImage = (Image) bitmap;
    }

    protected virtual Size GetDragHintSize(ISupportDrop iSupportDrop)
    {
      int width = this.owner.ColumnDragHint.Image.Size.Width;
      int height1 = this.owner.ColumnContainer.Size.Height;
      Padding margins = this.owner.ColumnDragHint.Margins;
      int height2 = height1 + margins.Vertical;
      return new Size(width + margins.Horizontal, height2);
    }

    protected virtual void DisposeDragHint()
    {
      if (this.dragHintWindow == null)
        return;
      this.dragHintWindow.Dispose();
      this.dragHintWindow = (RadLayeredWindow) null;
    }
  }
}
