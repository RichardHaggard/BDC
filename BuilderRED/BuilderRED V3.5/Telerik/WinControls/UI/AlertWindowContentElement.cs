// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AlertWindowContentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class AlertWindowContentElement : LightVisualElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(5);
      this.TextWrap = true;
      this.TextAlignment = ContentAlignment.TopLeft;
      this.ImageAlignment = ContentAlignment.MiddleLeft;
      this.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.AutoEllipsis = true;
      this.StretchVertically = false;
      this.BypassLayoutPolicies = true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RadDesktopAlertElement ancestor = this.FindAncestor<RadDesktopAlertElement>();
      if (ancestor != null && ancestor.AutoSizeHeight)
        return base.MeasureOverride(new SizeF(availableSize.Width, (float) int.MaxValue));
      return base.MeasureOverride(availableSize);
    }
  }
}
