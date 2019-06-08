// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPicker.ColorHexagonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.UI.RadColorPicker
{
  public class ColorHexagonElement
  {
    private Point[] hexagonPoints = new Point[6];
    private Color hexagonColor = Color.Empty;
    private Rectangle boundingRectangle = Rectangle.Empty;
    private bool isHovered;
    private bool isSelected;

    public void SetHexagonPoints(float xCoordinate, float yCoordinate, int hexagonWidth)
    {
      float num = (float) hexagonWidth * 0.5773503f;
      this.hexagonPoints[0] = new Point((int) Math.Floor((double) xCoordinate - (double) (hexagonWidth / 2)), (int) Math.Floor((double) yCoordinate - (double) num / 2.0) - 1);
      this.hexagonPoints[1] = new Point((int) Math.Floor((double) xCoordinate), (int) Math.Floor((double) yCoordinate - (double) (hexagonWidth / 2)) - 1);
      this.hexagonPoints[2] = new Point((int) Math.Floor((double) xCoordinate + (double) (hexagonWidth / 2)), (int) Math.Floor((double) yCoordinate - (double) num / 2.0) - 1);
      this.hexagonPoints[3] = new Point((int) Math.Floor((double) xCoordinate + (double) (hexagonWidth / 2)), (int) Math.Floor((double) yCoordinate + (double) num / 2.0) + 1);
      this.hexagonPoints[4] = new Point((int) Math.Floor((double) xCoordinate), (int) Math.Floor((double) yCoordinate + (double) (hexagonWidth / 2)) + 1);
      this.hexagonPoints[5] = new Point((int) Math.Floor((double) xCoordinate - (double) (hexagonWidth / 2)), (int) Math.Floor((double) yCoordinate + (double) num / 2.0) + 1);
      using (GraphicsPath graphicsPath = new GraphicsPath())
      {
        graphicsPath.AddPolygon(this.hexagonPoints);
        this.boundingRectangle = Rectangle.Round(graphicsPath.GetBounds());
        this.boundingRectangle.Inflate(2, 2);
      }
    }

    public void Paint(Graphics graphics)
    {
      GraphicsPath path = new GraphicsPath();
      path.AddPolygon(this.hexagonPoints);
      path.CloseAllFigures();
      using (SolidBrush solidBrush = new SolidBrush(this.hexagonColor))
        graphics.FillPath((Brush) solidBrush, path);
      if (this.isHovered || this.isSelected)
      {
        SmoothingMode smoothingMode = graphics.SmoothingMode;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (Pen pen = new Pen(Color.FromArgb(42, 91, 150), 2f))
          graphics.DrawPath(pen, path);
        using (Pen pen = new Pen(Color.FromArgb(150, 177, 239), 1f))
          graphics.DrawPath(pen, path);
        graphics.SmoothingMode = smoothingMode;
      }
      path.Dispose();
    }

    public Color CurrentColor
    {
      get
      {
        return this.hexagonColor;
      }
      set
      {
        this.hexagonColor = value;
      }
    }

    public Rectangle BoundingRectangle
    {
      get
      {
        return this.boundingRectangle;
      }
    }

    public bool IsHovered
    {
      get
      {
        return this.isHovered;
      }
      set
      {
        this.isHovered = value;
      }
    }

    public bool IsSelected
    {
      get
      {
        return this.isSelected;
      }
      set
      {
        this.isSelected = value;
      }
    }
  }
}
