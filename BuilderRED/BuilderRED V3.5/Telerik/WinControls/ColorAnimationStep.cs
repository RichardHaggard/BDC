// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ColorAnimationStep
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ColorAnimationStepConverter))]
  public class ColorAnimationStep
  {
    public int A;
    public int R;
    public int G;
    public int B;

    public ColorAnimationStep(int A, int R, int G, int B)
    {
      this.A = A;
      this.G = G;
      this.R = R;
      this.B = B;
    }
  }
}
