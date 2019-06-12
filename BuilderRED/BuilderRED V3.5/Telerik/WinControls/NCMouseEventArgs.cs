// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.NCMouseEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class NCMouseEventArgs : MouseEventArgs
  {
    public NCMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
      : base(button, clicks, x, y, delta)
    {
    }

    public NCMouseEventArgs(MouseEventArgs args)
      : base(args.Button, args.Clicks, args.X, args.Y, args.Delta)
    {
    }
  }
}
