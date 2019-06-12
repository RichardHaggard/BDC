// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.TitleBarTextPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.UI;

namespace Telerik.WinControls.Primitives
{
  public class TitleBarTextPrimitive : TextPrimitive
  {
    protected internal override TextParams CreateTextParams()
    {
      TextParams textParams = base.CreateTextParams();
      RadTitleBarElement ancestor = this.FindAncestor<RadTitleBarElement>();
      if (ancestor != null)
      {
        textParams.backColor = ancestor.FillPrimitive.BackColor;
        textParams.forceBackColor = true;
      }
      return textParams;
    }
  }
}
