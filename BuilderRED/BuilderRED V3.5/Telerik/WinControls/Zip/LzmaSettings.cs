// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaSettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  public class LzmaSettings : CompressionSettings
  {
    private int dictionarySize;
    private int positionStateBits;
    private int literalContextBits;
    private int literalPositionBits;
    private int fastBytes;
    private LzmaMatchFinderType matchFinderType;

    public LzmaSettings()
    {
      this.Method = CompressionMethod.Lzma;
      this.UseZipHeader = false;
      this.dictionarySize = 23;
      this.positionStateBits = 2;
      this.literalContextBits = 3;
      this.literalPositionBits = 0;
      this.fastBytes = 32;
      this.matchFinderType = LzmaMatchFinderType.BT4;
      this.StreamLength = -1L;
    }

    public int DictionarySize
    {
      get
      {
        return this.dictionarySize;
      }
      set
      {
        this.dictionarySize = value;
        this.OnPropertyChanged(nameof (DictionarySize));
      }
    }

    public int PositionStateBits
    {
      get
      {
        return this.positionStateBits;
      }
      set
      {
        this.positionStateBits = value;
        this.OnPropertyChanged(nameof (PositionStateBits));
      }
    }

    public int LiteralContextBits
    {
      get
      {
        return this.literalContextBits;
      }
      set
      {
        this.literalContextBits = value;
        this.OnPropertyChanged(nameof (LiteralContextBits));
      }
    }

    public int LiteralPositionBits
    {
      get
      {
        return this.literalPositionBits;
      }
      set
      {
        this.literalPositionBits = value;
        this.OnPropertyChanged(nameof (LiteralPositionBits));
      }
    }

    public int FastBytes
    {
      get
      {
        return this.fastBytes;
      }
      set
      {
        this.fastBytes = value;
        this.OnPropertyChanged(nameof (FastBytes));
      }
    }

    public LzmaMatchFinderType MatchFinderType
    {
      get
      {
        return this.matchFinderType;
      }
      set
      {
        this.matchFinderType = value;
        this.OnPropertyChanged(nameof (MatchFinderType));
      }
    }

    public long StreamLength { get; set; }

    internal long InternalStreamLength { get; set; }

    internal bool UseZipHeader { get; set; }

    internal override void CopyFrom(CompressionSettings baseSettings)
    {
      LzmaSettings lzmaSettings = baseSettings as LzmaSettings;
      if (lzmaSettings == null)
        return;
      this.UseZipHeader = lzmaSettings.UseZipHeader;
      this.DictionarySize = lzmaSettings.DictionarySize;
      this.PositionStateBits = lzmaSettings.PositionStateBits;
      this.LiteralContextBits = lzmaSettings.LiteralContextBits;
      this.LiteralPositionBits = lzmaSettings.LiteralPositionBits;
      this.FastBytes = lzmaSettings.FastBytes;
      this.MatchFinderType = lzmaSettings.MatchFinderType;
    }

    internal override void PrepareForZip(CentralDirectoryHeader header = null)
    {
      base.PrepareForZip(header);
      this.UseZipHeader = true;
      if (header == null)
        return;
      this.InternalStreamLength = (long) header.UncompressedSize;
    }
  }
}
