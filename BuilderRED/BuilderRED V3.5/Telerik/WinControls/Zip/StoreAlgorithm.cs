// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.StoreAlgorithm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal class StoreAlgorithm : ICompressionAlgorithm
  {
    public IBlockTransform CreateCompressor()
    {
      return (IBlockTransform) new StoreCompressor();
    }

    public IBlockTransform CreateDecompressor()
    {
      return (IBlockTransform) new StoreDecompressor();
    }

    public void Initialize(CompressionSettings settings)
    {
    }
  }
}
