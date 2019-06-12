// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaAlgorithm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaAlgorithm : ICompressionAlgorithm
  {
    private LzmaSettings lzmaSettings;

    public IBlockTransform CreateCompressor()
    {
      return (IBlockTransform) new LzmaCompressor(this.lzmaSettings);
    }

    public IBlockTransform CreateDecompressor()
    {
      return (IBlockTransform) new LzmaDecompressor(this.lzmaSettings);
    }

    public void Initialize(CompressionSettings settings)
    {
      this.lzmaSettings = settings as LzmaSettings;
      if (this.lzmaSettings == null)
        throw new ArgumentException("Wrong settings type.");
    }
  }
}
