// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.Checksum.Crc32Wrapper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip.Checksum
{
  public class Crc32Wrapper
  {
    private Crc32 wrapped = new Crc32();

    public long CrcValue { get; set; }

    public long UpdateValue(long value, byte[] buffer, int index, int length)
    {
      this.CrcValue = (long) this.wrapped.UpdateChecksum((uint) value, buffer, index, length);
      return this.CrcValue;
    }
  }
}
