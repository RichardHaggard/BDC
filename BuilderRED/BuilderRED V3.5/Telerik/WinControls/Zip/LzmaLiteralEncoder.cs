// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaLiteralEncoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaLiteralEncoder
  {
    private LzmaLiteralEncoder.Encoder[] encoders;
    private int previousBits;
    private int positionBits;
    private uint positionMask;

    public LzmaLiteralEncoder(int posBits, int previousBits)
    {
      this.positionBits = posBits;
      this.positionMask = (uint) ((1 << posBits) - 1);
      this.previousBits = previousBits;
      uint num = (uint) (1 << this.previousBits + this.positionBits);
      this.encoders = new LzmaLiteralEncoder.Encoder[(IntPtr) num];
      for (uint index = 0; index < num; ++index)
        this.encoders[(IntPtr) index].Create();
      for (uint index = 0; index < num; ++index)
        this.encoders[(IntPtr) index].Init();
    }

    public LzmaLiteralEncoder.Encoder GetSubCoder(
      uint position,
      byte previousByte)
    {
      return this.encoders[(IntPtr) (uint) ((((int) position & (int) this.positionMask) << this.previousBits) + ((int) previousByte >> 8 - this.previousBits))];
    }

    public struct Encoder
    {
      internal LzmaRangeBitEncoder[] Encoders { get; private set; }

      public void Create()
      {
        this.Encoders = new LzmaRangeBitEncoder[768];
      }

      public void Init()
      {
        for (int index = 0; index < 768; ++index)
          this.Encoders[index].Init();
      }

      public void Encode(LzmaRangeEncoder rangeEncoder, byte symbol)
      {
        uint num = 1;
        for (int index = 7; index >= 0; --index)
        {
          uint symbol1 = (uint) ((int) symbol >> index & 1);
          this.Encoders[(IntPtr) num].Encode(rangeEncoder, symbol1);
          num = num << 1 | symbol1;
        }
      }

      public void EncodeMatched(LzmaRangeEncoder rangeEncoder, byte matchByte, byte symbol)
      {
        uint num1 = 1;
        bool flag = true;
        for (int index = 7; index >= 0; --index)
        {
          uint symbol1 = (uint) ((int) symbol >> index & 1);
          uint num2 = num1;
          if (flag)
          {
            uint num3 = (uint) ((int) matchByte >> index & 1);
            num2 += (uint) (1 + (int) num3 << 8);
            flag = (int) num3 == (int) symbol1;
          }
          this.Encoders[(IntPtr) num2].Encode(rangeEncoder, symbol1);
          num1 = num1 << 1 | symbol1;
        }
      }

      public uint GetPrice(bool matchMode, byte matchByte, byte symbol)
      {
        uint num1 = 0;
        uint num2 = 1;
        int num3 = 7;
        if (matchMode)
        {
          for (; num3 >= 0; --num3)
          {
            uint num4 = (uint) ((int) matchByte >> num3 & 1);
            uint symbol1 = (uint) ((int) symbol >> num3 & 1);
            num1 += this.Encoders[(IntPtr) ((uint) (1 + (int) num4 << 8) + num2)].GetPrice(symbol1);
            num2 = num2 << 1 | symbol1;
            if ((int) num4 != (int) symbol1)
            {
              --num3;
              break;
            }
          }
        }
        for (; num3 >= 0; --num3)
        {
          uint symbol1 = (uint) ((int) symbol >> num3 & 1);
          num1 += this.Encoders[(IntPtr) num2].GetPrice(symbol1);
          num2 = num2 << 1 | symbol1;
        }
        return num1;
      }
    }
  }
}
