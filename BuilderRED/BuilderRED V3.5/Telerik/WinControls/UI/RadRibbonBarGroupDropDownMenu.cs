// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonBarGroupDropDownMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadRibbonBarGroupDropDownMenu : RadDropDownButtonPopup
  {
    public RadRibbonBarGroupDropDownMenu(RadRibbonBarGroupDropDownButtonElement element)
      : base((RadDropDownButtonElement) element)
    {
    }

    protected override void InitializeChildren()
    {
      base.InitializeChildren();
      (this.PopupElement as RadDropDownMenuElement).ScrollPanel.HorizontalScrollState = ScrollState.AlwaysHide;
      (this.PopupElement as RadDropDownMenuElement).ScrollPanel.VerticalScrollState = ScrollState.AlwaysHide;
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

    protected override RadElement CreatePopupElement()
    {
      RadRibbonBarGroupDropDownMenuElement dropDownMenuElement = new RadRibbonBarGroupDropDownMenuElement();
      this.Items.Owner = dropDownMenuElement.LayoutPanel;
      return (RadElement) dropDownMenuElement;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location);
      if (elementAtPoint == null || elementAtPoint is RadDropDownListElement || (elementAtPoint is RadSplitButtonElement || elementAtPoint is RadDropDownButtonElement) || (elementAtPoint is RadArrowButtonElement || elementAtPoint.Class == "GalleryPopupButtonButton" || (elementAtPoint.Class == "GalleryDownButton" || elementAtPoint.Class == "GalleryUpButton")) || (elementAtPoint is ActionButtonElement || elementAtPoint is RadGalleryElement || (elementAtPoint is RadRibbonBarButtonGroup || (object) elementAtPoint.GetType() == (object) typeof (RadItem))))
        return;
      this.ClosePopup(RadPopupCloseReason.Mouse);
      base.OnMouseUp(e);
    }
  }
}
