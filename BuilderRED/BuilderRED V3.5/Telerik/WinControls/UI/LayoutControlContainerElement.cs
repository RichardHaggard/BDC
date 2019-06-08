// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls.UI
{
  public class LayoutControlContainerElement : LightVisualElement, ILayoutControlItemsHost
  {
    public static readonly RadProperty PreviewRectangleFillProperty = RadProperty.Register(nameof (PreviewRectangleFill), typeof (Color), typeof (LayoutControlContainerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(77, 69, 129, 211)));
    public static readonly RadProperty PreviewRectangleStrokeProperty = RadProperty.Register(nameof (PreviewRectangleStroke), typeof (Color), typeof (LayoutControlContainerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(15, 92, 197)));
    private RadItemOwnerCollection items;
    private LayoutTree tree;
    private LayoutControlResizingBehavior resizingBehavior;

    [VsbBrowsable(true)]
    [Browsable(true)]
    public Color PreviewRectangleFill
    {
      get
      {
        return (Color) this.GetValue(LayoutControlContainerElement.PreviewRectangleFillProperty);
      }
      set
      {
        int num = (int) this.SetValue(LayoutControlContainerElement.PreviewRectangleFillProperty, (object) value);
      }
    }

    [Browsable(true)]
    [VsbBrowsable(true)]
    public Color PreviewRectangleStroke
    {
      get
      {
        return (Color) this.GetValue(LayoutControlContainerElement.PreviewRectangleStrokeProperty);
      }
      set
      {
        int num = (int) this.SetValue(LayoutControlContainerElement.PreviewRectangleStrokeProperty, (object) value);
      }
    }

    public LayoutControlResizingBehavior ResizingBehavior
    {
      get
      {
        return this.resizingBehavior;
      }
    }

    internal LayoutTree LayoutTree
    {
      get
      {
        return this.tree;
      }
    }

    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawBorder = false;
      this.ThemeRole = nameof (LayoutControlContainerElement);
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new System.Type[2]
      {
        typeof (LayoutControlItem),
        typeof (LayoutControlGroupItem)
      };
      this.items.Owner = (RadElement) this;
      this.items.ItemsChanged += new ItemChangedDelegate(this.items_ItemsChanged);
      this.tree = new LayoutTree(this);
      this.resizingBehavior = new LayoutControlResizingBehavior(this);
    }

    public Rectangle GetDropHintPreviewRectangle(
      DraggableLayoutControlItem item,
      Point controlPoint)
    {
      return this.GetDropHintPreviewRectangle(item, controlPoint, (System.Type) null);
    }

    public Rectangle GetDropHintPreviewRectangle(
      DraggableLayoutControlItem item,
      Point controlPoint,
      System.Type dragContext)
    {
      LayoutControlDropTargetInfo dropTargetNode = this.LayoutTree.GetDropTargetNode(item, controlPoint, dragContext);
      dropTargetNode?.TargetBounds.Offset(this.ControlBoundingRectangle.Location);
      if (dropTargetNode == null)
        return Rectangle.Empty;
      return dropTargetNode.TargetBounds;
    }

    public void HandleDrop(
      DraggableLayoutControlItem dropTargetElement,
      LayoutControlItemBase draggedElement,
      Point mousePosition)
    {
      this.LayoutTree.HandleDrop(dropTargetElement, draggedElement, mousePosition);
    }

    public void RebuildLayoutTree()
    {
      this.RebuildLayoutTree(true);
    }

    public void RebuildLayoutTree(bool performLayout)
    {
      RectangleF clientRectangle = this.GetClientRectangle((SizeF) this.Size);
      if (this.Size == Size.Empty)
      {
        if (this.Parent != null && this.Parent.Parent is RadPageViewContentAreaElement)
          clientRectangle = this.GetClientRectangle((SizeF) this.Parent.Parent.Bounds.Size);
        else if (this.Parent is LayoutControlGroupItem)
          clientRectangle = this.GetClientRectangle((SizeF) this.Parent.Bounds.Size);
        else if (this.ElementTree != null)
          clientRectangle = this.GetClientRectangle((SizeF) this.ElementTree.Control.Size);
      }
      List<LayoutControlItemBase> items = new List<LayoutControlItemBase>();
      foreach (RadItem radItem in (RadItemCollection) this.items)
      {
        if (radItem is LayoutControlItemBase)
          items.Add(radItem as LayoutControlItemBase);
      }
      Rectangle bounds = new Rectangle(0, 0, (int) clientRectangle.Width, (int) clientRectangle.Height);
      this.tree.Rebuild(items, bounds);
      foreach (RadItem radItem in (RadItemCollection) this.items)
      {
        if (radItem is LayoutControlGroupItem)
          ((LayoutControlGroupItem) radItem).ContainerElement.RebuildLayoutTree();
        else if (radItem is LayoutControlTabbedGroup && ((LayoutControlTabbedGroup) radItem).SelectedLayoutContainer != null)
          ((LayoutControlTabbedGroup) radItem).SelectedLayoutContainer.RebuildLayoutTree();
      }
      if (!performLayout)
        return;
      this.PerformControlLayout(false);
    }

    public void PerformControlLayout()
    {
      this.PerformControlLayout(true);
      if (this.ElementTree.Control.TopLevelControl == null)
        return;
      this.ElementTree.Control.TopLevelControl.Invalidate(true);
    }

    public void PerformControlLayout(bool recursive)
    {
      if (!this.IsInValidState(true) || this.Size == Size.Empty)
        return;
      if (this.LayoutTree.Root == null)
        this.RebuildLayoutTree(false);
      this.ElementTree.Control.SuspendLayout();
      foreach (RadItem radItem in (RadItemCollection) this.items)
      {
        LayoutControlGroupItem controlGroupItem = radItem as LayoutControlGroupItem;
        LayoutControlTabbedGroup controlTabbedGroup = radItem as LayoutControlTabbedGroup;
        if (controlGroupItem != null && recursive)
          controlGroupItem.ContainerElement.PerformControlLayout(recursive);
        else if (controlTabbedGroup != null && controlTabbedGroup.SelectedLayoutContainer != null && recursive)
          controlTabbedGroup.SelectedLayoutContainer.PerformControlLayout(recursive);
        LayoutControlItem layoutControlItem = radItem as LayoutControlItem;
        if (layoutControlItem != null && layoutControlItem.AssociatedControl != null)
          layoutControlItem.UpdateControlBounds();
      }
      this.ElementTree.Control.ResumeLayout(true);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      availableSize = this.SubtractControlScrollbarsSize(availableSize);
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (!this.ResizingBehavior.IsResizing)
      {
        RectangleF clientRectangle = this.GetClientRectangle(availableSize);
        this.tree.ChangeBounds((RectangleF) new Rectangle(0, 0, (int) clientRectangle.Width, (int) clientRectangle.Height));
      }
      if (this.tree.Root == null)
        return sizeF;
      return this.tree.Root.Bounds.Size;
    }

    protected virtual SizeF SubtractControlScrollbarsSize(SizeF availableSize)
    {
      RadLayoutControl control = this.ElementTree.Control as RadLayoutControl;
      if (control.ContainerElement == this)
      {
        availableSize.Width -= control.VerticalScrollbar.Visible ? (float) control.VerticalScrollbar.Width : 0.0f;
        availableSize.Height -= control.HorizontalScrollbar.Visible ? (float) control.HorizontalScrollbar.Height : 0.0f;
      }
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      Padding clientOffset = this.GetClientOffset(this.DrawBorder);
      SizeF sizeF = this.tree.Root != null ? new SizeF(this.tree.Root.Bounds.Size.Width + (float) clientOffset.Horizontal, this.tree.Root.Bounds.Size.Height + (float) clientOffset.Vertical) : finalSize;
      finalSize = new SizeF(Math.Max(sizeF.Width, finalSize.Width), Math.Max(sizeF.Height, finalSize.Height));
      this.PerformControlLayout();
      return finalSize;
    }

    private void items_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (this.ElementState != ElementState.Loaded)
        return;
      if (operation == ItemsChangeOperation.Removed)
      {
        this.tree.RemoveItem(target as LayoutControlItemBase);
        this.PerformControlLayout();
      }
      if (operation == ItemsChangeOperation.Inserted)
        this.EnsureItemControlAdded(target as LayoutControlItem);
      (this.ElementTree != null ? this.ElementTree.Control as RadLayoutControl : (RadLayoutControl) null)?.OnStructureChanged((object) this);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      foreach (LayoutControlItemBase layoutControlItemBase in (RadItemCollection) this.Items)
        this.EnsureItemControlAdded(layoutControlItemBase as LayoutControlItem);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.BoundsProperty || !(this.Parent is RootRadElement))
        return;
      (this.ElementTree.Control as RadLayoutControl)?.UpdateScrollbars();
    }

    protected virtual void EnsureItemControlAdded(LayoutControlItem controlItem)
    {
      RadLayoutControl radLayoutControl = this.ElementTree != null ? this.ElementTree.Control as RadLayoutControl : (RadLayoutControl) null;
      if (controlItem == null || controlItem.AssociatedControl == null || (radLayoutControl == null || radLayoutControl.IsInitializing) || radLayoutControl.Controls.Contains(controlItem.AssociatedControl))
        return;
      ((RadLayoutControlControlCollection) radLayoutControl.Controls).AddInternal(controlItem.AssociatedControl);
    }
  }
}
