// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollBarButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class ScrollBarButton : RadButtonItem
  {
    private ArrowPrimitive arrowPrimitive;
    private FillPrimitive background;
    private BorderPrimitive borderPrimitive;
    private ScrollButtonDirection direction;

    public ScrollBarButton()
    {
    }

    static ScrollBarButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ScrollButtonStateManagerFactory(), typeof (ScrollBarButton));
    }

    public ScrollBarButton(ScrollButtonDirection direction)
    {
      this.Direction = direction;
    }

    protected override void CreateChildElements()
    {
      this.background = new FillPrimitive();
      this.background.ZIndex = 0;
      this.background.Class = "ScrollBarButtonFill";
      this.Children.Add((RadElement) this.background);
      this.arrowPrimitive = new ArrowPrimitive(this.GetArrowDirection(this.Direction));
      this.arrowPrimitive.Alignment = ContentAlignment.MiddleCenter;
      this.arrowPrimitive.ZIndex = 1;
      this.arrowPrimitive.Class = "ScrollBarButtonArrow";
      this.Children.Add((RadElement) this.arrowPrimitive);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "ScrollBarButtonBorder";
      this.borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.Children.Add((RadElement) this.borderPrimitive);
      int num1 = (int) this.background.BindProperty(RadButtonItem.IsPressedProperty, (RadObject) this, RadButtonItem.IsPressedProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.arrowPrimitive.BindProperty(RadButtonItem.IsPressedProperty, (RadObject) this, RadButtonItem.IsPressedProperty, PropertyBindingOptions.OneWay);
    }

    public ScrollButtonDirection Direction
    {
      get
      {
        return this.direction;
      }
      set
      {
        if (this.direction == value)
          return;
        this.direction = value;
        if (this.arrowPrimitive == null)
          return;
        this.arrowPrimitive.Direction = this.GetArrowDirection(this.direction);
      }
    }

    public ArrowPrimitive ArrowPrimitive
    {
      get
      {
        return this.arrowPrimitive;
      }
    }

    public BorderPrimitive ButtonBorder
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    public FillPrimitive ButtonFill
    {
      get
      {
        return this.background;
      }
    }

    private ArrowDirection GetArrowDirection(ScrollButtonDirection buttonDirection)
    {
      return (ArrowDirection) buttonDirection;
    }
  }
}
