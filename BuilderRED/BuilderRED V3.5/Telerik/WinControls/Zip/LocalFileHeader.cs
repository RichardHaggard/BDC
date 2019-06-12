// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LocalFileHeader
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class LocalFileHeader : FileHeaderBase, ISpecData
  {
    public const uint Signature = 67324752;
    public const int StaticBlockLength = 26;

    public LocalFileHeader()
    {
    }

    internal LocalFileHeader(FileHeaderBase fileHeader)
    {
      this.FromFileHeader(fileHeader);
    }

    public bool TryReadBlock(BinaryReader reader)
    {
      if (reader.ReadUInt32() != 67324752U)
        return false;
      this.ReadFields(reader);
      this.ReadExtraData(reader);
      return true;
    }

    public void WriteBlock(BinaryWriter writer)
    {
      writer.Write(67324752U);
      this.WriteFields(writer);
      this.WriteExtraData(writer);
    }

    protected override FileHeaderInfoBase GetHeaderInfo()
    {
      return (FileHeaderInfoBase) new LocalFileHeaderInfo(this);
    }
  }
}
