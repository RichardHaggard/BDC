// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.AesEncryptionExtraField
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class AesEncryptionExtraField : ExtraFieldBase
  {
    public ushort VendorVersion { get; set; }

    public ushort Signature { get; set; }

    public byte KeyLength { get; set; }

    public ushort Method { get; set; }

    protected override ushort HeaderId
    {
      get
      {
        return 39169;
      }
    }

    protected override byte[] GetBlock()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          binaryWriter.Write(this.VendorVersion);
          binaryWriter.Write(this.Signature);
          binaryWriter.Write(this.KeyLength);
          binaryWriter.Write(this.Method);
          return memoryStream.ToArray();
        }
      }
    }

    protected override void ParseBlock(byte[] extraData)
    {
      this.VendorVersion = BitConverter.ToUInt16(extraData, 0);
      this.Signature = BitConverter.ToUInt16(extraData, 2);
      this.KeyLength = extraData[4];
      this.Method = BitConverter.ToUInt16(extraData, 5);
    }
  }
}
