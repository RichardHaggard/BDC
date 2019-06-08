// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorSelectionRectangle
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorSelectionRectangle : RangeSelectorVisualElementWithOrientation
  {
    private bool releaseSelctionRectangle = true;
    private bool enableSelectionRectangle = true;
    private double valueOne;
    private double valueTwo;
    private int minSelectionLenght;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = true;
      this.BackColor = Color.Aquamarine;
      this.GradientStyle = GradientStyles.Solid;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.BorderWidth = 0.0f;
      this.ShouldHandleMouseInput = true;
      this.StretchVertically = true;
      this.ZIndex = 1;
    }

    public double ValueOne
    {
      get
      {
        return this.valueOne;
      }
      set
      {
        this.valueOne = value;
        this.RangeSelectorElement.BodyElement.ViewContainer.InvalidateMeasure(true);
      }
    }

    public double ValueTwo
    {
      get
      {
        return this.valueTwo;
      }
      set
      {
        this.valueTwo = value;
        this.RangeSelectorElement.BodyElement.ViewContainer.InvalidateMeasure(true);
      }
    }

    public bool ReleaseSelectionRectangle
    {
      get
      {
        return this.releaseSelctionRectangle;
      }
      set
      {
        this.releaseSelctionRectangle = value;
      }
    }

    public int MinSelectionLength
    {
      get
      {
        return this.minSelectionLenght;
      }
      set
      {
        this.minSelectionLenght = value;
      }
    }

    public bool EnableSelectionRectangle
    {
      get
      {
        return this.enableSelectionRectangle;
      }
      set
      {
        this.enableSelectionRectangle = value;
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (!this.EnableSelectionRectangle)
        return;
      this.Capture = true;
      this.RangeSelectorElement.IsMouseUp = true;
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
        this.PerformHorizontalMouseDown(e);
      else
        this.PerformVerticalMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (!this.releaseSelctionRectangle)
        return;
      float startValue;
      float endValue;
      if (Math.Abs(this.valueOne - this.valueTwo) < (double) this.RangeSelectorElement.MinSelectionLength)
      {
        startValue = (float) Math.Min(this.ValueOne, this.ValueTwo) - ((float) this.RangeSelectorElement.MinSelectionLength - (float) Math.Abs(this.valueOne - this.valueTwo)) / 2f;
        endValue = (float) Math.Max(this.ValueOne, this.ValueTwo) + ((float) this.RangeSelectorElement.MinSelectionLength - (float) Math.Abs(this.valueOne - this.valueTwo)) / 2f;
      }
      else
      {
        startValue = (float) Math.Min(this.ValueOne, this.ValueTwo);
        endValue = (float) Math.Max(this.ValueOne, this.ValueTwo);
      }
      if ((double) startValue < 0.0)
      {
        startValue = 0.0f;
        endValue = (float) this.RangeSelectorElement.MinSelectionLength;
      }
      if ((double) endValue > 100.0)
      {
        endValue = 100f;
        startValue = (float) (100 - this.RangeSelectorElement.MinSelectionLength);
      }
      RangeSelectorSelectionChangingEventArgs changingArgs = new RangeSelectorSelectionChangingEventArgs(startValue, endValue);
      this.OnSelectionChanging(changingArgs);
      if (changingArgs.Cancel)
        return;
      if ((double) startValue < (double) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.StartRange)
      {
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.StartRange = startValue;
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.EndRange = endValue;
      }
      else
      {
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.EndRange = endValue;
        this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.StartRange = startValue;
      }
      this.OnSelectionChanged(new EventArgs());
      this.RangeSelectorElement.IsMouseUp = false;
      this.valueOne = 0.0;
      this.valueTwo = 0.0;
      this.Capture = false;
      this.RangeSelectorElement.InvalidateMeasure(true);
      if (!(this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement))
        return;
      (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).UpdateAssociatedView();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      this.MoveSelection(e);
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

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      float num = 1f;
      if (!this.ReleaseSelectionRectangle)
        num = this.RangeSelectorElement.TotalZoomFactor;
      int x = this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.LocationToControl().X;
      float width = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.BoundingRectangle.Width;
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
        return new SizeF((float) (Math.Abs(this.valueOne - this.valueTwo) * (double) width / 100.0), availableSize.Height);
      return new SizeF(availableSize.Width, (float) (Math.Abs(this.valueOne - this.valueTwo) * ((double) availableSize.Height * (double) num) / 100.0));
    }

    internal void PerformSelctionClick(MouseEventArgs e)
    {
      this.OnMouseDown(e);
    }

    private void PerformHorizontalMouseDown(MouseEventArgs e)
    {
      float x = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.LocationToControl().X;
      float width = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.BoundingRectangle.Width;
      if (this.releaseSelctionRectangle)
      {
        this.ValueOne = ((double) e.Location.X - (double) x) / (double) width * 100.0;
        if (this.ValueOne + (double) this.MinSelectionLength <= 100.0)
          this.ValueTwo = this.ValueOne + (double) this.MinSelectionLength;
        else
          this.ValueTwo = this.ValueOne - (double) this.MinSelectionLength;
      }
      else if (this.valueOne == 0.0 && this.valueTwo == 0.0)
      {
        this.ValueOne = ((double) e.Location.X - (double) x) / (double) width * 100.0;
        if (this.ValueOne + (double) this.MinSelectionLength <= 100.0)
          this.ValueTwo = this.ValueOne + (double) this.MinSelectionLength;
        else
          this.ValueTwo = this.ValueOne - (double) this.MinSelectionLength;
      }
      else
      {
        float num = (float) ((double) ((float) e.Location.X - x) / (double) width * 100.0);
        if (Math.Abs((double) num - this.valueOne) <= Math.Abs((double) num - this.valueTwo))
          this.ValueOne = (double) num;
        else
          this.ValueTwo = (double) num;
      }
    }

    private void PerformVerticalMouseDown(MouseEventArgs e)
    {
      float y = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.LocationToControl().Y;
      float height = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.BoundingRectangle.Height;
      if (this.releaseSelctionRectangle)
      {
        this.ValueOne = 100.0 - (double) ((float) e.Location.Y - y) / (double) height * 100.0;
        if (this.ValueOne + (double) this.MinSelectionLength <= 100.0)
          this.ValueTwo = this.ValueOne + (double) this.MinSelectionLength;
        else
          this.ValueTwo = this.ValueOne - (double) this.MinSelectionLength;
      }
      else if (this.valueOne == 0.0 && this.valueTwo == 0.0)
      {
        this.ValueOne = 100.0 - (double) ((float) e.Location.Y - y) / (double) height * 100.0;
        if (this.ValueOne + (double) this.MinSelectionLength <= 100.0)
          this.ValueTwo = this.ValueOne + (double) this.MinSelectionLength;
        else
          this.ValueTwo = this.ValueOne - (double) this.MinSelectionLength;
      }
      else
      {
        float num = (float) (100.0 - (double) ((float) e.Location.Y - y) / (double) height * 100.0);
        if (Math.Abs((double) num - this.valueOne) <= Math.Abs((double) num - this.valueTwo))
          this.ValueOne = (double) num;
        else
          this.ValueTwo = (double) num;
      }
    }

    private void MoveSelection(MouseEventArgs e)
    {
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
        this.PerformHorizontalSelectionRectangleMove(e);
      else
        this.PerformVerticalSelectionRectangleMove(e);
    }

    private void PerformHorizontalSelectionRectangleMove(MouseEventArgs e)
    {
      float x = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.LocationToControl().X;
      float width = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.BoundingRectangle.Width;
      float num = (float) (((double) e.Location.X - (double) x) / (double) width * 100.0);
      if ((double) num < 0.0)
        num = 0.0f;
      if ((double) num > 100.0)
        num = 100f;
      if (this.ReleaseSelectionRectangle)
        this.ValueTwo = (double) num;
      else if (Math.Abs((double) num - this.valueOne) <= Math.Abs((double) num - this.valueTwo))
        this.ValueOne = (double) num;
      else
        this.ValueTwo = (double) num;
    }

    private void PerformVerticalSelectionRectangleMove(MouseEventArgs e)
    {
      float y = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.LocationToControl().Y;
      float height = (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.BoundingRectangle.Height;
      float num = (float) (100.0 - ((double) e.Location.Y - (double) y) / (double) height * 100.0);
      if ((double) num < 0.0)
        num = 0.0f;
      if ((double) num > 100.0)
        num = 100f;
      if (this.ReleaseSelectionRectangle)
        this.ValueTwo = (double) num;
      else if (Math.Abs((double) num - this.valueOne) <= Math.Abs((double) num - this.valueTwo))
        this.ValueOne = (double) num;
      else
        this.ValueTwo = (double) num;
    }
  }
}
