// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WizardTextElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class WizardTextElement : BaseWizardElement
  {
    protected override void PaintOverride(
      IGraphics screenRadGraphics,
      Rectangle clipRectangle,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      base.PaintOverride(screenRadGraphics, clipRectangle, angle, scale, useRelativeTransformation);
      if (this.Visibility != ElementVisibility.Visible || this.Parent == null)
        return;
      WizardPageHeaderElement parent = this.Parent as WizardPageHeaderElement;
      if (parent == null)
        return;
      RadWizardElement owner = parent.Owner;
      if (!DWMAPI.IsCompositionEnabled || this.IsDesignMode || (owner == null || owner.OwnerControl == null) || (owner.Mode != WizardMode.Aero || !owner.EnableAeroStyle))
        return;
      TelerikPaintHelper.DrawGlowingText((Graphics) screenRadGraphics.UnderlayGraphics, this.Text, this.Font, this.ControlBoundingRectangle, this.ForeColor, TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
    }
  }
}
