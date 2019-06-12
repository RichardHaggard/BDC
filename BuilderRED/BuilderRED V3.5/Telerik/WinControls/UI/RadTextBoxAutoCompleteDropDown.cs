// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxAutoCompleteDropDown
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadTextBoxAutoCompleteDropDown : RadEditorPopupControlBase
  {
    public RadTextBoxAutoCompleteDropDown(RadTextBoxControlElement owner)
      : base((RadItem) owner)
    {
      this.SizingGripDockLayout.Children.Add((RadElement) this.TextBox.ListElement);
      this.SizingGrip.ShouldAspectRootElement = false;
      this.SizingMode = SizingMode.UpDownAndRightBottom;
      this.TabStop = false;
    }

    protected RadTextBoxControlElement TextBox
    {
      get
      {
        return this.OwnerElement as RadTextBoxControlElement;
      }
    }

    protected RadTextBoxListElement ListElement
    {
      get
      {
        return this.TextBox.ListElement;
      }
    }

    protected override bool ProcessFocusRequested(RadElement element)
    {
      return false;
    }

    public override bool OnMouseWheel(Control target, int delta)
    {
      base.OnMouseWheel(target, delta);
      this.ListElement.OnMouseWheel(delta);
      return true;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (!PopupManager.Default.ContainsPopup((IPopupControl) this))
        return;
      base.OnMouseUp(e);
      RadPopupCloseReason popupCloseReason = RadPopupCloseReason.Mouse;
      if (!this.CanClosePopup(popupCloseReason))
        return;
      this.ClosePopup(new PopupCloseInfo(popupCloseReason, (object) e));
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      if (reason == RadPopupCloseReason.Mouse)
      {
        Point client = this.PointToClient(Control.MousePosition);
        if (this.RootElement.ControlBoundingRectangle.Contains(client) && this.ListElement.GetVisualItemAtPoint(client) == null)
          return false;
      }
      return base.CanClosePopup(reason);
    }

    public override bool OnKeyDown(Keys keyData)
    {
      KeyEventArgs e = new KeyEventArgs(keyData);
      this.OnKeyDown(e);
      if (e.Handled)
        return true;
      bool flag = false;
      switch (keyData)
      {
        case Keys.Return:
        case Keys.Escape:
          this.ClosePopup(new PopupCloseInfo(RadPopupCloseReason.Keyboard, (object) e));
          flag = keyData != Keys.Return;
          break;
        case Keys.Prior:
        case Keys.Next:
          return this.ProcessPageKeys(keyData);
      }
      return flag;
    }

    protected virtual bool ProcessPageKeys(Keys keyData)
    {
      RadListDataItem selectedItem = this.ListElement.SelectedItem;
      int num1 = this.ListElement.ViewElement.Children.Count - 1;
      int num2 = this.ListElement.SelectedIndex;
      bool flag = false;
      switch (keyData)
      {
        case Keys.Prior:
          num2 = num2 == -1 ? this.ListElement.DataLayer.Items.Last.RowIndex : this.ListElement.GetFirstFullyVisibleItem().RowIndex;
          if (this.ListElement.SelectedIndex == num2)
          {
            num2 -= num1;
            break;
          }
          break;
        case Keys.Next:
          num2 = num2 == -1 ? this.ListElement.DataLayer.Items.First.RowIndex : this.ListElement.GetLastFullyVisibleItem().RowIndex;
          if (this.ListElement.SelectedIndex == num2)
            num2 += num1;
          flag = true;
          break;
      }
      int num3 = this.ListElement.Items.Count - 1;
      if (num2 < 0)
        num2 = 0;
      else if (num2 > num3)
        num2 = num3;
      if (this.ListElement.SelectedIndex == num2)
        return false;
      this.ListElement.SelectedIndex = num2;
      this.ListElement.UpdateLayout();
      if (flag)
        this.ListElement.ScrollToItem(selectedItem);
      return true;
    }

    protected override void OnPopupClosed(PopupCloseInfo info)
    {
      this.OnPopupClosed((RadPopupClosedEventArgs) new RadAutoCompleteDropDownClosedEventArgs(info.CloseReason, info.Context as EventArgs));
    }

    protected override void OnPopupClosed(RadPopupClosedEventArgs args)
    {
      base.OnPopupClosed(args);
      int selectedIndex = this.ListElement.SelectedIndex;
      this.ListElement.SuspendSuggestNotifications();
      this.ListElement.SelectedIndex = -1;
      if (selectedIndex >= 0)
      {
        RadListDataItem radListDataItem = this.ListElement.Items[selectedIndex];
        radListDataItem.Selected = false;
        radListDataItem.Active = false;
      }
      this.ListElement.ResumeSuggestNotifications();
    }
  }
}
