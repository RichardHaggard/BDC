// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ElementShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ElementShapeConverter))]
  [Editor(typeof (ElementShapeEditor), typeof (UITypeEditor))]
  public abstract class ElementShape : Component
  {
    private GraphicsPath shape;
    private GraphicsPath contour;
    private bool isRightToLeft;

    public ElementShape()
    {
      this.isRightToLeft = false;
    }

    public ElementShape(IContainer container)
    {
      this.isRightToLeft = false;
      container.Add((IComponent) this);
    }

    public bool IsRightToLeft
    {
      get
      {
        return this.isRightToLeft;
      }
      set
      {
        this.isRightToLeft = value;
      }
    }

    public GraphicsPath GetElementShape(RadElement element)
    {
      return this.CreatePath(this.GetBounds(element));
    }

    public GraphicsPath GetElementContour(RadElement element)
    {
      return this.CreateContour(this.GetBounds(element));
    }

    public GraphicsPath GetElementContour(Rectangle bounds)
    {
      return this.CreateContour(bounds);
    }

    public virtual Region CreateRegion(Rectangle bounds)
    {
      GraphicsPath path = this.CreatePath(bounds);
      Region region = new Region(path);
      path.Dispose();
      return region;
    }

    protected virtual Rectangle GetBounds(RadElement element)
    {
      Rectangle bounds = element.Bounds;
      bounds.Location = new Point(0, 0);
      bounds.Size = Size.Subtract(element.Size, new Size(1, 1));
      return bounds;
    }

    public abstract GraphicsPath CreatePath(Rectangle bounds);

    public virtual GraphicsPath CreatePath(RectangleF bounds)
    {
      return this.CreatePath(Rectangle.Round(bounds));
    }

    protected virtual GraphicsPath CreateContour(Rectangle bounds)
    {
      return this.CreatePath(bounds);
    }

    protected virtual void MirrorPath(GraphicsPath graphicsPath, RectangleF bounds)
    {
      if (!this.ShouldMirrorPath())
        return;
      using (Matrix matrix = new Matrix())
      {
        matrix.Translate(bounds.Width, 0.0f);
        matrix.Scale(-1f, 1f);
        graphicsPath.Transform(matrix);
      }
    }

    protected virtual bool ShouldMirrorPath()
    {
      return this.IsRightToLeft;
    }

    public virtual string SerializeProperties()
    {
      return string.Empty;
    }

    public virtual void DeserializeProperties(string propertiesString)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.shape != null)
        {
          this.shape.Dispose();
          this.shape = (GraphicsPath) null;
        }
        if (this.contour != null)
        {
          this.contour.Dispose();
          this.contour = (GraphicsPath) null;
        }
      }
      base.Dispose(disposing);
    }
  }
}
