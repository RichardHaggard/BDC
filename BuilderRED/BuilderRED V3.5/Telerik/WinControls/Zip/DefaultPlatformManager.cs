// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DefaultPlatformManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;
using System.Text;

namespace Telerik.WinControls.Zip
{
  internal class DefaultPlatformManager : IPlatformManager
  {
    public string AltDirectorySeparatorChar
    {
      get
      {
        return "/";
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
        return "\\";
      }
    }

    public Stream CreateTemporaryStream()
    {
      return (Stream) new MemoryStream();
    }

    public void DeleteTemporaryStream(Stream stream)
    {
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
