// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.FileHeaderBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Telerik.WinControls.Zip
{
  internal abstract class FileHeaderBase : DataDescriptorBase
  {
    private ushort extraFieldLength;
    private ushort fileNameLength;

    public FileHeaderBase()
    {
      this.ExtraFields = new List<ExtraFieldBase>();
    }

    public ushort VersionNeededToExtract { get; set; }

    public ushort GeneralPurposeBitFlag { get; set; }

    public ushort CompressionMethod { get; set; }

    public uint FileTime { get; set; }

    public byte[] FileName { get; set; }

    public byte[] ExtraFieldsData { get; set; }

    public List<ExtraFieldBase> ExtraFields { get; set; }

    internal void FromFileHeader(FileHeaderBase fileHeader)
    {
      this.CompressedSize = fileHeader.CompressedSize;
      this.CompressionMethod = fileHeader.CompressionMethod;
      this.UncompressedSize = fileHeader.UncompressedSize;
      this.Crc = fileHeader.Crc;
      if (fileHeader.ExtraFieldsData != null)
      {
        this.ExtraFieldsData = new byte[fileHeader.ExtraFieldsData.Length];
        Array.Copy((Array) fileHeader.ExtraFieldsData, (Array) this.ExtraFieldsData, fileHeader.ExtraFieldsData.Length);
        this.ExtraFields = new List<ExtraFieldBase>(ExtraFieldBase.GetExtraFields(this.GetHeaderInfo()));
      }
      if (fileHeader.FileName != null)
      {
        this.FileName = new byte[fileHeader.FileName.Length];
        Array.Copy((Array) fileHeader.FileName, (Array) this.FileName, fileHeader.FileName.Length);
      }
      this.FileTime = fileHeader.FileTime;
      this.GeneralPurposeBitFlag = fileHeader.GeneralPurposeBitFlag;
      this.VersionNeededToExtract = fileHeader.VersionNeededToExtract;
    }

    protected abstract FileHeaderInfoBase GetHeaderInfo();

    protected override void ReadFields(BinaryReader reader)
    {
      this.VersionNeededToExtract = reader.ReadUInt16();
      this.GeneralPurposeBitFlag = reader.ReadUInt16();
      this.CompressionMethod = reader.ReadUInt16();
      this.FileTime = reader.ReadUInt32();
      base.ReadFields(reader);
      this.fileNameLength = reader.ReadUInt16();
      this.extraFieldLength = reader.ReadUInt16();
    }

    protected void ReadExtraData(BinaryReader reader)
    {
      this.FileName = reader.ReadBytes((int) this.fileNameLength);
      this.ExtraFieldsData = reader.ReadBytes((int) this.extraFieldLength);
      this.ExtraFields = new List<ExtraFieldBase>(ExtraFieldBase.GetExtraFields(this.GetHeaderInfo()));
    }

    protected override void WriteFields(BinaryWriter writer)
    {
      writer.Write(this.VersionNeededToExtract);
      writer.Write(this.GeneralPurposeBitFlag);
      writer.Write(this.CompressionMethod);
      writer.Write(this.FileTime);
      base.WriteFields(writer);
      writer.Write((ushort) this.FileName.Length);
      if (this.ExtraFields != null)
      {
        this.ExtraFieldsData = ExtraFieldBase.GetExtraFieldsData((IEnumerable<ExtraFieldBase>) this.ExtraFields);
        writer.Write((ushort) this.ExtraFieldsData.Length);
      }
      else
        writer.Write((ushort) 0);
    }

    protected void WriteExtraData(BinaryWriter writer)
    {
      writer.Write(this.FileName);
      if (this.ExtraFieldsData == null)
        return;
      writer.Write(this.ExtraFieldsData);
    }
  }
}
