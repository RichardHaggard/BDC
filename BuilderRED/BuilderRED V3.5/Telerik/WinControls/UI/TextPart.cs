// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextPart
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class TextPart : VisualElementLayoutPart
  {
    public TextPart(LightVisualElement owner)
      : base(owner)
    {
    }

    public override SizeF Measure(SizeF availableSize)
    {
      SizeF sizeF = SizeF.Empty;
      if (this.Owner.DrawText)
      {
        TextParams textParams = this.Owner.TextParams;
        sizeF = this.Owner.MeasureOverride(availableSize, textParams);
      }
      this.DesiredSize = sizeF;
      this.DesiredSize = new SizeF(Math.Min(availableSize.Width, this.DesiredSize.Width), Math.Min(availableSize.Height, this.DesiredSize.Height));
      return this.DesiredSize;
    }
  }
}
