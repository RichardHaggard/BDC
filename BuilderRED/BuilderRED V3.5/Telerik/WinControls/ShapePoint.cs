// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ShapePoint
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  [ToolboxItem(false)]
  [DesignTimeVisible(false)]
  public class ShapePoint : Component
  {
    private PointF point;
    private bool isModified;
    private bool isLocked;
    private AnchorStyles anchor;

    public ShapePoint()
    {
      this.point = new PointF(0.0f, 0.0f);
      this.isLocked = false;
    }

    public ShapePoint(Point pt)
    {
      this.point = new PointF((float) pt.X, (float) pt.Y);
      this.isLocked = false;
    }

    public ShapePoint(PointF pt)
    {
      this.point = new PointF(pt.X, pt.Y);
      this.isLocked = false;
    }

    public ShapePoint(int x, int y)
    {
      this.point = new PointF((float) x, (float) y);
      this.isLocked = false;
    }

    public ShapePoint(float x, float y)
    {
      this.point = new PointF(x, y);
      this.isLocked = false;
    }

    public ShapePoint(ShapePoint pt)
    {
      this.point = pt.point;
      this.isLocked = pt.isLocked;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(false)]
    public PointF Location
    {
      get
      {
        return this.point;
      }
      set
      {
        if (this.isLocked)
          return;
        this.point = value;
      }
    }

    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("The X coordinate of the point")]
    [NotifyParentProperty(true)]
    [DefaultValue(0)]
    public float X
    {
      get
      {
        return this.point.X;
      }
      set
      {
        if (this.isLocked)
          return;
        this.point.X = value;
      }
    }

    [DefaultValue(0)]
    [Description("The Y coordinate of the point")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Layout")]
    [NotifyParentProperty(true)]
    public float Y
    {
      get
      {
        return this.point.Y;
      }
      set
      {
        if (this.isLocked)
          return;
        this.point.Y = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(false)]
    public bool IsModified
    {
      get
      {
        return this.isModified;
      }
      set
      {
        if (this.isLocked)
          return;
        this.isModified = value;
      }
    }

    [Description("Determines if the current point could be moved or not.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool IsLocked
    {
      get
      {
        return this.isLocked;
      }
      set
      {
        if (value)
          this.isModified = false;
        this.isLocked = value;
      }
    }

    public static float DistSquared(PointF a, PointF b)
    {
      PointF pointF = new PointF(a.X - b.X, a.Y - b.Y);
      return (float) ((double) pointF.X * (double) pointF.X + (double) pointF.Y * (double) pointF.Y);
    }

    public void Set(Point pt)
    {
      this.X = (float) pt.X;
      this.Y = (float) pt.Y;
    }

    public void Set(int x, int y)
    {
      this.X = (float) x;
      this.Y = (float) y;
    }

    public void Set(float x, float y)
    {
      this.X = x;
      this.Y = y;
    }

    public Point Get()
    {
      return new Point((int) Math.Round((double) this.X), (int) Math.Round((double) this.Y));
    }

    public Point GetPoint(Rectangle bounds)
    {
      return new Point(bounds.X + (int) Math.Round((double) this.X), bounds.Y + (int) Math.Round((double) this.Y));
    }

    public Point GetPoint(Rectangle src, Rectangle dst)
    {
      float num1 = this.X - (float) src.X;
      float num2 = this.Y - (float) src.Y;
      float num3 = num1 / (float) src.Width;
      float num4 = num2 / (float) src.Height;
      return new Point((int) Math.Round((this.anchor & AnchorStyles.Left) == AnchorStyles.None ? ((this.anchor & AnchorStyles.Right) == AnchorStyles.None ? (double) ((float) dst.X + num3 * (float) dst.Width) : (double) ((float) dst.Right - ((float) src.Width - num1))) : (double) ((float) dst.X + num1)), (int) Math.Round((this.anchor & AnchorStyles.Top) == AnchorStyles.None ? ((this.anchor & AnchorStyles.Bottom) == AnchorStyles.None ? (double) ((float) dst.Y + num4 * (float) dst.Height) : (double) ((float) dst.Bottom - ((float) src.Height - num2))) : (double) ((float) dst.Y + num2)));
    }

    public static implicit operator Point(ShapePoint pt)
    {
      return new Point((int) pt.X, (int) pt.Y);
    }

    public override string ToString()
    {
      return string.Format("Point: {0},{1}", (object) this.X.ToString(), (object) this.Y.ToString());
    }
  }
}
