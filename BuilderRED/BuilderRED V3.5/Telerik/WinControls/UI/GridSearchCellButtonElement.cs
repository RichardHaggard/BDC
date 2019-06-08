// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridSearchCellButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class GridSearchCellButtonElement : RadButtonElement
  {
    private ArrowPrimitive arrow;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DisplayStyle = DisplayStyle.None;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.arrow = this.CreateArrowPrimitive();
      this.arrow.ZIndex = 4;
      int num = (int) this.arrow.BindProperty(VisualElement.ForeColorProperty, (RadObject) this.TextElement, VisualElement.ForeColorProperty, PropertyBindingOptions.OneWay);
      this.Children.Add((RadElement) this.arrow);
    }

    protected override void DisposeManagedResources()
    {
      int num = (int) this.arrow.UnbindProperty(VisualElement.ForeColorProperty);
      base.DisposeManagedResources();
    }

    protected virtual ArrowPrimitive CreateArrowPrimitive()
    {
      ArrowPrimitive arrowPrimitive = new ArrowPrimitive();
      arrowPrimitive.Alignment = ContentAlignment.MiddleCenter;
      return arrowPrimitive;
    }

    public ArrowPrimitive Arrow
    {
      get
      {
        return this.arrow;
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadButtonElement);
      }
    }
  }
}
