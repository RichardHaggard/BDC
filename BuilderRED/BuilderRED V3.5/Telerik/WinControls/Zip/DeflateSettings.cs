// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DeflateSettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  public class DeflateSettings : CompressionSettings
  {
    private CompressionLevel compressionLevel;
    private CompressedStreamHeader headerType;

    public DeflateSettings()
    {
      this.Method = CompressionMethod.Deflate;
      this.CompressionLevel = CompressionLevel.Optimal;
      this.HeaderType = CompressedStreamHeader.ZLib;
    }

    public CompressionLevel CompressionLevel
    {
      get
      {
        return this.compressionLevel;
      }
      set
      {
        this.compressionLevel = value;
        this.OnPropertyChanged(nameof (CompressionLevel));
      }
    }

    public CompressedStreamHeader HeaderType
    {
      get
      {
        return this.headerType;
      }
      set
      {
        this.headerType = value;
        this.OnPropertyChanged(nameof (HeaderType));
      }
    }

    internal override void CopyFrom(CompressionSettings baseSettings)
    {
      DeflateSettings deflateSettings = baseSettings as DeflateSettings;
      if (deflateSettings == null)
        return;
      this.HeaderType = deflateSettings.HeaderType;
      this.CompressionLevel = deflateSettings.CompressionLevel;
    }

    internal override void PrepareForZip(CentralDirectoryHeader header = null)
    {
      base.PrepareForZip(header);
      this.HeaderType = CompressedStreamHeader.None;
    }
  }
}
