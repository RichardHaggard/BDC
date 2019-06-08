// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TimeZoneUnsafeNativeMethods
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;

namespace Telerik.WinControls
{
  internal static class TimeZoneUnsafeNativeMethods
  {
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int GetDynamicTimeZoneInformation(
      out TimeZoneNativeMethods.DynamicTimeZoneInformation lpDynamicTimeZoneInformation);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int GetTimeZoneInformation(
      out TimeZoneNativeMethods.TimeZoneInformation lpTimeZoneInformation);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetFileMUIPath(
      int flags,
      [MarshalAs(UnmanagedType.LPWStr)] string filePath,
      [MarshalAs(UnmanagedType.LPWStr)] StringBuilder language,
      ref int languageLength,
      [MarshalAs(UnmanagedType.LPWStr)] StringBuilder fileMuiPath,
      ref int fileMuiPathLength,
      ref long enumerator);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern SafeLibraryHandle LoadLibraryEx(
      string libFilename,
      IntPtr reserved,
      int flags);

    [DllImport("user32.dll", EntryPoint = "LoadStringW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    public static extern int LoadString(
      SafeLibraryHandle handle,
      int id,
      StringBuilder buffer,
      int bufferLength);
  }
}
