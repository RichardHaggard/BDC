// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.InnerItemLayoutElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class InnerItemLayoutElement : LightVisualElement
  {
    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      foreach (RadElement child in this.Children)
      {
        child.Measure(availableSize);
        empty.Height = Math.Max(empty.Height, child.DesiredSize.Height);
        empty.Width += child.DesiredSize.Width;
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          if ((double) empty.Width > (double) availableSize.Width)
          {
            child.Visibility = ElementVisibility.Hidden;
            empty.Width -= child.DesiredSize.Width;
          }
          else
            child.Visibility = ElementVisibility.Visible;
        }
      }
      empty.Width = Math.Min(availableSize.Width, empty.Width);
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float x1 = clientRectangle.X;
      foreach (RadElement child in this.Children)
      {
        float width = child.DesiredSize.Width;
        float x2 = x1;
        if (this.RightToLeft)
          x2 = clientRectangle.Right - x1 - width;
        child.Arrange(new RectangleF(x2, clientRectangle.Y, width, clientRectangle.Height));
        x1 += width;
      }
      return finalSize;
    }
  }
}
