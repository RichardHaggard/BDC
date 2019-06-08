// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.TextFormat
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.Drawing
{
  public sealed class TextFormat : IDisposable
  {
    private ContentAlignment contentAlignment = ContentAlignment.MiddleLeft;
    public static TextFormat Default = new TextFormat();
    private Orientation orientation;
    private TextFormatFlags textFormatFlags;
    private StringFormat format;

    public TextFormat()
      : this(ContentAlignment.MiddleCenter)
    {
    }

    public TextFormat(ContentAlignment contentAlignment)
      : this(contentAlignment, Orientation.Horizontal)
    {
    }

    public TextFormat(ContentAlignment contentAlignment, Orientation orientation)
      : this(contentAlignment, orientation, TextFormatFlags.Default)
    {
    }

    public TextFormat(StringFormat format)
    {
      this.format = format;
    }

    public TextFormat(
      ContentAlignment contentAlignment,
      Orientation orientation,
      TextFormatFlags textFormatFlags)
    {
      this.format = new StringFormat(StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap | StringFormatFlags.NoClip);
      this.ContentAlignment = contentAlignment;
      this.format.Alignment = StringAlignment.Near;
      this.format.LineAlignment = StringAlignment.Center;
      this.format.Trimming = StringTrimming.EllipsisCharacter;
      this.orientation = orientation;
      this.textFormatFlags = textFormatFlags;
    }

    public ContentAlignment ContentAlignment
    {
      get
      {
        return this.contentAlignment;
      }
      set
      {
        if (this.contentAlignment == value)
          return;
        this.contentAlignment = value;
        this.format.Alignment = this.CreateStringAlignment(this.contentAlignment);
      }
    }

    public Orientation Orinetation
    {
      get
      {
        return this.orientation;
      }
      set
      {
        this.orientation = value;
      }
    }

    public TextFormatFlags TextFormatFlags
    {
      get
      {
        return this.textFormatFlags;
      }
      set
      {
        this.textFormatFlags = value;
      }
    }

    public override string ToString()
    {
      return string.Format("TextFormat ContentAlignment:{0}, Orinetation:{1}, TextFormatFlags:{2}", (object) this.contentAlignment, (object) this.orientation, (object) this.textFormatFlags);
    }

    public static implicit operator StringFormat(TextFormat textFormat)
    {
      textFormat.format = new StringFormat(StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap | StringFormatFlags.NoClip);
      textFormat.format.Alignment = StringAlignment.Near;
      textFormat.format.LineAlignment = StringAlignment.Center;
      textFormat.format.Trimming = StringTrimming.EllipsisCharacter;
      return textFormat.format;
    }

    public static explicit operator TextFormat(StringFormat stringFormat)
    {
      return new TextFormat(stringFormat);
    }

    public void Dispose()
    {
      this.format.Dispose();
    }

    private StringAlignment CreateStringAlignment(ContentAlignment textAlign)
    {
      switch (textAlign)
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.BottomLeft:
          return StringAlignment.Near;
        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
        case ContentAlignment.BottomCenter:
          return StringAlignment.Center;
        case ContentAlignment.TopRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.BottomRight:
          return StringAlignment.Far;
        default:
          return StringAlignment.Center;
      }
    }
  }
}
