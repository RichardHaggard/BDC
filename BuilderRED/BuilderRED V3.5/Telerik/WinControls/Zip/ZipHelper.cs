// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.ZipHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;

namespace Telerik.WinControls.Zip
{
  internal static class ZipHelper
  {
    internal static readonly DateTime InvalidDateTime = new DateTime(1980, 1, 1, 0, 0, 0, 0);

    internal static void CopyStream(Stream input, Stream output, long bytes)
    {
      int length = 32768;
      int count;
      for (byte[] buffer = new byte[length]; bytes > 0L && (count = input.Read(buffer, 0, (int) Math.Min((long) length, bytes))) > 0; bytes -= (long) count)
        output.Write(buffer, 0, count);
    }

    internal static uint DateTimeToPacked(DateTime time)
    {
      return (uint) (ushort) (time.Day & 31 | time.Month << 5 & 480 | time.Year - 1980 << 9 & 65024) << 16 | (uint) (ushort) (time.Second / 2 & 31 | time.Minute << 5 & 2016 | time.Hour << 11 & 63488);
    }

    internal static ICompressionAlgorithm GetCompressionAlgorithm(
      CompressionSettings settings)
    {
      ICompressionAlgorithm compressionAlgorithm;
      switch (settings.Method)
      {
        case CompressionMethod.Stored:
          compressionAlgorithm = (ICompressionAlgorithm) new StoreAlgorithm();
          break;
        case CompressionMethod.Deflate:
          compressionAlgorithm = (ICompressionAlgorithm) new DeflateAlgorithm();
          break;
        case CompressionMethod.Lzma:
          compressionAlgorithm = (ICompressionAlgorithm) new LzmaAlgorithm();
          break;
        default:
          throw new NotSupportedException(string.Format("Compression method {0} is not supported.", (object) settings));
      }
      compressionAlgorithm?.Initialize(settings);
      return compressionAlgorithm;
    }

    internal static CompressionSettings GetCompressionSettings(
      CompressionMethod method,
      CompressionSettings baseSettings)
    {
      CompressionSettings compressionSettings;
      switch (method)
      {
        case CompressionMethod.Stored:
          compressionSettings = (CompressionSettings) new StoreSettings();
          break;
        case CompressionMethod.Deflate:
          compressionSettings = (CompressionSettings) new DeflateSettings();
          break;
        case CompressionMethod.Lzma:
          compressionSettings = (CompressionSettings) new LzmaSettings();
          break;
        default:
          throw new NotSupportedException(string.Format("Compression method {0} is not supported.", (object) method));
      }
      if (baseSettings != null && (object) compressionSettings.GetType() == (object) baseSettings.GetType())
        compressionSettings.CopyFrom(baseSettings);
      return compressionSettings;
    }

    internal static bool EndsWithDirChar(string path)
    {
      string str = path.Trim();
      if (!str.EndsWith(PlatformSettings.Manager.DirectorySeparatorChar))
        return str.EndsWith(PlatformSettings.Manager.AltDirectorySeparatorChar);
      return true;
    }

    internal static bool IsCompressionMethodSupported(CompressionMethod method)
    {
      if (method != CompressionMethod.Deflate && method != CompressionMethod.Stored)
        return method == CompressionMethod.Lzma;
      return true;
    }

    internal static DateTime PackedToDateTime(uint packedDateTime)
    {
      if (packedDateTime == (uint) ushort.MaxValue || packedDateTime == 0U)
        return new DateTime(1995, 1, 1, 0, 0, 0, 0);
      ushort num1 = (ushort) (packedDateTime & (uint) ushort.MaxValue);
      ushort num2 = (ushort) ((packedDateTime & 4294901760U) >> 16);
      int year = 1980 + (((int) num2 & 65024) >> 9);
      int month = ((int) num2 & 480) >> 5;
      int day = (int) num2 & 31;
      int hour = ((int) num1 & 63488) >> 11;
      int minute = ((int) num1 & 2016) >> 5;
      int second = ((int) num1 & 31) * 2;
      if (second >= 60)
      {
        minute += second / 60;
        second %= 60;
      }
      if (minute >= 60)
      {
        hour += minute / 60;
        minute %= 60;
      }
      if (hour >= 24)
      {
        day += hour / 24;
        hour %= 24;
      }
      DateTime now = DateTime.Now;
      try
      {
        return new DateTime(year, month, day, hour, minute, second, 0);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        return ZipHelper.InvalidDateTime;
      }
      catch (ArgumentException ex)
      {
        return ZipHelper.InvalidDateTime;
      }
    }

    internal static void ReadBytes(Stream stream, byte[] buffer, int bytesToRead)
    {
      int offset = 0;
      int num;
      for (; bytesToRead > 0; bytesToRead -= num)
      {
        num = stream.Read(buffer, offset, bytesToRead);
        if (num == 0)
          throw new IOException("Unexpected End Of Stream");
        offset += num;
      }
    }

    internal static bool SeekBackwardsToSignature(Stream stream, uint signatureToFind)
    {
      int bufferPointer = 0;
      uint num = 0;
      byte[] buffer = new byte[32];
      bool flag1 = false;
      bool flag2 = false;
label_6:
      while (!flag2 && !flag1)
      {
        flag1 = ZipHelper.SeekBackwardsAndRead(stream, buffer, out bufferPointer);
        while (true)
        {
          if (bufferPointer >= 0 && !flag2)
          {
            num = num << 8 | (uint) buffer[bufferPointer];
            if ((int) num != (int) signatureToFind)
              --bufferPointer;
            else
              flag2 = true;
          }
          else
            goto label_6;
        }
      }
      if (!flag2)
        return false;
      stream.Seek((long) bufferPointer, SeekOrigin.Current);
      return true;
    }

    private static bool SeekBackwardsAndRead(Stream stream, byte[] buffer, out int bufferPointer)
    {
      if (stream.Position < (long) buffer.Length)
      {
        int position = (int) stream.Position;
        stream.Seek(0L, SeekOrigin.Begin);
        ZipHelper.ReadBytes(stream, buffer, position);
        stream.Seek(0L, SeekOrigin.Begin);
        bufferPointer = position - 1;
        return true;
      }
      stream.Seek((long) -buffer.Length, SeekOrigin.Current);
      ZipHelper.ReadBytes(stream, buffer, buffer.Length);
      stream.Seek((long) -buffer.Length, SeekOrigin.Current);
      bufferPointer = buffer.Length - 1;
      return false;
    }
  }
}
