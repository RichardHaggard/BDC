// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class BarElement : BarcodeElementBase
  {
    private Color fill;

    public BarElement(RectangleF bounds)
      : this(bounds, Color.Black)
    {
    }

    public BarElement(RectangleF bounds, Color fill)
      : base(bounds)
    {
      this.fill = fill;
    }

    public Color Fill
    {
      get
      {
        return this.fill;
      }
      set
      {
        this.fill = value;
      }
    }

    public override void PaintElement(Graphics g)
    {
      using (SolidBrush solidBrush = new SolidBrush(this.Fill))
        g.FillRectangle((Brush) solidBrush, this.Bounds);
    }
  }
}
