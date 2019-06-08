// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.FileHeaderInfoBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal abstract class FileHeaderInfoBase
  {
    public FileHeaderInfoBase(FileHeaderBase localFileHeader)
    {
      this.ExtraFieldsData = localFileHeader.ExtraFieldsData;
      this.UncompressedSizeOverflow = localFileHeader.UncompressedSize == uint.MaxValue;
      this.CompressedSizeOverflow = localFileHeader.CompressedSize == uint.MaxValue;
    }

    public byte[] ExtraFieldsData { get; private set; }

    public bool UncompressedSizeOverflow { get; private set; }

    public bool CompressedSizeOverflow { get; private set; }
  }
}
