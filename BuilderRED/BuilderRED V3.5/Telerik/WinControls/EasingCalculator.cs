// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.EasingCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public abstract class EasingCalculator
  {
    public abstract int CalculateCurrentValue(
      int initialValue,
      int endValue,
      int currentFrame,
      int numFrames);

    public abstract float CalculateCurrentValue(
      float initialValue,
      float endValue,
      int currentFrame,
      int numFrames);

    public abstract double CalculateCurrentValue(
      double initialValue,
      double endValue,
      int currentFrame,
      int numFrames);
  }
}
