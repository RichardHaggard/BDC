// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ControlHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Telerik.WinControls
{
  public static class ControlHelper
  {
    public static bool GetAnyDisposingInHierarchy(Control child)
    {
      for (Control control = child; control != null; control = control.Parent)
      {
        if (control.IsDisposed || control.Disposing)
          return true;
      }
      return false;
    }

    public static Control GetControlUnderMouse()
    {
      Point mousePosition = Control.MousePosition;
      IntPtr handle = NativeMethods.WindowFromPoint(mousePosition.X, mousePosition.Y);
      if (handle == IntPtr.Zero)
        return (Control) null;
      return Control.FromChildHandle(handle);
    }

    public static void BringToFront(IntPtr handle, bool activate)
    {
      ControlHelper.UpdateZOrder(new HandleRef((object) null, handle), NativeMethods.HWND_TOP, activate);
    }

    public static void SendToBack(IntPtr handle, bool activate)
    {
      ControlHelper.UpdateZOrder(new HandleRef((object) null, handle), NativeMethods.HWND_BOTTOM, activate);
    }

    public static void InvalidateNonClient(Control frame, bool activate)
    {
      if (!frame.IsHandleCreated || frame.RecreatingHandle)
        return;
      int flags = 39;
      if (!activate)
        flags |= 16;
      NativeMethods.SetWindowPos(new HandleRef((object) null, frame.Handle), new HandleRef((object) null, IntPtr.Zero), 0, 0, 0, 0, flags);
    }

    public static bool IsDescendant(Control parent, Control child)
    {
      for (Control parent1 = child.Parent; parent1 != null; parent1 = parent1.Parent)
      {
        if (parent1 == parent)
          return true;
      }
      return false;
    }

    public static Control GetFocusedControl()
    {
      IntPtr focus = NativeMethods.GetFocus();
      if (focus == IntPtr.Zero)
        return (Control) null;
      return Control.FromChildHandle(focus);
    }

    public static bool GetControlStyle(Control instance, ControlStyles style)
    {
      MethodInfo method = typeof (Control).GetMethod("GetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
      if ((object) method == null || (object) method.ReturnType != (object) typeof (bool))
        return false;
      return (bool) method.Invoke((object) instance, new object[1]
      {
        (object) style
      });
    }

    public static void BeginUpdate(Control control)
    {
      if (control == null || !control.IsHandleCreated || !control.Visible)
        return;
      NativeMethods.SendMessage(new HandleRef((object) null, control.Handle), 11, (IntPtr) 0, (IntPtr) 0);
    }

    public static void EndUpdate(Control control, bool invalidate)
    {
      if (control == null || !control.IsHandleCreated || !control.Visible)
        return;
      NativeMethods.SendMessage(new HandleRef((object) null, control.Handle), 11, (IntPtr) 1, (IntPtr) 0);
      if (!invalidate)
        return;
      control.Invalidate(true);
    }

    public static List<Control> FilterChildControls(
      Control parent,
      Filter filter,
      bool recursive)
    {
      List<Control> controlList = new List<Control>();
      foreach (Control enumChildControl in ControlHelper.EnumChildControls(parent, recursive))
      {
        if (filter.Match((object) enumChildControl))
          controlList.Add(enumChildControl);
      }
      return controlList;
    }

    public static List<T> GetChildControls<T>(Control parent) where T : Control
    {
      return ControlHelper.GetChildControls<T>(parent, false);
    }

    public static T GetNextControl<T>(
      Control parent,
      T curr,
      bool recursive,
      bool forward,
      bool wrap)
      where T : Control
    {
      if (parent == null || (object) curr == null)
        return default (T);
      List<T> childControls = ControlHelper.GetChildControls<T>(parent, recursive);
      int num = childControls.IndexOf(curr);
      if (num < 0)
        return default (T);
      int count = childControls.Count;
      int index;
      if (forward)
      {
        index = num + 1;
        if (index > count - 1)
        {
          if (!wrap)
            return default (T);
          index = 0;
        }
      }
      else
      {
        index = num - 1;
        if (index < 0)
        {
          if (!wrap)
            return default (T);
          index = count - 1;
        }
      }
      return childControls[index];
    }

    public static T GetFirstControl<T>(Control parent, bool recursive) where T : Control
    {
      foreach (Control enumChildControl in ControlHelper.EnumChildControls(parent, recursive))
      {
        if (enumChildControl is T)
          return enumChildControl as T;
      }
      return default (T);
    }

    public static T GetLastControl<T>(Control parent, bool recursive) where T : Control
    {
      T obj = default (T);
      foreach (Control enumChildControl in ControlHelper.EnumChildControls(parent, recursive))
      {
        if (enumChildControl is T)
          obj = enumChildControl as T;
      }
      return obj;
    }

    public static List<T> GetChildControls<T>(Control parent, bool recursive) where T : Control
    {
      List<T> objList = new List<T>();
      foreach (Control enumChildControl in ControlHelper.EnumChildControls(parent, recursive))
      {
        if (enumChildControl is T)
          objList.Add((T) enumChildControl);
      }
      return objList;
    }

    public static IEnumerable<Control> EnumChildControls(
      Control parent,
      bool recursive)
    {
      IEnumerator enumerator = parent.Controls.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Control c = (Control) enumerator.Current;
          yield return c;
          if (recursive)
          {
            foreach (Control enumChildControl in ControlHelper.EnumChildControls(c, recursive))
              yield return enumChildControl;
          }
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        disposable?.Dispose();
      }
    }

    public static IEnumerable<Control> EnumChildControls(
      Control parent,
      bool recursive,
      Predicate<Control> parentFilter)
    {
      IEnumerator enumerator = parent.Controls.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Control c = (Control) enumerator.Current;
          if (parentFilter(c))
          {
            yield return c;
            if (recursive)
            {
              foreach (Control enumChildControl in ControlHelper.EnumChildControls(c, recursive, parentFilter))
                yield return enumChildControl;
            }
          }
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        disposable?.Dispose();
      }
    }

    public static T FindAncestor<T>(Control child) where T : Control
    {
      if (child == null)
        return default (T);
      for (Control parent = child.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is T)
          return (T) parent;
      }
      return default (T);
    }

    public static Control FindDescendant(Control parent, System.Type descendantType)
    {
      foreach (Control enumChildControl in ControlHelper.EnumChildControls(parent, true))
      {
        if ((object) enumChildControl.GetType() == (object) descendantType)
          return enumChildControl;
      }
      return (Control) null;
    }

    public static T FindDescendant<T>(Control parent) where T : Control
    {
      if (parent == null)
        return default (T);
      Queue<Control> controlQueue = new Queue<Control>();
      controlQueue.Enqueue(parent);
      while (controlQueue.Count > 0)
      {
        foreach (Control control in (ArrangedElementCollection) controlQueue.Dequeue().Controls)
        {
          if (control is T)
            return (T) control;
          controlQueue.Enqueue(control);
        }
      }
      return default (T);
    }

    private static void UpdateZOrder(HandleRef handle, HandleRef pos, bool activate)
    {
      int flags = 1539;
      if (!activate)
        flags |= 16;
      NativeMethods.SetWindowPos(handle, pos, 0, 0, 0, 0, flags);
    }
  }
}
