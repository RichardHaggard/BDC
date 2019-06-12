// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DropDownPopupForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DropDownPopupForm : RadEditorPopupControlBase
  {
    private RadDropDownListElement ownerDropDownListElement;
    private Keys? lastPressedKey;

    public DropDownPopupForm(RadDropDownListElement ownerDropDownListElement)
      : base((RadItem) ownerDropDownListElement)
    {
      this.ownerDropDownListElement = ownerDropDownListElement;
      this.SizingGripDockLayout.Children.Add((RadElement) this.ownerDropDownListElement.ListElement);
      this.SizingGrip.ShouldAspectRootElement = false;
      this.TabStop = false;
    }

    public Keys? LastPressedKey
    {
      get
      {
        return this.lastPressedKey;
      }
      internal set
      {
        this.lastPressedKey = value;
      }
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadDropDownPopupFormAccessibleObject(this);
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.EnableGesture(GestureType.Pan);
    }

    protected override bool ProcessFocusRequested(RadElement element)
    {
      return false;
    }

    public override void ClosePopup(RadPopupCloseReason reason)
    {
      base.ClosePopup(reason);
      this.ownerDropDownListElement.CallClosePopupCore();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.ownerDropDownListElement.ListElement == null || this.ownerDropDownListElement.ListElement.OnControlMouseDown(e))
        return;
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.ownerDropDownListElement.ListElement == null || this.ownerDropDownListElement.ListElement.OnControlMouseMove(e))
        return;
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.ownerDropDownListElement.ListElement == null || this.ownerDropDownListElement.ListElement.OnControlMouseUp(e))
        return;
      base.OnMouseUp(e);
    }

    public override bool OnMouseWheel(Control target, int delta)
    {
      this.CallMouseWheel(new MouseEventArgs(Control.MouseButtons, 0, Control.MousePosition.X, Control.MousePosition.Y, delta));
      if (!this.ownerDropDownListElement.EnableMouseWheel)
        return true;
      base.OnMouseWheel(target, delta);
      this.ownerDropDownListElement.ListElement.OnMouseWheel(delta);
      return true;
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      base.OnMouseClick(e);
      if (this.ownerDropDownListElement.ListElement != null && this.ownerDropDownListElement.ListElement.EnableKineticScrolling && this.ownerDropDownListElement.ListElement.ScrollBehavior.IsRunning || !this.CanClosePopupCore(e.Button, RadPopupCloseReason.Mouse))
        return;
      this.ClosePopup(RadPopupCloseReason.Mouse);
      this.GetMainDropDownListElement().ClosePopup(RadPopupCloseReason.Mouse);
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      return this.CanClosePopupCore(Control.MouseButtons, reason);
    }

    public override bool OnKeyDown(Keys keyData)
    {
      KeyEventArgs e = new KeyEventArgs(keyData);
      if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Prior && e.KeyCode != Keys.Down && e.KeyCode != Keys.Next && !this.ownerDropDownListElement.ContainsFocus)
      {
        this.ownerDropDownListElement.OnKeyDown(e);
        if (e.Handled)
          return false;
      }
      this.lastPressedKey = new Keys?(keyData);
      switch (keyData)
      {
        case Keys.Back:
          return false;
        case Keys.Tab:
          this.GetMainDropDownListElement().ClosePopup(RadPopupCloseReason.Keyboard);
          if (this.ownerDropDownListElement.isSuggestMode)
            this.ownerDropDownListElement.ClosePopup(RadPopupCloseReason.Keyboard);
          return false;
        default:
          RadListElement listElement = this.OwnerDropDownListElement.ListElement;
          bool flag = this.OwnerDropDownListElement.MaxDropDownItems == 1;
          if (listElement.ReadOnly)
            return false;
          if (keyData == Keys.Home)
            listElement.SelectedItem = this.OwnerDropDownListElement.Items.First;
          if (keyData == Keys.End)
            listElement.SelectedItem = this.OwnerDropDownListElement.Items.Last;
          if (keyData == Keys.Up && listElement.Items.Count > 0)
          {
            if (listElement.SelectedIndex > 0)
            {
              --listElement.SelectedIndex;
              if (flag)
                listElement.Scroller.ScrollToItem(listElement.SelectedItem);
            }
            return true;
          }
          if (keyData == Keys.Down && listElement.Items.Count > 0)
          {
            if (listElement.SelectedIndex < listElement.Items.Count - 1)
            {
              ++listElement.SelectedIndex;
              if (flag)
                listElement.Scroller.ScrollToItem(listElement.SelectedItem);
            }
            return true;
          }
          if (keyData == Keys.Prior)
            listElement.ScrollByPage(-1);
          if (keyData == Keys.Next)
            listElement.ScrollByPage(1);
          if (keyData != Keys.Return)
            return base.OnKeyDown(keyData);
          RadListDataItem selectedOrHoveredItem = this.GetSelectedOrHoveredItem();
          if (selectedOrHoveredItem != null)
          {
            this.ownerDropDownListElement.SelectedIndex = selectedOrHoveredItem.RowIndex;
            this.ownerDropDownListElement.ClosePopup(RadPopupCloseReason.Keyboard);
          }
          else
          {
            RadDropDownListElement dropDownListElement = this.GetMainDropDownListElement();
            if (dropDownListElement != null)
            {
              string text = dropDownListElement.TextBox.Text;
              dropDownListElement.SelectItemFromText(text);
              dropDownListElement.ClosePopup(RadPopupCloseReason.Keyboard);
            }
          }
          if (keyData == Keys.Return && this.ownerDropDownListElement.isSuggestMode)
            this.GetMainDropDownListElement()?.ClosePopup(RadPopupCloseReason.Keyboard);
          return base.OnKeyDown(keyData);
      }
    }

    protected virtual bool CanClosePopupCore(MouseButtons mouseButtons, RadPopupCloseReason reason)
    {
      if (reason == RadPopupCloseReason.Mouse && this.ownerDropDownListElement != null && (!this.ownerDropDownListElement.IsDisposed && !this.ownerDropDownListElement.IsDisposing) && (this.ownerDropDownListElement.ElementTree != null && this.ownerDropDownListElement.ElementTree.Control != null && (this.ownerDropDownListElement.ListElement != null && this.ownerDropDownListElement.ListElement.ElementTree != null)) && (this.ownerDropDownListElement.ListElement.ElementTree.Control != null && this.ownerDropDownListElement.ArrowButton != null))
      {
        Point client1 = this.ownerDropDownListElement.ElementTree.Control.PointToClient(Control.MousePosition);
        Point client2 = this.ownerDropDownListElement.ListElement.ElementTree.Control.PointToClient(Control.MousePosition);
        Rectangle boundingRectangle = this.ownerDropDownListElement.ArrowButton.ControlBoundingRectangle;
        if (this.ownerDropDownListElement.DropDownStyle == RadDropDownStyle.DropDownList && (this.ownerDropDownListElement.ContainsMouse || this.ownerDropDownListElement.Bounds.Contains(client1)))
          return false;
        if (mouseButtons != MouseButtons.Left)
          return this.ownerDropDownListElement.CanClosePopUp(reason, mouseButtons);
        if (this.ownerDropDownListElement.ListElement.VScrollBar != null && (this.ownerDropDownListElement.ListElement.VScrollBar.ThumbElement.Capture || (bool) this.ownerDropDownListElement.ListElement.VScrollBar.SecondButton.GetValue(RadButtonItem.IsPressedProperty) || (bool) this.ownerDropDownListElement.ListElement.VScrollBar.FirstButton.GetValue(RadButtonItem.IsPressedProperty)) || boundingRectangle.Contains(client1) || this.ownerDropDownListElement.ListElement.HScrollBar != null && this.ownerDropDownListElement.ListElement.HScrollBar.ControlBoundingRectangle.Contains(client2) || (this.ownerDropDownListElement.ListElement.VScrollBar != null && this.ownerDropDownListElement.ListElement.VScrollBar.ControlBoundingRectangle.Contains(client2) || this.SizingGrip != null && this.SizingGrip.ControlBoundingRectangle.Contains(client2)) || !this.ownerDropDownListElement.CanClosePopUp(reason, mouseButtons))
          return false;
      }
      return true;
    }

    public RadDropDownListElement OwnerDropDownListElement
    {
      get
      {
        return this.ownerDropDownListElement;
      }
    }

    private RadListDataItem GetSelectedOrHoveredItem()
    {
      RadListDataItem radListDataItem = (RadListDataItem) null;
      foreach (RadListVisualItem child in this.ownerDropDownListElement.ListElement.ViewElement.Children)
      {
        if (child.Selected && child.Data.RowIndex != this.ownerDropDownListElement.selectedIndexOnPopupOpen)
          return child.Data;
        if (child.IsMouseOver)
          radListDataItem = child.Data;
      }
      return radListDataItem;
    }

    private RadDropDownListElement GetMainDropDownListElement()
    {
      if (!this.ownerDropDownListElement.isSuggestMode)
        return this.ownerDropDownListElement;
      return this.ownerDropDownListElement.Parent as RadDropDownListElement ?? this.ownerDropDownListElement;
    }
  }
}
