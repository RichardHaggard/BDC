// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class TextElement : BarcodeElementBase
  {
    private string text;
    private Font font;
    private Color foreColor;

    public TextElement(RectangleF bounds, string text)
      : this(bounds, text, Color.Black)
    {
    }

    public TextElement(RectangleF bounds, string text, Color foreColor)
      : base(bounds)
    {
      this.text = text;
      this.foreColor = foreColor;
    }

    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        this.text = value;
      }
    }

    public Font Font
    {
      get
      {
        return this.font;
      }
      set
      {
        this.font = value;
      }
    }

    public Color ForeColor
    {
      get
      {
        return this.foreColor;
      }
      set
      {
        this.foreColor = value;
      }
    }

    public override void PaintElement(Graphics g)
    {
      using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
        g.DrawString(this.Text, this.Font, (Brush) solidBrush, this.Bounds);
    }
  }
}
