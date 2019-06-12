// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RibbonBar.RibbonBarCaptionFillPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI.RibbonBar
{
  public class RibbonBarCaptionFillPrimitive : FillPrimitive
  {
    public override void PaintPrimitive(IGraphics g, float angle, SizeF scale)
    {
      RadRibbonBar control = this.ElementTree.Control as RadRibbonBar;
      if (control == null)
        return;
      RadFormControlBase form = control.FindForm() as RadFormControlBase;
      if (form != null && form.FormBehavior is RadRibbonFormBehavior)
      {
        float num1 = (float) form.Width / (float) control.Width;
        int offsetX = (form.Width - control.Width) / 2;
        g.TranslateTransform(-offsetX, 0);
        if ((double) num1 != 0.0 && !float.IsInfinity(num1))
          g.ScaleTransform(new SizeF(num1, 1f));
        base.PaintPrimitive(g, angle, scale);
        g.TranslateTransform(offsetX, 0);
        float num2 = (float) control.Width / (float) form.Width;
        if ((double) num2 == 0.0 || float.IsInfinity(num2))
          return;
        g.ScaleTransform(new SizeF(num2, 1f));
      }
      else
        base.PaintPrimitive(g, angle, scale);
    }
  }
}
