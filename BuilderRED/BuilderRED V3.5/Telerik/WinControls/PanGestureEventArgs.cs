// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PanGestureEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls
{
  public class PanGestureEventArgs : GestureEventArgs
  {
    private Size offset;
    private Size velocity;

    internal PanGestureEventArgs(
      Size offset,
      Size velocity,
      GESTUREINFO gestureInfo,
      RadControl owner)
      : base(gestureInfo, owner)
    {
      this.offset = offset;
      this.velocity = velocity;
    }

    public Size Offset
    {
      get
      {
        return this.offset;
      }
    }

    public Size Velocity
    {
      get
      {
        return this.velocity;
      }
    }
  }
}
