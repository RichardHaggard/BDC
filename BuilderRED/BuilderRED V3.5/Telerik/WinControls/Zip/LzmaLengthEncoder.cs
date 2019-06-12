// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaLengthEncoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaLengthEncoder
  {
    private LzmaRangeBitEncoder choice = new LzmaRangeBitEncoder();
    private LzmaRangeBitEncoder choice2 = new LzmaRangeBitEncoder();
    private LzmaBitTreeEncoder[] lowCoder = new LzmaBitTreeEncoder[new IntPtr(16)];
    private LzmaBitTreeEncoder[] middleCoder = new LzmaBitTreeEncoder[new IntPtr(16)];
    private LzmaBitTreeEncoder highCoder = new LzmaBitTreeEncoder(8);

    public LzmaLengthEncoder()
    {
      for (uint index = 0; index < 16U; ++index)
      {
        this.lowCoder[(IntPtr) index] = new LzmaBitTreeEncoder(3);
        this.middleCoder[(IntPtr) index] = new LzmaBitTreeEncoder(3);
      }
    }

    public void Init(uint posStates)
    {
      this.choice.Init();
      this.choice2.Init();
      for (uint index = 0; index < posStates; ++index)
      {
        this.lowCoder[(IntPtr) index].Init();
        this.middleCoder[(IntPtr) index].Init();
      }
      this.highCoder.Init();
    }

    public void Encode(LzmaRangeEncoder rangeEncoder, uint symbol, uint posState)
    {
      if (symbol < 8U)
      {
        this.choice.Encode(rangeEncoder, 0U);
        this.lowCoder[(IntPtr) posState].Encode(rangeEncoder, symbol);
      }
      else
      {
        symbol -= 8U;
        this.choice.Encode(rangeEncoder, 1U);
        if (symbol < 8U)
        {
          this.choice2.Encode(rangeEncoder, 0U);
          this.middleCoder[(IntPtr) posState].Encode(rangeEncoder, symbol);
        }
        else
        {
          this.choice2.Encode(rangeEncoder, 1U);
          this.highCoder.Encode(rangeEncoder, symbol - 8U);
        }
      }
    }

    public void SetPrices(uint posState, uint symbolsCounter, uint[] prices, uint st)
    {
      uint price0 = this.choice.GetPrice0();
      uint price1 = this.choice.GetPrice1();
      uint num1 = price1 + this.choice2.GetPrice0();
      uint num2 = price1 + this.choice2.GetPrice1();
      uint symbol;
      for (symbol = 0U; symbol < 8U; ++symbol)
      {
        if (symbol >= symbolsCounter)
          return;
        prices[(IntPtr) (st + symbol)] = price0 + this.lowCoder[(IntPtr) posState].GetPrice(symbol);
      }
      for (; symbol < 16U; ++symbol)
      {
        if (symbol >= symbolsCounter)
          return;
        prices[(IntPtr) (st + symbol)] = num1 + this.middleCoder[(IntPtr) posState].GetPrice(symbol - 8U);
      }
      for (; symbol < symbolsCounter; ++symbol)
      {
        uint price = this.highCoder.GetPrice((uint) ((int) symbol - 8 - 8));
        prices[(IntPtr) (st + symbol)] = num2 + price;
      }
    }
  }
}
