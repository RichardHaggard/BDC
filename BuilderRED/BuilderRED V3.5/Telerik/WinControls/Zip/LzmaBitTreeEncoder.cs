// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaBitTreeEncoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal struct LzmaBitTreeEncoder
  {
    private LzmaRangeBitEncoder[] models;
    private int bitLevels;

    public LzmaBitTreeEncoder(int bitLevelsNumber)
    {
      this.bitLevels = bitLevelsNumber;
      this.models = new LzmaRangeBitEncoder[1 << this.bitLevels];
    }

    public static uint ReverseGetPrice(
      LzmaRangeBitEncoder[] models,
      uint startIndex,
      int bitLevelsNumber,
      uint symbol)
    {
      uint num1 = 0;
      uint num2 = 1;
      for (int index = bitLevelsNumber; index > 0; --index)
      {
        uint symbol1 = symbol & 1U;
        symbol >>= 1;
        num1 += models[(IntPtr) (startIndex + num2)].GetPrice(symbol1);
        num2 = num2 << 1 | symbol1;
      }
      return num1;
    }

    public static void ReverseEncode(
      LzmaRangeBitEncoder[] models,
      uint startIndex,
      LzmaRangeEncoder rangeEncoder,
      int bitLevelsNumber,
      uint symbol)
    {
      uint num = 1;
      for (int index = 0; index < bitLevelsNumber; ++index)
      {
        uint symbol1 = symbol & 1U;
        models[(IntPtr) (startIndex + num)].Encode(rangeEncoder, symbol1);
        num = num << 1 | symbol1;
        symbol >>= 1;
      }
    }

    public void Init()
    {
      for (uint index = 1; (long) index < (long) (1 << this.bitLevels); ++index)
        this.models[(IntPtr) index].Init();
    }

    public void Encode(LzmaRangeEncoder rangeEncoder, uint symbol)
    {
      uint num = 1;
      int bitLevels = this.bitLevels;
      while (bitLevels > 0)
      {
        --bitLevels;
        uint symbol1 = symbol >> bitLevels & 1U;
        this.models[(IntPtr) num].Encode(rangeEncoder, symbol1);
        num = num << 1 | symbol1;
      }
    }

    public void ReverseEncode(LzmaRangeEncoder rangeEncoder, uint symbol)
    {
      uint num = 1;
      for (uint index = 0; (long) index < (long) this.bitLevels; ++index)
      {
        uint symbol1 = symbol & 1U;
        this.models[(IntPtr) num].Encode(rangeEncoder, symbol1);
        num = num << 1 | symbol1;
        symbol >>= 1;
      }
    }

    public uint GetPrice(uint symbol)
    {
      uint num1 = 0;
      uint num2 = 1;
      int bitLevels = this.bitLevels;
      while (bitLevels > 0)
      {
        --bitLevels;
        uint symbol1 = symbol >> bitLevels & 1U;
        num1 += this.models[(IntPtr) num2].GetPrice(symbol1);
        num2 = (num2 << 1) + symbol1;
      }
      return num1;
    }

    public uint ReverseGetPrice(uint symbol)
    {
      uint num1 = 0;
      uint num2 = 1;
      for (int bitLevels = this.bitLevels; bitLevels > 0; --bitLevels)
      {
        uint symbol1 = symbol & 1U;
        symbol >>= 1;
        num1 += this.models[(IntPtr) num2].GetPrice(symbol1);
        num2 = num2 << 1 | symbol1;
      }
      return num1;
    }
  }
}
