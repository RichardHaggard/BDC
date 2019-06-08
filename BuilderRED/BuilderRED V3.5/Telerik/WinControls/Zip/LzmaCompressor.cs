// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaCompressor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaCompressor : LzmaTransformBase
  {
    private LzmaState compressorState = new LzmaState();
    private uint[] repeaterDistances = new uint[new IntPtr(4)];
    private LzmaOptimizationData[] optimizationData = new LzmaOptimizationData[new IntPtr(4096)];
    private LzmaRangeEncoder rangeEncoder = new LzmaRangeEncoder();
    private LzmaRangeBitEncoder[] matchRangeBitEncoders = new LzmaRangeBitEncoder[new IntPtr(192)];
    private LzmaRangeBitEncoder[] repeaterRangeBitEncoders = new LzmaRangeBitEncoder[new IntPtr(12)];
    private LzmaRangeBitEncoder[] repeaterG0RangeBitEncoders = new LzmaRangeBitEncoder[new IntPtr(12)];
    private LzmaRangeBitEncoder[] repeaterG1RangeBitEncoders = new LzmaRangeBitEncoder[new IntPtr(12)];
    private LzmaRangeBitEncoder[] repeaterG2RangeBitEncoders = new LzmaRangeBitEncoder[new IntPtr(12)];
    private LzmaRangeBitEncoder[] repeaterLongRangeBitEncoders = new LzmaRangeBitEncoder[new IntPtr(192)];
    private LzmaBitTreeEncoder[] positionSlotEncoder = new LzmaBitTreeEncoder[new IntPtr(4)];
    private LzmaRangeBitEncoder[] positionEncoders = new LzmaRangeBitEncoder[new IntPtr(114)];
    private LzmaBitTreeEncoder positionAlignEncoder = new LzmaBitTreeEncoder(4);
    private LzmaLengthTableEncoder lengthEncoder = new LzmaLengthTableEncoder();
    private LzmaLengthTableEncoder repeaterMatchLengthEncoder = new LzmaLengthTableEncoder();
    private uint[] matchDistances = new uint[new IntPtr(548)];
    private uint configFastBytes = 32;
    private uint[] positionSlotPrices = new uint[256];
    private uint[] distancesPrices = new uint[new IntPtr(512)];
    private uint[] alignPrices = new uint[new IntPtr(16)];
    private LzmaMatchFinderType matchFinderType = LzmaMatchFinderType.BT4;
    private uint[] repeaters = new uint[new IntPtr(4)];
    private uint[] repeaterLengths = new uint[new IntPtr(4)];
    private uint[] tempPrices = new uint[new IntPtr(128)];
    private static byte[] fastPositions = new byte[2048];
    public const uint OptimumsNumber = 4096;
    private const int MinBlockSize = 8192;
    private const int MaxBlockSize = 262144;
    private const int DefaultDictionaryLogSize = 22;
    private const int DictionaryMaxCompressValue = 27;
    private const uint FastBytesDefaultNumber = 32;
    private const uint LengthSpecialSymbolsNumber = 16;
    private const uint InfinityPrice = 268435455;
    private const byte FastSlotsNumber = 22;
    private bool finished;
    private byte currentPreviousByte;
    private LzmaBinaryTree matchFinder;
    private LzmaLiteralEncoder literalEncoder;
    private uint longestMatchLength;
    private uint distancePairsNumber;
    private uint additionalOffset;
    private uint optimumEndIndex;
    private uint optimumCurrentIndex;
    private bool longestMatchWasFound;
    private uint alignPriceCount;
    private uint configDistanceTableSize;
    private int configPositionStateBits;
    private uint configPositionStateMask;
    private int configLiteralPositionStateBits;
    private int configLiteralContextBits;
    private uint configDictionarySize;
    private long currentPosition;
    private LzmaState currentState;
    private uint matchPriceCount;
    private uint optimumPosition;
    private uint optimumLength;
    private uint optimumLengthEnd;
    private uint optimumNewLength;
    private uint repeaterMaxIndex;
    private uint distancePairsCounter;
    private uint optimumCurrent;
    private uint optimumAvailableBytesFull;
    private uint optimumPositionState;

    static LzmaCompressor()
    {
      int index1 = 2;
      LzmaCompressor.fastPositions[0] = (byte) 0;
      LzmaCompressor.fastPositions[1] = (byte) 1;
      for (byte index2 = 2; index2 < (byte) 22; ++index2)
      {
        uint num1 = (uint) (1 << ((int) index2 >> 1) - 1);
        uint num2 = 0;
        while (num2 < num1)
        {
          LzmaCompressor.fastPositions[index1] = index2;
          ++num2;
          ++index1;
        }
      }
    }

    public LzmaCompressor(LzmaSettings settings)
      : base(settings)
    {
      for (int index = 0; index < 4096; ++index)
        this.optimizationData[index] = new LzmaOptimizationData();
      for (int index = 0; index < 4; ++index)
        this.positionSlotEncoder[index] = new LzmaBitTreeEncoder(6);
      this.Initialize();
    }

    public override int OutputBlockSize
    {
      get
      {
        int configDictionarySize = (int) this.configDictionarySize;
        if (configDictionarySize < 8192)
          return 8192;
        if (configDictionarySize > 262144)
          return 262144;
        return configDictionarySize;
      }
    }

    public override void CreateHeader()
    {
      byte[] byteHeader;
      if (this.Settings.UseZipHeader)
      {
        byteHeader = new byte[9]
        {
          (byte) 9,
          (byte) 20,
          (byte) 5,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0
        };
        this.CreatePropertiesHeader(byteHeader, 4);
        this.Header.CountHeaderInCompressedSize = true;
      }
      else
      {
        byteHeader = new byte[13]
        {
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue
        };
        if (this.Settings.StreamLength > -1L)
        {
          byte[] bytes = BitConverter.GetBytes(this.Settings.StreamLength);
          Array.Copy((Array) bytes, 0, (Array) byteHeader, 5, bytes.Length);
        }
        this.CreatePropertiesHeader(byteHeader, 0);
      }
      this.Header.Buffer = byteHeader;
    }

    protected override bool ProcessTransform(bool finalBlock)
    {
      this.rangeEncoder.SetOutputBuffer(this.OutputBuffer, this.NextOut);
      if (!this.finished)
      {
        this.matchFinder.SetInputBuffer(this.InputBuffer, this.NextIn, this.AvailableBytesIn, finalBlock);
        this.CodeOneBlock(finalBlock);
      }
      this.NextOut = this.rangeEncoder.NextOut;
      if (!this.rangeEncoder.HasData)
        return this.matchFinder.HasInput;
      return true;
    }

    protected override void Dispose(bool disposing)
    {
      this.rangeEncoder.Dispose();
      base.Dispose(disposing);
    }

    private static uint GetPosSlot(uint index)
    {
      if (index < 2048U)
        return (uint) LzmaCompressor.fastPositions[(IntPtr) index];
      if (index < 2097152U)
        return (uint) LzmaCompressor.fastPositions[(IntPtr) (index >> 10)] + 20U;
      return (uint) LzmaCompressor.fastPositions[(IntPtr) (index >> 20)] + 40U;
    }

    private static uint GetPosSlot2(uint index)
    {
      if (index < 131072U)
        return (uint) LzmaCompressor.fastPositions[(IntPtr) (index >> 6)] + 12U;
      if (index < 134217728U)
        return (uint) LzmaCompressor.fastPositions[(IntPtr) (index >> 16)] + 32U;
      return (uint) LzmaCompressor.fastPositions[(IntPtr) (index >> 26)] + 52U;
    }

    private static void CheckPrice(
      LzmaOptimizationData optimum,
      uint curAndLenPrice,
      uint current,
      uint backPrev)
    {
      if (curAndLenPrice >= optimum.Price)
        return;
      optimum.Price = curAndLenPrice;
      optimum.PosPrev = current;
      optimum.BackPrev = backPrev;
      optimum.Prev1IsChar = false;
    }

    private void InitRepeaterDistances()
    {
      this.currentPreviousByte = (byte) 0;
      for (uint index = 0; index < 4U; ++index)
        this.repeaterDistances[(IntPtr) index] = 0U;
    }

    private void CreatePropertiesHeader(byte[] byteHeader, int propertiesIndex)
    {
      byteHeader[propertiesIndex] = (byte) ((this.configPositionStateBits * 5 + this.configLiteralPositionStateBits) * 9 + this.configLiteralContextBits);
      ++propertiesIndex;
      for (int index = 0; index < 4; ++index)
        byteHeader[propertiesIndex + index] = (byte) (this.configDictionarySize >> 8 * index & (uint) byte.MaxValue);
    }

    private void InitEncoders()
    {
      this.InitRepeaterDistances();
      for (uint index1 = 0; index1 < 12U; ++index1)
      {
        for (uint index2 = 0; index2 <= this.configPositionStateMask; ++index2)
        {
          uint num = (index1 << 4) + index2;
          this.matchRangeBitEncoders[(IntPtr) num].Init();
          this.repeaterLongRangeBitEncoders[(IntPtr) num].Init();
        }
        this.repeaterRangeBitEncoders[(IntPtr) index1].Init();
        this.repeaterG0RangeBitEncoders[(IntPtr) index1].Init();
        this.repeaterG1RangeBitEncoders[(IntPtr) index1].Init();
        this.repeaterG2RangeBitEncoders[(IntPtr) index1].Init();
      }
      for (uint index = 0; index < 4U; ++index)
        this.positionSlotEncoder[(IntPtr) index].Init();
      for (uint index = 0; index < 114U; ++index)
        this.positionEncoders[(IntPtr) index].Init();
      this.lengthEncoder.Init((uint) (1 << this.configPositionStateBits));
      this.repeaterMatchLengthEncoder.Init((uint) (1 << this.configPositionStateBits));
      this.positionAlignEncoder.Init();
      this.longestMatchWasFound = false;
      this.optimumEndIndex = 0U;
      this.optimumCurrentIndex = 0U;
      this.additionalOffset = 0U;
    }

    private void ReadMatchDistances(out uint length)
    {
      length = 0U;
      this.distancePairsCounter = this.matchFinder.GetMatches(this.matchDistances);
      uint distancePairsCounter = this.distancePairsCounter;
      if (distancePairsCounter > 0U)
      {
        length = this.matchDistances[(IntPtr) (distancePairsCounter - 2U)];
        if ((int) length == (int) this.configFastBytes)
          length += this.matchFinder.GetMatchLen((int) length - 1, this.matchDistances[(IntPtr) (distancePairsCounter - 1U)], 273U - length);
      }
      ++this.additionalOffset;
    }

    private void MovePosition(uint counter)
    {
      if (counter <= 0U)
        return;
      this.matchFinder.Skip(counter);
      this.additionalOffset += counter;
    }

    private uint GetRepeaterLen1Price(LzmaState state, uint positionState)
    {
      uint num = (state.Index << 4) + positionState;
      return this.repeaterG0RangeBitEncoders[(IntPtr) state.Index].GetPrice0() + this.repeaterLongRangeBitEncoders[(IntPtr) num].GetPrice0();
    }

    private uint GetPureRepeaterPrice(uint index, LzmaState state, uint positionState)
    {
      uint num;
      if (index == 0U)
      {
        num = this.repeaterG0RangeBitEncoders[(IntPtr) state.Index].GetPrice0() + this.repeaterLongRangeBitEncoders[(IntPtr) ((state.Index << 4) + positionState)].GetPrice1();
      }
      else
      {
        uint price1 = this.repeaterG0RangeBitEncoders[(IntPtr) state.Index].GetPrice1();
        num = index != 1U ? price1 + this.repeaterG1RangeBitEncoders[(IntPtr) state.Index].GetPrice1() + this.repeaterG2RangeBitEncoders[(IntPtr) state.Index].GetPrice(index - 2U) : price1 + this.repeaterG1RangeBitEncoders[(IntPtr) state.Index].GetPrice0();
      }
      return num;
    }

    private uint GetRepeaterPrice(uint index, uint length, LzmaState state, uint positionState)
    {
      return this.repeaterMatchLengthEncoder.GetPrice(length - 2U, positionState) + this.GetPureRepeaterPrice(index, state, positionState);
    }

    private uint GetPosLenPrice(uint position, uint length, uint positionState)
    {
      uint lenToPosState = LzmaState.GetLenToPosState(length);
      return (position >= 128U ? this.positionSlotPrices[(IntPtr) ((lenToPosState << 6) + LzmaCompressor.GetPosSlot2(position))] + this.alignPrices[(IntPtr) (position & 15U)] : this.distancesPrices[(IntPtr) (lenToPosState * 128U + position)]) + this.lengthEncoder.GetPrice(length - 2U, positionState);
    }

    private void Backward(uint current)
    {
      this.optimumEndIndex = current;
      uint posPrev = this.optimizationData[(IntPtr) current].PosPrev;
      uint backPrev = this.optimizationData[(IntPtr) current].BackPrev;
      do
      {
        if (this.optimizationData[(IntPtr) current].Prev1IsChar)
        {
          this.optimizationData[(IntPtr) posPrev].MakeAsChar();
          this.optimizationData[(IntPtr) posPrev].PosPrev = posPrev - 1U;
          if (this.optimizationData[(IntPtr) current].Prev2)
          {
            this.optimizationData[(IntPtr) (posPrev - 1U)].Prev1IsChar = false;
            this.optimizationData[(IntPtr) (posPrev - 1U)].PosPrev = this.optimizationData[(IntPtr) current].PosPrev2;
            this.optimizationData[(IntPtr) (posPrev - 1U)].BackPrev = this.optimizationData[(IntPtr) current].BackPrev2;
          }
        }
        uint num1 = posPrev;
        uint num2 = backPrev;
        backPrev = this.optimizationData[(IntPtr) num1].BackPrev;
        posPrev = this.optimizationData[(IntPtr) num1].PosPrev;
        this.optimizationData[(IntPtr) num1].BackPrev = num2;
        this.optimizationData[(IntPtr) num1].PosPrev = current;
        current = num1;
      }
      while (current > 0U);
      this.optimumPosition = this.optimizationData[0].BackPrev;
      this.optimumLength = this.optimumCurrentIndex = this.optimizationData[0].PosPrev;
    }

    private void GetOptimum(uint position)
    {
      this.optimumPosition = position;
      this.optimumPositionState = position & this.configPositionStateMask;
      if (this.CheckOptimumIndex())
        return;
      this.optimumCurrent = 0U;
      while (true)
      {
        ++this.optimumCurrent;
        ++this.optimumPosition;
        this.optimumPositionState = this.optimumPosition & this.configPositionStateMask;
        if (!this.CheckFastBytes())
        {
          this.GetCurrentState();
          this.ProcessOptimum();
        }
        else
          break;
      }
    }

    private void ProcessOptimum()
    {
      uint optimumCurrent = this.optimumCurrent;
      uint optimumPosition = this.optimumPosition;
      uint price = this.optimizationData[(IntPtr) optimumCurrent].Price;
      byte indexByte1 = this.matchFinder.GetIndexByte(-1);
      byte indexByte2 = this.matchFinder.GetIndexByte((int) ((long) -this.repeaters[0] - 2L));
      uint optimumPositionState = this.optimumPositionState;
      uint num1 = (this.currentState.Index << 4) + optimumPositionState;
      LzmaLiteralEncoder.Encoder subCoder = this.literalEncoder.GetSubCoder(optimumPosition, this.matchFinder.GetIndexByte(-2));
      LzmaOptimizationData optimizationData = this.optimizationData[(IntPtr) (optimumCurrent + 1U)];
      uint curAnd1Price = price + this.matchRangeBitEncoders[(IntPtr) num1].GetPrice0() + subCoder.GetPrice(!this.currentState.IsCharState(), indexByte2, indexByte1);
      bool flag = false;
      if (curAnd1Price < optimizationData.Price)
      {
        optimizationData.Price = curAnd1Price;
        optimizationData.PosPrev = optimumCurrent;
        optimizationData.MakeAsChar();
        flag = true;
      }
      uint num2 = (this.currentState.Index << 4) + optimumPositionState;
      uint matchPrice = price + this.matchRangeBitEncoders[(IntPtr) num2].GetPrice1();
      uint repMatchPrice = matchPrice + this.repeaterRangeBitEncoders[(IntPtr) this.currentState.Index].GetPrice1();
      if ((int) indexByte2 == (int) indexByte1 && (optimizationData.PosPrev >= optimumCurrent || optimizationData.BackPrev != 0U))
      {
        uint num3 = repMatchPrice + this.GetRepeaterLen1Price(this.currentState, optimumPositionState);
        if (num3 <= optimizationData.Price)
        {
          optimizationData.Price = num3;
          optimizationData.PosPrev = optimumCurrent;
          optimizationData.MakeAsShortRep();
          flag = true;
        }
      }
      uint availableBytes = this.GetAvailableBytes(optimumCurrent);
      if (availableBytes < 2U)
        return;
      if (!flag && (int) indexByte2 != (int) indexByte1)
        this.TryLiteralRep0(curAnd1Price);
      this.ProcessOptimumDistances(repMatchPrice, availableBytes, matchPrice);
    }

    private uint GetAvailableBytes(uint current)
    {
      this.optimumAvailableBytesFull = this.matchFinder.AvailableBytes + 1U;
      this.optimumAvailableBytesFull = Math.Min(4095U - current, this.optimumAvailableBytesFull);
      uint num = this.optimumAvailableBytesFull;
      if (num >= 2U && num > this.configFastBytes)
        num = this.configFastBytes;
      return num;
    }

    private void UpdatePositionLengthPrice(uint startLen, uint matchPrice)
    {
      uint optimumCurrent = this.optimumCurrent;
      uint normalMatchPrice = matchPrice + this.repeaterRangeBitEncoders[(IntPtr) this.currentState.Index].GetPrice0();
      while (this.optimumLengthEnd < optimumCurrent + this.optimumNewLength)
        this.optimizationData[(IntPtr) ++this.optimumLengthEnd].Price = 268435455U;
      this.UpdatePositionLengthPrice(startLen, optimumCurrent, normalMatchPrice);
    }

    private void UpdatePositionLengthPrice(uint startLen, uint current, uint normalMatchPrice)
    {
      uint optimumPositionState = this.optimumPositionState;
      uint num1 = 0;
      while (startLen > this.matchDistances[(IntPtr) num1])
        num1 += 2U;
      uint num2 = startLen;
      while (true)
      {
        uint matchDistance = this.matchDistances[(IntPtr) (num1 + 1U)];
        uint num3 = normalMatchPrice + this.GetPosLenPrice(matchDistance, num2, optimumPositionState);
        LzmaCompressor.CheckPrice(this.optimizationData[(IntPtr) (current + num2)], num3, current, matchDistance + 4U);
        if ((int) num2 == (int) this.matchDistances[(IntPtr) num1])
        {
          this.CheckAvailableLength(num2, matchDistance, num3, matchDistance + 4U, false);
          num1 += 2U;
          if ((int) num1 == (int) this.distancePairsCounter)
            break;
        }
        ++num2;
      }
    }

    private void ProcessOptimumDistances(uint repMatchPrice, uint availableBytes, uint matchPrice)
    {
      uint startLen = 2;
      uint optimumCurrent = this.optimumCurrent;
      uint optimumPositionState = this.optimumPositionState;
      for (uint index = 0; index < 4U; ++index)
      {
        uint matchLen = this.matchFinder.GetMatchLen(-1, this.repeaters[(IntPtr) index], availableBytes);
        if (matchLen >= 2U)
        {
          uint num = matchLen;
          do
          {
            while (this.optimumLengthEnd < optimumCurrent + matchLen)
              this.optimizationData[(IntPtr) ++this.optimumLengthEnd].Price = 268435455U;
            uint curAndLenPrice = repMatchPrice + this.GetRepeaterPrice(index, matchLen, this.currentState, optimumPositionState);
            LzmaCompressor.CheckPrice(this.optimizationData[(IntPtr) (optimumCurrent + matchLen)], curAndLenPrice, optimumCurrent, index);
          }
          while (--matchLen >= 2U);
          uint lenTest = num;
          if (index == 0U)
            startLen = lenTest + 1U;
          this.CheckAvailableLength(lenTest, this.repeaters[(IntPtr) index], repMatchPrice, index, true);
        }
      }
      if (this.optimumNewLength > availableBytes)
      {
        this.optimumNewLength = availableBytes;
        this.distancePairsCounter = 0U;
        while (this.optimumNewLength > this.matchDistances[(IntPtr) this.distancePairsCounter])
          this.distancePairsCounter += 2U;
        this.matchDistances[(IntPtr) this.distancePairsCounter] = this.optimumNewLength;
        this.distancePairsCounter += 2U;
      }
      if (this.optimumNewLength < startLen)
        return;
      this.UpdatePositionLengthPrice(startLen, matchPrice);
    }

    private void TryLiteralRep0(uint curAnd1Price)
    {
      uint num1 = this.optimumCurrent + 1U;
      uint matchLen = this.matchFinder.GetMatchLen(0, this.repeaters[0], Math.Min(this.optimumAvailableBytesFull - 1U, this.configFastBytes));
      if (matchLen < 2U)
        return;
      LzmaState currentState = this.currentState;
      currentState.UpdateChar();
      uint positionState = this.optimumPosition + 1U & this.configPositionStateMask;
      uint num2 = curAnd1Price + this.matchRangeBitEncoders[(IntPtr) ((currentState.Index << 4) + positionState)].GetPrice1() + this.repeaterRangeBitEncoders[(IntPtr) currentState.Index].GetPrice1();
      uint num3 = num1 + matchLen;
      while (this.optimumLengthEnd < num3)
        this.optimizationData[(IntPtr) ++this.optimumLengthEnd].Price = 268435455U;
      uint num4 = num2 + this.GetRepeaterPrice(0U, matchLen, currentState, positionState);
      LzmaOptimizationData optimizationData = this.optimizationData[(IntPtr) num3];
      if (num4 >= optimizationData.Price)
        return;
      optimizationData.Price = num4;
      optimizationData.PosPrev = num1;
      optimizationData.BackPrev = 0U;
      optimizationData.Prev1IsChar = true;
      optimizationData.Prev2 = false;
    }

    private void CheckAvailableLength(
      uint lenTest,
      uint curBack,
      uint startPrice,
      uint repIndex,
      bool repeater = true)
    {
      if (lenTest >= this.optimumAvailableBytesFull)
        return;
      uint optimumPosition = this.optimumPosition;
      uint optimumCurrent = this.optimumCurrent;
      uint optimumPositionState = this.optimumPositionState;
      uint limit = Math.Min(this.optimumAvailableBytesFull - 1U - lenTest, this.configFastBytes);
      uint matchLen = this.matchFinder.GetMatchLen((int) lenTest, curBack, limit);
      if (matchLen < 2U)
        return;
      uint num1 = startPrice;
      LzmaState currentState = this.currentState;
      uint num2 = optimumPosition + lenTest & this.configPositionStateMask;
      uint num3;
      if (repeater)
      {
        currentState.UpdateRep();
        uint num4 = (currentState.Index << 4) + num2;
        num3 = num1 + (this.GetRepeaterPrice(repIndex, lenTest, this.currentState, optimumPositionState) + this.matchRangeBitEncoders[(IntPtr) num4].GetPrice0() + this.literalEncoder.GetSubCoder(optimumPosition + lenTest, this.matchFinder.GetIndexByte((int) lenTest - 2)).GetPrice(true, this.matchFinder.GetIndexByte((int) lenTest - 1 - ((int) curBack + 1)), this.matchFinder.GetIndexByte((int) lenTest - 1)));
      }
      else
      {
        currentState.UpdateMatch();
        uint num4 = (currentState.Index << 4) + num2;
        num3 = num1 + (this.matchRangeBitEncoders[(IntPtr) num4].GetPrice0() + this.literalEncoder.GetSubCoder(optimumPosition + lenTest, this.matchFinder.GetIndexByte((int) lenTest - 2)).GetPrice(true, this.matchFinder.GetIndexByte((int) lenTest - ((int) curBack + 1) - 1), this.matchFinder.GetIndexByte((int) lenTest - 1)));
      }
      currentState.UpdateChar();
      uint positionState = (uint) ((int) optimumPosition + (int) lenTest + 1) & this.configPositionStateMask;
      uint num5 = (currentState.Index << 4) + positionState;
      uint num6 = num3 + this.matchRangeBitEncoders[(IntPtr) num5].GetPrice1() + this.repeaterRangeBitEncoders[(IntPtr) currentState.Index].GetPrice1();
      uint num7 = lenTest + 1U + matchLen;
      while (this.optimumLengthEnd < optimumCurrent + num7)
        this.optimizationData[(IntPtr) ++this.optimumLengthEnd].Price = 268435455U;
      uint num8 = num6 + this.GetRepeaterPrice(0U, matchLen, currentState, positionState);
      LzmaOptimizationData optimizationData = this.optimizationData[(IntPtr) (optimumCurrent + num7)];
      if (num8 >= optimizationData.Price)
        return;
      optimizationData.Price = num8;
      optimizationData.PosPrev = (uint) ((int) optimumCurrent + (int) lenTest + 1);
      optimizationData.BackPrev = 0U;
      optimizationData.Prev1IsChar = true;
      optimizationData.Prev2 = true;
      optimizationData.PosPrev2 = optimumCurrent;
      optimizationData.BackPrev2 = repIndex;
    }

    private LzmaState GetCurrentState()
    {
      uint optimumCurrent = this.optimumCurrent;
      uint posPrev = this.optimizationData[(IntPtr) optimumCurrent].PosPrev;
      if (this.optimizationData[(IntPtr) optimumCurrent].Prev1IsChar)
      {
        --posPrev;
        if (this.optimizationData[(IntPtr) optimumCurrent].Prev2)
        {
          this.currentState = this.optimizationData[(IntPtr) this.optimizationData[(IntPtr) optimumCurrent].PosPrev2].State;
          if (this.optimizationData[(IntPtr) optimumCurrent].BackPrev2 < 4U)
            this.currentState.UpdateRep();
          else
            this.currentState.UpdateMatch();
        }
        else
          this.currentState = this.optimizationData[(IntPtr) posPrev].State;
        this.currentState.UpdateChar();
      }
      else
        this.currentState = this.optimizationData[(IntPtr) posPrev].State;
      this.UpdateCurrentState(posPrev);
      return this.currentState;
    }

    private void UpdateCurrentState(uint previousPosition)
    {
      uint optimumCurrent = this.optimumCurrent;
      if ((int) previousPosition == (int) optimumCurrent - 1)
      {
        if (this.optimizationData[(IntPtr) optimumCurrent].IsShortRep())
          this.currentState.UpdateShortRep();
        else
          this.currentState.UpdateChar();
      }
      else
      {
        uint position;
        if (this.optimizationData[(IntPtr) optimumCurrent].Prev1IsChar && this.optimizationData[(IntPtr) optimumCurrent].Prev2)
        {
          previousPosition = this.optimizationData[(IntPtr) optimumCurrent].PosPrev2;
          position = this.optimizationData[(IntPtr) optimumCurrent].BackPrev2;
          this.currentState.UpdateRep();
        }
        else
        {
          position = this.optimizationData[(IntPtr) optimumCurrent].BackPrev;
          if (position < 4U)
            this.currentState.UpdateRep();
          else
            this.currentState.UpdateMatch();
        }
        this.UpdateRepeaters(position, previousPosition);
      }
      this.optimizationData[(IntPtr) optimumCurrent].State = this.currentState;
      this.optimizationData[(IntPtr) optimumCurrent].Backs0 = this.repeaters[0];
      this.optimizationData[(IntPtr) optimumCurrent].Backs1 = this.repeaters[1];
      this.optimizationData[(IntPtr) optimumCurrent].Backs2 = this.repeaters[2];
      this.optimizationData[(IntPtr) optimumCurrent].Backs3 = this.repeaters[3];
    }

    private void UpdateRepeaters(uint position, uint previousPosition)
    {
      LzmaOptimizationData optimizationData = this.optimizationData[(IntPtr) previousPosition];
      if (position < 4U)
      {
        switch (position)
        {
          case 0:
            this.repeaters[0] = optimizationData.Backs0;
            this.repeaters[1] = optimizationData.Backs1;
            this.repeaters[2] = optimizationData.Backs2;
            this.repeaters[3] = optimizationData.Backs3;
            break;
          case 1:
            this.repeaters[0] = optimizationData.Backs1;
            this.repeaters[1] = optimizationData.Backs0;
            this.repeaters[2] = optimizationData.Backs2;
            this.repeaters[3] = optimizationData.Backs3;
            break;
          case 2:
            this.repeaters[0] = optimizationData.Backs2;
            this.repeaters[1] = optimizationData.Backs0;
            this.repeaters[2] = optimizationData.Backs1;
            this.repeaters[3] = optimizationData.Backs3;
            break;
          default:
            this.repeaters[0] = optimizationData.Backs3;
            this.repeaters[1] = optimizationData.Backs0;
            this.repeaters[2] = optimizationData.Backs1;
            this.repeaters[3] = optimizationData.Backs2;
            break;
        }
      }
      else
      {
        this.repeaters[0] = position - 4U;
        this.repeaters[1] = optimizationData.Backs0;
        this.repeaters[2] = optimizationData.Backs1;
        this.repeaters[3] = optimizationData.Backs2;
      }
    }

    private bool CheckFastBytes()
    {
      uint optimumCurrent = this.optimumCurrent;
      if ((int) optimumCurrent == (int) this.optimumLengthEnd)
      {
        this.Backward(optimumCurrent);
        return true;
      }
      uint length;
      this.ReadMatchDistances(out length);
      this.optimumNewLength = length;
      if (this.optimumNewLength < this.configFastBytes)
        return false;
      this.distancePairsNumber = this.distancePairsCounter;
      this.longestMatchLength = this.optimumNewLength;
      this.longestMatchWasFound = true;
      this.Backward(optimumCurrent);
      return true;
    }

    private bool SetOptimizationData(uint lengthMain)
    {
      byte indexByte1 = this.matchFinder.GetIndexByte(-1);
      byte indexByte2 = this.matchFinder.GetIndexByte(-(int) this.repeaterDistances[0] - 2);
      if (lengthMain < 2U && (int) indexByte1 != (int) indexByte2 && this.repeaterLengths[(IntPtr) this.repeaterMaxIndex] < 2U)
      {
        this.optimumPosition = uint.MaxValue;
        this.optimumLength = 1U;
        return true;
      }
      uint currentPosition = (uint) this.currentPosition;
      uint optimumPositionState = this.optimumPositionState;
      this.optimizationData[0].State = this.compressorState;
      uint num1 = (this.compressorState.Index << 4) + optimumPositionState;
      bool flag = this.compressorState.IsCharState();
      LzmaLiteralEncoder.Encoder subCoder = this.literalEncoder.GetSubCoder(currentPosition, this.currentPreviousByte);
      this.optimizationData[1].Price = this.matchRangeBitEncoders[(IntPtr) num1].GetPrice0() + subCoder.GetPrice(!flag, indexByte2, indexByte1);
      this.optimizationData[1].MakeAsChar();
      uint price1 = this.matchRangeBitEncoders[(IntPtr) ((this.compressorState.Index << 4) + optimumPositionState)].GetPrice1();
      uint repMatchPrice = price1 + this.repeaterRangeBitEncoders[(IntPtr) this.compressorState.Index].GetPrice1();
      if ((int) indexByte2 == (int) indexByte1)
      {
        uint num2 = repMatchPrice + this.GetRepeaterLen1Price(this.compressorState, optimumPositionState);
        if (num2 < this.optimizationData[1].Price)
        {
          this.optimizationData[1].Price = num2;
          this.optimizationData[1].MakeAsShortRep();
        }
      }
      this.optimumLengthEnd = lengthMain >= this.repeaterLengths[(IntPtr) this.repeaterMaxIndex] ? lengthMain : this.repeaterLengths[(IntPtr) this.repeaterMaxIndex];
      if (this.optimumLengthEnd < 2U)
      {
        this.optimumPosition = this.optimizationData[1].BackPrev;
        this.optimumLength = 1U;
        return true;
      }
      this.SetOptimizationData(repMatchPrice, optimumPositionState);
      this.UpdateDistancePairs(price1, lengthMain);
      return false;
    }

    private void UpdateDistancePairs(uint matchPrice, uint lengthMain)
    {
      uint num1 = matchPrice + this.repeaterRangeBitEncoders[(IntPtr) this.compressorState.Index].GetPrice0();
      uint length = this.repeaterLengths[0] >= 2U ? this.repeaterLengths[0] + 1U : 2U;
      if (length > lengthMain)
        return;
      uint optimumPositionState = this.optimumPositionState;
      uint num2 = 0;
      while (length > this.matchDistances[(IntPtr) num2])
        num2 += 2U;
      while (true)
      {
        uint matchDistance = this.matchDistances[(IntPtr) (num2 + 1U)];
        uint curAndLenPrice = num1 + this.GetPosLenPrice(matchDistance, length, optimumPositionState);
        LzmaCompressor.CheckPrice(this.optimizationData[(IntPtr) length], curAndLenPrice, 0U, matchDistance + 4U);
        if ((int) length == (int) this.matchDistances[(IntPtr) num2])
        {
          num2 += 2U;
          if ((int) num2 == (int) this.distancePairsCounter)
            break;
        }
        ++length;
      }
    }

    private void SetOptimizationData(uint repMatchPrice, uint posState)
    {
      this.optimizationData[1].PosPrev = 0U;
      this.optimizationData[0].Backs0 = this.repeaters[0];
      this.optimizationData[0].Backs1 = this.repeaters[1];
      this.optimizationData[0].Backs2 = this.repeaters[2];
      this.optimizationData[0].Backs3 = this.repeaters[3];
      uint optimumLengthEnd = this.optimumLengthEnd;
      do
      {
        this.optimizationData[(IntPtr) optimumLengthEnd--].Price = 268435455U;
      }
      while (optimumLengthEnd >= 2U);
      for (uint index = 0; index < 4U; ++index)
      {
        uint repeaterLength = this.repeaterLengths[(IntPtr) index];
        if (repeaterLength >= 2U)
        {
          uint num = repMatchPrice + this.GetPureRepeaterPrice(index, this.compressorState, posState);
          do
          {
            uint curAndLenPrice = num + this.repeaterMatchLengthEncoder.GetPrice(repeaterLength - 2U, posState);
            LzmaCompressor.CheckPrice(this.optimizationData[(IntPtr) repeaterLength], curAndLenPrice, 0U, index);
          }
          while (--repeaterLength >= 2U);
        }
      }
    }

    private bool CalculateRepeaterMaxIndex(uint lengthMain)
    {
      this.repeaterMaxIndex = 0U;
      for (uint index = 0; index < 4U; ++index)
      {
        this.repeaters[(IntPtr) index] = this.repeaterDistances[(IntPtr) index];
        this.repeaterLengths[(IntPtr) index] = this.matchFinder.GetMatchLen(-1, this.repeaters[(IntPtr) index], 273U);
        if (this.repeaterLengths[(IntPtr) index] > this.repeaterLengths[(IntPtr) this.repeaterMaxIndex])
          this.repeaterMaxIndex = index;
      }
      if (this.repeaterLengths[(IntPtr) this.repeaterMaxIndex] >= this.configFastBytes)
      {
        this.optimumPosition = this.repeaterMaxIndex;
        this.optimumLength = this.repeaterLengths[(IntPtr) this.repeaterMaxIndex];
        this.MovePosition(this.optimumLength - 1U);
        return true;
      }
      if (lengthMain < this.configFastBytes)
        return false;
      this.optimumPosition = this.matchDistances[(IntPtr) (this.distancePairsCounter - 1U)] + 4U;
      this.MovePosition(lengthMain - 1U);
      this.optimumLength = lengthMain;
      return true;
    }

    private bool CheckOptimumIndex()
    {
      if ((int) this.optimumEndIndex != (int) this.optimumCurrentIndex)
      {
        uint optimumCurrentIndex = this.optimumCurrentIndex;
        this.optimumLength = this.optimizationData[(IntPtr) optimumCurrentIndex].PosPrev - optimumCurrentIndex;
        this.optimumPosition = this.optimizationData[(IntPtr) optimumCurrentIndex].BackPrev;
        this.optimumCurrentIndex = this.optimizationData[(IntPtr) optimumCurrentIndex].PosPrev;
        return true;
      }
      this.optimumCurrentIndex = this.optimumEndIndex = 0U;
      uint lenMain;
      if (this.CalculateMatchDistances(out lenMain))
      {
        this.optimumPosition = uint.MaxValue;
        this.optimumLength = 1U;
        return true;
      }
      return this.CalculateRepeaterMaxIndex(lenMain) || this.SetOptimizationData(lenMain);
    }

    private bool CalculateMatchDistances(out uint lenMain)
    {
      if (!this.longestMatchWasFound)
      {
        this.ReadMatchDistances(out lenMain);
      }
      else
      {
        lenMain = this.longestMatchLength;
        this.distancePairsCounter = this.distancePairsNumber;
        this.longestMatchWasFound = false;
      }
      return this.matchFinder.AvailableBytes + 1U < 2U;
    }

    private void WriteEndMarker(uint posState)
    {
      if (this.Settings.UseZipHeader || this.Settings.StreamLength > -1L)
        return;
      this.matchRangeBitEncoders[(IntPtr) ((this.compressorState.Index << 4) + posState)].Encode(this.rangeEncoder, 1U);
      this.repeaterRangeBitEncoders[(IntPtr) this.compressorState.Index].Encode(this.rangeEncoder, 0U);
      this.compressorState.UpdateMatch();
      uint length = 2;
      this.lengthEncoder.Encode(this.rangeEncoder, length - 2U, posState);
      uint symbol = 63;
      this.positionSlotEncoder[(IntPtr) LzmaState.GetLenToPosState(length)].Encode(this.rangeEncoder, symbol);
      int num1 = 30;
      uint num2 = (uint) ((1 << num1) - 1);
      this.rangeEncoder.EncodeDirectBits(num2 >> 4, num1 - 4);
      this.positionAlignEncoder.ReverseEncode(this.rangeEncoder, num2 & 15U);
    }

    private void Flush(bool finalBlock)
    {
      if (!finalBlock)
        return;
      this.finished = true;
      this.WriteEndMarker((uint) this.currentPosition & this.configPositionStateMask);
      this.rangeEncoder.FlushData();
    }

    private void CodeOneBlock(bool finalBlock)
    {
      if (!this.CodeFirstPart(finalBlock))
        return;
      if (this.matchFinder.AvailableBytes == 0U)
      {
        this.Flush(finalBlock);
      }
      else
      {
        bool readyToMove = this.matchFinder.ReadyToMove;
        while (!readyToMove && (this.matchFinder.Ready || finalBlock))
        {
          this.GetOptimum((uint) this.currentPosition);
          uint positionState = (uint) this.currentPosition & this.configPositionStateMask;
          uint complexState = (this.compressorState.Index << 4) + positionState;
          if (this.optimumLength == 1U && this.optimumPosition == uint.MaxValue)
            this.Code0(complexState);
          else
            this.Code1(complexState, positionState);
          if (this.UpdateCodePosition(finalBlock))
            return;
        }
        if (!readyToMove)
          return;
        this.matchFinder.MoveBlock();
      }
    }

    private bool UpdateCodePosition(bool finalBlock)
    {
      uint optimumLength = this.optimumLength;
      this.additionalOffset -= optimumLength;
      this.currentPosition += (long) optimumLength;
      if (this.additionalOffset == 0U)
      {
        if (this.matchPriceCount >= 128U)
          this.FillDistancesPrices();
        if (this.alignPriceCount >= 16U)
          this.FillAlignPrices();
        if (this.matchFinder.AvailableBytes == 0U)
        {
          this.Flush(finalBlock);
          return true;
        }
      }
      return false;
    }

    private void Code1(uint complexState, uint positionState)
    {
      uint optimumPosition = this.optimumPosition;
      uint optimumLength = this.optimumLength;
      this.matchRangeBitEncoders[(IntPtr) complexState].Encode(this.rangeEncoder, 1U);
      if (optimumPosition < 4U)
        this.Code1Encoding(complexState, positionState);
      else
        this.Code0Encoding(positionState);
      this.currentPreviousByte = this.matchFinder.GetIndexByte((int) optimumLength - 1 - (int) this.additionalOffset);
    }

    private void Code0Encoding(uint positionState)
    {
      uint optimumPosition = this.optimumPosition;
      uint optimumLength = this.optimumLength;
      this.repeaterRangeBitEncoders[(IntPtr) this.compressorState.Index].Encode(this.rangeEncoder, 0U);
      this.compressorState.UpdateMatch();
      this.lengthEncoder.Encode(this.rangeEncoder, optimumLength - 2U, positionState);
      uint index1 = optimumPosition - 4U;
      uint posSlot = LzmaCompressor.GetPosSlot(index1);
      this.positionSlotEncoder[(IntPtr) LzmaState.GetLenToPosState(optimumLength)].Encode(this.rangeEncoder, posSlot);
      if (posSlot >= 4U)
      {
        int bitLevelsNumber = (int) (posSlot >> 1) - 1;
        uint num = (uint) ((2 | (int) posSlot & 1) << bitLevelsNumber);
        uint symbol = index1 - num;
        if (posSlot < 14U)
        {
          LzmaBitTreeEncoder.ReverseEncode(this.positionEncoders, (uint) ((int) num - (int) posSlot - 1), this.rangeEncoder, bitLevelsNumber, symbol);
        }
        else
        {
          this.rangeEncoder.EncodeDirectBits(symbol >> 4, bitLevelsNumber - 4);
          this.positionAlignEncoder.ReverseEncode(this.rangeEncoder, symbol & 15U);
          ++this.alignPriceCount;
        }
      }
      uint num1 = index1;
      for (uint index2 = 3; index2 >= 1U; --index2)
        this.repeaterDistances[(IntPtr) index2] = this.repeaterDistances[(IntPtr) (index2 - 1U)];
      this.repeaterDistances[0] = num1;
      ++this.matchPriceCount;
      this.optimumPosition = index1;
    }

    private void Code1Encoding(uint complexState, uint positionState)
    {
      uint optimumPosition = this.optimumPosition;
      uint optimumLength = this.optimumLength;
      this.repeaterRangeBitEncoders[(IntPtr) this.compressorState.Index].Encode(this.rangeEncoder, 1U);
      if (optimumPosition == 0U)
      {
        this.repeaterG0RangeBitEncoders[(IntPtr) this.compressorState.Index].Encode(this.rangeEncoder, 0U);
        if (optimumLength == 1U)
          this.repeaterLongRangeBitEncoders[(IntPtr) complexState].Encode(this.rangeEncoder, 0U);
        else
          this.repeaterLongRangeBitEncoders[(IntPtr) complexState].Encode(this.rangeEncoder, 1U);
      }
      else
      {
        this.repeaterG0RangeBitEncoders[(IntPtr) this.compressorState.Index].Encode(this.rangeEncoder, 1U);
        if (optimumPosition == 1U)
        {
          this.repeaterG1RangeBitEncoders[(IntPtr) this.compressorState.Index].Encode(this.rangeEncoder, 0U);
        }
        else
        {
          this.repeaterG1RangeBitEncoders[(IntPtr) this.compressorState.Index].Encode(this.rangeEncoder, 1U);
          this.repeaterG2RangeBitEncoders[(IntPtr) this.compressorState.Index].Encode(this.rangeEncoder, optimumPosition - 2U);
        }
      }
      if (optimumLength == 1U)
      {
        this.compressorState.UpdateShortRep();
      }
      else
      {
        this.repeaterMatchLengthEncoder.Encode(this.rangeEncoder, optimumLength - 2U, positionState);
        this.compressorState.UpdateRep();
      }
      uint repeaterDistance = this.repeaterDistances[(IntPtr) optimumPosition];
      if (optimumPosition == 0U)
        return;
      for (uint index = optimumPosition; index >= 1U; --index)
        this.repeaterDistances[(IntPtr) index] = this.repeaterDistances[(IntPtr) (index - 1U)];
      this.repeaterDistances[0] = repeaterDistance;
    }

    private void Code0(uint complexState)
    {
      this.matchRangeBitEncoders[(IntPtr) complexState].Encode(this.rangeEncoder, 0U);
      byte indexByte1 = this.matchFinder.GetIndexByte(-(int) this.additionalOffset);
      LzmaLiteralEncoder.Encoder subCoder = this.literalEncoder.GetSubCoder((uint) this.currentPosition, this.currentPreviousByte);
      if (!this.compressorState.IsCharState())
      {
        byte indexByte2 = this.matchFinder.GetIndexByte(-(int) this.repeaterDistances[0] - 1 - (int) this.additionalOffset);
        subCoder.EncodeMatched(this.rangeEncoder, indexByte2, indexByte1);
      }
      else
        subCoder.Encode(this.rangeEncoder, indexByte1);
      this.currentPreviousByte = indexByte1;
      this.compressorState.UpdateChar();
    }

    private bool CodeFirstPart(bool finalBlock)
    {
      if (this.currentPosition == 0L)
      {
        if (!this.matchFinder.Ready)
          return false;
        if (this.matchFinder.AvailableBytes == 0U)
        {
          this.Flush(finalBlock);
          return false;
        }
        uint length;
        this.ReadMatchDistances(out length);
        this.matchRangeBitEncoders[(IntPtr) ((this.compressorState.Index << 4) + ((uint) this.currentPosition & this.configPositionStateMask))].Encode(this.rangeEncoder, 0U);
        this.compressorState.UpdateChar();
        byte indexByte = this.matchFinder.GetIndexByte(-(int) this.additionalOffset);
        this.literalEncoder.GetSubCoder((uint) this.currentPosition, this.currentPreviousByte).Encode(this.rangeEncoder, indexByte);
        this.currentPreviousByte = indexByte;
        --this.additionalOffset;
        ++this.currentPosition;
      }
      return true;
    }

    private void Initialize()
    {
      this.ApplySettings();
      this.InitEncoders();
      this.FillDistancesPrices();
      this.FillAlignPrices();
      this.lengthEncoder.SetTableSize((uint) ((int) this.configFastBytes + 1 - 2));
      this.lengthEncoder.UpdateTables((uint) (1 << this.configPositionStateBits));
      this.repeaterMatchLengthEncoder.SetTableSize((uint) ((int) this.configFastBytes + 1 - 2));
      this.repeaterMatchLengthEncoder.UpdateTables((uint) (1 << this.configPositionStateBits));
      this.currentPosition = 0L;
    }

    private void ApplySettings()
    {
      this.ApplyDictionarySize();
      this.ApplyPositionStateBits();
      this.ApplyLiteralContextBits();
      this.ApplyLiteralPositionBits();
      this.ApplyFastBytes();
      this.ApplyMatchFinderType();
      this.literalEncoder = new LzmaLiteralEncoder(this.configLiteralPositionStateBits, this.configLiteralContextBits);
    }

    private void ApplyDictionarySize()
    {
      int num1 = this.Settings.DictionarySize;
      if (num1 < 0)
        num1 = 0;
      else if (num1 > 27)
        num1 = 27;
      int num2 = 1 << num1;
      this.configDictionarySize = (uint) num2;
      int num3 = 0;
      while (num3 < 27 && (long) num2 > (long) (uint) (1 << num3))
        ++num3;
      this.configDistanceTableSize = (uint) (num3 * 2);
    }

    private void ApplyPositionStateBits()
    {
      int num = this.Settings.PositionStateBits;
      if (num < 0)
        num = 0;
      else if (num > 4)
        num = 4;
      this.configPositionStateBits = num;
      this.configPositionStateMask = (uint) ((1 << this.configPositionStateBits) - 1);
    }

    private void ApplyLiteralContextBits()
    {
      int num = this.Settings.LiteralContextBits;
      if (num < 0)
        num = 0;
      else if (num > 8)
        num = 8;
      this.configLiteralContextBits = num;
    }

    private void ApplyLiteralPositionBits()
    {
      int num = this.Settings.LiteralPositionBits;
      if (num < 0)
        num = 0;
      else if (num > 4)
        num = 4;
      this.configLiteralPositionStateBits = num;
    }

    private void ApplyFastBytes()
    {
      uint num = (uint) this.Settings.FastBytes;
      if (num < 5U)
        num = 5U;
      else if (num > 273U)
        num = 273U;
      this.configFastBytes = num;
    }

    private void ApplyMatchFinderType()
    {
      this.matchFinderType = this.Settings.MatchFinderType;
      int hashBytes = 4;
      if (this.matchFinderType == LzmaMatchFinderType.BT2)
        hashBytes = 2;
      this.matchFinder = new LzmaBinaryTree(hashBytes, this.configDictionarySize, this.configFastBytes, 274U);
    }

    private void FillDistancesPrices()
    {
      for (uint index = 4; index < 128U; ++index)
      {
        uint posSlot = LzmaCompressor.GetPosSlot(index);
        int bitLevelsNumber = (int) (posSlot >> 1) - 1;
        uint num = (uint) ((2 | (int) posSlot & 1) << bitLevelsNumber);
        this.tempPrices[(IntPtr) index] = LzmaBitTreeEncoder.ReverseGetPrice(this.positionEncoders, (uint) ((int) num - (int) posSlot - 1), bitLevelsNumber, index - num);
      }
      for (uint index1 = 0; index1 < 4U; ++index1)
      {
        LzmaBitTreeEncoder lzmaBitTreeEncoder = this.positionSlotEncoder[(IntPtr) index1];
        uint num1 = index1 << 6;
        for (uint symbol = 0; symbol < this.configDistanceTableSize; ++symbol)
          this.positionSlotPrices[(IntPtr) (num1 + symbol)] = lzmaBitTreeEncoder.GetPrice(symbol);
        for (uint index2 = 14; index2 < this.configDistanceTableSize; ++index2)
        {
          uint num2 = (uint) ((int) (index2 >> 1) - 1 - 4 << 6);
          this.positionSlotPrices[(IntPtr) (num1 + index2)] += num2;
        }
        uint num3 = index1 * 128U;
        uint index3;
        for (index3 = 0U; index3 < 4U; ++index3)
          this.distancesPrices[(IntPtr) (num3 + index3)] = this.positionSlotPrices[(IntPtr) (num1 + index3)];
        for (; index3 < 128U; ++index3)
          this.distancesPrices[(IntPtr) (num3 + index3)] = this.positionSlotPrices[(IntPtr) (num1 + LzmaCompressor.GetPosSlot(index3))] + this.tempPrices[(IntPtr) index3];
      }
      this.matchPriceCount = 0U;
    }

    private void FillAlignPrices()
    {
      for (uint symbol = 0; symbol < 16U; ++symbol)
        this.alignPrices[(IntPtr) symbol] = this.positionAlignEncoder.ReverseGetPrice(symbol);
      this.alignPriceCount = 0U;
    }
  }
}
