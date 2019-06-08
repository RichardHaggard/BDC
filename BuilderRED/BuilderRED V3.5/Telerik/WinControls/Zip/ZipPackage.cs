// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.ZipPackage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using Telerik.WinControls.Zip.Extensions;

namespace Telerik.WinControls.Zip
{
  [Obsolete("This class has been deprecated. Use ZipArchive instead.")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class ZipPackage : ZipArchive
  {
    private const uint ArchiveAttribute = 32;
    private string zipFileName;
    private List<ZipPackageEntry> zipPackageEntries;

    private ZipPackage(Stream stream, ZipArchiveMode mode)
      : base(stream, mode, false, (Encoding) null)
    {
    }

    public string FileName
    {
      get
      {
        return this.zipFileName;
      }
    }

    public IList<ZipPackageEntry> ZipPackageEntries
    {
      get
      {
        List<ZipPackageEntry> zipPackageEntryList = new List<ZipPackageEntry>();
        foreach (ZipArchiveEntry entry in this.Entries)
          zipPackageEntryList.Add(new ZipPackageEntry(entry));
        this.zipPackageEntries = zipPackageEntryList;
        return (IList<ZipPackageEntry>) zipPackageEntryList;
      }
    }

    public static ZipPackage Create(Stream stream)
    {
      return new ZipPackage(stream, ZipArchiveMode.Create);
    }

    public static ZipPackage CreateFile(string fileName)
    {
      ZipPackage zipPackage = ZipPackage.Create((Stream) new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None));
      zipPackage.zipFileName = fileName;
      return zipPackage;
    }

    public static bool IsZipFile(Stream stream)
    {
      if (stream == null || stream.Length < 22L)
        return false;
      long position = stream.Position;
      try
      {
        using (ZipPackage.Open(stream))
          return true;
      }
      catch
      {
        return false;
      }
      finally
      {
        stream.Position = position;
      }
    }

    public static bool IsZipFile(string fileName)
    {
      if (!File.Exists(fileName))
        return false;
      try
      {
        using (ZipPackage.OpenFile(fileName, FileAccess.Read))
          return true;
      }
      catch
      {
        return false;
      }
    }

    public static ZipPackage Open(Stream stream)
    {
      ZipArchiveMode mode = stream.CanWrite ? ZipArchiveMode.Update : ZipArchiveMode.Read;
      return new ZipPackage(stream, mode);
    }

    public static ZipPackage OpenFile(string fileName, FileAccess access)
    {
      if (access != FileAccess.Read)
        throw new InvalidOperationException("File should be opened with read access.");
      ZipPackage zipPackage = ZipPackage.Open((Stream) new FileStream(fileName, FileMode.Open, access, FileShare.Read));
      zipPackage.zipFileName = fileName;
      return zipPackage;
    }

    public void Add(string fileName)
    {
      string fileName1 = ZipPackage.GetFileName(fileName);
      this.Add(fileName, fileName1, CompressionType.Default);
    }

    public void Add(string fileName, CompressionType compressionType)
    {
      string fileName1 = ZipPackage.GetFileName(fileName);
      this.Add(fileName, fileName1, compressionType);
    }

    public void Add(IEnumerable<string> fileNames)
    {
      foreach (string fileName1 in fileNames)
      {
        string fileName2 = ZipPackage.GetFileName(fileName1);
        this.Add(fileName1, fileName2, CompressionType.Default);
      }
    }

    public void Add(IEnumerable<string> fileNames, CompressionType compressionType)
    {
      foreach (string fileName1 in fileNames)
      {
        string fileName2 = ZipPackage.GetFileName(fileName1);
        this.Add(fileName1, fileName2, compressionType);
      }
    }

    public void Add(string fileName, string fileNameInZip)
    {
      this.Add(fileName, fileNameInZip, DateTime.Now, CompressionType.Default);
    }

    public void Add(string fileName, string fileNameInZip, CompressionType compressionType)
    {
      this.Add(fileName, fileNameInZip, DateTime.Now, compressionType);
    }

    public void Add(string fileName, string fileNameInZip, DateTime dateTime)
    {
      if (string.Compare(fileName, this.FileName, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
        throw new ArgumentException("Can't add a zip file to itself.");
      bool flag1 = File.Exists(fileName);
      bool flag2 = !flag1 && Directory.Exists(fileName);
      if (!flag1 && !flag2)
        throw new FileNotFoundException(string.Format("File not found: '{0}'.", (object) fileName));
      this.AddEntry(fileName, fileNameInZip, dateTime, CompressionType.Default);
    }

    public void Add(
      string fileName,
      string fileNameInZip,
      DateTime dateTime,
      CompressionType compressionType)
    {
      if (string.Compare(fileName, this.FileName, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
        throw new ArgumentException("Can't add a zip file to itself.");
      bool flag1 = File.Exists(fileName);
      bool flag2 = !flag1 && Directory.Exists(fileName);
      if (!flag1 && !flag2)
        throw new FileNotFoundException(string.Format("File not found: '{0}'.", (object) fileName));
      this.AddEntry(fileName, fileNameInZip, dateTime, compressionType);
    }

    public void AddStream(Stream stream, string fileNameInZip)
    {
      this.AddStream(stream, fileNameInZip, ZipCompression.Default, DateTime.Now, CompressionType.Default);
    }

    public void AddStream(Stream stream, string fileNameInZip, CompressionType compressionType)
    {
      this.AddStream(stream, fileNameInZip, ZipCompression.Default, DateTime.Now, compressionType);
    }

    public void AddStream(
      Stream stream,
      string fileNameInZip,
      ZipCompression method,
      DateTime dateTime)
    {
      this.AddStream(stream, fileNameInZip, method, dateTime, CompressionType.Default);
    }

    public void AddStream(
      Stream stream,
      string fileNameInZip,
      ZipCompression method,
      DateTime dateTime,
      CompressionType compressionType)
    {
      if (compressionType == CompressionType.Lzma)
        throw new NotSupportedException();
      DeflateSettings deflateSettings = ZipOutputStream.CreateDeflateSettings(method);
      deflateSettings.HeaderType = CompressedStreamHeader.None;
      this.DeleteAvailableEntry(fileNameInZip);
      using (ZipArchiveEntry entry = this.CreateEntry(fileNameInZip, (CompressionSettings) deflateSettings))
      {
        entry.LastWriteTime = (DateTimeOffset) dateTime;
        entry.ExternalAttributes = 32;
        Stream destination = entry.Open();
        ZipFile.CopyStreamTo(stream, destination);
      }
    }

    public void Close(bool closeStream)
    {
      this.DisposeStreams(closeStream);
    }

    public int IndexOf(string fileNameInZip)
    {
      int num = 0;
      foreach (ZipPackageEntry zipPackageEntry in (IEnumerable<ZipPackageEntry>) this.ZipPackageEntries)
      {
        if (string.Compare(zipPackageEntry.FileNameInZip, fileNameInZip, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
          return num;
        ++num;
      }
      return -1;
    }

    public void RemoveEntry(ZipPackageEntry zipPackageEntry)
    {
      zipPackageEntry.Delete();
    }

    private static string GetFileName(string fileName)
    {
      fileName = Path.GetFileName(fileName);
      return fileName;
    }

    private void AddEntry(
      string fileName,
      string fileNameInZip,
      DateTime dateTime,
      CompressionType compressionType)
    {
      if (compressionType == CompressionType.Lzma)
        throw new NotSupportedException();
      if (File.Exists(fileName))
      {
        using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
          this.SaveEntry(stream, fileName, fileNameInZip, dateTime);
      }
      else
      {
        if (!Directory.Exists(fileName))
          throw new FileNotFoundException(string.Format("File not found: '{0}'.", (object) fileName));
        this.SaveEntry((FileStream) null, fileName, fileNameInZip, dateTime);
      }
    }

    private void DeleteAvailableEntry(string fileNameInZip)
    {
      int index = this.IndexOf(fileNameInZip);
      if (index == -1)
        return;
      this.zipPackageEntries[index].Delete();
    }

    private void SaveEntry(
      FileStream stream,
      string fileName,
      string fileNameInZip,
      DateTime dateTime)
    {
      FileInfo fileInfo = new FileInfo(fileName);
      int attributes = (int) fileInfo.Attributes;
      this.DeleteAvailableEntry(fileNameInZip);
      string entryName = fileNameInZip;
      DeflateSettings deflateSettings = new DeflateSettings() { CompressionLevel = CompressionLevel.Optimal, HeaderType = CompressedStreamHeader.None };
      using (ZipArchiveEntry entry = this.CreateEntry(entryName, (CompressionSettings) deflateSettings))
      {
        Stream stream1 = entry.Open();
        if (stream != null)
        {
          byte[] buffer = new byte[4096];
          int count;
          while ((count = stream.Read(buffer, 0, buffer.Length)) != 0)
            stream1.Write(buffer, 0, count);
        }
        entry.LastWriteTime = (DateTimeOffset) fileInfo.LastWriteTime;
        entry.ExternalAttributes = attributes;
      }
    }
  }
}
