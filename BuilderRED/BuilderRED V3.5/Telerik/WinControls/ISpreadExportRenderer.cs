// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ISpreadExportRenderer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Telerik.WinControls.Export;

namespace Telerik.WinControls
{
  public interface ISpreadExportRenderer
  {
    object GetFileExtension(SpreadExportFormat exportFormat);

    void RegisterFormatProvider(SpreadExportFormat exportFormat);

    void GetFormatProvider(SpreadExportFormat exportFormat);

    void ImportWorkbook(Stream stream);

    void Export(Stream stream);

    void CreateWorkbook();

    object GetWorksheet();

    void AddWorksheet(string sheetName);

    void SetWorksheetRowHeight(int rowIndex, int rowHeight, bool isCustom);

    double GetWorksheetRowHeight(int rowIndex);

    int GetWorksheetColumnWidth(int columnIndex);

    void SetWorksheetColumnWidth(int columnIndex, double value, bool isCustom);

    object GetCellSelection();

    void CreateCellSelection(int rowIndex, int columnIndex);

    void CreateCellSelection(
      int fromRowIndex,
      int fromColumnIndex,
      int toRowIndex,
      int toColumnIndex);

    void MergeCellSelection();

    object GetCellSelectionValue();

    void SetCellSelectionValue(string text);

    void SetCellSelectionValue(DataType dataType, object value);

    void CreateFloatingImage(
      int rowIndex,
      int columnIndex,
      int offsetX,
      int offsetY,
      byte[] bytes,
      string extension,
      int width,
      int height,
      int rotationAngle);

    void SetCellSelectionFormat(string formatString);

    void ClearCellSelectionValue();

    bool GetIsMerged(int rowIndex, int columnIndex);

    ISpreadExportCellStyleInfo GetCellStyleInfo();

    void CreateCellStyleInfo();

    void CreateCellStyleInfo(
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

    IGridViewSpreadExportRowInfo CreateGridViewExportDataRowInfo(
      int currentIndent,
      List<IGridViewSpreadExportCellInfo> cells,
      bool exportAsHidden);

    IGridViewSpreadExportRowInfo CreateGridViewExportDataRowInfo(
      int currentIndent,
      List<IGridViewSpreadExportCellInfo> cells,
      bool exportAsHidden,
      int hierarchyLevel);

    IGridViewSpreadExportRowInfo CreateGridViewExportGroupRowInfo(
      int currentIndent,
      string content,
      int colSpan,
      bool exportAsHidden);

    IGridViewSpreadExportRowInfo CreateGridViewExportGroupRowInfo(
      int currentIndent,
      string content,
      int colSpan,
      bool exportAsHidden,
      int hierarchyLevel);

    void CallWorkbookCreated();

    void CreateFreezePanes(int rowsCount, int columnsCount);

    void GroupRows(int startRow, int endRow, int level);
  }
}
