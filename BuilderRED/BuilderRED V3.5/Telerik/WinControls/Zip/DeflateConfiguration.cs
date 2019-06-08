// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DeflateConfiguration
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal class DeflateConfiguration
  {
    private static readonly DeflateConfiguration[] table = new DeflateConfiguration[10]{ new DeflateConfiguration(0, 0, 0, 0), new DeflateConfiguration(4, 4, 8, 4), new DeflateConfiguration(4, 5, 16, 8), new DeflateConfiguration(4, 6, 32, 32), new DeflateConfiguration(4, 4, 16, 16), new DeflateConfiguration(8, 16, 32, 32), new DeflateConfiguration(8, 16, 128, 128), new DeflateConfiguration(8, 32, 128, 256), new DeflateConfiguration(32, 128, 258, 1024), new DeflateConfiguration(32, 258, 258, 4096) };

    private DeflateConfiguration(int goodLength, int maxLazy, int niceLength, int maxChainLength)
    {
      this.GoodLength = goodLength;
      this.MaxLazy = maxLazy;
      this.NiceLength = niceLength;
      this.MaxChainLength = maxChainLength;
    }

    internal int GoodLength { get; private set; }

    internal int MaxLazy { get; private set; }

    internal int NiceLength { get; private set; }

    internal int MaxChainLength { get; private set; }

    public static DeflateConfiguration Lookup(int compressionLevel)
    {
      return DeflateConfiguration.table[compressionLevel];
    }
  }
}
