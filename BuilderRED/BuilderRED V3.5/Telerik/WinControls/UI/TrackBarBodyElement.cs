// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarBodyElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TrackBarBodyElement : TrackBarVisualElement
  {
    private TrackBarScaleContainerElement scaleElement;
    private TrackBarIndicatorContainerElement indicatorContainerElement;

    protected override void CreateChildElements()
    {
      this.scaleElement = new TrackBarScaleContainerElement();
      this.indicatorContainerElement = new TrackBarIndicatorContainerElement();
      this.Children.Add((RadElement) this.scaleElement);
      this.Children.Add((RadElement) this.indicatorContainerElement);
      base.CreateChildElements();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.Alignment = ContentAlignment.MiddleCenter;
      this.ShouldHandleMouseInput = true;
      this.NotifyParentOnMouseInput = false;
    }

    public TrackBarScaleContainerElement ScaleContainerElement
    {
      get
      {
        return this.scaleElement;
      }
    }

    public TrackBarIndicatorContainerElement IndicatorContainerElement
    {
      get
      {
        return this.indicatorContainerElement;
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.PerformMouseClick(e);
    }

    public void PerformMouseClick(MouseEventArgs e)
    {
      int oldValue = (int) this.TrackBarElement.Value;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
        this.PerformThumbMoveHorizntal(e);
      else
        this.PerformThumbMoveVertical(e);
      this.TrackBarElement.FireScrollEvent(oldValue);
    }

    private void PerformThumbMoveHorizntal(MouseEventArgs e)
    {
      float num1 = 0.0f;
      int num2 = 1;
      float tickOffSet = this.TrackBarElement.TickOffSet;
      float num3 = !this.RightToLeft ? (float) (e.Location.X - this.LocationToControl().X - this.TrackBarElement.ThumbSize.Width / 2) : (float) (this.BoundingRectangle.Width - e.Location.X + this.LocationToControl().X - this.TrackBarElement.ThumbSize.Width / 2);
      if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None)
        num1 = num3 / tickOffSet;
      if (this.TrackBarElement.SnapMode == TrackBarSnapModes.SnapToTicks)
        num1 = (double) num3 % (double) tickOffSet >= 0.5 * (double) tickOffSet ? (float) ((int) ((double) num3 / (double) tickOffSet) + num2) : (float) (int) ((double) num3 / (double) tickOffSet);
      if ((double) num1 < 0.0)
        num1 = 0.0f;
      if ((double) num1 > (double) this.TrackBarElement.MaxTickNumber)
        num1 = (float) this.TrackBarElement.MaxTickNumber;
      if (this.TrackBarElement.LargeChange > 0)
      {
        if ((double) this.TrackBarElement.Ranges[0].End < (double) (this.TrackBarElement.Minimum + num1 * (float) num2))
          this.TrackBarElement.Ranges[0].End += (float) this.TrackBarElement.LargeChange;
        else
          this.TrackBarElement.Ranges[0].End -= (float) this.TrackBarElement.LargeChange;
      }
      else
        this.TrackBarElement.Ranges.PerformThumbMove(this.TrackBarElement.Minimum + num1);
    }

    private void PerformThumbMoveVertical(MouseEventArgs e)
    {
      float num1 = this.TrackBarElement.Minimum;
      int num2 = 1;
      float tickOffSet = this.TrackBarElement.TickOffSet;
      float num3 = (float) (this.BoundingRectangle.Height - e.Location.Y + this.LocationToControl().Y - this.TrackBarElement.ThumbSize.Width / 2);
      if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None)
        num1 = num3 / tickOffSet;
      if (this.TrackBarElement.SnapMode == TrackBarSnapModes.SnapToTicks)
        num1 = (double) num3 % (double) tickOffSet >= 0.5 * (double) tickOffSet ? (float) ((int) ((double) num3 / (double) tickOffSet) + num2) : (float) (int) ((double) num3 / (double) tickOffSet);
      if ((double) num1 < 0.0)
        num1 = 0.0f;
      if ((double) num1 > (double) this.TrackBarElement.MaxTickNumber)
        num1 = (float) this.TrackBarElement.MaxTickNumber;
      if (this.TrackBarElement.LargeChange > 0)
      {
        if ((double) this.TrackBarElement.Ranges[0].End < (double) (this.TrackBarElement.Minimum + num1 * (float) num2))
          this.TrackBarElement.Ranges[0].End += (float) this.TrackBarElement.LargeChange;
        else
          this.TrackBarElement.Ranges[0].End -= (float) this.TrackBarElement.LargeChange;
      }
      else
        this.TrackBarElement.Ranges.PerformThumbMove(this.TrackBarElement.Minimum + num1 * (float) num2);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      this.scaleElement.Measure(availableSize);
      this.indicatorContainerElement.Measure(availableSize);
      empty.Width = Math.Max(this.scaleElement.DesiredSize.Width, this.indicatorContainerElement.DesiredSize.Width);
      empty.Height = Math.Max(this.scaleElement.DesiredSize.Height, this.indicatorContainerElement.DesiredSize.Height);
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.scaleElement.Arrange(new RectangleF(0.0f, 0.0f, finalSize.Width, finalSize.Height));
      int num = 1;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        float y = (float) (this.scaleElement.TrackBarLineElement.BoundingRectangle.Y - (int) (((double) this.indicatorContainerElement.DesiredSize.Height - (double) this.scaleElement.TrackBarLineElement.BoundingRectangle.Height) / 2.0));
        this.indicatorContainerElement.Arrange(new RectangleF((float) num, y, finalSize.Width - (float) (2 * num), this.indicatorContainerElement.DesiredSize.Height));
      }
      else
        this.indicatorContainerElement.Arrange(new RectangleF((float) (this.scaleElement.TrackBarLineElement.BoundingRectangle.X - (int) (((double) this.indicatorContainerElement.DesiredSize.Width - (double) this.scaleElement.TrackBarLineElement.BoundingRectangle.Width) / 2.0)), (float) num, this.indicatorContainerElement.DesiredSize.Width, finalSize.Height - (float) (2 * num)));
      return finalSize;
    }
  }
}
