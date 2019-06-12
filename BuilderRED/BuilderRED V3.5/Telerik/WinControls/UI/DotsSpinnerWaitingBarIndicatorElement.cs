// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DotsSpinnerWaitingBarIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class DotsSpinnerWaitingBarIndicatorElement : BaseRingWaitingBarIndicatorElement
  {
    public static RadProperty AccelerationSpeedProperty = RadProperty.Register(nameof (AccelerationSpeed), typeof (double), typeof (DotsSpinnerWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 10.0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DotRadiusProperty = RadProperty.Register(nameof (DotRadius), typeof (int), typeof (DotsSpinnerWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MaxSpeedSweepAngleProperty = RadProperty.Register(nameof (MaxSpeedSweepAngle), typeof (int), typeof (DotsSpinnerWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 210, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DotSweepAngleLifeCycleProperty = RadProperty.Register(nameof (DotSweepAngleLifeCycle), typeof (int), typeof (DotsSpinnerWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 720, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DistanceBetweenDotsProperty = RadProperty.Register(nameof (DistanceBetweenDots), typeof (double), typeof (DotsSpinnerWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2.0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DelayBetweenAnimationCyclesProperty = RadProperty.Register(nameof (DelayBetweenAnimationCycles), typeof (int), typeof (DotsSpinnerWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsDisplay));
    private IList<int> dotAnglePositions;
    private int minAngleBetweenDots;
    private int minAngleStep;
    private int currentEmptyAnimationCycleIndex;

    public DotsSpinnerWaitingBarIndicatorElement()
    {
      this.dotAnglePositions = this.CreateDotAnglePositions();
      this.CalculateDotAngles();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.currentEmptyAnimationCycleIndex = -1;
      this.minAngleStep = 1;
    }

    [RadPropertyDefaultValue("DotRadius", typeof (DotsSpinnerWaitingBarIndicatorElement))]
    [RadRange(0, 2147483647)]
    [Category("Appearance")]
    public virtual int DotRadius
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(DotsSpinnerWaitingBarIndicatorElement.DotRadiusProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(DotsSpinnerWaitingBarIndicatorElement.DotRadiusProperty, (object) value);
      }
    }

    [RadRange(0, 100)]
    [RadPropertyDefaultValue("AccelerationSpeed", typeof (DotsSpinnerWaitingBarIndicatorElement))]
    [Category("Appearance")]
    public double AccelerationSpeed
    {
      get
      {
        return (double) this.GetValue(DotsSpinnerWaitingBarIndicatorElement.AccelerationSpeedProperty);
      }
      set
      {
        int num = (int) this.SetValue(DotsSpinnerWaitingBarIndicatorElement.AccelerationSpeedProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("MaxSpeedSweepAngle", typeof (DotsSpinnerWaitingBarIndicatorElement))]
    [RadRange(0, 360)]
    [Category("Appearance")]
    public virtual int MaxSpeedSweepAngle
    {
      get
      {
        return (int) this.GetValue(DotsSpinnerWaitingBarIndicatorElement.MaxSpeedSweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(DotsSpinnerWaitingBarIndicatorElement.MaxSpeedSweepAngleProperty, (object) value);
      }
    }

    [RadRange(0, 2147483647)]
    [RadPropertyDefaultValue("DotSweepAngleLifeCycle", typeof (DotsSpinnerWaitingBarIndicatorElement))]
    [Category("Appearance")]
    public virtual int DotSweepAngleLifeCycle
    {
      get
      {
        return (int) this.GetValue(DotsSpinnerWaitingBarIndicatorElement.DotSweepAngleLifeCycleProperty);
      }
      set
      {
        int num = (int) this.SetValue(DotsSpinnerWaitingBarIndicatorElement.DotSweepAngleLifeCycleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("DistanceBetweenDots", typeof (DotsSpinnerWaitingBarIndicatorElement))]
    [RadRange(0, 2147483647)]
    [Category("Appearance")]
    public double DistanceBetweenDots
    {
      get
      {
        return (double) this.GetValue(DotsSpinnerWaitingBarIndicatorElement.DistanceBetweenDotsProperty) * (double) this.DpiScaleFactor.Width;
      }
      set
      {
        int num = (int) this.SetValue(DotsSpinnerWaitingBarIndicatorElement.DistanceBetweenDotsProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("DelayBetweenAnimationCycles", typeof (DotsSpinnerWaitingBarIndicatorElement))]
    public int DelayBetweenAnimationCycles
    {
      get
      {
        return (int) this.GetValue(DotsSpinnerWaitingBarIndicatorElement.DelayBetweenAnimationCyclesProperty);
      }
      set
      {
        int num = (int) this.SetValue(DotsSpinnerWaitingBarIndicatorElement.DelayBetweenAnimationCyclesProperty, (object) value);
      }
    }

    protected virtual int CalculateDotNextAngle(int angle, int step)
    {
      step = Math.Max(step, this.minAngleStep);
      int num = angle;
      return !this.IsDotInMaxSpeed(angle) ? num + step : num + (int) ((double) step * this.AccelerationSpeed);
    }

    private bool IsDotInMaxSpeed(int dotAngle)
    {
      if (dotAngle < 0)
        return true;
      int rotatedAngle = this.GetRotatedAngle(dotAngle);
      int num = this.InitialStartElementAngle - this.MaxSpeedSweepAngle < 0 ? this.InitialStartElementAngle + 360 : this.InitialStartElementAngle;
      return rotatedAngle <= num && rotatedAngle + this.MaxSpeedSweepAngle / 2 >= num || rotatedAngle >= this.InitialStartElementAngle && rotatedAngle - this.MaxSpeedSweepAngle / 2 <= this.InitialStartElementAngle;
    }

    private int GetRotatedAngle(int angle)
    {
      int startElementAngle = this.InitialStartElementAngle;
      switch (this.RotationDirection)
      {
        case RotationDirection.Clockwise:
          startElementAngle -= angle;
          break;
        case RotationDirection.CounterClockwise:
          startElementAngle += angle;
          break;
      }
      int num = startElementAngle % 360;
      return num + (num < 0 ? 360 : 0);
    }

    public override void Paint(Graphics graphics, Rectangle rectangle)
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      using (Brush brush = (Brush) new SolidBrush(this.ElementColor))
      {
        for (int dotIndex = 0; dotIndex < this.dotAnglePositions.Count; ++dotIndex)
        {
          bool shouldPaintDot = true;
          PointF dotLocation = this.CalculateDotLocation(dotIndex, ref shouldPaintDot);
          if (shouldPaintDot)
            graphics.FillEllipse(brush, new RectangleF(dotLocation, new SizeF((float) this.DotRadius, (float) this.DotRadius)));
        }
      }
    }

    private PointF CalculateDotLocation(int dotIndex, ref bool shouldPaintDot)
    {
      int dotAnglePosition = this.dotAnglePositions[dotIndex];
      if (dotAnglePosition < 0 || dotAnglePosition > this.DotSweepAngleLifeCycle)
      {
        shouldPaintDot = false;
        return (PointF) Point.Empty;
      }
      int rotatedAngle = this.GetRotatedAngle(this.dotAnglePositions[dotIndex]);
      double num = (double) rotatedAngle * Math.PI / 180.0;
      PointF pt = new PointF((float) this.Radius * (float) Math.Cos(num), (float) (-1 * this.Radius) * (float) Math.Sin(num));
      pt = PointF.Add(pt, new Size(this.GetRectangleCenter(this.BoundingRectangle)));
      pt.X -= (float) this.DotRadius / 2f;
      pt.Y -= (float) this.DotRadius / 2f;
      if (dotIndex == 0)
        this.CurrentLeadingElementAngle = rotatedAngle;
      return pt;
    }

    public override void Animate(int step)
    {
      if (this.ShouldSkipAnimationFrame())
        return;
      base.Animate(step);
      int num1 = -1;
      bool flag1 = true;
      bool flag2 = false;
      for (int index = 0; index < this.dotAnglePositions.Count; ++index)
      {
        int num2 = this.CalculateDotNextAngle(this.dotAnglePositions[index], step);
        if (index > 0)
        {
          if (this.dotAnglePositions[index] < 0 && flag2)
            num2 = -1 - (int) (this.AccelerationSpeed * (double) this.minAngleBetweenDots);
          else if (num2 > num1 - this.minAngleBetweenDots)
            num2 = num1 - this.minAngleBetweenDots;
        }
        if (flag1 && 0 < num2 && num2 <= this.DotSweepAngleLifeCycle)
          flag1 = false;
        flag2 = this.dotAnglePositions[index] < 0;
        this.dotAnglePositions[index] = num2;
        num1 = num2;
      }
      if (!flag1)
        return;
      for (int index = 0; index < this.dotAnglePositions.Count; ++index)
        this.dotAnglePositions[index] = -1;
      this.currentEmptyAnimationCycleIndex = 0;
    }

    public override void ResetAnimation()
    {
      base.ResetAnimation();
      this.CalculateDotAngles();
      this.dotAnglePositions = this.CreateDotAnglePositions();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == DotsSpinnerWaitingBarIndicatorElement.DotRadiusProperty)
      {
        if ((int) e.NewValue < 0)
          throw new ArgumentException("DotRadius cannot be negative.");
        this.ResetAnimation();
      }
      else if (e.Property == DotsSpinnerWaitingBarIndicatorElement.AccelerationSpeedProperty)
      {
        double newValue = (double) e.NewValue;
        if (newValue < 0.0 || 100.0 < newValue)
          throw new ArgumentException("AccelerationSpeed must be between 0 and 100.");
        this.ResetAnimation();
      }
      else if (e.Property == DotsSpinnerWaitingBarIndicatorElement.MaxSpeedSweepAngleProperty)
      {
        int newValue = (int) e.NewValue;
        if (newValue < 0 || 360 < newValue)
          throw new ArgumentException("MaxSpeedSweepAngle must be between 0 and 360");
        this.ResetAnimation();
      }
      else if (e.Property == DotsSpinnerWaitingBarIndicatorElement.DotSweepAngleLifeCycleProperty)
      {
        if ((int) e.NewValue < 0)
          throw new ArgumentException("DotSweepAngleLifeCycle must be greater than 0.");
        this.ResetAnimation();
      }
      else if (e.Property == DotsSpinnerWaitingBarIndicatorElement.DistanceBetweenDotsProperty)
      {
        if ((double) e.NewValue < 0.0)
          throw new ArgumentException("DistanceBetweenDots cannot be negative.");
        this.ResetAnimation();
      }
      else
      {
        if (e.Property != DotsSpinnerWaitingBarIndicatorElement.DelayBetweenAnimationCyclesProperty)
          return;
        this.ResetAnimation();
      }
    }

    protected virtual IList<int> CreateDotAnglePositions()
    {
      List<int> intList = new List<int>(this.ElementCount);
      for (int index = 0; index < this.ElementCount; ++index)
        intList.Add(-1);
      return (IList<int>) intList;
    }

    private void CalculateDotAngles()
    {
      this.minAngleBetweenDots = (int) ((this.DistanceBetweenDots + (double) this.DotRadius) / (double) this.Radius * 180.0 / Math.PI);
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
  }
}
