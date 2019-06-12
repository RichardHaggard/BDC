// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.CentralDirectoryHeader
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class CentralDirectoryHeader : FileHeaderBase, ISpecData
  {
    public const uint Signature = 33639248;
    public const int StaticBlockLength = 42;

    public byte VersionMadeBy { get; set; }

    public byte OsCompatibility { get; set; }

    public ushort DiskNumberStart { get; set; }

    public ushort InternalAttributes { get; set; }

    public uint ExternalAttributes { get; set; }

    public uint LocalHeaderOffset { get; set; }

    public byte[] FileComment { get; set; }

    public bool TryReadBlock(BinaryReader reader)
    {
      if (reader.ReadUInt32() != 33639248U)
        return false;
      this.VersionMadeBy = reader.ReadByte();
      this.OsCompatibility = reader.ReadByte();
      this.ReadFields(reader);
      int count = (int) reader.ReadInt16();
      this.DiskNumberStart = reader.ReadUInt16();
      this.InternalAttributes = reader.ReadUInt16();
      this.ExternalAttributes = reader.ReadUInt32();
      this.LocalHeaderOffset = reader.ReadUInt32();
      this.ReadExtraData(reader);
      this.FileComment = reader.ReadBytes(count);
      return true;
    }

    public void WriteBlock(BinaryWriter writer)
    {
      writer.Write(33639248U);
      writer.Write(this.VersionMadeBy);
      writer.Write(this.OsCompatibility);
      this.WriteFields(writer);
      writer.Write(this.FileComment != null ? (ushort) this.FileComment.Length : (ushort) 0);
      writer.Write(this.DiskNumberStart);
      writer.Write(this.InternalAttributes);
      writer.Write(this.ExternalAttributes);
      writer.Write(this.LocalHeaderOffset);
      this.WriteExtraData(writer);
      if (this.FileComment == null)
        return;
      writer.Write(this.FileComment);
    }

    protected override FileHeaderInfoBase GetHeaderInfo()
    {
      return (FileHeaderInfoBase) new CentralDirectoryHeaderInfo(this);
    }
  }
}
