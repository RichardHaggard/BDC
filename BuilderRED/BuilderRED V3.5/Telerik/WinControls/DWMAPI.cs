// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.DWMAPI
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Telerik.WinControls
{
  [SuppressUnmanagedCodeSecurity]
  internal class DWMAPI
  {
    public const int DTT_COMPOSITED = 8192;
    public const int DTT_GLOWSIZE = 2048;
    public const int DTT_TEXTCOLOR = 1;
    public const int BPBF_TOPDOWNDIB = 2;

    [DllImport("dwmapi.dll")]
    public static extern void DwmIsCompositionEnabled(ref bool isEnabled);

    [DllImport("dwmapi.dll")]
    public static extern void DwmExtendFrameIntoClientArea(
      IntPtr hWnd,
      ref NativeMethods.MARGINS pMargins);

    [DllImport("dwmapi.dll")]
    public static extern int DwmDefWindowProc(
      IntPtr hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam,
      out IntPtr result);

    [DllImport("dwmapi.dll")]
    public static extern bool DwmEnableBlurBehindWindow(
      IntPtr hWnd,
      ref DWMAPI.DWMBLURBEHIND blurInfo);

    public static void DrawTextOnGlass(
      Graphics graphics,
      string text,
      Font font,
      Rectangle bounds,
      Color color,
      TextFormatFlags flags,
      int rflags)
    {
      IntPtr hdc = graphics.GetHdc();
      IntPtr compatibleDc = NativeMethods.CreateCompatibleDC(new HandleRef((object) null, hdc));
      NativeMethods.BITMAPINFO pbmi = new NativeMethods.BITMAPINFO();
      pbmi.bmiHeader_biSize = Marshal.SizeOf((object) pbmi);
      pbmi.bmiHeader_biWidth = bounds.Width;
      pbmi.bmiHeader_biHeight = -bounds.Height;
      pbmi.bmiHeader_biPlanes = (short) 1;
      pbmi.bmiHeader_biBitCount = (short) 32;
      pbmi.bmiHeader_biCompression = 0;
      IntPtr ppvBits = IntPtr.Zero;
      IntPtr dibSection = NativeMethods.CreateDIBSection(hdc, pbmi, 0U, out ppvBits, IntPtr.Zero, 0U);
      NativeMethods.SelectObject(new HandleRef((object) null, compatibleDc), new HandleRef((object) null, dibSection));
      IntPtr hfont = font.ToHfont();
      NativeMethods.SelectObject(new HandleRef((object) null, compatibleDc), new HandleRef((object) null, hfont));
      VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
      DWMAPI.DTTOPTS pOptions = new DWMAPI.DTTOPTS();
      pOptions.dwSize = Marshal.SizeOf(typeof (DWMAPI.DTTOPTS));
      pOptions.dwFlags = 8192 | rflags | 1;
      pOptions.crText = ColorTranslator.ToWin32(color);
      pOptions.iGlowSize = 10;
      NativeMethods.RECT pRect = new NativeMethods.RECT(0, 0, bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
      UXTheme.DrawThemeTextEx(visualStyleRenderer.Handle, compatibleDc, 0, 0, text, -1, (int) flags, ref pRect, ref pOptions);
      NativeMethods.BitBlt(hdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, compatibleDc, 0, 0, 13369376);
      NativeMethods.DeleteObject(new HandleRef((object) null, hfont));
      NativeMethods.DeleteObject(new HandleRef((object) null, dibSection));
      NativeMethods.DeleteDC(new HandleRef((object) null, compatibleDc));
      graphics.ReleaseHdc(hdc);
    }

    public static bool IsVista
    {
      get
      {
        return Environment.OSVersion.Version.Major >= 6;
      }
    }

    public static bool IsCompositionEnabled
    {
      get
      {
        if (!DWMAPI.IsVista)
          return false;
        bool isEnabled = false;
        DWMAPI.DwmIsCompositionEnabled(ref isEnabled);
        return isEnabled;
      }
    }

    public static bool IsAlphaBlendingSupported
    {
      get
      {
        return Environment.OSVersion.Version.Major >= 5 && Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]).ToLower() != "msaccess";
      }
    }

    public struct DTTOPTS
    {
      public int dwSize;
      public int dwFlags;
      public int crText;
      public int crBorder;
      public int crShadow;
      public int iTextShadowType;
      public NativeMethods.POINT ptShadowOffset;
      public int iBorderSize;
      public int iFontPropId;
      public int iColorPropId;
      public int iStateId;
      public bool fApplyOverlay;
      public int iGlowSize;
      public int pfnDrawTextCallback;
      public IntPtr lParam;
    }

    public struct DWMBLURBEHIND
    {
      public int dwFlags;
      public bool fEnable;
      public IntPtr hRgnBlur;
      public bool fTransitionOnMaximized;
    }
  }
}
