// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.Zip64DataDescriptorBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class Zip64DataDescriptorBase
  {
    public uint Crc { get; set; }

    public ulong CompressedSize { get; set; }

    public ulong UncompressedSize { get; set; }

    protected virtual void ReadFields(BinaryReader reader)
    {
      this.Crc = reader.ReadUInt32();
      this.ReadSize(reader);
    }

    protected void ReadSize(BinaryReader reader)
    {
      this.CompressedSize = reader.ReadUInt64();
      this.UncompressedSize = reader.ReadUInt64();
    }

    protected virtual void WriteFields(BinaryWriter writer)
    {
      writer.Write(this.Crc);
      writer.Write(this.CompressedSize);
      writer.Write(this.UncompressedSize);
    }
  }
}
