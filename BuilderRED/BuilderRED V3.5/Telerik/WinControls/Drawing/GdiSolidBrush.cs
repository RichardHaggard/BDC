// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.GdiSolidBrush
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Drawing
{
  public class GdiSolidBrush : RadSolidBrush
  {
    private readonly SolidBrush rawBrush;

    public GdiSolidBrush(Color color)
    {
      this.rawBrush = new SolidBrush(color);
    }

    protected override void DisposeUnmanagedResources()
    {
      base.DisposeUnmanagedResources();
      if (this.rawBrush == null)
        return;
      this.rawBrush.Dispose();
    }

    public override Color Color
    {
      get
      {
        return this.rawBrush.Color;
      }
      set
      {
        this.rawBrush.Color = value;
      }
    }

    public override object RawBrush
    {
      get
      {
        return (object) this.rawBrush;
      }
    }
  }
}
