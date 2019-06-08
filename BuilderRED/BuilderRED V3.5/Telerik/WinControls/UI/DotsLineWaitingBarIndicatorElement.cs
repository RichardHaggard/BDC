// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DotsLineWaitingBarIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class DotsLineWaitingBarIndicatorElement : BaseWaitingBarIndicatorElement
  {
    public static RadProperty DotRadiusProperty = RadProperty.Register(nameof (DotRadius), typeof (int), typeof (DotsLineWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AccelerationSpeedProperty = RadProperty.Register(nameof (AccelerationSpeed), typeof (double), typeof (DotsLineWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5.0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DistanceBetweenDotsProperty = RadProperty.Register(nameof (DistanceBetweenDots), typeof (double), typeof (DotsLineWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 3.0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SlowSpeedRangeProperty = RadProperty.Register(nameof (SlowSpeedRange), typeof (int), typeof (DotsLineWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 42, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty WaitingDirectionProperty = RadProperty.Register(nameof (WaitingDirection), typeof (ProgressOrientation), typeof (DotsLineWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ProgressOrientation.Right, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DelayBetweenAnimationCyclesProperty = RadProperty.Register(nameof (DelayBetweenAnimationCycles), typeof (int), typeof (DotsLineWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 10, ElementPropertyOptions.AffectsDisplay));
    private IList<float> dotPositions;
    private float minDistanceBetweenDots;
    private int currentEmptyAnimationCycleIndex;

    public DotsLineWaitingBarIndicatorElement()
    {
      this.dotPositions = this.CreateDotPositions();
      this.CalculateDotDistances();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.currentEmptyAnimationCycleIndex = -1;
    }

    public bool IsVertical
    {
      get
      {
        ProgressOrientation waitingDirection = this.WaitingDirection;
        if (waitingDirection != ProgressOrientation.Bottom)
          return waitingDirection == ProgressOrientation.Top;
        return true;
      }
    }

    [Category("Appearance")]
    [RadRange(0, 2147483647)]
    [RadPropertyDefaultValue("DotRadius", typeof (DotsLineWaitingBarIndicatorElement))]
    public int DotRadius
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(DotsLineWaitingBarIndicatorElement.DotRadiusProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(DotsLineWaitingBarIndicatorElement.DotRadiusProperty, (object) value);
      }
    }

    [RadRange(0, 50)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("AccelerationSpeed", typeof (DotsLineWaitingBarIndicatorElement))]
    public double AccelerationSpeed
    {
      get
      {
        return (double) this.GetValue(DotsLineWaitingBarIndicatorElement.AccelerationSpeedProperty);
      }
      set
      {
        int num = (int) this.SetValue(DotsLineWaitingBarIndicatorElement.AccelerationSpeedProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadRange(0, 2147483647)]
    [RadPropertyDefaultValue("DistanceBetweenDots", typeof (DotsLineWaitingBarIndicatorElement))]
    public double DistanceBetweenDots
    {
      get
      {
        return (double) this.GetValue(DotsLineWaitingBarIndicatorElement.DistanceBetweenDotsProperty) * (double) this.DpiScaleFactor.Width;
      }
      set
      {
        int num = (int) this.SetValue(DotsLineWaitingBarIndicatorElement.DistanceBetweenDotsProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadRange(0, 100)]
    [RadPropertyDefaultValue("SlowSpeedRange", typeof (DotsLineWaitingBarIndicatorElement))]
    public int SlowSpeedRange
    {
      get
      {
        return (int) this.GetValue(DotsLineWaitingBarIndicatorElement.SlowSpeedRangeProperty);
      }
      set
      {
        int num = (int) this.SetValue(DotsLineWaitingBarIndicatorElement.SlowSpeedRangeProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("WaitingDirection", typeof (DotsLineWaitingBarIndicatorElement))]
    public ProgressOrientation WaitingDirection
    {
      get
      {
        return (ProgressOrientation) this.GetValue(DotsLineWaitingBarIndicatorElement.WaitingDirectionProperty);
      }
      set
      {
        int num = (int) this.SetValue(DotsLineWaitingBarIndicatorElement.WaitingDirectionProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("DelayBetweenAnimationCycles", typeof (DotsLineWaitingBarIndicatorElement))]
    public int DelayBetweenAnimationCycles
    {
      get
      {
        return (int) this.GetValue(DotsLineWaitingBarIndicatorElement.DelayBetweenAnimationCyclesProperty);
      }
      set
      {
        int num = (int) this.SetValue(DotsLineWaitingBarIndicatorElement.DelayBetweenAnimationCyclesProperty, (object) value);
      }
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.Paint(((RadGdiGraphics) graphics).Graphics, this.Parent.BoundingRectangle);
    }

    public void Paint(Graphics graphics, Rectangle rectangle)
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      using (Brush brush = (Brush) new SolidBrush(this.ElementColor))
      {
        for (int dotIndex = 0; dotIndex < this.dotPositions.Count; ++dotIndex)
        {
          bool shouldPaintDot = true;
          PointF dotLocation = this.CalculateDotLocation(dotIndex, ref shouldPaintDot);
          if (shouldPaintDot)
            graphics.FillEllipse(brush, new RectangleF(dotLocation, new SizeF((float) this.DotRadius, (float) this.DotRadius)));
        }
      }
    }

    protected virtual PointF CalculateDotLocation(int dotIndex, ref bool shouldPaintDot)
    {
      PointF empty = PointF.Empty;
      float dotPosition = this.dotPositions[dotIndex];
      if ((double) dotPosition < 0.0)
      {
        shouldPaintDot = false;
        return (PointF) Point.Empty;
      }
      switch (this.WaitingDirection)
      {
        case ProgressOrientation.Top:
          empty.X = (float) (this.BoundingRectangle.X + this.BoundingRectangle.Width - this.DotRadius) / 2f;
          empty.Y = (float) this.BoundingRectangle.Bottom - dotPosition;
          break;
        case ProgressOrientation.Bottom:
          empty.X = (float) (this.BoundingRectangle.X + this.BoundingRectangle.Width - this.DotRadius) / 2f;
          empty.Y = (float) this.BoundingRectangle.Top + dotPosition;
          break;
        case ProgressOrientation.Left:
          empty.X = (float) this.BoundingRectangle.Right - dotPosition;
          empty.Y = (float) (this.BoundingRectangle.Y + this.BoundingRectangle.Height - this.DotRadius) / 2f;
          break;
        default:
          empty.X = (float) this.BoundingRectangle.Left + dotPosition;
          empty.Y = (float) (this.BoundingRectangle.Y + this.BoundingRectangle.Height - this.DotRadius) / 2f;
          break;
      }
      if (this.IsVertical)
      {
        if ((double) empty.Y < 0.0 || (double) this.BoundingRectangle.Height < (double) empty.Y)
          shouldPaintDot = false;
      }
      else if ((double) empty.X < 0.0 || (double) this.BoundingRectangle.Width < (double) empty.X)
        shouldPaintDot = false;
      return empty;
    }

    public override void Animate(int step)
    {
      if (this.ShouldSkipAnimationFrame())
        return;
      float num1 = -1f;
      bool flag1 = true;
      bool flag2 = false;
      for (int index = 0; index < this.dotPositions.Count; ++index)
      {
        float num2 = this.CalculateDotNextPosition(this.dotPositions[index], step);
        if (index > 0)
        {
          if ((double) this.dotPositions[index] < 0.0 && flag2)
            num2 = -1f - (float) this.AccelerationSpeed * this.minDistanceBetweenDots;
          else if ((double) num2 > (double) num1 - (double) this.minDistanceBetweenDots)
            num2 = num1 - this.minDistanceBetweenDots;
        }
        if (flag1 && 0.0 <= (double) num2 && (double) num2 <= (double) this.BoundingRectangle.Width)
          flag1 = false;
        flag2 = (double) this.dotPositions[index] < 0.0;
        this.dotPositions[index] = num2;
        num1 = num2;
      }
      if (!flag1)
        return;
      for (int index = 0; index < this.dotPositions.Count; ++index)
        this.dotPositions[index] = -1f;
      this.currentEmptyAnimationCycleIndex = 0;
    }

    public override void ResetAnimation()
    {
      this.CalculateDotDistances();
      this.dotPositions = this.CreateDotPositions();
    }

    private bool ShouldSkipAnimationFrame()
    {
      if (this.DelayBetweenAnimationCycles <= 0 || this.currentEmptyAnimationCycleIndex < 0)
        return false;
      ++this.currentEmptyAnimationCycleIndex;
      if (this.DelayBetweenAnimationCycles == this.currentEmptyAnimationCycleIndex)
        this.currentEmptyAnimationCycleIndex = -1;
      return true;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == DotsLineWaitingBarIndicatorElement.DotRadiusProperty)
      {
        if ((int) e.NewValue < 0)
          throw new ArgumentException("DotRadius cannot be negative.");
      }
      else if (e.Property == DotsLineWaitingBarIndicatorElement.AccelerationSpeedProperty)
      {
        double newValue = (double) e.NewValue;
        if (newValue < 0.0 || newValue > 50.0)
          throw new ArgumentException("AccelerationSpeed must be between 0 and 50.");
        this.ResetAnimation();
      }
      else if (e.Property == DotsLineWaitingBarIndicatorElement.DistanceBetweenDotsProperty)
      {
        if ((double) e.NewValue < 0.0)
          throw new ArgumentException("DistanceBetweenDots cannot be negative.");
        this.ResetAnimation();
      }
      else if (e.Property == DotsLineWaitingBarIndicatorElement.SlowSpeedRangeProperty)
      {
        int newValue = (int) e.NewValue;
        if (newValue < 0 || newValue > 100)
          throw new ArgumentException("SlowSpeedRange must be between 0 and 100.");
        this.ResetAnimation();
      }
      else if (e.Property == DotsLineWaitingBarIndicatorElement.WaitingDirectionProperty)
      {
        this.ResetAnimation();
      }
      else
      {
        if (e.Property != DotsLineWaitingBarIndicatorElement.DelayBetweenAnimationCyclesProperty)
          return;
        this.ResetAnimation();
      }
    }

    private float CalculateDotNextPosition(float dotPosition, int step)
    {
      float num = dotPosition;
      return !this.IsDotInMaxSpeed(dotPosition) ? num + (float) step : num + (float) step * (float) this.AccelerationSpeed;
    }

    protected virtual bool IsDotInMaxSpeed(float dotPosition)
    {
      if ((double) dotPosition < 0.0)
        return true;
      float num1 = this.WaitingDirection == ProgressOrientation.Bottom || this.WaitingDirection == ProgressOrientation.Top ? (float) this.BoundingRectangle.Height : (float) this.BoundingRectangle.Width;
      float num2 = (float) ((double) num1 * (double) (100 - this.SlowSpeedRange) / 200.0);
      float num3 = (float) ((double) num1 * (double) this.SlowSpeedRange / 100.0) + num2;
      return (double) dotPosition < (double) num2 || (double) num3 < (double) dotPosition;
    }

    protected virtual void CalculateDotDistances()
    {
      this.minDistanceBetweenDots = (float) this.DotRadius + (float) this.DistanceBetweenDots;
    }

    protected virtual IList<float> CreateDotPositions()
    {
      List<float> floatList = new List<float>(this.ElementCount);
      for (int index = 0; index < this.ElementCount; ++index)
        floatList.Add(-1f);
      return (IList<float>) floatList;
    }

    public override void UpdateWaitingDirection(ProgressOrientation waitingDirection)
    {
      this.WaitingDirection = waitingDirection;
      base.UpdateWaitingDirection(waitingDirection);
    }
  }
}
