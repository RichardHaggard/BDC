// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.OldShapePoint
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  public class OldShapePoint : OldShapePointBase
  {
    private OldShapePointBase controlPoint1 = new OldShapePointBase();
    private OldShapePointBase controlPoint2 = new OldShapePointBase();
    private bool bezier;

    public OldShapePointBase ControlPoint1
    {
      get
      {
        return this.controlPoint1;
      }
      set
      {
        this.controlPoint1 = value;
      }
    }

    public OldShapePointBase ControlPoint2
    {
      get
      {
        return this.controlPoint2;
      }
      set
      {
        this.controlPoint2 = value;
      }
    }

    public bool Bezier
    {
      get
      {
        return this.bezier;
      }
      set
      {
        this.bezier = value;
      }
    }

    public OldShapePoint()
    {
    }

    public OldShapePoint(int x, int y)
      : base((float) x, (float) y)
    {
    }

    public OldShapePoint(Point point)
      : base(point)
    {
    }

    public OldShapePoint(OldShapePoint point)
      : base((OldShapePointBase) point)
    {
      this.ControlPoint1 = new OldShapePointBase(point.ControlPoint1);
      this.ControlPoint2 = new OldShapePointBase(point.ControlPoint2);
      this.Bezier = point.bezier;
    }
  }
}
