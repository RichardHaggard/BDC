// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DataDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class DataDescriptor : DataDescriptorBase, ISpecData
  {
    public const uint Signature = 134695760;
    public const int StaticBlockLength = 12;
    private bool useZip64;

    public ulong CompressedSizeZip64 { get; set; }

    public ulong UncompressedSizeZip64 { get; set; }

    public bool TryReadBlock(BinaryReader reader)
    {
      byte[] buffer = new byte[4];
      ZipHelper.ReadBytes(reader.BaseStream, buffer, buffer.Length);
      uint uint32 = BitConverter.ToUInt32(buffer, 0);
      if (uint32 == 134695760U)
      {
        this.ReadFields(reader);
      }
      else
      {
        this.Crc = uint32;
        this.ReadSize(reader);
      }
      return true;
    }

    public void WriteBlock(BinaryWriter writer)
    {
      writer.Write(134695760U);
      if (!this.useZip64)
      {
        this.WriteFields(writer);
      }
      else
      {
        writer.Write(this.Crc);
        writer.Write(this.CompressedSizeZip64);
        writer.Write(this.UncompressedSizeZip64);
      }
    }

    internal static DataDescriptor FromFileHeader(FileHeaderBase fileHeader)
    {
      DataDescriptor dataDescriptor1 = new DataDescriptor();
      dataDescriptor1.CompressedSize = fileHeader.CompressedSize;
      dataDescriptor1.Crc = fileHeader.Crc;
      dataDescriptor1.UncompressedSize = fileHeader.UncompressedSize;
      DataDescriptor dataDescriptor2 = dataDescriptor1;
      Zip64ExtraField zip64ExtraField = (Zip64ExtraField) null;
      foreach (ExtraFieldBase extraField in fileHeader.ExtraFields)
      {
        zip64ExtraField = extraField as Zip64ExtraField;
        if (zip64ExtraField != null)
          break;
      }
      if (zip64ExtraField != null)
      {
        dataDescriptor2.useZip64 = true;
        dataDescriptor2.CompressedSizeZip64 = !zip64ExtraField.CompressedSize.HasValue ? (ulong) fileHeader.CompressedSize : zip64ExtraField.CompressedSize.Value;
        dataDescriptor2.UncompressedSizeZip64 = !zip64ExtraField.OriginalSize.HasValue ? (ulong) fileHeader.UncompressedSize : zip64ExtraField.OriginalSize.Value;
      }
      return dataDescriptor2;
    }
  }
}
