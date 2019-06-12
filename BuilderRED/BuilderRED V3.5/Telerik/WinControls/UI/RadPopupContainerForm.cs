// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPopupContainerForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadPopupContainerForm : RadEditorPopupControlBase
  {
    private const int DownKeyState = 32768;
    private const int ToggleKeyState = 1;
    private Panel panel;

    public RadPopupContainerForm(RadItem owner)
      : base(owner)
    {
      this.SetStyle(ControlStyles.Selectable, true);
    }

    public virtual Panel Panel
    {
      get
      {
        return this.panel;
      }
      set
      {
        this.panel = value;
      }
    }

    public override bool OnMouseWheel(Control target, int delta)
    {
      base.OnMouseWheel(target, delta);
      return true;
    }

    public override bool OnKeyDown(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Tab:
          if (this.panel != null)
            this.SetFocusCore();
          return true;
        case Keys.Escape:
          this.ClosePopup(RadPopupCloseReason.Keyboard);
          return true;
        default:
          return false;
      }
    }

    protected virtual void SetFocusCore()
    {
      bool forward = !RadPopupContainerForm.IsKeyDown(Keys.LShiftKey) && !RadPopupContainerForm.IsKeyDown(Keys.RShiftKey);
      Control ctl = this.GetFocusedControl();
      do
      {
        if (!forward && ctl is System.Windows.Forms.TextBox && ctl.Parent is RadControl)
          ctl = ctl.Parent;
        ctl = this.panel.GetNextControl(ctl, forward);
      }
      while (ctl != null && !ctl.CanSelect);
      if (ctl == null || !ctl.CanFocus)
        return;
      if (ctl is RadTextBox)
        ctl = ((RadTextBoxBase) ctl).TextBoxItem.HostedControl;
      ctl.Focus();
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr GetFocus();

    private Control GetFocusedControl()
    {
      Control control = (Control) null;
      IntPtr focus = RadPopupContainerForm.GetFocus();
      if (focus != IntPtr.Zero)
        control = Control.FromHandle(focus);
      return control;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern short GetKeyState(int keyCode);

    private static RadPopupContainerForm.KeyStates GetKeyState(Keys key)
    {
      RadPopupContainerForm.KeyStates keyStates = RadPopupContainerForm.KeyStates.None;
      short keyState = RadPopupContainerForm.GetKeyState((int) key);
      if (((int) keyState & 32768) == 32768)
        keyStates |= RadPopupContainerForm.KeyStates.Down;
      if (((int) keyState & 1) == 1)
        keyStates |= RadPopupContainerForm.KeyStates.Toggled;
      return keyStates;
    }

    public static bool IsKeyDown(Keys key)
    {
      return RadPopupContainerForm.KeyStates.Down == (RadPopupContainerForm.GetKeyState(key) & RadPopupContainerForm.KeyStates.Down);
    }

    [Flags]
    private enum KeyStates
    {
      None = 0,
      Down = 1,
      Toggled = 2,
    }
  }
}
