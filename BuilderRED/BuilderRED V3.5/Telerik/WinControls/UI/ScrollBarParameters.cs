// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollBarParameters
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public struct ScrollBarParameters
  {
    public int Minimum;
    public int Maximum;
    public int SmallChange;
    public int LargeChange;

    public ScrollBarParameters(int minimum, int maximum, int smallChange, int largeChange)
    {
      this.Minimum = minimum;
      this.Maximum = maximum;
      this.SmallChange = smallChange;
      this.LargeChange = largeChange;
    }
  }
}
