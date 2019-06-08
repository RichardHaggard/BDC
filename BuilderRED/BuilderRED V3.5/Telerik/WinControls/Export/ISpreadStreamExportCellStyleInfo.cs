// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.ISpreadStreamExportCellStyleInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Export
{
  public interface ISpreadStreamExportCellStyleInfo
  {
    Color BackColor { get; set; }

    Color ForeColor { get; set; }

    FontFamily FontFamily { get; set; }

    double FontSize { get; set; }

    bool IsBold { get; set; }

    bool IsItalic { get; set; }

    bool Underline { get; set; }

    ContentAlignment TextAlignment { get; set; }

    object TopBorder { get; set; }

    object BottomBorder { get; set; }

    object RightBorder { get; set; }

    object LeftBorder { get; set; }

    object DiagonalUpBorder { get; set; }

    object DiagonalDownBorder { get; set; }

    bool TextWrap { get; set; }

    string NumberFormat { get; set; }
  }
}
