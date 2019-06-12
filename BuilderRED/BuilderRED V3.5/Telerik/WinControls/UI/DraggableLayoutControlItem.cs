// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DraggableLayoutControlItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class DraggableLayoutControlItem : LayoutControlItemBase
  {
    private Color selectionFillColor = Color.FromArgb(128, 191, 219, (int) byte.MaxValue);
    private Color selectionStrokeColor = Color.FromArgb((int) byte.MaxValue, 120, 148, 186);
    private LayoutControlItemBase associatedItem;
    private bool isSelected;
    private DraggableLayoutControlOverlayElement groupContainer;
    private DraggableLayoutControlItem childItem;
    private Bitmap cachedElement;

    public DraggableLayoutControlItem()
    {
    }

    public DraggableLayoutControlItem(LayoutControlItemBase associatedItem)
    {
      this.AssociatedItem = associatedItem;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.AllowDrag = true;
      this.AllowDrop = true;
    }

    public Color SelectionFillColor
    {
      get
      {
        return this.selectionFillColor;
      }
      set
      {
        this.selectionFillColor = value;
      }
    }

    public Color SelectionStrokeColor
    {
      get
      {
        return this.selectionStrokeColor;
      }
      set
      {
        this.selectionStrokeColor = value;
      }
    }

    public LayoutControlItemBase AssociatedItem
    {
      get
      {
        return this.associatedItem;
      }
      set
      {
        this.associatedItem = value;
        if (this.associatedItem != null)
        {
          this.Bounds = this.associatedItem.Bounds;
          this.Name = this.associatedItem.Name;
        }
        if (this.associatedItem is LayoutControlGroupItem)
          this.InitializeChildContainer((LayoutControlGroupItem) this.associatedItem);
        if (!(this.associatedItem is LayoutControlTabbedGroup))
          return;
        this.InitializeChildItem((LayoutControlTabbedGroup) this.associatedItem);
      }
    }

    public DraggableLayoutControlOverlayElement GroupContainer
    {
      get
      {
        return this.groupContainer;
      }
    }

    public DraggableLayoutControlItem ChildItem
    {
      get
      {
        return this.childItem;
      }
    }

    public bool IsSelected
    {
      get
      {
        return this.isSelected;
      }
      set
      {
        if (this.isSelected == value)
          return;
        this.isSelected = value;
        this.Invalidate();
      }
    }

    private void InitializeChildItem(LayoutControlTabbedGroup tabbedItem)
    {
      if (tabbedItem.ItemGroups.Count == 0 || tabbedItem.SelectedGroup == null)
        return;
      this.childItem = new DraggableLayoutControlItem()
      {
        AssociatedItem = (LayoutControlItemBase) tabbedItem.SelectedGroup
      };
      this.Children.Add((RadElement) this.childItem);
      tabbedItem.TabStrip.UpdateLayout();
      Rectangle boundingRectangle = tabbedItem.TabStrip.ContentArea.BoundingRectangle;
      boundingRectangle.Offset(tabbedItem.TabStrip.ContentArea.Padding.Left, tabbedItem.TabStrip.ContentArea.Padding.Right);
      boundingRectangle.Size = new Size(boundingRectangle.Width - tabbedItem.TabStrip.ContentArea.Padding.Horizontal, boundingRectangle.Height - tabbedItem.TabStrip.ContentArea.Padding.Vertical);
      this.childItem.Bounds = boundingRectangle;
    }

    private void InitializeChildContainer(LayoutControlGroupItem groupItem)
    {
      this.groupContainer = new DraggableLayoutControlOverlayElement();
      if (groupItem.ContainerElement.Visibility != ElementVisibility.Visible)
        return;
      this.groupContainer.AutoSize = false;
      this.groupContainer.Bounds = groupItem.ContainerElement.BoundingRectangle;
      this.Children.Add((RadElement) this.groupContainer);
      foreach (LayoutControlItemBase layoutControlItemBase in groupItem.Items)
        this.groupContainer.Items.Add((RadItem) new DraggableLayoutControlItem()
        {
          AssociatedItem = layoutControlItemBase
        });
    }

    public void InvalidateSnapshot()
    {
      if (this.associatedItem.AutoSize || this.associatedItem.Size != this.Size)
      {
        if (this.cachedElement != null)
          this.cachedElement.Dispose();
        this.cachedElement = (Bitmap) null;
      }
      if (!this.associatedItem.AutoSize)
        this.Bounds = this.associatedItem.Bounds;
      if (this.groupContainer != null)
      {
        LayoutControlGroupItem associatedItem = this.AssociatedItem as LayoutControlGroupItem;
        if (associatedItem != null)
          this.groupContainer.Bounds = associatedItem.ContainerElement.BoundingRectangle;
        foreach (DraggableLayoutControlItem layoutControlItem in (RadItemCollection) this.groupContainer.Items)
          layoutControlItem.InvalidateSnapshot();
      }
      if (this.childItem == null)
        return;
      LayoutControlTabbedGroup associatedItem1 = this.AssociatedItem as LayoutControlTabbedGroup;
      if (associatedItem1 != null)
      {
        Rectangle boundingRectangle = associatedItem1.TabStrip.ContentArea.BoundingRectangle;
        boundingRectangle.Offset(associatedItem1.TabStrip.ContentArea.Padding.Left, associatedItem1.TabStrip.ContentArea.Padding.Right);
        boundingRectangle.Size = new Size(boundingRectangle.Width - associatedItem1.TabStrip.ContentArea.Padding.Horizontal, boundingRectangle.Height - associatedItem1.TabStrip.ContentArea.Padding.Vertical);
        this.childItem.Bounds = boundingRectangle;
      }
      this.childItem.InvalidateSnapshot();
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      if (this.associatedItem == null)
        return;
      if (this.cachedElement == null)
      {
        this.cachedElement = new Bitmap(this.Size.Width + 1, this.Size.Height + 1);
        using (Graphics graphics1 = Graphics.FromImage((Image) this.cachedElement))
        {
          using (Bitmap asBitmapSnapshot = DraggableLayoutControlItem.GetAsBitmapSnapshot(this, Point.Empty, new Size(this.Size.Width + 1, this.Size.Height + 1)))
          {
            if (asBitmapSnapshot != null)
              graphics1.DrawImage((Image) asBitmapSnapshot, 0, 0);
          }
          if (this.associatedItem is LayoutControlItem)
          {
            Control associatedControl = ((LayoutControlItem) this.associatedItem).AssociatedControl;
            if (associatedControl != null)
            {
              if (associatedControl.Width > 0)
              {
                if (associatedControl.Height > 0)
                {
                  using (Bitmap bitmap = new Bitmap(associatedControl.Width, associatedControl.Height))
                  {
                    associatedControl.DrawToBitmap(bitmap, new Rectangle(Point.Empty, associatedControl.Size));
                    Point point = this.associatedItem.PointFromControl(associatedControl.Location);
                    graphics1.DrawImage((Image) bitmap, point.X, point.Y);
                  }
                }
              }
            }
          }
        }
      }
      graphics.DrawBitmap((Image) this.cachedElement, 0, 0);
      if (this.IsSelected)
        return;
      graphics.FillRectangle(new Rectangle(0, 0, this.Size.Width + 1, this.Size.Height + 1), Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
    }

    private static Bitmap GetAsBitmapSnapshot(
      DraggableLayoutControlItem draggableItem,
      Point offset,
      Size size)
    {
      RadElement associatedItem = (RadElement) draggableItem.AssociatedItem;
      if (associatedItem.ElementState != ElementState.Loaded)
        return (Bitmap) null;
      if (size.Width <= 0 || size.Height <= 0)
        return (Bitmap) null;
      LayoutControlDraggableOverlay control = draggableItem.ElementTree.Control as LayoutControlDraggableOverlay;
      if (control == null)
        return (Bitmap) null;
      Bitmap cachedSnapshot = control.CachedSnapshot;
      Bitmap bitmap = new Bitmap(size.Width, size.Height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        Rectangle boundingRectangle = associatedItem.ControlBoundingRectangle;
        boundingRectangle.Offset(offset);
        boundingRectangle.Size = size;
        graphics.DrawImage((Image) cachedSnapshot, new Rectangle(Point.Empty, size), boundingRectangle, GraphicsUnit.Pixel);
      }
      return bitmap;
    }

    protected override void PostPaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale)
    {
      base.PostPaintChildren(graphics, clipRectange, angle, scale);
      if (!this.IsSelected)
        return;
      graphics.FillRectangle(new Rectangle(0, 0, this.Size.Width + 1, this.Size.Height + 1), this.selectionFillColor);
      graphics.DrawRectangle((RectangleF) new Rectangle(0, 0, this.Size.Width, this.Size.Height), this.selectionStrokeColor, PenAlignment.Inset, 1f, DashStyle.Dash);
    }

    protected override bool CanDragCore(Point dragStartPoint)
    {
      return this.ElementState == ElementState.Loaded;
    }

    protected override bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      return true;
    }
  }
}
