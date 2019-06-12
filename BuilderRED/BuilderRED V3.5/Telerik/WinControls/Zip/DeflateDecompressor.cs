// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DeflateDecompressor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.Zip
{
  internal class DeflateDecompressor : DeflateTransformBase
  {
    private static readonly int[] lengthBase = new int[29]{ 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67, 83, 99, 115, 131, 163, 195, 227, 258 };
    private static readonly int[] distanceBasePosition = new int[32]{ 1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769, 1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577, 0, 0 };
    private static readonly byte[] staticDistanceTreeTable = new byte[32]{ (byte) 0, (byte) 16, (byte) 8, (byte) 24, (byte) 4, (byte) 20, (byte) 12, (byte) 28, (byte) 2, (byte) 18, (byte) 10, (byte) 26, (byte) 6, (byte) 22, (byte) 14, (byte) 30, (byte) 1, (byte) 17, (byte) 9, (byte) 25, (byte) 5, (byte) 21, (byte) 13, (byte) 29, (byte) 3, (byte) 19, (byte) 11, (byte) 27, (byte) 7, (byte) 23, (byte) 15, (byte) 31 };
    private byte[] blockLengthBuffer = new byte[4];
    private Queue<byte[]> pendingInput = new Queue<byte[]>();
    private DeflateDecompressor.DecompressorState state;
    private InputBitsBuffer input;
    private OutputWindow output;
    private int bfinal;
    private DeflateDecompressor.BlockType blockType;
    private int blockLength;
    private int distanceCode;
    private int extraBits;
    private int loopCounter;
    private int literalLengthCodeCount;
    private int distanceCodeCount;
    private int codeLengthCodeCount;
    private int codeArraySize;
    private int lengthCode;
    private byte[] codeList;
    private byte[] codeLengthTreeCodeLength;
    private InflateTree literalLengthTree;
    private InflateTree distanceTree;
    private InflateTree codeLengthTree;

    public DeflateDecompressor(DeflateSettings settings)
      : base(settings)
    {
      this.output = new OutputWindow();
      this.input = new InputBitsBuffer();
      this.codeList = new byte[320];
      this.codeLengthTreeCodeLength = new byte[19];
      this.Reset();
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
      if (this.Settings.HeaderType != CompressedStreamHeader.ZLib)
        return;
      this.Header.BytesToRead = 2;
    }

    public override void ProcessHeader()
    {
      base.ProcessHeader();
      if (this.Settings.HeaderType == CompressedStreamHeader.ZLib)
      {
        string message = string.Empty;
        if (this.Header.Buffer == null || this.Header.Buffer.Length != 2)
        {
          message = "Invalid header length";
        }
        else
        {
          int num1 = (int) this.Header.Buffer[0];
          int num2 = (num1 >> 4) + 8;
          int num3 = (int) this.Header.Buffer[1];
          if ((num1 & 15) != 8)
            message = string.Format("Unknown compression method (0x{0:X2})", (object) num1);
          else if (num2 > 15)
            message = string.Format("Invalid window size ({0})", (object) num2);
          else if (((num1 << 8) + num3) % 31 != 0)
            message = "Invalid header";
        }
        if (message.Length != 0)
          throw new InvalidDataException(message);
      }
      this.Header.BytesToRead = 0;
    }

    protected override bool ProcessTransform(bool finalBlock)
    {
      this.SetInputBuffer();
      this.NextOut += this.Inflate(this.OutputBuffer, this.NextOut, this.AvailableBytesOut);
      if (this.output.AvailableBytes > 0)
        return true;
      if (this.state == DeflateDecompressor.DecompressorState.Done)
        return false;
      if (this.input.InputRequired)
        return this.pendingInput.Count > 0;
      return true;
    }

    private static void ThrowInvalidData()
    {
      throw new InvalidDataException("Invalid data");
    }

    private static void ThrowInvalidDataGeneric()
    {
      throw new InvalidDataException();
    }

    private static void ThrowUnknownBlockType()
    {
      throw new InvalidDataException("Unknown block type");
    }

    private static void ThrowUnknownState()
    {
      throw new InvalidDataException("Unknown state");
    }

    private bool Decode()
    {
      if (this.state == DeflateDecompressor.DecompressorState.ReadingBFinal)
      {
        if (!this.input.CheckAvailable(1))
          return false;
        this.bfinal = this.input.GetBits(1);
        this.state = DeflateDecompressor.DecompressorState.ReadingBType;
      }
      if (this.state == DeflateDecompressor.DecompressorState.ReadingBType)
      {
        if (!this.input.CheckAvailable(2))
          return false;
        this.ProcessBlockType();
      }
      return this.CheckDecodeState();
    }

    private void ProcessBlockType()
    {
      this.blockType = (DeflateDecompressor.BlockType) this.input.GetBits(2);
      switch (this.blockType)
      {
        case DeflateDecompressor.BlockType.Stored:
          this.state = DeflateDecompressor.DecompressorState.UncompressedAligning;
          break;
        case DeflateDecompressor.BlockType.Static:
          this.literalLengthTree = InflateTree.StaticLiteralLengthTree;
          this.distanceTree = InflateTree.StaticDistanceTree;
          this.state = DeflateDecompressor.DecompressorState.DecodeTop;
          break;
        case DeflateDecompressor.BlockType.Dynamic:
          this.state = DeflateDecompressor.DecompressorState.ReadingNumLitCodes;
          break;
        default:
          DeflateDecompressor.ThrowUnknownBlockType();
          break;
      }
    }

    private bool CheckDecodeState()
    {
      bool endOfBlock = false;
      bool flag;
      if (this.blockType == DeflateDecompressor.BlockType.Dynamic)
        flag = this.state < DeflateDecompressor.DecompressorState.DecodeTop ? this.DecodeDynamicBlockHeader() : this.DecodeBlock(out endOfBlock);
      else if (this.blockType != DeflateDecompressor.BlockType.Static)
      {
        if (this.blockType != DeflateDecompressor.BlockType.Stored)
          DeflateDecompressor.ThrowUnknownBlockType();
        flag = this.DecodeUncompressedBlock(out endOfBlock);
      }
      else
        flag = this.DecodeBlock(out endOfBlock);
      if (endOfBlock && this.bfinal != 0)
        this.state = DeflateDecompressor.DecompressorState.Done;
      return flag;
    }

    private bool DecodeBlock(out bool endOfBlock)
    {
      int freeBytes = this.output.FreeBytes;
      endOfBlock = false;
      while (freeBytes > 258)
      {
        switch (this.state)
        {
          case DeflateDecompressor.DecompressorState.DecodeTop:
            bool? nullable1 = this.DecodeTop(ref endOfBlock, ref freeBytes);
            if (nullable1.HasValue)
              return nullable1.Value;
            continue;
          case DeflateDecompressor.DecompressorState.HaveInitialLength:
            bool? nullable2 = this.DecodeInitialLength();
            if (nullable2.HasValue)
              return nullable2.Value;
            continue;
          case DeflateDecompressor.DecompressorState.HaveFullLength:
            bool? nullable3 = this.DecodeFullLength();
            if (nullable3.HasValue)
              return nullable3.Value;
            continue;
          case DeflateDecompressor.DecompressorState.HaveDistCode:
            bool? nullable4 = this.DecodeDistanceCode(ref freeBytes);
            if (nullable4.HasValue)
              return nullable4.Value;
            continue;
          default:
            DeflateDecompressor.ThrowUnknownState();
            continue;
        }
      }
      return true;
    }

    private bool? DecodeTop(ref bool endOfBlock, ref int freeBytes)
    {
      int nextSymbol = this.literalLengthTree.GetNextSymbol(this.input);
      if (nextSymbol < 0)
        return new bool?(false);
      if (nextSymbol >= 256)
      {
        if (nextSymbol == 256)
        {
          endOfBlock = true;
          this.state = DeflateDecompressor.DecompressorState.ReadingBFinal;
          return new bool?(true);
        }
        this.DecodeDistance(ref nextSymbol);
        this.state = DeflateDecompressor.DecompressorState.HaveInitialLength;
      }
      else
      {
        this.output.AddByte((byte) nextSymbol);
        --freeBytes;
      }
      return new bool?();
    }

    private void DecodeDistance(ref int nextSymbol)
    {
      nextSymbol -= 257;
      if (nextSymbol < 8)
      {
        nextSymbol += 3;
        this.extraBits = 0;
      }
      else if (nextSymbol != 28)
      {
        if (nextSymbol < 0 || nextSymbol >= Tree.ExtraLengthBits.Length)
          DeflateDecompressor.ThrowInvalidData();
        this.extraBits = Tree.ExtraLengthBits[nextSymbol];
      }
      else
      {
        nextSymbol = 258;
        this.extraBits = 0;
      }
      this.blockLength = nextSymbol;
    }

    private bool? DecodeInitialLength()
    {
      if (this.extraBits > 0)
      {
        int bits = this.input.GetBits(this.extraBits);
        if (bits < 0)
          return new bool?(false);
        if (this.blockLength < 0 || this.blockLength >= DeflateDecompressor.lengthBase.Length)
          DeflateDecompressor.ThrowInvalidData();
        this.blockLength = DeflateDecompressor.lengthBase[this.blockLength] + bits;
      }
      this.state = DeflateDecompressor.DecompressorState.HaveFullLength;
      return new bool?();
    }

    private bool? DecodeFullLength()
    {
      if (this.blockType != DeflateDecompressor.BlockType.Dynamic)
      {
        this.distanceCode = this.input.GetBits(5);
        if (this.distanceCode >= 0)
          this.distanceCode = (int) DeflateDecompressor.staticDistanceTreeTable[this.distanceCode];
      }
      else
        this.distanceCode = this.distanceTree.GetNextSymbol(this.input);
      if (this.distanceCode < 0)
        return new bool?(false);
      this.state = DeflateDecompressor.DecompressorState.HaveDistCode;
      return new bool?();
    }

    private bool? DecodeDistanceCode(ref int freeBytes)
    {
      int distance;
      if (this.distanceCode <= 3)
      {
        distance = this.distanceCode + 1;
      }
      else
      {
        this.extraBits = this.distanceCode - 2 >> 1;
        int bits = this.input.GetBits(this.extraBits);
        if (bits < 0)
          return new bool?(false);
        distance = DeflateDecompressor.distanceBasePosition[this.distanceCode] + bits;
      }
      this.output.Copy(this.blockLength, distance);
      freeBytes -= this.blockLength;
      this.state = DeflateDecompressor.DecompressorState.DecodeTop;
      return new bool?();
    }

    private bool DecodeDynamicBlockHeader()
    {
      switch (this.state)
      {
        case DeflateDecompressor.DecompressorState.ReadingNumLitCodes:
          this.literalLengthCodeCount = this.input.GetBits(5);
          if (this.literalLengthCodeCount < 0)
            return false;
          this.literalLengthCodeCount += 257;
          this.state = DeflateDecompressor.DecompressorState.ReadingNumDistCodes;
          goto case DeflateDecompressor.DecompressorState.ReadingNumDistCodes;
        case DeflateDecompressor.DecompressorState.ReadingNumDistCodes:
          this.distanceCodeCount = this.input.GetBits(5);
          if (this.distanceCodeCount < 0)
            return false;
          ++this.distanceCodeCount;
          this.state = DeflateDecompressor.DecompressorState.ReadingNumCodeLengthCodes;
          goto case DeflateDecompressor.DecompressorState.ReadingNumCodeLengthCodes;
        case DeflateDecompressor.DecompressorState.ReadingNumCodeLengthCodes:
          this.codeLengthCodeCount = this.input.GetBits(4);
          if (this.codeLengthCodeCount < 0)
            return false;
          this.codeLengthCodeCount += 4;
          this.loopCounter = 0;
          this.state = DeflateDecompressor.DecompressorState.ReadingCodeLengthCodes;
          goto case DeflateDecompressor.DecompressorState.ReadingCodeLengthCodes;
        case DeflateDecompressor.DecompressorState.ReadingCodeLengthCodes:
          if (!this.DecodeDynamicCodes())
            return false;
          this.state = DeflateDecompressor.DecompressorState.ReadingTreeCodesBefore;
          goto case DeflateDecompressor.DecompressorState.ReadingTreeCodesBefore;
        case DeflateDecompressor.DecompressorState.ReadingTreeCodesBefore:
        case DeflateDecompressor.DecompressorState.ReadingTreeCodesAfter:
          return this.ReadTreeCodes();
        default:
          DeflateDecompressor.ThrowUnknownState();
          return true;
      }
    }

    private bool DecodeDynamicCodes()
    {
      for (; this.loopCounter < this.codeLengthCodeCount; ++this.loopCounter)
      {
        int bits = this.input.GetBits(3);
        if (bits < 0)
          return false;
        this.codeLengthTreeCodeLength[(int) Tree.BitLengthOrder[this.loopCounter]] = (byte) bits;
      }
      for (int codeLengthCodeCount = this.codeLengthCodeCount; codeLengthCodeCount < Tree.BitLengthOrder.Length; ++codeLengthCodeCount)
        this.codeLengthTreeCodeLength[(int) Tree.BitLengthOrder[codeLengthCodeCount]] = (byte) 0;
      this.codeLengthTree = new InflateTree(this.codeLengthTreeCodeLength);
      this.codeArraySize = this.literalLengthCodeCount + this.distanceCodeCount;
      this.loopCounter = 0;
      return true;
    }

    private bool ReadTreeCodes()
    {
      while (this.loopCounter < this.codeArraySize)
      {
        if (this.state == DeflateDecompressor.DecompressorState.ReadingTreeCodesBefore)
        {
          this.lengthCode = this.codeLengthTree.GetNextSymbol(this.input);
          if (this.lengthCode < 0)
            return false;
        }
        if (this.lengthCode > 15)
        {
          if (!this.input.CheckAvailable(7))
          {
            this.state = DeflateDecompressor.DecompressorState.ReadingTreeCodesAfter;
            return false;
          }
          this.SetPreviousCode();
        }
        else
          this.codeList[this.loopCounter++] = (byte) this.lengthCode;
        this.state = DeflateDecompressor.DecompressorState.ReadingTreeCodesBefore;
      }
      this.SetTreeCodes();
      this.state = DeflateDecompressor.DecompressorState.DecodeTop;
      return true;
    }

    private void SetPreviousCode()
    {
      byte num1 = 0;
      int num2;
      if (this.lengthCode == 16)
      {
        if (this.loopCounter == 0)
          DeflateDecompressor.ThrowInvalidDataGeneric();
        num1 = this.codeList[this.loopCounter - 1];
        num2 = this.input.GetBits(2) + 3;
      }
      else
        num2 = this.lengthCode == 17 ? this.input.GetBits(3) + 3 : this.input.GetBits(7) + 11;
      if (this.loopCounter + num2 > this.codeArraySize)
        DeflateDecompressor.ThrowInvalidDataGeneric();
      for (int index = 0; index < num2; ++index)
        this.codeList[this.loopCounter++] = num1;
    }

    private void SetTreeCodes()
    {
      byte[] codeLengths1 = new byte[288];
      byte[] codeLengths2 = new byte[32];
      Array.Copy((Array) this.codeList, (Array) codeLengths1, this.literalLengthCodeCount);
      Array.Copy((Array) this.codeList, this.literalLengthCodeCount, (Array) codeLengths2, 0, this.distanceCodeCount);
      if (codeLengths1[256] == (byte) 0)
        DeflateDecompressor.ThrowInvalidDataGeneric();
      this.literalLengthTree = new InflateTree(codeLengths1);
      this.distanceTree = new InflateTree(codeLengths2);
    }

    private bool DecodeUncompressedBlock(out bool endOfBlock)
    {
      endOfBlock = false;
      while (this.state != DeflateDecompressor.DecompressorState.DecodingUncompressed)
      {
        switch (this.state)
        {
          case DeflateDecompressor.DecompressorState.UncompressedAligning:
            this.input.SkipToByteBoundary();
            this.state = DeflateDecompressor.DecompressorState.UncompressedByte1;
            continue;
          case DeflateDecompressor.DecompressorState.UncompressedByte1:
          case DeflateDecompressor.DecompressorState.UncompressedByte2:
          case DeflateDecompressor.DecompressorState.UncompressedByte3:
          case DeflateDecompressor.DecompressorState.UncompressedByte4:
            if (!this.DecodeBits())
              return false;
            continue;
          default:
            DeflateDecompressor.ThrowUnknownState();
            continue;
        }
      }
      this.blockLength -= this.output.ReadInput(this.input, this.blockLength);
      if (this.blockLength != 0)
        return this.output.FreeBytes == 0;
      this.state = DeflateDecompressor.DecompressorState.ReadingBFinal;
      endOfBlock = true;
      return true;
    }

    private bool DecodeBits()
    {
      int bits = this.input.GetBits(8);
      if (bits < 0)
        return false;
      this.blockLengthBuffer[(int) (this.state - 16)] = (byte) bits;
      if (this.state == DeflateDecompressor.DecompressorState.UncompressedByte4)
      {
        this.blockLength = (int) this.blockLengthBuffer[0] + (int) this.blockLengthBuffer[1] * 256;
        if ((int) (ushort) this.blockLength != (int) (ushort) ~((int) this.blockLengthBuffer[2] + (int) this.blockLengthBuffer[3] * 256))
          throw new InvalidDataException("Invalid block length");
      }
      ++this.state;
      return true;
    }

    private int Inflate(byte[] bytes, int offset, int length)
    {
      int num1 = 0;
      while (length > 0 && this.state != DeflateDecompressor.DecompressorState.Done)
      {
        int num2 = this.output.Read(bytes, offset, length);
        if (num2 > 0)
        {
          offset += num2;
          num1 += num2;
          length -= num2;
        }
        if (!this.Decode() && (this.state == DeflateDecompressor.DecompressorState.Done || !this.TrySetInputBuffer()))
          break;
      }
      if (this.output.AvailableBytes > 0 && length > 0)
      {
        int num2 = this.output.Read(bytes, offset, length);
        num1 += num2;
      }
      return num1;
    }

    private void Reset()
    {
      this.state = DeflateDecompressor.DecompressorState.ReadingBFinal;
    }

    private void SetInput(byte[] inputBytes, int offset, int length)
    {
      this.input.SetBuffer(inputBytes, offset, length);
    }

    private void EnqueueInputBuffer()
    {
      if (this.AvailableBytesIn <= 0)
        return;
      byte[] numArray = new byte[this.AvailableBytesIn];
      Array.Copy((Array) this.InputBuffer, 0, (Array) numArray, 0, numArray.Length);
      this.pendingInput.Enqueue(numArray);
    }

    private void SetInputBuffer()
    {
      if (this.input.InputRequired)
      {
        byte[] inputBytes;
        if (this.pendingInput.Count > 0)
        {
          this.EnqueueInputBuffer();
          inputBytes = this.pendingInput.Dequeue();
          this.AvailableBytesIn = inputBytes.Length;
        }
        else
        {
          inputBytes = new byte[this.AvailableBytesIn];
          Array.Copy((Array) this.InputBuffer, 0, (Array) inputBytes, 0, inputBytes.Length);
        }
        this.SetInput(inputBytes, 0, this.AvailableBytesIn);
      }
      else
        this.EnqueueInputBuffer();
    }

    private bool TrySetInputBuffer()
    {
      if (!this.input.InputRequired || this.pendingInput.Count <= 0)
        return false;
      byte[] inputBytes = this.pendingInput.Dequeue();
      this.AvailableBytesIn = inputBytes.Length;
      this.SetInput(inputBytes, 0, this.AvailableBytesIn);
      return true;
    }

    private enum DecompressorState
    {
      ReadingBFinal = 2,
      ReadingBType = 3,
      ReadingNumLitCodes = 4,
      ReadingNumDistCodes = 5,
      ReadingNumCodeLengthCodes = 6,
      ReadingCodeLengthCodes = 7,
      ReadingTreeCodesBefore = 8,
      ReadingTreeCodesAfter = 9,
      DecodeTop = 10, // 0x0000000A
      HaveInitialLength = 11, // 0x0000000B
      HaveFullLength = 12, // 0x0000000C
      HaveDistCode = 13, // 0x0000000D
      UncompressedAligning = 15, // 0x0000000F
      UncompressedByte1 = 16, // 0x00000010
      UncompressedByte2 = 17, // 0x00000011
      UncompressedByte3 = 18, // 0x00000012
      UncompressedByte4 = 19, // 0x00000013
      DecodingUncompressed = 20, // 0x00000014
      Done = 24, // 0x00000018
    }

    private enum BlockType
    {
      Stored,
      Static,
      Dynamic,
    }
  }
}
