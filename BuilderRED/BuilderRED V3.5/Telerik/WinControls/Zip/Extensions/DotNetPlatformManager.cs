// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.Extensions.DotNetPlatformManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Telerik.WinControls.Zip.Extensions
{
  public class DotNetPlatformManager : IPlatformManager
  {
    private static readonly string altDirectorySeparatorChar = new string(Path.AltDirectorySeparatorChar, 1);
    private static readonly string directorySeparatorChar = new string(Path.DirectorySeparatorChar, 1);
    private Dictionary<Stream, string> temporaryStreams = new Dictionary<Stream, string>();

    public DotNetPlatformManager()
    {
      this.TemporaryStreamType = TemporaryStreamType.Memory;
    }

    public string AltDirectorySeparatorChar
    {
      get
      {
        return DotNetPlatformManager.altDirectorySeparatorChar;
      }
    }

    public Encoding DefaultEncoding
    {
      get
      {
        return Encoding.UTF8;
      }
    }

    public string DirectorySeparatorChar
    {
      get
      {
        return DotNetPlatformManager.directorySeparatorChar;
      }
    }

    public TemporaryStreamType TemporaryStreamType { get; set; }

    public Stream CreateTemporaryStream()
    {
      if (this.TemporaryStreamType == TemporaryStreamType.Memory)
        return (Stream) new MemoryStream();
      string tempFileName = Path.GetTempFileName();
      FileStream fileStream = File.Open(tempFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
      this.temporaryStreams.Add((Stream) fileStream, tempFileName);
      return (Stream) fileStream;
    }

    public void DeleteTemporaryStream(Stream stream)
    {
      if (this.TemporaryStreamType != TemporaryStreamType.File)
        return;
      string path = (string) null;
      if (!this.temporaryStreams.TryGetValue(stream, out path))
        return;
      File.Delete(path);
      this.temporaryStreams.Remove(stream);
    }

    public ICryptoProvider GetCryptoProvider(EncryptionSettings settings)
    {
      switch (settings.Algorithm.ToUpperInvariant())
      {
        case "DEFAULT":
          ICryptoProvider cryptoProvider = (ICryptoProvider) new DefaultCryptoProvider();
          cryptoProvider?.Initialize(settings);
          return cryptoProvider;
        default:
          throw new NotSupportedException();
      }
    }

    public bool IsEncodingSupported(Encoding encoding)
    {
      if (encoding == null)
        return true;
      if (!encoding.Equals((object) Encoding.BigEndianUnicode))
        return !encoding.Equals((object) Encoding.Unicode);
      return false;
    }
  }
}
