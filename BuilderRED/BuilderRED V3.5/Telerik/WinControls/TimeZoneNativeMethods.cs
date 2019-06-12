// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TimeZoneNativeMethods
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  internal sealed class TimeZoneNativeMethods
  {
    internal struct SystemTime
    {
      [MarshalAs(UnmanagedType.U2)]
      public short Year;
      [MarshalAs(UnmanagedType.U2)]
      public short Month;
      [MarshalAs(UnmanagedType.U2)]
      public short DayOfWeek;
      [MarshalAs(UnmanagedType.U2)]
      public short Day;
      [MarshalAs(UnmanagedType.U2)]
      public short Hour;
      [MarshalAs(UnmanagedType.U2)]
      public short Minute;
      [MarshalAs(UnmanagedType.U2)]
      public short Second;
      [MarshalAs(UnmanagedType.U2)]
      public short Milliseconds;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct DynamicTimeZoneInformation
    {
      [MarshalAs(UnmanagedType.I4)]
      public int Bias;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string StandardName;
      public TimeZoneNativeMethods.SystemTime StandardDate;
      [MarshalAs(UnmanagedType.I4)]
      public int StandardBias;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string DaylightName;
      public TimeZoneNativeMethods.SystemTime DaylightDate;
      [MarshalAs(UnmanagedType.I4)]
      public int DaylightBias;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      public string TimeZoneKeyName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct TimeZoneInformation
    {
      [MarshalAs(UnmanagedType.I4)]
      public int Bias;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string StandardName;
      public TimeZoneNativeMethods.SystemTime StandardDate;
      [MarshalAs(UnmanagedType.I4)]
      public int StandardBias;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string DaylightName;
      public TimeZoneNativeMethods.SystemTime DaylightDate;
      [MarshalAs(UnmanagedType.I4)]
      public int DaylightBias;

      public TimeZoneInformation(
        TimeZoneNativeMethods.DynamicTimeZoneInformation dtzi)
      {
        this.Bias = dtzi.Bias;
        this.StandardName = dtzi.StandardName;
        this.StandardDate = dtzi.StandardDate;
        this.StandardBias = dtzi.StandardBias;
        this.DaylightName = dtzi.DaylightName;
        this.DaylightDate = dtzi.DaylightDate;
        this.DaylightBias = dtzi.DaylightBias;
      }
    }

    internal struct RegistryTimeZoneInformation
    {
      [MarshalAs(UnmanagedType.I4)]
      public int Bias;
      [MarshalAs(UnmanagedType.I4)]
      public int StandardBias;
      [MarshalAs(UnmanagedType.I4)]
      public int DaylightBias;
      public TimeZoneNativeMethods.SystemTime StandardDate;
      public TimeZoneNativeMethods.SystemTime DaylightDate;

      public RegistryTimeZoneInformation(TimeZoneNativeMethods.TimeZoneInformation tzi)
      {
        this.Bias = tzi.Bias;
        this.StandardDate = tzi.StandardDate;
        this.StandardBias = tzi.StandardBias;
        this.DaylightDate = tzi.DaylightDate;
        this.DaylightBias = tzi.DaylightBias;
      }

      public RegistryTimeZoneInformation(byte[] bytes)
      {
        if (bytes == null || bytes.Length != 44)
          throw new ArgumentException("Invalid format", nameof (bytes));
        this.Bias = BitConverter.ToInt32(bytes, 0);
        this.StandardBias = BitConverter.ToInt32(bytes, 4);
        this.DaylightBias = BitConverter.ToInt32(bytes, 8);
        this.StandardDate.Year = BitConverter.ToInt16(bytes, 12);
        this.StandardDate.Month = BitConverter.ToInt16(bytes, 14);
        this.StandardDate.DayOfWeek = BitConverter.ToInt16(bytes, 16);
        this.StandardDate.Day = BitConverter.ToInt16(bytes, 18);
        this.StandardDate.Hour = BitConverter.ToInt16(bytes, 20);
        this.StandardDate.Minute = BitConverter.ToInt16(bytes, 22);
        this.StandardDate.Second = BitConverter.ToInt16(bytes, 24);
        this.StandardDate.Milliseconds = BitConverter.ToInt16(bytes, 26);
        this.DaylightDate.Year = BitConverter.ToInt16(bytes, 28);
        this.DaylightDate.Month = BitConverter.ToInt16(bytes, 30);
        this.DaylightDate.DayOfWeek = BitConverter.ToInt16(bytes, 32);
        this.DaylightDate.Day = BitConverter.ToInt16(bytes, 34);
        this.DaylightDate.Hour = BitConverter.ToInt16(bytes, 36);
        this.DaylightDate.Minute = BitConverter.ToInt16(bytes, 38);
        this.DaylightDate.Second = BitConverter.ToInt16(bytes, 40);
        this.DaylightDate.Milliseconds = BitConverter.ToInt16(bytes, 42);
      }
    }
  }
}
