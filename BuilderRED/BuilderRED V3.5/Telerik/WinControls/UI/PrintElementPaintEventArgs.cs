// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PrintElementPaintEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class PrintElementPaintEventArgs : PrintElementEventArgs
  {
    private Graphics graphics;
    private Rectangle bounds;

    public PrintElementPaintEventArgs(RadPrintElement element, Graphics graphics, Rectangle bounds)
      : base(element)
    {
      this.graphics = graphics;
      this.bounds = bounds;
    }

    public Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
    }

    public Rectangle Bounds
    {
      get
      {
        return this.bounds;
      }
      set
      {
        this.bounds = value;
      }
    }
  }
}
