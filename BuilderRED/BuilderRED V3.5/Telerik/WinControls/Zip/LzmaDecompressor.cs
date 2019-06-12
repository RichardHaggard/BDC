// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaDecompressor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaDecompressor : LzmaTransformBase
  {
    private LzmaBitTreeDecoder[] positionSlotDecoder = new LzmaBitTreeDecoder[new IntPtr(4)];
    private LzmaRangeDecoder rangeDecoder = new LzmaRangeDecoder();
    private LzmaRangeBitDecoder[] matchDecoders = new LzmaRangeBitDecoder[new IntPtr(192)];
    private LzmaRangeBitDecoder[] repeaterDecoders = new LzmaRangeBitDecoder[new IntPtr(12)];
    private LzmaRangeBitDecoder[] repeaterG0Decoders = new LzmaRangeBitDecoder[new IntPtr(12)];
    private LzmaRangeBitDecoder[] repeaterG1Decoders = new LzmaRangeBitDecoder[new IntPtr(12)];
    private LzmaRangeBitDecoder[] repeaterG2Decoders = new LzmaRangeBitDecoder[new IntPtr(12)];
    private LzmaRangeBitDecoder[] repeaterLongDecoders = new LzmaRangeBitDecoder[new IntPtr(192)];
    private LzmaRangeBitDecoder[] positionDecoders = new LzmaRangeBitDecoder[new IntPtr(114)];
    private LzmaBitTreeDecoder positionAlignDecoder = new LzmaBitTreeDecoder(4);
    private LzmaState lzmaState;
    private uint dictionarySize;
    private uint dictionarySizeCheck;
    private LzmaLengthDecoder lengthDecoder;
    private LzmaLengthDecoder repeaterLengthDecoder;
    private LzmaLiteralDecoder literalDecoder;
    private LzmaOutputWindow outputWindow;
    private LzmaDecompressor.LzmaDecompressorState decompressState;
    private ulong currentPosition;
    private uint repeater;
    private uint repeaterPosition1;
    private uint repeaterPosition2;
    private uint repeaterPosition3;
    private uint decodedLength;
    private uint positionStateMask;
    private uint decodedState;
    private uint positionState;
    private uint positionSlot;

    public LzmaDecompressor(LzmaSettings settings)
      : base(settings)
    {
      this.dictionarySize = uint.MaxValue;
      for (int index = 0; index < 4; ++index)
        this.positionSlotDecoder[index] = new LzmaBitTreeDecoder(6);
    }

    public override int OutputBlockSize
    {
      get
      {
        return 262144;
      }
    }

    public override void InitHeaderReading()
    {
      base.InitHeaderReading();
      if (this.Settings.UseZipHeader)
        this.Header.BytesToRead = 14;
      else
        this.Header.BytesToRead = 18;
    }

    public override void ProcessHeader()
    {
      base.ProcessHeader();
      this.SetProperties();
      this.Header.BytesToRead = 0;
    }

    protected override bool ProcessTransform(bool finalBlock)
    {
      this.rangeDecoder.FinalBlock = finalBlock;
      this.SetInputBuffer();
      this.outputWindow.SetOutputBuffer(this.OutputBuffer);
      this.outputWindow.Flush();
      while (!this.rangeDecoder.InputRequired && !this.outputWindow.Full && this.decompressState != LzmaDecompressor.LzmaDecompressorState.Done)
      {
        this.Decompress();
        this.outputWindow.Flush();
      }
      this.NextOut += this.outputWindow.Copied;
      if (this.outputWindow.AvailableBytes > 0)
        return true;
      if (this.decompressState != LzmaDecompressor.LzmaDecompressorState.Done)
        return !this.rangeDecoder.InputRequired;
      return false;
    }

    private static void InvalidLzmaProperties()
    {
      throw new InvalidDataException("Invalid LZMA properties");
    }

    private void SetProperties()
    {
      string message = string.Empty;
      string str = "Invalid header length";
      if (this.Settings.UseZipHeader)
      {
        if (this.Header.Buffer == null || this.Header.Buffer.Length != 14)
        {
          message = str;
        }
        else
        {
          this.InitializeDecompressor(4);
          this.Init(this.Header.Buffer, 9);
        }
      }
      else if (this.Header.Buffer == null || this.Header.Buffer.Length != 18)
      {
        message = str;
      }
      else
      {
        this.InitializeDecompressor(0);
        this.Init(this.Header.Buffer, 13);
        this.Settings.InternalStreamLength = BitConverter.ToInt64(this.Header.Buffer, 5);
      }
      if (message.Length != 0)
        throw new InvalidDataException(message);
    }

    private void InitializeDecompressor(int propertiesIndex)
    {
      byte[] buffer = this.Header.Buffer;
      int lc = (int) buffer[propertiesIndex] % 9;
      int num = (int) buffer[propertiesIndex] / 9;
      int lp = num % 5;
      int pb = num / 5;
      if (pb > 4)
        LzmaDecompressor.InvalidLzmaProperties();
      uint size = 0;
      ++propertiesIndex;
      for (int index = 0; index < 4; ++index)
        size += (uint) buffer[propertiesIndex + index] << index * 8;
      this.SetDictionarySize(size);
      this.SetLiteralProperties(lp, lc);
      this.SetPosBitsProperties(pb);
    }

    private void SetLiteralProperties(int lp, int lc)
    {
      if (lp > 8 || lc > 8)
        LzmaDecompressor.InvalidLzmaProperties();
      this.literalDecoder = new LzmaLiteralDecoder(lp, lc);
    }

    private void SetPosBitsProperties(int pb)
    {
      if (pb > 4)
        LzmaDecompressor.InvalidLzmaProperties();
      uint positionStates = (uint) (1 << pb);
      this.lengthDecoder = new LzmaLengthDecoder(positionStates);
      this.repeaterLengthDecoder = new LzmaLengthDecoder(positionStates);
      this.positionStateMask = positionStates - 1U;
    }

    private void SetDictionarySize(uint size)
    {
      this.dictionarySize = size;
      this.dictionarySizeCheck = Math.Max(this.dictionarySize, 1U);
      this.outputWindow = new LzmaOutputWindow(Math.Max(this.dictionarySizeCheck, 4096U));
    }

    private void Init(byte[] inputBuffer, int startIndex)
    {
      this.lzmaState = new LzmaState();
      this.rangeDecoder.Init(inputBuffer, startIndex);
      for (uint index1 = 0; index1 < 12U; ++index1)
      {
        for (uint index2 = 0; index2 <= this.positionStateMask; ++index2)
        {
          uint num = (index1 << 4) + index2;
          this.matchDecoders[(IntPtr) num].Init();
          this.repeaterLongDecoders[(IntPtr) num].Init();
        }
        this.repeaterDecoders[(IntPtr) index1].Init();
        this.repeaterG0Decoders[(IntPtr) index1].Init();
        this.repeaterG1Decoders[(IntPtr) index1].Init();
        this.repeaterG2Decoders[(IntPtr) index1].Init();
      }
      this.literalDecoder.Init();
      for (uint index = 0; index < 4U; ++index)
        this.positionSlotDecoder[(IntPtr) index].Init();
      for (uint index = 0; index < 114U; ++index)
        this.positionDecoders[(IntPtr) index].Init();
      this.lengthDecoder.Init();
      this.repeaterLengthDecoder.Init();
      this.positionAlignDecoder.Init();
    }

    private void DecompressStart()
    {
      this.decodedState = this.matchDecoders[(IntPtr) (this.lzmaState.Index << 4)].DecodeState(this.rangeDecoder);
      if (this.decodedState != 0U)
      {
        if (this.currentPosition != 0UL || this.AvailableBytesIn != 0 && this.AvailableBytesIn != 5)
          throw new InvalidDataException();
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.Done;
      }
      else
      {
        this.lzmaState.UpdateChar();
        this.outputWindow.PutByte(this.literalDecoder.DecodeNormal(this.rangeDecoder, 0U, (byte) 0));
        ++this.currentPosition;
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.MatchDecoder;
      }
    }

    private bool Decompress()
    {
      while (!this.rangeDecoder.InputRequired && !this.outputWindow.Full && this.decompressState != LzmaDecompressor.LzmaDecompressorState.Done)
      {
        switch (this.decompressState)
        {
          case LzmaDecompressor.LzmaDecompressorState.Start:
            this.DecompressStart();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.MatchDecoder:
            this.positionState = (uint) this.currentPosition & this.positionStateMask;
            this.DecompressMatchDecoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.LiteralDecoder:
            this.DecompressLiteralDecoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.RepeaterDecoder:
            this.DecompressRepeaterDecoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.RepeaterG0Decoder:
            this.DecompressRepeaterG0Decoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.Repeater0LongDecoder:
            this.DecompressRepeater0LongDecoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.RepeaterG1Decoder:
            this.DecompressRepeaterG1Decoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.RepeaterG2Decoder:
            this.DecompressRepeaterG2Decoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.RepeaterLengthDecoder:
            this.DecompressRepeaterLengthDecoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.LengthDecoder:
            this.DecompressLengthDecoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.PosSlotDecoder:
            this.DecompressPosSlotDecoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.PosSlotProcess:
            this.DecompressPosSlotProcess();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.ReverseDecoder:
            this.DecompressReverseDecoder();
            continue;
          case LzmaDecompressor.LzmaDecompressorState.CopyBlock:
            this.DecompressCopyBlock();
            continue;
          default:
            continue;
        }
      }
      return false;
    }

    private void DecompressMatchDecoder()
    {
      uint num = this.matchDecoders[(IntPtr) ((this.lzmaState.Index << 4) + this.positionState)].DecodeState(this.rangeDecoder);
      bool inputRequired = this.rangeDecoder.InputRequired;
      bool flag = this.rangeDecoder.CheckInputRequired(1, true);
      if (!flag && !inputRequired && this.rangeDecoder.FinalBlock)
      {
        if (this.currentPosition < (ulong) this.Settings.InternalStreamLength)
          flag = this.rangeDecoder.CheckInputRequired(0, true);
        else
          this.decompressState = LzmaDecompressor.LzmaDecompressorState.Done;
      }
      if (!flag)
        return;
      this.decodedState = num;
      if (this.decodedState == 0U)
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.LiteralDecoder;
      else
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.RepeaterDecoder;
    }

    private void DecompressLiteralDecoder()
    {
      byte previousByte = this.outputWindow.GetByte(0U);
      if (!this.rangeDecoder.CheckInputRequired(256, false))
        return;
      this.outputWindow.PutByte(!this.lzmaState.IsCharState() ? this.literalDecoder.DecodeWithMatchByte(this.rangeDecoder, (uint) this.currentPosition, previousByte, this.outputWindow.GetByte(this.repeater)) : this.literalDecoder.DecodeNormal(this.rangeDecoder, (uint) this.currentPosition, previousByte));
      this.lzmaState.UpdateChar();
      ++this.currentPosition;
      this.decompressState = LzmaDecompressor.LzmaDecompressorState.MatchDecoder;
    }

    private void DecompressRepeaterDecoder()
    {
      uint num = this.repeaterDecoders[(IntPtr) this.lzmaState.Index].DecodeState(this.rangeDecoder);
      if (this.rangeDecoder.InputRequired)
        return;
      this.decodedState = num;
      if (this.decodedState == 0U)
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.LengthDecoder;
      else
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.RepeaterG0Decoder;
    }

    private void DecompressRepeaterG0Decoder()
    {
      uint num = this.repeaterG0Decoders[(IntPtr) this.lzmaState.Index].DecodeState(this.rangeDecoder);
      if (this.rangeDecoder.InputRequired)
        return;
      this.decodedState = num;
      if (this.decodedState == 0U)
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.Repeater0LongDecoder;
      else
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.RepeaterG1Decoder;
    }

    private void DecompressRepeater0LongDecoder()
    {
      uint num = this.repeaterLongDecoders[(IntPtr) ((this.lzmaState.Index << 4) + this.positionState)].DecodeState(this.rangeDecoder);
      if (this.rangeDecoder.InputRequired)
        return;
      this.decodedState = num;
      if (this.decodedState == 0U)
      {
        this.lzmaState.UpdateShortRep();
        this.outputWindow.PutByte(this.outputWindow.GetByte(this.repeater));
        ++this.currentPosition;
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.MatchDecoder;
      }
      else
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.RepeaterLengthDecoder;
    }

    private void DecompressRepeaterLengthDecoder()
    {
      this.decodedLength = this.repeaterLengthDecoder.Decode(this.rangeDecoder, this.positionState) + 2U;
      if (this.rangeDecoder.InputRequired)
        return;
      this.lzmaState.UpdateRep();
      this.decompressState = LzmaDecompressor.LzmaDecompressorState.CopyBlock;
    }

    private void DecompressRepeaterG1Decoder()
    {
      uint num = this.repeaterG1Decoders[(IntPtr) this.lzmaState.Index].DecodeState(this.rangeDecoder);
      if (this.rangeDecoder.InputRequired)
        return;
      this.decodedState = num;
      if (this.decodedState == 0U)
      {
        uint repeaterPosition1 = this.repeaterPosition1;
        this.repeaterPosition1 = this.repeater;
        this.repeater = repeaterPosition1;
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.RepeaterLengthDecoder;
      }
      else
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.RepeaterG2Decoder;
    }

    private void DecompressRepeaterG2Decoder()
    {
      uint num1 = this.repeaterG2Decoders[(IntPtr) this.lzmaState.Index].DecodeState(this.rangeDecoder);
      if (this.rangeDecoder.InputRequired)
        return;
      this.decodedState = num1;
      uint num2;
      if (this.decodedState == 0U)
      {
        num2 = this.repeaterPosition2;
      }
      else
      {
        num2 = this.repeaterPosition3;
        this.repeaterPosition3 = this.repeaterPosition2;
      }
      this.repeaterPosition2 = this.repeaterPosition1;
      this.repeaterPosition1 = this.repeater;
      this.repeater = num2;
      this.decompressState = LzmaDecompressor.LzmaDecompressorState.RepeaterLengthDecoder;
    }

    private void DecompressCopyBlock()
    {
      if ((ulong) this.repeater >= this.currentPosition || this.repeater >= this.dictionarySizeCheck)
      {
        if (this.repeater != uint.MaxValue)
          throw new InvalidDataException();
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.Done;
      }
      else
      {
        this.outputWindow.CopyBlock(this.repeater, this.decodedLength);
        this.currentPosition += (ulong) this.decodedLength;
        this.decompressState = LzmaDecompressor.LzmaDecompressorState.MatchDecoder;
      }
    }

    private void DecompressReverseDecoder()
    {
      uint num = this.positionAlignDecoder.ReverseDecode(this.rangeDecoder);
      if (this.rangeDecoder.InputRequired)
        return;
      this.repeater += num;
      this.decompressState = LzmaDecompressor.LzmaDecompressorState.CopyBlock;
    }

    private void DecompressPosSlotProcess()
    {
      if (this.positionSlot >= 4U)
      {
        int numBitLevels = (int) (this.positionSlot >> 1) - 1;
        this.repeater = (uint) ((2 | (int) this.positionSlot & 1) << numBitLevels);
        if (this.positionSlot < 14U)
        {
          uint num = LzmaBitTreeDecoder.ReverseDecode(this.positionDecoders, (uint) ((int) this.repeater - (int) this.positionSlot - 1), this.rangeDecoder, numBitLevels);
          if (this.rangeDecoder.InputRequired)
            return;
          this.repeater += num;
        }
        else
        {
          uint num = this.rangeDecoder.DecodeDirectBits(numBitLevels - 4) << 4;
          if (this.rangeDecoder.InputRequired)
            return;
          this.repeater += num;
          this.decompressState = LzmaDecompressor.LzmaDecompressorState.ReverseDecoder;
          return;
        }
      }
      else
        this.repeater = this.positionSlot;
      this.decompressState = LzmaDecompressor.LzmaDecompressorState.CopyBlock;
    }

    private void DecompressPosSlotDecoder()
    {
      this.positionSlot = this.positionSlotDecoder[(IntPtr) LzmaState.GetLenToPosState(this.decodedLength)].Decode(this.rangeDecoder);
      if (this.rangeDecoder.InputRequired)
        return;
      this.decompressState = LzmaDecompressor.LzmaDecompressorState.PosSlotProcess;
    }

    private void DecompressLengthDecoder()
    {
      this.decodedLength = 2U + this.lengthDecoder.Decode(this.rangeDecoder, this.positionState);
      if (this.rangeDecoder.InputRequired)
        return;
      this.repeaterPosition3 = this.repeaterPosition2;
      this.repeaterPosition2 = this.repeaterPosition1;
      this.repeaterPosition1 = this.repeater;
      this.lzmaState.UpdateMatch();
      this.decompressState = LzmaDecompressor.LzmaDecompressorState.PosSlotDecoder;
    }

    private void SetInput(byte[] inputBytes, int length)
    {
      this.rangeDecoder.SetBuffer(inputBytes, length);
    }

    private void SetInputBuffer()
    {
      if (this.AvailableBytesIn <= 0)
        return;
      byte[] inputBytes = new byte[this.AvailableBytesIn];
      Array.Copy((Array) this.InputBuffer, 0, (Array) inputBytes, this.NextIn, inputBytes.Length);
      this.SetInput(inputBytes, this.AvailableBytesIn);
    }

    private enum LzmaDecompressorState
    {
      Start,
      MatchDecoder,
      LiteralDecoder,
      RepeaterDecoder,
      RepeaterG0Decoder,
      Repeater0LongDecoder,
      RepeaterG1Decoder,
      RepeaterG2Decoder,
      RepeaterLengthDecoder,
      LengthDecoder,
      PosSlotDecoder,
      PosSlotProcess,
      ReverseDecoder,
      CopyBlock,
      Done,
    }
  }
}
