// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DeflateAlgorithm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class DeflateAlgorithm : ICompressionAlgorithm
  {
    private DeflateSettings deflateSettings;

    public IBlockTransform CreateCompressor()
    {
      return (IBlockTransform) new DeflateCompressor(this.deflateSettings);
    }

    public IBlockTransform CreateDecompressor()
    {
      return (IBlockTransform) new DeflateDecompressor(this.deflateSettings);
    }

    public void Initialize(CompressionSettings settings)
    {
      this.deflateSettings = settings as DeflateSettings;
      if (this.deflateSettings == null)
        throw new ArgumentException("Wrong settings type.");
    }
  }
}
