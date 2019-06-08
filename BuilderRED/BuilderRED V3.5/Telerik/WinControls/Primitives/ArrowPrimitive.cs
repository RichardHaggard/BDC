// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.ArrowPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class ArrowPrimitive : BasePrimitive
  {
    public static readonly RadProperty DirectionProperty = RadProperty.Register(nameof (Direction), typeof (ArrowDirection), typeof (ArrowPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ArrowDirection.Down, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static readonly Size MinHorizontalSize = new Size(4, 8);
    public static readonly Size MinVerticalSize = new Size(8, 4);

    public ArrowPrimitive()
    {
    }

    public ArrowPrimitive(ArrowDirection arrowDirection)
    {
      this.Direction = arrowDirection;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.SmoothingMode = SmoothingMode.AntiAlias;
      this.Direction = ArrowDirection.Down;
      this.ResetSizesByDirection(ArrowDirection.Down, true);
    }

    [DefaultValue(false)]
    public override bool StretchHorizontally
    {
      get
      {
        return base.StretchHorizontally;
      }
      set
      {
        base.StretchHorizontally = value;
      }
    }

    [DefaultValue(false)]
    public override bool StretchVertically
    {
      get
      {
        return base.StretchVertically;
      }
      set
      {
        base.StretchVertically = value;
      }
    }

    [RadPropertyDefaultValue("Direction", typeof (ArrowPrimitive))]
    [Description("Appearance")]
    public ArrowDirection Direction
    {
      get
      {
        return (ArrowDirection) this.GetValue(ArrowPrimitive.DirectionProperty);
      }
      set
      {
        int num = (int) this.SetValue(ArrowPrimitive.DirectionProperty, (object) value);
      }
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      Rectangle rectangle = new Rectangle(Point.Empty, this.Size);
      graphics.ChangeSmoothingMode(SmoothingMode.None);
      switch (this.Direction)
      {
        case ArrowDirection.Left:
          graphics.FillPolygon(this.ForeColor, new Point[3]
          {
            new Point(rectangle.Width, rectangle.Top),
            new Point(rectangle.Left, rectangle.Height / 2),
            new Point(rectangle.Width, rectangle.Height)
          });
          break;
        case ArrowDirection.Up:
          graphics.FillPolygon(this.ForeColor, new Point[3]
          {
            new Point(rectangle.Left, rectangle.Height),
            new Point(rectangle.Width / 2, rectangle.Top - 1),
            new Point(rectangle.Width, rectangle.Height)
          });
          break;
        case ArrowDirection.Right:
          graphics.FillPolygon(this.ForeColor, new Point[3]
          {
            new Point(rectangle.Left, rectangle.Top),
            new Point(rectangle.Width, rectangle.Height / 2),
            new Point(rectangle.Left, rectangle.Height)
          });
          break;
        case ArrowDirection.Down:
          graphics.FillPolygon(this.ForeColor, new Point[3]
          {
            new Point(rectangle.Left + 1, rectangle.Top),
            new Point(rectangle.Width / 2, rectangle.Height),
            new Point(rectangle.Width, rectangle.Top)
          });
          break;
      }
      graphics.RestoreSmoothingMode();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      SizeF empty = SizeF.Empty;
      return SizeF.Add(this.Direction == ArrowDirection.Left || this.Direction == ArrowDirection.Right ? (SizeF) TelerikDpiHelper.ScaleSize(ArrowPrimitive.MinHorizontalSize, this.DpiScaleFactor) : (SizeF) TelerikDpiHelper.ScaleSize(ArrowPrimitive.MinVerticalSize, this.DpiScaleFactor), (SizeF) this.Padding.Size);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.AutoSizeProperty)
      {
        this.Size = this.MinSize;
      }
      else
      {
        if (e.Property != ArrowPrimitive.DirectionProperty)
          return;
        this.ResetSizesByDirection(this.Direction, this.AutoSize);
      }
    }

    private void ResetSizesByDirection(ArrowDirection newDirection, bool setMinSize)
    {
      this.SuspendPropertyNotifications();
      if (newDirection == ArrowDirection.Left || newDirection == ArrowDirection.Right)
      {
        if (setMinSize)
          this.MinSize = ArrowPrimitive.MinHorizontalSize;
        else
          this.Size = ArrowPrimitive.MinHorizontalSize;
      }
      else if (setMinSize)
        this.MinSize = ArrowPrimitive.MinVerticalSize;
      else
        this.Size = ArrowPrimitive.MinVerticalSize;
      this.ResumePropertyNotifications();
    }
  }
}
