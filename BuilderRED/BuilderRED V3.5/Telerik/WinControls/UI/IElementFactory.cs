// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IElementFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.ObjectModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public interface IElementFactory
  {
    RadBarcodeElement BarcodeElement { get; }

    ReadOnlyCollection<BarcodeElementBase> Elements { get; }

    RectangleF ElementsBounds { get; }

    void ClearElements();

    void CreateBarElement(RectangleF rect);

    void CreateTextElement(string text, RectangleF rect);
  }
}
