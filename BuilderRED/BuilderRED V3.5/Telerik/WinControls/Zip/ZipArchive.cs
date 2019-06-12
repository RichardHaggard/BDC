// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.ZipArchive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Telerik.WinControls.Zip.Extensions;

namespace Telerik.WinControls.Zip
{
  public class ZipArchive : IDisposable, INotifyPropertyChanged
  {
    private Dictionary<string, ZipArchiveEntry> entries = new Dictionary<string, ZipArchiveEntry>();
    private ZipArchiveMode archiveMode;
    private BinaryReader archiveReader;
    private BinaryWriter archiveWriter;
    private bool disposed;
    private Encoding entryNameEncoding;
    private Stream originalStream;
    private Stream workingStream;
    private bool leaveStreamOpen;
    private bool centralDirectoryRead;
    private EndOfCentralDirectoryRecord endOfCentralDirectoryRecord;
    private Zip64EndOfCentralDirectoryLocator zip64EndOfCentralDirectoryLocator;
    private Zip64EndOfCentralDirectoryRecord zip64EndOfCentralDirectoryRecord;

    public ZipArchive(Stream stream)
      : this(stream, ZipArchiveMode.Read, true, (Encoding) null, (CompressionSettings) null, (EncryptionSettings) null)
    {
    }

    public ZipArchive(
      Stream stream,
      ZipArchiveMode mode,
      bool leaveOpen,
      Encoding entryNameEncoding)
      : this(stream, mode, leaveOpen, entryNameEncoding, (CompressionSettings) null, (EncryptionSettings) null)
    {
    }

    public ZipArchive(
      Stream stream,
      ZipArchiveMode mode,
      bool leaveOpen,
      Encoding entryNameEncoding,
      CompressionSettings compressionSettings,
      EncryptionSettings encryptionSettings)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      this.EntryNameEncoding = entryNameEncoding;
      if (compressionSettings == null)
        compressionSettings = (CompressionSettings) new DeflateSettings()
        {
          HeaderType = CompressedStreamHeader.None
        };
      this.CompressionSettings = compressionSettings;
      this.CompressionSettings.PrepareForZip((CentralDirectoryHeader) null);
      this.EncryptionSettings = encryptionSettings;
      this.Init(stream, mode, leaveOpen);
    }

    ~ZipArchive()
    {
      this.Dispose(false);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public IEnumerable<ZipArchiveEntry> Entries
    {
      get
      {
        this.EnsureCentralDirectoryRead();
        foreach (ZipArchiveEntry zipArchiveEntry in this.entries.Values)
          yield return zipArchiveEntry;
      }
    }

    public Encoding EntryNameEncoding
    {
      get
      {
        return this.entryNameEncoding;
      }
      private set
      {
        if (!PlatformSettings.Manager.IsEncodingSupported(value))
          throw new ArgumentException("Entry name encoding is not supported", nameof (value));
        this.entryNameEncoding = value;
      }
    }

    public ZipArchiveMode Mode
    {
      get
      {
        return this.archiveMode;
      }
    }

    internal CompressionSettings CompressionSettings { get; private set; }

    internal EncryptionSettings EncryptionSettings { get; private set; }

    internal uint NumberOfThisDisk
    {
      get
      {
        if (this.zip64EndOfCentralDirectoryRecord != null)
          return this.zip64EndOfCentralDirectoryRecord.NumberOfThisDisk;
        return (uint) this.endOfCentralDirectoryRecord.NumberOfThisDisk;
      }
    }

    internal BinaryReader Reader
    {
      get
      {
        return this.archiveReader;
      }
    }

    internal BinaryWriter Writer
    {
      get
      {
        return this.archiveWriter;
      }
    }

    private long CentralDirectoryStart
    {
      get
      {
        if (this.zip64EndOfCentralDirectoryRecord != null)
          return (long) this.zip64EndOfCentralDirectoryRecord.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;
        return (long) this.endOfCentralDirectoryRecord.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;
      }
    }

    private ulong NumberOfEntriesInTheCentralDirectory
    {
      get
      {
        if (this.zip64EndOfCentralDirectoryRecord != null)
          return this.zip64EndOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectory;
        return (ulong) this.endOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectory;
      }
    }

    public ZipArchiveEntry CreateEntry(string entryName)
    {
      ZipArchiveEntry zipArchiveEntry = new ZipArchiveEntry(this, entryName, this.CompressionSettings);
      this.entries.Add(zipArchiveEntry.FullName, zipArchiveEntry);
      return zipArchiveEntry;
    }

    public ZipArchiveEntry CreateEntry(
      string entryName,
      CompressionSettings settings)
    {
      ZipArchiveEntry zipArchiveEntry = new ZipArchiveEntry(this, entryName, settings);
      this.entries.Add(zipArchiveEntry.FullName, zipArchiveEntry);
      return zipArchiveEntry;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public ZipArchiveEntry GetEntry(string entryName)
    {
      if (entryName == null)
        throw new ArgumentNullException(nameof (entryName));
      if (this.Mode == ZipArchiveMode.Create)
        throw new NotSupportedException("Can't get entry in the create mode.");
      this.EnsureCentralDirectoryRead();
      ZipArchiveEntry zipArchiveEntry;
      this.entries.TryGetValue(entryName, out zipArchiveEntry);
      return zipArchiveEntry;
    }

    internal void ThrowIfDisposed()
    {
      if (this.disposed)
        throw new ObjectDisposedException(this.GetType().Name);
    }

    internal void RemoveEntry(ZipArchiveEntry entry)
    {
      this.entries.Remove(entry.FullName);
    }

    internal void DisposeStreams(bool closeStream)
    {
      this.leaveStreamOpen = !closeStream;
      this.Dispose();
    }

    private void ClearEntries()
    {
      foreach (ZipArchiveEntry zipArchiveEntry in this.entries.Values)
      {
        zipArchiveEntry.Dispose();
        zipArchiveEntry.Archive = (ZipArchive) null;
      }
      this.entries.Clear();
    }

    private void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          if (this.Mode == ZipArchiveMode.Read)
          {
            this.DisposeStreams();
          }
          else
          {
            try
            {
              this.WriteArchive();
              this.DisposeStreams();
            }
            catch (InvalidDataException ex)
            {
              this.DisposeStreams();
              this.disposed = true;
              throw;
            }
          }
        }
        this.ClearEntries();
      }
      this.disposed = true;
    }

    private void DisposeStreams()
    {
      if (!this.leaveStreamOpen)
      {
        this.workingStream.Dispose();
        if (this.originalStream != null)
        {
          this.originalStream.Dispose();
          PlatformSettings.Manager.DeleteTemporaryStream(this.workingStream);
        }
        if (this.archiveReader != null)
          this.archiveReader.Close();
        if (this.archiveWriter == null)
          return;
        this.archiveWriter.Close();
      }
      else
      {
        if (this.originalStream == null)
          return;
        this.originalStream.Flush();
        this.workingStream.Dispose();
        PlatformSettings.Manager.DeleteTemporaryStream(this.workingStream);
      }
    }

    private void EnsureCentralDirectoryRead()
    {
      if (this.centralDirectoryRead)
        return;
      this.ReadCentralDirectory();
      this.centralDirectoryRead = true;
    }

    private void Init(Stream stream, ZipArchiveMode mode, bool leaveOpen)
    {
      try
      {
        this.originalStream = (Stream) null;
        switch (mode)
        {
          case ZipArchiveMode.Create:
            if (!stream.CanWrite || !stream.CanSeek)
              throw new ArgumentException("Stream must support writing and seeking.");
            stream.SetLength(0L);
            break;
          case ZipArchiveMode.Read:
            if (!stream.CanRead)
              throw new ArgumentException("Stream must support reading.");
            if (!stream.CanSeek)
            {
              this.originalStream = stream;
              stream = PlatformSettings.Manager.CreateTemporaryStream();
              ZipFile.CopyStreamTo(this.originalStream, stream);
              stream.Seek(0L, SeekOrigin.Begin);
              break;
            }
            break;
          case ZipArchiveMode.Update:
            if (stream.CanRead && stream.CanSeek)
            {
              this.originalStream = stream;
              stream = PlatformSettings.Manager.CreateTemporaryStream();
              stream.Seek(0L, SeekOrigin.Begin);
              if (stream.CanWrite)
                break;
            }
            throw new ArgumentException("Stream must support reading, writing and seeking.");
          default:
            throw new ArgumentOutOfRangeException(nameof (mode));
        }
        this.archiveMode = mode;
        this.workingStream = stream;
        switch (mode)
        {
          case ZipArchiveMode.Read:
            this.archiveWriter = (BinaryWriter) null;
            this.archiveReader = new BinaryReader(this.workingStream);
            break;
          case ZipArchiveMode.Update:
            this.archiveReader = new BinaryReader(this.originalStream);
            this.archiveWriter = new BinaryWriter(this.workingStream);
            break;
          default:
            this.archiveReader = (BinaryReader) null;
            this.archiveWriter = new BinaryWriter(this.workingStream);
            break;
        }
        this.centralDirectoryRead = false;
        this.leaveStreamOpen = leaveOpen;
        switch (mode)
        {
          case ZipArchiveMode.Create:
            this.centralDirectoryRead = true;
            break;
          case ZipArchiveMode.Read:
            this.ReadEndOfCentralDirectory();
            break;
          case ZipArchiveMode.Update:
            if (this.Reader.BaseStream.Length != 0L)
            {
              this.ReadEndOfCentralDirectory();
              this.EnsureCentralDirectoryRead();
              using (IEnumerator<ZipArchiveEntry> enumerator = this.Entries.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  ZipArchiveEntry current = enumerator.Current;
                  string message = (string) null;
                  if (!current.CheckIntegrity(out message))
                    throw new InvalidDataException(message);
                }
                break;
              }
            }
            else
            {
              this.centralDirectoryRead = true;
              break;
            }
        }
      }
      catch
      {
        if (this.originalStream != null)
          PlatformSettings.Manager.DeleteTemporaryStream(this.workingStream);
        throw;
      }
    }

    private void ReadCentralDirectory()
    {
      this.Reader.BaseStream.Seek(this.CentralDirectoryStart, SeekOrigin.Begin);
      for (ulong index = 0; index < this.NumberOfEntriesInTheCentralDirectory; ++index)
      {
        CentralDirectoryHeader header = new CentralDirectoryHeader();
        if (!header.TryReadBlock(this.archiveReader))
          throw new InvalidDataException("Central directory header is broken.");
        ZipArchiveEntry zipArchiveEntry = new ZipArchiveEntry(this, header);
        this.entries.Add(zipArchiveEntry.FullName, zipArchiveEntry);
      }
    }

    private void ReadEndOfCentralDirectory()
    {
      try
      {
        this.Reader.BaseStream.Seek(-18L, SeekOrigin.End);
        if (!ZipHelper.SeekBackwardsToSignature(this.Reader.BaseStream, 101010256U))
          throw new InvalidDataException("End of central directory not found.");
        long position = this.Reader.BaseStream.Position;
        this.endOfCentralDirectoryRecord = new EndOfCentralDirectoryRecord();
        if (!this.endOfCentralDirectoryRecord.TryReadBlock(this.archiveReader))
          throw new InvalidDataException("End of central directory not found.");
        if ((int) this.endOfCentralDirectoryRecord.NumberOfThisDisk != (int) this.endOfCentralDirectoryRecord.NumberOfTheDiskWithTheStartOfTheCentralDirectory)
          throw new InvalidDataException("Splitted archive is not supported.");
        if ((int) this.endOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectory != (int) this.endOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectoryOnThisDisk)
          throw new InvalidDataException("Splitted archive is not supported.");
        if (this.endOfCentralDirectoryRecord.NumberOfThisDisk == ushort.MaxValue || this.endOfCentralDirectoryRecord.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber == uint.MaxValue || this.endOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectory == ushort.MaxValue)
        {
          this.Reader.BaseStream.Seek(position - 16L, SeekOrigin.Begin);
          if (ZipHelper.SeekBackwardsToSignature(this.Reader.BaseStream, 117853008U))
          {
            this.zip64EndOfCentralDirectoryLocator = new Zip64EndOfCentralDirectoryLocator();
            if (!this.zip64EndOfCentralDirectoryLocator.TryReadBlock(this.archiveReader))
              throw new InvalidDataException("ZIP64 End of central directory locator not found.");
            if (this.zip64EndOfCentralDirectoryLocator.RelativeOffsetOfTheZip64EndOfCentralDirectoryRecord > (ulong) long.MaxValue)
              throw new InvalidDataException("Relative offset of the Zip64 End Of Central Directory Record is too big.");
            this.Reader.BaseStream.Seek((long) this.zip64EndOfCentralDirectoryLocator.RelativeOffsetOfTheZip64EndOfCentralDirectoryRecord, SeekOrigin.Begin);
            this.zip64EndOfCentralDirectoryRecord = new Zip64EndOfCentralDirectoryRecord();
            if (!this.zip64EndOfCentralDirectoryRecord.TryReadBlock(this.archiveReader))
              throw new InvalidDataException("Zip64 End Of Central Directory Record not found.");
            if (this.zip64EndOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectory > (ulong) long.MaxValue)
              throw new InvalidDataException("Number of entries is too big.");
            if (this.zip64EndOfCentralDirectoryRecord.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber > (ulong) long.MaxValue)
              throw new InvalidDataException("Relative offset of the Central Directory Start is too big.");
            if ((long) this.zip64EndOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectory != (long) this.zip64EndOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectoryOnThisDisk)
              throw new InvalidDataException("Splitted archive is not supported.");
          }
        }
        if (this.CentralDirectoryStart > this.Reader.BaseStream.Length)
          throw new InvalidDataException("Relative offset of the Central Directory Start is too big.");
      }
      catch (EndOfStreamException ex)
      {
        throw new InvalidDataException("Archive corrupted", (Exception) ex);
      }
      catch (IOException ex)
      {
        throw new InvalidDataException("Archive corrupted", (Exception) ex);
      }
    }

    private void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void WriteArchive()
    {
      if (this.Mode == ZipArchiveMode.Update)
      {
        foreach (ZipArchiveEntry zipArchiveEntry in this.entries.Values)
          zipArchiveEntry.Dispose();
      }
      long position1 = this.Writer.BaseStream.Position;
      foreach (ZipArchiveEntry zipArchiveEntry in this.entries.Values)
        zipArchiveEntry.WriteCentralDirectoryHeader();
      long num = this.Writer.BaseStream.Position - position1;
      bool flag = false;
      if (position1 >= (long) uint.MaxValue || num >= (long) uint.MaxValue || this.entries.Count >= (int) ushort.MaxValue)
        flag = true;
      if (flag)
      {
        long position2 = this.Writer.BaseStream.Position;
        if (this.zip64EndOfCentralDirectoryRecord == null)
          this.zip64EndOfCentralDirectoryRecord = new Zip64EndOfCentralDirectoryRecord();
        this.zip64EndOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectory = (ulong) this.entries.Count;
        this.zip64EndOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectoryOnThisDisk = (ulong) this.entries.Count;
        this.zip64EndOfCentralDirectoryRecord.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber = (ulong) position1;
        this.zip64EndOfCentralDirectoryRecord.SizeOfCentralDirectory = (ulong) num;
        this.zip64EndOfCentralDirectoryRecord.WriteBlock(this.Writer);
        if (this.zip64EndOfCentralDirectoryLocator == null)
          this.zip64EndOfCentralDirectoryLocator = new Zip64EndOfCentralDirectoryLocator();
        this.zip64EndOfCentralDirectoryLocator.NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDirectory = 0U;
        this.zip64EndOfCentralDirectoryLocator.NumberOfDisks = 1U;
        this.zip64EndOfCentralDirectoryLocator.RelativeOffsetOfTheZip64EndOfCentralDirectoryRecord = (ulong) position2;
        this.zip64EndOfCentralDirectoryLocator.WriteBlock(this.Writer);
      }
      if (this.endOfCentralDirectoryRecord == null)
        this.endOfCentralDirectoryRecord = new EndOfCentralDirectoryRecord();
      this.endOfCentralDirectoryRecord.NumberOfThisDisk = (ushort) 0;
      this.endOfCentralDirectoryRecord.NumberOfTheDiskWithTheStartOfTheCentralDirectory = (ushort) 0;
      this.endOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectoryOnThisDisk = this.entries.Count < (int) ushort.MaxValue ? (ushort) this.entries.Count : ushort.MaxValue;
      this.endOfCentralDirectoryRecord.NumberOfEntriesInTheCentralDirectory = this.entries.Count < (int) ushort.MaxValue ? (ushort) this.entries.Count : ushort.MaxValue;
      this.endOfCentralDirectoryRecord.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber = position1 > (long) uint.MaxValue ? uint.MaxValue : (uint) position1;
      this.endOfCentralDirectoryRecord.SizeOfCentralDirectory = num > (long) uint.MaxValue ? uint.MaxValue : (uint) num;
      this.endOfCentralDirectoryRecord.WriteBlock(this.Writer);
      if (this.Mode != ZipArchiveMode.Update)
        return;
      this.workingStream.Seek(0L, SeekOrigin.Begin);
      this.originalStream.Seek(0L, SeekOrigin.Begin);
      ZipFile.CopyStreamTo(this.workingStream, this.originalStream);
      this.originalStream.SetLength(this.workingStream.Length);
    }
  }
}
