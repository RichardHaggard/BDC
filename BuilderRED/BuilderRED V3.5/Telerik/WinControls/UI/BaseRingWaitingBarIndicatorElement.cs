// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseRingWaitingBarIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public abstract class BaseRingWaitingBarIndicatorElement : BaseWaitingBarIndicatorElement
  {
    public static RadProperty InnerRadiusProperty = RadProperty.Register(nameof (InnerRadius), typeof (int), typeof (BaseRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 8, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RadiusProperty = RadProperty.Register(nameof (Radius), typeof (int), typeof (BaseRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InitialStartElementAngleProperty = RadProperty.Register(nameof (InitialStartElementAngle), typeof (int), typeof (BaseRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RotationDirectionProperty = RadProperty.Register(nameof (RotationDirection), typeof (RotationDirection), typeof (BaseRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RotationDirection.Clockwise, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ElementGradientPercentageProperty = RadProperty.Register(nameof (ElementGradientPercentage), typeof (float), typeof (BaseRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.5f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ElementGradientPercentage2Property = RadProperty.Register(nameof (ElementGradientPercentage2), typeof (float), typeof (BaseRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.666f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ElementColor3Property = RadProperty.Register(nameof (ElementColor3), typeof (Color), typeof (BaseRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(101, 137, 191), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ElementColor4Property = RadProperty.Register(nameof (ElementColor4), typeof (Color), typeof (BaseRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Transparent, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ElementNumberOfColorsProperty = RadProperty.Register(nameof (ElementNumberOfColors), typeof (int), typeof (BaseRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.AffectsDisplay));
    private int currentLeadElementAngle;

    [RadRange(0, 2147483647)]
    [RadPropertyDefaultValue("InnerRadius", typeof (BaseRingWaitingBarIndicatorElement))]
    [Category("Appearance")]
    public virtual int InnerRadius
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(BaseRingWaitingBarIndicatorElement.InnerRadiusProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(BaseRingWaitingBarIndicatorElement.InnerRadiusProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("Radius", typeof (BaseRingWaitingBarIndicatorElement))]
    public virtual int Radius
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(BaseRingWaitingBarIndicatorElement.RadiusProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(BaseRingWaitingBarIndicatorElement.RadiusProperty, (object) value);
      }
    }

    [RadRange(0, 360)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("InitialStartElementAngle", typeof (BaseRingWaitingBarIndicatorElement))]
    public virtual int InitialStartElementAngle
    {
      get
      {
        return (int) this.GetValue(BaseRingWaitingBarIndicatorElement.InitialStartElementAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseRingWaitingBarIndicatorElement.InitialStartElementAngleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("RotationDirection", typeof (BaseRingWaitingBarIndicatorElement))]
    [Category("Appearance")]
    public virtual RotationDirection RotationDirection
    {
      get
      {
        return (RotationDirection) this.GetValue(BaseRingWaitingBarIndicatorElement.RotationDirectionProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseRingWaitingBarIndicatorElement.RotationDirectionProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadRange(0, 1)]
    [RadPropertyDefaultValue("ElementGradientPercentage", typeof (BaseRingWaitingBarIndicatorElement))]
    public virtual float ElementGradientPercentage
    {
      get
      {
        return (float) this.GetValue(BaseRingWaitingBarIndicatorElement.ElementGradientPercentageProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseRingWaitingBarIndicatorElement.ElementGradientPercentageProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ElementGradientPercentage2", typeof (BaseRingWaitingBarIndicatorElement))]
    [RadRange(0, 1)]
    [Category("Appearance")]
    public virtual float ElementGradientPercentage2
    {
      get
      {
        return (float) this.GetValue(BaseRingWaitingBarIndicatorElement.ElementGradientPercentage2Property);
      }
      set
      {
        int num = (int) this.SetValue(BaseRingWaitingBarIndicatorElement.ElementGradientPercentage2Property, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("ElementColor3", typeof (BaseRingWaitingBarIndicatorElement))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color ElementColor3
    {
      get
      {
        return (Color) this.GetValue(BaseRingWaitingBarIndicatorElement.ElementColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(BaseRingWaitingBarIndicatorElement.ElementColor3Property, (object) value);
      }
    }

    [Category("Appearance")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [RadPropertyDefaultValue("ElementColor4", typeof (BaseRingWaitingBarIndicatorElement))]
    public virtual Color ElementColor4
    {
      get
      {
        return (Color) this.GetValue(BaseRingWaitingBarIndicatorElement.ElementColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(BaseRingWaitingBarIndicatorElement.ElementColor4Property, (object) value);
      }
    }

    [RadPropertyDefaultValue("ElementNumberOfColors", typeof (BaseRingWaitingBarIndicatorElement))]
    [RadRange(1, 4)]
    [Category("Appearance")]
    public virtual int ElementNumberOfColors
    {
      get
      {
        return (int) this.GetValue(BaseRingWaitingBarIndicatorElement.ElementNumberOfColorsProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseRingWaitingBarIndicatorElement.ElementNumberOfColorsProperty, (object) value);
      }
    }

    protected virtual int CurrentLeadingElementAngle
    {
      get
      {
        return this.currentLeadElementAngle;
      }
      set
      {
        if (value < 0 || 360 < value)
          throw new ArgumentException("CurrentLeadingElementAngle must be between 0 and 360.");
        this.currentLeadElementAngle = value;
      }
    }

    public abstract void Paint(Graphics graphics, Rectangle rectangle);

    public override void Animate(int step)
    {
      if (this.RotationDirection == RotationDirection.Clockwise)
        step *= -1;
      this.CurrentLeadingElementAngle = this.ValidateAngle(this.CurrentLeadingElementAngle + step);
    }

    public override void ResetAnimation()
    {
      this.CurrentLeadingElementAngle = this.InitialStartElementAngle;
    }

    public override void UpdateWaitingDirection(ProgressOrientation waitingDirection)
    {
      switch (waitingDirection)
      {
        case ProgressOrientation.Top:
        case ProgressOrientation.Left:
          this.RotationDirection = RotationDirection.CounterClockwise;
          break;
        default:
          this.RotationDirection = RotationDirection.Clockwise;
          break;
      }
      base.UpdateWaitingDirection(waitingDirection);
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.Paint(((RadGdiGraphics) graphics).Graphics, this.BoundingRectangle);
    }

    protected Point GetRectangleCenter(Rectangle rect)
    {
      return new Point(rect.X + (int) ((double) rect.Width / 2.0), rect.Y + (int) ((double) rect.Height / 2.0));
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == BaseRingWaitingBarIndicatorElement.InnerRadiusProperty)
      {
        if ((double) (int) e.NewValue < 0.0)
          throw new ArgumentException("InnerRadius cannot be negative.");
      }
      else if (e.Property == BaseRingWaitingBarIndicatorElement.RadiusProperty)
      {
        if ((double) (int) e.NewValue < 0.0)
          throw new ArgumentException("Radius cannot be negative.");
      }
      else if (e.Property == BaseRingWaitingBarIndicatorElement.InitialStartElementAngleProperty)
      {
        int newValue = (int) e.NewValue;
        if (newValue < 0 || 360 < newValue)
          throw new ArgumentException("InitialStartElementAngle must be between 0 and 360.");
        this.ResetAnimation();
      }
      else
      {
        if (e.Property != BaseRingWaitingBarIndicatorElement.RotationDirectionProperty)
          return;
        this.ResetAnimation();
      }
    }

    internal int ValidateAngle(int angle)
    {
      return (int) this.ValidateAngle((double) angle);
    }

    internal double ValidateAngle(double angle)
    {
      angle %= 360.0;
      angle += angle < 0.0 ? 360.0 : 0.0;
      return angle;
    }

    internal PointF PointFromCenter(PointF center, double radius, double angle)
    {
      double num = angle * Math.PI / 180.0;
      PointF pt = new PointF((float) (radius * Math.Cos(num)), (float) (-1.0 * radius * Math.Sin(num)));
      return PointF.Add(center, new SizeF(pt));
    }

    internal RectangleF GetRectangleFromRadius(PointF center, float radius)
    {
      if ((double) radius == 0.0)
        return RectangleF.Empty;
      return new RectangleF(center.X - radius, center.Y - radius, radius + radius, radius + radius);
    }
  }
}
