// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.ListViewSpreadExportCell
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.Export
{
  public class ListViewSpreadExportCell
  {
    public ListViewSpreadExportCell()
    {
      this.ColSpan = 1;
      this.BackColor = Color.White;
      this.ForeColor = Color.Black;
      this.BorderColor = Color.Transparent;
    }

    public int ColSpan { get; set; }

    public System.Type Type { get; set; }

    public Size Size { get; set; }

    public object Value { get; set; }

    public string Text { get; set; }

    public string FormatString { get; set; }

    public bool TextWrap { get; set; }

    public Font Font { get; set; }

    public ContentAlignment TextAlignment { get; set; }

    public Image Image { get; set; }

    public ContentAlignment ImageAlignment { get; set; }

    public TextImageRelation TextImageRelation { get; set; }

    public Color BackColor { get; set; }

    public Color ForeColor { get; set; }

    public BorderBoxStyle BorderBoxStyle { get; set; }

    public Color BorderColor { get; set; }

    public Color BorderLeftColor { get; set; }

    public Color BorderTopColor { get; set; }

    public Color BorderRightColor { get; set; }

    public Color BorderBottomColor { get; set; }
  }
}
