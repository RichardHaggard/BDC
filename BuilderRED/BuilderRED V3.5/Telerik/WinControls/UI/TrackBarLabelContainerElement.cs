// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarLabelContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TrackBarLabelContainerElement : TrackBarVisualElement
  {
    private int smallTickFrequency = 1;
    private int largeTickFrequency = 5;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = false;
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
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

    public virtual void FormatLabels()
    {
      foreach (TrackBarLabelElement child in this.Children)
        this.OnLabelFormatting(child);
    }

    public void UpdateLabelElements()
    {
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        this.StretchHorizontally = true;
        this.StretchVertically = false;
      }
      else
      {
        this.StretchHorizontally = false;
        this.StretchVertically = true;
      }
      this.Children.Clear();
      this.Invalidate();
      int num1 = 0;
      bool isTopLeft = (this.Parent as TrackBarScaleElement).IsTopLeft;
      for (int index = 0; index <= this.TrackBarElement.MaxTickNumber; ++index)
      {
        if (this.largeTickFrequency > 0 && index % this.largeTickFrequency == 0)
        {
          TrackBarLabelElement labelElement = new TrackBarLabelElement();
          labelElement.NotifyParentOnMouseInput = true;
          labelElement.ShouldHandleMouseInput = false;
          float num2 = this.TrackBarElement.Minimum + (float) (num1 * this.largeTickFrequency);
          labelElement.Text = num2.ToString();
          labelElement.IsTopLeft = isTopLeft;
          this.OnLabelFormatting(labelElement);
          this.Children.Add((RadElement) labelElement);
          ++num1;
        }
      }
      int orientation = (int) this.TrackBarElement.Orientation;
    }

    private void LabelsReverseOrder()
    {
      Stack<RadElement> radElementStack = new Stack<RadElement>();
      foreach (RadElement child in this.Children)
        radElementStack.Push(child);
      this.Children.Clear();
      while (radElementStack.Count > 0)
        this.Children.Add(radElementStack.Pop());
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        for (int index = 0; index < this.Children.Count; ++index)
        {
          TrackBarLabelElement child = this.Children[index] as TrackBarLabelElement;
          if (child != null)
          {
            child.Measure(availableSize);
            empty.Height = Math.Max(empty.Height, child.DesiredSize.Height);
            empty.Width += child.DesiredSize.Width;
            if ((index == 0 || index == this.Children.Count - 1) && (double) child.DesiredSize.Width / 2.0 > (double) this.TrackBarElement.ThumbSize.Width && (double) this.TrackBarElement.BodyElement.Margin.Left < (double) child.DesiredSize.Width / 2.0 - (double) this.TrackBarElement.ThumbSize.Width)
            {
              int num = (int) child.DesiredSize.Width / 2 - this.TrackBarElement.ThumbSize.Width + 2;
              this.TrackBarElement.BodyElement.Margin = new Padding(num, this.TrackBarElement.BodyElement.Margin.Top, num, this.TrackBarElement.BodyElement.Margin.Bottom);
            }
          }
        }
        if ((double) empty.Height == 0.0)
          empty.Width = 0.0f;
      }
      else
      {
        for (int index = 0; index < this.Children.Count; ++index)
        {
          TrackBarLabelElement child = this.Children[index] as TrackBarLabelElement;
          if (child != null)
          {
            child.Measure(availableSize);
            empty.Height += child.DesiredSize.Height;
            empty.Width = Math.Max(empty.Width, child.DesiredSize.Width);
            if ((index == 0 || index == this.Children.Count - 1) && (double) child.DesiredSize.Height / 2.0 > (double) this.TrackBarElement.ThumbSize.Height && (double) this.TrackBarElement.BodyElement.Margin.Top < (double) child.DesiredSize.Height / 2.0 - (double) this.TrackBarElement.ThumbSize.Height)
            {
              int num = (int) child.DesiredSize.Height / 2 - this.TrackBarElement.ThumbSize.Height + 1;
              this.TrackBarElement.BodyElement.Margin = new Padding(this.TrackBarElement.BodyElement.Margin.Left, num, this.TrackBarElement.BodyElement.Margin.Right, num);
            }
          }
        }
        if ((double) empty.Width == 0.0)
          empty.Height = 0.0f;
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      int num = 0;
      foreach (LightVisualElement child in this.Children)
      {
        RectangleF finalRect = new RectangleF(this.TrackBarElement.Orientation != Orientation.Horizontal ? new PointF(0.0f, (float) ((double) finalSize.Height - (double) (this.TrackBarElement.ThumbSize.Width / 2) - 2.0 - (double) (int) ((double) num * (double) this.TrackBarElement.TickOffSet) - (double) child.DesiredSize.Height / 2.0)) : (!this.RightToLeft ? new PointF((float) (this.TrackBarElement.ThumbSize.Width / 2 + (int) ((double) num * (double) this.TrackBarElement.TickOffSet)) - child.DesiredSize.Width / 2f, 0.0f) : new PointF((float) ((double) finalSize.Width - (double) (this.TrackBarElement.ThumbSize.Width / 2) - (double) (int) ((double) num * (double) this.TrackBarElement.TickOffSet) - (double) child.DesiredSize.Width / 2.0), 0.0f)), child.DesiredSize);
        child.Arrange(finalRect);
        num += this.largeTickFrequency;
      }
      return finalSize;
    }

    protected virtual void OnLabelFormatting(TrackBarLabelElement labelElement)
    {
      this.TrackBarElement.OnLabelFormatting(labelElement);
    }
  }
}
