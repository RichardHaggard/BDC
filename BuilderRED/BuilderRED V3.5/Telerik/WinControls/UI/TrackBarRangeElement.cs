// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarRangeElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TrackBarRangeElement : TrackBarElementWithOrientation
  {
    private TrackBarRange rangeInfo;
    private float startDelta;
    private float endDelta;

    static TrackBarRangeElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TrackBarStateManager(), typeof (TrackBarRangeElement));
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.rangeInfo = (this.Parent as TrackBarIndicatorElement).RangeInfo;
      this.ToolTipText = this.rangeInfo.ToolTipText;
      this.rangeInfo.PropertyChanged += new PropertyChangedEventHandler(this.rangeInfo_PropertyChanged);
    }

    protected override void DisposeManagedResources()
    {
      if (this.rangeInfo != null)
        this.rangeInfo.PropertyChanged -= new PropertyChangedEventHandler(this.rangeInfo_PropertyChanged);
      base.DisposeManagedResources();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.Alignment = ContentAlignment.MiddleRight;
      int num = (int) this.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(0, 2));
      this.NotifyParentOnMouseInput = false;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.TrackBarElement.BodyElement.IndicatorContainerElement.RefreshRanges();
      foreach (TrackBarRange range in this.TrackBarElement.Ranges)
        range.IsSelected = false;
      this.TrackBarElement.BodyElement.IndicatorContainerElement.RefreshRanges();
      this.TrackBarElement.CurrentThumb = (TrackBarThumbElement) null;
      this.rangeInfo.IsSelected = true;
      this.TrackBarElement.BodyElement.IndicatorContainerElement.InvalidateMeasure();
      this.Capture = true;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.Capture = false;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      if (this.TrackBarElement.TrackBarMode == TrackBarRangeMode.SingleThumb)
        this.TrackBarElement.BodyElement.PerformMouseClick(e);
      if (this.TrackBarElement.TrackBarMode != TrackBarRangeMode.Range)
        return;
      this.UpdateSelectedRange(e);
    }

    private void rangeInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "ToolTipText"))
        return;
      this.ToolTipText = this.rangeInfo.ToolTipText;
    }

    private void CaptureDeltas(MouseEventArgs e)
    {
      float num1 = this.TrackBarElement.Minimum;
      float tickOffSet = this.TrackBarElement.TickOffSet;
      int num2 = 1;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        if (this.RightToLeft)
        {
          float num3 = (float) (this.Parent.Parent.BoundingRectangle.Width - e.Location.X + this.Parent.Parent.LocationToControl().X - this.TrackBarElement.ThumbSize.Width / 2);
          if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None)
            num1 = num3 / tickOffSet;
          if (this.TrackBarElement.SnapMode == TrackBarSnapModes.SnapToTicks)
            num1 = (double) num3 % (double) tickOffSet >= 0.5 * (double) tickOffSet ? (float) ((int) ((double) num3 / (double) tickOffSet) + num2) : (float) (int) ((double) num3 / (double) tickOffSet);
        }
        else
        {
          int num3 = this.Parent.Parent.LocationToControl().X + this.TrackBarElement.ThumbSize.Width / 2;
          if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None)
            num1 = (float) (e.Location.X - num3) / tickOffSet;
          if (this.TrackBarElement.SnapMode == TrackBarSnapModes.SnapToTicks)
            num1 = (double) (e.Location.X - num3) % (double) tickOffSet >= 0.5 * (double) tickOffSet ? (float) ((int) ((double) (e.Location.X - num3) / (double) tickOffSet) + num2) : (float) (int) ((double) (e.Location.X - num3) / (double) tickOffSet);
        }
      }
      else
      {
        int num3 = this.TrackBarElement.ThumbSize.Width / 2 - this.Parent.Parent.LocationToControl().Y;
        int height = this.Parent.Parent.BoundingRectangle.Height;
        if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None)
          num1 = (float) (height - e.Location.Y - num3) / tickOffSet;
        if (this.TrackBarElement.SnapMode == TrackBarSnapModes.SnapToTicks)
          num1 = (double) (height - e.Location.Y - num3) % (double) tickOffSet >= 0.5 * (double) tickOffSet ? (float) ((int) ((double) (height - e.Location.Y - num3) / (double) tickOffSet) + num2) : (float) (int) ((double) (height - e.Location.Y - num3) / (double) tickOffSet);
      }
      this.startDelta = num1 - this.rangeInfo.Start;
      this.endDelta = this.rangeInfo.End - num1;
    }

    private void UpdateSelectedRange(MouseEventArgs e)
    {
      if (!this.Capture)
        this.CaptureDeltas(e);
      float delta = Math.Abs(this.rangeInfo.Start - this.rangeInfo.End);
      float tickOffSet = this.TrackBarElement.TickOffSet;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
        this.PerformHorizontalRangeMove(delta, tickOffSet, e);
      else
        this.PerformVerticalRangeMove(delta, tickOffSet, e);
    }

    private void PerformHorizontalRangeMove(float delta, float tickOffSet, MouseEventArgs e)
    {
      float newIndex = this.TrackBarElement.Minimum;
      float newIndex2 = this.TrackBarElement.Minimum;
      if (this.RightToLeft)
      {
        float num = (float) (this.Parent.Parent.BoundingRectangle.Width - e.Location.X + this.Parent.Parent.LocationToControl().X - this.TrackBarElement.ThumbSize.Width / 2);
        if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None)
        {
          newIndex = num / tickOffSet - this.startDelta;
          newIndex2 = num / tickOffSet + this.endDelta;
        }
        if (this.TrackBarElement.SnapMode == TrackBarSnapModes.SnapToTicks)
        {
          if ((double) num % (double) tickOffSet < 0.5 * (double) tickOffSet)
          {
            newIndex = (float) ((int) ((double) num / (double) tickOffSet) - (int) this.startDelta);
            newIndex2 = (float) ((int) ((double) num / (double) tickOffSet) + (int) this.endDelta);
          }
          else
          {
            newIndex = (float) ((int) ((double) num / (double) tickOffSet) - (int) this.startDelta + 1);
            newIndex2 = (float) ((int) ((double) num / (double) tickOffSet) + (int) this.endDelta + 1);
          }
        }
      }
      else
      {
        int num = this.Parent.Parent.LocationToControl().X + this.TrackBarElement.ThumbSize.Width / 2;
        if (this.TrackBarElement.SnapMode == TrackBarSnapModes.None)
        {
          newIndex = (float) (e.Location.X - num) / tickOffSet - this.startDelta;
          newIndex2 = (float) (e.Location.X - num) / tickOffSet + this.endDelta;
        }
        if (this.TrackBarElement.SnapMode == TrackBarSnapModes.SnapToTicks)
        {
          if ((double) (e.Location.X - num) % (double) tickOffSet < 0.5 * (double) tickOffSet)
          {
            newIndex = (float) ((int) ((double) (e.Location.X - num) / (double) tickOffSet) - (int) this.startDelta);
            newIndex2 = (float) ((int) ((double) (e.Location.X - num) / (double) tickOffSet) + (int) this.endDelta);
          }
          else
          {
            newIndex = (float) ((int) ((double) (e.Location.X - num) / (double) tickOffSet) - (int) this.startDelta + 1);
            newIndex2 = (float) ((int) ((double) (e.Location.X - num) / (double) tickOffSet) + (int) this.endDelta + 1);
          }
        }
      }
      this.PerformRangeMove(delta, ref newIndex, ref newIndex2);
    }

    private void PerformVerticalRangeMove(float delta, float tickOffSet, MouseEventArgs e)
    {
      float newIndex = this.TrackBarElement.Minimum;
      float newIndex2 = this.TrackBarElement.Minimum;
      int num = this.TrackBarElement.ThumbSize.Width / 2;
      int height = this.Parent.Parent.Parent.BoundingRectangle.Height;
      switch (this.TrackBarElement.SnapMode)
      {
        case TrackBarSnapModes.None:
          newIndex = (float) (height - e.Location.Y - num) / tickOffSet - this.startDelta;
          newIndex2 = (float) (height - e.Location.Y - num) / tickOffSet + this.endDelta;
          break;
        case TrackBarSnapModes.SnapToTicks:
          if ((double) (height - e.Location.Y - num) % (double) tickOffSet < 0.5 * (double) tickOffSet)
          {
            newIndex = (float) ((int) ((double) (height - e.Location.Y - num) / (double) tickOffSet) - (int) this.startDelta);
            newIndex2 = (float) ((int) ((double) (height - e.Location.Y - num) / (double) tickOffSet) + (int) this.endDelta);
            break;
          }
          newIndex = (float) ((int) ((double) (height - e.Location.Y - num) / (double) tickOffSet) - (int) this.startDelta + 1);
          newIndex2 = (float) ((int) ((double) (height - e.Location.Y - num) / (double) tickOffSet) + (int) this.endDelta + 1);
          break;
      }
      this.PerformRangeMove(delta, ref newIndex, ref newIndex2);
    }

    private void PerformRangeMove(float delta, ref float newIndex, ref float newIndex2)
    {
      if ((double) newIndex < (double) this.TrackBarElement.Minimum)
      {
        newIndex = this.TrackBarElement.Minimum;
        newIndex2 = this.TrackBarElement.Minimum + delta;
      }
      if ((double) newIndex2 > (double) this.TrackBarElement.Maximum)
      {
        newIndex2 = this.TrackBarElement.Maximum;
        newIndex = this.TrackBarElement.Maximum - delta;
      }
      if (!this.TrackBarElement.Ranges.CheckThumbMove(newIndex, true, this.rangeInfo) || !this.TrackBarElement.Ranges.CheckThumbMove(newIndex2, false, this.rangeInfo))
        return;
      this.rangeInfo.Start = newIndex;
      this.rangeInfo.End = newIndex2;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        if (this.TrackBarElement.TrackBarMode == TrackBarRangeMode.SingleThumb || this.TrackBarElement.TrackBarMode == TrackBarRangeMode.StartFromTheBeginning)
          return new SizeF(Math.Abs(this.TrackBarElement.Minimum - this.rangeInfo.End) * this.TrackBarElement.TickOffSet, (float) this.MinSize.Height);
        return new SizeF((this.rangeInfo.End - this.rangeInfo.Start) * this.TrackBarElement.TickOffSet - (float) this.TrackBarElement.ThumbSize.Width, (float) this.MinSize.Height);
      }
      if (this.TrackBarElement.TrackBarMode == TrackBarRangeMode.SingleThumb || this.TrackBarElement.TrackBarMode == TrackBarRangeMode.StartFromTheBeginning)
        return new SizeF((float) this.MinSize.Height, Math.Abs(this.TrackBarElement.Minimum - this.rangeInfo.End) * this.TrackBarElement.TickOffSet);
      return new SizeF((float) this.MinSize.Height, (this.rangeInfo.End - this.rangeInfo.Start) * this.TrackBarElement.TickOffSet - (float) this.TrackBarElement.ThumbSize.Width);
    }
  }
}
