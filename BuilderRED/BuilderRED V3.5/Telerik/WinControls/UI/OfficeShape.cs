// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.OfficeShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class OfficeShape : ElementShape
  {
    private bool roundedBottom;

    public OfficeShape()
    {
    }

    public OfficeShape(bool roundedBottom)
    {
      this.roundedBottom = roundedBottom;
    }

    [Description("Gets or sets whether the bottom edges of the form should be rounded.")]
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool RoundedBottom
    {
      get
      {
        return this.roundedBottom;
      }
      set
      {
        this.roundedBottom = value;
      }
    }

    public GraphicsPath GetContourPath(Rectangle bounds)
    {
      GraphicsPath graphicsPath1 = new GraphicsPath();
      if (bounds.Height <= 0 || bounds.Width <= 0)
        return graphicsPath1;
      if (bounds.Height < 10 || bounds.Width < 10)
        return new RoundRectShape(5).CreatePath(bounds);
      GraphicsPath graphicsPath2 = new GraphicsPath();
      Point[] points = !this.RoundedBottom ? this.GetCutBottomContour(bounds) : this.GetRoundedBottomContour(bounds);
      graphicsPath2.AddLines(points);
      return graphicsPath2;
    }

    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      if (bounds.Height <= 0 || bounds.Width <= 0)
        return graphicsPath;
      if (bounds.Height < 10 || bounds.Width < 10)
        return new RoundRectShape(5).CreatePath(bounds);
      Rectangle[] rectangles = this.GetRectangles(bounds);
      graphicsPath.AddRectangles(rectangles);
      return graphicsPath;
    }

    protected override Rectangle GetBounds(RadElement element)
    {
      return new Rectangle(Point.Empty, element.Size);
    }

    public override string SerializeProperties()
    {
      return this.RoundedBottom.ToString();
    }

    public override void DeserializeProperties(string propertiesString)
    {
      if (string.IsNullOrEmpty(propertiesString))
        return;
      this.RoundedBottom = bool.Parse(propertiesString);
    }

    private Rectangle[] GetRectangles(Rectangle bounds)
    {
      Rectangle[] rectangleArray;
      if (this.RoundedBottom)
        rectangleArray = new Rectangle[7]
        {
          new Rectangle(bounds.X + 4, bounds.Y, bounds.Width - 8, 1),
          new Rectangle(bounds.X + 2, bounds.Y + 1, bounds.Width - 4, 1),
          new Rectangle(bounds.X + 1, bounds.Y + 2, bounds.Width - 2, 2),
          new Rectangle(bounds.X, bounds.Y + 4, bounds.Width, bounds.Height - 8),
          new Rectangle(bounds.X + 1, bounds.Bottom - 4, bounds.Width - 2, 2),
          new Rectangle(bounds.X + 2, bounds.Bottom - 2, bounds.Width - 4, 1),
          new Rectangle(bounds.X + 4, bounds.Bottom - 1, bounds.Width - 8, 1)
        };
      else
        rectangleArray = new Rectangle[4]
        {
          new Rectangle(bounds.X + 4, bounds.Y, bounds.Width - 8, 1),
          new Rectangle(bounds.X + 2, bounds.Y + 1, bounds.Width - 4, 1),
          new Rectangle(bounds.X + 1, bounds.Y + 2, bounds.Width - 2, 2),
          new Rectangle(bounds.X, bounds.Y + 4, bounds.Width, bounds.Height - 4)
        };
      return rectangleArray;
    }

    private Point[] GetRoundedBottomContour(Rectangle bounds)
    {
      return new Point[25]{ new Point(bounds.X + 4, bounds.Y), new Point(bounds.X + 3, bounds.Y + 1), new Point(bounds.X + 2, bounds.Y + 1), new Point(bounds.X + 1, bounds.Y + 2), new Point(bounds.X + 1, bounds.Y + 3), new Point(bounds.X, bounds.Y + 4), new Point(bounds.X, bounds.Bottom - 4), new Point(bounds.X + 1, bounds.Bottom - 3), new Point(bounds.X + 1, bounds.Bottom - 2), new Point(bounds.X + 2, bounds.Bottom - 1), new Point(bounds.X + 3, bounds.Bottom - 1), new Point(bounds.X + 4, bounds.Bottom), new Point(bounds.Right - 4, bounds.Bottom), new Point(bounds.Right - 3, bounds.Bottom - 1), new Point(bounds.Right - 2, bounds.Bottom - 1), new Point(bounds.Right - 1, bounds.Bottom - 2), new Point(bounds.Right - 1, bounds.Bottom - 3), new Point(bounds.Right, bounds.Bottom - 4), new Point(bounds.Right, bounds.Y + 4), new Point(bounds.Right - 1, bounds.Y + 3), new Point(bounds.Right - 1, bounds.Y + 2), new Point(bounds.Right - 2, bounds.Y + 1), new Point(bounds.Right - 3, bounds.Y + 1), new Point(bounds.Right - 4, bounds.Y), new Point(bounds.X + 4, bounds.Y) };
    }

    private Point[] GetCutBottomContour(Rectangle bounds)
    {
      return new Point[15]{ new Point(bounds.X + 4, bounds.Y), new Point(bounds.X + 3, bounds.Y + 1), new Point(bounds.X + 2, bounds.Y + 1), new Point(bounds.X + 1, bounds.Y + 2), new Point(bounds.X + 1, bounds.Y + 3), new Point(bounds.X, bounds.Y + 4), new Point(bounds.X, bounds.Bottom), new Point(bounds.Right, bounds.Bottom), new Point(bounds.Right, bounds.Y + 4), new Point(bounds.Right - 1, bounds.Y + 3), new Point(bounds.Right - 1, bounds.Y + 2), new Point(bounds.Right - 2, bounds.Y + 1), new Point(bounds.Right - 3, bounds.Y + 1), new Point(bounds.Right - 4, bounds.Y), new Point(bounds.X + 4, bounds.Y) };
    }
  }
}
