// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadApplicationMenuButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadApplicationMenuButtonElement : RadDropDownButtonElement
  {
    internal const long ShowTwoColumnDropDownMenuStateKey = 35184372088832;

    static RadApplicationMenuButtonElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ApplicationMenuButtonStateManagerFactory(), typeof (RadApplicationMenuButtonElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BitState[35184372088832L] = true;
    }

    protected override RadDropDownButtonPopup CreateDropDown()
    {
      RadApplicationMenuDropDown applicationMenuDropDown = new RadApplicationMenuDropDown(this);
      applicationMenuDropDown.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      applicationMenuDropDown.ThemeClassName = typeof (RadApplicationMenuButtonElement).Namespace + ".RadApplicationMenuDropDown";
      applicationMenuDropDown.Items.ItemsChanged += new ItemChangedDelegate(this.OnItemsChanged);
      return (RadDropDownButtonPopup) applicationMenuDropDown;
    }

    private void OnItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
          RadMenuItemBase radMenuItemBase1 = target as RadMenuItemBase;
          if (radMenuItemBase1 == null)
            break;
          radMenuItemBase1.DropDownOpening += new CancelEventHandler(this.OnMenuItem_DropDownOpening);
          break;
        case ItemsChangeOperation.Removed:
          RadMenuItemBase radMenuItemBase2 = target as RadMenuItemBase;
          if (radMenuItemBase2 == null)
            break;
          radMenuItemBase2.DropDownOpening -= new CancelEventHandler(this.OnMenuItem_DropDownOpening);
          break;
        case ItemsChangeOperation.Clearing:
          using (RadItemCollection.RadItemEnumerator enumerator = changed.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              RadItem current = enumerator.Current;
              if (current is RadMenuItemBase)
                ((RadMenuItemBase) current).DropDownOpening -= new CancelEventHandler(this.OnMenuItem_DropDownOpening);
            }
            break;
          }
      }
    }

    private void OnMenuItem_DropDownOpening(object sender, CancelEventArgs e)
    {
      if (this.IsDesignMode)
        return;
      RadMenuItemBase radMenuItemBase = sender as RadMenuItemBase;
      if (radMenuItemBase == null)
        return;
      RadPopupOpeningEventArgs openingEventArgs = e as RadPopupOpeningEventArgs;
      if (!(radMenuItemBase.Parent is RadDropDownMenuLayout))
        return;
      RadDropDownMenuLayout parent = radMenuItemBase.Parent as RadDropDownMenuLayout;
      openingEventArgs.CustomLocation = new Point(openingEventArgs.CustomLocation.X, parent.ControlBoundingRectangle.Y + this.DropDownMenu.Location.Y);
      RadDropDownMenu dropDown = radMenuItemBase.DropDown;
      Size size = new Size(dropDown.Width, parent.ControlBoundingRectangle.Height);
      dropDown.MinimumSize = size;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty || this.DropDownMenu == null)
        return;
      this.DropDownMenu.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
    }

    protected override void OnDropDownOpening(CancelEventArgs e)
    {
      base.OnDropDownOpening(e);
      if (!this.IsDesignMode)
        return;
      RadApplicationMenuDropDownElement popupElement = this.DropDownMenu.PopupElement as RadApplicationMenuDropDownElement;
      if (popupElement == null)
        return;
      int num1 = (int) popupElement.MenuElement.SetValue(RadDropDownMenuElement.DropDownPositionProperty, (object) DropDownPosition.LeftContent);
      int num2 = (int) popupElement.TopRightContentElement.SetValue(RadDropDownMenuElement.DropDownPositionProperty, (object) DropDownPosition.RightContent);
      foreach (RadObject radObject in popupElement.TopRightContentElement.ChildrenHierarchy)
      {
        int num3 = (int) radObject.SetValue(RadDropDownMenuElement.DropDownPositionProperty, (object) DropDownPosition.RightContent);
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets the whether RadApplicationMenu will have TwoColumnDropDownMenu.")]
    public bool ShowTwoColumnDropDownMenu
    {
      get
      {
        return this.GetBitState(35184372088832L);
      }
      set
      {
        this.SetBitState(35184372088832L, value);
      }
    }

    protected override void OnBitStateChanged(long key, bool oldValue, bool newValue)
    {
      base.OnBitStateChanged(key, oldValue, newValue);
      if (key != 35184372088832L)
        return;
      if (newValue)
        ((RadApplicationMenuDropDownElement) this.DropDownMenu.PopupElement).TopRightContentElement.Visibility = ElementVisibility.Visible;
      else
        ((RadApplicationMenuDropDownElement) this.DropDownMenu.PopupElement).TopRightContentElement.Visibility = ElementVisibility.Collapsed;
      this.OnNotifyPropertyChanged("ShowTwoColumnDropDownMenu");
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      if (this.DropDownMenu == null)
        return;
      this.DropDownMenu.Items.ItemsChanged -= new ItemChangedDelegate(this.OnItemsChanged);
    }
  }
}
