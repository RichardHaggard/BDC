// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TelerikPaintHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class TelerikPaintHelper
  {
    public static void CopyImageFromGraphics(Graphics graphics, Bitmap destinationImage)
    {
      Graphics graphics1 = Graphics.FromImage((Image) destinationImage);
      IntPtr hdc1 = graphics.GetHdc();
      IntPtr hdc2 = graphics1.GetHdc();
      NativeMethods.BitBlt(hdc2, 0, 0, destinationImage.Width, destinationImage.Height, hdc1, 0, 0, 13369376);
      graphics1.ReleaseHdc(hdc2);
      graphics.ReleaseHdc(hdc1);
    }

    public static IntPtr CreateHalftoneBrush()
    {
      short[] lpvBits = new short[8];
      for (int index = 0; index < 8; ++index)
        lpvBits[index] = (short) (21845 << (index & 1));
      IntPtr bitmap = NativeMethods.CreateBitmap(8, 8, 1, 1, lpvBits);
      IntPtr brushIndirect = NativeMethods.CreateBrushIndirect(new NativeMethods.LOGBRUSH()
      {
        lbColor = ColorTranslator.ToWin32(Color.Black),
        lbStyle = 3,
        lbHatch = bitmap
      });
      NativeMethods.DeleteObject(new HandleRef((object) null, bitmap));
      return brushIndirect;
    }

    [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
    public static void DrawHalftoneLine(Rectangle rectangle1)
    {
      TelerikPaintHelper.DrawHalftoneLine(NativeMethods.GetDesktopWindow(), rectangle1, (Control) null);
    }

    public static void DrawHalftoneLine(Control canvasControl, Rectangle rectangle1)
    {
      TelerikPaintHelper.DrawHalftoneLine(canvasControl.Handle, rectangle1, canvasControl);
    }

    [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
    public static void DrawHalftoneLine(
      IntPtr windowHandle,
      Rectangle rectangle1,
      Control managedBase)
    {
      IntPtr dcEx = NativeMethods.GetDCEx(new HandleRef((object) managedBase, windowHandle), NativeMethods.NullHandleRef, 1026);
      IntPtr halftoneBrush = TelerikPaintHelper.CreateHalftoneBrush();
      IntPtr handle = NativeMethods.SelectObject(new HandleRef((object) managedBase, dcEx), new HandleRef((object) null, halftoneBrush));
      NativeMethods.PatBlt(new HandleRef((object) managedBase, dcEx), rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height, 5898313);
      NativeMethods.SelectObject(new HandleRef((object) managedBase, dcEx), new HandleRef((object) null, handle));
      NativeMethods.DeleteObject(new HandleRef((object) null, halftoneBrush));
      NativeMethods.ReleaseDC(new HandleRef((object) managedBase, windowHandle), new HandleRef((object) null, dcEx));
    }

    public static void DrawGlowingText(
      Graphics graphics,
      string text,
      Font font,
      Rectangle bounds,
      Color color,
      TextFormatFlags flags)
    {
      if (!Application.RenderWithVisualStyles)
        return;
      DWMAPI.DrawTextOnGlass(graphics, text, font, bounds, color, flags, 2048);
    }

    public static bool IsCompositionEnabled()
    {
      return DWMAPI.IsCompositionEnabled;
    }
  }
}
