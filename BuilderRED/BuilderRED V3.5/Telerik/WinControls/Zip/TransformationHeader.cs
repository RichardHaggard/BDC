// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.TransformationHeader
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  public class TransformationHeader
  {
    internal TransformationHeader()
    {
      this.Buffer = (byte[]) null;
      this.BytesToRead = 0;
    }

    public byte[] Buffer { get; set; }

    public int BytesToRead { get; set; }

    public byte[] InitData { get; set; }

    public int Length { get; internal set; }

    public bool CountHeaderInCompressedSize { get; set; }
  }
}
