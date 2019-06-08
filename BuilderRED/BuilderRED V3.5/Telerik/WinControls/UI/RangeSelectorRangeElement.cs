// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorRangeElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;
using Telerik.WinControls.UI.RangeSelector.InterfacesAndEnum;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorRangeElement : RangeSelectorVisualElementWithOrientation
  {
    private long prev = DateTime.Now.Ticks;
    private float startDelta;
    private float endDelta;
    private float newIndex;
    private float newIndex2;
    private long ticks;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = true;
      this.GradientStyle = GradientStyles.Solid;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.RangeSelectorElement.BodyElement.ViewContainer.SelectionRectangle.ReleaseSelectionRectangle)
        this.Capture = true;
      else
        this.RangeSelectorElement.BodyElement.ViewContainer.SelectionRectangle.PerformSelctionClick(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.RangeSelectorElement.IsMouseUp = true;
      if (this.RangeSelectorElement.UpdateMode == UpdateMode.Deferred)
        this.UpdateAssociatedChartPanFactor();
      this.Capture = false;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      this.ticks = DateTime.Now.Ticks;
      if (this.ticks - this.prev > this.RangeSelectorElement.LayoutsRefreshRateInTicks)
        this.MoveRange(e);
      this.prev = this.ticks;
      if (this.RangeSelectorElement.UpdateMode != UpdateMode.Immediate)
        return;
      this.UpdateAssociatedChartPanFactor();
    }

    private void UpdateAssociatedChartPanFactor()
    {
      if (this.RangeSelectorElement.EnableFastScrolling)
      {
        RangeSelectorSelectionChangingEventArgs changingArgs = new RangeSelectorSelectionChangingEventArgs(this.newIndex, this.newIndex2);
        this.OnSelectionChanging(changingArgs);
        if (changingArgs.Cancel)
          return;
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupStartRange(this.newIndex);
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupEndRange(this.newIndex2);
        this.OnSelectionChanged(new EventArgs());
      }
      if (!(this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement))
        return;
      (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).UpdateAssociatedView();
    }

    private void MoveRange(MouseEventArgs e)
    {
      if (!this.Capture)
        this.CaptureDeltas(e);
      float delta = Math.Abs(this.RangeSelectorElement.StartRange - this.RangeSelectorElement.EndRange);
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
        this.PerformHorizontalRangeMove(delta, e);
      else
        this.PerformVerticalRangeMove(delta, e);
    }

    private void PerformHorizontalRangeMove(float delta, MouseEventArgs e)
    {
      float num = (float) ((double) (e.Location.X - this.Parent.LocationToControl().X) / (double) this.Parent.BoundingRectangle.Width * 100.0);
      this.newIndex = num - this.startDelta;
      this.newIndex2 = num + this.endDelta;
      if ((double) this.newIndex < 0.0)
      {
        this.newIndex = 0.0f;
        this.newIndex2 = delta;
      }
      if ((double) this.newIndex2 > 100.0)
      {
        this.newIndex2 = 100f;
        this.newIndex = 100f - delta;
      }
      if ((double) this.newIndex == (double) this.RangeSelectorElement.StartRange || (double) this.newIndex2 == (double) this.RangeSelectorElement.EndRange)
        return;
      if (!this.RangeSelectorElement.EnableFastScrolling)
      {
        RangeSelectorSelectionChangingEventArgs changingArgs = new RangeSelectorSelectionChangingEventArgs(this.newIndex, this.newIndex2);
        this.OnSelectionChanging(changingArgs);
        if (changingArgs.Cancel)
          return;
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.StartRange = this.newIndex;
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.EndRange = this.newIndex2;
        this.OnSelectionChanged(new EventArgs());
      }
      else
      {
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupStartRangeWithOutEvents(this.newIndex);
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupEndRangeWithOutEvents(this.newIndex2);
      }
    }

    private void PerformVerticalRangeMove(float delta, MouseEventArgs e)
    {
      float num1 = (float) (100.0 - (double) (e.Location.Y - this.Parent.LocationToControl().Y) / (double) this.Parent.BoundingRectangle.Height * 100.0);
      float num2 = num1 - this.startDelta;
      float num3 = num1 + this.endDelta;
      if ((double) num2 < 0.0)
      {
        num2 = 0.0f;
        num3 = delta;
      }
      if ((double) num3 > 100.0)
      {
        num3 = 100f;
        num2 = 100f - delta;
      }
      this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.StartRange = num2;
      this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.EndRange = num3;
    }

    private void CaptureDeltas(MouseEventArgs e)
    {
      float num = 0.0f;
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
      {
        if (!this.RightToLeft)
          num = (float) ((double) (e.Location.X - this.Parent.LocationToControl().X) / (double) this.Parent.BoundingRectangle.Width * 100.0);
      }
      else
        num = (float) ((double) (e.Location.Y - this.Parent.LocationToControl().Y) / (double) this.Parent.BoundingRectangle.Height * 100.0);
      this.startDelta = num - this.RangeSelectorElement.StartRange;
      this.endDelta = this.RangeSelectorElement.EndRange - num;
    }

    private void OnSelectionChanging(
      RangeSelectorSelectionChangingEventArgs changingArgs)
    {
      this.RangeSelectorElement.OnSelectionChanging(changingArgs);
    }

    private void OnSelectionChanged(EventArgs eventArgs)
    {
      this.RangeSelectorElement.OnSelectionChanged(eventArgs);
    }
  }
}
