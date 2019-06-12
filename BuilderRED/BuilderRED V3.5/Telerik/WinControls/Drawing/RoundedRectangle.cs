// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.RoundedRectangle
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Drawing
{
  public abstract class RoundedRectangle : DisposableObject
  {
    private float radius;
    private RectangleF rectangle;

    public RoundedRectangle(RectangleF rectangle, float radius)
    {
      this.rectangle = rectangle;
      this.radius = radius;
    }

    protected RectangleF Rectangle
    {
      get
      {
        return this.rectangle;
      }
    }

    public float X
    {
      get
      {
        return this.rectangle.X;
      }
      set
      {
        this.rectangle.X = value;
        this.OnPropertyChanged(nameof (X));
      }
    }

    public float Y
    {
      get
      {
        return this.rectangle.Y;
      }
      set
      {
        this.rectangle.Y = value;
        this.OnPropertyChanged(nameof (Y));
      }
    }

    public float Width
    {
      get
      {
        return this.rectangle.Width;
      }
      set
      {
        this.rectangle.Width = value;
        this.OnPropertyChanged(nameof (Width));
      }
    }

    public float Height
    {
      get
      {
        return this.rectangle.Height;
      }
      set
      {
        this.rectangle.Height = value;
        this.OnPropertyChanged(nameof (Height));
      }
    }

    public PointF Location
    {
      get
      {
        return this.rectangle.Location;
      }
      set
      {
        this.rectangle.Location = value;
        this.OnPropertyChanged(nameof (Location));
      }
    }

    public float Radius
    {
      get
      {
        return this.radius;
      }
      set
      {
        this.radius = value;
        this.OnPropertyChanged(nameof (Radius));
      }
    }

    public float Top
    {
      get
      {
        return this.rectangle.Top;
      }
    }

    public float Left
    {
      get
      {
        return this.rectangle.Left;
      }
    }

    public float Right
    {
      get
      {
        return this.rectangle.Right;
      }
    }

    public float Bottom
    {
      get
      {
        return this.rectangle.Bottom;
      }
    }

    public bool IsEmpty
    {
      get
      {
        return this.rectangle.IsEmpty;
      }
    }

    public SizeF Size
    {
      get
      {
        return this.rectangle.Size;
      }
      set
      {
        if (!(this.rectangle.Size != value))
          return;
        this.rectangle.Size = value;
        this.OnPropertyChanged(nameof (Size));
      }
    }

    public abstract object RawRoundedRectangle { get; }

    protected virtual void OnPropertyChanged(string propertyName)
    {
    }
  }
}
