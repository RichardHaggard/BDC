// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.ShapeInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI.ImageEditor
{
  public class ShapeInfo
  {
    private ShapeType shapeType;
    private Color fill;
    private Color borderColor;
    private int borderThickness;

    public ShapeInfo(ShapeType shapeType, Color fill, Color borderColor, int borderThickness)
    {
      this.shapeType = shapeType;
      this.fill = fill;
      this.borderColor = borderColor;
      this.borderThickness = borderThickness;
    }

    public ShapeType ShapeType
    {
      get
      {
        return this.shapeType;
      }
      set
      {
        this.shapeType = value;
      }
    }

    public Color Fill
    {
      get
      {
        return this.fill;
      }
      set
      {
        this.fill = value;
      }
    }

    public Color BorderColor
    {
      get
      {
        return this.borderColor;
      }
      set
      {
        this.borderColor = value;
      }
    }

    public int BorderThickness
    {
      get
      {
        return this.borderThickness;
      }
      set
      {
        this.borderThickness = value;
      }
    }
  }
}
