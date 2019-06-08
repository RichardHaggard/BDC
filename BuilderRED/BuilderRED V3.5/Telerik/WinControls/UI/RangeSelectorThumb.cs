// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorThumb
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI.RangeSelector.InterfacesAndEnum;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorThumb : RangeSelectorVisualElementWithOrientation
  {
    private PointF oldLocation = PointF.Empty;
    private long prev = DateTime.Now.Ticks;
    private bool isFirst;
    private float startDelta;
    private float endDelta;
    private ToolTip toolTip;
    private float start;
    private float end;
    private long ticks;

    public RangeSelectorThumb(bool isFirst)
    {
      this.isFirst = isFirst;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = true;
      this.BackColor = Color.Red;
      this.GradientStyle = GradientStyles.Solid;
      this.MinSize = new Size(20, 20);
      this.StretchVertically = true;
      this.StretchHorizontally = false;
      this.ZIndex = 4;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.Capture = true;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.RangeSelectorElement.IsMouseUp = true;
      this.RangeSelectorElement.ShouldFireSelectionChangeEvent = false;
      if (this.RangeSelectorElement.UpdateMode == UpdateMode.Deferred)
        this.UpdateAssociatedChartZoomFactor();
      this.Capture = false;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.toolTip == null)
      {
        this.toolTip = this.ElementTree.ComponentTreeHandler.Behavior.ToolTip;
        this.toolTip.InitialDelay = 0;
      }
      if ((PointF) e.Location == this.oldLocation)
      {
        base.OnMouseMove(e);
      }
      else
      {
        this.oldLocation = (PointF) e.Location;
        if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        {
          this.RangeSelectorElement.IsMouseUp = false;
          this.RangeSelectorElement.ShouldFireSelectionChangeEvent = true;
          this.ticks = DateTime.Now.Ticks;
          if (this.ticks - this.prev > this.RangeSelectorElement.LayoutsRefreshRateInTicks)
            this.MoveThumb(e);
          this.prev = this.ticks;
        }
        if (this.RangeSelectorElement.UpdateMode == UpdateMode.Immediate)
          this.UpdateAssociatedChartZoomFactor();
        if (!this.ElementTree.ComponentTreeHandler.Behavior.ShowItemToolTips)
          return;
        RangeSelectorTrackingElement trackingElement = this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement;
        this.toolTip.Show(string.Format(trackingElement.ToolTipThumbFormatString, (object) this.RangeSelectorElement.StartRange, (object) this.RangeSelectorElement.EndRange), (IWin32Window) this.ElementTree.Control, e.X + trackingElement.ToolTipOffset.X, e.Y + trackingElement.ToolTipOffset.Y, trackingElement.ToolTipDuration);
      }
    }

    private void UpdateAssociatedChartZoomFactor()
    {
      if (this.RangeSelectorElement.EnableFastScrolling)
      {
        if (this.isFirst)
          this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupStartRangeWithAllEvents(this.start);
        else
          this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupEndRangeWithAllEvents(this.end);
      }
      if (!(this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement))
        return;
      (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).UpdateAssociatedView();
    }

    private void MoveThumb(MouseEventArgs e)
    {
      if (!this.Capture)
        this.CaptureDeltas(e);
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
        this.PerformHorizontalThumbMove(e);
      else
        this.PerformVerticalThumbMove(e);
    }

    private void CaptureDeltas(MouseEventArgs e)
    {
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
      {
        if (this.RangeSelectorElement.ShowButtons)
        {
          this.startDelta = (float) e.X - (this.RangeSelectorElement.BodyElement.LeftArrow.DesiredSize.Width + (float) this.BoundingRectangle.X) - (float) (this.BoundingRectangle.Width / 2);
          this.endDelta = this.startDelta + this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.LeftThumbLine.DesiredSize.Width;
        }
        else
        {
          this.startDelta = (float) (e.X - this.BoundingRectangle.X - this.BoundingRectangle.Width / 2);
          this.endDelta = this.startDelta + this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.LeftThumbLine.DesiredSize.Width;
        }
      }
      else if (this.RangeSelectorElement.ShowButtons)
      {
        this.startDelta = (float) e.Y - (this.RangeSelectorElement.BodyElement.LeftArrow.DesiredSize.Height + (float) this.BoundingRectangle.Y) - (float) (this.BoundingRectangle.Height / 2);
        this.endDelta = this.startDelta + this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.LeftThumbLine.DesiredSize.Height;
      }
      else
      {
        this.startDelta = (float) (e.Y - this.BoundingRectangle.Y - this.BoundingRectangle.Height / 2);
        this.endDelta = this.startDelta + this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.LeftThumbLine.DesiredSize.Height;
      }
    }

    private void PerformHorizontalThumbMove(MouseEventArgs e)
    {
      if (this.isFirst)
      {
        this.start = (float) (((double) e.Location.X - (double) this.startDelta - (double) this.Parent.LocationToControl().X) / (double) this.Parent.BoundingRectangle.Width * 100.0);
        if ((double) this.start > (double) (100 - this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.MinSelectionLength))
          this.start = (float) (100 - this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.MinSelectionLength);
        if ((double) this.start < 0.0)
          this.start = 0.0f;
        if (!this.RangeSelectorElement.EnableFastScrolling)
          this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.StartRange = this.start;
        else
          this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupStartRangeWithOutEvents(this.start);
      }
      else
      {
        this.end = (float) (((double) e.Location.X - (double) this.startDelta - (double) this.Parent.LocationToControl().X) / (double) this.Parent.BoundingRectangle.Width * 100.0);
        if ((double) this.end < (double) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.MinSelectionLength)
          this.end = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.MinSelectionLength;
        if ((double) this.end > 100.0)
          this.end = 100f;
        if (!this.RangeSelectorElement.EnableFastScrolling)
          this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.EndRange = this.end;
        else
          this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupEndRangeWithOutEvents(this.end);
      }
    }

    private void PerformVerticalThumbMove(MouseEventArgs e)
    {
      if (this.isFirst)
      {
        this.start = (float) (100.0 - ((double) e.Location.Y - (double) this.startDelta - 1.0 - (double) this.Parent.LocationToControl().Y) / (double) this.Parent.BoundingRectangle.Height * 100.0);
        if ((double) this.start > (double) (100 - this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.MinSelectionLength))
          this.start = (float) (100 - this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.MinSelectionLength);
        if ((double) this.start < 0.0)
          this.start = 0.0f;
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.StartRange = this.start;
      }
      else
      {
        this.end = (float) (100.0 - ((double) e.Location.Y - (double) this.startDelta - 1.0 - (double) this.Parent.LocationToControl().Y) / (double) this.Parent.BoundingRectangle.Height * 100.0);
        if ((double) this.end < (double) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.MinSelectionLength)
          this.end = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.MinSelectionLength;
        if ((double) this.end > 100.0)
          this.end = 100f;
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.EndRange = this.end;
      }
    }

    internal void PerformThumbClick(MouseEventArgs e)
    {
      this.OnMouseDown(e);
    }
  }
}
