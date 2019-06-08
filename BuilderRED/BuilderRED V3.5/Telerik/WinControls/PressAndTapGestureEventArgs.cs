// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PressAndTapGestureEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls
{
  public class PressAndTapGestureEventArgs : GestureEventArgs
  {
    private Point delta;

    internal PressAndTapGestureEventArgs(Point delta, GESTUREINFO gestureInfo, RadControl owner)
      : base(gestureInfo, owner)
    {
      this.delta = delta;
    }

    public Point Delta
    {
      get
      {
        return this.delta;
      }
    }
  }
}
