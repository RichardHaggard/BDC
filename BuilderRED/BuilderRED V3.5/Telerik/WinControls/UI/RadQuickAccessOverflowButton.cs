// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadQuickAccessOverflowButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Runtime.InteropServices;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  [ComVisible(false)]
  public class RadQuickAccessOverflowButton : RadDropDownButtonElement
  {
    private OverflowPrimitive overFlowPrimitive;
    private FillPrimitive fill;
    private BorderPrimitive border;
    private HiddenItemsPrimitive hiddenItems;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
    }

    protected override void CreateChildElements()
    {
      this.fill = new FillPrimitive();
      this.fill.Class = "overFlowButtonFill";
      this.Children.Add((RadElement) this.fill);
      this.border = new BorderPrimitive();
      this.border.Class = "overFlowBorder";
      this.Children.Add((RadElement) this.border);
      this.hiddenItems = new HiddenItemsPrimitive();
      this.hiddenItems.Class = "OverFlowArrows";
      this.hiddenItems.Visibility = ElementVisibility.Hidden;
      this.Children.Add((RadElement) this.hiddenItems);
      this.overFlowPrimitive = new OverflowPrimitive(ArrowDirection.Down);
      this.overFlowPrimitive.Class = "overFlowButton";
      this.overFlowPrimitive.MinSize = new Size(6, 7);
      this.Children.Add((RadElement) this.overFlowPrimitive);
    }

    public override DropDownButtonArrowPosition ArrowPosition
    {
      get
      {
        return DropDownButtonArrowPosition.Left;
      }
    }

    public override bool ShowArrow
    {
      get
      {
        return true;
      }
    }

    public OverflowPrimitive OverFlowPrimitive
    {
      get
      {
        return this.overFlowPrimitive;
      }
    }

    public FillPrimitive Fill
    {
      get
      {
        return this.fill;
      }
    }

    public BorderPrimitive Border
    {
      get
      {
        return this.border;
      }
    }

    public void ShowSmallArrows(bool show)
    {
      if (show)
        this.hiddenItems.Visibility = ElementVisibility.Visible;
      else
        this.hiddenItems.Visibility = ElementVisibility.Hidden;
    }

    protected override void ShowDropDownOnClick()
    {
      if (this.DropDownMenu.IsVisible)
        this.HideDropDown();
      else
        base.ShowDropDownOnClick();
    }
  }
}
