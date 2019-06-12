// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.GestureEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class GestureEventArgs : EventArgs
  {
    private GESTUREINFO gestureInfo;
    private RadControl owner;
    private bool handled;

    internal GestureEventArgs(GESTUREINFO gestureInfo, RadControl owner)
    {
      this.gestureInfo = gestureInfo;
      this.owner = owner;
    }

    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    public GestureType GestureType
    {
      get
      {
        return (GestureType) this.gestureInfo.dwID;
      }
    }

    public bool IsBegin
    {
      get
      {
        return ((int) this.gestureInfo.dwFlags & 1) != 0;
      }
    }

    public bool IsEnd
    {
      get
      {
        return ((int) this.gestureInfo.dwFlags & 4) != 0;
      }
    }

    public bool IsInertia
    {
      get
      {
        return ((int) this.gestureInfo.dwFlags & 2) != 0;
      }
    }

    public Point Location
    {
      get
      {
        return this.owner.PointToClient(new Point((int) this.gestureInfo.ptsLocation.x, (int) this.gestureInfo.ptsLocation.y));
      }
    }
  }
}
