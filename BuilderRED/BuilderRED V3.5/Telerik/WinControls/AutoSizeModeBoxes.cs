// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AutoSizeModeBoxes
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  internal static class AutoSizeModeBoxes
  {
    internal static object AutoBox = (object) RadAutoSizeMode.Auto;
    internal static object WrapAroundChildrenBox = (object) RadAutoSizeMode.WrapAroundChildren;
    internal static object FitToAvailableSizeBox = (object) RadAutoSizeMode.FitToAvailableSize;

    internal static object Box(RadAutoSizeMode value)
    {
      if (value == RadAutoSizeMode.Auto)
        return AutoSizeModeBoxes.AutoBox;
      if (value == RadAutoSizeMode.WrapAroundChildren)
        return AutoSizeModeBoxes.WrapAroundChildrenBox;
      return AutoSizeModeBoxes.FitToAvailableSizeBox;
    }
  }
}
