// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaBinaryTree
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaBinaryTree
  {
    private uint cutValue = (uint) byte.MaxValue;
    private bool useHashArray = true;
    private uint minMatchCheck = 4;
    private uint fixHashSize = 66560;
    private const uint Hash2Size = 1024;
    private const uint Hash3Size = 65536;
    private const uint BT2HashSize = 65536;
    private const uint StartMaxLen = 1;
    private const uint Hash3Offset = 1024;
    private const uint EmptyHashValue = 0;
    private const uint NormalizeMaxValue = 2147483647;
    private byte[] buffer;
    private uint positionLimit;
    private bool inputComplete;
    private uint lastSafePosition;
    private uint bufferOffset;
    private uint[] currentDistances;
    private uint currentBufferIndex;
    private uint currentLengthLimit;
    private uint currentMatch;
    private uint currentMatchMaxLen;
    private uint currentMatchMinPos;
    private uint currentOffset;
    private uint blockSize;
    private uint keepSizeBefore;
    private uint keepSizeAfter;
    private uint bufferIndex;
    private uint streamIndex;
    private byte[] inputBuffer;
    private int inputBufferLength;
    private int inputBufferIndex;
    private bool finalBlock;
    private uint cyclicBufferPos;
    private uint cyclicBufferSize;
    private uint matchMaxLength;
    private uint[] son;
    private uint[] hash;
    private uint hashMask;
    private uint hashSizeSum;
    private uint hashDirectBytes;
    private int startInputBufferIndex;

    public LzmaBinaryTree(
      int hashBytes,
      uint historySize,
      uint matchMaxLen,
      uint keepAddBufferAfter)
    {
      this.useHashArray = hashBytes > 2;
      if (this.useHashArray)
      {
        this.hashDirectBytes = 0U;
        this.minMatchCheck = 4U;
        this.fixHashSize = 66560U;
      }
      else
      {
        this.hashDirectBytes = 2U;
        this.minMatchCheck = 3U;
        this.fixHashSize = 0U;
      }
      uint num1 = 4096;
      uint num2 = 256U + (historySize + num1 + matchMaxLen + keepAddBufferAfter) / 2U;
      this.keepSizeBefore = historySize + num1;
      this.keepSizeAfter = matchMaxLen + keepAddBufferAfter;
      this.blockSize = this.keepSizeBefore + this.keepSizeAfter + num2;
      this.buffer = new byte[(IntPtr) this.blockSize];
      this.lastSafePosition = this.blockSize - this.keepSizeAfter;
      this.matchMaxLength = matchMaxLen;
      this.cutValue = 16U + (this.matchMaxLength >> 1);
      uint num3 = historySize + 1U;
      if ((int) this.cyclicBufferSize != (int) num3)
        this.son = new uint[(IntPtr) ((this.cyclicBufferSize = num3) * 2U)];
      this.CreateHash(historySize);
      this.Init();
    }

    public uint AvailableBytes
    {
      get
      {
        return this.streamIndex - this.bufferIndex;
      }
    }

    public bool HasInput
    {
      get
      {
        return this.inputBufferIndex - this.startInputBufferIndex < this.inputBufferLength;
      }
    }

    public bool Ready
    {
      get
      {
        return this.inputComplete || -(int) this.bufferOffset + (int) this.blockSize - (int) this.streamIndex == 0;
      }
    }

    public bool ReadyToMove
    {
      get
      {
        if (!this.inputComplete)
        {
          uint num = this.bufferIndex + 1U;
          if (num > this.positionLimit && this.bufferOffset + num > this.lastSafePosition)
            return true;
        }
        return false;
      }
    }

    public uint GetMatches(uint[] distances)
    {
      this.GetLengthLimit();
      if (this.currentLengthLimit == 0U)
        return this.currentLengthLimit;
      this.SetInitialMatchValues(distances);
      this.CalculateHashValues(true);
      this.CheckDirectBytes();
      this.ProcessMatches(true);
      return this.currentOffset;
    }

    public void Skip(uint counter)
    {
      do
      {
        this.GetLengthLimit();
        if (this.currentLengthLimit != 0U)
        {
          this.SetInitialMatchValues((uint[]) null);
          this.CalculateHashValues(false);
          this.ProcessMatches(false);
        }
      }
      while (--counter != 0U);
    }

    public void MoveBlock()
    {
      uint num = this.bufferOffset + this.bufferIndex - this.keepSizeBefore;
      if (num > 0U)
        --num;
      int length = (int) this.bufferOffset + (int) this.streamIndex - (int) num;
      Array.Copy((Array) this.buffer, (int) num, (Array) this.buffer, 0, length);
      this.bufferOffset -= num;
    }

    public void SetInputBuffer(byte[] buffer, int offset, int length, bool finalBlock)
    {
      if (this.HasInput)
        this.ReadCicleBlock();
      int num = this.HasInput ? 1 : 0;
      this.inputBuffer = buffer;
      this.inputBufferLength = length;
      this.startInputBufferIndex = this.inputBufferIndex = offset;
      this.finalBlock = finalBlock;
      this.ReadCicleBlock();
    }

    public byte GetIndexByte(int index)
    {
      return this.buffer[(long) (this.bufferOffset + this.bufferIndex) + (long) index];
    }

    public uint GetMatchLen(int index, uint distance, uint limit)
    {
      if (this.inputComplete && (long) this.bufferIndex + (long) index + (long) limit > (long) this.streamIndex)
        limit = this.streamIndex - (uint) ((ulong) this.bufferIndex + (ulong) index);
      ++distance;
      uint num1 = (uint) ((int) this.bufferOffset + (int) this.bufferIndex + index);
      uint num2 = 0;
      while (num2 < limit && (int) this.buffer[(IntPtr) (num1 + num2)] == (int) this.buffer[(IntPtr) (num1 + num2 - distance)])
        ++num2;
      return num2;
    }

    private static void NormalizeLinks(uint[] items, uint itemsCounter, uint subValue)
    {
      for (uint index = 0; index < itemsCounter; ++index)
      {
        uint num1 = items[(IntPtr) index];
        uint num2 = num1 > subValue ? num1 - subValue : 0U;
        items[(IntPtr) index] = num2;
      }
    }

    private void Init()
    {
      this.bufferOffset = 0U;
      this.bufferIndex = 0U;
      this.streamIndex = 0U;
      this.inputComplete = false;
      for (uint index = 0; index < this.hashSizeSum; ++index)
        this.hash[(IntPtr) index] = 0U;
      this.cyclicBufferPos = 0U;
      this.ReduceOffsets(-1);
    }

    private void CreateHash(uint historySize)
    {
      uint num1 = 65536;
      if (this.useHashArray)
      {
        uint num2 = historySize - 1U;
        uint num3 = num2 | num2 >> 1;
        uint num4 = num3 | num3 >> 2;
        uint num5 = num4 | num4 >> 4;
        uint num6 = (num5 | num5 >> 8) >> 1 | (uint) ushort.MaxValue;
        if (num6 > 16777216U)
          num6 >>= 1;
        this.hashMask = num6;
        num1 = num6 + 1U + this.fixHashSize;
      }
      if ((int) num1 == (int) this.hashSizeSum)
        return;
      this.hash = new uint[(IntPtr) (this.hashSizeSum = num1)];
    }

    private void Normalize()
    {
      uint subValue = this.bufferIndex - this.cyclicBufferSize;
      LzmaBinaryTree.NormalizeLinks(this.son, this.cyclicBufferSize * 2U, subValue);
      LzmaBinaryTree.NormalizeLinks(this.hash, this.hashSizeSum, subValue);
      this.ReduceOffsets((int) subValue);
    }

    private void ReduceOffsets(int subValue)
    {
      this.bufferOffset += (uint) subValue;
      this.positionLimit -= (uint) subValue;
      this.bufferIndex -= (uint) subValue;
      this.streamIndex -= (uint) subValue;
    }

    private void ReadBlock()
    {
      if (this.inputComplete)
        return;
      do
      {
        int val1 = -(int) this.bufferOffset + (int) this.blockSize - (int) this.streamIndex;
        if (val1 == 0)
        {
          this.SetRestInputBuffer();
          return;
        }
        int length = Math.Min(val1, this.inputBufferLength + this.startInputBufferIndex - this.inputBufferIndex);
        Array.Copy((Array) this.inputBuffer, this.inputBufferIndex, (Array) this.buffer, (int) this.bufferOffset + (int) this.streamIndex, length);
        this.inputBufferIndex += length;
        this.streamIndex += (uint) length;
        if (this.streamIndex >= this.bufferIndex + this.keepSizeAfter)
          this.positionLimit = this.streamIndex - this.keepSizeAfter;
      }
      while (this.HasInput);
      if (!this.finalBlock)
        return;
      this.SetInputComplete();
    }

    private void SetInputComplete()
    {
      this.positionLimit = this.streamIndex;
      if (this.bufferOffset + this.positionLimit > this.lastSafePosition)
        this.positionLimit = this.lastSafePosition - this.bufferOffset;
      this.inputComplete = true;
    }

    private void SetRestInputBuffer()
    {
      int length = this.inputBufferLength + this.startInputBufferIndex - this.inputBufferIndex;
      byte[] numArray = new byte[length];
      Array.Copy((Array) this.inputBuffer, this.inputBufferIndex, (Array) numArray, 0, length);
      this.startInputBufferIndex = this.inputBufferIndex = 0;
      this.inputBufferLength = length;
      this.inputBuffer = numArray;
    }

    private void ReadCicleBlock()
    {
      this.TryMoveBlock();
      this.ReadBlock();
    }

    private bool TryMoveBlock()
    {
      if (this.bufferIndex <= this.positionLimit || this.bufferOffset + this.bufferIndex <= this.lastSafePosition)
        return false;
      this.MoveBlock();
      return true;
    }

    private void GetLengthLimit()
    {
      if (this.bufferIndex + this.matchMaxLength <= this.streamIndex)
      {
        this.currentLengthLimit = this.matchMaxLength;
      }
      else
      {
        this.currentLengthLimit = this.streamIndex - this.bufferIndex;
        if (this.currentLengthLimit >= this.minMatchCheck)
          return;
        this.MovePos();
        this.currentLengthLimit = 0U;
      }
    }

    private void MovePos()
    {
      if (++this.cyclicBufferPos >= this.cyclicBufferSize)
        this.cyclicBufferPos = 0U;
      ++this.bufferIndex;
      if (this.TryMoveBlock())
        this.ReadBlock();
      if (this.bufferIndex != (uint) int.MaxValue)
        return;
      this.Normalize();
    }

    private void SetInitialMatchValues(uint[] distances = null)
    {
      this.currentDistances = distances;
      this.currentOffset = 0U;
      this.currentMatchMinPos = this.bufferIndex > this.cyclicBufferSize ? this.bufferIndex - this.cyclicBufferSize : 0U;
      this.currentBufferIndex = this.bufferOffset + this.bufferIndex;
      this.currentMatchMaxLen = 1U;
    }

    private void CalculateHashValues(bool matchProcess = true)
    {
      uint currentBufferIndex = this.currentBufferIndex;
      uint num1 = 0;
      uint num2 = 0;
      uint num3;
      if (this.useHashArray)
      {
        uint num4 = Crc32.Table[(int) this.buffer[(IntPtr) currentBufferIndex]] ^ (uint) this.buffer[(IntPtr) (currentBufferIndex + 1U)];
        num1 = num4 & 1023U;
        uint num5 = num4 ^ (uint) this.buffer[(IntPtr) (currentBufferIndex + 2U)] << 8;
        num2 = num5 & (uint) ushort.MaxValue;
        num3 = (num5 ^ Crc32.Table[(int) this.buffer[(IntPtr) (currentBufferIndex + 3U)]] << 5) & this.hashMask;
      }
      else
      {
        num3 = (uint) this.buffer[(IntPtr) currentBufferIndex] ^ (uint) this.buffer[(IntPtr) (currentBufferIndex + 1U)] << 8;
        this.currentMatch = this.hash[(IntPtr) (this.fixHashSize + num3)];
      }
      this.currentMatch = this.hash[(IntPtr) (this.fixHashSize + num3)];
      if (this.useHashArray)
      {
        uint curMatch2 = this.hash[(IntPtr) num1];
        this.hash[(IntPtr) num1] = this.bufferIndex;
        uint curMatch3 = this.hash[(IntPtr) (1024U + num2)];
        this.hash[(IntPtr) (1024U + num2)] = this.bufferIndex;
        if (matchProcess)
          this.UpdateMatches(curMatch2, curMatch3);
      }
      else
      {
        num3 = (uint) this.buffer[(IntPtr) currentBufferIndex] ^ (uint) this.buffer[(IntPtr) (currentBufferIndex + 1U)] << 8;
        this.currentMatch = this.hash[(IntPtr) (this.fixHashSize + num3)];
      }
      this.hash[(IntPtr) (this.fixHashSize + num3)] = this.bufferIndex;
    }

    private void UpdateMatches(uint curMatch2, uint curMatch3)
    {
      if (!this.useHashArray)
        return;
      uint[] currentDistances = this.currentDistances;
      uint currentMatchMinPos = this.currentMatchMinPos;
      uint num1 = this.currentOffset;
      uint currentBufferIndex = this.currentBufferIndex;
      if (curMatch2 > currentMatchMinPos && (int) this.buffer[(IntPtr) (this.bufferOffset + curMatch2)] == (int) this.buffer[(IntPtr) currentBufferIndex])
      {
        uint[] numArray1 = currentDistances;
        int num2 = (int) num1;
        uint num3 = (uint) (num2 + 1);
        uint num4 = (uint) num2;
        int num5 = (int) (this.currentMatchMaxLen = 2U);
        numArray1[(IntPtr) num4] = (uint) num5;
        uint[] numArray2 = currentDistances;
        int num6 = (int) num3;
        num1 = (uint) (num6 + 1);
        uint num7 = (uint) num6;
        int num8 = (int) this.bufferIndex - (int) curMatch2 - 1;
        numArray2[(IntPtr) num7] = (uint) num8;
      }
      if (curMatch3 > currentMatchMinPos && (int) this.buffer[(IntPtr) (this.bufferOffset + curMatch3)] == (int) this.buffer[(IntPtr) currentBufferIndex])
      {
        if ((int) curMatch3 == (int) curMatch2)
          num1 -= 2U;
        uint[] numArray1 = currentDistances;
        int num2 = (int) num1;
        uint num3 = (uint) (num2 + 1);
        uint num4 = (uint) num2;
        int num5 = (int) (this.currentMatchMaxLen = 3U);
        numArray1[(IntPtr) num4] = (uint) num5;
        uint[] numArray2 = currentDistances;
        int num6 = (int) num3;
        num1 = (uint) (num6 + 1);
        uint num7 = (uint) num6;
        int num8 = (int) this.bufferIndex - (int) curMatch3 - 1;
        numArray2[(IntPtr) num7] = (uint) num8;
        curMatch2 = curMatch3;
      }
      if (num1 != 0U && (int) curMatch2 == (int) this.currentMatch)
      {
        num1 -= 2U;
        this.currentMatchMaxLen = 1U;
      }
      this.currentOffset = num1;
    }

    private void CheckDirectBytes()
    {
      if (this.hashDirectBytes == 0U || this.currentMatch <= this.currentMatchMinPos || (int) this.buffer[(IntPtr) (this.bufferOffset + this.currentMatch + this.hashDirectBytes)] == (int) this.buffer[(IntPtr) (this.currentBufferIndex + this.hashDirectBytes)])
        return;
      this.currentDistances[(IntPtr) this.currentOffset++] = this.currentMatchMaxLen = this.hashDirectBytes;
      this.currentDistances[(IntPtr) this.currentOffset++] = (uint) ((int) this.bufferIndex - (int) this.currentMatch - 1);
    }

    private void ProcessMatches(bool matchProcess = true)
    {
      uint pointer1 = this.cyclicBufferPos << 1;
      uint pointer0 = pointer1 + 1U;
      uint currentBufferIndex = this.currentBufferIndex;
      uint currentMatchMinPos = this.currentMatchMinPos;
      uint cutValue = this.cutValue;
      uint val2;
      uint val1 = val2 = this.hashDirectBytes;
      while (true)
      {
        uint currentMatch = this.currentMatch;
        if (currentMatch > currentMatchMinPos && cutValue-- != 0U)
        {
          uint num = this.bufferIndex - currentMatch;
          uint cyclicPosition = (uint) ((num <= this.cyclicBufferPos ? (int) this.cyclicBufferPos - (int) num : (int) this.cyclicBufferPos - (int) num + (int) this.cyclicBufferSize) << 1);
          uint pointerByte1 = this.bufferOffset + currentMatch;
          uint length = Math.Min(val1, val2);
          if ((int) this.buffer[(IntPtr) (pointerByte1 + length)] == (int) this.buffer[(IntPtr) (currentBufferIndex + length)])
          {
            length = this.SkipToLimit(currentBufferIndex, pointerByte1, length);
            if (this.CheckMatchLength(matchProcess, cyclicPosition, length, pointer0, pointer1))
              goto label_8;
          }
          if ((int) this.buffer[(IntPtr) (pointerByte1 + length)] < (int) this.buffer[(IntPtr) (currentBufferIndex + length)])
          {
            this.son[(IntPtr) pointer1] = currentMatch;
            pointer1 = cyclicPosition + 1U;
            this.currentMatch = this.son[(IntPtr) pointer1];
            val2 = length;
          }
          else
          {
            this.son[(IntPtr) pointer0] = currentMatch;
            pointer0 = cyclicPosition;
            this.currentMatch = this.son[(IntPtr) pointer0];
            val1 = length;
          }
        }
        else
          break;
      }
      this.son[(IntPtr) pointer0] = this.son[(IntPtr) pointer1] = 0U;
label_8:
      this.MovePos();
    }

    private uint SkipToLimit(uint current, uint pointerByte1, uint length)
    {
      do
        ;
      while ((int) ++length != (int) this.currentLengthLimit && (int) this.buffer[(IntPtr) (pointerByte1 + length)] == (int) this.buffer[(IntPtr) (current + length)]);
      return length;
    }

    private bool CheckMatchLength(
      bool matchProcess,
      uint cyclicPosition,
      uint length,
      uint pointer0,
      uint pointer1)
    {
      bool flag = this.currentMatchMaxLen < length;
      if (matchProcess && flag)
      {
        uint[] currentDistances = this.currentDistances;
        uint currentOffset = this.currentOffset;
        uint num1 = this.bufferIndex - this.currentMatch;
        uint[] numArray1 = currentDistances;
        int num2 = (int) currentOffset;
        uint num3 = (uint) (num2 + 1);
        uint num4 = (uint) num2;
        int num5 = (int) (this.currentMatchMaxLen = length);
        numArray1[(IntPtr) num4] = (uint) num5;
        uint[] numArray2 = currentDistances;
        int num6 = (int) num3;
        uint num7 = (uint) (num6 + 1);
        uint num8 = (uint) num6;
        int num9 = (int) num1 - 1;
        numArray2[(IntPtr) num8] = (uint) num9;
        this.currentOffset = num7;
      }
      if (matchProcess && !flag || (int) length != (int) this.currentLengthLimit)
        return false;
      this.son[(IntPtr) pointer1] = this.son[(IntPtr) cyclicPosition];
      this.son[(IntPtr) pointer0] = this.son[(IntPtr) (cyclicPosition + 1U)];
      return true;
    }
  }
}
