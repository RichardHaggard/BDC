// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MessageBoxDetailsButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class MessageBoxDetailsButtonElement : RadButtonElement
  {
    private ArrowPrimitive arrow;

    public ArrowPrimitive Arrow
    {
      get
      {
        return this.arrow;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.arrow = new ArrowPrimitive(ArrowDirection.Down);
      this.arrow.ZIndex = 4;
      int num = (int) this.arrow.BindProperty(VisualElement.ForeColorProperty, (RadObject) this.TextElement, VisualElement.ForeColorProperty, PropertyBindingOptions.OneWay);
      this.Children.Add((RadElement) this.arrow);
    }

    protected override void DisposeManagedResources()
    {
      int num = (int) this.arrow.UnbindProperty(VisualElement.ForeColorProperty);
      base.DisposeManagedResources();
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadButtonElement);
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF finalSize1 = base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize1);
      float num = (float) (((double) clientRectangle.Height - (double) this.arrow.DesiredSize.Height) / 2.0);
      this.Arrow.Arrange(new RectangleF(clientRectangle.X + num, clientRectangle.Y + num, this.arrow.DesiredSize.Width, this.arrow.DesiredSize.Height));
      return finalSize1;
    }
  }
}
