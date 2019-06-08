// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.TextParams
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class TextParams
  {
    public bool autoEllipsis;
    public bool useMnemonic;
    public bool useCompatibleTextRendering;
    public bool flipText;
    public bool measureTrailingSpaces;
    public bool rightToLeft;
    public bool showKeyboardCues;
    public bool textWrap;
    public bool stretchHorizontally;
    public bool enabled;
    public ContentAlignment alignment;
    public Orientation textOrientation;
    public TextRenderingHint textRenderingHint;
    public string text;
    public RectangleF paintingRectangle;
    public Font font;
    public Color backColor;
    public Color foreColor;
    public Color highlightColor;
    public CharacterRange[] highlightRanges;
    public ShadowSettings shadow;
    public bool ClipText;
    public bool lineLimit;
    public bool forceBackColor;

    public virtual StringFormat CreateStringFormat()
    {
      StringFormat stringFormat = TelerikHelper.StringFormatForAlignment(this.alignment);
      if (this.rightToLeft)
        stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
      if (this.autoEllipsis)
        stringFormat.Trimming = StringTrimming.EllipsisCharacter;
      if (this.lineLimit)
        stringFormat.FormatFlags |= StringFormatFlags.LineLimit;
      stringFormat.HotkeyPrefix = this.useMnemonic ? (!this.showKeyboardCues ? HotkeyPrefix.Hide : HotkeyPrefix.Show) : HotkeyPrefix.None;
      if (this.measureTrailingSpaces)
        stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
      if (!this.textWrap)
        stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
      if (this.ClipText)
        stringFormat.FormatFlags &= ~StringFormatFlags.NoClip;
      else
        stringFormat.FormatFlags |= StringFormatFlags.NoClip;
      stringFormat.Alignment = !this.rightToLeft ? TelerikAlignHelper.TranslateAlignment(this.alignment) : TelerikAlignHelper.TranslateAlignment(TelerikAlignHelper.RtlTranslateContent(this.alignment));
      stringFormat.LineAlignment = TelerikAlignHelper.TranslateLineAlignment(this.alignment);
      return stringFormat;
    }
  }
}
