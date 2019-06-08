// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.CheckPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Design;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class CheckPrimitive : RadioPrimitive
  {
    public static RadProperty CheckPrimitiveStyleProperty = RadProperty.Register(nameof (CheckPrimitiveStyle), typeof (CheckPrimitiveStyleEnum), typeof (CheckPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) CheckPrimitiveStyleEnum.XP, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DrawFillProperty = RadProperty.Register(nameof (DrawFill), typeof (bool), typeof (CheckPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty UseFixedCheckSizeProperty = RadProperty.Register(nameof (UseFixedCheckSize), typeof (bool), typeof (CheckPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty CheckThicknessProperty = RadProperty.Register(nameof (CheckThickness), typeof (int), typeof (CheckPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 3, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty VerticalOffestProperty = RadProperty.Register(nameof (VerticalOffest), typeof (int), typeof (CheckPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HorizontalOffestProperty = RadProperty.Register(nameof (HorizontalOffest), typeof (int), typeof (CheckPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SquareWidthAdjustProperty = RadProperty.Register(nameof (SquareWidthAdjust), typeof (int), typeof (CheckPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SquareHeightAdjustProperty = RadProperty.Register(nameof (SquareHeightAdjust), typeof (int), typeof (CheckPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    private ToggleState checkState = ToggleState.Indeterminate;

    [Description("Gets or sets a value indicating the style of the check box primitive.")]
    [RadPropertyDefaultValue("CheckPrimitiveStyle", typeof (CheckPrimitive))]
    public CheckPrimitiveStyleEnum CheckPrimitiveStyle
    {
      get
      {
        return (CheckPrimitiveStyleEnum) this.GetValue(CheckPrimitive.CheckPrimitiveStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(CheckPrimitive.CheckPrimitiveStyleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("DrawFill", typeof (CheckPrimitive))]
    [Description("Gets or sets a value indicating whether to draw the background.")]
    public bool DrawFill
    {
      get
      {
        return (bool) this.GetValue(CheckPrimitive.DrawFillProperty);
      }
      set
      {
        int num = (int) this.SetValue(CheckPrimitive.DrawFillProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("UseFixedCheckSize", typeof (CheckPrimitive))]
    [Description("Gets or sets a value that determines whether the checkmark size is fixed to 8;8 pixels.")]
    public bool UseFixedCheckSize
    {
      get
      {
        return (bool) this.GetValue(CheckPrimitive.UseFixedCheckSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(CheckPrimitive.UseFixedCheckSizeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("CheckThickness", typeof (CheckPrimitive))]
    [Description("Gets or sets a value that determines the checkmark thickness. Use this property only when UseFixedCheckSize property is set to false.")]
    public int CheckThickness
    {
      get
      {
        return (int) this.GetValue(CheckPrimitive.CheckThicknessProperty);
      }
      set
      {
        int num = (int) this.SetValue(CheckPrimitive.CheckThicknessProperty, (object) value);
      }
    }

    public ToggleState CheckState
    {
      get
      {
        return this.checkState;
      }
      set
      {
        this.checkState = value;
      }
    }

    [Description("Gets or sets a value that determines how the checkmark position in Indeterminate state will be adjusted vertical (in pixels).")]
    [RadPropertyDefaultValue("VerticalOffest", typeof (CheckPrimitive))]
    public int VerticalOffest
    {
      get
      {
        return (int) this.GetValue(CheckPrimitive.VerticalOffestProperty);
      }
      set
      {
        int num = (int) this.SetValue(CheckPrimitive.VerticalOffestProperty, (object) value);
      }
    }

    [Description("Gets or sets a value that determines how the checkmark position in Indeterminate will be adjusted horizontal (in pixels).")]
    [RadPropertyDefaultValue("HorizontalOffest", typeof (CheckPrimitive))]
    public int HorizontalOffest
    {
      get
      {
        return (int) this.GetValue(CheckPrimitive.HorizontalOffestProperty);
      }
      set
      {
        int num = (int) this.SetValue(CheckPrimitive.HorizontalOffestProperty, (object) value);
      }
    }

    [Description("Gets or sets a value that determines how the checkmark width in Indeterminate state will be adjusted (in pixels).")]
    [RadPropertyDefaultValue("SquareWidthAdjust", typeof (CheckPrimitive))]
    public int SquareWidthAdjust
    {
      get
      {
        return (int) this.GetValue(CheckPrimitive.SquareWidthAdjustProperty);
      }
      set
      {
        int num = (int) this.SetValue(CheckPrimitive.SquareWidthAdjustProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("SquareHeightAdjust", typeof (CheckPrimitive))]
    [Description("Gets or sets a value that determines how the checkmark height in Indeterminate state will be adjusted (in pixels).")]
    public int SquareHeightAdjust
    {
      get
      {
        return (int) this.GetValue(CheckPrimitive.SquareHeightAdjustProperty);
      }
      set
      {
        int num = (int) this.SetValue(CheckPrimitive.SquareHeightAdjustProperty, (object) value);
      }
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      if (this.DrawFill)
        base.PaintPrimitive(graphics, angle, scale);
      RectangleF rectangleF1 = new RectangleF((float) this.Padding.Left, (float) this.Padding.Top, (float) (this.Size.Width - this.Padding.Horizontal), (float) (this.Size.Height - this.Padding.Vertical));
      PointF pointF = new PointF(rectangleF1.Width / 2f, rectangleF1.Height / 2f);
      PointF[] points = (PointF[]) null;
      int num1 = (int) Math.Round(3.0 * (double) this.DpiScaleFactor.Width);
      if (!this.UseFixedCheckSize)
        num1 = (int) Math.Round((double) this.CheckThickness * (double) this.DpiScaleFactor.Width);
      for (int index = 0; index < num1; ++index)
      {
        if (this.UseFixedCheckSize)
        {
          using (GraphicsPath path = new GraphicsPath())
          {
            switch (this.CheckPrimitiveStyle)
            {
              case CheckPrimitiveStyleEnum.XP:
                points = new PointF[3]
                {
                  new PointF(pointF.X - 3f * this.DpiScaleFactor.Width, pointF.Y + 1f * this.DpiScaleFactor.Height - (float) index),
                  new PointF(pointF.X - 1f * this.DpiScaleFactor.Width, pointF.Y + 3f * this.DpiScaleFactor.Height - (float) index),
                  new PointF(pointF.X + 3f * this.DpiScaleFactor.Width, pointF.Y - 1f * this.DpiScaleFactor.Height - (float) index)
                };
                break;
              case CheckPrimitiveStyleEnum.Vista:
                points = new PointF[3]
                {
                  new PointF(pointF.X - 4f * this.DpiScaleFactor.Width, pointF.Y - (float) index),
                  new PointF(pointF.X - 1f * this.DpiScaleFactor.Width, pointF.Y + 3f * this.DpiScaleFactor.Height - (float) index),
                  new PointF(pointF.X + 3.8f * this.DpiScaleFactor.Width, (float) ((double) pointF.Y - (double) index - 3.0 * (double) this.DpiScaleFactor.Height))
                };
                break;
              case CheckPrimitiveStyleEnum.Mac:
                points = new PointF[3]
                {
                  new PointF(pointF.X - 4f * this.DpiScaleFactor.Width, pointF.Y - (float) index),
                  new PointF(pointF.X - 1f * this.DpiScaleFactor.Width, pointF.Y + 3f * this.DpiScaleFactor.Width - (float) index),
                  new PointF(pointF.X + 5.6f * this.DpiScaleFactor.Width, (float) ((double) pointF.Y - (double) index - 8.0 * (double) this.DpiScaleFactor.Height))
                };
                break;
              case CheckPrimitiveStyleEnum.Win8:
                if (this.CheckState == ToggleState.Off)
                  return;
                if (this.CheckState == ToggleState.On)
                  points = new PointF[3]
                  {
                    new PointF(pointF.X - 4.5f * this.DpiScaleFactor.Width, (float) ((double) pointF.Y - (double) index + 0.5 * (double) this.DpiScaleFactor.Height)),
                    new PointF(pointF.X - 0.5f * this.DpiScaleFactor.Width, (float) ((double) pointF.Y - (double) index + 4.5 * (double) this.DpiScaleFactor.Height)),
                    new PointF(pointF.X + 4f * this.DpiScaleFactor.Width, (float) ((double) pointF.Y - (double) index - 3.0 * (double) this.DpiScaleFactor.Height))
                  };
                if (this.CheckState == ToggleState.Indeterminate)
                {
                  double num2 = Math.Round((double) (5 + this.SquareWidthAdjust) * (double) this.DpiScaleFactor.Width, MidpointRounding.AwayFromZero);
                  double num3 = Math.Round((double) (5 + this.SquareWidthAdjust) * (double) this.DpiScaleFactor.Height, MidpointRounding.AwayFromZero);
                  RectangleF rectangleF2 = new RectangleF((float) Math.Round((double) this.HorizontalOffest * (double) this.DpiScaleFactor.Width + ((double) this.Size.Width - num2) / 2.0, MidpointRounding.AwayFromZero), (float) Math.Round((double) this.VerticalOffest * (double) this.DpiScaleFactor.Height + ((double) this.Size.Height - num3) / 2.0, MidpointRounding.AwayFromZero), (float) num2, (float) num3);
                  graphics.DrawRectangle(rectangleF2, this.ForeColor, PenAlignment.Center, 1f);
                  graphics.FillRectangle(rectangleF2, this.ForeColor);
                  return;
                }
                break;
            }
            if (points != null)
            {
              path.AddLines(points);
              graphics.DrawPath(path, this.ForeColor, PenAlignment.Center, 1f);
            }
          }
        }
        else
        {
          using (GraphicsPath path = new GraphicsPath())
          {
            switch (this.CheckPrimitiveStyle)
            {
              case CheckPrimitiveStyleEnum.XP:
                points = new PointF[3]
                {
                  new PointF(pointF.X - rectangleF1.Width / 4f, pointF.Y + rectangleF1.Height / 8f - (float) index),
                  new PointF(pointF.X - rectangleF1.Width / 8f, pointF.Y + rectangleF1.Height / 4f - (float) index),
                  new PointF(pointF.X + rectangleF1.Width / 4f, pointF.Y - rectangleF1.Height / 8f - (float) index)
                };
                break;
              case CheckPrimitiveStyleEnum.Vista:
                points = new PointF[3]
                {
                  new PointF(pointF.X - rectangleF1.Width / 4f, pointF.Y - (float) index),
                  new PointF(pointF.X - rectangleF1.Width / 8f, pointF.Y + rectangleF1.Height / 4f - (float) index),
                  new PointF((float) ((double) pointF.X + (double) rectangleF1.Width / 4.0 + (double) rectangleF1.Width / 8.0 * 0.800000011920929 - 1.0), (float) ((double) pointF.Y - (double) index - (double) rectangleF1.Height / 4.0 + 1.0))
                };
                break;
              case CheckPrimitiveStyleEnum.Mac:
                points = new PointF[3]
                {
                  new PointF(pointF.X - rectangleF1.Width / 4f, pointF.Y - (float) index),
                  new PointF(pointF.X - rectangleF1.Width / 8f, pointF.Y + rectangleF1.Height / 4f - (float) index),
                  new PointF((float) ((double) pointF.X + (double) rectangleF1.Width / 4.0 + 1.60000002384186 - 1.0), (float) ((double) pointF.Y - (double) index - (double) rectangleF1.Height / 3.0 + 1.0))
                };
                break;
              case CheckPrimitiveStyleEnum.Win8:
                if (this.CheckState == ToggleState.Off)
                  return;
                if (this.CheckState == ToggleState.On)
                  points = new PointF[3]
                  {
                    new PointF(pointF.X - (float) ((double) rectangleF1.Width / 16.0 * 4.5), (float) ((double) pointF.Y - (double) index + (double) rectangleF1.Height / 16.0 * 0.5)),
                    new PointF(pointF.X - (float) ((double) rectangleF1.Width / 16.0 * 0.5), pointF.Y + (float) ((double) rectangleF1.Height / 16.0 * 4.5) - (float) index),
                    new PointF(pointF.X + (float) ((double) rectangleF1.Width / 16.0 * 4.0), (float) ((double) pointF.Y - (double) index - (double) rectangleF1.Height / 16.0 * 3.0))
                  };
                if (this.CheckState == ToggleState.Indeterminate)
                {
                  double num2 = Math.Round((double) rectangleF1.Width / 16.0 * 5.0, MidpointRounding.AwayFromZero);
                  double num3 = Math.Round((double) rectangleF1.Height / 16.0 * 5.0, MidpointRounding.AwayFromZero);
                  RectangleF rectangleF2 = new RectangleF((float) (Math.Round((double) rectangleF1.Width / 2.0, MidpointRounding.AwayFromZero) - num2 / 2.0 + (double) this.HorizontalOffest * (double) this.DpiScaleFactor.Width), (float) (Math.Round((double) rectangleF1.Height / 2.0, MidpointRounding.AwayFromZero) - num3 / 2.0 + (double) this.VerticalOffest * (double) this.DpiScaleFactor.Height), (float) (num2 + (double) this.SquareWidthAdjust * (double) this.DpiScaleFactor.Width), (float) (int) num3 + (float) this.SquareHeightAdjust * this.DpiScaleFactor.Height);
                  graphics.DrawRectangle(rectangleF2, this.ForeColor, PenAlignment.Center, 1f);
                  graphics.FillRectangle(rectangleF2, this.ForeColor);
                  return;
                }
                break;
            }
            if (points != null)
            {
              path.AddLines(points);
              graphics.DrawPath(path, this.ForeColor, PenAlignment.Center, 1f);
            }
          }
        }
      }
    }

    protected override Rectangle GetClientRect()
    {
      return new Rectangle(this.Padding.Left, this.Padding.Top, this.Size.Width - this.Padding.Horizontal, this.Size.Height - this.Padding.Vertical);
    }
  }
}
