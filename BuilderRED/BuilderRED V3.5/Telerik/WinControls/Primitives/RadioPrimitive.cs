// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.RadioPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class RadioPrimitive : FillPrimitive
  {
    public static RadProperty UseParentShapeProperty = RadProperty.Register(nameof (UseParentShape), typeof (bool), typeof (RadioPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MinSize = new Size(13, 13);
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.BackColor = Color.Black;
      this.BackColor2 = Color.Black;
      this.ForeColor = Color.Black;
    }

    [RadPropertyDefaultValue("UseParentShape", typeof (RadioPrimitive))]
    public bool UseParentShape
    {
      get
      {
        return (bool) this.GetValue(RadioPrimitive.UseParentShapeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadioPrimitive.UseParentShapeProperty, (object) value);
      }
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      if (!this.CheckForValidParameters())
        return;
      int val2 = 4;
      Color[] colorStops = new Color[Math.Min(Math.Max(this.NumberOfColors, 1), val2)];
      float[] colorOffsets = new float[Math.Min(Math.Max(this.NumberOfColors, 1), val2)];
      Rectangle clientRect = this.GetClientRect();
      GraphicsPath path = this.CreatePath(clientRect);
      if (this.NumberOfColors > 0)
      {
        colorStops[0] = this.BackColor;
        colorOffsets[0] = 0.0f;
      }
      if (this.NumberOfColors > 1)
      {
        colorStops[1] = this.BackColor2;
        colorOffsets[colorStops.Length - 1] = 1f;
      }
      if (this.NumberOfColors > 2)
      {
        colorStops[2] = this.BackColor3;
        colorOffsets[1] = this.GradientPercentage;
      }
      if (this.NumberOfColors > 3)
      {
        colorStops[3] = this.BackColor4;
        colorOffsets[2] = this.GradientPercentage2;
      }
      this.PaintPrimitiveCore(graphics, clientRect, colorStops, colorOffsets, path);
    }

    protected virtual bool CheckForValidParameters()
    {
      return (this.BackColor.A != (byte) 0 || this.NumberOfColors > 1 && (this.BackColor2.A != (byte) 0 || this.NumberOfColors > 2 && (this.BackColor3.A != (byte) 0 || this.NumberOfColors > 3 && this.BackColor4.A != (byte) 0))) && (this.Size.Width > 0 && this.Size.Height > 0);
    }

    protected virtual GraphicsPath CreatePath(Rectangle rect)
    {
      if (this.UseParentShape && this.Parent != null && this.Parent.Shape != null)
        return this.Parent.Shape.CreatePath(rect);
      return (GraphicsPath) null;
    }

    protected virtual void PaintPrimitiveCore(
      IGraphics g,
      Rectangle rect,
      Color[] colorStops,
      float[] colorOffsets,
      GraphicsPath graphPath)
    {
      if (graphPath != null)
      {
        g.FillPath(colorStops, colorOffsets, this.GradientAngle, this.GradientPercentage, this.GradientPercentage2, rect, graphPath);
        g.DrawPath(graphPath, this.ForeColor, PenAlignment.Center, 1f);
        graphPath.Dispose();
      }
      else
      {
        g.FillGradientRectangle(rect, colorStops, colorOffsets, this.GradientStyle, this.GradientAngle, this.GradientPercentage, this.GradientPercentage2);
        g.DrawRectangle(rect, this.ForeColor);
      }
    }

    protected virtual Rectangle GetClientRect()
    {
      Rectangle rectangle = new Rectangle(Point.Empty, this.Size);
      return new Rectangle(rectangle.Width / 4, rectangle.Height / 4, rectangle.Width / 2, rectangle.Height / 2);
    }
  }
}
