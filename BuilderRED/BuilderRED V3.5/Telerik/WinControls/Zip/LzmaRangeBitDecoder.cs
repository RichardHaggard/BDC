// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaRangeBitDecoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal struct LzmaRangeBitDecoder
  {
    private const int ModelTotalBits = 11;
    private const uint ModelTotal = 2048;
    private const int MoveBits = 5;
    private uint bitMask;
    private uint savedBitMask;

    public void Init()
    {
      this.bitMask = 1024U;
    }

    public uint DecodeState(LzmaRangeDecoder rangeDecoder)
    {
      rangeDecoder.SaveState();
      uint num = this.Decode(rangeDecoder);
      if (rangeDecoder.InputRequired)
        rangeDecoder.RestoreState();
      return num;
    }

    public uint Decode(LzmaRangeDecoder rangeDecoder)
    {
      uint newBound = (rangeDecoder.Range >> 11) * this.bitMask;
      this.SaveState();
      uint num;
      if (rangeDecoder.Code < newBound)
      {
        this.bitMask += 2048U - this.bitMask >> 5;
        rangeDecoder.UpdateRange(newBound);
        num = 0U;
      }
      else
      {
        this.bitMask -= this.bitMask >> 5;
        rangeDecoder.MoveRange(newBound);
        num = 1U;
      }
      if (rangeDecoder.InputRequired)
        this.RestoreState();
      return num;
    }

    public void RestoreState()
    {
      this.bitMask = this.savedBitMask;
    }

    private void SaveState()
    {
      this.savedBitMask = this.bitMask;
    }
  }
}
