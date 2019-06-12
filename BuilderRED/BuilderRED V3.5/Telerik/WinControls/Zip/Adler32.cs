// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.Adler32
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal class Adler32 : IChecksumAlgorithm
  {
    private const uint Base = 65521;
    private const int MaxIterations = 5552;

    public uint UpdateChecksum(uint checksum, byte[] buffer, int offset, int length)
    {
      if (checksum == 1U || buffer == null)
        checksum = 1U;
      if (buffer != null)
      {
        OperationStream.ValidateBufferParameters(buffer, offset, length, true);
        uint num1 = checksum & (uint) ushort.MaxValue;
        uint num2 = checksum >> 16 & (uint) ushort.MaxValue;
        while (length > 0)
        {
          int num3 = length < 5552 ? length : 5552;
          length -= num3;
          for (int index = 0; index < num3; ++index)
          {
            num1 += (uint) buffer[offset++] & (uint) byte.MaxValue;
            num2 += num1;
          }
          num1 %= 65521U;
          num2 %= 65521U;
        }
        checksum = num2 << 16 | num1;
      }
      return checksum;
    }
  }
}
