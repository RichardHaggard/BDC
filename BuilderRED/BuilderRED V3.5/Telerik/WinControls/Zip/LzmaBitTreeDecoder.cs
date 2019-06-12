// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaBitTreeDecoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaBitTreeDecoder
  {
    private LzmaRangeBitDecoder[] models;
    private int bitLevels;

    public LzmaBitTreeDecoder(int levels)
    {
      this.bitLevels = levels;
      this.models = new LzmaRangeBitDecoder[1 << this.bitLevels];
    }

    public static uint ReverseDecode(
      LzmaRangeBitDecoder[] models,
      uint startIndex,
      LzmaRangeDecoder rangeDecoder,
      int numBitLevels)
    {
      uint num1 = 1;
      uint num2 = 0;
      if (rangeDecoder.CheckInputRequired(numBitLevels, false))
      {
        for (int index = 0; index < numBitLevels; ++index)
        {
          uint num3 = models[(IntPtr) (startIndex + num1)].Decode(rangeDecoder);
          if (!rangeDecoder.InputRequired)
          {
            num1 = (num1 << 1) + num3;
            num2 |= num3 << index;
          }
          else
            break;
        }
      }
      return num2;
    }

    public void Init()
    {
      int num = 1 << this.bitLevels;
      for (uint index = 1; (long) index < (long) num; ++index)
        this.models[(IntPtr) index].Init();
    }

    public uint Decode(LzmaRangeDecoder rangeDecoder)
    {
      uint num = 1;
      if (rangeDecoder.CheckInputRequired(this.bitLevels, false))
      {
        for (int bitLevels = this.bitLevels; bitLevels > 0; --bitLevels)
          num = (num << 1) + this.models[(IntPtr) num].Decode(rangeDecoder);
      }
      return num - (uint) (1 << this.bitLevels);
    }

    public uint ReverseDecode(LzmaRangeDecoder rangeDecoder)
    {
      return LzmaBitTreeDecoder.ReverseDecode(this.models, 0U, rangeDecoder, this.bitLevels);
    }
  }
}
