// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaRangeBitEncoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal struct LzmaRangeBitEncoder
  {
    private static uint[] prices = new uint[new IntPtr(512)];
    public const int BitPriceShiftBits = 6;
    private const int BitModelTotalBits = 11;
    private const uint BitModelTotal = 2048;
    private const int MoveBitsNumber = 5;
    private const int MoveReducingBitsNumber = 2;
    private const int BitsNumber = 9;
    private uint probability;

    static LzmaRangeBitEncoder()
    {
      for (int index1 = 8; index1 >= 0; --index1)
      {
        uint num1 = (uint) (1 << 9 - index1 - 1);
        uint num2 = (uint) (1 << 9 - index1);
        for (uint index2 = num1; index2 < num2; ++index2)
          LzmaRangeBitEncoder.prices[(IntPtr) index2] = (uint) (index1 << 6) + ((uint) ((int) num2 - (int) index2 << 6) >> 9 - index1 - 1);
      }
    }

    public void Init()
    {
      this.probability = 1024U;
    }

    public void Encode(LzmaRangeEncoder encoder, uint symbol)
    {
      uint num = (encoder.Range >> 11) * this.probability;
      if (symbol == 0U)
      {
        encoder.Range = num;
        this.probability += 2048U - this.probability >> 5;
      }
      else
      {
        encoder.Low += (ulong) num;
        encoder.Range -= num;
        this.probability -= this.probability >> 5;
      }
      if (encoder.Range >= 16777216U)
        return;
      encoder.Range <<= 8;
      encoder.ShiftLow();
    }

    public uint GetPrice(uint symbol)
    {
      return LzmaRangeBitEncoder.prices[(((long) (this.probability - symbol) ^ (long) -(int) symbol) & 2047L) >> 2];
    }

    public uint GetPrice0()
    {
      return LzmaRangeBitEncoder.prices[(IntPtr) (this.probability >> 2)];
    }

    public uint GetPrice1()
    {
      return LzmaRangeBitEncoder.prices[(IntPtr) (2048U - this.probability >> 2)];
    }
  }
}
