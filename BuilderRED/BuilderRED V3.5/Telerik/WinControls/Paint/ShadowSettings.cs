// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Paint.ShadowSettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.Paint
{
  [TypeConverter("Telerik.WinControls.UI.Design.ShadowSettingsConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class ShadowSettings
  {
    private Point depth;
    private Color shadowColor;

    public ShadowSettings(Point depth, Color shadowColor)
    {
      this.depth = depth;
      this.shadowColor = shadowColor;
    }

    public ShadowSettings()
    {
      this.depth = new Point(1, 1);
      this.shadowColor = Color.Black;
    }

    public Point Depth
    {
      get
      {
        return this.depth;
      }
      set
      {
        this.depth = value;
      }
    }

    public Color ShadowColor
    {
      get
      {
        return this.shadowColor;
      }
      set
      {
        this.shadowColor = value;
      }
    }
  }
}
