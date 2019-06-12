// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.CryptoStream
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class CryptoStream : OperationStream
  {
    private ICryptoProvider cryptoProvider;

    public CryptoStream(Stream input, StreamOperationMode mode, ICryptoProvider cryptoProvider)
      : base(input, mode)
    {
      if (!input.CanRead && mode == StreamOperationMode.Read || !input.CanWrite && mode == StreamOperationMode.Write)
        throw new ArgumentOutOfRangeException(nameof (mode));
      this.cryptoProvider = cryptoProvider;
      switch (this.Mode)
      {
        case StreamOperationMode.Read:
          this.Transform = this.cryptoProvider.CreateDecryptor();
          break;
        case StreamOperationMode.Write:
          this.Transform = this.cryptoProvider.CreateEncryptor();
          break;
      }
    }

    ~CryptoStream()
    {
      this.Dispose(false);
    }

    protected override void Dispose(bool disposing)
    {
      this.cryptoProvider = (ICryptoProvider) null;
      base.Dispose(disposing);
    }
  }
}
