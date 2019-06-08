// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.OverflowPrimitive
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
  public class OverflowPrimitive : BasePrimitive
  {
    public static readonly RadProperty ArrowDirectionProperty = RadProperty.Register("ArrowDirection", typeof (ArrowDirection), typeof (OverflowPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ArrowDirection.Down, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty ShadowColorProperty = RadProperty.Register(nameof (ShadowColor), typeof (Color), typeof (OverflowPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty DrawArrowProperty = RadProperty.Register(nameof (DrawArrow), typeof (bool), typeof (OverflowPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static readonly Size MinHorizontalSize = new Size(4, 8);
    public static readonly Size MinVerticalSize = new Size(8, 10);

    public OverflowPrimitive(ArrowDirection arrowDirection)
    {
      this.Direction = arrowDirection;
      if (this.Direction == ArrowDirection.Left || this.Direction == ArrowDirection.Right)
        this.Size = OverflowPrimitive.MinHorizontalSize;
      else
        this.Size = OverflowPrimitive.MinVerticalSize;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
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

    [RadPropertyDefaultValue("ArrowDirection", typeof (OverflowPrimitive))]
    [Category("Appearance")]
    public ArrowDirection Direction
    {
      get
      {
        return (ArrowDirection) this.GetValue(OverflowPrimitive.ArrowDirectionProperty);
      }
      set
      {
        int num = (int) this.SetValue(OverflowPrimitive.ArrowDirectionProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ShadowColor", typeof (OverflowPrimitive))]
    [Category("Appearance")]
    public Color ShadowColor
    {
      get
      {
        return (Color) this.GetValue(OverflowPrimitive.ShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(OverflowPrimitive.ShadowColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("DrawArrow", typeof (OverflowPrimitive))]
    [Category("Appearance")]
    public bool DrawArrow
    {
      get
      {
        return (bool) this.GetValue(OverflowPrimitive.DrawArrowProperty);
      }
      set
      {
        int num = (int) this.SetValue(OverflowPrimitive.DrawArrowProperty, (object) value);
      }
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      if (!this.DrawArrow)
        return;
      Rectangle rectangle = new Rectangle(Point.Empty, this.Size);
      graphics.ChangeSmoothingMode(SmoothingMode.AntiAlias);
      switch (this.Direction)
      {
        case ArrowDirection.Left:
          graphics.FillPolygon(this.ForeColor, new Point[3]
          {
            new Point(rectangle.Width - 3, 0),
            new Point(0, rectangle.Height / 2),
            new Point(rectangle.Width - 3, rectangle.Height)
          });
          graphics.DrawLine(this.BackColor, rectangle.Width - 1, 0, rectangle.Width - 1, rectangle.Height - 1);
          graphics.DrawLine(this.ForeColor, rectangle.Width - 2, 0, rectangle.Width - 2, rectangle.Height);
          break;
        case ArrowDirection.Up:
          graphics.FillPolygon(this.ForeColor, new Point[3]
          {
            new Point(0, rectangle.Height - 3),
            new Point(rectangle.Width / 2, 0),
            new Point(rectangle.Width, rectangle.Height - 3)
          });
          graphics.DrawLine(this.ForeColor, rectangle.Width / 2, rectangle.Height - 3, 0, rectangle.Width);
          graphics.DrawLine(this.ForeColor, 0, rectangle.Height - 1, rectangle.Width, rectangle.Height - 1);
          graphics.DrawLine(this.BackColor, 1, rectangle.Height - 2, rectangle.Width - 1, rectangle.Height - 2);
          break;
        case ArrowDirection.Right:
          graphics.FillPolygon(this.ForeColor, new Point[3]
          {
            new Point(3, 0),
            new Point(rectangle.Width, rectangle.Height / 2),
            new Point(3, rectangle.Height)
          });
          graphics.DrawLine(this.ForeColor, 1, 0, 1, rectangle.Height);
          break;
        case ArrowDirection.Down:
          graphics.FillPolygon(this.ShadowColor, new Point[3]
          {
            new Point(0, 5),
            new Point((rectangle.Width - 1) / 2 + 1, rectangle.Height),
            new Point(rectangle.Width, 5)
          });
          graphics.FillPolygon(this.ForeColor, new Point[3]
          {
            new Point(0, 4),
            new Point((rectangle.Width - 1) / 2, rectangle.Height - 1),
            new Point(rectangle.Width - 1, 4)
          });
          graphics.DrawLine(this.ForeColor, 0, 1, rectangle.Width - 1, 1);
          graphics.DrawLine(this.ShadowColor, 1, 2, rectangle.Width, 2);
          break;
      }
      graphics.RestoreSmoothingMode();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == ArrowPrimitive.DirectionProperty)
      {
        if (this.Direction == ArrowDirection.Left || this.Direction == ArrowDirection.Right)
        {
          if (this.AutoSize)
            this.MinSize = OverflowPrimitive.MinHorizontalSize;
          else
            this.Size = OverflowPrimitive.MinHorizontalSize;
        }
        else if (this.AutoSize)
          this.MinSize = OverflowPrimitive.MinVerticalSize;
        else
          this.Size = OverflowPrimitive.MinVerticalSize;
      }
      base.OnPropertyChanged(e);
    }
  }
}
