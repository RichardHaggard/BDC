// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImeHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal static class ImeHelper
  {
    [ThreadStatic]
    private static bool threadMgrFailed = false;
    [ThreadStatic]
    private static IMENativeWrapper.ITfThreadMgr threadMgr;

    public static string GetImmCompositionString(IntPtr immContext, int dwIndex)
    {
      int compositionStringW1 = IMENativeWrapper.ImmGetCompositionStringW(immContext, dwIndex, (StringBuilder) null, 0);
      if (compositionStringW1 < 0)
        return (string) null;
      StringBuilder lpBuf = new StringBuilder(compositionStringW1 / 2);
      int compositionStringW2 = IMENativeWrapper.ImmGetCompositionStringW(immContext, dwIndex, lpBuf, compositionStringW1);
      if (compositionStringW2 < 0)
        return (string) null;
      return lpBuf.ToString().Substring(0, compositionStringW2 / 2);
    }

    public static IntPtr GetContext(IntPtr source)
    {
      return IMENativeWrapper.ImmGetContext(source);
    }

    public static IntPtr AssociateContext(NativeWindow source, IntPtr hIMC)
    {
      return IMENativeWrapper.ImmAssociateContext(source.Handle, hIMC);
    }

    public static bool ReleaseContext(IntPtr source, IntPtr hIMC)
    {
      if (hIMC != IntPtr.Zero)
        return IMENativeWrapper.ImmReleaseContext(source, hIMC);
      return false;
    }

    public static bool NotifyCompositionStringCancel(IntPtr hIMC)
    {
      return IMENativeWrapper.ImmNotifyIME(hIMC, 21, 4, 0);
    }

    public static void EnableImmComposition()
    {
      if (ImeHelper.threadMgrFailed)
        return;
      if (ImeHelper.threadMgr == null)
      {
        IMENativeWrapper.TF_CreateThreadMgr(out ImeHelper.threadMgr);
        if (ImeHelper.threadMgr == null)
        {
          ImeHelper.threadMgrFailed = true;
          return;
        }
      }
      ImeHelper.threadMgr.SetFocus(IntPtr.Zero);
    }

    public static bool SetCompositionWindow(
      IntPtr hIMC,
      RadTextBoxControlElement caretTextBox,
      Point point)
    {
      ImeHelper.SetCompositionFont(caretTextBox, hIMC);
      return IMENativeWrapper.ImmSetCompositionWindow(hIMC, ref new IMENativeWrapper.CompositionForm() { dwStyle = 32, ptCurrentPos = { x = Math.Max(0, point.X), y = Math.Max(0, point.Y) } });
    }

    public static IntPtr GetDefaultIMEWnd()
    {
      return IMENativeWrapper.ImmGetDefaultIMEWnd(IntPtr.Zero);
    }

    private static void SetCompositionFont(RadTextBoxControlElement caretTextBox, IntPtr hIMC)
    {
      double height = (double) caretTextBox.Font.Height;
      IMENativeWrapper.LOGFONT logfont = new IMENativeWrapper.LOGFONT() { lfHeight = (int) height, lfFaceName = caretTextBox.Font.FontFamily.Name };
      IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf((object) logfont));
      try
      {
        Marshal.StructureToPtr((object) logfont, num, true);
        IMENativeWrapper.ImmSetCompositionFontW(hIMC, num);
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }
  }
}
