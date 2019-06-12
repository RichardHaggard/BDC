// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CarouselEllipsePath
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class CarouselEllipsePath : CarouselParameterPath
  {
    private Point3D center = new Point3D();
    private Point3D u = new Point3D();
    private Point3D v = new Point3D();
    private double finalAngle = 360.0;
    private double initialAngle;

    [Description("Gets or sets the center of the ellipse of the path")]
    [NotifyParentProperty(true)]
    public Point3D Center
    {
      get
      {
        return this.center;
      }
      set
      {
        if (this.center == value)
          return;
        this.center = value;
        this.OnPropertyChanged(nameof (Center));
      }
    }

    [NotifyParentProperty(true)]
    [Description("Gets or sets the first focus of the ellipse of the path")]
    public Point3D U
    {
      get
      {
        return this.u;
      }
      set
      {
        if (this.u == value)
          return;
        this.u = value;
        this.OnPropertyChanged(nameof (U));
      }
    }

    [Description("Gets or sets the second focus of the ellipse of the path")]
    [NotifyParentProperty(true)]
    public Point3D V
    {
      get
      {
        return this.v;
      }
      set
      {
        if (this.v == value)
          return;
        this.v = value;
        this.OnPropertyChanged(nameof (V));
      }
    }

    [NotifyParentProperty(true)]
    [Description("Gets or sets the angle where itms new items will first appear in the carousel view.")]
    public double InitialAngle
    {
      get
      {
        return this.initialAngle;
      }
      set
      {
        if (this.initialAngle == value)
          return;
        this.initialAngle = value;
        this.OnPropertyChanged(nameof (InitialAngle));
      }
    }

    [NotifyParentProperty(true)]
    [Description("Gets or sets the final angle of the ellipse of the path")]
    public double FinalAngle
    {
      get
      {
        return this.finalAngle;
      }
      set
      {
        if (this.finalAngle == value)
          return;
        this.finalAngle = value;
        this.OnPropertyChanged(nameof (FinalAngle));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets initial path point in the path")]
    public override object InitialPathPoint
    {
      get
      {
        return (object) CarouselEllipsePath.Evaluate3D(this.Center, this.U, this.V, CarouselEllipsePath.ToRadians(this.initialAngle));
      }
    }

    [Description("Gets final path point in the path")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override object FinalPathPoint
    {
      get
      {
        return (object) CarouselEllipsePath.Evaluate3D(this.Center, this.U, this.V, CarouselEllipsePath.ToRadians(this.finalAngle));
      }
    }

    private static Point3D Evaluate3D(Point3D C, Point3D u, Point3D v, double angle)
    {
      Point3D point3D1 = new Point3D(u);
      Point3D point3D2 = new Point3D(v);
      double num1 = point3D1.Length();
      double num2 = point3D2.Length();
      point3D1.Normalize();
      point3D2.Normalize();
      double num3 = Math.Cos(angle);
      double num4 = Math.Sin(angle);
      return new Point3D(C.X + num1 * num3 * point3D1.X + num2 * num4 * point3D2.X, C.Y + num1 * num3 * point3D1.Y + num2 * num4 * point3D2.Y, C.Z + num1 * num3 * point3D1.Z + num2 * num4 * point3D2.Z);
    }

    public override object EvaluateByParameter(
      VisualElement element,
      CarouselPathAnimationData data,
      double value)
    {
      return (object) CarouselEllipsePath.Evaluate3D(this.center, this.u, this.v, 2.0 * value * Math.PI + CarouselEllipsePath.ToRadians(this.initialAngle));
    }

    private static double ToRadians(double angle)
    {
      return angle * Math.PI / 180.0;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "Center":
        case "U":
        case "V":
        case "RelativePath":
        case "OpacityChangeCondition":
          this.UpdateZindexSource();
          break;
      }
      base.OnPropertyChanged(e);
    }

    private void UpdateZindexSource()
    {
      this.zIndexScale = 100000000.0 / (Math.Abs(Math.Max(Math.Max(this.Center.Z, this.U.Z), this.V.Z)) + Math.Abs(Math.Min(Math.Min(this.Center.Z, this.U.Z), this.V.Z)));
      this.zIndexFromPathValue = this.Center.Z != this.U.Z || this.Center.Z != this.V.Z;
    }
  }
}
