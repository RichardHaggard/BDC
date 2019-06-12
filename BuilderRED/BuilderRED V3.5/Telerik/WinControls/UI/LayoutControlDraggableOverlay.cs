// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlDraggableOverlay
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class LayoutControlDraggableOverlay : RadControl
  {
    private DraggableLayoutControlOverlayElement overlayElement;
    private RadLayoutControl owner;
    private LayoutControlDragDropService dragDropService;
    private RadDropDownMenu contextMenu;
    private RadMenuItem hideMenuItem;
    private bool canStartDrag;
    private Point initialMousePos;
    private LayoutControlResizingBehavior capturedBehavior;
    private Rectangle previewRectangle;
    private Bitmap cachedSnapshot;

    public LayoutControlDraggableOverlay(RadLayoutControl owner)
    {
      this.owner = owner;
      this.dragDropService = new LayoutControlDragDropService(this.owner, this);
      this.dragDropService.Stopped += new EventHandler(this.dragDropService_Stopped);
      this.contextMenu = new RadDropDownMenu();
      this.hideMenuItem = new RadMenuItem(LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuHideItemText"));
      this.hideMenuItem.Click += new EventHandler(this.hideMenuItem_Click);
      this.contextMenu.Items.Add((RadItem) this.hideMenuItem);
      LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.UpdateLocalizableStrings);
    }

    private void UpdateLocalizableStrings(object sender, EventArgs e)
    {
      this.hideMenuItem.Text = LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuHideItemText");
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.overlayElement = new DraggableLayoutControlOverlayElement();
      this.overlayElement.AutoSize = false;
      this.overlayElement.ShouldHandleMouseInput = true;
      parent.Children.Add((RadElement) this.overlayElement);
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.contextMenu.Dispose();
      LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.UpdateLocalizableStrings);
    }

    public RadLayoutControl Owner
    {
      get
      {
        return this.owner;
      }
    }

    public DraggableLayoutControlOverlayElement OverlayElement
    {
      get
      {
        return this.overlayElement;
      }
    }

    public LayoutControlDragDropService DragDropService
    {
      get
      {
        return this.dragDropService;
      }
    }

    internal Bitmap CachedSnapshot
    {
      get
      {
        this.owner.ShowItemBorders = true;
        if (this.cachedSnapshot == null)
        {
          Color backColor = this.owner.RootElement.BackColor;
          if (backColor == Color.Transparent)
          {
            backColor = this.owner.ElementTree.Control.BackColor;
            for (Control parent = this.ElementTree.Control.Parent; backColor == Color.Transparent && parent != null; parent = parent.Parent)
              backColor = parent.BackColor;
          }
          using (SolidBrush solidBrush = new SolidBrush(backColor))
            this.cachedSnapshot = this.owner.RootElement.GetAsBitmap((Brush) solidBrush, this.owner.RootElement.AngleTransform, this.owner.RootElement.ScaleTransform);
        }
        this.owner.ShowItemBorders = false;
        return this.cachedSnapshot;
      }
    }

    public ReadOnlyCollection<DraggableLayoutControlItem> SelectedItems
    {
      get
      {
        List<DraggableLayoutControlItem> layoutControlItemList = new List<DraggableLayoutControlItem>();
        foreach (DraggableLayoutControlItem layoutControlItem in this.GetItems())
        {
          if (layoutControlItem.IsSelected)
            layoutControlItemList.Add(layoutControlItem);
        }
        return layoutControlItemList.AsReadOnly();
      }
    }

    public void UpdatePreview()
    {
      this.UpdatePreview(true);
    }

    public void UpdatePreview(bool refreshElements)
    {
      if (this.cachedSnapshot != null)
      {
        this.cachedSnapshot.Dispose();
        this.cachedSnapshot = (Bitmap) null;
      }
      this.overlayElement.Size = this.owner.ContainerElement.BoundingRectangle.Size;
      this.overlayElement.PositionOffset = this.owner.ContainerElement.PositionOffset;
      this.owner.RootElement.InvalidateMeasure(true);
      this.owner.RootElement.UpdateLayout();
      this.owner.Refresh();
      if (refreshElements)
      {
        this.overlayElement.SuspendLayout();
        this.overlayElement.Items.Clear();
        foreach (LayoutControlItemBase layoutControlItemBase in (RadItemCollection) this.owner.Items)
          this.overlayElement.Items.Add((RadItem) new DraggableLayoutControlItem()
          {
            AssociatedItem = layoutControlItemBase
          });
        this.overlayElement.ResumeLayout(true);
        this.overlayElement.UpdateLayout();
        this.Refresh();
      }
      else
      {
        this.overlayElement.SuspendLayout(true);
        foreach (DraggableLayoutControlItem layoutControlItem in (RadItemCollection) this.overlayElement.Items)
          layoutControlItem.InvalidateSnapshot();
        this.overlayElement.ResumeLayout(true, true);
        this.overlayElement.UpdateLayout();
        this.Refresh();
      }
    }

    public void SelectLayoutItem(LayoutControlItemBase item)
    {
      foreach (DraggableLayoutControlItem layoutControlItem in this.GetItems())
        layoutControlItem.IsSelected = item == layoutControlItem.AssociatedItem;
    }

    public void SelectItem(DraggableLayoutControlItem item, bool extend)
    {
      if (extend)
      {
        DraggableLayoutControlItem layoutControlItem = item;
        layoutControlItem.IsSelected = !layoutControlItem.IsSelected;
      }
      else
      {
        foreach (DraggableLayoutControlItem layoutControlItem in this.GetItems())
          layoutControlItem.IsSelected = item == layoutControlItem;
      }
    }

    public IEnumerable<DraggableLayoutControlItem> GetItems()
    {
      return this.GetItems((DraggableLayoutControlOverlayElement) null);
    }

    public IEnumerable<DraggableLayoutControlItem> GetItems(
      DraggableLayoutControlOverlayElement overlayElement)
    {
      overlayElement = overlayElement ?? this.overlayElement;
      foreach (DraggableLayoutControlItem layoutControlItem1 in (RadItemCollection) overlayElement.Items)
      {
        yield return layoutControlItem1;
        if (layoutControlItem1.GroupContainer != null)
        {
          foreach (DraggableLayoutControlItem layoutControlItem2 in this.GetItems(layoutControlItem1.GroupContainer))
            yield return layoutControlItem2;
        }
        if (layoutControlItem1.ChildItem != null)
        {
          yield return layoutControlItem1.ChildItem;
          foreach (DraggableLayoutControlItem layoutControlItem2 in this.GetItems(layoutControlItem1.ChildItem.GroupContainer))
            yield return layoutControlItem2;
        }
      }
    }

    public void SetPreviewRectangle(Rectangle rectangle)
    {
      if (!(this.previewRectangle != rectangle))
        return;
      this.previewRectangle = rectangle;
      this.Invalidate();
    }

    public DraggableLayoutControlItem FindDraggableItem(
      LayoutControlItemBase item)
    {
      foreach (DraggableLayoutControlItem layoutControlItem in this.GetItems())
      {
        if (item == layoutControlItem.AssociatedItem)
          return layoutControlItem;
      }
      return (DraggableLayoutControlItem) null;
    }

    public void StartDrag(LayoutControlItemBase item)
    {
      DraggableLayoutControlItem draggableItem = this.FindDraggableItem(item);
      if (draggableItem == null)
        return;
      this.dragDropService.Start((object) draggableItem);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button == MouseButtons.Left)
      {
        Cursor cursorAtPoint = this.owner.GetCursorAtPoint(e.Location);
        LayoutControlResizingBehavior behaviorAtPoint = this.owner.GetBehaviorAtPoint(e.Location);
        if (cursorAtPoint != Cursors.Default)
        {
          Orientation resizeType = cursorAtPoint == Cursors.SizeNS ? Orientation.Vertical : Orientation.Horizontal;
          if (behaviorAtPoint.BeginResize(e.Location, resizeType))
          {
            this.capturedBehavior = behaviorAtPoint;
            this.capturedBehavior.Resized += new EventHandler(this.ResizingBehavior_Resized);
            this.Capture = true;
          }
          this.canStartDrag = false;
        }
        else
        {
          DraggableLayoutControlItem elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location) as DraggableLayoutControlItem;
          if (elementAtPoint == null)
            return;
          if (elementAtPoint.AssociatedItem is LayoutControlTabbedGroup)
          {
            LayoutControlTabbedGroup associatedItem = elementAtPoint.AssociatedItem as LayoutControlTabbedGroup;
            foreach (LayoutControlTabStripItem controlTabStripItem in (IEnumerable<RadPageViewItem>) associatedItem.TabStrip.Items)
            {
              if (controlTabStripItem.ControlBoundingRectangle.Contains(e.Location))
              {
                associatedItem.TabStrip.SelectedItem = (RadPageViewItem) controlTabStripItem;
                this.canStartDrag = true;
                this.initialMousePos = e.Location;
                this.UpdatePreview();
                return;
              }
            }
          }
          this.SelectItem(elementAtPoint, Control.ModifierKeys == Keys.Control);
          this.canStartDrag = true;
          this.initialMousePos = e.Location;
        }
      }
      else
      {
        if (e.Button != MouseButtons.Right || this.SelectedItems.Count > 1)
          return;
        this.SelectItem(this.ElementTree.GetElementAtPoint(e.Location) as DraggableLayoutControlItem, false);
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.capturedBehavior != null)
      {
        this.capturedBehavior.Resize(e.Location);
      }
      else
      {
        this.Cursor = this.owner.GetCursorAtPoint(e.Location);
        if (e.Button != MouseButtons.Left || !this.canStartDrag || Math.Abs(this.initialMousePos.X - e.X) <= SystemInformation.DragSize.Width && Math.Abs(this.initialMousePos.Y - e.Y) <= SystemInformation.DragSize.Height)
          return;
        DraggableLayoutControlItem layoutControlItem = this.ElementTree.GetElementAtPoint(e.Location) as DraggableLayoutControlItem;
        if (layoutControlItem.AssociatedItem is LayoutControlTabbedGroup && ((LayoutControlTabbedGroup) layoutControlItem.AssociatedItem).TabStrip.SelectedTab != null && ((LayoutControlTabbedGroup) layoutControlItem.AssociatedItem).TabStrip.SelectedTab.ControlBoundingRectangle.Contains(e.Location))
          layoutControlItem = layoutControlItem.ChildItem;
        this.dragDropService.Start((object) layoutControlItem);
        this.canStartDrag = false;
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.canStartDrag = false;
      this.Capture = false;
      if (this.capturedBehavior != null)
      {
        this.capturedBehavior.Resized -= new EventHandler(this.ResizingBehavior_Resized);
        this.capturedBehavior.EndResize();
        this.capturedBehavior = (LayoutControlResizingBehavior) null;
      }
      if (e.Button != MouseButtons.Right || !this.owner.AllowHiddenItems)
        return;
      this.contextMenu.ThemeName = this.ThemeName;
      this.contextMenu.Show((Control) this, e.Location);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      using (Brush brush = (Brush) new SolidBrush(this.owner.ContainerElement.PreviewRectangleFill))
      {
        using (Pen pen = new Pen(this.owner.ContainerElement.PreviewRectangleStroke))
        {
          e.Graphics.FillRectangle(brush, this.previewRectangle);
          e.Graphics.DrawRectangle(pen, this.previewRectangle);
        }
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData != Keys.Delete)
        return base.ProcessCmdKey(ref msg, keyData);
      bool flag = false;
      foreach (DraggableLayoutControlItem selectedItem in this.SelectedItems)
      {
        if (selectedItem.AssociatedItem.AllowDelete)
        {
          this.owner.RemoveItem(selectedItem.AssociatedItem);
          selectedItem.AssociatedItem.Dispose();
          flag = true;
        }
      }
      if (flag)
        this.UpdatePreview();
      return true;
    }

    private void hideMenuItem_Click(object sender, EventArgs e)
    {
      foreach (DraggableLayoutControlItem selectedItem in this.SelectedItems)
        this.owner.HideItem(selectedItem.AssociatedItem);
      this.UpdatePreview();
    }

    private void dragDropService_Stopped(object sender, EventArgs e)
    {
      this.UpdatePreview();
      this.canStartDrag = false;
    }

    private void ResizingBehavior_Resized(object sender, EventArgs e)
    {
      this.UpdatePreview(false);
    }
  }
}
