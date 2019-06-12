// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.StrongEncryptionExtraField
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class StrongEncryptionExtraField : ExtraFieldBase
  {
    public ushort Format { get; set; }

    public ushort AlgorithmId { get; set; }

    public ushort KeyLength { get; set; }

    public ushort Flags { get; set; }

    protected override ushort HeaderId
    {
      get
      {
        return 23;
      }
    }

    protected override byte[] GetBlock()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          binaryWriter.Write(this.Format);
          binaryWriter.Write(this.AlgorithmId);
          binaryWriter.Write(this.KeyLength);
          binaryWriter.Write(this.Flags);
          return memoryStream.ToArray();
        }
      }
    }

    protected override void ParseBlock(byte[] extraData)
    {
      this.Format = BitConverter.ToUInt16(extraData, 0);
      this.AlgorithmId = BitConverter.ToUInt16(extraData, 2);
      this.KeyLength = BitConverter.ToUInt16(extraData, 4);
      this.Flags = BitConverter.ToUInt16(extraData, 6);
    }
  }
}
