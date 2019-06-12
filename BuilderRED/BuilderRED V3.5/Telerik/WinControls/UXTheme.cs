// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UXTheme
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms.VisualStyles;

namespace Telerik.WinControls
{
  [SuppressUnmanagedCodeSecurity]
  internal class UXTheme
  {
    private const string NativeDll = "uxtheme.dll";

    [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr OpenThemeData(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszClassList);

    [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
    public static extern int CloseThemeData(IntPtr hTheme);

    [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
    public static extern int SetWindowTheme(IntPtr handle, string windowName, string ids);

    [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
    public static extern int DrawThemeBackground(
      IntPtr hTheme,
      IntPtr hDC,
      int partId,
      int stateId,
      ref NativeMethods.RECT rect,
      IntPtr clip);

    [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
    public static extern bool IsThemePartDefined(IntPtr hTheme, int iPartId, int iStateId);

    [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
    public static extern int GetThemeColor(
      IntPtr hTheme,
      int iPartId,
      int iStateId,
      int iPropId,
      ref int pColor);

    [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
    public static extern int GetThemePartSize(
      IntPtr hTheme,
      IntPtr hdc,
      int iPartId,
      int iStateId,
      IntPtr rect,
      ThemeSizeType eSize,
      [Out] NativeMethods.SIZE psz);

    [DllImport("uxtheme.dll")]
    public static extern IntPtr BeginBufferedPaint(
      IntPtr pHdcTarget,
      IntPtr lpRect,
      IntPtr bufferFormat,
      IntPtr bpPaintParams,
      ref IntPtr pHdc);

    [DllImport("uxtheme.dll")]
    public static extern IntPtr BufferedPaintSetAlpha(
      IntPtr hBufferedPaint,
      IntPtr prc,
      byte alpha);

    [DllImport("uxtheme.dll")]
    public static extern IntPtr EndBufferedPaint(IntPtr hBufferedPaint, IntPtr fUpdateTarget);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    public static extern int DrawThemeTextEx(
      IntPtr hTheme,
      IntPtr hdc,
      int iPartId,
      int iStateId,
      string text,
      int iCharCount,
      int dwFlags,
      ref NativeMethods.RECT pRect,
      ref DWMAPI.DTTOPTS pOptions);
  }
}
