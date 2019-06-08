// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSpinArrowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadSpinArrowElement : RadItem
  {
    private ArrowPrimitive arrow;

    public RadSpinArrowElement(ArrowDirection direction)
    {
      this.Arrow.Visibility = ElementVisibility.Visible;
      this.Arrow.Direction = direction;
    }

    public ArrowPrimitive Arrow
    {
      get
      {
        return this.arrow;
      }
    }

    protected override void CreateChildElements()
    {
      this.arrow = new ArrowPrimitive();
      this.arrow.Visibility = ElementVisibility.Hidden;
      this.Children.Add((RadElement) this.arrow);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.arrow.Arrange(new RectangleF(finalSize.Width - 12f, (float) (((double) finalSize.Height - 6.0) / 2.0), 6f, 6f));
      return finalSize;
    }
  }
}
