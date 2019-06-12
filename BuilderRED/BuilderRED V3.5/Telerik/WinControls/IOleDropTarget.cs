// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IOleDropTarget
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  [Guid("00000122-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IOleDropTarget
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleDragEnter([MarshalAs(UnmanagedType.Interface), In] object pDataObj, [MarshalAs(UnmanagedType.U4), In] int grfKeyState, [MarshalAs(UnmanagedType.U8), In] long pt, [In, Out] ref int pdwEffect);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleDragOver([MarshalAs(UnmanagedType.U4), In] int grfKeyState, [MarshalAs(UnmanagedType.U8), In] long pt, [In, Out] ref int pdwEffect);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleDragLeave();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleDrop([MarshalAs(UnmanagedType.Interface), In] object pDataObj, [MarshalAs(UnmanagedType.U4), In] int grfKeyState, [MarshalAs(UnmanagedType.U8), In] long pt, [In, Out] ref int pdwEffect);
  }
}
