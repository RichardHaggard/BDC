// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IOleDropSource
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  [Guid("00000121-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IOleDropSource
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleQueryContinueDrag(int fEscapePressed, [MarshalAs(UnmanagedType.U4), In] int grfKeyState);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OleGiveFeedback([MarshalAs(UnmanagedType.U4), In] int dwEffect);
  }
}
