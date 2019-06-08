// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.Zip64ExtraField
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class Zip64ExtraField : ExtraFieldBase
  {
    private readonly FileHeaderInfoBase headerInfo;

    internal Zip64ExtraField(FileHeaderInfoBase headerInfo)
    {
      this.headerInfo = headerInfo;
    }

    public ulong? OriginalSize { get; set; }

    public ulong? CompressedSize { get; set; }

    public ulong? RelativeHeaderOffset { get; set; }

    public uint? DiskStartNumber { get; set; }

    protected override ushort HeaderId
    {
      get
      {
        return 1;
      }
    }

    protected override byte[] GetBlock()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          if (this.OriginalSize.HasValue)
            binaryWriter.Write(this.OriginalSize.Value);
          if (this.CompressedSize.HasValue)
            binaryWriter.Write(this.CompressedSize.Value);
          if (this.RelativeHeaderOffset.HasValue)
            binaryWriter.Write(this.RelativeHeaderOffset.Value);
          if (this.DiskStartNumber.HasValue)
            binaryWriter.Write(this.DiskStartNumber.Value);
          return memoryStream.ToArray();
        }
      }
    }

    protected override void ParseBlock(byte[] extraData)
    {
      int startIndex = 0;
      if (this.headerInfo.UncompressedSizeOverflow)
      {
        this.OriginalSize = new ulong?(BitConverter.ToUInt64(extraData, startIndex));
        startIndex += 8;
      }
      if (this.headerInfo.CompressedSizeOverflow)
      {
        this.CompressedSize = new ulong?(BitConverter.ToUInt64(extraData, startIndex));
        startIndex += 8;
      }
      CentralDirectoryHeaderInfo headerInfo = this.headerInfo as CentralDirectoryHeaderInfo;
      if (headerInfo != null && headerInfo.LocalHeaderOffsetOverflow)
      {
        this.RelativeHeaderOffset = new ulong?(BitConverter.ToUInt64(extraData, startIndex));
        startIndex += 8;
      }
      if (extraData.Length < startIndex + 24)
        return;
      this.DiskStartNumber = new uint?(BitConverter.ToUInt32(extraData, 24));
    }
  }
}
