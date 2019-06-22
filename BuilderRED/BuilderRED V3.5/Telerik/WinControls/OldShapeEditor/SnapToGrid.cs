﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.OldShapeEditor.SnapToGrid
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.OldShapeEditor
{
  public class SnapToGrid
  {
    private float fieldWidth;
    private float deltaSnap;
    private float coefSnap;
    private float cachedSnap;
    private bool isLastSnapped;
    private PointF snapPoint;
    private SnapToGrid.SnapTypes snapType;

    public SnapToGrid()
    {
      this.isLastSnapped = false;
      this.fieldWidth = 1f;
      this.coefSnap = 0.2f;
      this.deltaSnap = this.fieldWidth * this.coefSnap;
      this.snapType = SnapToGrid.SnapTypes.Relative;
      this.snapPoint = new PointF();
    }

    public SnapToGrid(ref SnapToGrid e)
    {
      this.isLastSnapped = false;
      this.fieldWidth = e.fieldWidth;
      this.coefSnap = e.coefSnap;
      this.deltaSnap = e.deltaSnap;
      this.snapType = e.snapType;
      this.snapPoint = e.snapPoint;
    }

    public SnapToGrid.SnapTypes SnapType
    {
      get
      {
        return this.snapType;
      }
      set
      {
        this.snapType = value;
        this.CacheSnap();
      }
    }

    public bool IsLastSnapped
    {
      get
      {
        return this.isLastSnapped;
      }
    }

    public float FieldWidth
    {
      get
      {
        return this.fieldWidth;
      }
      set
      {
        if ((double) value <= 0.0)
          return;
        this.fieldWidth = value;
        this.CacheSnap();
      }
    }

    public float SnapFixed
    {
      get
      {
        return this.deltaSnap;
      }
      set
      {
        this.deltaSnap = value;
        this.CacheSnap();
      }
    }

    public float SnapRelative
    {
      get
      {
        return this.coefSnap;
      }
      set
      {
        this.coefSnap = value;
        this.CacheSnap();
      }
    }

    public float CachedSnap
    {
      get
      {
        return this.cachedSnap;
      }
    }

    public PointF SnappedPoint
    {
      get
      {
        return this.snapPoint;
      }
    }

    private void CacheSnap()
    {
      switch (this.snapType)
      {
        case SnapToGrid.SnapTypes.Fixed:
          this.cachedSnap = this.deltaSnap / this.fieldWidth;
          if ((double) this.cachedSnap <= 0.5)
            break;
          this.cachedSnap = 0.5f;
          break;
        default:
          this.cachedSnap = this.coefSnap;
          if ((double) this.cachedSnap <= 0.5)
            break;
          this.coefSnap = this.cachedSnap = 0.5f;
          break;
      }
    }

    public bool SnapPtToGrid(Point pos)
    {
      bool flag = false;
      PointF pointF1 = new PointF((float) (int) ((double) pos.X / (double) this.fieldWidth), (float) (int) ((double) pos.Y / (double) this.fieldWidth));
      PointF pointF2 = new PointF((float) pos.X % this.fieldWidth / this.fieldWidth, (float) pos.Y % this.fieldWidth / this.fieldWidth);
      if ((double) Math.Abs(pointF2.X) < (double) this.cachedSnap)
      {
        flag = true;
        pointF2.X = 0.0f;
      }
      else if ((double) Math.Abs(pointF2.X) > 1.0 - (double) this.cachedSnap)
      {
        flag = true;
        pointF2.X = (float) Math.Sign(pointF2.X);
      }
      if ((double) Math.Abs(pointF2.Y) < (double) this.cachedSnap)
      {
        pointF2.Y = 0.0f;
        flag = true;
      }
      else if ((double) Math.Abs(pointF2.Y) > 1.0 - (double) this.cachedSnap)
      {
        flag = true;
        pointF2.Y = (float) Math.Sign(pointF2.Y);
      }
      pointF1.X += pointF2.X;
      pointF1.Y += pointF2.Y;
      this.snapPoint.X = (float) (int) ((double) pointF1.X * (double) this.fieldWidth);
      this.snapPoint.Y = (float) (int) ((double) pointF1.Y * (double) this.fieldWidth);
      this.isLastSnapped = flag;
      return flag;
    }

    public enum SnapTypes
    {
      Relative,
      Fixed,
    }
  }
}