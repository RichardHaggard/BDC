// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ZoomGestureEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls
{
  public class ZoomGestureEventArgs : GestureEventArgs
  {
    private double zoomFactor;
    private Point center;

    internal ZoomGestureEventArgs(
      double zoomFactor,
      Point center,
      GESTUREINFO gestureInfo,
      RadControl owner)
      : base(gestureInfo, owner)
    {
      this.zoomFactor = zoomFactor;
      this.center = center;
    }

    public double ZoomFactor
    {
      get
      {
        return this.zoomFactor;
      }
    }

    public Point Center
    {
      get
      {
        return this.center;
      }
    }
  }
}
