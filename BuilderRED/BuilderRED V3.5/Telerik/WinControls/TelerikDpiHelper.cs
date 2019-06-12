// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TelerikDpiHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class TelerikDpiHelper
  {
    public static int ScaleInt(int value, SizeF scaleFactor)
    {
      return (int) Math.Round((double) value * (double) scaleFactor.Width, MidpointRounding.AwayFromZero);
    }

    public static float ScaleFloat(float value, SizeF scaleFactor)
    {
      return value * scaleFactor.Width;
    }

    public static double ScaleDouble(double value, SizeF scaleFactor)
    {
      return value * (double) scaleFactor.Width;
    }

    public static Padding ScalePadding(Padding value, SizeF scaleFactor)
    {
      return new Padding((int) Math.Round((double) value.Left * (double) scaleFactor.Width, MidpointRounding.AwayFromZero), (int) Math.Round((double) value.Top * (double) scaleFactor.Height, MidpointRounding.AwayFromZero), (int) Math.Round((double) value.Right * (double) scaleFactor.Width, MidpointRounding.AwayFromZero), (int) Math.Round((double) value.Bottom * (double) scaleFactor.Height, MidpointRounding.AwayFromZero));
    }

    public static Size ScaleSize(Size value, SizeF scaleFactor)
    {
      return new Size((int) Math.Round((double) value.Width * (double) scaleFactor.Width, MidpointRounding.AwayFromZero), (int) Math.Round((double) value.Height * (double) scaleFactor.Height, MidpointRounding.AwayFromZero));
    }

    public static SizeF ScaleSizeF(SizeF value, SizeF scaleFactor)
    {
      return new SizeF(value.Width * scaleFactor.Width, value.Height * scaleFactor.Height);
    }

    public static Point ScalePoint(Point value, SizeF scaleFactor)
    {
      return new Point((int) Math.Round((double) value.X * (double) scaleFactor.Width, MidpointRounding.AwayFromZero), (int) Math.Round((double) value.Y * (double) scaleFactor.Height, MidpointRounding.AwayFromZero));
    }

    public static PointF ScalePointF(PointF value, SizeF scaleFactor)
    {
      return new PointF(value.X * scaleFactor.Width, value.Y * scaleFactor.Height);
    }

    public static Rectangle ScaleRectangle(Rectangle value, SizeF scaleFactor)
    {
      return new Rectangle(TelerikDpiHelper.ScalePoint(value.Location, scaleFactor), TelerikDpiHelper.ScaleSize(value.Size, scaleFactor));
    }

    public static RectangleF ScaleRectangleF(RectangleF value, SizeF scaleFactor)
    {
      return new RectangleF(TelerikDpiHelper.ScalePointF(value.Location, scaleFactor), TelerikDpiHelper.ScaleSizeF(value.Size, scaleFactor));
    }
  }
}
