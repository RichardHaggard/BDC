// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadModalFilter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadModalFilter : IMessageFilter
  {
    private static RadModalFilter instance = (RadModalFilter) null;
    internal static bool Animating = false;
    private bool suspended;
    private bool menuAutoExpand;
    private WindowsHook hookMouse;
    internal List<RadDropDownMenu> DropDowns;
    internal RadDropDownMenu ActiveDropDown;
    public static bool InDesignMode;
    public bool DisableAutoClose;

    protected RadModalFilter()
    {
      this.DropDowns = new List<RadDropDownMenu>();
    }

    public static RadModalFilter Instance
    {
      get
      {
        if (RadModalFilter.instance == null)
          RadModalFilter.instance = new RadModalFilter();
        return RadModalFilter.instance;
      }
    }

    public bool Suspended
    {
      get
      {
        return this.suspended;
      }
      set
      {
        if (this.suspended == value)
          return;
        this.suspended = value;
        RadDropDownMenu activeDropDown = this.ActiveDropDown;
      }
    }

    public bool MenuAutoExpand
    {
      get
      {
        return this.menuAutoExpand;
      }
      set
      {
        this.menuAutoExpand = value;
      }
    }

    public event EventHandler MenuHierarchyClosing;

    public void Register(RadDropDownMenu menu)
    {
      if (menu == this.ActiveDropDown || this.DropDowns.Contains(menu))
        return;
      this.Suspended = true;
      if (this.ActiveDropDown != null)
      {
        if (menu != null && menu.OwnerElement != null && menu.OwnerElement.ElementTree != null)
        {
          RadDropDownMenu control = menu.OwnerElement.ElementTree.Control as RadDropDownMenu;
          if (control != null)
          {
            int num = this.DropDowns.IndexOf(control);
            while (num < this.DropDowns.Count - 1)
              this.DropDowns[this.DropDowns.Count - 1].ClosePopup(RadPopupCloseReason.CloseCalled);
          }
        }
      }
      else if (!RadModalFilter.InDesignMode)
      {
        Application.AddMessageFilter((IMessageFilter) this);
      }
      else
      {
        this.hookMouse = new WindowsHook(WindowsHook.HookType.WH_MOUSE_LL);
        this.hookMouse.HookInvoked += new WindowsHook.HookEventHandler(this.hookMouse_HookInvoked);
        this.hookMouse.Install();
      }
      this.DropDowns.Add(menu);
      this.ActiveDropDown = menu;
      this.Suspended = false;
    }

    public void UnRegister(RadDropDownMenu menu)
    {
      if (!this.DropDowns.Contains(menu))
        return;
      this.Suspended = true;
      if (this.DropDowns.Count > 0)
      {
        if (this.DropDowns[0].OwnerElement != null && this.DropDowns[0].OwnerElement.ElementState == ElementState.Loaded)
        {
          Control control = this.DropDowns[0].OwnerElement.ElementTree.Control;
        }
        this.DropDowns.Remove(menu);
      }
      if (this.DropDowns.Count > 0)
        this.ActiveDropDown = this.DropDowns[this.DropDowns.Count - 1];
      else
        this.Cleanup();
      this.Suspended = false;
    }

    public void UnRegisterActiveMenu()
    {
      this.UnRegister(this.ActiveDropDown);
    }

    public void UnRegisterAllMenus()
    {
      if (this.DropDowns.Count <= 0)
        return;
      this.Suspended = true;
      if (this.DropDowns[0].OwnerElement == null)
        this.MenuAutoExpand = false;
      while (this.DropDowns.Count > 0)
      {
        if (this.DropDowns[this.DropDowns.Count - 1].Visible)
        {
          if (this.DropDowns[this.DropDowns.Count - 1].OwnerElement is RadDropDownButtonElement && this.DropDowns[this.DropDowns.Count - 1].OwnerElement.ElementTree.Control is RadDropDownMenu)
          {
            this.DropDowns[this.DropDowns.Count - 1].ClosePopup(RadPopupCloseReason.CloseCalled);
            return;
          }
          this.DropDowns[this.DropDowns.Count - 1].ClosePopup(RadPopupCloseReason.CloseCalled);
        }
        else
          this.DropDowns.RemoveAt(this.DropDowns.Count - 1);
      }
      this.Cleanup();
      this.Suspended = false;
    }

    public void SetActiveDropDown(RadDropDownMenu menu)
    {
      int num = this.DropDowns.IndexOf(menu);
      if (num < 0 || menu == this.ActiveDropDown)
        return;
      for (int index = this.DropDowns.Count - 1; index > num; --index)
        this.DropDowns[index].ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    public bool PreFilterMessage(ref Message m)
    {
      if (RadModalFilter.Animating || this.ActiveDropDown == null)
        return false;
      if (!this.ActiveDropDown.Visible)
        this.UnRegisterAllMenus();
      if (!RadModalFilter.InDesignMode)
      {
        switch (m.Msg)
        {
          case 161:
          case 164:
          case 167:
          case 513:
          case 516:
          case 519:
            if (!this.Suspended)
              return this.OnMouseDown(ref m);
            if (!this.ActiveDropDown.ClientRectangle.Contains(this.ActiveDropDown.PointToClient(Control.MousePosition)))
            {
              if (this.MenuHierarchyClosing != null)
                this.MenuHierarchyClosing((object) this, EventArgs.Empty);
              if (!this.DisableAutoClose)
              {
                this.UnRegisterAllMenus();
                return true;
              }
            }
            return false;
        }
      }
      return false;
    }

    private bool OnMouseDown(ref Message m)
    {
      if (this.ActiveDropDown == null || this.ActiveDropDown.IsDisposed || this.ActiveDropDown.ClientRectangle.Contains(this.ActiveDropDown.PointToClient(Control.MousePosition)))
        return false;
      for (int index1 = this.DropDowns.Count - 2; index1 >= 0; --index1)
      {
        Point client = this.DropDowns[index1].PointToClient(Control.MousePosition);
        if (this.DropDowns[index1].ClientRectangle.Contains(client))
        {
          if (this.ActiveDropDown.OwnerElement != null)
          {
            RadElement elementAtPoint = this.ActiveDropDown.OwnerElement.ElementTree.ComponentTreeHandler.ElementTree.GetElementAtPoint(client);
            if (this.DropDowns[index1] == this.ActiveDropDown.OwnerElement.ElementTree.Control && elementAtPoint == this.ActiveDropDown.OwnerElement)
              return true;
          }
          for (int index2 = this.DropDowns.Count - 1; index2 > index1; --index2)
            this.ActiveDropDown.ClosePopup(RadPopupCloseReason.Mouse);
          return false;
        }
      }
      this.UnRegisterAllMenus();
      return false;
    }

    private bool hookMouse_HookInvoked(object sender, HookEventArgs e)
    {
      if (e.wParam == (IntPtr) 513 || e.wParam == (IntPtr) 519 || (e.wParam == (IntPtr) 516 || e.wParam == (IntPtr) 161) || (e.wParam == (IntPtr) 167 || e.wParam == (IntPtr) 164))
      {
        WindowsHook.MSLLHOOKSTRUCT structure = (WindowsHook.MSLLHOOKSTRUCT) Marshal.PtrToStructure(e.lParam, typeof (WindowsHook.MSLLHOOKSTRUCT));
        Point p = new Point(structure.pt.x, structure.pt.y);
        try
        {
          if (!this.ActiveDropDown.ClientRectangle.Contains(this.ActiveDropDown.PointToClient(p)))
          {
            for (int index1 = this.DropDowns.Count - 2; index1 >= 0; --index1)
            {
              Point client = this.DropDowns[index1].PointToClient(p);
              if (this.DropDowns[index1].ClientRectangle.Contains(client))
              {
                for (int index2 = this.DropDowns.Count - 1; index2 > index1; --index2)
                  this.DropDowns[index2].ClosePopup(RadPopupCloseReason.Mouse);
                return true;
              }
            }
            this.UnRegisterAllMenus();
          }
        }
        catch
        {
          this.UnRegisterAllMenus();
        }
      }
      return true;
    }

    private void Cleanup()
    {
      this.DropDowns.Clear();
      if (this.ActiveDropDown != null && this.ActiveDropDown.activeHwnd != IntPtr.Zero)
        Telerik.WinControls.NativeMethods.SetActiveWindow(new HandleRef((object) null, this.ActiveDropDown.activeHwnd));
      this.ActiveDropDown = (RadDropDownMenu) null;
      this.MenuAutoExpand = false;
      if (!RadModalFilter.InDesignMode)
        Application.RemoveMessageFilter((IMessageFilter) this);
      else
        this.hookMouse.Uninstall();
    }

    private bool ActivateNextRootMenuItem(Keys key, bool force)
    {
      return false;
    }
  }
}
