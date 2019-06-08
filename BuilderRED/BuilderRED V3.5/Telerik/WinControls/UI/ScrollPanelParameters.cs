// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollPanelParameters
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public struct ScrollPanelParameters
  {
    public ScrollBarParameters HorizontalScrollParameters;
    public ScrollBarParameters VerticalScrollParameters;

    public ScrollPanelParameters(
      ScrollBarParameters horizontalScrollParameters,
      ScrollBarParameters verticalScrollParameters)
    {
      this.HorizontalScrollParameters = horizontalScrollParameters;
      this.VerticalScrollParameters = verticalScrollParameters;
    }

    public ScrollPanelParameters(
      int horizMinimum,
      int horizMaximum,
      int horizSmallChange,
      int horizLargeChange,
      int vertMinimum,
      int vertMaximum,
      int vertSmallChange,
      int vertLargeChange)
    {
      this.HorizontalScrollParameters = new ScrollBarParameters(horizMinimum, horizMaximum, horizSmallChange, horizLargeChange);
      this.VerticalScrollParameters = new ScrollBarParameters(vertMinimum, vertMaximum, vertSmallChange, vertLargeChange);
    }
  }
}
