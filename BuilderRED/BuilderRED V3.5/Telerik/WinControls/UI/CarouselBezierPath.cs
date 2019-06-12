// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CarouselBezierPath
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Threading;

namespace Telerik.WinControls.UI
{
  public class CarouselBezierPath : CarouselParameterPath
  {
    private ReaderWriterLock pointsLock = new ReaderWriterLock();
    private ReaderWriterLock paramsLock = new ReaderWriterLock();
    private double[][] parameters = new double[3][]{ new double[3], new double[3], new double[3] };
    private Point3D[] points = new Point3D[4]{ Point3D.Empty, Point3D.Empty, Point3D.Empty, Point3D.Empty };
    private string[] propertyNames = new string[4]{ nameof (FirstPoint), nameof (CtrlPoint1), nameof (CtrlPoint2), nameof (LastPoint) };
    private bool closedPath;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Point3D[] ControlPoints
    {
      get
      {
        return this.points;
      }
    }

    [NotifyParentProperty(true)]
    public Point3D FirstPoint
    {
      get
      {
        return this.points[0];
      }
      set
      {
        this.SetPoint(0, value);
      }
    }

    private bool ShouldSerializeFirstPoint()
    {
      return this.FirstPoint != Point3D.Empty;
    }

    private void ResetFirstPoint()
    {
      this.FirstPoint = Point3D.Empty;
    }

    private bool ShouldSerializeCtrlPoint1()
    {
      return this.CtrlPoint1 != Point3D.Empty;
    }

    private void ResetCtrlPoint1()
    {
      this.CtrlPoint1 = Point3D.Empty;
    }

    private bool ShouldSerializeCtrlPoint2()
    {
      return this.CtrlPoint2 != Point3D.Empty;
    }

    private void ResetCtrlPoint2()
    {
      this.CtrlPoint2 = Point3D.Empty;
    }

    private bool ShouldSerializeLastPoint()
    {
      return this.LastPoint != Point3D.Empty;
    }

    private void ResetLastPoint()
    {
      this.LastPoint = Point3D.Empty;
    }

    [NotifyParentProperty(true)]
    public Point3D CtrlPoint1
    {
      get
      {
        return this.points[1];
      }
      set
      {
        this.SetPoint(1, value);
      }
    }

    [NotifyParentProperty(true)]
    public Point3D CtrlPoint2
    {
      get
      {
        return this.points[2];
      }
      set
      {
        this.SetPoint(2, value);
      }
    }

    [NotifyParentProperty(true)]
    public Point3D LastPoint
    {
      get
      {
        return this.points[3];
      }
      set
      {
        this.SetPoint(3, value);
      }
    }

    private static double EvaluateCoordinate(double[] tPow, double point, double[] para)
    {
      for (int index = 0; index < 3; ++index)
        point += para[index] * tPow[index];
      return point;
    }

    private void EvaluateParameters()
    {
      this.pointsLock.AcquireReaderLock(20);
      if (this.isModified)
      {
        this.paramsLock.AcquireWriterLock(20);
        this.isModified = false;
        this.parameters[0][2] = 3.0 * (this.points[1].X - this.points[0].X);
        this.parameters[0][1] = 3.0 * (this.points[2].X - this.points[1].X) - this.parameters[0][2];
        this.parameters[0][0] = this.points[3].X - this.points[0].X - this.parameters[0][2] - this.parameters[0][1];
        this.parameters[1][2] = 3.0 * (this.points[1].Y - this.points[0].Y);
        this.parameters[1][1] = 3.0 * (this.points[2].Y - this.points[1].Y) - this.parameters[1][2];
        this.parameters[1][0] = this.points[3].Y - this.points[0].Y - this.parameters[1][2] - this.parameters[1][1];
        this.parameters[2][2] = 3.0 * (this.points[1].Z - this.points[0].Z);
        this.parameters[2][1] = 3.0 * (this.points[2].Z - this.points[1].Z) - this.parameters[2][2];
        this.parameters[2][0] = this.points[3].Z - this.points[0].Z - this.parameters[2][2] - this.parameters[2][1];
        this.paramsLock.ReleaseWriterLock();
      }
      this.pointsLock.ReleaseReaderLock();
    }

    private void SetPoint(int pt, Point3D value)
    {
      bool flag = false;
      this.pointsLock.AcquireWriterLock(10);
      if (this.points[pt] != value)
      {
        this.points[pt] = value;
        this.isModified = true;
        flag = true;
      }
      this.pointsLock.ReleaseWriterLock();
      if (!flag)
        return;
      this.OnPropertyChanged(this.propertyNames[pt]);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override object InitialPathPoint
    {
      get
      {
        return (object) this.points[0];
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override object FinalPathPoint
    {
      get
      {
        return (object) this.points[3];
      }
    }

    public override object EvaluateByParameter(
      VisualElement element,
      CarouselPathAnimationData data,
      double value)
    {
      double[] tPow = new double[3]{ 0.0, 0.0, value };
      tPow[1] = tPow[2] * tPow[2];
      tPow[0] = tPow[1] * tPow[2];
      this.EvaluateParameters();
      this.paramsLock.AcquireReaderLock(20);
      Point3D point3D = new Point3D(CarouselBezierPath.EvaluateCoordinate(tPow, this.points[0].X, this.parameters[0]), CarouselBezierPath.EvaluateCoordinate(tPow, this.points[0].Y, this.parameters[1]), CarouselBezierPath.EvaluateCoordinate(tPow, this.points[0].Z, this.parameters[2]));
      this.paramsLock.ReleaseReaderLock();
      return (object) point3D;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "FirstPoint":
        case "CtrlPoint1":
        case "CtrlPoint2":
        case "LastPoint":
        case "RelativePath":
        case "ZScale":
        case "OpacityChangeCondition":
          this.UpdateZindexSource();
          break;
      }
      base.OnPropertyChanged(e);
    }

    private void UpdateZindexSource()
    {
      double val1_1 = this.points[0].Z;
      double val1_2 = val1_1;
      bool flag = false;
      for (int index = 1; index < 3; ++index)
      {
        flag |= this.points[0].Z != this.points[index].Z;
        val1_1 = Math.Min(val1_1, this.points[index].Z);
        val1_2 = Math.Max(val1_2, this.points[index].Z);
      }
      this.closedPath = this.FirstPoint.Equals((object) this.LastPoint);
      this.zIndexScale = 10000000.0 / (Math.Abs(val1_1) + Math.Abs(val1_2));
      this.zIndexFromPathValue = flag;
    }

    protected override bool IsClosedPath
    {
      get
      {
        return this.closedPath;
      }
    }
  }
}
