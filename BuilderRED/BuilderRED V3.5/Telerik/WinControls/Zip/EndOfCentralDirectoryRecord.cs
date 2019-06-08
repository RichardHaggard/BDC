// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.EndOfCentralDirectoryRecord
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class EndOfCentralDirectoryRecord : ISpecData
  {
    public const uint Signature = 101010256;
    public const int StaticBlockLength = 18;

    public ushort NumberOfThisDisk { get; set; }

    public ushort NumberOfTheDiskWithTheStartOfTheCentralDirectory { get; set; }

    public ushort NumberOfEntriesInTheCentralDirectoryOnThisDisk { get; set; }

    public ushort NumberOfEntriesInTheCentralDirectory { get; set; }

    public uint SizeOfCentralDirectory { get; set; }

    public uint OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber { get; set; }

    public byte[] ArchiveComment { get; set; }

    public bool TryReadBlock(BinaryReader reader)
    {
      if (reader.ReadUInt32() != 101010256U)
        return false;
      this.NumberOfThisDisk = reader.ReadUInt16();
      this.NumberOfTheDiskWithTheStartOfTheCentralDirectory = reader.ReadUInt16();
      this.NumberOfEntriesInTheCentralDirectoryOnThisDisk = reader.ReadUInt16();
      this.NumberOfEntriesInTheCentralDirectory = reader.ReadUInt16();
      this.SizeOfCentralDirectory = reader.ReadUInt32();
      this.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber = reader.ReadUInt32();
      this.ArchiveComment = reader.ReadBytes((int) reader.ReadUInt16());
      return true;
    }

    public void WriteBlock(BinaryWriter writer)
    {
      writer.Write(101010256U);
      writer.Write(this.NumberOfThisDisk);
      writer.Write(this.NumberOfTheDiskWithTheStartOfTheCentralDirectory);
      writer.Write(this.NumberOfEntriesInTheCentralDirectoryOnThisDisk);
      writer.Write(this.NumberOfEntriesInTheCentralDirectory);
      writer.Write(this.SizeOfCentralDirectory);
      writer.Write(this.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber);
      ushort num = this.ArchiveComment != null ? (ushort) this.ArchiveComment.Length : (ushort) 0;
      writer.Write(num);
      if (this.ArchiveComment == null)
        return;
      writer.Write(this.ArchiveComment);
    }
  }
}
