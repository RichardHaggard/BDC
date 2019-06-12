// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaLiteralDecoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaLiteralDecoder
  {
    private LzmaLiteralDecoder.InternalDecoder[] coders;
    private int numerPreviousBits;
    private int numberPositionBits;
    private uint positionMask;

    public LzmaLiteralDecoder(int positionBits, int previousBits)
    {
      if (this.coders != null && this.numerPreviousBits == previousBits && this.numberPositionBits == positionBits)
        return;
      this.numberPositionBits = positionBits;
      this.positionMask = (uint) ((1 << positionBits) - 1);
      this.numerPreviousBits = previousBits;
      uint num = (uint) (1 << this.numerPreviousBits + this.numberPositionBits);
      this.coders = new LzmaLiteralDecoder.InternalDecoder[(IntPtr) num];
      for (uint index = 0; index < num; ++index)
        this.coders[(IntPtr) index].Create();
    }

    public void Init()
    {
      uint num = (uint) (1 << this.numerPreviousBits + this.numberPositionBits);
      for (uint index = 0; index < num; ++index)
        this.coders[(IntPtr) index].Init();
    }

    public byte DecodeNormal(LzmaRangeDecoder rangeDecoder, uint position, byte previousByte)
    {
      return this.coders[(IntPtr) this.GetState(position, previousByte)].DecodeNormal(rangeDecoder);
    }

    public byte DecodeWithMatchByte(
      LzmaRangeDecoder rangeDecoder,
      uint pos,
      byte previousByte,
      byte matchByte)
    {
      return this.coders[(IntPtr) this.GetState(pos, previousByte)].DecodeWithMatchByte(rangeDecoder, matchByte);
    }

    private uint GetState(uint position, byte previousByte)
    {
      return (uint) ((((int) position & (int) this.positionMask) << this.numerPreviousBits) + ((int) previousByte >> 8 - this.numerPreviousBits));
    }

    private struct InternalDecoder
    {
      private LzmaRangeBitDecoder[] decoders;

      public void Create()
      {
        this.decoders = new LzmaRangeBitDecoder[768];
      }

      public void Init()
      {
        for (int index = 0; index < 768; ++index)
          this.decoders[index].Init();
      }

      public byte DecodeNormal(LzmaRangeDecoder rangeDecoder)
      {
        uint num = 1;
        do
        {
          num = num << 1 | this.decoders[(IntPtr) num].Decode(rangeDecoder);
        }
        while (num < 256U);
        return (byte) num;
      }

      public byte DecodeWithMatchByte(LzmaRangeDecoder rangeDecoder, byte matchByte)
      {
        uint num1 = 1;
        do
        {
          uint num2 = (uint) ((int) matchByte >> 7 & 1);
          matchByte <<= 1;
          uint num3 = this.decoders[(IntPtr) ((uint) (1 + (int) num2 << 8) + num1)].Decode(rangeDecoder);
          num1 = num1 << 1 | num3;
          if ((int) num2 != (int) num3)
          {
            while (num1 < 256U)
              num1 = num1 << 1 | this.decoders[(IntPtr) num1].Decode(rangeDecoder);
            break;
          }
        }
        while (num1 < 256U);
        return (byte) num1;
      }
    }
  }
}
