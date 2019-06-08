// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.ITextPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public interface ITextPrimitive
  {
    void PaintPrimitive(IGraphics graphics, float angle, SizeF scale, TextParams textParams);

    void PaintPrimitive(IGraphics graphics, TextParams textParams);

    SizeF MeasureOverride(SizeF availableSize, TextParams textParams);

    void OnMouseMove(object sender, MouseEventArgs e);

    SizeF GetTextSize(SizeF proposedSize, TextParams textParams);

    SizeF GetTextSize(TextParams textParams);
  }
}
