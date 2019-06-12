// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadContextMenuDropDown
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadContextMenuDropDown : RadDropDownMenu
  {
    public RadContextMenuDropDown()
    {
    }

    public RadContextMenuDropDown(RadElement ownerElement)
      : base(ownerElement)
    {
    }

    protected override void OnDropDownOpening(CancelEventArgs args)
    {
      base.OnDropDownOpening(args);
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) this.Items)
        this.ShowItemCues(radMenuItemBase);
    }

    private void ShowItemCues(RadMenuItemBase item)
    {
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) item.Items)
        this.ShowItemCues(radMenuItemBase);
      RadMenuItem radMenuItem = item as RadMenuItem;
      if (radMenuItem == null)
        return;
      radMenuItem.ShowKeyboardCue = true;
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      return true;
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadDropDownMenu).FullName;
      }
    }
  }
}
