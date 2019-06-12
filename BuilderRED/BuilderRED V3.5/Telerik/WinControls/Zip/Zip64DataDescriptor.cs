// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.Zip64DataDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class Zip64DataDescriptor : Zip64DataDescriptorBase, ISpecData
  {
    public const uint Signature = 134695760;

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
      this.WriteFields(writer);
    }
  }
}
