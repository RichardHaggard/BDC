// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.Zip64EndOfCentralDirectoryLocator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class Zip64EndOfCentralDirectoryLocator : ISpecData
  {
    public const uint Signature = 117853008;
    public const int StaticBlockLength = 16;

    public uint NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDirectory { get; set; }

    public ulong RelativeOffsetOfTheZip64EndOfCentralDirectoryRecord { get; set; }

    public uint NumberOfDisks { get; set; }

    public bool TryReadBlock(BinaryReader reader)
    {
      if (reader.ReadUInt32() != 117853008U)
        return false;
      this.NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDirectory = reader.ReadUInt32();
      this.RelativeOffsetOfTheZip64EndOfCentralDirectoryRecord = reader.ReadUInt64();
      this.NumberOfDisks = reader.ReadUInt32();
      return true;
    }

    public void WriteBlock(BinaryWriter writer)
    {
      writer.Write(117853008U);
      writer.Write(this.NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDirectory);
      writer.Write(this.RelativeOffsetOfTheZip64EndOfCentralDirectoryRecord);
      writer.Write(this.NumberOfDisks);
    }
  }
}
