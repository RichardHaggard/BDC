// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaLengthDecoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaLengthDecoder
  {
    private LzmaRangeBitDecoder choice = new LzmaRangeBitDecoder();
    private LzmaRangeBitDecoder choice2 = new LzmaRangeBitDecoder();
    private LzmaBitTreeDecoder[] lowCoder = new LzmaBitTreeDecoder[new IntPtr(16)];
    private LzmaBitTreeDecoder[] middleCoder = new LzmaBitTreeDecoder[new IntPtr(16)];
    private LzmaBitTreeDecoder highCoder = new LzmaBitTreeDecoder(8);
    private uint numberPositionStates;

    public LzmaLengthDecoder(uint positionStates)
    {
      for (uint numberPositionStates = this.numberPositionStates; numberPositionStates < positionStates; ++numberPositionStates)
      {
        this.lowCoder[(IntPtr) numberPositionStates] = new LzmaBitTreeDecoder(3);
        this.middleCoder[(IntPtr) numberPositionStates] = new LzmaBitTreeDecoder(3);
      }
      this.numberPositionStates = positionStates;
    }

    public void Init()
    {
      this.choice.Init();
      for (uint index = 0; index < this.numberPositionStates; ++index)
      {
        this.lowCoder[(IntPtr) index].Init();
        this.middleCoder[(IntPtr) index].Init();
      }
      this.choice2.Init();
      this.highCoder.Init();
    }

    public uint Decode(LzmaRangeDecoder rangeDecoder, uint positionState)
    {
      rangeDecoder.SaveState();
      uint num1 = this.choice.Decode(rangeDecoder);
      if (rangeDecoder.InputRequired)
        return 0;
      uint num2;
      if (num1 == 0U)
      {
        num2 = this.lowCoder[(IntPtr) positionState].Decode(rangeDecoder);
      }
      else
      {
        num2 = 8U;
        uint num3 = this.choice2.Decode(rangeDecoder);
        if (!rangeDecoder.InputRequired)
        {
          if (num3 == 0U)
            num2 += this.middleCoder[(IntPtr) positionState].Decode(rangeDecoder);
          else
            num2 = num2 + 8U + this.highCoder.Decode(rangeDecoder);
        }
        if (rangeDecoder.InputRequired)
          this.choice2.RestoreState();
      }
      if (rangeDecoder.InputRequired)
      {
        this.choice.RestoreState();
        rangeDecoder.RestoreState();
      }
      return num2;
    }
  }
}
