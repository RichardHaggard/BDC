// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.LinearGaugeBrushFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.UI.Gauges
{
  public class LinearGaugeBrushFactory : IBrushFactory
  {
    public Brush CreateBrush(GaugeVisualElement owner, GaugeBrushType type)
    {
      LinearGaugeBar bar = owner as LinearGaugeBar;
      if (bar == null)
        return (Brush) null;
      switch (type)
      {
        case GaugeBrushType.Other:
          return (Brush) null;
        case GaugeBrushType.Rainbow:
        case GaugeBrushType.Rainbow2:
        case GaugeBrushType.Rainbow3:
          return this.CreateRainbowBrush(bar);
        case GaugeBrushType.Gradient:
          if (bar.BackColor == bar.BackColor2)
            return (Brush) new SolidBrush(bar.BackColor);
          return (Brush) new LinearGradientBrush(bar.Path.GetBounds(), bar.BackColor, bar.BackColor2, 90f);
        default:
          return (Brush) null;
      }
    }

    private Brush CreateRainbowBrush(LinearGaugeBar bar)
    {
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bar.BoundingRectangle, Color.Black, Color.Black, bar.Owner.Vertical ? 90f : 180f, false);
      ColorBlend colorBlend = new ColorBlend();
      colorBlend.Positions = new float[9];
      int num1 = 0;
      for (float num2 = 0.0f; (double) num2 <= 1.0; num2 += 0.125f)
        colorBlend.Positions[num1++] = num2;
      colorBlend.Colors = new Color[9]
      {
        Color.Red,
        Color.Orange,
        Color.Yellow,
        Color.Green,
        Color.LightBlue,
        Color.Blue,
        Color.Indigo,
        Color.Violet,
        Color.Red
      };
      linearGradientBrush.InterpolationColors = colorBlend;
      return (Brush) linearGradientBrush;
    }
  }
}
