// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DeflateCompressor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class DeflateCompressor : DeflateTransformBase
  {
    private Tree treeLiterals = new Tree();
    private Tree treeDistances = new Tree();
    private Tree treeBitLengths = new Tree();
    private const int WindowBitsDefault = 15;
    private const int DefaultMemoryLevel = 8;
    private const int StaticTreeBlock = 1;
    private const int DynamicTreeBlock = 2;
    private const int BufferSize = 16;
    private const int MinMatch = 3;
    private const int MaxMatch = 258;
    private const int MinLookAhead = 262;
    private const int EndBlock = 256;
    private int goodLength;
    private int maxChainLength;
    private int maxLazy;
    private int niceLength;
    private DeflateCompressor.CompressionMethod deflateFunction;
    private bool finishPending;
    private int windowSize;
    private int windowBits;
    private int windowMask;
    private byte[] window;
    private int actualWindowSize;
    private short[] head;
    private short[] previous;
    private int hashIndexOfString;
    private int hashSize;
    private int hashBits;
    private int hashMask;
    private int hashShift;
    private int blockStart;
    private int matchLength;
    private int previousMatch;
    private int matchAvailable;
    private int startOfString;
    private int matchStart;
    private int lookAhead;
    private int previousLength;
    private int compressionLevel;
    private short[] dynamicLiteralsTree;
    private short[] dynamicDistanceTree;
    private short[] dynamicBitLengthTree;
    private int lengthOffset;
    private int literalsBufsize;
    private int lastLiteral;
    private int distanceOffset;
    private int matches;
    private short bitsBuffer;
    private int bitsValid;
    private byte[] pending;
    private int nextPending;
    private int pendingCount;

    public DeflateCompressor(DeflateSettings settings)
      : base(settings)
    {
      int length = 573;
      this.dynamicLiteralsTree = new short[length * 2];
      this.dynamicDistanceTree = new short[122];
      this.dynamicBitLengthTree = new short[78];
      this.BitLengthCount = new short[16];
      this.Heap = new int[length];
      this.Depth = new sbyte[length];
      this.Initialize(settings);
    }

    public short[] BitLengthCount { get; private set; }

    public int[] Heap { get; private set; }

    public int HeapLength { get; set; }

    public int HeapMax { get; set; }

    public sbyte[] Depth { get; private set; }

    public int OptimalLength { get; set; }

    public int StaticLength { get; set; }

    public override void CreateHeader()
    {
      if (this.Settings.HeaderType != CompressedStreamHeader.ZLib)
        return;
      byte[] numArray = new byte[2];
      int num1 = 8 + (this.windowBits - 8 << 4) << 8;
      int num2 = (this.compressionLevel - 1 & (int) byte.MaxValue) >> 1;
      if (num2 > 3)
        num2 = 3;
      int num3 = num1 | num2 << 6;
      int num4 = num3 + (31 - num3 % 31);
      numArray[0] = (byte) (num4 >> 8);
      numArray[1] = (byte) num4;
      this.Header.Buffer = numArray;
    }

    internal void DownHeap(short[] tree, int nodeIndex)
    {
      int index = nodeIndex << 1;
      int nodeIndex1 = this.Heap[nodeIndex];
      for (; index <= this.HeapLength; index <<= 1)
      {
        if (index < this.HeapLength && DeflateCompressor.IsSmaller(tree, this.Heap[index + 1], this.Heap[index], this.Depth))
          ++index;
        if (!DeflateCompressor.IsSmaller(tree, nodeIndex1, this.Heap[index], this.Depth))
        {
          this.Heap[nodeIndex] = this.Heap[index];
          nodeIndex = index;
        }
        else
          break;
      }
      this.Heap[nodeIndex] = nodeIndex1;
    }

    protected override bool ProcessTransform(bool finalBlock)
    {
      this.Deflate(finalBlock);
      return this.pendingCount != 0;
    }

    private static bool IsSmaller(short[] tree, int nodeIndex1, int nodeIndex2, sbyte[] depth)
    {
      short num1 = tree[nodeIndex1 * 2];
      short num2 = tree[nodeIndex2 * 2];
      if ((int) num1 < (int) num2)
        return true;
      if ((int) num1 == (int) num2)
        return (int) depth[nodeIndex1] <= (int) depth[nodeIndex2];
      return false;
    }

    private void Deflate(bool flush)
    {
      if (this.AvailableBytesOut <= 1)
        throw new InvalidOperationException("Output buffer is full");
      if (this.pendingCount != 0)
      {
        this.FlushPending();
        if (this.AvailableBytesOut == 0)
          return;
      }
      else if (this.AvailableBytesIn == 0 && !flush)
        return;
      if (this.finishPending && this.AvailableBytesIn != 0)
        throw new InvalidOperationException("Status is finish, but input bytes count is not zero");
      if (this.AvailableBytesIn == 0 && this.lookAhead == 0 && (!flush || this.finishPending))
        return;
      DeflateBlockState deflateBlockState = this.deflateFunction(flush);
      if (deflateBlockState == DeflateBlockState.FinishStarted || deflateBlockState == DeflateBlockState.FinishDone)
        this.finishPending = true;
      if (deflateBlockState == DeflateBlockState.NeedMore || deflateBlockState == DeflateBlockState.FinishStarted || deflateBlockState != DeflateBlockState.BlockDone)
        return;
      this.SendStoredBlock(0, 0, false);
      this.FlushPending();
      int availableBytesOut = this.AvailableBytesOut;
    }

    private void ScanTree(short[] tree, int maxCode)
    {
      int num1 = -1;
      int num2 = (int) tree[1];
      int num3 = 0;
      int num4 = 7;
      int num5 = 4;
      if (num2 == 0)
      {
        num4 = 138;
        num5 = 3;
      }
      tree[(maxCode + 1) * 2 + 1] = short.MaxValue;
      for (int index = 0; index <= maxCode; ++index)
      {
        int num6 = num2;
        num2 = (int) tree[(index + 1) * 2 + 1];
        if (++num3 >= num4 || num6 != num2)
        {
          if (num3 < num5)
            this.dynamicBitLengthTree[num6 * 2] = (short) ((int) this.dynamicBitLengthTree[num6 * 2] + num3);
          else if (num6 != 0)
          {
            if (num6 != num1)
              ++this.dynamicBitLengthTree[num6 * 2];
            ++this.dynamicBitLengthTree[32];
          }
          else if (num3 <= 10)
            ++this.dynamicBitLengthTree[34];
          else
            ++this.dynamicBitLengthTree[36];
          num3 = 0;
          num1 = num6;
          if (num2 == 0)
          {
            num4 = 138;
            num5 = 3;
          }
          else if (num6 == num2)
          {
            num4 = 6;
            num5 = 3;
          }
          else
          {
            num4 = 7;
            num5 = 4;
          }
        }
      }
    }

    private int BuildBitLengthTree()
    {
      this.ScanTree(this.dynamicLiteralsTree, this.treeLiterals.MaxCode);
      this.ScanTree(this.dynamicDistanceTree, this.treeDistances.MaxCode);
      this.treeBitLengths.BuildTree(this);
      int index = 18;
      while (index >= 3 && this.dynamicBitLengthTree[(int) Tree.BitLengthOrder[index] * 2 + 1] == (short) 0)
        --index;
      this.OptimalLength += 3 * (index + 1) + 5 + 5 + 4;
      return index;
    }

    private void SendAllTrees(int literalCodes, int distanceCodes, int bitLengthCodes)
    {
      this.SendBits(literalCodes - 257, 5);
      this.SendBits(distanceCodes - 1, 5);
      this.SendBits(bitLengthCodes - 4, 4);
      for (int index = 0; index < bitLengthCodes; ++index)
        this.SendBits((int) this.dynamicBitLengthTree[(int) Tree.BitLengthOrder[index] * 2 + 1], 3);
      this.SendTree(this.dynamicLiteralsTree, literalCodes - 1);
      this.SendTree(this.dynamicDistanceTree, distanceCodes - 1);
    }

    private void SendTree(short[] tree, int maxCode)
    {
      int num1 = -1;
      int num2 = (int) tree[1];
      int num3 = 0;
      int num4 = 7;
      int num5 = 4;
      if (num2 == 0)
      {
        num4 = 138;
        num5 = 3;
      }
      for (int index = 0; index <= maxCode; ++index)
      {
        int nodeIndex = num2;
        num2 = (int) tree[(index + 1) * 2 + 1];
        if (++num3 >= num4 || nodeIndex != num2)
        {
          if (num3 < num5)
          {
            do
            {
              this.SendCode(nodeIndex, this.dynamicBitLengthTree);
            }
            while (--num3 != 0);
          }
          else if (nodeIndex != 0)
          {
            if (nodeIndex != num1)
            {
              this.SendCode(nodeIndex, this.dynamicBitLengthTree);
              --num3;
            }
            this.SendCode(16, this.dynamicBitLengthTree);
            this.SendBits(num3 - 3, 2);
          }
          else if (num3 <= 10)
          {
            this.SendCode(17, this.dynamicBitLengthTree);
            this.SendBits(num3 - 3, 3);
          }
          else
          {
            this.SendCode(18, this.dynamicBitLengthTree);
            this.SendBits(num3 - 11, 7);
          }
          num3 = 0;
          num1 = nodeIndex;
          if (num2 == 0)
          {
            num4 = 138;
            num5 = 3;
          }
          else if (nodeIndex == num2)
          {
            num4 = 6;
            num5 = 3;
          }
          else
          {
            num4 = 7;
            num5 = 4;
          }
        }
      }
    }

    private void PutBytes(byte[] buffer, int start, int length)
    {
      Array.Copy((Array) buffer, start, (Array) this.pending, this.pendingCount, length);
      this.pendingCount += length;
    }

    private void SendCode(int nodeIndex, short[] tree)
    {
      int index = nodeIndex * 2;
      this.SendBits((int) tree[index] & (int) ushort.MaxValue, (int) tree[index + 1] & (int) ushort.MaxValue);
    }

    private void SendBits(int value, int length)
    {
      int num = length;
      this.bitsBuffer |= (short) (value << this.bitsValid & (int) ushort.MaxValue);
      if (this.bitsValid > 16 - num)
      {
        this.pending[this.pendingCount++] = (byte) this.bitsBuffer;
        this.pending[this.pendingCount++] = (byte) ((uint) this.bitsBuffer >> 8);
        this.bitsBuffer = (short) ((uint) value >> 16 - this.bitsValid);
        this.bitsValid += num - 16;
      }
      else
        this.bitsValid += num;
    }

    private bool TreeTally(int distance, int lengthOrChar)
    {
      this.pending[this.distanceOffset + this.lastLiteral * 2] = (byte) ((uint) distance >> 8);
      this.pending[this.distanceOffset + this.lastLiteral * 2 + 1] = (byte) distance;
      this.pending[this.lengthOffset + this.lastLiteral] = (byte) lengthOrChar;
      ++this.lastLiteral;
      if (distance == 0)
      {
        ++this.dynamicLiteralsTree[lengthOrChar * 2];
      }
      else
      {
        ++this.matches;
        --distance;
        ++this.dynamicLiteralsTree[((int) Tree.LengthCode[lengthOrChar] + 256 + 1) * 2];
        ++this.dynamicDistanceTree[Tree.GetDistanceCode(distance) * 2];
      }
      if ((this.lastLiteral & 8191) == 0 && this.compressionLevel > 2)
      {
        int num1 = this.lastLiteral << 3;
        int num2 = this.startOfString - this.blockStart;
        for (int index = 0; index < 30; ++index)
          num1 = (int) ((long) num1 + (long) this.dynamicDistanceTree[index * 2] * (5L + (long) Tree.ExtraDistanceBits[index]));
        int num3 = num1 >> 3;
        if (this.matches < this.lastLiteral / 2 && num3 < num2 / 2)
          return true;
      }
      if (this.lastLiteral != this.literalsBufsize - 1)
        return this.lastLiteral == this.literalsBufsize;
      return true;
    }

    private void SendCompressedBlock(short[] literalTree, short[] distanceTree)
    {
      int num1 = 0;
      if (this.lastLiteral != 0)
      {
        do
        {
          int index1 = this.distanceOffset + num1 * 2;
          int num2 = (int) this.pending[index1] << 8 & 65280 | (int) this.pending[index1 + 1] & (int) byte.MaxValue;
          int nodeIndex = (int) this.pending[this.lengthOffset + num1] & (int) byte.MaxValue;
          ++num1;
          if (num2 == 0)
          {
            this.SendCode(nodeIndex, literalTree);
          }
          else
          {
            int index2 = (int) Tree.LengthCode[nodeIndex];
            this.SendCode(index2 + 256 + 1, literalTree);
            int extraLengthBit = Tree.ExtraLengthBits[index2];
            if (extraLengthBit != 0)
              this.SendBits(nodeIndex - Tree.LengthBase[index2], extraLengthBit);
            int distance = num2 - 1;
            int distanceCode = Tree.GetDistanceCode(distance);
            this.SendCode(distanceCode, distanceTree);
            int extraDistanceBit = Tree.ExtraDistanceBits[distanceCode];
            if (extraDistanceBit != 0)
              this.SendBits(distance - Tree.DistanceBase[distanceCode], extraDistanceBit);
          }
        }
        while (num1 < this.lastLiteral);
      }
      this.SendCode(256, literalTree);
    }

    private void AlginOnByteBoundary()
    {
      if (this.bitsValid > 8)
      {
        this.pending[this.pendingCount++] = (byte) this.bitsBuffer;
        this.pending[this.pendingCount++] = (byte) ((uint) this.bitsBuffer >> 8);
      }
      else if (this.bitsValid > 0)
        this.pending[this.pendingCount++] = (byte) this.bitsBuffer;
      this.bitsBuffer = (short) 0;
      this.bitsValid = 0;
    }

    private void CopyBlock(int buffer, int length, bool header)
    {
      this.AlginOnByteBoundary();
      if (header)
      {
        this.pending[this.pendingCount++] = (byte) length;
        this.pending[this.pendingCount++] = (byte) (length >> 8);
        this.pending[this.pendingCount++] = (byte) ~length;
        this.pending[this.pendingCount++] = (byte) (~length >> 8);
      }
      this.PutBytes(this.window, buffer, length);
    }

    private void FlushBlockOnly(bool lastBlock)
    {
      this.TreeFlushBlock(this.blockStart >= 0 ? this.blockStart : -1, this.startOfString - this.blockStart, lastBlock);
      this.blockStart = this.startOfString;
      this.FlushPending();
    }

    private void SendStoredBlock(int offset, int length, bool lastBlock)
    {
      this.SendBits(lastBlock ? 1 : 0, 3);
      this.CopyBlock(offset, length, true);
    }

    private void TreeFlushBlock(int offset, int length, bool lastBlock)
    {
      int num1 = 0;
      int num2;
      int num3;
      if (this.compressionLevel > 0)
      {
        this.treeLiterals.BuildTree(this);
        this.treeDistances.BuildTree(this);
        num1 = this.BuildBitLengthTree();
        num2 = this.OptimalLength + 3 + 7 >> 3;
        num3 = this.StaticLength + 3 + 7 >> 3;
        if (num3 <= num2)
          num2 = num3;
      }
      else
        num2 = num3 = length + 5;
      if (length + 4 <= num2 && offset != -1)
      {
        this.SendStoredBlock(offset, length, lastBlock);
      }
      else
      {
        int num4 = lastBlock ? 1 : 0;
        if (num3 == num2)
        {
          this.SendBits(2 + num4, 3);
          this.SendCompressedBlock(StaticTree.LengthAndLiteralsTreeCodes, StaticTree.DistTreeCodes);
        }
        else
        {
          this.SendBits(4 + num4, 3);
          this.SendAllTrees(this.treeLiterals.MaxCode + 1, this.treeDistances.MaxCode + 1, num1 + 1);
          this.SendCompressedBlock(this.dynamicLiteralsTree, this.dynamicDistanceTree);
        }
      }
      this.InitializeBlocks();
      if (!lastBlock)
        return;
      this.AlginOnByteBoundary();
    }

    private void FillWindow()
    {
      do
      {
        int size = this.actualWindowSize - this.lookAhead - this.startOfString;
        if (size == 0 && this.startOfString == 0 && this.lookAhead == 0)
          size = this.windowSize;
        else if (size == -1)
          --size;
        else if (this.startOfString >= this.windowSize + this.windowSize - 262)
        {
          Array.Copy((Array) this.window, this.windowSize, (Array) this.window, 0, this.windowSize);
          this.matchStart -= this.windowSize;
          this.startOfString -= this.windowSize;
          this.blockStart -= this.windowSize;
          int hashSize = this.hashSize;
          int index1 = hashSize;
          do
          {
            int num = (int) this.head[--index1] & (int) ushort.MaxValue;
            this.head[index1] = num >= this.windowSize ? (short) (num - this.windowSize) : (short) 0;
          }
          while (--hashSize != 0);
          int windowSize = this.windowSize;
          int index2 = windowSize;
          do
          {
            int num = (int) this.previous[--index2] & (int) ushort.MaxValue;
            this.previous[index2] = num >= this.windowSize ? (short) (num - this.windowSize) : (short) 0;
          }
          while (--windowSize != 0);
          size += this.windowSize;
        }
        if (this.AvailableBytesIn == 0)
          break;
        this.lookAhead += this.ReadBuffer(this.window, this.startOfString + this.lookAhead, size);
        if (this.lookAhead >= 3)
        {
          this.hashIndexOfString = (int) this.window[this.startOfString] & (int) byte.MaxValue;
          this.hashIndexOfString = (this.hashIndexOfString << this.hashShift ^ (int) this.window[this.startOfString + 1] & (int) byte.MaxValue) & this.hashMask;
        }
      }
      while (this.lookAhead < 262 && this.AvailableBytesIn != 0);
    }

    private DeflateBlockState DeflateFast(bool flush)
    {
      int currentMatch = 0;
      do
      {
        bool flag;
        do
        {
          if (this.lookAhead < 262)
          {
            this.FillWindow();
            if (this.lookAhead < 262 && !flush)
              return DeflateBlockState.NeedMore;
            if (this.lookAhead == 0)
              goto label_19;
          }
          if (this.lookAhead >= 3)
            currentMatch = this.UpdateHead();
          if (currentMatch != 0 && (this.startOfString - currentMatch & (int) ushort.MaxValue) <= this.windowSize - 262)
            this.matchLength = this.LongestMatch(currentMatch);
          if (this.matchLength >= 3)
          {
            flag = this.TreeTally(this.startOfString - this.matchStart, this.matchLength - 3);
            this.lookAhead -= this.matchLength;
            if (this.matchLength <= this.maxLazy && this.lookAhead >= 3)
            {
              --this.matchLength;
              do
              {
                ++this.startOfString;
                currentMatch = this.UpdateHead();
              }
              while (--this.matchLength != 0);
              ++this.startOfString;
            }
            else
            {
              this.startOfString += this.matchLength;
              this.matchLength = 0;
              this.hashIndexOfString = (int) this.window[this.startOfString] & (int) byte.MaxValue;
              this.hashIndexOfString = (this.hashIndexOfString << this.hashShift ^ (int) this.window[this.startOfString + 1] & (int) byte.MaxValue) & this.hashMask;
            }
          }
          else
          {
            flag = this.TreeTally(0, (int) this.window[this.startOfString] & (int) byte.MaxValue);
            --this.lookAhead;
            ++this.startOfString;
          }
        }
        while (!flag);
        this.FlushBlockOnly(false);
      }
      while (this.AvailableBytesOut != 0);
      return DeflateBlockState.NeedMore;
label_19:
      return this.CompleteFlushBlock(flush);
    }

    private int UpdateHead()
    {
      this.hashIndexOfString = (this.hashIndexOfString << this.hashShift ^ (int) this.window[this.startOfString + 3 - 1] & (int) byte.MaxValue) & this.hashMask;
      int num = (int) this.head[this.hashIndexOfString] & (int) ushort.MaxValue;
      this.previous[this.startOfString & this.windowMask] = this.head[this.hashIndexOfString];
      this.head[this.hashIndexOfString] = (short) this.startOfString;
      return num;
    }

    private DeflateBlockState CompleteFlushBlock(bool flush)
    {
      this.FlushBlockOnly(flush);
      if (this.AvailableBytesOut == 0)
        return !flush ? DeflateBlockState.NeedMore : DeflateBlockState.FinishStarted;
      return !flush ? DeflateBlockState.BlockDone : DeflateBlockState.FinishDone;
    }

    private DeflateBlockState DeflateNone(bool flush)
    {
      int num1 = (int) ushort.MaxValue;
      if (num1 > this.pending.Length - 5)
        num1 = this.pending.Length - 5;
      do
      {
        do
        {
          if (this.lookAhead <= 1)
          {
            this.FillWindow();
            if (this.lookAhead == 0 && !flush)
              return DeflateBlockState.NeedMore;
            if (this.lookAhead == 0)
              goto label_12;
          }
          this.startOfString += this.lookAhead;
          this.lookAhead = 0;
          int num2 = this.blockStart + num1;
          if (this.startOfString == 0 || this.startOfString >= num2)
          {
            this.lookAhead = this.startOfString - num2;
            this.startOfString = num2;
            this.FlushBlockOnly(false);
            if (this.AvailableBytesOut == 0)
              return DeflateBlockState.NeedMore;
          }
        }
        while (this.startOfString - this.blockStart < this.windowSize - 262);
        this.FlushBlockOnly(false);
      }
      while (this.AvailableBytesOut != 0);
      return DeflateBlockState.NeedMore;
label_12:
      return this.CompleteFlushBlock(flush);
    }

    private DeflateBlockState DeflateSlow(bool flush)
    {
      int currentMatch = 0;
      while (true)
      {
        do
        {
          if (this.lookAhead < 262)
          {
            this.FillWindow();
            if (this.lookAhead < 262 && !flush)
              return DeflateBlockState.NeedMore;
            if (this.lookAhead == 0)
              goto label_24;
          }
          if (this.lookAhead >= 3)
            currentMatch = this.UpdateHead();
          this.previousLength = this.matchLength;
          this.previousMatch = this.matchStart;
          this.matchLength = 2;
          if (currentMatch != 0 && this.previousLength < this.maxLazy && (this.startOfString - currentMatch & (int) ushort.MaxValue) <= this.windowSize - 262)
          {
            this.matchLength = this.LongestMatch(currentMatch);
            if (this.matchLength <= 5 && this.matchLength == 3 && this.startOfString - this.matchStart > 4096)
              this.matchLength = 2;
          }
          if (this.previousLength >= 3 && this.matchLength <= this.previousLength)
          {
            int num = this.startOfString + this.lookAhead - 3;
            bool flag = this.TreeTally(this.startOfString - 1 - this.previousMatch, this.previousLength - 3);
            this.lookAhead -= this.previousLength - 1;
            this.previousLength -= 2;
            do
            {
              if (++this.startOfString <= num)
                currentMatch = this.UpdateHead();
            }
            while (--this.previousLength != 0);
            this.matchAvailable = 0;
            this.matchLength = 2;
            ++this.startOfString;
            if (flag)
            {
              this.FlushBlockOnly(false);
              if (this.AvailableBytesOut == 0)
                return DeflateBlockState.NeedMore;
            }
          }
          else if (this.matchAvailable != 0)
          {
            if (this.TreeTally(0, (int) this.window[this.startOfString - 1] & (int) byte.MaxValue))
              this.FlushBlockOnly(false);
            ++this.startOfString;
            --this.lookAhead;
          }
          else
            goto label_23;
        }
        while (this.AvailableBytesOut != 0);
        break;
label_23:
        this.matchAvailable = 1;
        ++this.startOfString;
        --this.lookAhead;
      }
      return DeflateBlockState.NeedMore;
label_24:
      if (this.matchAvailable != 0)
      {
        this.TreeTally(0, (int) this.window[this.startOfString - 1] & (int) byte.MaxValue);
        this.matchAvailable = 0;
      }
      return this.CompleteFlushBlock(flush);
    }

    private int LongestMatch(int currentMatch)
    {
      int maxChainLength = this.maxChainLength;
      int index1 = this.startOfString;
      int num1 = this.previousLength;
      int num2 = this.startOfString > this.windowSize - 262 ? this.startOfString - (this.windowSize - 262) : 0;
      int num3 = this.niceLength;
      int windowMask = this.windowMask;
      int num4 = this.startOfString + 258;
      int index2 = index1 + num1;
      byte num5 = this.window[index2 - 1];
      byte num6 = this.window[index2];
      if (this.previousLength >= this.goodLength)
        maxChainLength >>= 2;
      if (num3 > this.lookAhead)
        num3 = this.lookAhead;
      do
      {
        int index3 = currentMatch;
        int num7;
        if ((int) this.window[index3 + num1] == (int) num6 && (int) this.window[index3 + num1 - 1] == (int) num5 && ((int) this.window[index3] == (int) this.window[index1] && (int) this.window[num7 = index3 + 1] == (int) this.window[index1 + 1]))
        {
          int num8 = index1 + 2;
          int num9 = num7 + 1;
          int num10;
          int num11;
          int num12;
          int num13;
          int num14;
          int num15;
          int num16;
          do
            ;
          while ((int) this.window[++num8] == (int) this.window[num10 = num9 + 1] && (int) this.window[++num8] == (int) this.window[num11 = num10 + 1] && ((int) this.window[++num8] == (int) this.window[num12 = num11 + 1] && (int) this.window[++num8] == (int) this.window[num13 = num12 + 1]) && ((int) this.window[++num8] == (int) this.window[num14 = num13 + 1] && (int) this.window[++num8] == (int) this.window[num15 = num14 + 1] && ((int) this.window[++num8] == (int) this.window[num16 = num15 + 1] && (int) this.window[++num8] == (int) this.window[num9 = num16 + 1])) && num8 < num4);
          int num17 = 258 - (num4 - num8);
          index1 = num4 - 258;
          if (num17 > num1)
          {
            this.matchStart = currentMatch;
            num1 = num17;
            if (num17 < num3)
            {
              num5 = this.window[index1 + num1 - 1];
              num6 = this.window[index1 + num1];
            }
            else
              break;
          }
        }
      }
      while ((currentMatch = (int) this.previous[currentMatch & windowMask] & (int) ushort.MaxValue) > num2 && --maxChainLength != 0);
      if (num1 <= this.lookAhead)
        return num1;
      return this.lookAhead;
    }

    private void Initialize(DeflateSettings deflateSettings)
    {
      this.compressionLevel = (int) deflateSettings.CompressionLevel;
      this.windowBits = 15;
      int num = 8;
      this.windowSize = 1 << this.windowBits;
      this.windowMask = this.windowSize - 1;
      this.hashBits = num + 7;
      this.hashSize = 1 << this.hashBits;
      this.hashMask = this.hashSize - 1;
      this.hashShift = (this.hashBits + 3 - 1) / 3;
      this.window = new byte[this.windowSize * 2];
      this.previous = new short[this.windowSize];
      this.head = new short[this.hashSize];
      this.literalsBufsize = 1 << num + 6;
      this.pending = new byte[this.literalsBufsize * 4];
      this.distanceOffset = this.literalsBufsize;
      this.lengthOffset = 3 * this.literalsBufsize;
      this.TotalBytesIn = this.TotalBytesOut = 0;
      this.pendingCount = 0;
      this.nextPending = 0;
      this.InitializeTreeData();
      this.InitializeLazyMatch();
    }

    private void InitializeLazyMatch()
    {
      this.actualWindowSize = 2 * this.windowSize;
      Array.Clear((Array) this.head, 0, this.hashSize);
      this.SetConfiguration(this.compressionLevel);
      this.startOfString = 0;
      this.blockStart = 0;
      this.lookAhead = 0;
      this.matchLength = this.previousLength = 2;
      this.matchAvailable = 0;
      this.hashIndexOfString = 0;
    }

    private void InitializeTreeData()
    {
      this.treeLiterals.DynamicTree = this.dynamicLiteralsTree;
      this.treeLiterals.StaticTree = StaticTree.Literals;
      this.treeDistances.DynamicTree = this.dynamicDistanceTree;
      this.treeDistances.StaticTree = StaticTree.Distances;
      this.treeBitLengths.DynamicTree = this.dynamicBitLengthTree;
      this.treeBitLengths.StaticTree = StaticTree.BitLengths;
      this.bitsBuffer = (short) 0;
      this.bitsValid = 0;
      this.InitializeBlocks();
    }

    private void InitializeBlocks()
    {
      for (int index = 0; index < 286; ++index)
        this.dynamicLiteralsTree[index * 2] = (short) 0;
      for (int index = 0; index < 30; ++index)
        this.dynamicDistanceTree[index * 2] = (short) 0;
      for (int index = 0; index < 19; ++index)
        this.dynamicBitLengthTree[index * 2] = (short) 0;
      this.dynamicLiteralsTree[512] = (short) 1;
      this.OptimalLength = this.StaticLength = 0;
      this.lastLiteral = this.matches = 0;
    }

    private void SetConfiguration(int level)
    {
      DeflateConfiguration deflateConfiguration = DeflateConfiguration.Lookup(level);
      this.goodLength = deflateConfiguration.GoodLength;
      this.maxChainLength = deflateConfiguration.MaxChainLength;
      this.maxLazy = deflateConfiguration.MaxLazy;
      this.niceLength = deflateConfiguration.NiceLength;
      if (level > 0)
      {
        if (level > 3)
          this.deflateFunction = new DeflateCompressor.CompressionMethod(this.DeflateSlow);
        else
          this.deflateFunction = new DeflateCompressor.CompressionMethod(this.DeflateFast);
      }
      else
        this.deflateFunction = new DeflateCompressor.CompressionMethod(this.DeflateNone);
    }

    private void FlushPending()
    {
      int length = this.pendingCount;
      if (length > this.AvailableBytesOut)
        length = this.AvailableBytesOut;
      if (length == 0)
        return;
      if (this.pending.Length <= this.nextPending || this.OutputBuffer.Length <= this.NextOut || (this.pending.Length < this.nextPending + length || this.OutputBuffer.Length < this.NextOut + length))
        throw new InvalidOperationException(string.Format("Invalid State. (pending.Length={0}, pendingCount={1})", (object) this.pending.Length, (object) this.pendingCount));
      Array.Copy((Array) this.pending, this.nextPending, (Array) this.OutputBuffer, this.NextOut, length);
      this.NextOut += length;
      this.nextPending += length;
      this.TotalBytesOut += length;
      this.AvailableBytesOut -= length;
      this.pendingCount -= length;
      if (this.pendingCount != 0)
        return;
      this.nextPending = 0;
    }

    private int ReadBuffer(byte[] buffer, int start, int size)
    {
      int length = this.AvailableBytesIn;
      if (length > size)
        length = size;
      if (length == 0)
        return 0;
      this.AvailableBytesIn -= length;
      Array.Copy((Array) this.InputBuffer, this.NextIn, (Array) buffer, start, length);
      this.NextIn += length;
      this.TotalBytesIn += length;
      return length;
    }

    private delegate DeflateBlockState CompressionMethod(bool flush);
  }
}
