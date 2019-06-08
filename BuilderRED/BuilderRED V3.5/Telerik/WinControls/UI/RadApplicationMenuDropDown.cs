// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadApplicationMenuDropDown
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [Description("Builds attractive application menu")]
  [RadToolboxItem(false)]
  [ToolboxItem(false)]
  [DefaultBindingProperty("Items")]
  [DefaultProperty("Items")]
  public class RadApplicationMenuDropDown : RadDropDownButtonPopup
  {
    private RadItemOwnerCollection buttonItems;
    private RadItemOwnerCollection rightColumnItems;

    public RadApplicationMenuDropDown(RadApplicationMenuButtonElement ownerElement)
      : base((RadDropDownButtonElement) ownerElement)
    {
      this.FadeAnimationType = FadeAnimationType.FadeOut;
      this.DropShadow = true;
      this.HorizontalAlignmentCorrectionMode = AlignmentCorrectionMode.Smooth;
    }

    protected override void Construct()
    {
      this.buttonItems = new RadItemOwnerCollection();
      this.buttonItems.ItemTypes = new System.Type[2]
      {
        typeof (RadMenuItemBase),
        typeof (RadMenuButtonItem)
      };
      this.buttonItems.DefaultType = typeof (RadMenuItem);
      this.buttonItems.ItemsChanged += new ItemChangedDelegate(this.ItemsChanged);
      this.rightColumnItems = new RadItemOwnerCollection();
      this.rightColumnItems.ItemTypes = new System.Type[6]
      {
        typeof (RadMenuItemBase),
        typeof (RadMenuItem),
        typeof (RadMenuSeparatorItem),
        typeof (RadMenuHeaderItem),
        typeof (RadMenuComboItem),
        typeof (RadMenuButtonItem)
      };
      this.rightColumnItems.DefaultType = typeof (RadMenuButtonItem);
      this.rightColumnItems.ItemsChanged += new ItemChangedDelegate(this.ItemsChanged);
      base.Construct();
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadApplicationMenuDropDown).FullName;
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    [DefaultValue(300)]
    public int RightColumnWidth
    {
      get
      {
        RadApplicationMenuDropDownElement popupElement = this.PopupElement as RadApplicationMenuDropDownElement;
        if (popupElement != null)
          return popupElement.RightColumnWidth;
        return 0;
      }
      set
      {
        RadApplicationMenuDropDownElement popupElement = this.PopupElement as RadApplicationMenuDropDownElement;
        if (popupElement == null)
          return;
        popupElement.RightColumnWidth = value;
        this.ElementTree.PerformInnerLayout(true, 0, 0, this.DefaultSize.Width, this.DefaultSize.Height);
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual RadItemOwnerCollection RightColumnItems
    {
      get
      {
        return this.rightColumnItems;
      }
    }

    [Category("Data")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual RadItemOwnerCollection ButtonItems
    {
      get
      {
        return this.buttonItems;
      }
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (element.GetType().Equals(typeof (RadMenuButtonItem)))
        return false;
      return base.ControlDefinesThemeForElement(element);
    }

    protected override RadElement CreatePopupElement()
    {
      RadApplicationMenuDropDownElement menuDropDownElement = new RadApplicationMenuDropDownElement();
      this.Items.Owner = menuDropDownElement.MenuElement.LayoutPanel;
      this.RightColumnItems.Owner = (RadElement) menuDropDownElement.TopRightContentElement.Layout;
      this.buttonItems.Owner = (RadElement) menuDropDownElement.BottomContentElement.Layout;
      return (RadElement) menuDropDownElement;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.buttonItems.ItemsChanged -= new ItemChangedDelegate(this.ItemsChanged);
        this.rightColumnItems.ItemsChanged -= new ItemChangedDelegate(this.ItemsChanged);
      }
      base.Dispose(disposing);
    }

    protected override void ShowCore(Point point, int ownerOffset, RadDirection popupDirection)
    {
      RadApplicationMenuDropDownElement popupElement = ((RadDropDownMenu) this.ElementTree.Control).PopupElement as RadApplicationMenuDropDownElement;
      if (popupElement != null && !this.IsDesignMode)
      {
        popupElement.InvalidateMeasure(true);
        popupElement.UpdateLayout();
        Size size = popupElement.TopRightContentElement.Layout.Size;
        size.Width -= 3;
        size.Height -= 3;
        this.PopupElement.MinSize = size;
        int num = (int) this.PopupElement.SetValue(RadDropDownMenuElement.DropDownPositionProperty, (object) DropDownPosition.RightPopupContent);
      }
      base.ShowCore(point, ownerOffset, popupDirection);
    }

    private void ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      RadMenuItemBase radMenuItemBase = target as RadMenuItemBase;
      if (radMenuItemBase == null)
        return;
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
        case ItemsChangeOperation.Set:
          radMenuItemBase.HierarchyParent = this.OwnerElement as IHierarchicalItem;
          break;
        case ItemsChangeOperation.Removed:
          radMenuItemBase.HierarchyParent = (IHierarchicalItem) null;
          break;
      }
    }

    public override bool OnMouseWheel(Control target, int delta)
    {
      RadApplicationMenuDropDownElement popupElement = this.PopupElement as RadApplicationMenuDropDownElement;
      if (popupElement == null)
        return false;
      if (popupElement.MenuElement.ScrollPanel.VerticalScrollBar.Visibility != ElementVisibility.Visible)
        return true;
      if (delta > 0)
        popupElement.MenuElement.ScrollPanel.LineUp();
      else
        popupElement.MenuElement.ScrollPanel.LineDown();
      return true;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (this.RootElement.Shape != null && this.RootElement.ApplyShapeToControl)
        this.Region = this.RootElement.Shape.CreateRegion(new Rectangle(0, 0, this.Width, this.Height));
      else
        this.Region = Region.FromHrgn(Telerik.WinControls.NativeMethods.CreateRoundRectRgn(0, 0, this.Bounds.Right - this.Bounds.Left + 1, this.Bounds.Bottom - this.Bounds.Top + 1, 4, 4));
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      if (this.OwnerElement != null && this.OwnerElement.IsInValidState(true))
      {
        RadRibbonBar control = this.OwnerElement.ElementTree.Control as RadRibbonBar;
        if (control != null && control.IsDesignMode)
        {
          Point screen = control.Parent.PointToScreen(control.Location);
          if (new Rectangle(screen, control.Size).Contains(Control.MousePosition))
          {
            Point location = screen;
            location.Offset(control.RibbonBarElement.ApplicationButtonElement.ControlBoundingRectangle.Location);
            return !new Rectangle(location, control.RibbonBarElement.ApplicationButtonElement.ControlBoundingRectangle.Size).Contains(Control.MousePosition);
          }
        }
      }
      if (this.OwnerElement != null && this.OwnerElement.IsInValidState(true) && (this.OwnerElement.ElementTree.Control is RadApplicationMenu && (this.OwnerElement.ElementTree.Control as RadApplicationMenu).IsDesignMode) && new Rectangle(this.OwnerElement.ElementTree.Control.Parent.PointToScreen(this.OwnerElement.ElementTree.Control.Location), this.OwnerElement.ElementTree.Control.Size).Contains(Control.MousePosition))
        return false;
      return base.CanClosePopup(reason);
    }
  }
}
