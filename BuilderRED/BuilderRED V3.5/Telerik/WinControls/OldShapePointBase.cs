// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.OldShapePointBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  public class OldShapePointBase
  {
    private float x;
    private float y;
    private AnchorStyles anchor;
    private bool locked;

    public float X
    {
      get
      {
        return this.x;
      }
      set
      {
        this.x = value;
      }
    }

    public float Y
    {
      get
      {
        return this.y;
      }
      set
      {
        this.y = value;
      }
    }

    public AnchorStyles Anchor
    {
      get
      {
        return this.anchor;
      }
      set
      {
        this.anchor = value;
      }
    }

    public bool Locked
    {
      get
      {
        return this.locked;
      }
      set
      {
        this.locked = value;
      }
    }

    public OldShapePointBase()
    {
    }

    public OldShapePointBase(float x, float y)
    {
      this.x = x;
      this.y = y;
    }

    public OldShapePointBase(Point point)
    {
      this.x = (float) point.X;
      this.y = (float) point.Y;
    }

    public OldShapePointBase(OldShapePointBase point)
    {
      this.x = point.X;
      this.y = point.Y;
      this.anchor = point.Anchor;
      this.locked = point.Locked;
    }

    public void Set(float x, float y)
    {
      this.x = x;
      this.y = y;
    }

    public void Set(Point point)
    {
      this.Set((float) point.X, (float) point.Y);
    }

    public Point GetPoint()
    {
      return new Point((int) this.x, (int) this.y);
    }

    public Point GetPoint(Rectangle bounds)
    {
      return new Point(bounds.X + (int) Math.Round((double) this.x), bounds.Y + (int) Math.Round((double) this.y));
    }

    public Point GetPoint(Rectangle src, Rectangle dst)
    {
      float num1 = this.x - (float) src.X;
      float num2 = this.y - (float) src.Y;
      float num3 = num1 / (float) src.Width;
      float num4 = num2 / (float) src.Height;
      return new Point((int) Math.Round((this.anchor & AnchorStyles.Left) == AnchorStyles.None ? ((this.anchor & AnchorStyles.Right) == AnchorStyles.None ? (double) ((float) dst.X + num3 * (float) dst.Width) : (double) ((float) dst.Right - ((float) src.Width - num1))) : (double) ((float) dst.X + num1)), (int) Math.Round((this.anchor & AnchorStyles.Top) == AnchorStyles.None ? ((this.anchor & AnchorStyles.Bottom) == AnchorStyles.None ? (double) ((float) dst.Y + num4 * (float) dst.Height) : (double) ((float) dst.Bottom - ((float) src.Height - num2))) : (double) ((float) dst.Y + num2)));
    }

    public Rectangle GetBounds(int weight)
    {
      return new Rectangle((int) Math.Round((double) this.x - (double) (weight / 4)), (int) Math.Round((double) this.y - (double) (weight / 4)), weight, weight);
    }

    public bool IsVisible(int x, int y, int width)
    {
      return this.GetBounds(width).Contains(x, y);
    }

    public override string ToString()
    {
      return string.Format("Point: {0},{1}", (object) this.x.ToString(), (object) this.y.ToString());
    }
  }
}
