// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.BrushFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Drawing;

namespace Telerik.WinControls.UI.Gauges
{
  public class BrushFactory : IBrushFactory
  {
    public Brush CreateBrush(GaugeVisualElement owner, GaugeBrushType type)
    {
      RadialGaugeArc owner1 = owner as RadialGaugeArc;
      if (owner1 == null)
        return (Brush) null;
      switch (type)
      {
        case GaugeBrushType.Rainbow:
          return this.CreateRainbowBrush(owner1);
        case GaugeBrushType.Rainbow2:
          return this.CreateRainbow2Brush(owner1);
        case GaugeBrushType.Rainbow3:
          return this.CreateRainbow3Brush(owner1);
        case GaugeBrushType.Gradient:
          return this.CreateGradientBrush(owner1);
        default:
          return this.CreateRainbowBrush(owner1);
      }
    }

    public Brush CreateGradientBrush(RadialGaugeArc owner)
    {
      float num = (float) owner.OutherRadiusPercentage;
      if ((double) num == 0.0)
        num = (float) (owner.Owner.LabelRadius - owner.Owner.LabelGap);
      switch (owner.NumberOfColors)
      {
        case 1:
          return (Brush) new SolidBrush(owner.BackColor);
        case 2:
          return (Brush) new GdiRadialGradientBrush(owner.Owner.GaugeCenter, num, num, new GradientStop[4]{ new GradientStop(owner.BackColor, 0.0f), new GradientStop(owner.BackColor2, owner.GradientPercentage), new GradientStop(owner.BackColor, owner.GradientPercentage2), new GradientStop(owner.BackColor, 1f) }).RawBrush;
        case 3:
          return (Brush) new GdiRadialGradientBrush(owner.Owner.GaugeCenter, num, num, new GradientStop[4]{ new GradientStop(owner.BackColor, 0.0f), new GradientStop(owner.BackColor2, owner.GradientPercentage), new GradientStop(owner.BackColor3, owner.GradientPercentage2), new GradientStop(owner.BackColor2, 1f) }).RawBrush;
        case 4:
          return (Brush) new GdiRadialGradientBrush(owner.Owner.GaugeCenter, num, num, new GradientStop[4]{ new GradientStop(owner.BackColor, 0.0f), new GradientStop(owner.BackColor2, owner.GradientPercentage), new GradientStop(owner.BackColor3, owner.GradientPercentage2), new GradientStop(owner.BackColor3, 1f) }).RawBrush;
        default:
          return (Brush) new SolidBrush(owner.BackColor);
      }
    }

    public Brush CreateRainbow2Brush(RadialGaugeArc owner)
    {
      float num = (float) (owner.Owner.LabelRadius - owner.Owner.LabelGap);
      List<PointF> pointFList = new List<PointF>(0);
      PointF pointF1 = new PointF(num + 10f, 0.0f);
      for (float startAngle = (float) owner.startAngle; (double) startAngle < owner.endAngle; ++startAngle)
      {
        Matrix matrix = new Matrix();
        matrix.Rotate(startAngle);
        PointF[] pts = new PointF[1]{ pointF1 };
        matrix.TransformPoints(pts);
        PointF pointF2 = new PointF(owner.Owner.GaugeCenter.X + pts[0].X, owner.Owner.GaugeCenter.Y + pts[0].Y);
        pointFList.Add(pointF2);
      }
      PathGradientBrush pathGradientBrush = new PathGradientBrush(pointFList.ToArray());
      pathGradientBrush.CenterColor = Color.Transparent;
      pathGradientBrush.CenterPoint = owner.Owner.GaugeCenter;
      Color[] baseColors = new Color[4]{ Color.Green, Color.Yellow, Color.Red, Color.Magenta };
      double[] colorFactors = new double[4]{ 0.0, 0.5, 0.9, 1.0 };
      Color[] colorArray = new Color[pointFList.Count];
      for (int index = 0; index < pointFList.Count; ++index)
      {
        Color color = BrushFactory.GetColor(Math.Abs(Math.Atan2((double) pointFList[index].Y - (double) owner.Owner.GaugeCenter.Y, (double) pointFList[index].X - (double) owner.Owner.GaugeCenter.X)) / Math.PI, baseColors, colorFactors);
        colorArray[index] = color;
      }
      pathGradientBrush.SurroundColors = colorArray;
      return (Brush) pathGradientBrush;
    }

    public Brush CreateRainbow3Brush(RadialGaugeArc owner)
    {
      float num = (float) (owner.Owner.LabelRadius - owner.Owner.LabelGap);
      List<PointF> pointFList = new List<PointF>(0);
      PointF pointF1 = new PointF(num + 10f, 0.0f);
      for (float startAngle = (float) owner.startAngle; (double) startAngle < owner.endAngle; ++startAngle)
      {
        Matrix matrix = new Matrix();
        matrix.Rotate(startAngle);
        PointF[] pts = new PointF[1]{ pointF1 };
        matrix.TransformPoints(pts);
        PointF pointF2 = new PointF(owner.Owner.GaugeCenter.X + pts[0].X, owner.Owner.GaugeCenter.Y + pts[0].Y);
        pointFList.Add(pointF2);
      }
      PathGradientBrush pathGradientBrush = new PathGradientBrush(pointFList.ToArray());
      pathGradientBrush.CenterColor = Color.Transparent;
      pathGradientBrush.CenterPoint = owner.Owner.GaugeCenter;
      Color[] baseColors = new Color[4]{ Color.Green, Color.Cyan, Color.Blue, Color.Magenta };
      double[] colorFactors = new double[4]{ 0.0, 0.5, 0.9, 1.0 };
      Color[] colorArray = new Color[pointFList.Count];
      for (int index = 0; index < pointFList.Count; ++index)
      {
        Color color = BrushFactory.GetColor(Math.Abs(Math.Atan2((double) pointFList[index].Y - (double) owner.Owner.GaugeCenter.Y, (double) pointFList[index].X - (double) owner.Owner.GaugeCenter.X)) / Math.PI, baseColors, colorFactors);
        colorArray[index] = color;
      }
      pathGradientBrush.SurroundColors = colorArray;
      return (Brush) pathGradientBrush;
    }

    public Brush CreateRainbowBrush(RadialGaugeArc owner)
    {
      float num = (float) (owner.Owner.LabelRadius - owner.Owner.LabelGap);
      List<PointF> pointFList = new List<PointF>(0);
      PointF pointF1 = new PointF(num + 10f, 0.0f);
      for (float startAngle = (float) owner.startAngle; (double) startAngle < owner.endAngle; ++startAngle)
      {
        Matrix matrix = new Matrix();
        matrix.Rotate(startAngle);
        PointF[] pts = new PointF[1]{ pointF1 };
        matrix.TransformPoints(pts);
        PointF pointF2 = new PointF(owner.Owner.GaugeCenter.X + pts[0].X, owner.Owner.GaugeCenter.Y + pts[0].Y);
        pointFList.Add(pointF2);
      }
      PathGradientBrush pathGradientBrush = new PathGradientBrush(pointFList.ToArray());
      pathGradientBrush.CenterColor = Color.Transparent;
      pathGradientBrush.CenterPoint = owner.Owner.GaugeCenter;
      Color[] baseColors = new Color[7]{ Color.Red, Color.Red, Color.Yellow, Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue, 0), Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue, 0), Color.Cyan, Color.Blue };
      double[] colorFactors = new double[7]{ 0.0, 0.25, 0.45, 0.55, 0.65, 0.85, 1.0 };
      Color[] colorArray = new Color[pointFList.Count];
      for (int index = 0; index < pointFList.Count; ++index)
      {
        Color color = BrushFactory.GetColor(Math.Abs(Math.Atan2((double) pointFList[index].Y - (double) owner.Owner.GaugeCenter.Y, (double) pointFList[index].X - (double) owner.Owner.GaugeCenter.X)) / Math.PI, baseColors, colorFactors);
        colorArray[index] = color;
      }
      pathGradientBrush.SurroundColors = colorArray;
      return (Brush) pathGradientBrush;
    }

    private static Color GetColor(double position, Color[] baseColors, double[] colorFactors)
    {
      int num1 = Array.BinarySearch<double>(colorFactors, position);
      int index1 = Math.Max(0, Math.Min(num1 < 0 ? ~num1 - 1 : num1 - 1, colorFactors.Length - 1));
      int index2 = Math.Min(index1 + 1, colorFactors.Length - 1);
      if (index1 == index2)
        return baseColors[index1];
      double num2 = (position - colorFactors[index1]) / (colorFactors[index2] - colorFactors[index1]);
      return Color.FromArgb((int) baseColors[index1].A + (int) ((double) ((int) baseColors[index2].A - (int) baseColors[index1].A) * num2), (int) baseColors[index1].R + (int) ((double) ((int) baseColors[index2].R - (int) baseColors[index1].R) * num2), (int) baseColors[index1].G + (int) ((double) ((int) baseColors[index2].G - (int) baseColors[index1].G) * num2), (int) baseColors[index1].B + (int) ((double) ((int) baseColors[index2].B - (int) baseColors[index1].B) * num2));
    }
  }
}
