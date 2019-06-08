// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.ZipPackageEntry
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.IO;

namespace Telerik.WinControls.Zip
{
  [Obsolete("This class has been deprecated. Use ZipArchiveEntry instead.")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class ZipPackageEntry
  {
    private ZipArchiveEntry entry;

    internal ZipPackageEntry(ZipArchiveEntry entry)
    {
      this.entry = entry;
    }

    public FileAttributes Attributes
    {
      get
      {
        return (FileAttributes) this.entry.ExternalAttributes;
      }
    }

    public int CompressedSize
    {
      get
      {
        return (int) this.entry.CompressedLength;
      }
    }

    public string FileNameInZip
    {
      get
      {
        return this.entry.FullName;
      }
    }

    public int UncompressedSize
    {
      get
      {
        return (int) this.entry.Length;
      }
    }

    public Stream OpenInputStream()
    {
      return this.entry.Open();
    }

    internal void Delete()
    {
      this.entry.Delete();
    }
  }
}
