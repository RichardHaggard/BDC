// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.Extensions.ZipFile
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Telerik.WinControls.Zip.Extensions
{
  public static class ZipFile
  {
    public static ZipArchiveEntry CreateEntryFromFile(
      ZipArchive destination,
      string sourceFileName,
      string entryName)
    {
      return ZipFile.CreateEntryFromFile(destination, sourceFileName, entryName, (CompressionSettings) null);
    }

    public static ZipArchiveEntry CreateEntryFromFile(
      ZipArchive destination,
      string sourceFileName,
      string entryName,
      CompressionLevel compressionLevel)
    {
      DeflateSettings deflateSettings = new DeflateSettings() { CompressionLevel = compressionLevel, HeaderType = CompressedStreamHeader.None };
      return ZipFile.CreateEntryFromFile(destination, sourceFileName, entryName, (CompressionSettings) deflateSettings);
    }

    public static ZipArchiveEntry CreateEntryFromFile(
      ZipArchive destination,
      string sourceFileName,
      string entryName,
      CompressionSettings compressionSettings)
    {
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (sourceFileName == null)
        throw new ArgumentNullException(nameof (sourceFileName));
      if (entryName == null)
        throw new ArgumentNullException(nameof (entryName));
      ZipArchiveEntry zipArchiveEntry;
      using (Stream stream1 = (Stream) File.Open(sourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        zipArchiveEntry = compressionSettings == null ? destination.CreateEntry(entryName) : destination.CreateEntry(entryName, compressionSettings);
        zipArchiveEntry.ExternalAttributes = (int) File.GetAttributes(sourceFileName);
        DateTime dateTime = File.GetLastWriteTime(sourceFileName);
        if (dateTime.Year < 1980 || dateTime.Year > 2107)
          dateTime = new DateTime(1980, 1, 1, 0, 0, 0);
        zipArchiveEntry.LastWriteTime = (DateTimeOffset) dateTime;
        Stream stream2 = zipArchiveEntry.Open();
        byte[] buffer = new byte[4096];
        int count;
        while ((count = stream1.Read(buffer, 0, buffer.Length)) != 0)
          stream2.Write(buffer, 0, count);
        if (destination.Mode == ZipArchiveMode.Create)
        {
          stream2.Dispose();
          zipArchiveEntry.Dispose();
        }
      }
      return zipArchiveEntry;
    }

    public static void CreateFromDirectory(
      string sourceDirectoryName,
      string destinationArchiveFileName)
    {
      ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, (CompressionSettings) null, false, (Encoding) null);
    }

    public static void CreateFromDirectory(
      string sourceDirectoryName,
      string destinationArchiveFileName,
      CompressionLevel compressionLevel,
      bool includeBaseDirectory)
    {
      DeflateSettings deflateSettings = new DeflateSettings() { CompressionLevel = compressionLevel, HeaderType = CompressedStreamHeader.None };
      ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, (CompressionSettings) deflateSettings, includeBaseDirectory, (Encoding) null);
    }

    public static void CreateFromDirectory(
      string sourceDirectoryName,
      string destinationArchiveFileName,
      CompressionLevel compressionLevel,
      bool includeBaseDirectory,
      Encoding entryNameEncoding)
    {
      DeflateSettings deflateSettings = new DeflateSettings() { CompressionLevel = compressionLevel, HeaderType = CompressedStreamHeader.None };
      ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, (CompressionSettings) deflateSettings, includeBaseDirectory, entryNameEncoding);
    }

    public static void CreateFromDirectory(
      string sourceDirectoryName,
      string destinationArchiveFileName,
      CompressionSettings compressionSettings,
      bool includeBaseDirectory,
      Encoding entryNameEncoding)
    {
      char[] chArray = new char[2]{ Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
      sourceDirectoryName = Path.GetFullPath(sourceDirectoryName);
      destinationArchiveFileName = Path.GetFullPath(destinationArchiveFileName);
      using (ZipArchive destination = ZipFile.Open(destinationArchiveFileName, ZipArchiveMode.Create, entryNameEncoding))
      {
        bool flag = true;
        DirectoryInfo directoryInfo1 = new DirectoryInfo(sourceDirectoryName);
        string fullName = directoryInfo1.FullName;
        if (includeBaseDirectory && directoryInfo1.Parent != null)
          fullName = directoryInfo1.Parent.FullName;
        foreach (FileSystemInfo fileSystemInfo in ZipFile.GetAllFilesRecursively(directoryInfo1))
        {
          flag = false;
          int length = fileSystemInfo.FullName.Length - fullName.Length;
          string entryName = fileSystemInfo.FullName.Substring(fullName.Length, length).TrimStart(chArray);
          DirectoryInfo directoryInfo2 = fileSystemInfo as DirectoryInfo;
          if (directoryInfo2 != null)
          {
            if (ZipFile.IsDirectoryEmpty(directoryInfo2))
              destination.CreateEntry(entryName + (object) Path.DirectorySeparatorChar);
          }
          else
            ZipFile.CreateEntryFromFile(destination, fileSystemInfo.FullName, entryName, compressionSettings);
        }
        if (!includeBaseDirectory || !flag)
          return;
        destination.CreateEntry(directoryInfo1.Name + (object) Path.DirectorySeparatorChar);
      }
    }

    private static IEnumerable<FileSystemInfo> GetAllFilesRecursively(
      DirectoryInfo directoryInfo)
    {
      foreach (FileSystemInfo fileSystemInfo in directoryInfo.GetFileSystemInfos())
      {
        yield return fileSystemInfo;
        DirectoryInfo di = fileSystemInfo as DirectoryInfo;
        if (di != null)
        {
          IEnumerator<FileSystemInfo> enumerator = ZipFile.GetAllFilesRecursively(di).GetEnumerator();
          while (enumerator.MoveNext())
          {
            FileSystemInfo childInfo = enumerator.Current;
            yield return childInfo;
          }
        }
      }
    }

    public static void ExtractToDirectory(
      string sourceArchiveFileName,
      string destinationDirectoryName)
    {
      ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, (Encoding) null);
    }

    public static void ExtractToDirectory(
      string sourceArchiveFileName,
      string destinationDirectoryName,
      Encoding entryNameEncoding)
    {
      if (sourceArchiveFileName == null)
        throw new ArgumentNullException(nameof (sourceArchiveFileName));
      using (ZipArchive source = ZipFile.Open(sourceArchiveFileName, ZipArchiveMode.Read, entryNameEncoding))
        ZipFile.ExtractToDirectory(source, destinationDirectoryName);
    }

    public static void ExtractToDirectory(ZipArchive source, string destinationDirectoryName)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (destinationDirectoryName == null)
        throw new ArgumentNullException(nameof (destinationDirectoryName));
      string fullName = Directory.CreateDirectory(destinationDirectoryName).FullName;
      foreach (ZipArchiveEntry entry in source.Entries)
      {
        string fullPath = Path.GetFullPath(Path.Combine(fullName, entry.FullName));
        if (!fullPath.StartsWith(fullName, StringComparison.OrdinalIgnoreCase))
          throw new IOException("Extracting results in outside");
        if (Path.GetFileName(fullPath).Length != 0)
        {
          Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
          ZipFile.ExtractToFile(entry, fullPath, false);
        }
        else
        {
          if (entry.Length != 0L)
            throw new IOException("Directory name with data");
          Directory.CreateDirectory(fullPath);
        }
      }
    }

    public static void ExtractToFile(ZipArchiveEntry source, string destinationFileName)
    {
      ZipFile.ExtractToFile(source, destinationFileName, false);
    }

    public static void ExtractToFile(
      ZipArchiveEntry source,
      string destinationFileName,
      bool overwrite)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (destinationFileName == null)
        throw new ArgumentNullException(nameof (destinationFileName));
      using (Stream stream1 = (Stream) File.Open(destinationFileName, overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write, FileShare.None))
      {
        using (Stream stream2 = source.Open())
        {
          byte[] buffer = new byte[4096];
          int count;
          while ((count = stream2.Read(buffer, 0, buffer.Length)) != 0)
            stream1.Write(buffer, 0, count);
        }
      }
      File.SetLastWriteTime(destinationFileName, source.LastWriteTime.DateTime);
    }

    public static ZipArchive Open(string archiveFileName, ZipArchiveMode mode)
    {
      return ZipFile.Open(archiveFileName, mode, (Encoding) null);
    }

    public static ZipArchive Open(
      string archiveFileName,
      ZipArchiveMode mode,
      Encoding entryNameEncoding)
    {
      FileMode mode1;
      FileAccess access;
      FileShare share;
      switch (mode)
      {
        case ZipArchiveMode.Create:
          mode1 = FileMode.CreateNew;
          access = FileAccess.Write;
          share = FileShare.None;
          break;
        case ZipArchiveMode.Read:
          mode1 = FileMode.Open;
          access = FileAccess.Read;
          share = FileShare.Read;
          break;
        case ZipArchiveMode.Update:
          mode1 = FileMode.OpenOrCreate;
          access = FileAccess.ReadWrite;
          share = FileShare.None;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (mode));
      }
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = File.Open(archiveFileName, mode1, access, share);
        return new ZipArchive((Stream) fileStream, mode, false, entryNameEncoding);
      }
      catch
      {
        fileStream?.Dispose();
        throw;
      }
    }

    public static ZipArchive OpenRead(string archiveFileName)
    {
      return ZipFile.Open(archiveFileName, ZipArchiveMode.Read);
    }

    private static bool IsDirectoryEmpty(DirectoryInfo directoryInfo)
    {
      bool flag = true;
      using (IEnumerator<FileSystemInfo> enumerator = ZipFile.GetAllFilesRecursively(directoryInfo).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          FileSystemInfo current = enumerator.Current;
          flag = false;
        }
      }
      return flag;
    }

    public static void CopyStreamTo(Stream source, Stream destination)
    {
      byte[] buffer = new byte[4096];
      while (true)
      {
        int num = source.Read(buffer, 0, buffer.Length);
        int count = num;
        if (num != 0)
          destination.Write(buffer, 0, count);
        else
          break;
      }
    }
  }
}
