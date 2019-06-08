// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.MeasurementControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls
{
  public sealed class MeasurementControl : RadControl
  {
    [ThreadStatic]
    private static MeasurementControl instance;

    private MeasurementControl()
    {
      this.LoadElementTree();
    }

    public override void RegisterHostedControl(RadHostItem hostElement)
    {
    }

    public override void UnregisterHostedControl(RadHostItem hostElement, bool removeControl)
    {
    }

    public SizeF GetDesiredSize(RadElement element, SizeF availableSize)
    {
      element.SuspendThemeRefresh();
      RadElement parent = element.Parent;
      int index = -1;
      if (parent != null)
        index = parent.Children.IndexOf(element);
      ElementVisibility visibility = element.Visibility;
      if (visibility != ElementVisibility.Visible)
        element.Visibility = ElementVisibility.Visible;
      this.RootElement.Children.Add(element);
      element.ResetLayout(true);
      element.Measure(availableSize);
      SizeF desiredSize = element.GetDesiredSize(false);
      if (parent != null)
        parent.Children.Insert(index, element);
      else
        this.RootElement.Children.Remove(element);
      if (visibility != ElementVisibility.Visible)
        element.Visibility = visibility;
      element.ResumeThemeRefresh();
      return desiredSize;
    }

    public void GetAsBitmapEx(
      Graphics graphics,
      RadElement element,
      SizeF availableSize,
      SizeF finalSize,
      Brush brush,
      float totalAngle,
      SizeF totalScale)
    {
      this.GetAsBitmapEx(graphics, true, element, availableSize, finalSize, brush, totalAngle, totalScale);
    }

    public Bitmap GetAsBitmapEx(
      RadElement element,
      SizeF availableSize,
      SizeF finalSize,
      Brush brush,
      float totalAngle,
      SizeF totalScale)
    {
      return this.GetAsBitmapEx(true, element, availableSize, finalSize, brush, totalAngle, totalScale);
    }

    public void GetAsBitmapEx(
      Graphics graphics,
      bool doArrangeAndMeasure,
      RadElement element,
      SizeF availableSize,
      SizeF finalSize,
      Brush brush,
      float totalAngle,
      SizeF totalScale)
    {
      RadElement parent = element.Parent;
      int index = -1;
      if (parent != null)
        index = parent.Children.IndexOf(element);
      this.RootElement.Children.Add(element);
      if (doArrangeAndMeasure)
      {
        element.SetBitState(8L, true);
        element.ResetLayout(true);
        element.Measure(availableSize);
        element.Arrange(new RectangleF(new PointF(0.0f, 0.0f), finalSize));
        element.SetBitState(8L, false);
      }
      this.GetAsBitmapEx(graphics, element, brush, totalAngle, totalScale);
      if (parent != null)
        parent.Children.Insert(index, element);
      else
        this.RootElement.Children.Remove(element);
    }

    public Bitmap GetAsBitmapEx(
      bool doArrangeAndMeasure,
      RadElement element,
      SizeF availableSize,
      SizeF finalSize,
      Brush brush,
      float totalAngle,
      SizeF totalScale)
    {
      RadElement parent = element.Parent;
      int index = -1;
      if (parent != null)
        index = parent.Children.IndexOf(element);
      this.RootElement.Children.Add(element);
      if (doArrangeAndMeasure)
      {
        element.ResetLayout(true);
        element.Measure(availableSize);
        element.Arrange(new RectangleF(new PointF(0.0f, 0.0f), finalSize));
      }
      Bitmap asBitmapEx = element.GetAsBitmapEx(brush, totalAngle, totalScale);
      if (parent != null)
        parent.Children.Insert(index, element);
      else
        this.RootElement.Children.Remove(element);
      return asBitmapEx;
    }

    public Bitmap GetAsBitmapEx(
      RadElement element,
      Brush backColor,
      float totalAngle,
      SizeF totalScale)
    {
      Size size = element.Size;
      if (size.Width <= 0 || size.Height <= 0)
        return (Bitmap) null;
      Bitmap bitmap = new Bitmap(size.Width, size.Height);
      using (Graphics memoryGraphics = Graphics.FromImage((Image) bitmap))
        this.GetAsBitmapEx(memoryGraphics, element, backColor, totalAngle, totalScale);
      return bitmap;
    }

    public void GetAsBitmapEx(
      Graphics memoryGraphics,
      RadElement element,
      Brush backColor,
      float totalAngle,
      SizeF totalScale)
    {
      Size size = element.Size;
      if (size.Width <= 0 || size.Height <= 0)
        return;
      memoryGraphics.FillRectangle(backColor, new Rectangle(Point.Empty, size));
      memoryGraphics.SmoothingMode = SmoothingMode.HighQuality;
      memoryGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      memoryGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      memoryGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      RadGdiGraphics radGdiGraphics = new RadGdiGraphics(memoryGraphics);
      element.SetBitState(8L, true);
      element.Paint((IGraphics) radGdiGraphics, new Rectangle(Point.Empty, size)
      {
        Location = element.LocationToControl()
      }, totalAngle, totalScale, true);
      element.SetBitState(8L, false);
    }

    public static MeasurementControl ThreadInstance
    {
      get
      {
        if (MeasurementControl.instance == null)
          MeasurementControl.instance = new MeasurementControl();
        return MeasurementControl.instance;
      }
    }
  }
}
