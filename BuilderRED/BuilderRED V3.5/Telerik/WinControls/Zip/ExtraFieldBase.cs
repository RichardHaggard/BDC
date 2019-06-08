// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.ExtraFieldBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Telerik.WinControls.Zip
{
  internal abstract class ExtraFieldBase
  {
    public ExtraFieldType ExtraFieldType
    {
      get
      {
        return ExtraFieldBase.GetExtraFieldType(this.HeaderId);
      }
    }

    protected abstract ushort HeaderId { get; }

    public static IEnumerable<ExtraFieldBase> GetExtraFields(
      FileHeaderInfoBase headerInfo)
    {
      MemoryStream stream = new MemoryStream(headerInfo.ExtraFieldsData);
      using (BinaryReader reader = new BinaryReader((Stream) stream))
      {
        while (stream.Position < stream.Length)
        {
          ExtraFieldBase field = ExtraFieldBase.GetExtraField(headerInfo, reader);
          yield return field;
        }
      }
    }

    public static byte[] GetExtraFieldsData(IEnumerable<ExtraFieldBase> fields)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          foreach (ExtraFieldBase field in fields)
          {
            binaryWriter.Write(field.HeaderId);
            byte[] block = field.GetBlock();
            binaryWriter.Write((ushort) block.Length);
            binaryWriter.Write(block);
          }
          return memoryStream.ToArray();
        }
      }
    }

    protected abstract void ParseBlock(byte[] extraData);

    protected abstract byte[] GetBlock();

    private static ExtraFieldBase GetExtraField(
      FileHeaderInfoBase headerInfo,
      BinaryReader reader)
    {
      ushort num1 = reader.ReadUInt16();
      ExtraFieldType extraFieldType = ExtraFieldBase.GetExtraFieldType(num1);
      ushort num2 = reader.ReadUInt16();
      byte[] extraData = reader.ReadBytes((int) num2);
      ExtraFieldBase extraFieldBase;
      switch (extraFieldType)
      {
        case ExtraFieldType.Zip64:
          extraFieldBase = (ExtraFieldBase) new Zip64ExtraField(headerInfo);
          break;
        case ExtraFieldType.AesEncryption:
          extraFieldBase = (ExtraFieldBase) new AesEncryptionExtraField();
          break;
        default:
          extraFieldBase = (ExtraFieldBase) new UnknownExtraField(num1);
          break;
      }
      extraFieldBase.ParseBlock(extraData);
      return extraFieldBase;
    }

    private static ExtraFieldType GetExtraFieldType(ushort headerID)
    {
      if (Enum.IsDefined(typeof (ExtraFieldType), (object) (int) headerID))
        return (ExtraFieldType) headerID;
      return ExtraFieldType.Unknown;
    }
  }
}
