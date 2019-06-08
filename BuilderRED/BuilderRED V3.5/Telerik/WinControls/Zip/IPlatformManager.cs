// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.IPlatformManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.IO;
using System.Text;

namespace Telerik.WinControls.Zip
{
  public interface IPlatformManager
  {
    string AltDirectorySeparatorChar { get; }

    Encoding DefaultEncoding { get; }

    string DirectorySeparatorChar { get; }

    Stream CreateTemporaryStream();

    void DeleteTemporaryStream(Stream stream);

    ICryptoProvider GetCryptoProvider(EncryptionSettings settings);

    bool IsEncodingSupported(Encoding encoding);
  }
}
