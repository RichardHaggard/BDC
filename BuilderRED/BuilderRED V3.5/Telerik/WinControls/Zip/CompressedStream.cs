// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.CompressedStream
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;

namespace Telerik.WinControls.Zip
{
  public class CompressedStream : OperationStream
  {
    private ICompressionAlgorithm algorithm;
    private long compressedSize;

    public CompressedStream(
      Stream baseStream,
      StreamOperationMode mode,
      CompressionSettings settings)
      : this(baseStream, mode, settings, true, (EncryptionSettings) null)
    {
    }

    public CompressedStream(
      Stream baseStream,
      StreamOperationMode mode,
      CompressionSettings settings,
      bool useCrc32,
      EncryptionSettings encryptionSettings)
      : base(baseStream, mode)
    {
      baseStream = encryptionSettings != null ? (Stream) new CryptoStream(baseStream, mode, PlatformSettings.Manager.GetCryptoProvider(encryptionSettings)) : baseStream;
      ICompressionAlgorithm compressionAlgorithm = ZipHelper.GetCompressionAlgorithm(settings);
      IChecksumAlgorithm checksumAlgorithm = useCrc32 ? (IChecksumAlgorithm) new Crc32() : (IChecksumAlgorithm) new Adler32();
      this.Initialize(baseStream, compressionAlgorithm, checksumAlgorithm);
    }

    internal CompressedStream(
      Stream baseStream,
      StreamOperationMode mode,
      ICompressionAlgorithm compressionAlgorithm,
      IChecksumAlgorithm checksumAlgorithm)
      : base(baseStream, mode)
    {
      if (compressionAlgorithm == null)
        throw new ArgumentNullException(nameof (compressionAlgorithm));
      this.Initialize(baseStream, compressionAlgorithm, checksumAlgorithm);
    }

    public event EventHandler ChecksumReady;

    public long Checksum { get; private set; }

    public long CompressedSize
    {
      get
      {
        if (this.IsDisposed)
          return this.compressedSize;
        CryptoStream baseStream = this.BaseStream as CryptoStream;
        if (baseStream != null)
          return baseStream.TotalTransformedCount + (baseStream.Transform == null || baseStream.Transform.Header.Buffer == null ? 0L : (long) baseStream.Transform.Header.Buffer.Length);
        return this.TotalTransformedCount + (this.Transform == null || this.Transform.Header.Buffer == null || !this.Transform.Header.CountHeaderInCompressedSize ? 0L : (long) this.Transform.Header.Buffer.Length);
      }
    }

    internal IChecksumAlgorithm ChecksumAlgorithm { get; set; }

    public override int Read(byte[] buffer, int offset, int count)
    {
      int length = base.Read(buffer, offset, count);
      if (length != 0)
      {
        if (this.ChecksumAlgorithm != null)
          this.Checksum = (long) this.ChecksumAlgorithm.UpdateChecksum((uint) this.Checksum, buffer, offset, length);
      }
      else if (this.ChecksumReady != null)
        this.ChecksumReady((object) this, EventArgs.Empty);
      return length;
    }

    public override void SetLength(long value)
    {
      if (this.Mode != StreamOperationMode.Read)
        throw new NotSupportedException();
      (this.BaseStream as CryptoStream)?.SetLength(value);
      base.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      base.Write(buffer, offset, count);
      if (this.ChecksumAlgorithm == null)
        return;
      this.Checksum = (long) this.ChecksumAlgorithm.UpdateChecksum((uint) this.Checksum, buffer, offset, count);
      if (this.ChecksumReady == null)
        return;
      this.ChecksumReady((object) this, EventArgs.Empty);
    }

    protected override void Dispose(bool disposing)
    {
      if (this.IsDisposed)
        return;
      try
      {
        if (!this.HasFlushedFinalBlock)
          this.FlushFinalBlock();
        this.compressedSize = this.CompressedSize;
      }
      finally
      {
        this.algorithm = (ICompressionAlgorithm) null;
        base.Dispose(disposing);
      }
    }

    private void Initialize(
      Stream baseStream,
      ICompressionAlgorithm compressionAlgorithm,
      IChecksumAlgorithm checksumAlgorithm)
    {
      this.BaseStream = baseStream;
      this.algorithm = compressionAlgorithm;
      this.ChecksumAlgorithm = checksumAlgorithm;
      switch (this.Mode)
      {
        case StreamOperationMode.Read:
          this.Transform = this.algorithm.CreateDecompressor();
          break;
        case StreamOperationMode.Write:
          this.Transform = this.algorithm.CreateCompressor();
          break;
      }
    }
  }
}
