// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarIndicatorContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TrackBarIndicatorContainerElement : TrackBarVisualElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = false;
      this.Alignment = ContentAlignment.MiddleCenter;
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
      this.BorderWidth = 0.0f;
    }

    public void RefreshRanges()
    {
      foreach (RadElement child in this.Children)
      {
        TrackBarIndicatorElement indicatorElement = child as TrackBarIndicatorElement;
        indicatorElement.Orientation = this.TrackBarElement.Orientation;
        if (indicatorElement != null)
        {
          if (!indicatorElement.RangeInfo.IsSelected)
          {
            indicatorElement.StartThumbElement.IsSelected = false;
            indicatorElement.EndThumbElement.IsSelected = false;
          }
          if (!indicatorElement.StartThumbElement.Equals((object) this.TrackBarElement.CurrentThumb))
            indicatorElement.StartThumbElement.IsSelected = false;
          if (!indicatorElement.EndThumbElement.Equals((object) this.TrackBarElement.CurrentThumb))
            indicatorElement.EndThumbElement.IsSelected = false;
        }
      }
    }

    public void UpdateIndicatorElements()
    {
      this.Children.Clear();
      switch (this.TrackBarElement.TrackBarMode)
      {
        case TrackBarRangeMode.SingleThumb:
          if (this.TrackBarElement.Ranges.Count <= 0)
            break;
          TrackBarIndicatorElement indicatorElement1 = new TrackBarIndicatorElement(this.TrackBarElement.Ranges[0]);
          indicatorElement1.IsSelected = this.TrackBarElement.Ranges[0].IsSelected;
          indicatorElement1.StartThumbElement.Visibility = ElementVisibility.Collapsed;
          this.Children.Add((RadElement) indicatorElement1);
          break;
        case TrackBarRangeMode.StartFromTheBeginning:
          using (IEnumerator<TrackBarRange> enumerator = this.TrackBarElement.Ranges.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TrackBarRange current = enumerator.Current;
              TrackBarIndicatorElement indicatorElement2 = new TrackBarIndicatorElement(current);
              indicatorElement2.IsSelected = current.IsSelected;
              indicatorElement2.EndThumbElement.IsSelected = false;
              indicatorElement2.StartThumbElement.Visibility = ElementVisibility.Collapsed;
              this.Children.Add((RadElement) indicatorElement2);
            }
            break;
          }
        case TrackBarRangeMode.Range:
          using (IEnumerator<TrackBarRange> enumerator = this.TrackBarElement.Ranges.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TrackBarRange current = enumerator.Current;
              this.Children.Add((RadElement) new TrackBarIndicatorElement(current)
              {
                IsSelected = current.IsSelected
              });
            }
            break;
          }
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
        this.ArrangeHorizontal(finalSize);
      else
        this.ArrangeVertical(finalSize);
      return finalSize;
    }

    private void ArrangeHorizontal(SizeF finalSize)
    {
      switch (this.TrackBarElement.TrackBarMode)
      {
        case TrackBarRangeMode.SingleThumb:
          if (this.Children.Count <= 0)
            break;
          SizeF desiredSize1 = this.Children[0].DesiredSize;
          this.Children[0].Arrange(new RectangleF(!this.RightToLeft ? new PointF(0.0f, 0.0f) : new PointF(finalSize.Width - desiredSize1.Width, 0.0f), desiredSize1));
          break;
        case TrackBarRangeMode.StartFromTheBeginning:
          using (RadElementCollection.RadElementEnumerator enumerator = this.Children.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              RadElement current = enumerator.Current;
              SizeF desiredSize2 = current.DesiredSize;
              PointF location = !this.RightToLeft ? new PointF(0.0f, 0.0f) : new PointF(finalSize.Width - desiredSize2.Width, 0.0f);
              current.ZIndex = (int) (1000.0 - (double) current.DesiredSize.Width * 10.0);
              RectangleF finalRect = new RectangleF(location, desiredSize2);
              current.Arrange(finalRect);
            }
            break;
          }
        case TrackBarRangeMode.Range:
          using (RadElementCollection.RadElementEnumerator enumerator = this.Children.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              RadElement current = enumerator.Current;
              TrackBarIndicatorElement indicatorElement = current as TrackBarIndicatorElement;
              SizeF desiredSize2 = current.DesiredSize;
              RectangleF finalRect = new RectangleF(!this.RightToLeft ? new PointF(Math.Abs(this.TrackBarElement.Minimum - indicatorElement.RangeInfo.Start) * this.TrackBarElement.TickOffSet, 0.0f) : new PointF((float) ((double) finalSize.Width - (double) desiredSize2.Width - (double) Math.Abs(this.TrackBarElement.Minimum - indicatorElement.RangeInfo.Start) * (double) this.TrackBarElement.TickOffSet), 0.0f), desiredSize2);
              current.Arrange(finalRect);
            }
            break;
          }
      }
    }

    private void ArrangeVertical(SizeF finalSize)
    {
      switch (this.TrackBarElement.TrackBarMode)
      {
        case TrackBarRangeMode.SingleThumb:
          if (this.Children.Count <= 0)
            break;
          SizeF desiredSize1 = this.Children[0].DesiredSize;
          this.Children[0].Arrange(new RectangleF(new PointF(0.0f, finalSize.Height - desiredSize1.Height), desiredSize1));
          break;
        case TrackBarRangeMode.StartFromTheBeginning:
          using (RadElementCollection.RadElementEnumerator enumerator = this.Children.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              RadElement current = enumerator.Current;
              SizeF desiredSize2 = current.DesiredSize;
              current.ZIndex = (int) (1000.0 - (double) current.DesiredSize.Height * 10.0);
              RectangleF finalRect = new RectangleF(new PointF(0.0f, finalSize.Height - desiredSize2.Height), desiredSize2);
              current.Arrange(finalRect);
            }
            break;
          }
        case TrackBarRangeMode.Range:
          using (RadElementCollection.RadElementEnumerator enumerator = this.Children.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              RadElement current = enumerator.Current;
              TrackBarIndicatorElement indicatorElement = current as TrackBarIndicatorElement;
              SizeF desiredSize2 = current.DesiredSize;
              RectangleF finalRect = new RectangleF(new PointF(0.0f, finalSize.Height - Math.Abs(this.TrackBarElement.Minimum - indicatorElement.RangeInfo.Start) * this.TrackBarElement.TickOffSet - desiredSize2.Height), desiredSize2);
              current.Arrange(finalRect);
            }
            break;
          }
      }
    }
  }
}
