// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.IPdfEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Export
{
  public interface IPdfEditor
  {
    double OffsetX { get; }

    double OffsetY { get; }

    void SetStrokeColor(Color color);

    void SetFillColor(Color color);

    void SetTextFontSize(double size);

    bool TrySetFont(string fontName, FontStyle fontStyle);

    void DrawLine(PointF startPoint, PointF endPoint);

    void DrawRectangle(PointF topLeft, PointF bottomRight);

    void SetLinearGradient(
      int numberOfColors,
      PointF startPoint,
      PointF endPoint,
      Color color,
      Color color2,
      Color color3,
      Color color4);

    void PushClipping(double x, double y, double width, double height);

    void PopClipping();

    void CreateMatrixPosition();

    void CreateMatrixPosition(double x, double y);

    void TranslatePosition(double x, double y);

    void SaveProperties();

    void SavePosition();

    void RestoreProperties();

    void RestorePosition();
  }
}
