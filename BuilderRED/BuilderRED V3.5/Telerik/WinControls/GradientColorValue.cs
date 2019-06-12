// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.GradientColorValue
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls
{
  public class GradientColorValue
  {
    public Color ColorValue;
    public float ColorPosition;

    public GradientColorValue()
    {
      this.ColorValue = Color.White;
      this.ColorPosition = 0.0f;
    }

    public GradientColorValue(Color color, float point)
    {
      this.ColorValue = color;
      this.ColorPosition = point;
    }
  }
}
