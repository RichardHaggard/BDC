// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.Zip64EndOfCentralDirectoryRecord
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class Zip64EndOfCentralDirectoryRecord : ISpecData
  {
    public const uint Signature = 101075792;

    public ulong SizeOfZip64EndOfCentralDirectoryRecord { get; set; }

    public byte VersionMadeBy { get; set; }

    public byte OsCompatibility { get; set; }

    public ushort VersionNeededToExtract { get; set; }

    public uint NumberOfThisDisk { get; set; }

    public uint NumberOfTheDiskWithTheStartOfTheCentralDirectory { get; set; }

    public ulong NumberOfEntriesInTheCentralDirectoryOnThisDisk { get; set; }

    public ulong NumberOfEntriesInTheCentralDirectory { get; set; }

    public ulong SizeOfCentralDirectory { get; set; }

    public ulong OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber { get; set; }

    public bool TryReadBlock(BinaryReader reader)
    {
      if (reader.ReadUInt32() != 101075792U)
        return false;
      this.SizeOfZip64EndOfCentralDirectoryRecord = reader.ReadUInt64();
      this.VersionMadeBy = reader.ReadByte();
      this.OsCompatibility = reader.ReadByte();
      this.VersionNeededToExtract = reader.ReadUInt16();
      this.NumberOfThisDisk = reader.ReadUInt32();
      this.NumberOfTheDiskWithTheStartOfTheCentralDirectory = reader.ReadUInt32();
      this.NumberOfEntriesInTheCentralDirectoryOnThisDisk = reader.ReadUInt64();
      this.NumberOfEntriesInTheCentralDirectory = reader.ReadUInt64();
      this.SizeOfCentralDirectory = reader.ReadUInt64();
      this.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber = reader.ReadUInt64();
      return true;
    }

    public void WriteBlock(BinaryWriter writer)
    {
      byte[] block = this.GetBlock();
      this.SizeOfZip64EndOfCentralDirectoryRecord = (ulong) block.Length;
      writer.Write(101075792U);
      writer.Write(this.SizeOfZip64EndOfCentralDirectoryRecord);
      writer.Write(block);
    }

    private byte[] GetBlock()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          binaryWriter.Write(this.VersionMadeBy);
          binaryWriter.Write(this.OsCompatibility);
          binaryWriter.Write(this.VersionNeededToExtract);
          binaryWriter.Write(this.NumberOfThisDisk);
          binaryWriter.Write(this.NumberOfTheDiskWithTheStartOfTheCentralDirectory);
          binaryWriter.Write(this.NumberOfEntriesInTheCentralDirectoryOnThisDisk);
          binaryWriter.Write(this.NumberOfEntriesInTheCentralDirectory);
          binaryWriter.Write(this.SizeOfCentralDirectory);
          binaryWriter.Write(this.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber);
          return memoryStream.ToArray();
        }
      }
    }
  }
}
