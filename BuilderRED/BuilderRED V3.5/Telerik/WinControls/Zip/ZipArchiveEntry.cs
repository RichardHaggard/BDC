// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.ZipArchiveEntry
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using Telerik.WinControls.Zip.Extensions;

namespace Telerik.WinControls.Zip
{
  public class ZipArchiveEntry : IDisposable, INotifyPropertyChanged
  {
    private long? offsetOfCompressedData = new long?();
    private ZipArchive archiveRef;
    private CentralDirectoryHeader header;
    private CompressionSettings settings;
    private CompressedStream compressedData;
    private bool written;
    private bool existedInArchive;
    private LocalFileHeader localFileHeader;
    private Stream updatableData;
    private bool deleted;
    private bool disposed;

    internal ZipArchiveEntry(ZipArchive archive, CentralDirectoryHeader header)
    {
      this.Archive = archive;
      this.header = header;
      this.settings = ZipHelper.GetCompressionSettings((CompressionMethod) this.header.CompressionMethod, this.Archive.CompressionSettings);
      this.settings.PrepareForZip((CentralDirectoryHeader) null);
      this.existedInArchive = true;
    }

    internal ZipArchiveEntry(ZipArchive archive, string entryName)
    {
      this.Archive = archive;
      this.settings = this.Archive.CompressionSettings;
      this.header = new CentralDirectoryHeader();
      this.header.VersionNeededToExtract = (ushort) 10;
      this.header.GeneralPurposeBitFlag = (ushort) 8;
      if (this.Archive.EncryptionSettings is DefaultEncryptionSettings)
      {
        this.header.GeneralPurposeBitFlag |= (ushort) 1;
        this.ValidateVersionNeeded(VersionNeededToExtract.Deflate);
      }
      this.FullName = entryName;
      this.LastWriteTime = DateTimeOffset.Now;
    }

    internal ZipArchiveEntry(ZipArchive archive, string entryName, CompressionSettings settings)
      : this(archive, entryName)
    {
      this.settings = settings;
      this.settings.PrepareForZip((CentralDirectoryHeader) null);
      this.CompressionMethod = settings.Method;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public ZipArchive Archive
    {
      get
      {
        return this.archiveRef;
      }
      set
      {
        this.archiveRef = value;
      }
    }

    public long CompressedLength
    {
      get
      {
        this.EnsureCentralDirectoryHeader();
        Zip64ExtraField zip64ExtraField = this.SelectZip64ExtraField();
        if (zip64ExtraField != null && zip64ExtraField.CompressedSize.HasValue)
          return (long) zip64ExtraField.CompressedSize.Value;
        return (long) this.header.CompressedSize;
      }
      private set
      {
        this.EnsureCentralDirectoryHeader();
        this.ValidateArchiveModeForWriting();
        if (value >= (long) uint.MaxValue)
        {
          this.EnsureZip64ExtraField().CompressedSize = new ulong?((ulong) value);
          this.header.CompressedSize = uint.MaxValue;
        }
        else
          this.header.CompressedSize = (uint) value;
      }
    }

    public int ExternalAttributes
    {
      get
      {
        this.EnsureCentralDirectoryHeader();
        return (int) this.header.ExternalAttributes;
      }
      set
      {
        this.EnsureCentralDirectoryHeader();
        this.header.ExternalAttributes = (uint) value;
        this.OnPropertyChanged(nameof (ExternalAttributes));
      }
    }

    public string FullName
    {
      get
      {
        this.EnsureCentralDirectoryHeader();
        return this.DecodeEntryName(this.header.FileName);
      }
      private set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.EnsureCentralDirectoryHeader();
        this.header.FileName = this.EncodeEntryName(value.Replace(PlatformSettings.Manager.DirectorySeparatorChar, "/").Replace(PlatformSettings.Manager.AltDirectorySeparatorChar, "/"));
        if (ZipHelper.EndsWithDirChar(value))
          this.ValidateVersionNeeded(VersionNeededToExtract.Deflate);
        this.OnPropertyChanged(nameof (FullName));
      }
    }

    public DateTimeOffset LastWriteTime
    {
      get
      {
        this.EnsureCentralDirectoryHeader();
        return new DateTimeOffset(ZipHelper.PackedToDateTime(this.header.FileTime));
      }
      set
      {
        this.EnsureCentralDirectoryHeader();
        this.ValidateArchiveModeForWriting();
        if (value.DateTime.Year < 1980 || value.DateTime.Year > 2107)
          throw new ArgumentOutOfRangeException(nameof (value), "Date-Time out of range.");
        this.header.FileTime = ZipHelper.DateTimeToPacked(value.DateTime);
      }
    }

    public long Length
    {
      get
      {
        this.EnsureCentralDirectoryHeader();
        Zip64ExtraField zip64ExtraField = this.SelectZip64ExtraField();
        if (zip64ExtraField != null && zip64ExtraField.OriginalSize.HasValue)
          return (long) zip64ExtraField.OriginalSize.Value;
        return (long) this.header.UncompressedSize;
      }
      private set
      {
        this.EnsureCentralDirectoryHeader();
        this.ValidateArchiveModeForWriting();
        if (value >= (long) uint.MaxValue)
        {
          this.EnsureZip64ExtraField().OriginalSize = new ulong?((ulong) value);
          this.header.UncompressedSize = uint.MaxValue;
        }
        else
          this.header.UncompressedSize = (uint) value;
      }
    }

    public string Name
    {
      get
      {
        string[] strArray = this.FullName.Split(new string[2]{ PlatformSettings.Manager.DirectorySeparatorChar, PlatformSettings.Manager.AltDirectorySeparatorChar }, StringSplitOptions.None);
        return strArray[strArray.Length - 1];
      }
    }

    internal CompressionMethod CompressionMethod
    {
      get
      {
        this.EnsureCentralDirectoryHeader();
        return (CompressionMethod) this.header.CompressionMethod;
      }
      set
      {
        this.EnsureCentralDirectoryHeader();
        if (value == CompressionMethod.Deflate)
          this.ValidateVersionNeeded(VersionNeededToExtract.Deflate);
        this.header.CompressionMethod = (ushort) value;
      }
    }

    private long CompressedDataOffset
    {
      get
      {
        if (!this.offsetOfCompressedData.HasValue)
        {
          this.Archive.Reader.BaseStream.Seek(this.LocalHeaderOffset, SeekOrigin.Begin);
          this.localFileHeader = new LocalFileHeader();
          if (!this.localFileHeader.TryReadBlock(this.Archive.Reader))
            throw new InvalidDataException("Local file header is corrupted.");
          this.offsetOfCompressedData = new long?(this.Archive.Reader.BaseStream.Position);
        }
        return this.offsetOfCompressedData.Value;
      }
      set
      {
        this.offsetOfCompressedData = new long?(value);
      }
    }

    private uint DiskStartNumber
    {
      get
      {
        this.EnsureCentralDirectoryHeader();
        Zip64ExtraField zip64ExtraField = this.SelectZip64ExtraField();
        if (zip64ExtraField != null && zip64ExtraField.DiskStartNumber.HasValue)
          return zip64ExtraField.DiskStartNumber.Value;
        return (uint) this.header.DiskNumberStart;
      }
    }

    private long LocalHeaderOffset
    {
      get
      {
        this.EnsureCentralDirectoryHeader();
        Zip64ExtraField zip64ExtraField = this.SelectZip64ExtraField();
        if (zip64ExtraField != null && zip64ExtraField.RelativeHeaderOffset.HasValue)
          return (long) zip64ExtraField.RelativeHeaderOffset.Value;
        return (long) this.header.LocalHeaderOffset;
      }
      set
      {
        this.EnsureCentralDirectoryHeader();
        this.ValidateArchiveModeForWriting();
        if (value >= (long) uint.MaxValue)
        {
          this.EnsureZip64ExtraField().RelativeHeaderOffset = new ulong?((ulong) value);
          this.header.LocalHeaderOffset = uint.MaxValue;
        }
        else
          this.header.LocalHeaderOffset = (uint) value;
      }
    }

    private Stream UpdatableData
    {
      get
      {
        if (this.updatableData == null)
        {
          this.updatableData = PlatformSettings.Manager.CreateTemporaryStream();
          if (this.existedInArchive)
          {
            using (Stream source = this.OpenForReading())
            {
              try
              {
                ZipFile.CopyStreamTo(source, this.updatableData);
              }
              catch (InvalidDataException ex)
              {
                this.updatableData.Dispose();
                PlatformSettings.Manager.DeleteTemporaryStream(this.updatableData);
                this.updatableData = (Stream) null;
                this.written = false;
                throw;
              }
            }
          }
        }
        return this.updatableData;
      }
    }

    public void Delete()
    {
      if (this.Archive == null)
        return;
      if (this.Archive.Mode != ZipArchiveMode.Update)
        throw new NotSupportedException("Entry can be deleted in Update mode only.");
      this.Archive.ThrowIfDisposed();
      this.Archive.RemoveEntry(this);
      this.Archive = (ZipArchive) null;
      this.deleted = true;
      this.DisposeStreams();
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public Stream Open()
    {
      switch (this.Archive.Mode)
      {
        case ZipArchiveMode.Create:
          return this.OpenForWriting();
        case ZipArchiveMode.Read:
          return this.OpenForReading();
        case ZipArchiveMode.Update:
          return this.OpenForUpdate();
        default:
          return (Stream) null;
      }
    }

    internal bool CheckIntegrity(out string message)
    {
      message = (string) null;
      if (this.existedInArchive)
      {
        if (!ZipHelper.IsCompressionMethodSupported(this.CompressionMethod))
        {
          message = string.Format("Unsupported compression method: {0}.", (object) this.CompressionMethod);
          return false;
        }
        if ((int) this.DiskStartNumber != (int) this.Archive.NumberOfThisDisk)
        {
          message = "Splitted archive is not supported.";
          return false;
        }
        if (this.LocalHeaderOffset > this.Archive.Reader.BaseStream.Length)
        {
          message = "Local file header is corrupted.";
          return false;
        }
        this.Archive.Reader.BaseStream.Seek(this.LocalHeaderOffset, SeekOrigin.Begin);
        this.localFileHeader = new LocalFileHeader();
        if (!this.localFileHeader.TryReadBlock(this.Archive.Reader))
        {
          message = "Local file header is corrupted.";
          return false;
        }
        if (!this.ValidateLocalFileHeader())
        {
          message = "Local file header is corrupted.";
          return false;
        }
        if (this.CompressedDataOffset + this.CompressedLength > this.Archive.Reader.BaseStream.Length)
        {
          message = "Local file header is corrupted.";
          return false;
        }
      }
      return true;
    }

    internal void WriteCentralDirectoryHeader()
    {
      this.header.WriteBlock(this.Archive.Writer);
    }

    private void CompressedData_ChecksumReady(object sender, EventArgs e)
    {
      if (this.Archive.Mode != ZipArchiveMode.Create && this.Archive.Mode != ZipArchiveMode.Update)
        return;
      this.header.Crc = (uint) this.compressedData.Checksum;
    }

    private string DecodeEntryName(byte[] entryNameBytes)
    {
      return this.GetEntryNameEncoding().GetString(entryNameBytes, 0, entryNameBytes.Length);
    }

    private void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      if (disposing)
      {
        this.FlushEntryData();
        this.DisposeStreams();
      }
      this.disposed = true;
    }

    private void DisposeStreams()
    {
      if (this.compressedData != null)
      {
        if (!this.deleted)
          this.compressedData.Dispose();
        this.compressedData.ChecksumReady -= new EventHandler(this.CompressedData_ChecksumReady);
        this.compressedData = (CompressedStream) null;
      }
      if (this.updatableData == null)
        return;
      this.updatableData.Dispose();
      PlatformSettings.Manager.DeleteTemporaryStream(this.updatableData);
      this.updatableData = (Stream) null;
    }

    private byte[] EncodeEntryName(string entryName)
    {
      if (string.IsNullOrEmpty(entryName))
        return new byte[0];
      Encoding encoding = this.Archive == null ? PlatformSettings.Manager.DefaultEncoding : this.Archive.EntryNameEncoding ?? PlatformSettings.Manager.DefaultEncoding;
      if (encoding is UTF8Encoding && encoding.Equals((object) Encoding.UTF8))
        this.header.GeneralPurposeBitFlag |= (ushort) 2048;
      else
        this.header.GeneralPurposeBitFlag &= (ushort) 63487;
      return encoding.GetBytes(entryName);
    }

    private void EnsureCentralDirectoryHeader()
    {
      if (this.header != null)
        return;
      this.header = new CentralDirectoryHeader();
    }

    private Zip64ExtraField EnsureZip64ExtraField()
    {
      Zip64ExtraField zip64ExtraField = (Zip64ExtraField) null;
      foreach (ExtraFieldBase extraField in this.header.ExtraFields)
      {
        zip64ExtraField = extraField as Zip64ExtraField;
        if (zip64ExtraField != null)
          break;
      }
      if (zip64ExtraField == null)
      {
        zip64ExtraField = new Zip64ExtraField((FileHeaderInfoBase) new CentralDirectoryHeaderInfo(this.header));
        this.header.ExtraFields.Add((ExtraFieldBase) zip64ExtraField);
      }
      return zip64ExtraField;
    }

    private void FlushEntryData()
    {
      if (this.Archive.Mode == ZipArchiveMode.Create)
      {
        if (this.compressedData == null)
          return;
        this.FlushFinalBlockAndDataDescriptor();
      }
      else
      {
        if (this.Archive.Mode != ZipArchiveMode.Update)
          return;
        if (this.updatableData != null)
        {
          this.OpenForWriting();
          this.updatableData.Seek(0L, SeekOrigin.Begin);
          ZipFile.CopyStreamTo(this.updatableData, (Stream) this.compressedData);
          this.FlushFinalBlockAndDataDescriptor();
        }
        else
        {
          long compressedDataOffset = this.CompressedDataOffset;
          this.WriteLocalFileHeader();
          this.Archive.Reader.BaseStream.Seek(compressedDataOffset, SeekOrigin.Begin);
          ZipHelper.CopyStream(this.Archive.Reader.BaseStream, this.Archive.Writer.BaseStream, this.CompressedLength);
          this.WriteDataDescriptor();
        }
      }
    }

    private void FlushFinalBlockAndDataDescriptor()
    {
      if (!this.compressedData.HasFlushedFinalBlock)
        this.compressedData.Flush();
      this.CompressedLength = this.compressedData.CompressedSize;
      this.Length = this.compressedData.TotalPlainCount;
      this.WriteDataDescriptor();
    }

    private Encoding GetEntryNameEncoding()
    {
      return (ushort) ((uint) this.header.GeneralPurposeBitFlag & 2048U) == (ushort) 0 ? (this.Archive == null ? PlatformSettings.Manager.DefaultEncoding : this.Archive.EntryNameEncoding ?? PlatformSettings.Manager.DefaultEncoding) : Encoding.UTF8;
    }

    private void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private Stream OpenForReading()
    {
      string message = (string) null;
      if (!this.CheckIntegrity(out message))
        throw new InvalidDataException(message);
      this.Archive.Reader.BaseStream.Position = this.CompressedDataOffset;
      this.compressedData = new CompressedStream(this.Archive.Reader.BaseStream, StreamOperationMode.Read, this.settings, true, this.Archive.EncryptionSettings);
      this.compressedData.ChecksumReady += new EventHandler(this.CompressedData_ChecksumReady);
      this.compressedData.SetLength(this.CompressedLength);
      return (Stream) this.compressedData;
    }

    private Stream OpenForUpdate()
    {
      if (this.written)
        throw new IOException("Create mode writes entry once and one entry at a time");
      this.UpdatableData.Seek(0L, SeekOrigin.Begin);
      return this.UpdatableData;
    }

    private Stream OpenForWriting()
    {
      if (this.written)
        throw new IOException("Create mode writes entry once and one entry at a time");
      this.WriteLocalFileHeader();
      if (this.Archive.EncryptionSettings != null)
      {
        DefaultEncryptionSettings encryptionSettings = this.Archive.EncryptionSettings as DefaultEncryptionSettings;
        if (encryptionSettings != null)
          encryptionSettings.FileTime = this.header.FileTime;
      }
      this.compressedData = new CompressedStream(this.Archive.Writer.BaseStream, StreamOperationMode.Write, this.settings, true, this.Archive.EncryptionSettings);
      this.compressedData.ChecksumReady += new EventHandler(this.CompressedData_ChecksumReady);
      return (Stream) this.compressedData;
    }

    private Zip64ExtraField SelectZip64ExtraField()
    {
      Zip64ExtraField zip64ExtraField = (Zip64ExtraField) null;
      if (this.header.ExtraFields.Count > 0)
      {
        foreach (ExtraFieldBase extraField in this.header.ExtraFields)
        {
          zip64ExtraField = extraField as Zip64ExtraField;
          if (zip64ExtraField != null)
            break;
        }
      }
      return zip64ExtraField;
    }

    private bool ValidateLocalFileHeader()
    {
      return this.localFileHeader != null;
    }

    private void ValidateVersionNeeded(VersionNeededToExtract value)
    {
      if ((VersionNeededToExtract) this.header.VersionNeededToExtract >= value)
        return;
      this.header.VersionNeededToExtract = (ushort) value;
    }

    private void ValidateArchiveModeForWriting()
    {
      if (this.Archive.Mode == ZipArchiveMode.Read)
        throw new NotSupportedException("Read only archive.");
      if (this.Archive.Mode == ZipArchiveMode.Create && this.written)
        throw new IOException("Entry is frozen after write.");
    }

    private void WriteDataDescriptor()
    {
      if (this.CompressedLength >= (long) uint.MaxValue || this.Length >= (long) uint.MaxValue)
        this.header.VersionNeededToExtract = (ushort) 45;
      DataDescriptor.FromFileHeader((FileHeaderBase) this.header).WriteBlock(this.Archive.Writer);
    }

    private void WriteLocalFileHeader()
    {
      this.LocalHeaderOffset = this.Archive.Writer.BaseStream.Position;
      new LocalFileHeader((FileHeaderBase) this.header).WriteBlock(this.Archive.Writer);
      this.CompressedDataOffset = this.Archive.Writer.BaseStream.Position;
    }
  }
}
