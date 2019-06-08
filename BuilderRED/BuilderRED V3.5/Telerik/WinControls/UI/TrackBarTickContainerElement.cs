// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarTickContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TrackBarTickContainerElement : TrackBarVisualElement
  {
    public static RadProperty LargeTickHeightProperty = RadProperty.Register(nameof (LargeTickHeight), typeof (int), typeof (TrackBarTickContainerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 10, ElementPropertyOptions.InvalidatesLayout));
    public static RadProperty SmallTickHeightProperty = RadProperty.Register(nameof (SmallTickHeight), typeof (int), typeof (TrackBarTickContainerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.InvalidatesLayout));
    private int smallTickFrequency = 1;
    private int largeTickFrequency = 5;
    private float oldTickOffset = float.MinValue;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
    }

    public int LargeTickHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(TrackBarTickContainerElement.LargeTickHeightProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarTickContainerElement.LargeTickHeightProperty, (object) value);
      }
    }

    public int SmallTickHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(TrackBarTickContainerElement.SmallTickHeightProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarTickContainerElement.SmallTickHeightProperty, (object) value);
      }
    }

    public Color TickColor
    {
      get
      {
        TrackBarTickElement child = this.Children[0] as TrackBarTickElement;
        if (child == null)
          return Color.FromArgb(160, 160, 160);
        return child.Line1.BackColor;
      }
      set
      {
        foreach (TrackBarTickElement child in this.Children)
        {
          child.Line1.BackColor = value;
          child.Line2.BackColor = value;
        }
      }
    }

    internal int LargeTickFrequency
    {
      get
      {
        return this.largeTickFrequency;
      }
      set
      {
        this.largeTickFrequency = value;
      }
    }

    internal int SmallTickFrequency
    {
      get
      {
        return this.smallTickFrequency;
      }
      set
      {
        this.smallTickFrequency = value;
      }
    }

    protected internal void UpdateTickElements()
    {
      this.Children.Clear();
      this.Invalidate();
      bool isTopLeft = (this.Parent as TrackBarScaleElement).IsTopLeft;
      for (int index = 0; index <= this.TrackBarElement.MaxTickNumber; ++index)
      {
        TrackBarTickElement tickElement = new TrackBarTickElement();
        tickElement.IsTopLeft = isTopLeft;
        tickElement.NotifyParentOnMouseInput = true;
        tickElement.ShouldHandleMouseInput = false;
        if (this.TrackBarElement.Orientation == Orientation.Horizontal)
          tickElement.Orientation = Orientation.Vertical;
        else
          tickElement.Orientation = Orientation.Horizontal;
        if (this.largeTickFrequency != 0 && index % this.largeTickFrequency == 0)
        {
          if (tickElement.Orientation == Orientation.Vertical)
            tickElement.MinSize = new Size(tickElement.Line1.LineWidth + tickElement.Line2.LineWidth, this.LargeTickHeight);
          else
            tickElement.MinSize = new Size(this.LargeTickHeight, tickElement.Line1.LineWidth + tickElement.Line2.LineWidth);
          tickElement.IsLargeTick = true;
          tickElement.TickNumber = index;
          this.OnTickFormatting(tickElement);
          this.Children.Add((RadElement) tickElement);
        }
        else if (this.smallTickFrequency != 0 && index % this.smallTickFrequency == 0)
        {
          if (tickElement.Orientation == Orientation.Vertical)
            tickElement.MinSize = new Size(tickElement.Line1.LineWidth + tickElement.Line2.LineWidth, this.SmallTickHeight);
          else
            tickElement.MinSize = new Size(this.SmallTickHeight, tickElement.Line1.LineWidth + tickElement.Line2.LineWidth);
          tickElement.IsLargeTick = false;
          tickElement.TickNumber = index;
          this.OnTickFormatting(tickElement);
          this.Children.Add((RadElement) tickElement);
        }
      }
    }

    public virtual void FormatTicks()
    {
      foreach (TrackBarTickElement child in this.Children)
        this.OnTickFormatting(child);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        foreach (RadElement child in this.Children)
        {
          child.Measure(availableSize);
          empty.Height = Math.Max(empty.Height, child.DesiredSize.Height);
          empty.Width += child.DesiredSize.Width;
        }
      }
      else
      {
        foreach (RadElement child in this.Children)
        {
          child.Measure(availableSize);
          empty.Height += child.DesiredSize.Height;
          empty.Width = Math.Max(empty.Width, child.DesiredSize.Width);
        }
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.Children.Count == 0)
        return base.ArrangeOverride(finalSize);
      float tickWidth;
      float num;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        tickWidth = this.Children[this.Children.Count - 1].DesiredSize.Width;
        num = (finalSize.Width - tickWidth - (float) this.TrackBarElement.ThumbSize.Width) / (float) this.TrackBarElement.MaxTickNumber;
      }
      else
      {
        tickWidth = this.Children[this.Children.Count - 1].DesiredSize.Height;
        num = (finalSize.Height - tickWidth - (float) this.TrackBarElement.ThumbSize.Width) / (float) this.TrackBarElement.MaxTickNumber;
      }
      if ((this.Parent as TrackBarScaleElement).IsTopLeft)
        this.ArrangeTopLeftTicks(this.TrackBarElement.TickOffSet, finalSize, tickWidth);
      else
        this.ArrangeBottomRightTicks(this.TrackBarElement.TickOffSet, finalSize, tickWidth);
      this.TrackBarElement.tickOffSet = num;
      if (this.TrackBarElement.IsMeasureValid && (double) this.oldTickOffset != (double) num)
      {
        this.oldTickOffset = num;
        this.TrackBarElement.InvalidateMeasure(true);
      }
      return finalSize;
    }

    private void ArrangeTopLeftTicks(float tickOffSet, SizeF finalSize, float tickWidth)
    {
      int index1 = 0;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        for (int index2 = 0; index2 <= this.TrackBarElement.MaxTickNumber; ++index2)
        {
          float x = !this.RightToLeft ? (float) ((int) (((double) this.TrackBarElement.ThumbSize.Width - (double) tickWidth) / 2.0) + (int) ((double) index2 * (double) tickOffSet)) : finalSize.Width - (float) (int) ((double) (this.TrackBarElement.ThumbSize.Width / 2) + (double) tickWidth + 1.0) - (float) (int) ((double) index2 * (double) tickOffSet);
          float y = (float) (int) ((double) finalSize.Height - (double) this.LargeTickHeight);
          PointF location = PointF.Empty;
          if (this.largeTickFrequency != 0 && index2 % this.largeTickFrequency == 0)
            location = new PointF(x, y);
          else if (this.smallTickFrequency != 0 && index2 % this.smallTickFrequency == 0)
            location = new PointF(x, (float) (int) ((double) finalSize.Height - (double) this.SmallTickHeight));
          else
            continue;
          SizeF desiredSize = this.Children[index1].DesiredSize;
          desiredSize.Width = Math.Max(5f, desiredSize.Width);
          this.Children[index1].Arrange(new RectangleF(location, desiredSize));
          ++index1;
        }
      }
      else
      {
        for (int index2 = 0; index2 <= this.TrackBarElement.MaxTickNumber; ++index2)
        {
          float x = (float) (int) ((double) finalSize.Width - (double) this.LargeTickHeight);
          float y = finalSize.Height - (float) ((int) ((double) (this.TrackBarElement.ThumbSize.Width / 2) + (double) tickWidth + 1.0) + (int) ((double) index2 * (double) tickOffSet));
          PointF location = PointF.Empty;
          if (this.largeTickFrequency != 0 && index2 % this.largeTickFrequency == 0)
            location = new PointF(x, y);
          else if (this.smallTickFrequency != 0 && index2 % this.smallTickFrequency == 0)
            location = new PointF(finalSize.Width - (float) this.SmallTickHeight, y);
          else
            continue;
          RectangleF finalRect = new RectangleF(location, this.Children[index1].DesiredSize);
          this.Children[index1].Arrange(finalRect);
          ++index1;
        }
      }
    }

    private void ArrangeBottomRightTicks(float tickOffSet, SizeF finalSize, float tickWidth)
    {
      int index1 = 0;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        for (int index2 = 0; index2 <= this.TrackBarElement.MaxTickNumber; ++index2)
        {
          if (this.largeTickFrequency != 0 && index2 % this.largeTickFrequency == 0 || this.smallTickFrequency != 0 && index2 % this.smallTickFrequency == 0)
          {
            PointF location = !this.RightToLeft ? new PointF((float) ((int) (((double) this.TrackBarElement.ThumbSize.Width - (double) tickWidth) / 2.0) + (int) ((double) index2 * (double) tickOffSet)), 0.0f) : new PointF(finalSize.Width - (float) (int) ((double) (this.TrackBarElement.ThumbSize.Width / 2) + (double) tickWidth + 1.0) - (float) (int) ((double) index2 * (double) tickOffSet), 0.0f);
            SizeF desiredSize = this.Children[index1].DesiredSize;
            desiredSize.Width = Math.Max(5f, desiredSize.Width);
            RectangleF finalRect = new RectangleF(location, desiredSize);
            this.Children[index1].Arrange(finalRect);
            ++index1;
          }
        }
      }
      else
      {
        for (int index2 = 0; index2 <= this.TrackBarElement.MaxTickNumber; ++index2)
        {
          if (this.largeTickFrequency != 0 && index2 % this.largeTickFrequency == 0 || this.smallTickFrequency != 0 && index2 % this.smallTickFrequency == 0)
          {
            RectangleF finalRect = new RectangleF(new PointF(0.0f, finalSize.Height - (float) ((int) ((double) (this.TrackBarElement.ThumbSize.Width / 2) + (double) tickWidth + 1.0) + (int) ((double) index2 * (double) tickOffSet))), this.Children[index1].DesiredSize);
            this.Children[index1].Arrange(finalRect);
            ++index1;
          }
        }
      }
    }

    protected virtual void OnTickFormatting(TrackBarTickElement tickElement)
    {
      this.TrackBarElement.OnTickFormatting(tickElement);
    }
  }
}
