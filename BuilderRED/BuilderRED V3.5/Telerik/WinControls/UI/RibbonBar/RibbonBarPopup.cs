// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RibbonBar.RibbonBarPopup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI.RibbonBar
{
  [ToolboxItem(false)]
  public class RibbonBarPopup : RadPopupControlBase
  {
    private bool isPopupShown;

    public RibbonBarPopup(RadRibbonBarElement ownerRibbon)
      : base((RadElement) ownerRibbon)
    {
      this.FadeAnimationType = FadeAnimationType.FadeOut;
      this.DropShadow = true;
      this.FadeAnimationFrames = 30;
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadRibbonBar).FullName;
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    public bool IsPopupShown
    {
      get
      {
        return this.isPopupShown;
      }
    }

    internal RadRibbonBarElement RibbonBarElement
    {
      get
      {
        return this.OwnerElement as RadRibbonBarElement;
      }
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      System.Type type = element.GetType();
      if ((object) type == (object) typeof (RadButtonElement) || (object) type == (object) typeof (RadRibbonBarButtonGroup) || ((object) type == (object) typeof (RadRibbonBarElement) || (object) type == (object) typeof (RadScrollViewer)) || ((object) type == (object) typeof (RadCheckBoxElement) || (object) type == (object) typeof (RadToggleButtonElement) || ((object) type == (object) typeof (RadDropDownButtonElement) || (object) type == (object) typeof (RadRepeatButtonElement))))
        return true;
      if (type.Equals(typeof (RadDropDownTextBoxElement)))
      {
        if (element.FindAncestorByThemeEffectiveType(typeof (RadDropDownListElement)) != null)
          return true;
      }
      else if (type.Equals(typeof (RadMaskedEditBoxElement)) && element.FindAncestor<RadDateTimePickerElement>() != null)
        return true;
      return false;
    }

    protected override Point GetCorrectedLocation(
      Screen currentScreen,
      Rectangle alignmentRectangle)
    {
      return alignmentRectangle.Location;
    }

    public override void ShowPopup(Rectangle alignmentRectangle)
    {
      base.ShowPopup(alignmentRectangle);
      this.isPopupShown = true;
    }

    public override void ClosePopup(RadPopupCloseReason reason)
    {
      base.ClosePopup(reason);
      this.isPopupShown = false;
    }

    protected override bool ProcessFocusRequested(RadElement element)
    {
      return false;
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      if (this.RibbonBarElement.TabStripElement.ItemContainer.ControlBoundingRectangle.Contains((this.RibbonBarElement.ElementTree.Control as RadRibbonBar).PointToClient(Cursor.Position)) && reason != RadPopupCloseReason.AppFocusChange)
        return false;
      Control control = Control.FromHandle(Telerik.WinControls.NativeMethods._WindowFromPoint(new Telerik.WinControls.NativeMethods.POINTSTRUCT() { x = Cursor.Position.X, y = Cursor.Position.Y }));
      if (control is ZoomPopup || control is RadDropDownMenu)
        return false;
      return base.CanClosePopup(reason);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      RadElement elementAtPoint = this.RootElement.ElementTree.GetElementAtPoint(e.Location);
      if (elementAtPoint == null || elementAtPoint.FindAncestor<PopupEditorBaseElement>() != null || (elementAtPoint.FindAncestor<RadGalleryElement>() != null || elementAtPoint is RadGalleryElement) || (elementAtPoint.FindAncestor<RadDropDownButtonElement>() != null || elementAtPoint is RadDropDownButtonElement || elementAtPoint is ActionButtonElement))
        return;
      this.ClosePopup(RadPopupCloseReason.Mouse);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.Region = Region.FromHrgn(Telerik.WinControls.NativeMethods.CreateRoundRectRgn(0, 0, this.Bounds.Right - this.Bounds.Left + 1, this.Bounds.Bottom - this.Bounds.Top + 1, 4, 4));
    }

    protected override void OnScreenTipNeeded(object sender, ScreenTipNeededEventArgs e)
    {
      base.OnScreenTipNeeded(sender, e);
      ((IComponentTreeHandler) this.RibbonBarElement.ElementTree.Control).CallOnScreenTipNeeded(sender, e);
    }

    protected override void OnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
    {
      base.OnToolTipTextNeeded(sender, e);
      ((IComponentTreeHandler) this.RibbonBarElement.ElementTree.Control).CallOnToolTipTextNeeded(sender, e);
    }
  }
}
