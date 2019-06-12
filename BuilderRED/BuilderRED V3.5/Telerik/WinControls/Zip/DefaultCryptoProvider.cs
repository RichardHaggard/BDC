// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DefaultCryptoProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  public class DefaultCryptoProvider : ICryptoProvider
  {
    private DefaultEncryptionSettings cryptoSettings;

    public IBlockTransform CreateDecryptor()
    {
      DefaultDecryptor defaultDecryptor1 = (DefaultDecryptor) null;
      DefaultDecryptor defaultDecryptor2 = (DefaultDecryptor) null;
      try
      {
        defaultDecryptor2 = new DefaultDecryptor();
        defaultDecryptor2.InitKeys(this.cryptoSettings.Password);
        defaultDecryptor1 = defaultDecryptor2;
        defaultDecryptor2 = (DefaultDecryptor) null;
      }
      finally
      {
        defaultDecryptor2?.Dispose();
      }
      return (IBlockTransform) defaultDecryptor1;
    }

    public IBlockTransform CreateEncryptor()
    {
      DefaultEncryptor defaultEncryptor1 = (DefaultEncryptor) null;
      DefaultEncryptor defaultEncryptor2 = (DefaultEncryptor) null;
      try
      {
        defaultEncryptor2 = new DefaultEncryptor();
        defaultEncryptor2.InitKeys(this.cryptoSettings.Password);
        if (this.cryptoSettings.FileTime > 0U)
        {
          byte[] numArray = new byte[1]{ (byte) (this.cryptoSettings.FileTime >> 8 & (uint) byte.MaxValue) };
          defaultEncryptor2.Header.InitData = numArray;
        }
        defaultEncryptor1 = defaultEncryptor2;
        defaultEncryptor2 = (DefaultEncryptor) null;
      }
      finally
      {
        defaultEncryptor2?.Dispose();
      }
      return (IBlockTransform) defaultEncryptor1;
    }

    public void Initialize(EncryptionSettings settings)
    {
      if (settings == null)
        throw new ArgumentNullException(nameof (settings));
      this.cryptoSettings = settings as DefaultEncryptionSettings;
      if (this.cryptoSettings == null)
        throw new ArgumentException("Invalid argument type", nameof (settings));
    }
  }
}
