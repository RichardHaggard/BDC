// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.ITextProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public interface ITextProvider
  {
    string Text { get; set; }

    ContentAlignment TextAlignment { get; set; }

    ShadowSettings Shadow { get; set; }

    Orientation TextOrientation { get; set; }

    bool FlipText { get; set; }

    bool AutoEllipsis { get; set; }

    bool UseMnemonic { get; set; }

    RectangleF GetFaceRectangle();

    bool TextWrap { get; set; }

    Font Font { set; get; }

    bool ShowKeyboardCues { get; set; }

    bool MeasureTrailingSpaces { set; get; }
  }
}
