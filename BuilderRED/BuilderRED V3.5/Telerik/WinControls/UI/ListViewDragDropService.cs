// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ListViewDragDropService : RadDragDropService
  {
    private ListViewDataItem draggedItem;
    private RadListViewElement owner;
    private RadLayeredWindow dragHintWindow;
    private List<ListViewDataItem> draggedItems;

    public ListViewDragDropService(RadListViewElement owner)
    {
      this.owner = owner;
    }

    public List<ListViewDataItem> DraggedItems
    {
      get
      {
        return this.draggedItems;
      }
    }

    public ListViewDataItem DraggedItem
    {
      get
      {
        return this.draggedItem;
      }
    }

    public RadListViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    protected override void HandleMouseMove(Point mousePos)
    {
      base.HandleMouseMove(mousePos);
      if (this.dragHintWindow == null && this.CanCommit)
        this.PrepareDragHint();
      this.UpdateDragHintLocation(mousePos);
      Point point = this.owner.PointFromScreen(mousePos);
      if (point.Y < 0 && this.Owner.ViewElement.Orientation == Orientation.Vertical || point.X < 0 && this.Owner.ViewElement.Orientation == Orientation.Horizontal)
      {
        this.owner.ViewElement.Scroller.Scrollbar.PerformSmallDecrement(1);
      }
      else
      {
        if ((point.Y <= this.owner.Size.Height || this.Owner.ViewElement.Orientation != Orientation.Vertical) && (point.X <= this.owner.Size.Width || this.Owner.ViewElement.Orientation != Orientation.Horizontal))
          return;
        this.owner.ViewElement.Scroller.Scrollbar.PerformSmallIncrement(1);
      }
    }

    protected override bool CanStart(object context)
    {
      RadElement radElement = context as RadElement;
      if (radElement != null && !radElement.Enabled || !this.owner.AllowDragDrop)
        return false;
      return base.CanStart(context);
    }

    protected override void PerformStart()
    {
      base.PerformStart();
      this.draggedItem = (this.Context as BaseListViewVisualItem).Data;
      if (this.draggedItem == null || !this.owner.AllowDragDrop)
      {
        this.Stop(false);
      }
      else
      {
        if (!this.draggedItem.Selected)
          this.owner.ViewElement.ProcessSelection(this.draggedItem, Keys.None, true);
        if (this.draggedItem.Owner.MultiSelect && this.draggedItem.Owner.SelectedItems.Count > 1)
        {
          this.draggedItems = new List<ListViewDataItem>();
          foreach (ListViewDataItem selectedItem in (ReadOnlyCollection<ListViewDataItem>) this.draggedItem.Owner.SelectedItems)
            this.draggedItems.Add(selectedItem);
        }
        this.PrepareDragHint();
      }
    }

    protected override void PerformStop()
    {
      base.PerformStop();
      this.draggedItem = (ListViewDataItem) null;
      this.draggedItems = (List<ListViewDataItem>) null;
      this.DisposeDragHint();
    }

    protected override void OnPreviewDragOver(RadDragOverEventArgs e)
    {
      base.OnPreviewDragOver(e);
      if (e.CanDrop)
        return;
      this.DisposeDragHint();
    }

    protected override void OnPreviewDragDrop(RadDropEventArgs e)
    {
      base.OnPreviewDragDrop(e);
      if (e.Handled || this.owner.ListSource.IsDataBound)
        return;
      if ((this.owner.EnableGrouping || this.owner.EnableCustomGrouping) && this.owner.ShowGroups || (this.owner.EnableSorting || this.owner.ListSource.IsDataBound))
      {
        int num = (int) RadMessageBox.Show("DragDrop is not supported in grouped, sorted or data-bound mode. \nPlease handle the ListViewElement.DragDropService.PreviewDragDrop event manually \nand set e.Handled = true  or just set the AllowDragDrop property to false to disable this message.", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
      }
      else
      {
        RadElement hitTarget1 = e.HitTarget as RadElement;
        BaseListViewVisualItem listViewVisualItem = e.HitTarget as BaseListViewVisualItem;
        BaseListViewElement hitTarget2 = e.HitTarget as BaseListViewElement;
        if (hitTarget1 != null)
        {
          Point control = hitTarget1.PointToControl(e.DropLocation);
          if (listViewVisualItem == null)
            listViewVisualItem = this.owner.ViewElement.GetVisualItemAt(control);
        }
        if (listViewVisualItem != null && listViewVisualItem.Data.Owner != this.owner || hitTarget2 != null && hitTarget2.Owner != this.owner)
          this.HandleDropBetweenListViews(e);
        else
          this.HandleDragDrop(e);
        this.owner.InvalidateMeasure(true);
      }
    }

    protected override bool IsDropTargetValid(ISupportDrop dropTarget)
    {
      BaseListViewVisualItem listViewVisualItem = dropTarget as BaseListViewVisualItem;
      if (listViewVisualItem != null)
        return listViewVisualItem.Data != this.draggedItem;
      if (dropTarget is BaseListViewElement)
        return true;
      return base.IsDropTargetValid(dropTarget);
    }

    protected virtual void HandleDropBetweenListViews(RadDropEventArgs e)
    {
      RadElement hitTarget = e.HitTarget as RadElement;
      BaseListViewVisualItem listViewVisualItem = e.HitTarget as BaseListViewVisualItem;
      BaseListViewElement baseListViewElement = e.HitTarget as BaseListViewElement;
      if (listViewVisualItem != null && baseListViewElement == null)
        baseListViewElement = listViewVisualItem.Data.Owner.ViewElement;
      ListViewDataItem data;
      if (hitTarget != null)
      {
        Point control = hitTarget.PointToControl(e.DropLocation);
        if (listViewVisualItem == null)
          listViewVisualItem = baseListViewElement.GetVisualItemAt(control);
        data = listViewVisualItem?.Data;
      }
      else
        data = listViewVisualItem?.Data;
      if (data != null)
      {
        data.Owner.ListSource.BeginUpdate();
        int index = data.Owner.ListSource.IndexOf(data);
        if (e.DropLocation.Y > listViewVisualItem.Size.Height / 2)
          ++index;
        if (this.draggedItems != null)
        {
          foreach (ListViewDataItem draggedItem in this.draggedItems)
          {
            this.owner.ListSource.Remove(draggedItem);
            data.Owner.ListSource.Insert(index, draggedItem);
            ++index;
          }
        }
        else if (this.draggedItem != null)
        {
          this.owner.ListSource.Remove(this.draggedItem);
          data.Owner.ListSource.Insert(index, this.draggedItem);
        }
        data.Owner.ListSource.EndUpdate();
        data.Owner.ViewElement.Scroller.UpdateScrollValue();
      }
      else
      {
        if (baseListViewElement == null)
          return;
        baseListViewElement.Owner.ListSource.BeginUpdate();
        if (this.draggedItems != null)
        {
          foreach (ListViewDataItem draggedItem in this.draggedItems)
          {
            this.owner.ListSource.Remove(draggedItem);
            baseListViewElement.Owner.ListSource.Add(draggedItem);
          }
        }
        else if (this.draggedItem != null)
        {
          this.owner.ListSource.Remove(this.draggedItem);
          baseListViewElement.Owner.ListSource.Add(this.draggedItem);
        }
        baseListViewElement.Owner.ListSource.EndUpdate();
        baseListViewElement.Owner.ViewElement.Scroller.UpdateScrollValue();
      }
    }

    protected virtual void HandleDragDrop(RadDropEventArgs e)
    {
      RadElement hitTarget1 = e.HitTarget as RadElement;
      BaseListViewVisualItem targetElement = e.HitTarget as BaseListViewVisualItem;
      BaseListViewElement hitTarget2 = e.HitTarget as BaseListViewElement;
      if (hitTarget1 != null)
        targetElement = this.owner.ViewElement.GetVisualItemAt(hitTarget1.PointToControl(e.DropLocation));
      ListViewDataItem listViewDataItem = targetElement?.Data;
      bool flag = false;
      if (listViewDataItem != null)
      {
        listViewDataItem.Owner.ListSource.BeginUpdate();
        if (this.owner.ViewElement.ShouldDropAfter(targetElement, e.DropLocation))
        {
          ITraverser<ListViewDataItem> enumerator = this.owner.ViewElement.Scroller.Traverser.GetEnumerator() as ITraverser<ListViewDataItem>;
          enumerator.Position = (object) listViewDataItem;
          flag = !enumerator.MoveNext();
          listViewDataItem = enumerator.Current;
        }
        int newIndex = this.owner.ListSource.IndexOf(listViewDataItem);
        if (this.draggedItems != null)
        {
          foreach (ListViewDataItem draggedItem in this.draggedItems)
          {
            int oldIndex = this.owner.ListSource.IndexOf(draggedItem);
            this.owner.ListSource.Move(oldIndex, newIndex);
            if (oldIndex > newIndex)
              ++newIndex;
          }
        }
        else if (this.draggedItem != null)
        {
          int num1 = this.owner.ListSource.IndexOf(this.draggedItem);
          if (flag)
          {
            int num2 = this.owner.ListSource.Count - 1;
          }
          else if (num1 < newIndex)
            --newIndex;
          this.owner.ListSource.Move(this.owner.ListSource.IndexOf(this.draggedItem), newIndex);
        }
        listViewDataItem.Owner.ListSource.EndUpdate();
        listViewDataItem.Owner.ViewElement.Scroller.UpdateScrollValue();
      }
      else
      {
        if (hitTarget2 == null)
          return;
        hitTarget2.Owner.ListSource.BeginUpdate();
        if (this.draggedItems != null)
        {
          foreach (ListViewDataItem draggedItem in this.draggedItems)
            hitTarget2.Owner.ListSource.Move(this.owner.ListSource.IndexOf(draggedItem), this.owner.ListSource.Count - 1);
        }
        else if (this.draggedItem != null)
          hitTarget2.Owner.ListSource.Move(this.owner.ListSource.IndexOf(this.draggedItem), this.owner.ListSource.Count - 1);
        hitTarget2.Owner.ListSource.EndUpdate();
        hitTarget2.Owner.ViewElement.Scroller.UpdateScrollValue();
      }
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
        if (dropTarget == null)
        {
          this.DisposeDragHint();
        }
        else
        {
          Point control = dropTarget.PointToControl(this.DropLocation);
          BaseListViewVisualItem elementAtPoint = dropTarget.ElementTree.GetElementAtPoint<BaseListViewVisualItem>(control);
          if (elementAtPoint == null)
          {
            this.DisposeDragHint();
          }
          else
          {
            Point dragHintLocation = elementAtPoint.Data.ListView.ListViewElement.ViewElement.GetDragHintLocation(elementAtPoint, elementAtPoint.PointFromScreen(mousePosition));
            this.UpdateDragHintSize();
            this.dragHintWindow.ShowWindow(dragHintLocation);
          }
        }
      }
    }

    protected virtual void PrepareDragHint()
    {
      this.dragHintWindow = new RadLayeredWindow();
      if (this.owner.ViewElement.DragHint == null)
        return;
      this.UpdateDragHintSize();
    }

    private void UpdateDragHintSize()
    {
      if (this.dragHintWindow == null)
        return;
      Size dragHintSize = this.GetDragHintSize(this.DropTarget);
      if (dragHintSize.Width <= 0 || dragHintSize.Height <= 0)
        return;
      Bitmap bitmap = new Bitmap(dragHintSize.Width, dragHintSize.Height);
      Graphics g = Graphics.FromImage((Image) bitmap);
      this.owner.ViewElement.DragHint.Paint(g, new RectangleF(PointF.Empty, (SizeF) dragHintSize));
      g.Dispose();
      this.dragHintWindow.BackgroundImage = (Image) bitmap;
    }

    protected virtual Size GetDragHintSize(ISupportDrop dropTarget)
    {
      return this.owner.ViewElement.GetDragHintSize(dropTarget);
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
