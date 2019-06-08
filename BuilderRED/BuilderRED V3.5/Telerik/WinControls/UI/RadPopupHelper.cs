// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPopupHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadPopupHelper
  {
    private static Rectangle GetScreenBounds(Screen screen, bool excludeTaskBar)
    {
      return screen.WorkingArea;
    }

    private static Rectangle GetScreenBounds(Screen screen)
    {
      return screen.Bounds;
    }

    public static Point GetValidLocationForContextMenu(
      RadDirection popupDirection,
      Point location,
      Size popupSize,
      ref bool corected)
    {
      Rectangle screenRect = RadPopupHelper.GetScreenRect(location);
      Rectangle rectangle = new Rectangle(location, popupSize);
      Point point = location;
      switch (popupDirection)
      {
        case RadDirection.Left:
          location = new Point(rectangle.Left - popupSize.Width, rectangle.Top);
          break;
        case RadDirection.Up:
          location = new Point(rectangle.Left, rectangle.Top - popupSize.Height);
          break;
      }
      if (RadPopupHelper.GetCorrectingDirection(rectangle, screenRect, ref popupDirection))
      {
        switch (popupDirection)
        {
          case RadDirection.Left:
            location = new Point(rectangle.Left - popupSize.Width, rectangle.Top);
            break;
          case RadDirection.Right:
            location = new Point(rectangle.Right, rectangle.Top);
            break;
          case RadDirection.Up:
            location = new Point(rectangle.Left, rectangle.Top - popupSize.Height);
            break;
          case RadDirection.Down:
            location = new Point(rectangle.Left, rectangle.Bottom);
            break;
        }
        rectangle = new Rectangle(location, popupSize);
        if (RadPopupHelper.GetCorrectingDirection(rectangle, screenRect, ref popupDirection))
          location = point;
        corected = true;
      }
      rectangle.Location = location;
      rectangle = RadPopupHelper.EnsureBoundsInScreen(rectangle, screenRect);
      return rectangle.Location;
    }

    public static Point GetValidLocationForDropDown(
      RadDirection popupDirection,
      Size popupSize,
      RadElement owner,
      int ownerOffset,
      ref bool corected)
    {
      Rectangle screenRect = RadPopupHelper.GetScreenRect(owner);
      Rectangle rectangle = owner.ControlBoundingRectangle;
      if (owner.ElementTree != null)
        rectangle = owner.ElementTree.Control.RectangleToScreen(rectangle);
      return RadPopupHelper.GetValidLocationForDropDown(popupDirection, screenRect, popupSize, rectangle, ownerOffset, ref corected);
    }

    public static Point GetValidLocationForDropDown(
      RadDirection popupDirection,
      Rectangle screenRect,
      Size popupSize,
      Rectangle ownerRect,
      int ownerOffset,
      ref bool corected)
    {
      Point point = RadPopupHelper.CalcLocation(popupDirection, popupSize, ownerRect, ownerOffset);
      Point location = point;
      RadDirection correction = popupDirection;
      Rectangle rectangle = new Rectangle(location, popupSize);
      if (RadPopupHelper.GetCorrectingDirection(rectangle, screenRect, ref correction))
      {
        location = RadPopupHelper.CalcLocation(correction, popupSize, ownerRect, ownerOffset);
        rectangle = new Rectangle(location, popupSize);
        if (RadPopupHelper.GetCorrectingDirection(rectangle, screenRect, ref correction))
          location = point;
        corected = true;
      }
      rectangle.Location = location;
      return RadPopupHelper.EnsureBoundsInScreen(rectangle, screenRect).Location;
    }

    public static Screen GetScreen(Point pointInScreen)
    {
      Screen primaryScreen = Screen.PrimaryScreen;
      for (int index = 0; index < Screen.AllScreens.Length; ++index)
      {
        if (RadPopupHelper.GetScreenBounds(Screen.AllScreens[index]).Contains(pointInScreen))
          return Screen.AllScreens[index];
      }
      return primaryScreen;
    }

    public static Rectangle GetScreenRect(RadElement elementOnScreen)
    {
      Rectangle rectangle = RadPopupHelper.GetScreenBounds(Screen.PrimaryScreen);
      if (elementOnScreen != null && elementOnScreen.ElementTree != null && elementOnScreen.ElementTree != null)
      {
        Point point = new Point(elementOnScreen.Size.Width / 2, elementOnScreen.Size.Height / 2);
        Point control = elementOnScreen.PointToControl(point);
        rectangle = RadPopupHelper.GetScreenRect(elementOnScreen.ElementTree.Control.PointToScreen(control));
      }
      return rectangle;
    }

    public static Rectangle GetScreenRect(Point pointInScreen)
    {
      return RadPopupHelper.GetScreenBounds(RadPopupHelper.GetScreen(pointInScreen), true);
    }

    public static Rectangle EnsureBoundsInScreen(
      Rectangle popupBounds,
      Rectangle screenRect)
    {
      if (!screenRect.IsEmpty)
      {
        if (popupBounds.Right > screenRect.Right)
          popupBounds.X -= popupBounds.Right - screenRect.Right;
        if (popupBounds.Bottom > screenRect.Bottom)
          popupBounds.Y -= popupBounds.Bottom - screenRect.Bottom;
        if (popupBounds.X < screenRect.X)
          popupBounds.X = screenRect.X;
        if (popupBounds.Y < screenRect.Y)
          popupBounds.Y = screenRect.Y;
      }
      return popupBounds;
    }

    private static Point CalcLocation(
      RadDirection popupDirection,
      Size popupSize,
      Rectangle ownerRect,
      int ownerOffset)
    {
      Point point = new Point(0, 0);
      switch (popupDirection)
      {
        case RadDirection.Left:
          point = new Point(ownerRect.Left - ownerOffset - popupSize.Width, ownerRect.Top);
          break;
        case RadDirection.Right:
          point = new Point(ownerRect.Right + ownerOffset, ownerRect.Top);
          break;
        case RadDirection.Up:
          point = new Point(ownerRect.Left, ownerRect.Top - ownerOffset - popupSize.Height);
          break;
        case RadDirection.Down:
          point = new Point(ownerRect.Left, ownerRect.Bottom + ownerOffset);
          break;
      }
      return point;
    }

    private static bool GetCorrectingDirection(
      Rectangle popupRect,
      Rectangle screenRect,
      ref RadDirection correction)
    {
      bool flag = true;
      if (popupRect.Left < screenRect.Left && correction == RadDirection.Left)
        correction = RadDirection.Right;
      else if (popupRect.Top < screenRect.Top && correction == RadDirection.Up)
        correction = RadDirection.Down;
      else if (popupRect.Right > screenRect.Right && correction == RadDirection.Right)
        correction = RadDirection.Left;
      else if (popupRect.Bottom > screenRect.Bottom && correction == RadDirection.Down)
        correction = RadDirection.Up;
      else
        flag = false;
      return flag;
    }

    public static void SetTopMost(IntPtr handle)
    {
      RadPopupHelper.SetWindowPosition(handle, (IntPtr) Telerik.WinControls.NativeMethods.HWND_TOPMOST);
    }

    public static void SetWindowPosition(IntPtr handle, IntPtr parentForm)
    {
      Point point = Point.Empty;
      Control control = Control.FromHandle(handle);
      if (control != null)
        point = control.Location;
      Telerik.WinControls.NativeMethods.SetWindowPos(new HandleRef((object) null, handle), new HandleRef((object) null, parentForm), point.X, point.Y, 0, 0, 83);
    }

    public static bool SetVisibleCore(Control control)
    {
      RadPopupHelper.SetVisibleCore(control, (IntPtr) Telerik.WinControls.NativeMethods.HWND_TOPMOST);
      return true;
    }

    public static bool SetVisibleCore(Control control, IntPtr parentForm)
    {
      RadPopupHelper.SetWindowPosition(control.Handle, parentForm);
      Telerik.WinControls.NativeMethods.ShowWindow(control.Handle, 8);
      return true;
    }

    public static void ActivateForm(IntPtr form)
    {
      Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) null, form), 134, 1, 0);
    }
  }
}
