// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.NativeMethods
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [SuppressUnmanagedCodeSecurity]
  public static class NativeMethods
  {
    [ThreadStatic]
    private static Graphics measurementGraphics = (Graphics) null;
    public static HandleRef HWND_TOP = new HandleRef((object) null, IntPtr.Zero);
    public static HandleRef HWND_TOPMOST = new HandleRef((object) null, new IntPtr(-1));
    public static HandleRef HWND_BOTTOM = new HandleRef((object) null, (IntPtr) 1);
    public static HandleRef NullHandleRef = new HandleRef((object) null, IntPtr.Zero);
    private static int wmMouseEnterMessage = -1;
    public const int WM_KEYFIRST = 256;
    public const int WM_KEYLAST = 264;
    public const int WM_MOUSEFIRST = 512;
    public const int WM_MOUSELAST = 522;
    public const int WM_KEYDOWN = 256;
    public const int WM_KEYUP = 257;
    public const int WM_SYSKEYDOWN = 260;
    public const int WM_SYSKEYUP = 261;
    public const int WM_CHAR = 258;
    public const int WM_SYSCHAR = 262;
    public const int WM_MOUSEACTIVATE = 33;
    public const int WM_MOUSEMOVE = 512;
    public const int WM_ACTIVATE = 6;
    public const int WM_ACTIVATEAPP = 28;
    public const int WM_NCACTIVATE = 134;
    public const int WM_NCCALCSIZE = 131;
    public const int WM_NCCREATE = 129;
    public const int WM_NCDESTROY = 130;
    public const int WM_NCHITTEST = 132;
    public const int WM_NCLBUTTONDBLCLK = 163;
    public const int WM_NCLBUTTONDOWN = 161;
    public const int WM_NCLBUTTONUP = 162;
    public const int WM_NCMBUTTONDBLCLK = 169;
    public const int WM_NCMBUTTONDOWN = 167;
    public const int WM_NCMBUTTONUP = 168;
    public const int WM_NCMOUSELEAVE = 674;
    public const int WM_NCMOUSEMOVE = 160;
    public const int WM_NCPAINT = 133;
    public const int WM_NCRBUTTONDBLCLK = 166;
    public const int WM_NCRBUTTONDOWN = 164;
    public const int WM_NCRBUTTONUP = 165;
    public const int WM_LBUTTONDBLCLK = 515;
    public const int WM_LBUTTONDOWN = 513;
    public const int WM_LBUTTONUP = 514;
    public const int WM_MBUTTONDBLCLK = 521;
    public const int WM_MBUTTONDOWN = 519;
    public const int WM_MBUTTONUP = 520;
    public const int WM_RBUTTONDBLCLK = 518;
    public const int WM_RBUTTONDOWN = 516;
    public const int WM_RBUTTONUP = 517;
    public const int WM_XBUTTONDBLCLK = 525;
    public const int WM_XBUTTONDOWN = 523;
    public const int WM_XBUTTONUP = 524;
    public const int WM_PAINT = 15;
    public const int WM_ERASEBKGND = 20;
    public const int WM_SHOWWINDOW = 24;
    public const int WM_CAPTURECHANGED = 533;
    public const int WM_DWMCOMPOSITIONCHANGED = 798;
    public const int WM_NCUAHDRAWCAPTION = 174;
    public const int WM_NCUAHDRAWFRAME = 175;
    public const int WM_SIZE = 5;
    public const int WM_SIZING = 532;
    public const int WM_MOVE = 3;
    public const int WM_MOVING = 534;
    public const int WM_GETMINMAXINFO = 36;
    public const int WM_PRINT = 791;
    public const int WM_HSCROLL = 276;
    public const int WM_VSCROLL = 277;
    public const int WM_MOUSEWHEEL = 522;
    public const int WM_SETFOCUS = 7;
    public const int WM_KILLFOCUS = 8;
    public const int WM_SYSCOMMAND = 274;
    public const int WM_POPUPSYSTEMMENU = 787;
    public const int WM_SETTEXT = 12;
    public const int WM_GETICON = 127;
    public const int WM_SETICON = 128;
    public const int WM_STYLECHANGED = 125;
    public const int WM_MDIACTIVATE = 546;
    public const int WM_WINDOWPOSCHANGED = 71;
    public const int WM_WINDOWPOSCHANGING = 70;
    public const int WM_MOUSELEAVE = 675;
    public const int WM_SETREDRAW = 11;
    public const int WM_PARENTNOTIFY = 528;
    public const int WM_IME_CHAR = 646;
    public const int WM_HELP = 83;
    public const int WM_INITMENUPOPUP = 279;
    public const int WM_DPICHANGED = 736;
    public const int WS_BORDER = 8388608;
    public const int WS_CAPTION = 12582912;
    public const int WS_CHILD = 1073741824;
    public const int WS_CLIPCHILDREN = 33554432;
    public const int WS_CLIPSIBLINGS = 67108864;
    public const int WS_DISABLED = 134217728;
    public const int WS_DLGFRAME = 4194304;
    public const int WS_EX_APPWINDOW = 262144;
    public const int WS_EX_CLIENTEDGE = 512;
    public const int WS_EX_COMPOSITED = 33554432;
    public const int WS_EX_CONTEXTHELP = 1024;
    public const int WS_EX_CONTROLPARENT = 65536;
    public const int WS_EX_DLGMODALFRAME = 1;
    public const int WS_EX_LAYERED = 524288;
    public const int WS_EX_TRANSPARENT = 32;
    public const int WS_EX_LAYOUTRTL = 4194304;
    public const int WS_EX_LEFT = 0;
    public const int WS_EX_LTRREADING = 0;
    public const int WS_EX_LEFTSCROLLBAR = 16384;
    public const int WS_EX_RIGHTSCROLLBAR = 0;
    public const int WS_EX_MDICHILD = 64;
    public const int WS_EX_NOINHERITLAYOUT = 1048576;
    public const int WS_EX_NOPARENTNOTIFY = 4;
    public const int WS_EX_RIGHT = 4096;
    public const int WS_EX_RTLREADING = 8192;
    public const int WS_EX_STATICEDGE = 131072;
    public const int WS_EX_TOOLWINDOW = 128;
    public const int WS_EX_TOPMOST = 8;
    public const int WS_HSCROLL = 1048576;
    public const int WS_MAXIMIZE = 16777216;
    public const int WS_MAXIMIZEBOX = 65536;
    public const int WS_MINIMIZE = 536870912;
    public const int WS_MINIMIZEBOX = 131072;
    public const int WS_OVERLAPPED = 0;
    public const int WS_POPUP = -2147483648;
    public const int WS_SYSMENU = 524288;
    public const int WS_TABSTOP = 65536;
    public const int WS_THICKFRAME = 262144;
    public const int WS_VISIBLE = 268435456;
    public const int WS_VSCROLL = 2097152;
    public const int CS_DBLCLKS = 8;
    public const int CS_DROPSHADOW = 131072;
    public const int CS_SAVEBITS = 2048;
    public const int HTERROR = -2;
    public const int HTTRANSPARENT = -1;
    public const int HTNOWHERE = 0;
    public const int HTCLIENT = 1;
    public const int HTCAPTION = 2;
    public const int HTSYSMENU = 3;
    public const int HTGROWBOX = 4;
    public const int HTSIZE = 4;
    public const int HTMENU = 5;
    public const int HTHSCROLL = 6;
    public const int HTVSCROLL = 7;
    public const int HTMINBUTTON = 8;
    public const int HTMAXBUTTON = 9;
    public const int HTLEFT = 10;
    public const int HTRIGHT = 11;
    public const int HTTOP = 12;
    public const int HTTOPLEFT = 13;
    public const int HTTOPRIGHT = 14;
    public const int HTBOTTOM = 15;
    public const int HTBOTTOMLEFT = 16;
    public const int HTBOTTOMRIGHT = 17;
    public const int HTBORDER = 18;
    public const int HTREDUCE = 8;
    public const int HTZOOM = 9;
    public const int HTSIZEFIRST = 10;
    public const int HTSIZELAST = 17;
    public const int HTOBJECT = 19;
    public const int HTCLOSE = 20;
    public const int HTHELP = 21;
    public const int SRCAND = 8913094;
    public const int SRCCOPY = 13369376;
    public const int SRCPAINT = 15597702;
    public const int SWP_DRAWFRAME = 32;
    public const int SWP_NOSENDCHANGING = 1024;
    public const int SWP_DEFERERASE = 8192;
    public const int SWP_FRAMECHANGED = 32;
    public const int SWP_HIDEWINDOW = 128;
    public const int SWP_NOACTIVATE = 16;
    public const int SWP_NOCOPYBITS = 256;
    public const int SWP_NOMOVE = 2;
    public const int SWP_NOOWNERZORDER = 512;
    public const int SWP_NOSIZE = 1;
    public const int SWP_NOZORDER = 4;
    public const int SWP_NOREDRAW = 8;
    public const int SWP_SHOWWINDOW = 64;
    public const int VK_RETURN = 13;
    public const int VK_CONTROL = 17;
    public const int VK_DOWN = 40;
    public const int VK_ESCAPE = 27;
    public const int VK_INSERT = 45;
    public const int VK_LEFT = 37;
    public const int VK_MENU = 18;
    public const int VK_RIGHT = 39;
    public const int VK_SHIFT = 16;
    public const int VK_TAB = 9;
    public const int VK_UP = 38;
    public const int VK_SPACE = 32;
    public const int SC_CLOSE = 61536;
    public const int SC_CONTEXTHELP = 61824;
    public const int SC_KEYMENU = 61696;
    public const int SC_MAXIMIZE = 61488;
    public const int SC_MINIMIZE = 61472;
    public const int SC_MOVE = 61456;
    public const int SC_RESTORE = 61728;
    public const int SC_SIZE = 61440;
    public const int MF_ENABLED = 0;
    public const int MF_GRAYED = 1;
    public const int TME_HOVER = 1;
    public const int TME_LEAVE = 2;
    public const int TME_NONCLIENT = 16;
    public const int TME_QUERY = 1073741824;
    public const int TME_CANCEL = 8;
    public const int PRF_CHECKVISIBLE = 1;
    public const int PRF_CHILDREN = 16;
    public const int PRF_CLIENT = 4;
    public const int PRF_ERASEBKGND = 8;
    public const int PRF_OWNED = 32;
    public const int PRF_NONCLIENT = 2;
    public const int OBJ_BITMAP = 7;
    public const int OBJ_BRUSH = 2;
    public const int OBJ_DC = 3;
    public const int OBJ_ENHMETADC = 12;
    public const int OBJ_EXTPEN = 11;
    public const int OBJ_FONT = 6;
    public const int OBJ_MEMDC = 10;
    public const int OBJ_METADC = 4;
    public const int OBJ_METAFILE = 9;
    public const int OBJ_PAL = 5;
    public const int OBJ_PEN = 1;
    public const int OBJ_REGION = 8;
    public const int WMSZ_BOTTOM = 6;
    public const int WMSZ_BOTTOMLEFT = 7;
    public const int WMSZ_BOTTOMRIGHT = 8;
    public const int WMSZ_LEFT = 1;
    public const int WMSZ_RIGHT = 2;
    public const int WMSZ_TOP = 3;
    public const int WMSZ_TOPLEFT = 4;
    public const int WMSZ_TOPRIGHT = 5;
    public const int EM_POSFROMCHAR = 214;
    public const int EM_LINEFROMCHAR = 201;
    public const int SW_SHOWNOACTIVATE = 4;
    public const int WA_ACTIVE = 1;
    public const int WA_CLICKACTIVE = 2;
    public const int MA_NOACTIVATE = 3;
    public const int DCX_CACHE = 2;
    public const int DCX_INTERSECTRGN = 128;
    public const int DCX_LOCKWINDOWUPDATE = 1024;
    public const int DCX_WINDOW = 1;
    public const int DCX_CLIPSIBLINGS = 16;
    public const int DCX_VALIDATE = 2097152;
    public const int SIZE_MAXIMIZED = 2;
    public const int SIZE_MINIMIZED = 1;
    public const int SIZE_RESTORED = 0;
    public const int RDW_ALLCHILDREN = 128;
    public const int RDW_ERASE = 4;
    public const int RDW_ERASENOW = 512;
    public const int RDW_FRAME = 1024;
    public const int RDW_INVALIDATE = 1;
    public const int RDW_UPDATENOW = 256;
    public const int ICON_BIG = 1;
    public const int ICON_SMALL = 0;
    public const int LWA_ALPHA = 2;
    public const int LWA_COLORKEY = 1;
    public const int DWM_BB_ENABLE = 1;
    public const int DWM_BB_BLURREGION = 2;
    public const int DWM_BB_TRANSITIONONMAXIMIZED = 4;
    public const int GW_CHILD = 5;
    public const int GW_HWNDFIRST = 0;
    public const int GW_HWNDLAST = 1;
    public const int GW_HWNDNEXT = 2;
    public const int GW_HWNDPREV = 3;
    public const int GWL_EXSTYLE = -20;
    public const int GWL_HWNDPARENT = -8;
    public const int GWL_ID = -12;
    public const int GWL_STYLE = -16;
    public const int GWL_WNDPROC = -4;
    public const int GA_PARENT = 1;
    public const int GA_ROOT = 2;
    public const int TTM_GETDELAYTIME = 1045;
    public const int WM_GESTURE = 281;
    public const int GID_BEGIN = 1;
    public const int GID_END = 2;
    public const int GID_ZOOM = 3;
    public const int GID_PAN = 4;
    public const int GID_ROTATE = 5;
    public const int GID_TWOFINGERTAP = 6;
    public const int GID_PRESSANDTAP = 7;
    public const uint GC_ALLGESTURES = 1;
    public const uint GC_PAN = 1;
    public const uint GC_PAN_WITH_SINGLE_FINGER_VERTICALLY = 2;
    public const uint GC_PAN_WITH_SINGLE_FINGER_HORIZONTALLY = 4;
    public const uint GC_PAN_WITH_GUTTER = 8;
    public const uint GC_PAN_WITH_INERTIA = 16;
    public const uint GC_ZOOM = 1;
    public const uint GC_ROTATE = 1;
    public const uint GC_TWOFINGERTAP = 1;
    public const uint GC_PRESSANDTAP = 1;
    public const uint GF_BEGIN = 1;
    public const uint GF_INERTIA = 2;
    public const uint GF_END = 4;
    private const int S_OK = 0;
    private const int MONITOR_DEFAULTTONEAREST = 2;
    private const int E_INVALIDARG = -2147024809;
    public const uint SHGFI_ICON = 256;
    public const uint SHGFI_LARGEICON = 0;
    public const uint SHGFI_SMALLICON = 1;
    public static readonly int TTM_ADDTOOL;
    public static readonly int TTM_DELTOOL;
    public static readonly int TTM_ENUMTOOLS;
    public static readonly int TTM_GETCURRENTTOOL;
    public static readonly int TTM_GETTEXT;
    public static readonly int TTM_GETTOOLINFO;
    public static readonly int TTM_HITTEST;
    public static readonly int TTM_NEWTOOLRECT;
    public static readonly int TTM_SETTITLE;
    public static readonly int TTM_SETTOOLINFO;
    public static readonly int TTM_UPDATETIPTEXT;

    static NativeMethods()
    {
      if (Marshal.SystemDefaultCharSize == 1)
      {
        NativeMethods.TTM_ADDTOOL = 1028;
        NativeMethods.TTM_SETTITLE = 1056;
        NativeMethods.TTM_DELTOOL = 1029;
        NativeMethods.TTM_NEWTOOLRECT = 1030;
        NativeMethods.TTM_GETTOOLINFO = 1032;
        NativeMethods.TTM_SETTOOLINFO = 1033;
        NativeMethods.TTM_HITTEST = 1034;
        NativeMethods.TTM_GETTEXT = 1035;
        NativeMethods.TTM_UPDATETIPTEXT = 1036;
        NativeMethods.TTM_ENUMTOOLS = 1038;
        NativeMethods.TTM_GETCURRENTTOOL = 1039;
      }
      else
      {
        NativeMethods.TTM_ADDTOOL = 1074;
        NativeMethods.TTM_SETTITLE = 1057;
        NativeMethods.TTM_DELTOOL = 1075;
        NativeMethods.TTM_NEWTOOLRECT = 1076;
        NativeMethods.TTM_GETTOOLINFO = 1077;
        NativeMethods.TTM_SETTOOLINFO = 1078;
        NativeMethods.TTM_HITTEST = 1079;
        NativeMethods.TTM_GETTEXT = 1080;
        NativeMethods.TTM_UPDATETIPTEXT = 1081;
        NativeMethods.TTM_ENUMTOOLS = 1082;
        NativeMethods.TTM_GETCURRENTTOOL = 1083;
      }
    }

    [DllImport("user32.dll")]
    public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

    public static int WM_MOUSEENTER
    {
      get
      {
        if (NativeMethods.wmMouseEnterMessage == -1)
          NativeMethods.wmMouseEnterMessage = NativeMethods.RegisterWindowMessage("WinFormsMouseEnter");
        return NativeMethods.wmMouseEnterMessage;
      }
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr LoadLibrary(string libname);

    [DllImport("kernel32.dll")]
    public static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    [DllImport("kernel32.dll")]
    public static extern bool GetPhysicallyInstalledSystemMemory(out long totalMemoryInKilobytes);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool GlobalMemoryStatusEx([In, Out] NativeMethods.MEMORYSTATUSEX lpBuffer);

    internal static void GetTotalPhysicalMemory(ref ulong totalMemoryInBytes)
    {
      NativeMethods.MEMORYSTATUSEX lpBuffer = new NativeMethods.MEMORYSTATUSEX();
      if (!NativeMethods.GlobalMemoryStatusEx(lpBuffer))
        return;
      totalMemoryInBytes = lpBuffer.ullTotalPhys;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern int lstrlen(string s);

    public static bool IsLibraryAvailable(string libname)
    {
      bool flag = false;
      IntPtr hModule = NativeMethods.LoadLibrary(libname);
      if (hModule != IntPtr.Zero)
      {
        NativeMethods.FreeLibrary(hModule);
        flag = true;
      }
      return flag;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      IntPtr hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      HandleRef hWnd,
      int msg,
      bool wParam,
      int lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      HandleRef hWnd,
      int Msg,
      int wParam,
      [In, Out] ref Rectangle lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      HandleRef hWnd,
      int msg,
      int wParam,
      int[] lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      HandleRef hWnd,
      int Msg,
      [MarshalAs(UnmanagedType.Bool), In, Out] ref bool wParam,
      IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      HandleRef hWnd,
      int Msg,
      ref short wParam,
      ref short lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      HandleRef hWnd,
      int Msg,
      int wParam,
      IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      HandleRef hWnd,
      int msg,
      int wParam,
      string lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.IUnknown)] out object editOle);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      HandleRef hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      HandleRef hWnd,
      int msg,
      IntPtr wParam,
      string lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hwnd, int msg, bool wparam, int lparam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(
      HandleRef hWnd,
      int msg,
      int wParam,
      NativeMethods.POINT lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr PostMessage(HandleRef hwnd, int msg, int wparam, int lparam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr PostMessage(
      HandleRef hwnd,
      int msg,
      int wparam,
      IntPtr lparam);

    [DllImport("user32", CharSet = CharSet.Auto)]
    public static extern int PostMessage(IntPtr handle, int msg, int wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool SetWindowPos(
      HandleRef hWnd,
      HandleRef hWndInsertAfter,
      int x,
      int y,
      int cx,
      int cy,
      int flags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool RedrawWindow(
      HandleRef hwnd,
      IntPtr rcUpdate,
      HandleRef hrgnUpdate,
      int flags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool RedrawWindow(
      HandleRef hwnd,
      ref NativeMethods.RECT rcUpdate,
      HandleRef hrgnUpdate,
      int flags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref NativeMethods.RECT rect);

    [DllImport("user32.dll", EntryPoint = "WindowFromPoint", CharSet = CharSet.Auto)]
    public static extern IntPtr _WindowFromPoint(NativeMethods.POINTSTRUCT pt);

    [DllImport("user32.dll", EntryPoint = "GetDC", CharSet = CharSet.Auto)]
    private static extern IntPtr IntGetDC(HandleRef hWnd);

    [DllImport("user32.dll", EntryPoint = "GetDCEx", CharSet = CharSet.Auto)]
    internal static extern IntPtr IntGetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags);

    [DllImport("user32.dll", EntryPoint = "GetWindowDC", CharSet = CharSet.Auto)]
    internal static extern IntPtr IntGetWindowDC(HandleRef hWnd);

    [DllImport("user32.dll", EntryPoint = "ReleaseDC", CharSet = CharSet.Auto)]
    internal static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int UpdateLayeredWindow(
      IntPtr hwnd,
      IntPtr hdcDst,
      ref NativeMethods.POINTSTRUCT pptDst,
      ref NativeMethods.SIZESTRUCT psize,
      IntPtr hdcSrc,
      ref NativeMethods.POINTSTRUCT pprSrc,
      int crKey,
      ref NativeMethods.BLENDFUNCTION pblend,
      int dwFlags);

    [DllImport("user32.dll")]
    public static extern int MsgWaitForMultipleObjects(
      int nCount,
      int pHandles,
      bool bWaitAll,
      int dwMilliseconds,
      int dwWakeMask);

    [DllImport("user32.dll")]
    public static extern bool HideCaret(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool IsWindow(HandleRef hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool IsZoomed(HandleRef hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool IsIconic(HandleRef hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int RegisterWindowMessage(string msg);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SetFocus(HandleRef hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetFocus();

    [DllImport("user32.dll")]
    public static extern IntPtr SetActiveWindow(HandleRef handle);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll")]
    public static extern uint MapVirtualKey(uint uCode, uint uMapType);

    [DllImport("user32.dll")]
    public static extern bool GetKeyboardState(byte[] lpKeyState);

    [DllImport("user32.dll")]
    public static extern IntPtr GetKeyboardLayout(uint idThread);

    [DllImport("user32.dll")]
    public static extern int ToUnicodeEx(
      uint wVirtKey,
      uint wScanCode,
      byte[] lpKeyState,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pwszBuff,
      int cchBuff,
      uint wFlags,
      IntPtr dwhkl);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int ClientToScreen(HandleRef hWnd, [In, Out] NativeMethods.POINT pt);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool AdjustWindowRectEx(
      ref NativeMethods.RECT lpRect,
      int dwStyle,
      bool bMenu,
      int dwExStyle);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MapWindowPoints(
      HandleRef hWndFrom,
      HandleRef hWndTo,
      [In, Out] ref NativeMethods.RECT rect,
      int cPoints);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MapWindowPoints(
      HandleRef hWndFrom,
      HandleRef hWndTo,
      [In, Out] NativeMethods.POINT pt,
      int cPoints);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool SetLayeredWindowAttributes(
      HandleRef hwnd,
      int crKey,
      byte bAlpha,
      int dwFlags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetCapture();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool ReleaseCapture();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool MessageBeep(int type);

    [DllImport("user32.dll")]
    public static extern bool GetIconInfo(IntPtr hIcon, ref NativeMethods.IconInfo pIconInfo);

    [DllImport("user32.dll")]
    public static extern IntPtr CreateIconIndirect(ref NativeMethods.IconInfo icon);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern short GetKeyState(int keyCode);

    [DllImport("user32.dll")]
    public static extern bool InvertRect(IntPtr hDC, [In] ref NativeMethods.RECT lprc);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetSysColor(int nIndex);

    public static void InvertRect(Graphics graphics, Rectangle rectangle)
    {
      IntPtr hdc = graphics.GetHdc();
      NativeMethods.RECT lprc = new NativeMethods.RECT(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom);
      NativeMethods.InvertRect(hdc, ref lprc);
      graphics.ReleaseHdc();
    }

    [DllImport("user32.dll")]
    public static extern bool GetUpdateRect(IntPtr hWnd, ref NativeMethods.RECT rect, bool bErase);

    [DllImport("Gdi32.dll")]
    public static extern bool GetTextMetrics(IntPtr hdc, ref NativeMethods.TEXTMETRIC tm);

    [DllImport("Gdi32.dll")]
    public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);

    [DllImport("Gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool IntDeleteObject(IntPtr hObject);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern bool BitBlt(
      IntPtr hDC,
      int x,
      int y,
      int nWidth,
      int nHeight,
      IntPtr hSrcDC,
      int xSrc,
      int ySrc,
      int dwRop);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool PatBlt(
      HandleRef hdc,
      int left,
      int top,
      int width,
      int height,
      int rop);

    [DllImport("Gdi32.dll", EntryPoint = "CreateCompatibleDC", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

    [DllImport("Gdi32.dll", EntryPoint = "DeleteDC", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool IntDeleteDC(HandleRef hDC);

    [DllImport("Gdi32.dll")]
    public static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse);

    [DllImport("Gdi32.dll", EntryPoint = "CreateBitmap", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntCreateBitmap(
      int nWidth,
      int nHeight,
      int nPlanes,
      int nBitsPerPixel,
      IntPtr lpvBits);

    [DllImport("Gdi32.dll", EntryPoint = "CreateBitmap", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntCreateBitmapByte(
      int nWidth,
      int nHeight,
      int nPlanes,
      int nBitsPerPixel,
      byte[] lpvBits);

    [DllImport("Gdi32.dll", EntryPoint = "CreateBitmap", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntCreateBitmapShort(
      int nWidth,
      int nHeight,
      int nPlanes,
      int nBitsPerPixel,
      short[] lpvBits);

    [DllImport("Gdi32.dll", EntryPoint = "CreateBrushIndirect", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr IntCreateBrushIndirect(NativeMethods.LOGBRUSH lb);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr CreateDIBSection(
      IntPtr hdc,
      [MarshalAs(UnmanagedType.LPStruct), In] NativeMethods.BITMAPINFO pbmi,
      uint iUsage,
      out IntPtr ppvBits,
      IntPtr hSection,
      uint dwOffset);

    [DllImport("Gdi32.dll")]
    public static extern bool PtInRegion(IntPtr hRgn, int x, int y);

    [DllImport("Gdi32.dll")]
    public static extern IntPtr CreateDC(
      string strDriver,
      string strDevice,
      string strOutput,
      IntPtr pData);

    [DllImport("Gdi32.dll")]
    public static extern int GetPixel(IntPtr hdc, int x, int y);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SaveDC(HandleRef hDC);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool RestoreDC(HandleRef hDC, int nSavedDC);

    [DllImport("Gdi32.dll")]
    public static extern bool BitBlt(
      IntPtr hdcDest,
      int nXDest,
      int nYDest,
      int nWidth,
      int nHeight,
      IntPtr hdcSrc,
      int nXSrc,
      int nYSrc,
      long dwRop);

    [DllImport("Gdi32.dll", SetLastError = true)]
    public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("Gdi32.dll", SetLastError = true)]
    public static extern bool DeleteDC(IntPtr hdc);

    [DllImport("Gdi32.dll", SetLastError = true)]
    public static extern bool DeleteObject(IntPtr hObject);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

    [DllImport("comctl32.dll")]
    public static extern bool _TrackMouseEvent(NativeMethods.TRACKMOUSEEVENT tme);

    [DllImport("comctl32.dll")]
    public static extern bool InitCommonControlsEx(NativeMethods.INITCOMMONCONTROLSEX icc);

    [DllImport("user32")]
    public static extern bool SetGestureConfig(
      IntPtr hwnd,
      uint dwReserved,
      uint cIDs,
      NativeMethods.GESTURECONFIG[] pGestureConfig,
      uint cbSize);

    public static double GID_ROTATE_ANGLE_FROM_ARGUMENT(ulong arg)
    {
      return (double) arg / (double) ushort.MaxValue * 4.0 * 3.14159265 - 6.2831853;
    }

    public static ushort LoWord(uint number)
    {
      return (ushort) (number & (uint) ushort.MaxValue);
    }

    public static ushort HiWord(uint number)
    {
      return (ushort) (number >> 16 & (uint) ushort.MaxValue);
    }

    public static uint LoDWord(ulong number)
    {
      return (uint) (number & (ulong) uint.MaxValue);
    }

    public static uint HiDWord(ulong number)
    {
      return (uint) (number >> 32 & (ulong) uint.MaxValue);
    }

    public static short LoWord(int number)
    {
      return (short) number;
    }

    public static short HiWord(int number)
    {
      return (short) (number >> 16);
    }

    public static int LoDWord(long number)
    {
      return (int) number;
    }

    public static int HiDWord(long number)
    {
      return (int) (number >> 32);
    }

    public static Point GetSystemDpi()
    {
      Point point = new Point();
      IntPtr dc = NativeMethods.DeviceCapsNativeMethods.GetDC(IntPtr.Zero);
      point.X = NativeMethods.DeviceCapsNativeMethods.GetDeviceCaps(dc, NativeMethods.DeviceCapsNativeMethods.LOGPIXELSX);
      point.Y = NativeMethods.DeviceCapsNativeMethods.GetDeviceCaps(dc, NativeMethods.DeviceCapsNativeMethods.LOGPIXELSY);
      NativeMethods.DeviceCapsNativeMethods.ReleaseDC(IntPtr.Zero, dc);
      return point;
    }

    public static float GetSystemDpiScaling()
    {
      IntPtr dc = NativeMethods.DeviceCapsNativeMethods.GetDC(IntPtr.Zero);
      int deviceCaps1 = NativeMethods.DeviceCapsNativeMethods.GetDeviceCaps(dc, NativeMethods.DeviceCapsNativeMethods.VERTRES);
      int deviceCaps2 = NativeMethods.DeviceCapsNativeMethods.GetDeviceCaps(dc, NativeMethods.DeviceCapsNativeMethods.DESKTOPVERTRES);
      NativeMethods.DeviceCapsNativeMethods.ReleaseDC(IntPtr.Zero, dc);
      return (float) deviceCaps2 / (float) deviceCaps1;
    }

    public static bool IsDifferentFont()
    {
      Point systemDpi = NativeMethods.GetSystemDpi();
      if (systemDpi.X == 96)
        return systemDpi.Y != 96;
      return true;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromPoint([In] Point pt, [In] uint dwFlags);

    [DllImport("Shcore.dll")]
    private static extern IntPtr GetDpiForMonitor(
      [In] IntPtr hmonitor,
      [In] NativeMethods.DpiType dpiType,
      out uint dpiX,
      out uint dpiY);

    public static SizeF GetMonitorDpi(Screen screen, NativeMethods.DpiType dpiType)
    {
      if (Environment.OSVersion.Version.Major < 6 || Environment.OSVersion.Version.Major == 6 && Environment.Version.Minor < 3)
      {
        if (NativeMethods.measurementGraphics == null)
          NativeMethods.measurementGraphics = Graphics.FromHwnd(IntPtr.Zero);
        return new SizeF(NativeMethods.measurementGraphics.DpiX / 96f, NativeMethods.measurementGraphics.DpiY / 96f);
      }
      uint dpiX;
      uint dpiY;
      switch (NativeMethods.GetDpiForMonitor(NativeMethods.MonitorFromPoint(new Point(screen.Bounds.Left + 1, screen.Bounds.Top + 1), 2U), dpiType, out dpiX, out dpiY).ToInt32())
      {
        case -2147024809:
          throw new ArgumentException("Unknown error. See https://msdn.microsoft.com/en-us/library/windows/desktop/dn280510.aspx for more information.");
        case 0:
          return new SizeF((float) dpiX / 96f, (float) dpiY / 96f);
        default:
          throw new COMException("Unknown error. See https://msdn.microsoft.com/en-us/library/windows/desktop/dn280510.aspx for more information.");
      }
    }

    public static IntPtr GetDC(HandleRef hWnd)
    {
      return HandleCollector.Add(NativeMethods.IntGetDC(hWnd), NativeMethods.CommonHandles.HDC);
    }

    public static IntPtr GetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags)
    {
      return HandleCollector.Add(NativeMethods.IntGetDCEx(hWnd, hrgnClip, flags), NativeMethods.CommonHandles.HDC);
    }

    public static IntPtr GetWindowDC(HandleRef hWnd)
    {
      return HandleCollector.Add(NativeMethods.IntGetWindowDC(hWnd), NativeMethods.CommonHandles.HDC);
    }

    public static IntPtr CreateCompatibleDC(HandleRef hDC)
    {
      return HandleCollector.Add(NativeMethods.IntCreateCompatibleDC(hDC), NativeMethods.CommonHandles.CompatibleHDC);
    }

    public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
    {
      HandleCollector.Remove((IntPtr) hDC, NativeMethods.CommonHandles.HDC);
      return NativeMethods.IntReleaseDC(hWnd, hDC);
    }

    public static bool DeleteDC(HandleRef hDC)
    {
      HandleCollector.Remove((IntPtr) hDC, NativeMethods.CommonHandles.HDC);
      return NativeMethods.IntDeleteDC(hDC);
    }

    public static IntPtr WindowFromPoint(int x, int y)
    {
      return NativeMethods._WindowFromPoint(new NativeMethods.POINTSTRUCT(x, y));
    }

    public static bool DeleteObject(HandleRef hObject)
    {
      HandleCollector.Remove((IntPtr) hObject, NativeMethods.CommonHandles.GDI);
      return NativeMethods.IntDeleteObject((IntPtr) hObject);
    }

    public static int GetObject(HandleRef hObject, NativeMethods.LOGBRUSH lb)
    {
      return NativeMethods.GetObject(hObject, Marshal.SizeOf(typeof (NativeMethods.LOGBRUSH)), lb);
    }

    public static int GetObject(HandleRef hObject, NativeMethods.LOGFONT lp)
    {
      return NativeMethods.GetObject(hObject, Marshal.SizeOf(typeof (NativeMethods.LOGFONT)), lp);
    }

    public static int GetObject(HandleRef hObject, NativeMethods.LOGPEN lp)
    {
      return NativeMethods.GetObject(hObject, Marshal.SizeOf(typeof (NativeMethods.LOGPEN)), lp);
    }

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetObject(HandleRef hObject, int nSize, ref int nEntries);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] NativeMethods.BITMAP bm);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] NativeMethods.LOGPEN lp);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetObject(HandleRef hObject, int nSize, int[] nEntries);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] NativeMethods.LOGBRUSH lb);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] NativeMethods.LOGFONT lf);

    public static void UpdateZOrder(HandleRef handle, HandleRef pos, bool activate)
    {
      int flags = 1539;
      if (!activate)
        flags |= 16;
      NativeMethods.SetWindowPos(handle, pos, 0, 0, 0, 0, flags);
    }

    [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
    public static extern IntPtr SetWindowLongPtr32(
      HandleRef hWnd,
      int nIndex,
      HandleRef dwNewLong);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
    public static extern IntPtr SetWindowLongPtr64(
      HandleRef hWnd,
      int nIndex,
      HandleRef dwNewLong);

    public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
    {
      if (IntPtr.Size == 4)
        return NativeMethods.SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
      return NativeMethods.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
    }

    [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
    public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
    public static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);

    public static IntPtr GetWindowLong(HandleRef hWnd, int nIndex)
    {
      if (IntPtr.Size == 4)
        return NativeMethods.GetWindowLong32(hWnd, nIndex);
      return NativeMethods.GetWindowLongPtr64(hWnd, nIndex);
    }

    [DllImport("user32.dll", EntryPoint = "GetClassLong")]
    public static extern uint GetClassLongPtr32(HandleRef hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
    public static extern IntPtr GetClassLongPtr64(HandleRef hWnd, int nIndex);

    public static IntPtr GetClassLongPtr(HandleRef hWnd, int nIndex)
    {
      if (IntPtr.Size > 4)
        return NativeMethods.GetClassLongPtr64(hWnd, nIndex);
      return new IntPtr((long) NativeMethods.GetClassLongPtr32(hWnd, nIndex));
    }

    [DllImport("user32.dll", EntryPoint = "SetClassLong", CharSet = CharSet.Auto)]
    public static extern IntPtr SetClassLongPtr32(
      HandleRef hwnd,
      int nIndex,
      IntPtr dwNewLong);

    [DllImport("user32.dll", EntryPoint = "SetClassLongPtr", CharSet = CharSet.Auto)]
    public static extern IntPtr SetClassLongPtr64(
      HandleRef hwnd,
      int nIndex,
      IntPtr dwNewLong);

    public static IntPtr SetClassLong(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
    {
      if (IntPtr.Size == 4)
        return NativeMethods.SetClassLongPtr32(hWnd, nIndex, dwNewLong);
      return NativeMethods.SetClassLongPtr64(hWnd, nIndex, dwNewLong);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool AnimateWindow(
      IntPtr hwnd,
      int time,
      NativeMethods.AnimateWindowFlags flags);

    [DllImport("user32.dll")]
    public static extern IntPtr GetTopWindow(IntPtr hwnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetWindow(IntPtr hWnd, NativeMethods.GetWindow_Cmd uCmd);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern int GetTextFace(IntPtr hdc, int nCount, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpFaceName);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern int GetFontUnicodeRanges(IntPtr hdc, [MarshalAs(UnmanagedType.LPStruct), Out] NativeMethods.GlyphSet lpgs);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern int GetFontData(
      IntPtr hdc,
      int dwTable,
      int dwOffset,
      [MarshalAs(UnmanagedType.LPArray)] byte[] lpvBuffer,
      int cbData);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern int GetGlyphIndices(
      IntPtr hdc,
      string lpstr,
      int c,
      [MarshalAs(UnmanagedType.LPArray)] short[] pgi,
      int fl);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetCurrentObject(
      IntPtr hdc,
      NativeMethods.GdiDcObject uObjectType);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetCurrentObject(HandleRef hdc, int uObjectType);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern int EnumFontFamilies(
      IntPtr hdc,
      [MarshalAs(UnmanagedType.LPTStr)] string lpszFamily,
      NativeMethods.FontEnumDelegate lpEnumFontFamProc,
      int lParam);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern int EnumFontFamiliesEx(
      IntPtr hdc,
      [MarshalAs(UnmanagedType.LPStruct)] NativeMethods.LOGFONT lplf,
      NativeMethods.FontEnumDelegate lpEnumFontFamProc,
      int lParam,
      int dwFlags);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr CreateFontIndirect([MarshalAs(UnmanagedType.LPStruct)] NativeMethods.LOGFONT lplf);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern int AddFontResourceEx([MarshalAs(UnmanagedType.LPTStr), In] string lpszFilename, int fl, int pdv);

    public static Region CreateRoundRectRgn(Rectangle bounds, int radius)
    {
      IntPtr roundRectRgn = NativeMethods.CreateRoundRectRgn(bounds.X, bounds.Y, bounds.Width + 1, bounds.Height + 1, radius, radius);
      Region region = Region.FromHrgn(roundRectRgn);
      NativeMethods.DeleteObject(new HandleRef((object) null, roundRectRgn));
      return region;
    }

    public static IntPtr CreateBitmap(
      int nWidth,
      int nHeight,
      int nPlanes,
      int nBitsPerPixel,
      IntPtr lpvBits)
    {
      return HandleCollector.Add(NativeMethods.IntCreateBitmap(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), NativeMethods.CommonHandles.GDI);
    }

    public static IntPtr CreateBitmap(
      int nWidth,
      int nHeight,
      int nPlanes,
      int nBitsPerPixel,
      byte[] lpvBits)
    {
      return HandleCollector.Add(NativeMethods.IntCreateBitmapByte(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), NativeMethods.CommonHandles.GDI);
    }

    public static IntPtr CreateBitmap(
      int nWidth,
      int nHeight,
      int nPlanes,
      int nBitsPerPixel,
      short[] lpvBits)
    {
      return HandleCollector.Add(NativeMethods.IntCreateBitmapShort(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), NativeMethods.CommonHandles.GDI);
    }

    public static IntPtr CreateBrushIndirect(NativeMethods.LOGBRUSH lb)
    {
      return HandleCollector.Add(NativeMethods.IntCreateBrushIndirect(lb), NativeMethods.CommonHandles.GDI);
    }

    [DllImport("shell32.dll")]
    public static extern IntPtr SHGetFileInfo(
      string pszPath,
      uint dwFileAttributes,
      ref NativeMethods.SHFILEINFO psfi,
      uint cbSizeFileInfo,
      uint uFlags);

    [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
    public static extern bool GetTextMetrics(IntPtr hdc, out NativeMethods.TextMetric tm);

    [DllImport("Gdi32.dll")]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [SecuritySafeCritical]
    public static NativeMethods.TextMetric GetTextMetrics(IntPtr hdc, IntPtr hFont)
    {
      NativeMethods.TextMetric tm;
      NativeMethods.GetTextMetrics(hdc, out tm);
      return tm;
    }

    public struct SHFILEINFO
    {
      public IntPtr hIcon;
      public IntPtr iIcon;
      public uint dwAttributes;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
      public string szDisplayName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
      public string szTypeName;
    }

    public struct DWM_BLURBEHIND
    {
      public const uint DWM_BB_ENABLE = 1;
      public const uint DWM_BB_BLURREGION = 2;
      public const uint DWM_BB_TRANSITIONONMAXIMIZED = 4;
      public uint dwFlags;
      [MarshalAs(UnmanagedType.Bool)]
      public bool fEnable;
      public IntPtr hRegionBlur;
      [MarshalAs(UnmanagedType.Bool)]
      public bool fTransitionOnMaximized;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
      public int x;
      public int y;

      public POINT()
      {
      }

      public POINT(int x, int y)
      {
        this.x = x;
        this.y = y;
      }
    }

    public struct RECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;

      public RECT(int left, int top, int right, int bottom)
      {
        this.left = left;
        this.top = top;
        this.right = right;
        this.bottom = bottom;
      }

      public RECT(Rectangle r)
      {
        this.left = r.Left;
        this.top = r.Top;
        this.right = r.Right;
        this.bottom = r.Bottom;
      }

      public static NativeMethods.RECT FromXYWH(int x, int y, int width, int height)
      {
        return new NativeMethods.RECT(x, y, x + width, y + height);
      }

      public static NativeMethods.RECT FromRectangle(Rectangle rect)
      {
        return new NativeMethods.RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
      }

      public Size Size
      {
        get
        {
          return new Size(this.right - this.left, this.bottom - this.top);
        }
      }

      public Rectangle Rect
      {
        get
        {
          return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
        }
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class SIZE
    {
      public int cx;
      public int cy;

      public SIZE()
      {
      }

      public SIZE(int cx, int cy)
      {
        this.cx = cx;
        this.cy = cy;
      }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class TOOLINFO_TOOLTIP
    {
      public int cbSize;
      public int uFlags;
      public IntPtr hwnd;
      public IntPtr uId;
      public NativeMethods.RECT rect;
      public IntPtr hinst;
      public IntPtr lpszText;
      public IntPtr lParam;

      public TOOLINFO_TOOLTIP()
      {
        this.cbSize = Marshal.SizeOf(typeof (NativeMethods.TOOLINFO_TOOLTIP));
        this.hinst = IntPtr.Zero;
        this.lParam = IntPtr.Zero;
      }
    }

    public struct NCCALCSIZE_PARAMS
    {
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
      public NativeMethods.RECT[] rgrc;
      public IntPtr lppos;
    }

    public struct POINTSTRUCT
    {
      public int x;
      public int y;

      public POINTSTRUCT(int x, int y)
      {
        this.x = x;
        this.y = y;
      }
    }

    public struct SIZESTRUCT
    {
      public int cx;
      public int cy;

      public SIZESTRUCT(int cx, int cy)
      {
        this.cx = cx;
        this.cy = cy;
      }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BLENDFUNCTION
    {
      public byte BlendOp;
      public byte BlendFlags;
      public byte SourceConstantAlpha;
      public byte AlphaFormat;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TEXTMETRIC
    {
      public int tmHeight;
      public int tmAscent;
      public int tmDescent;
      public int tmInternalLeading;
      public int tmExternalLeading;
      public int tmAveCharWidth;
      public int tmMaxCharWidth;
      public int tmWeight;
      public int tmOverhang;
      public int tmDigitizedAspectX;
      public int tmDigitizedAspectY;
      public char tmFirstChar;
      public char tmLastChar;
      public char tmDefaultChar;
      public char tmBreakChar;
      public byte tmItalic;
      public byte tmUnderlined;
      public byte tmStruckOut;
      public byte tmPitchAndFamily;
      public byte tmCharSet;
    }

    public struct MARGINS
    {
      public int cxLeftWidth;
      public int cxRightWidth;
      public int cyTopHeight;
      public int cyBottomHeight;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MINMAXINFO
    {
      public NativeMethods.POINT ptReserved;
      public NativeMethods.POINT ptMaxSize;
      public NativeMethods.POINT ptMaxPosition;
      public NativeMethods.POINT ptMinTrackSize;
      public NativeMethods.POINT ptMaxTrackSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class TRACKMOUSEEVENT
    {
      public int cbSize;
      public int dwFlags;
      public IntPtr hwndTrack;
      public int dwHoverTime;

      public TRACKMOUSEEVENT()
      {
        this.cbSize = Marshal.SizeOf(typeof (NativeMethods.TRACKMOUSEEVENT));
        this.dwHoverTime = 100;
      }
    }

    public struct WINDOWPOS
    {
      public IntPtr hwnd;
      public IntPtr hwndInsertAfter;
      public int x;
      public int y;
      public int cx;
      public int cy;
      public int flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class INITCOMMONCONTROLSEX
    {
      public int dwSize;
      public int dwICC;

      public INITCOMMONCONTROLSEX()
      {
        this.dwSize = 8;
      }
    }

    public struct PAINTSTRUCT
    {
      public IntPtr hdc;
      public bool fErase;
      public int rcPaint_left;
      public int rcPaint_top;
      public int rcPaint_right;
      public int rcPaint_bottom;
      public bool fRestore;
      public bool fIncUpdate;
      public int reserved1;
      public int reserved2;
      public int reserved3;
      public int reserved4;
      public int reserved5;
      public int reserved6;
      public int reserved7;
      public int reserved8;
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

      public LOGFONT()
      {
      }

      public LOGFONT(NativeMethods.LOGFONT lf)
      {
        this.lfHeight = lf.lfHeight;
        this.lfWidth = lf.lfWidth;
        this.lfEscapement = lf.lfEscapement;
        this.lfOrientation = lf.lfOrientation;
        this.lfWeight = lf.lfWeight;
        this.lfItalic = lf.lfItalic;
        this.lfUnderline = lf.lfUnderline;
        this.lfStrikeOut = lf.lfStrikeOut;
        this.lfCharSet = lf.lfCharSet;
        this.lfOutPrecision = lf.lfOutPrecision;
        this.lfClipPrecision = lf.lfClipPrecision;
        this.lfQuality = lf.lfQuality;
        this.lfPitchAndFamily = lf.lfPitchAndFamily;
        this.lfFaceName = lf.lfFaceName;
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class LOGPEN
    {
      public int lopnStyle;
      public int lopnWidth_x;
      public int lopnWidth_y;
      public int lopnColor;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class LOGBRUSH
    {
      public int lbStyle;
      public int lbColor;
      public IntPtr lbHatch;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class BITMAP
    {
      public int bmType;
      public int bmWidth;
      public int bmHeight;
      public int bmWidthBytes;
      public short bmPlanes;
      public short bmBitsPixel;
      public IntPtr bmBits;

      public BITMAP()
      {
        this.bmBits = IntPtr.Zero;
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class BITMAPINFO
    {
      public int bmiHeader_biSize;
      public int bmiHeader_biWidth;
      public int bmiHeader_biHeight;
      public short bmiHeader_biPlanes;
      public short bmiHeader_biBitCount;
      public int bmiHeader_biCompression;
      public int bmiHeader_biSizeImage;
      public int bmiHeader_biXPelsPerMeter;
      public int bmiHeader_biYPelsPerMeter;
      public int bmiHeader_biClrUsed;
      public int bmiHeader_biClrImportant;
      public byte bmiColors_rgbBlue;
      public byte bmiColors_rgbGreen;
      public byte bmiColors_rgbRed;
      public byte bmiColors_rgbReserved;

      internal BITMAPINFO()
      {
        this.bmiHeader_biSize = 40;
      }
    }

    public struct IconInfo
    {
      public bool fIcon;
      public int xHotspot;
      public int yHotspot;
      public IntPtr hbmMask;
      public IntPtr hbmColor;
    }

    public sealed class CommonHandles
    {
      public static readonly int Accelerator = HandleCollector.RegisterType(nameof (Accelerator), 80, 50);
      public static readonly int Cursor = HandleCollector.RegisterType(nameof (Cursor), 20, 500);
      public static readonly int EMF = HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
      public static readonly int Find = HandleCollector.RegisterType(nameof (Find), 0, 1000);
      public static readonly int GDI = HandleCollector.RegisterType(nameof (GDI), 50, 500);
      public static readonly int HDC = HandleCollector.RegisterType(nameof (HDC), 100, 2);
      public static readonly int CompatibleHDC = HandleCollector.RegisterType("ComptibleHDC", 50, 50);
      public static readonly int Icon = HandleCollector.RegisterType(nameof (Icon), 20, 500);
      public static readonly int Kernel = HandleCollector.RegisterType(nameof (Kernel), 0, 1000);
      public static readonly int Menu = HandleCollector.RegisterType(nameof (Menu), 30, 1000);
      public static readonly int Window = HandleCollector.RegisterType(nameof (Window), 5, 1000);
    }

    public static class Util
    {
      private static int GetEmbeddedNullStringLengthAnsi(string s)
      {
        int length = s.IndexOf(char.MinValue);
        if (length > -1)
          return NativeMethods.Util.GetPInvokeStringLength(s.Substring(0, length)) + NativeMethods.Util.GetEmbeddedNullStringLengthAnsi(s.Substring(length + 1)) + 1;
        return NativeMethods.Util.GetPInvokeStringLength(s);
      }

      public static int GetPInvokeStringLength(string s)
      {
        if (s == null)
          return 0;
        if (Marshal.SystemDefaultCharSize == 2)
          return s.Length;
        if (s.Length == 0)
          return 0;
        if (s.IndexOf(char.MinValue) > -1)
          return NativeMethods.Util.GetEmbeddedNullStringLengthAnsi(s);
        return NativeMethods.lstrlen(s);
      }

      public static int HIWORD(int n)
      {
        return n >> 16 & (int) ushort.MaxValue;
      }

      public static int HIWORD(IntPtr n)
      {
        return NativeMethods.Util.HIWORD((int) (long) n);
      }

      public static int LOWORD(int n)
      {
        return n & (int) ushort.MaxValue;
      }

      public static int LOWORD(IntPtr n)
      {
        return NativeMethods.Util.LOWORD((int) (long) n);
      }

      public static int MAKELONG(int low, int high)
      {
        return high << 16 | low & (int) ushort.MaxValue;
      }

      public static IntPtr MAKELPARAM(int low, int high)
      {
        return (IntPtr) (high << 16 | low & (int) ushort.MaxValue);
      }

      public static int SignedHIWORD(int n)
      {
        return (int) (short) (n >> 16 & (int) ushort.MaxValue);
      }

      public static int SignedHIWORD(IntPtr n)
      {
        return NativeMethods.Util.SignedHIWORD((int) (long) n);
      }

      public static int SignedLOWORD(int n)
      {
        return (int) (short) (n & (int) ushort.MaxValue);
      }

      public static int SignedLOWORD(IntPtr n)
      {
        return NativeMethods.Util.SignedLOWORD((int) (long) n);
      }

      public static int LowOrder(int param)
      {
        return param & (int) ushort.MaxValue;
      }

      public static int HighOrder(int param)
      {
        return param >> 16 & (int) ushort.MaxValue;
      }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private class MEMORYSTATUSEX
    {
      public uint dwLength;
      public uint dwMemoryLoad;
      public ulong ullTotalPhys;
      public ulong ullAvailPhys;
      public ulong ullTotalPageFile;
      public ulong ullAvailPageFile;
      public ulong ullTotalVirtual;
      public ulong ullAvailVirtual;
      public ulong ullAvailExtendedVirtual;

      public MEMORYSTATUSEX()
      {
        this.dwLength = (uint) Marshal.SizeOf(typeof (NativeMethods.MEMORYSTATUSEX));
      }
    }

    public enum TernaryRasterOperations
    {
      BLACKNESS = 66, // 0x00000042
      NOTSRCERASE = 1114278, // 0x001100A6
      NOTSRCCOPY = 3342344, // 0x00330008
      SRCERASE = 4457256, // 0x00440328
      DSTINVERT = 5570569, // 0x00550009
      PATINVERT = 5898313, // 0x005A0049
      SRCINVERT = 6684742, // 0x00660046
      SRCAND = 8913094, // 0x008800C6
      MERGEPAINT = 12255782, // 0x00BB0226
      MERGECOPY = 12583114, // 0x00C000CA
      SRCCOPY = 13369376, // 0x00CC0020
      SRCPAINT = 15597702, // 0x00EE0086
      PATCOPY = 15728673, // 0x00F00021
      PATPAINT = 16452105, // 0x00FB0A09
      WHITENESS = 16711778, // 0x00FF0062
      CAPTUREBLT = 1073741824, // 0x40000000
    }

    public enum ScrollBarDirection
    {
      SB_HORZ,
      SB_VERT,
      SB_CTL,
      SB_BOTH,
    }

    public struct GESTURECONFIG
    {
      public uint dwID;
      public uint dwWant;
      public uint dwBlock;
    }

    private static class DeviceCapsNativeMethods
    {
      public static readonly int LOGPIXELSX = 88;
      public static readonly int LOGPIXELSY = 90;
      public static readonly int VERTRES = 10;
      public static readonly int DESKTOPVERTRES = 117;

      [DllImport("Gdi32.dll")]
      public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

      [DllImport("user32.dll")]
      public static extern IntPtr GetDC(IntPtr hWnd);

      [DllImport("user32.dll")]
      public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    }

    public enum DpiType
    {
      Effective,
      Angular,
      Raw,
    }

    [Flags]
    public enum AnimateWindowFlags
    {
      AW_HOR_POSITIVE = 1,
      AW_HOR_NEGATIVE = 2,
      AW_VER_POSITIVE = 4,
      AW_VER_NEGATIVE = 8,
      AW_CENTER = 16, // 0x00000010
      AW_HIDE = 65536, // 0x00010000
      AW_ACTIVATE = 131072, // 0x00020000
      AW_SLIDE = 262144, // 0x00040000
      AW_BLEND = 524288, // 0x00080000
    }

    public enum GetWindow_Cmd : uint
    {
      GW_HWNDFIRST,
      GW_HWNDLAST,
      GW_HWNDNEXT,
      GW_HWNDPREV,
      GW_OWNER,
      GW_CHILD,
      GW_ENABLEDPOPUP,
    }

    public delegate int FontEnumDelegate(
      [MarshalAs(UnmanagedType.Struct)] ref NativeMethods.EnumLogFont lpelf,
      [MarshalAs(UnmanagedType.Struct)] ref NativeMethods.NewTextMetric lpntm,
      int fontType,
      int lParam);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct EnumLogFont
    {
      public NativeMethods.LOGFONT elfLogFont;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
      public char[] elfFullName;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
      public char[] elfStyle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class GlyphSet
    {
      public int cbThis;
      public int flAccel;
      public int cGlyphsSupported;
      public int cRanges;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20000)]
      public byte[] ranges;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct NewTextMetric
    {
      public long tmHeight;
      public long tmAscent;
      public long tmDescent;
      public long tmInternalLeading;
      public long tmExternalLeading;
      public long tmAvecharWidth;
      public long tmMaxcharWidth;
      public long tmWeight;
      public long tmOverhang;
      public long tmDigitizedAspectX;
      public long tmDigitizedAspectY;
      public char tmFirstchar;
      public char tmLastchar;
      public char tmDefaultchar;
      public char tmBreakchar;
      public byte tmItalic;
      public byte tmUnderlined;
      public byte tmStruckOut;
      public byte tmPitchAndFamily;
      public byte tmcharSet;
      public ulong ntmFlags;
      public int ntmSizeEM;
      public int ntmCellHeight;
      public int ntmAvgWidth;
    }

    public enum GdiDcObject
    {
      Pen = 1,
      Brush = 2,
      Pal = 5,
      Font = 6,
      Bitmap = 7,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct TextMetric
    {
      public int tmHeight;
      public int tmAscent;
      public int tmDescent;
      public int tmInternalLeading;
      public int tmExternalLeading;
      public int tmAveCharWidth;
      public int tmMaxCharWidth;
      public int tmWeight;
      public int tmOverhang;
      public int tmDigitizedAspectX;
      public int tmDigitizedAspectY;
      public char tmFirstChar;
      public char tmLastChar;
      public char tmDefaultChar;
      public char tmBreakChar;
      public byte tmItalic;
      public byte tmUnderlined;
      public byte tmStruckOut;
      public byte tmPitchAndFamily;
      public byte tmCharSet;
    }
  }
}
