// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownButtonPopup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadDropDownButtonPopup : RadDropDownMenu
  {
    public RadDropDownButtonPopup(RadDropDownButtonElement ownerElement)
      : base((RadElement) ownerElement)
    {
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadDropDownMenu).FullName;
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    protected override void OnDropDownOpening(CancelEventArgs args)
    {
      base.OnDropDownOpening(args);
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem is RadMenuItem)
          this.ShowItemCues(radItem as RadMenuItem);
      }
    }

    protected virtual void AdjustDropDownAlignmentForPopupDirection(RadDirection popupDirection)
    {
      switch (popupDirection)
      {
        case RadDirection.Left:
          this.HorizontalPopupAlignment = HorizontalPopupAlignment.RightToLeft;
          this.VerticalPopupAlignment = VerticalPopupAlignment.TopToTop;
          break;
        case RadDirection.Right:
          this.HorizontalPopupAlignment = HorizontalPopupAlignment.LeftToRight;
          this.VerticalPopupAlignment = VerticalPopupAlignment.TopToTop;
          break;
        case RadDirection.Up:
          this.HorizontalPopupAlignment = this.OwnerElement == null || !this.OwnerElement.RightToLeft ? HorizontalPopupAlignment.LeftToLeft : HorizontalPopupAlignment.RightToRight;
          this.VerticalPopupAlignment = VerticalPopupAlignment.BottomToTop;
          break;
        case RadDirection.Down:
          this.HorizontalPopupAlignment = this.OwnerElement == null || !this.OwnerElement.RightToLeft ? HorizontalPopupAlignment.LeftToLeft : HorizontalPopupAlignment.RightToRight;
          this.VerticalPopupAlignment = VerticalPopupAlignment.TopToBottom;
          break;
      }
    }

    protected override void ShowCore(Point point, int ownerOffset, RadDirection popupDirection)
    {
      this.AdjustDropDownAlignmentForPopupDirection(popupDirection);
      base.ShowCore(point, ownerOffset, popupDirection);
    }

    private void ShowItemCues(RadMenuItem item)
    {
      foreach (RadItem radItem in (RadItemCollection) item.Items)
      {
        if (radItem is RadMenuItem)
          this.ShowItemCues(radItem as RadMenuItem);
      }
      RadMenuItem radMenuItem = item;
      if (radMenuItem == null)
        return;
      radMenuItem.ShowKeyboardCue = true;
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      if (this.OwnerElement.IsDesignMode && new Rectangle(this.OwnerElement.ElementTree.Control.Parent.PointToScreen(this.OwnerElement.ElementTree.Control.Location), this.OwnerElement.ElementTree.Control.Size).Contains(Control.MousePosition))
        return false;
      return base.CanClosePopup(reason);
    }

    protected override void OnItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      base.OnItemsChanged(changed, target, operation);
      if (operation != ItemsChangeOperation.Inserted)
        return;
      RadMenuItemBase radMenuItemBase = target as RadMenuItemBase;
      if (radMenuItemBase == null)
        return;
      radMenuItemBase.IsMainMenuItem = false;
    }

    protected internal override Size ApplySizingConstraints(
      Size availableSize,
      Screen currentScreen)
    {
      int width = availableSize.Width;
      int height = availableSize.Height;
      Rectangle boundsFromScreen = this.GetAvailableBoundsFromScreen(currentScreen);
      if ((this.FitToScreenMode & FitToScreenModes.FitHeight) != FitToScreenModes.None && height > boundsFromScreen.Height)
      {
        height = boundsFromScreen.Height;
        if (width + RadScrollBarElement.VerticalScrollBarWidth < boundsFromScreen.Width)
          width += RadScrollBarElement.VerticalScrollBarWidth;
      }
      if ((this.FitToScreenMode & FitToScreenModes.FitWidth) != FitToScreenModes.None && width > boundsFromScreen.Width)
        width = boundsFromScreen.Width;
      return new Size(width, height);
    }
  }
}
