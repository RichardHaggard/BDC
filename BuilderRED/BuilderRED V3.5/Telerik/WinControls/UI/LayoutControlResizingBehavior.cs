// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlResizingBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;

namespace Telerik.WinControls.UI
{
  public class LayoutControlResizingBehavior
  {
    private bool isResizing;
    private Orientation resizeType;
    private LayoutTreeNode resizedGroup;
    private LayoutControlContainerElement owner;

    public event EventHandler ResizeStarted;

    public event EventHandler Resized;

    public event EventHandler ResizeFinished;

    public LayoutControlResizingBehavior(LayoutControlContainerElement owner)
    {
      this.owner = owner;
    }

    public bool BeginResize(Point point, Orientation resizeType)
    {
      point.Offset(-this.owner.ControlBoundingRectangle.Location.X, -this.owner.ControlBoundingRectangle.Location.Y);
      this.resizedGroup = this.owner.LayoutTree.FindGroupBySplitter(point, resizeType);
      if (this.resizedGroup == null)
        return false;
      if (this.resizedGroup.Left != null && this.resizedGroup.Left.Item is LayoutControlSplitterItem || this.resizedGroup.Right != null && this.resizedGroup.Right.Item is LayoutControlSplitterItem)
        return this.BeginResize(this.resizedGroup.Left.Item as LayoutControlSplitterItem ?? this.resizedGroup.Right.Item as LayoutControlSplitterItem);
      this.isResizing = true;
      this.resizeType = resizeType;
      this.OnResizeStarted();
      return true;
    }

    public bool BeginResize(LayoutControlSplitterItem splitter)
    {
      LayoutTreeNode groupBySplitter1;
      LayoutTreeNode groupBySplitter2;
      if (splitter.Orientation == Orientation.Vertical)
      {
        groupBySplitter1 = this.owner.LayoutTree.FindGroupBySplitter(new Point(splitter.Bounds.Left, (splitter.Bounds.Top + splitter.Bounds.Bottom) / 2), Orientation.Horizontal);
        groupBySplitter2 = this.owner.LayoutTree.FindGroupBySplitter(new Point(splitter.Bounds.Right, (splitter.Bounds.Top + splitter.Bounds.Bottom) / 2), Orientation.Horizontal);
      }
      else
      {
        groupBySplitter1 = this.owner.LayoutTree.FindGroupBySplitter(new Point((splitter.Bounds.Left + splitter.Bounds.Right) / 2, splitter.Bounds.Top), Orientation.Vertical);
        groupBySplitter2 = this.owner.LayoutTree.FindGroupBySplitter(new Point((splitter.Bounds.Left + splitter.Bounds.Right) / 2, splitter.Bounds.Bottom), Orientation.Vertical);
      }
      if (groupBySplitter1 == null && groupBySplitter2 == null)
        return false;
      if (groupBySplitter1 == null || groupBySplitter2 == null)
      {
        this.resizedGroup = groupBySplitter1 ?? groupBySplitter2;
        this.isResizing = true;
        this.resizeType = this.resizedGroup.SplitType;
        this.OnResizeStarted();
        return true;
      }
      if (groupBySplitter2.Left != null && groupBySplitter2.Left.Item == splitter || groupBySplitter2.Right != null && groupBySplitter2.Right.Item == splitter)
      {
        this.resizedGroup = groupBySplitter1;
      }
      else
      {
        if ((groupBySplitter1.Left == null || groupBySplitter1.Left.Item != splitter) && (groupBySplitter1.Right == null || groupBySplitter1.Right.Item != splitter))
          return false;
        this.resizedGroup = groupBySplitter2;
      }
      this.isResizing = true;
      this.resizeType = this.resizedGroup.SplitType;
      this.OnResizeStarted();
      return true;
    }

    public void Resize(Point point)
    {
      if (!this.IsResizing)
        return;
      point.Offset(-this.owner.ControlBoundingRectangle.Location.X, -this.owner.ControlBoundingRectangle.Location.Y);
      float splitterDiff = this.resizeType == Orientation.Horizontal ? (float) point.X - this.resizedGroup.SplitPosition : (float) point.Y - this.resizedGroup.SplitPosition;
      if ((double) Math.Abs(splitterDiff) <= 1.40129846432482E-45)
        return;
      this.owner.LayoutTree.MoveSplitter(splitterDiff, this.resizedGroup);
      (this.owner.ElementTree.Control as RadLayoutControl)?.UpdateControlsLayout();
      this.OnResized();
    }

    public void EndResize()
    {
      if (!this.IsResizing)
        return;
      this.isResizing = false;
      this.resizedGroup = (LayoutTreeNode) null;
      this.owner.LayoutTree.ResetOriginalBounds();
      this.OnResizeFinished();
      Control control = this.owner.ElementTree.Control;
      ControlTraceMonitor.TrackAtomicFeature(control.Parent is RadDataLayout ? (RadControl) control.Parent : (RadControl) control, "LayoutModified", (object) "Resize");
    }

    public Cursor GetCursorAtPoint(Point point)
    {
      point.Offset(-this.owner.ControlBoundingRectangle.Location.X, -this.owner.ControlBoundingRectangle.Location.Y);
      foreach (LayoutControlItemBase layoutControlItemBase in (RadItemCollection) this.owner.Items)
      {
        if (layoutControlItemBase != null && (Math.Abs(layoutControlItemBase.Bounds.Left - point.X) < 4 || Math.Abs(layoutControlItemBase.Bounds.Right - point.X) < 4) && (point.Y > layoutControlItemBase.Bounds.Top && point.Y < layoutControlItemBase.Bounds.Bottom))
          return Cursors.SizeWE;
        if (layoutControlItemBase != null && (Math.Abs(layoutControlItemBase.Bounds.Top - point.Y) < 4 || Math.Abs(layoutControlItemBase.Bounds.Bottom - point.Y) < 4) && (point.X > layoutControlItemBase.Bounds.Left && point.X < layoutControlItemBase.Bounds.Right))
          return Cursors.SizeNS;
      }
      return Cursors.Default;
    }

    protected virtual void OnResizeStarted()
    {
      if (this.ResizeStarted == null)
        return;
      this.ResizeStarted((object) this, EventArgs.Empty);
    }

    protected virtual void OnResized()
    {
      if (this.Resized == null)
        return;
      this.Resized((object) this, EventArgs.Empty);
    }

    protected virtual void OnResizeFinished()
    {
      if (this.ResizeFinished == null)
        return;
      this.ResizeFinished((object) this, EventArgs.Empty);
    }

    public bool IsResizing
    {
      get
      {
        return this.isResizing;
      }
    }
  }
}
