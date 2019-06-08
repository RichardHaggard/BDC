// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.GradientStop
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Drawing
{
  public struct GradientStop
  {
    private Color color;
    private float position;

    public GradientStop(Color color, float position)
    {
      this.color = color;
      this.position = position;
    }

    public Color Color
    {
      get
      {
        return this.color;
      }
      set
      {
        this.color = value;
      }
    }

    public float Position
    {
      get
      {
        return this.position;
      }
      set
      {
        this.position = value;
      }
    }
  }
}
