// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.ZipInputStream
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.IO;

namespace Telerik.WinControls.Zip
{
  [Obsolete("This class has been deprecated. Use CompressedStream instead of ZipInputStream.")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class ZipInputStream : CompressedStream
  {
    private byte[] rb = new byte[1];

    public ZipInputStream(Stream baseStream)
      : base(baseStream, StreamOperationMode.Read, (CompressionSettings) new DeflateSettings(), false, (EncryptionSettings) null)
    {
    }

    public new Stream BaseStream
    {
      get
      {
        return base.BaseStream;
      }
    }

    public int UncompressedSize
    {
      get
      {
        return (int) this.TotalPlainCount;
      }
    }

    public override int ReadByte()
    {
      if (this.Read(this.rb, 0, 1) != 0)
        return (int) this.rb[0];
      return -1;
    }
  }
}
