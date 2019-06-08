// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTimelineItemBottomStackElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewTimelineItemBottomStackElement : LightVisualElement
  {
    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      int count = this.Children.Count;
      int num1 = (int) Math.Round((double) availableSize.Width, MidpointRounding.AwayFromZero);
      int num2 = num1 / count;
      int num3 = num1 % count;
      foreach (RadElement child in this.Children)
      {
        int num4 = num3 <= 0 ? 0 : 1;
        child.Measure(new SizeF((float) (num2 + num4), availableSize.Height));
      }
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      int count = this.Children.Count;
      int num1 = (int) Math.Round((double) finalSize.Width, MidpointRounding.AwayFromZero);
      int num2 = num1 / count;
      int num3 = num1 % count;
      int num4 = num3 > 0 ? 1 : 0;
      int num5 = 0;
      int num6 = 0;
      foreach (RadElement child in this.Children)
      {
        if (num3 <= 0)
          num4 = 0;
        child.Arrange(new RectangleF((float) num5, (float) num6, (float) (num2 + num4), finalSize.Height));
        --num3;
        num5 += num2 + num4;
      }
      return finalSize;
    }
  }
}
