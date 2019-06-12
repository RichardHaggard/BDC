// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ITextBlock
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public interface ITextBlock
  {
    int Index { get; set; }

    int Offset { get; set; }

    int Length { get; }

    string Text { get; set; }

    SizeF DesiredSize { get; }

    Rectangle ControlBoundingRectangle { get; }

    RectangleF GetRectangleFromCharacterIndex(int index, bool trailEdge);

    int GetCharacterIndexFromX(float x);

    void Measure(SizeF availableSize);

    void Arrange(RectangleF finalRectangle);
  }
}
