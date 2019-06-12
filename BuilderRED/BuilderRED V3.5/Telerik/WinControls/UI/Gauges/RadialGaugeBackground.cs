// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.RadialGaugeBackground
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI.Gauges
{
  public class RadialGaugeBackground : GaugeVisualElement, IDrawing
  {
    private double extrusionPercentage = 40.0;
    private double radiusOffset = 100.0;
    private RadRadialGaugeElement owner;

    public RadialGaugeBackground()
    {
      this.DrawFill = true;
      this.DrawText = false;
    }

    public RadialGaugeBackground(RadRadialGaugeElement owner)
      : this()
    {
      this.owner = owner;
      this.NumberOfColors = 3;
      this.GradientStyle = GradientStyles.Linear;
      this.BackColor = Color.LightYellow;
      this.BackColor2 = Color.Yellow;
      this.BackColor3 = Color.DarkMagenta;
      this.Shape = (ElementShape) new RadialGaugeBackground.GaugeBackgroundShape(owner, this.extrusionPercentage, this.radiusOffset);
      this.ZIndex = 1000;
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent == null)
        return;
      this.owner = (RadRadialGaugeElement) this.Parent;
      this.Shape = (ElementShape) new RadialGaugeBackground.GaugeBackgroundShape(this.owner, this.extrusionPercentage, this.radiusOffset);
    }

    public double RadiusOffsetPercentage
    {
      get
      {
        return this.radiusOffset;
      }
      set
      {
        this.radiusOffset = value;
        this.OnNotifyPropertyChanged("RadiusOffset");
      }
    }

    public double ExtrusionPercentage
    {
      get
      {
        return this.extrusionPercentage;
      }
      set
      {
        this.extrusionPercentage = value;
        this.OnNotifyPropertyChanged(nameof (ExtrusionPercentage));
      }
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (!(propertyName == "ExtrusionPercentage") && !(propertyName == "RadiusOffset"))
        return;
      this.Shape = (ElementShape) new RadialGaugeBackground.GaugeBackgroundShape(this.owner, this.extrusionPercentage, this.radiusOffset);
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
    }

    public void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      float radius1 = (float) (this.owner.LabelRadius - this.owner.LabelGap);
      RectangleF rectangleFromRadius = this.GetRectangleFromRadius(radius1);
      double num = 12.0;
      double radius2 = num + ((double) radius1 - num) * this.extrusionPercentage / 100.0;
      PointF pt1_1 = this.owner.PointFromCenter(this.owner.GaugeCenter, (double) radius1, this.owner.StartAngle);
      PointF pt2_1 = this.owner.PointFromCenter(this.owner.GaugeCenter, radius2, this.owner.StartAngle);
      PointF pt2_2 = this.owner.PointFromCenter(this.owner.GaugeCenter, radius2, this.owner.EndAngle);
      PointF pt1_2 = this.owner.PointFromCenter(this.owner.GaugeCenter, (double) radius1, this.owner.EndAngle);
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddArc(rectangleFromRadius, (float) this.owner.StartAngle, (float) this.owner.SweepAngle);
        path.AddLine(pt1_2, pt2_2);
        path.AddArc(this.GetRectangleFromRadius((float) radius2), (float) this.owner.EndAngle, 360f - (float) this.owner.SweepAngle);
        path.AddLine(pt1_1, pt2_1);
        graphics.FillPath(Brushes.LightBlue, path);
        PathGradientBrush pathGradientBrush = new PathGradientBrush(path);
        pathGradientBrush.CenterPoint = this.owner.GaugeCenter;
        pathGradientBrush.CenterColor = Color.Wheat;
        pathGradientBrush.SurroundColors = new Color[1]
        {
          Color.BlueViolet
        };
        pathGradientBrush.SetBlendTriangularShape(0.5f, 1f);
        pathGradientBrush.FocusScales = new PointF(0.0f, 0.0f);
        graphics.FillPath((Brush) pathGradientBrush, path);
      }
    }

    public override GraphicsPath Path
    {
      get
      {
        float radius1 = (float) (this.owner.LabelRadius - this.owner.LabelGap);
        RectangleF rectangleFromRadius = this.GetRectangleFromRadius(radius1);
        double num = 12.0;
        double radius2 = num + ((double) radius1 - num) * this.extrusionPercentage / 100.0;
        PointF pt1_1 = this.owner.PointFromCenter(this.owner.GaugeCenter, (double) radius1, this.owner.StartAngle);
        PointF pt2_1 = this.owner.PointFromCenter(this.owner.GaugeCenter, radius2, this.owner.StartAngle);
        PointF pt2_2 = this.owner.PointFromCenter(this.owner.GaugeCenter, radius2, this.owner.EndAngle);
        PointF pt1_2 = this.owner.PointFromCenter(this.owner.GaugeCenter, (double) radius1, this.owner.EndAngle);
        GraphicsPath graphicsPath = new GraphicsPath();
        graphicsPath.AddArc(rectangleFromRadius, (float) this.owner.StartAngle, (float) this.owner.SweepAngle);
        graphicsPath.AddLine(pt1_2, pt2_2);
        graphicsPath.AddArc(this.GetRectangleFromRadius((float) radius2), (float) this.owner.EndAngle, 360f - (float) this.owner.SweepAngle);
        graphicsPath.AddLine(pt1_1, pt2_1);
        return graphicsPath;
      }
    }

    private RectangleF GetRectangleFromRadius(float radius)
    {
      return new RectangleF(this.owner.GaugeCenter.X - radius, this.owner.GaugeCenter.Y - radius, radius + radius, radius + radius);
    }

    public class GaugeBackgroundShape : ElementShape
    {
      private readonly RadRadialGaugeElement owner;
      private readonly double extrusionPercentage;
      private readonly double radiusOffset;

      public GaugeBackgroundShape(
        RadRadialGaugeElement owner,
        double extrusionPercentage,
        double radiusOffset)
      {
        this.owner = owner;
        this.extrusionPercentage = extrusionPercentage;
        this.radiusOffset = radiusOffset;
      }

      public override GraphicsPath CreatePath(Rectangle bounds)
      {
        if (this.owner == null)
          return (GraphicsPath) null;
        float radius1 = (float) ((this.owner.LabelRadius - this.owner.LabelGap) * this.radiusOffset / 100.0);
        RectangleF rectangleFromRadius = this.owner.GetRectangleFromRadius(radius1);
        double num = 12.0;
        double radius2 = num + ((double) radius1 - num) * this.extrusionPercentage / 100.0;
        PointF pt1_1 = this.owner.PointFromCenter(this.owner.GaugeCenter, (double) radius1, this.owner.StartAngle);
        PointF pt2_1 = this.owner.PointFromCenter(this.owner.GaugeCenter, radius2, this.owner.StartAngle);
        PointF pt2_2 = this.owner.PointFromCenter(this.owner.GaugeCenter, radius2, this.owner.EndAngle);
        PointF pt1_2 = this.owner.PointFromCenter(this.owner.GaugeCenter, (double) radius1, this.owner.EndAngle);
        GraphicsPath graphicsPath = new GraphicsPath();
        graphicsPath.AddArc(rectangleFromRadius, (float) this.owner.StartAngle, (float) this.owner.SweepAngle);
        graphicsPath.AddLine(pt1_2, pt2_2);
        graphicsPath.AddArc(this.owner.GetRectangleFromRadius((float) radius2), (float) this.owner.EndAngle, 360f - (float) this.owner.SweepAngle);
        graphicsPath.AddLine(pt1_1, pt2_1);
        this.MirrorPath(graphicsPath, (RectangleF) bounds);
        return graphicsPath;
      }
    }
  }
}
