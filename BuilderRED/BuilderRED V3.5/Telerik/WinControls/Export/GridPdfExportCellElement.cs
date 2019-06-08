// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridPdfExportCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.Export
{
  public class GridPdfExportCellElement
  {
    public System.Type RowType { get; set; }

    public int RowIndex { get; set; }

    public System.Type ColumnType { get; set; }

    public int ColumnIndex { get; set; }

    public Image Image { get; set; }

    public ImageLayout ImageLayout { get; set; }

    public ContentAlignment ImageAlignment { get; set; }

    public Font Font { get; set; }

    public Color ForeColor { get; set; }

    public string Text { get; set; }

    public bool TextWrap { get; set; }

    public ContentAlignment TextAlignment { get; set; }

    public Color BorderColor { get; set; }

    public Color BorderLeftColor { get; set; }

    public Color BorderLeftShadowColor { get; set; }

    public Color BorderTopColor { get; set; }

    public Color BorderTopShadowColor { get; set; }

    public Color BorderRightColor { get; set; }

    public Color BorderRightShadowColor { get; set; }

    public Color BorderBottomColor { get; set; }

    public Color BorderBottomShadowColor { get; set; }

    public float BorderLeftWidth { get; set; }

    public float BorderTopWidth { get; set; }

    public float BorderRightWidth { get; set; }

    public float BorderBottomWidth { get; set; }

    public BorderBoxStyle BorderBoxStyle { get; set; }

    public Color BackColor { get; set; }

    public Color BackColor2 { get; set; }

    public Color BackColor3 { get; set; }

    public Color BackColor4 { get; set; }

    public int NumberOfColors { get; set; }

    public GradientStyles GradientStyle { get; set; }
  }
}
