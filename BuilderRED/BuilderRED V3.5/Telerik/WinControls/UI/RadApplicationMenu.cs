// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadApplicationMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Menus & Toolbars")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Description("Builds attractive application menu")]
  [DefaultBindingProperty("Items")]
  [DefaultProperty("Items")]
  [ToolboxItem(true)]
  [RadToolboxItem(true)]
  [ToolboxBitmap(typeof (RadContextMenu), "RadApplicationMenu.bmp")]
  [Designer("Telerik.WinControls.UI.Design.RadApplicationMenuDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadApplicationMenu : RadDropDownButton
  {
    public RadApplicationMenu()
    {
      this.DisplayStyle = DisplayStyle.Image;
    }

    protected override RadDropDownButtonElement CreateButtonElement()
    {
      return (RadDropDownButtonElement) new RadApplicationMenuButtonElement();
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(48, 48));
      }
    }

    [Category("Appearance")]
    [DefaultValue(DisplayStyle.Image)]
    [Browsable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public override DisplayStyle DisplayStyle
    {
      get
      {
        return base.DisplayStyle;
      }
      set
      {
        base.DisplayStyle = value;
      }
    }

    [RadEditItemsAction]
    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection RightColumnItems
    {
      get
      {
        return (this.DropDownButtonElement.DropDownMenu as RadApplicationMenuDropDown)?.RightColumnItems;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [RadEditItemsAction]
    [Browsable(true)]
    public RadItemOwnerCollection ButtonItems
    {
      get
      {
        return (this.DropDownButtonElement.DropDownMenu as RadApplicationMenuDropDown)?.ButtonItems;
      }
    }

    [DefaultValue(300)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Category("Appearance")]
    public int RightColumnWidth
    {
      get
      {
        return ((RadApplicationMenuDropDown) this.DropDownButtonElement.DropDownMenu).RightColumnWidth;
      }
      set
      {
        ((RadApplicationMenuDropDown) this.DropDownButtonElement.DropDownMenu).RightColumnWidth = value;
      }
    }

    [Description("Gets or sets the whether RadApplicationMenu will have TwoColumnDropDownMenu.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool ShowTwoColumnDropDownMenu
    {
      get
      {
        return ((RadApplicationMenuButtonElement) this.DropDownButtonElement).ShowTwoColumnDropDownMenu;
      }
      set
      {
        ((RadApplicationMenuButtonElement) this.DropDownButtonElement).ShowTwoColumnDropDownMenu = value;
      }
    }

    public override bool ShowItemToolTips
    {
      get
      {
        return this.DropDownButtonElement.DropDownMenu.ShowItemToolTips;
      }
      set
      {
        this.DropDownButtonElement.DropDownMenu.ShowItemToolTips = value;
      }
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      if (this.DropDownButtonElement.DropDownMenu == null || this.DropDownButtonElement.DropDownMenu.Bounds.Contains(Control.MousePosition))
        return;
      this.DropDownButtonElement.DropDownMenu.ClosePopup(RadPopupCloseReason.AppFocusChange);
    }

    protected override void SetBackColorThemeOverrides()
    {
    }

    protected override void SetForeColorThemeOverrides()
    {
    }
  }
}
