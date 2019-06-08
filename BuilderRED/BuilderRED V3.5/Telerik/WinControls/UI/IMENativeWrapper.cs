// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IMENativeWrapper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Telerik.WinControls.UI
{
  public static class IMENativeWrapper
  {
    public const int WM_IME_COMPOSITION = 271;
    public const int WM_IME_STARTCOMPOSITION = 269;
    public const int WM_IME_ENDCOMPOSITION = 270;
    public const int WM_IME_CHAR = 646;
    public const int NI_COMPOSITIONSTR = 21;
    public const int GCS_RESULTSTR = 2048;
    public const int GCS_COMPSTR = 8;
    public const int CPS_CANCEL = 4;

    [DllImport("imm32.dll")]
    public static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

    [DllImport("imm32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

    [DllImport("imm32.dll")]
    public static extern IntPtr ImmGetContext(IntPtr hWnd);

    [DllImport("imm32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ImmSetCompositionWindow(
      IntPtr hIMC,
      ref IMENativeWrapper.CompositionForm form);

    [DllImport("imm32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ImmGetCompositionWindow(
      IntPtr hIMC,
      ref IMENativeWrapper.CompositionForm form);

    [DllImport("imm32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ImmNotifyIME(IntPtr hIMC, int dwAction, int dwIndex, int dwValue = 0);

    [DllImport("imm32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ImmSetCompositionFontW(IntPtr hIMC, IntPtr lplf);

    [DllImport("imm32.dll")]
    public static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);

    [DllImport("msctf.dll")]
    public static extern int TF_CreateThreadMgr(out IMENativeWrapper.ITfThreadMgr threadMgr);

    [DllImport("imm32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.I4)]
    public static extern int ImmGetCompositionStringW(
      IntPtr hIMC,
      int dwIndex,
      StringBuilder lpBuf,
      int dwBufLen);

    public struct CompositionForm
    {
      public int dwStyle;
      public IMENativeWrapper.POINT ptCurrentPos;
      public IMENativeWrapper.RECT rcArea;
    }

    public struct POINT
    {
      public int x;
      public int y;
    }

    public struct RECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class LOGFONT
    {
      public int lfHeight;
      public int lfWidth;
      public int lfEscapement;
      public int lfOrientation;
      public int lfWeight;
      public byte lfItalic;
      public byte lfUnderline;
      public byte lfStrikeOut;
      public byte lfCharSet;
      public byte lfOutPrecision;
      public byte lfClipPrecision;
      public byte lfQuality;
      public byte lfPitchAndFamily;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string lfFaceName;
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("aa80e801-2021-11d2-93e0-0060b067b86e")]
    [ComImport]
    public interface ITfThreadMgr
    {
      [SuppressUnmanagedCodeSecurity]
      [SecurityCritical]
      void Activate(out int clientId);

      [SecurityCritical]
      [SuppressUnmanagedCodeSecurity]
      void Deactivate();

      [SuppressUnmanagedCodeSecurity]
      [SecurityCritical]
      void CreateDocumentMgr(out object docMgr);

      void EnumDocumentMgrs(out object enumDocMgrs);

      void GetFocus(out IntPtr docMgr);

      [SuppressUnmanagedCodeSecurity]
      [SecurityCritical]
      void SetFocus(IntPtr docMgr);

      void AssociateFocus(IntPtr hwnd, object newDocMgr, out object prevDocMgr);

      void IsThreadFocus([MarshalAs(UnmanagedType.Bool)] out bool isFocus);

      [SuppressUnmanagedCodeSecurity]
      [SecurityCritical]
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetFunctionProvider(ref Guid classId, out object funcProvider);

      void EnumFunctionProviders(out object enumProviders);

      [SuppressUnmanagedCodeSecurity]
      [SecurityCritical]
      void GetGlobalCompartment(out object compartmentMgr);
    }
  }
}
