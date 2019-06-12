// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IRadScrollViewport
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public interface IRadScrollViewport
  {
    Size GetExtentSize();

    void InvalidateViewport();

    Point ResetValue(Point currentValue, Size viewportSize, Size extentSize);

    void DoScroll(Point oldValue, Point newValue);

    Size ScrollOffsetForChildVisible(RadElement childElement, Point currentScrollValue);

    ScrollPanelParameters GetScrollParams(Size viewportSize, Size extentSize);
  }
}
