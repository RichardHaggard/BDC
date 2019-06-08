// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.ZipOutputStream
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.IO;

namespace Telerik.WinControls.Zip
{
  [Obsolete("This class has been deprecated. Use CompressedStream instead of ZipOutputStream.")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class ZipOutputStream : CompressedStream
  {
    private byte[] rb = new byte[1];

    public ZipOutputStream(Stream baseStream)
      : this(baseStream, CompressionMethod.Deflate)
    {
    }

    public ZipOutputStream(Stream baseStream, CompressionMethod method)
      : base(baseStream, StreamOperationMode.Write, ZipHelper.GetCompressionSettings(method, (CompressionSettings) null), false, (EncryptionSettings) null)
    {
    }

    public ZipOutputStream(Stream baseStream, ZipCompression compressionLevel)
      : base(baseStream, StreamOperationMode.Write, (CompressionSettings) ZipOutputStream.CreateDeflateSettings(compressionLevel), false, (EncryptionSettings) null)
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

    public override void WriteByte(byte value)
    {
      this.rb[0] = value;
      this.Write(this.rb, 0, 1);
    }

    internal static DeflateSettings CreateDeflateSettings(
      ZipCompression compressionLevel)
    {
      return new DeflateSettings() { CompressionLevel = compressionLevel == ZipCompression.Default ? CompressionLevel.Optimal : (CompressionLevel) compressionLevel };
    }
  }
}
