// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ISpreadStreamExportRenderer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.IO;
using Telerik.WinControls.Export;

namespace Telerik.WinControls
{
  public interface ISpreadStreamExportRenderer
  {
    int RowIndex { get; set; }

    int ColumnIndex { get; set; }

    string GetFileExtension(SpreadStreamExportFormat exportFormat);

    void FinishExport();

    void CreateWorkbook(Stream stream, SpreadStreamExportFormat exportFormat);

    void CreateWorkbook(
      Stream stream,
      SpreadStreamExportFormat exportFormat,
      FileExportMode fileExportMode);

    object GetWorksheet();

    void AddWorksheet(string sheetName);

    bool CallOnWorksheetCreated();

    void SetHiddenRow();

    void SetRowHeight(double height, bool inPixels);

    void SetHiddenColumn();

    void SetColumnWidth(double width, bool inPixels);

    void SkipCells(int count);

    object GetCell();

    void CreateCell();

    void FinishCell();

    void CreateMergedCells(
      int fromRowIndex,
      int fromColumnIndex,
      int toRowIndex,
      int toColumnIndex);

    void CreateRow();

    object GetRow(bool finishCell);

    void CreateColumn();

    void SkipColumns(int count);

    object GetColumn();

    void SetCellValue(string text);

    void SetCellValue(DataType dataType, object value);

    void SetCellFormat(string formatString, bool apply);

    void ClearCellValue();

    void ApplyCellStyle(ISpreadStreamExportCellStyleInfo cellStyle, string formatString);

    object GetCellFormat(bool createNew);

    void ApplyCellFormat(object format);

    ISpreadStreamExportCellStyleInfo GetCellStyleInfo();

    ISpreadStreamExportCellStyleInfo CreateCellStyleFromLightStyle(
      ISpreadStreamExportCellStyleInfo cellStyle);

    void CreateCellStyleFromTheme();

    void CreateCellStyle(
      Color backColor,
      Color foreColor,
      FontFamily fontFamily,
      double fontSize,
      bool isBold,
      bool isItalic,
      bool underline,
      ContentAlignment textAlignment,
      bool textWrap,
      BorderBoxStyle borderBoxStyle,
      Color borderColor,
      Color borderTopColor,
      Color borderBottomColor,
      Color borderRightColor,
      Color borderLeftColor);

    void CreateLightCellStyle(
      Color backColor,
      Color foreColor,
      FontFamily fontFamily,
      double fontSize,
      bool isBold,
      bool isItalic,
      bool underline,
      ContentAlignment textAlignment,
      bool textWrap,
      BorderBoxStyle borderBoxStyle,
      Color borderColor,
      Color borderTopColor,
      Color borderBottomColor,
      Color borderRightColor,
      Color borderLeftColor);

    void CreateBorderCellStyle(ISpreadStreamExportCellStyleInfo cellStyle);

    ISpreadStreamExportCellStyleInfo GetBordersFromExistingStyle(
      ISpreadStreamExportCellStyleInfo cellStyle);

    void GroupCurrentRow(int level);

    bool GetIsMerged(int rowIndex, int columnIndex);

    void CreateFreezePanes(int rowsCount, int columnsCount);
  }
}
