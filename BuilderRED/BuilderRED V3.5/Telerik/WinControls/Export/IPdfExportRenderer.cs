// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.IPdfExportRenderer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.IO;

namespace Telerik.WinControls.Export
{
  public interface IPdfExportRenderer
  {
    int CurrentMatrixRow { get; set; }

    int CurrentMatrixColumn { get; set; }

    void CreateDocumentPageMatrixEditor(SizeF pageSize, ref IPdfEditor editor);

    void AddMatrixPagesLeftRight();

    void AddMatrixPagesUpDown();

    IPdfEditor GetCurrentPageEditor();

    IPdfEditor GetDownPageEditor();

    IPdfEditor GetRightPageEditor();

    bool IsMatrixCurrentPageLastOnRow();

    void CallCurrentRowPageExported();

    int GetCurrentPageNumber();

    void ExportDocument(Stream stream, string author, string title, string description);

    void CreateBlock();

    void ApplyEditorGraphicAndTextPropertiesToBlock();

    void SetBlockLeftIndent(double indent);

    void InsertBlockText(string text);

    void InsertBlockLineBreak();

    void InsertBlockImage(Stream stream, double width, double height);

    SizeF MeasureBlock();

    SizeF MeasureBlock(SizeF size);

    void DrawBlock();

    void DrawBlock(SizeF size);
  }
}
