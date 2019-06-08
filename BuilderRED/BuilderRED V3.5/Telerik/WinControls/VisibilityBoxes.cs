// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.VisibilityBoxes
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  internal static class VisibilityBoxes
  {
    internal static object VisibleBox = (object) ElementVisibility.Visible;
    internal static object HiddenBox = (object) ElementVisibility.Hidden;
    internal static object CollapsedBox = (object) ElementVisibility.Collapsed;

    internal static object Box(ElementVisibility value)
    {
      if (value == ElementVisibility.Visible)
        return VisibilityBoxes.VisibleBox;
      if (value == ElementVisibility.Hidden)
        return VisibilityBoxes.HiddenBox;
      return VisibilityBoxes.CollapsedBox;
    }
  }
}
