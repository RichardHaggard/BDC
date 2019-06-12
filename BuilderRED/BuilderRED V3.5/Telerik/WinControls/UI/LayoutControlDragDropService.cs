// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;

namespace Telerik.WinControls.UI
{
  public class LayoutControlDragDropService : RadDragDropService
  {
    private DraggableLayoutControlItem draggedItem;
    private RadLayoutControl owner;
    private LayoutControlDraggableOverlay overlay;
    private Cursor deleteCursor;

    public LayoutControlDragDropService(
      RadLayoutControl owner,
      LayoutControlDraggableOverlay overlay)
    {
      this.owner = owner;
      this.overlay = overlay;
      try
      {
        Bitmap bitmap = new Bitmap(typeof (LayoutControlDragDropService).Assembly.GetManifestResourceStream("Telerik.WinControls.UI.RadLayoutControl.Editing.Remove.png"));
        this.deleteCursor = new Cursor(bitmap.GetHicon());
        bitmap.Dispose();
      }
      catch
      {
      }
    }

    protected override void HandleMouseMove(Point mousePos)
    {
      base.HandleMouseMove(mousePos);
      this.UpdateDragHintLocation(mousePos);
    }

    protected override void PerformStart()
    {
      base.PerformStart();
      this.draggedItem = this.Context as DraggableLayoutControlItem;
      if (this.draggedItem != null)
        return;
      this.Stop(false);
    }

    protected override void PerformStop()
    {
      base.PerformStop();
      this.HideDragHint();
    }

    protected override void OnPreviewDragOver(RadDragOverEventArgs e)
    {
      base.OnPreviewDragOver(e);
      if (e.HitTarget is BaseListViewElement || e.HitTarget is BaseListViewVisualItem)
      {
        RadElement hitTarget = e.HitTarget as RadElement;
        e.CanDrop = this.CanDropOnListView(hitTarget);
      }
      if (e.CanDrop)
        return;
      this.HideDragHint();
    }

    protected override void OnPreviewDragDrop(RadDropEventArgs e)
    {
      DraggableLayoutControlItem hitTarget = e.HitTarget as DraggableLayoutControlItem;
      base.OnPreviewDragDrop(e);
      if (e.Handled)
        return;
      hitTarget?.AssociatedItem.FindAncestor<LayoutControlContainerElement>().LayoutTree.HandleDrop(hitTarget, (LayoutControlItemBase) this.draggedItem, hitTarget.PointToControl(e.DropLocation));
      if ((this.DropTarget is BaseListViewElement || this.DropTarget is BaseListViewVisualItem) && this.CanDropOnListView(this.DropTarget as RadElement))
        this.owner.HideItem(this.draggedItem.AssociatedItem);
      ControlTraceMonitor.TrackAtomicFeature(this.owner.Parent is RadDataLayout ? (RadControl) this.owner.Parent : (RadControl) this.owner, "LayoutModified", (object) "DragDrop");
    }

    protected override bool IsDropTargetValid(ISupportDrop dropTarget)
    {
      DraggableLayoutControlItem layoutControlItem = dropTarget as DraggableLayoutControlItem;
      if (this.draggedItem.ControlBoundingRectangle.Contains(this.draggedItem.ElementTree.Control.PointToClient(Control.MousePosition)))
        return false;
      if (layoutControlItem != null)
        return layoutControlItem != this.draggedItem;
      return base.IsDropTargetValid(dropTarget);
    }

    protected virtual void UpdateDragHintLocation(Point mousePosition)
    {
      if (!this.CanCommit)
      {
        this.overlay.SetPreviewRectangle(Rectangle.Empty);
      }
      else
      {
        DraggableLayoutControlItem dropTarget = this.DropTarget as DraggableLayoutControlItem;
        if (dropTarget != null)
        {
          LayoutControlContainerElement ancestor = dropTarget.AssociatedItem.FindAncestor<LayoutControlContainerElement>();
          Point client = this.overlay.PointToClient(mousePosition);
          LayoutControlDropTargetInfo dropTargetNode = ancestor.LayoutTree.GetDropTargetNode(dropTarget, client, this.draggedItem.AssociatedItem.GetType());
          if (dropTargetNode != null)
          {
            this.ValidCursor = (Cursor) null;
            dropTargetNode.TargetBounds.Offset(ancestor.ControlBoundingRectangle.Location);
            this.overlay.SetPreviewRectangle(dropTargetNode.TargetBounds);
            return;
          }
        }
        else if ((this.DropTarget is BaseListViewElement || this.DropTarget is BaseListViewVisualItem) && this.CanDropOnListView(this.DropTarget as RadElement))
        {
          this.overlay.SetPreviewRectangle(Rectangle.Empty);
          this.ValidCursor = this.deleteCursor;
          return;
        }
        this.ValidCursor = (Cursor) null;
        this.overlay.SetPreviewRectangle(Rectangle.Empty);
      }
    }

    private bool CanDropOnListView(RadElement hitElement)
    {
      if (hitElement.ElementTree.Control.FindForm() != this.owner.CustomizeDialog || !this.owner.AllowHiddenItems)
        return hitElement.ElementTree.Control.FindForm() is ILayoutControlDesignTimeEditor;
      return true;
    }

    protected virtual void HideDragHint()
    {
      this.overlay.SetPreviewRectangle(Rectangle.Empty);
    }
  }
}
