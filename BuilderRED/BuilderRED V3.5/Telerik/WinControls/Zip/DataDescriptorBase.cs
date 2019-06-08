// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DataDescriptorBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class DataDescriptorBase
  {
    public uint Crc { get; set; }

    public uint CompressedSize { get; set; }

    public uint UncompressedSize { get; set; }

    protected virtual void ReadFields(BinaryReader reader)
    {
      this.Crc = reader.ReadUInt32();
      this.ReadSize(reader);
    }

    protected void ReadSize(BinaryReader reader)
    {
      this.CompressedSize = reader.ReadUInt32();
      this.UncompressedSize = reader.ReadUInt32();
    }

    protected virtual void WriteFields(BinaryWriter writer)
    {
      writer.Write(this.Crc);
      writer.Write(this.CompressedSize);
      writer.Write(this.UncompressedSize);
    }
  }
}
