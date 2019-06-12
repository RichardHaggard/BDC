// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListControlDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ListControlDragDropService : RadDragDropService
  {
    private RadListElement owner;
    private RadLayeredWindow dropHintWindow;
    private RadListDataItem draggedItem;

    public ListControlDragDropService(RadListElement owner)
    {
      this.owner = owner;
    }

    protected virtual void PrepareDragHint(RadListElement listElement)
    {
      Size size = Size.Empty;
      size = new Size(this.GetDropHintWidth(listElement), 1);
      Bitmap bitmap = new Bitmap(size.Width, size.Height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        using (SolidBrush solidBrush = new SolidBrush(Color.Red))
          graphics.FillRectangle((Brush) solidBrush, new Rectangle(Point.Empty, size));
      }
      if (bitmap == null)
        return;
      this.dropHintWindow = new RadLayeredWindow();
      this.dropHintWindow.BackgroundImage = (Image) bitmap;
    }

    private int GetDropHintWidth(RadListElement listElement)
    {
      int num = listElement.ControlBoundingRectangle.Width - LightVisualElement.GetBorderThickness((LightVisualElement) listElement, true).Horizontal;
      if (listElement.VScrollBar.Visibility == ElementVisibility.Visible)
        num -= listElement.VScrollBar.Size.Width;
      return num;
    }

    protected override bool CanStart(object context)
    {
      if (base.CanStart(context))
        return this.owner.AllowDrag;
      return false;
    }

    protected override void OnPreviewDragHint(PreviewDragHintEventArgs e)
    {
      base.OnPreviewDragHint(e);
    }

    protected override void OnPreviewDragOver(RadDragOverEventArgs e)
    {
      RadListVisualItem hitTarget = e.HitTarget as RadListVisualItem;
      RadListElement radListElement = e.HitTarget as RadListElement;
      if (hitTarget != null)
        radListElement = hitTarget.Data.Owner;
      e.CanDrop = radListElement.AllowDragDrop;
      base.OnPreviewDragOver(e);
    }

    protected override void OnPreviewDragDrop(RadDropEventArgs e)
    {
      base.OnPreviewDragDrop(e);
      if (e.Handled || this.owner.DataSource != null)
        return;
      RadListVisualItem targetElement = e.HitTarget as RadListVisualItem;
      RadListElement targetList = e.HitTarget as RadListElement;
      if (targetElement != null)
        targetList = targetElement.Data.Owner;
      else
        targetElement = targetList.ElementTree.GetElementAtPoint(e.DropLocation) as RadListVisualItem;
      if (this.draggedItem == null || this.draggedItem.DataBoundItem != null || targetList.DataSource != null)
        return;
      this.OnPreviewDragDropCore(targetList, targetElement);
    }

    protected virtual void OnPreviewDragDropCore(
      RadListElement targetList,
      RadListVisualItem targetElement)
    {
      int num = targetList.Items.Count - 1;
      if (targetElement != null)
        num = targetList.Items.IndexOf(targetElement.Data);
      if (this.draggedItem.Owner != targetList)
        ++num;
      RadListElement owner = this.draggedItem.Owner;
      IList<RadListDataItem> radListDataItemList = (IList<RadListDataItem>) new List<RadListDataItem>(owner.SelectedItems.Count);
      foreach (RadListDataItem selectedItem in (IEnumerable<RadListDataItem>) owner.SelectedItems)
        radListDataItemList.Add(selectedItem);
      owner.BeginUpdate();
      foreach (RadListDataItem radListDataItem in (IEnumerable<RadListDataItem>) radListDataItemList)
      {
        radListDataItem.Selected = radListDataItem.Active = false;
        owner.Items.Remove(radListDataItem);
      }
      owner.EndUpdate();
      targetList.BeginUpdate();
      foreach (RadListDataItem radListDataItem in (IEnumerable<RadListDataItem>) radListDataItemList)
      {
        targetList.Items.Insert(num++, radListDataItem);
        radListDataItem.Selected = radListDataItem.Active = true;
      }
      targetList.EndUpdate();
    }

    protected override void PerformStart()
    {
      base.PerformStart();
      RadListVisualItem context = this.Context as RadListVisualItem;
      if (context == null)
        return;
      this.draggedItem = context.Data;
    }

    protected override void PerformStop()
    {
      base.PerformStop();
      this.DisposeHint();
      this.draggedItem = (RadListDataItem) null;
    }

    protected override void HandleMouseMove(Point mousePosition)
    {
      base.HandleMouseMove(mousePosition);
      if (this.DropTarget == null)
        return;
      RadListElement dropTarget = this.DropTarget as RadListElement;
      if (dropTarget == null || !this.CanShowDropHint(mousePosition) || !this.CanCommit)
      {
        this.DisposeHint();
      }
      else
      {
        if (this.dropHintWindow == null)
          this.PrepareDragHint(dropTarget);
        if (this.dropHintWindow == null)
          return;
        this.UpdateHintPosition(mousePosition);
      }
    }

    protected virtual void DisposeHint()
    {
      if (this.dropHintWindow == null)
        return;
      this.dropHintWindow.Dispose();
      this.dropHintWindow = (RadLayeredWindow) null;
    }

    protected virtual void UpdateHintPosition(Point mousePosition)
    {
      RadListElement dropTarget = this.DropTarget as RadListElement;
      RadListVisualItem elementAtPoint = dropTarget.ElementTree.GetElementAtPoint(dropTarget.ElementTree.Control.PointToClient(mousePosition)) as RadListVisualItem;
      if (elementAtPoint == null)
        return;
      Rectangle screen = dropTarget.ElementTree.Control.RectangleToScreen(elementAtPoint.ControlBoundingRectangle);
      Padding empty = Padding.Empty;
      int num1 = 1;
      int num2 = dropTarget.PointFromScreen(mousePosition).Y <= dropTarget.Size.Height / 2 ? screen.Y : screen.Bottom;
      Point screenLocation = new Point(screen.X - empty.Left, num2 - num1 / 2);
      this.dropHintWindow.Width = screen.Width;
      this.dropHintWindow.ShowWindow(screenLocation);
    }

    protected virtual bool CanShowDropHint(Point mousePosition)
    {
      RadListElement dropTarget = this.DropTarget as RadListElement;
      if (dropTarget == null)
        return false;
      Point point = dropTarget.PointFromScreen(mousePosition);
      int num = dropTarget.ItemHeight / 3;
      if (point.Y >= num)
        return point.Y > num * 2;
      return true;
    }
  }
}
