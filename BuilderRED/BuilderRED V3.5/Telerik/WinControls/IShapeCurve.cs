// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IShapeCurve
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls
{
  public interface IShapeCurve
  {
    ShapePoint[] Points { get; }

    ShapePoint FirstPoint { get; set; }

    ShapePoint LastPoint { get; set; }

    ShapePoint[] Extensions { get; }

    PointF SnappedPoint { get; }

    ShapePoint SnappedCtrlPoint { get; }

    bool IsModified { get; }

    ShapePoint[] ControlPoints { get; }

    List<PointF> TestPoints { get; }

    bool SnapToCurve(PointF pt, float snapDistance);

    bool SnapToCurveExtension(PointF pt, float snapDistance);

    bool SnapToCtrlPoints(PointF pt, float snapDistance);

    bool SnapToHorizontal(PointF pt, float xVal, float snapDistance);

    bool SnapToVertical(PointF pt, float yVal, float snapDistance);

    bool Update();

    IShapeCurve Create();
  }
}
