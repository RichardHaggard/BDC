// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MeasureContext
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class MeasureContext : IMeasureContext
  {
    private Font font;

    public MeasureContext(Font font)
    {
      this.font = font;
    }

    public Font Font
    {
      get
      {
        return this.font;
      }
    }

    public SizeF MeasureString(string s)
    {
      return RadGdiGraphics.MeasurementGraphics.MeasureString(s, this.Font);
    }
  }
}
