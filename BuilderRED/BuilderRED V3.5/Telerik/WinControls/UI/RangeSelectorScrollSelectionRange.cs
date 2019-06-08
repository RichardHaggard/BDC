// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorScrollSelectionRange
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorScrollSelectionRange : RangeSelectorVisualElementWithOrientation
  {
    private PointF oldLocation = PointF.Empty;
    private float startDelta;
    private float endDelta;
    private float newIndex;
    private float newIndex2;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MinSize = new Size(10, 10);
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.BackColor = Color.White;
      this.GradientStyle = GradientStyles.Solid;
      this.DrawFill = true;
      this.ZIndex = 2;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.Capture = true;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.RangeSelectorElement.IsMouseUp = true;
      if (this.RangeSelectorElement.EnableFastScrolling)
      {
        RangeSelectorSelectionChangingEventArgs changingArgs = new RangeSelectorSelectionChangingEventArgs(this.newIndex, this.newIndex2);
        this.OnSelectionChanging(changingArgs);
        if (changingArgs.Cancel)
          return;
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupStartRange(this.newIndex);
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupEndRange(this.newIndex2);
        this.RangeSelectorElement.ScrollSelectorElement.InvalidateMeasure(true);
        this.OnSelectionChanged(new EventArgs());
      }
      if (this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement)
        (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).UpdateAssociatedView();
      this.Capture = false;
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
        this.MoveRange(e);
      if (!((PointF) e.Location == this.oldLocation) && this.ElementTree.ComponentTreeHandler.Behavior.ShowItemToolTips)
      {
        string text = string.Format(scrollSelectorElement.ToolTipSelectionFormatString, (object) this.RangeSelectorElement.StartRange, (object) this.RangeSelectorElement.EndRange);
        scrollSelectorElement.ToolTip.Show(text, (IWin32Window) this.ElementTree.Control, e.X + scrollSelectorElement.ToolTipOffset.X, e.Y + scrollSelectorElement.ToolTipOffset.Y, scrollSelectorElement.ToolTipDuration);
      }
      this.oldLocation = (PointF) e.Location;
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

    private void MoveRange(MouseEventArgs e)
    {
      if (!this.Capture)
        this.CaptureDeltas(e);
      float delta = Math.Abs(this.RangeSelectorElement.EndRange - this.RangeSelectorElement.StartRange);
      this.RangeSelectorElement.IsMouseUp = false;
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
      this.startDelta = num1 - this.RangeSelectorElement.StartRange;
      this.endDelta = this.RangeSelectorElement.EndRange - num1;
    }

    private void PerformHorizontalRangeMove(float delta, MouseEventArgs e)
    {
      float num1 = (float) this.Parent.BoundingRectangle.Width - this.RangeSelectorElement.ScrollSelectorElement.LeftTopButton.DesiredSize.Width - this.RangeSelectorElement.ScrollSelectorElement.BottomRightButton.DesiredSize.Width;
      float num2 = (float) ((double) (e.Location.X - this.Parent.LocationToControl().X) / (double) num1 * 100.0);
      this.newIndex = num2 - this.startDelta;
      this.newIndex2 = num2 + this.endDelta;
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
      if ((double) this.newIndex == (double) this.RangeSelectorElement.StartRange && (double) this.newIndex2 == (double) this.RangeSelectorElement.EndRange)
        return;
      if (!this.RangeSelectorElement.EnableFastScrolling)
      {
        RangeSelectorSelectionChangingEventArgs changingArgs = new RangeSelectorSelectionChangingEventArgs(this.newIndex, this.newIndex2);
        this.OnSelectionChanging(changingArgs);
        if (changingArgs.Cancel)
          return;
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.StartRange = this.newIndex;
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.EndRange = this.newIndex2;
        this.RangeSelectorElement.ScrollSelectorElement.InvalidateMeasure(true);
        this.OnSelectionChanged(new EventArgs());
      }
      else
      {
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupStartRangeWithOutEvents(this.newIndex);
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.SetupEndRangeWithOutEvents(this.newIndex2);
      }
    }
  }
}
