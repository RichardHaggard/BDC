// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorScrollRange
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorScrollRange : RangeSelectorVisualElementWithOrientation
  {
    private PointF oldLocation = PointF.Empty;
    private long prev = DateTime.Now.Ticks;
    private float startDelta;
    private float endDelta;
    private long ticks;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.BackColor = Color.Blue;
      this.GradientStyle = GradientStyles.Solid;
      this.DrawFill = true;
      this.ZIndex = 1;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.Capture = true;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement)
        (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).UpdateAssociatedView();
      this.Capture = false;
      this.RangeSelectorElement.RangeSelectorViewZoomStart = this.RangeSelectorElement.ScrollSelectorElement.Start;
      this.RangeSelectorElement.RangeSelectorViewZoomEnd = this.RangeSelectorElement.ScrollSelectorElement.End;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      RangeSelectorScrollElement scrollSelectorElement = this.RangeSelectorElement.ScrollSelectorElement;
      if (scrollSelectorElement.ToolTip == null)
      {
        scrollSelectorElement.ToolTip = this.ElementTree.ComponentTreeHandler.Behavior.ToolTip;
        scrollSelectorElement.ToolTip.InitialDelay = 0;
      }
      if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
      {
        this.ticks = DateTime.Now.Ticks;
        if (this.ticks - this.prev > this.RangeSelectorElement.LayoutsRefreshRateInTicks)
          this.MoveRange(e);
        this.prev = this.ticks;
      }
      if (!((PointF) e.Location == this.oldLocation) && this.ElementTree.ComponentTreeHandler.Behavior.ShowItemToolTips)
      {
        string text = string.Format(scrollSelectorElement.ToolTipSelectionFormatString, (object) scrollSelectorElement.Start, (object) scrollSelectorElement.End);
        scrollSelectorElement.ToolTip.Show(text, (IWin32Window) this.ElementTree.Control, e.X + scrollSelectorElement.ToolTipOffset.X, e.Y + scrollSelectorElement.ToolTipOffset.Y, scrollSelectorElement.ToolTipDuration);
      }
      this.oldLocation = (PointF) e.Location;
    }

    private void MoveRange(MouseEventArgs e)
    {
      if (!this.Capture)
        this.CaptureDeltas(e);
      float delta = Math.Abs(this.RangeSelectorElement.ScrollSelectorElement.End - this.RangeSelectorElement.ScrollSelectorElement.Start);
      if (this.RangeSelectorElement.Orientation != Orientation.Horizontal)
        return;
      this.PerformHorizontalRangeMove(delta, e);
    }

    private void CaptureDeltas(MouseEventArgs e)
    {
      float num1 = 0.0f;
      float num2 = (float) this.Parent.BoundingRectangle.Width - this.RangeSelectorElement.ScrollSelectorElement.LeftTopButton.DesiredSize.Width - this.RangeSelectorElement.ScrollSelectorElement.BottomRightButton.DesiredSize.Width;
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal && !this.RightToLeft)
        num1 = (float) ((double) (e.Location.X - this.Parent.LocationToControl().X) / (double) num2 * 100.0);
      this.startDelta = num1 - this.RangeSelectorElement.ScrollSelectorElement.Start;
      this.endDelta = this.RangeSelectorElement.ScrollSelectorElement.End - num1;
    }

    private void PerformHorizontalRangeMove(float delta, MouseEventArgs e)
    {
      float num1 = (float) this.Parent.BoundingRectangle.Width - this.RangeSelectorElement.ScrollSelectorElement.LeftTopButton.DesiredSize.Width - this.RangeSelectorElement.ScrollSelectorElement.BottomRightButton.DesiredSize.Width;
      float num2 = (float) ((double) (e.Location.X - this.Parent.LocationToControl().X) / (double) num1 * 100.0);
      float num3 = num2 - this.startDelta;
      float num4 = num2 + this.endDelta;
      if ((double) num3 < 0.0)
      {
        num3 = 0.0f;
        num4 = delta;
      }
      if ((double) num4 > 100.0)
      {
        num4 = 100f;
        num3 = 100f - delta;
      }
      if ((double) this.RangeSelectorElement.ScrollSelectorElement.Start < (double) num3)
      {
        this.RangeSelectorElement.ScrollSelectorElement.End = num4;
        this.RangeSelectorElement.ScrollSelectorElement.Start = num3;
      }
      else
      {
        this.RangeSelectorElement.ScrollSelectorElement.Start = num3;
        this.RangeSelectorElement.ScrollSelectorElement.End = num4;
      }
    }
  }
}
