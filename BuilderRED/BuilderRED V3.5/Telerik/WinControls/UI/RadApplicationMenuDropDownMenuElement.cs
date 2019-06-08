// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadApplicationMenuDropDownMenuElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadApplicationMenuDropDownMenuElement : RadDropDownMenuElement
  {
    private float? cachedVerticalClampMargin = new float?();
    private float? cachedHorizontalClampMargin = new float?();

    protected override SizeF ApplySizeConstraints(SizeF desiredSize)
    {
      RadApplicationMenuDropDownElement ancestor = this.FindAncestor<RadApplicationMenuDropDownElement>();
      if (ancestor == null)
        return desiredSize;
      SizeF desiredSize1 = ancestor.DesiredSize;
      SizeF sizeF = base.ApplySizeConstraints((SizeF) ancestor.DesiredSize.ToSize());
      float num1 = desiredSize1.Height - sizeF.Height;
      if (!this.cachedVerticalClampMargin.HasValue || (double) num1 > 0.0)
        this.cachedVerticalClampMargin = new float?(num1);
      float num2 = desiredSize1.Width - sizeF.Width;
      if (!this.cachedHorizontalClampMargin.HasValue || (double) num2 > 0.0)
        this.cachedHorizontalClampMargin = new float?(num2);
      return new SizeF(desiredSize.Width - this.cachedHorizontalClampMargin.Value, desiredSize.Height - this.cachedVerticalClampMargin.Value);
    }
  }
}
