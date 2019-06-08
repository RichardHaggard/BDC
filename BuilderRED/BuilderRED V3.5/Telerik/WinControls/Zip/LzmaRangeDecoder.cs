// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaRangeDecoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;

namespace Telerik.WinControls.Zip
{
  internal class LzmaRangeDecoder
  {
    private Collection<byte[]> buffers = new Collection<byte[]>();
    private bool inputRequired = true;
    public const uint TopValue = 16777216;
    private uint range;
    private uint code;
    private byte[] buffer;
    private int lastBufferSize;
    private int index;
    private uint savedRange;
    private uint savedCode;
    private int savedIndex;

    public bool FinalBlock { get; set; }

    public uint Range
    {
      get
      {
        return this.range;
      }
    }

    public uint Code
    {
      get
      {
        return this.code;
      }
    }

    public bool InputRequired
    {
      get
      {
        return this.inputRequired;
      }
    }

    public void Init(byte[] inputBuffer, int startIndex)
    {
      this.code = 0U;
      this.range = uint.MaxValue;
      for (int index = 0; index < 5; ++index)
        this.code = this.code << 8 | (uint) inputBuffer[startIndex + index];
    }

    public bool CheckInputRequired(int length, bool ignoreFinalBlock = false)
    {
      if (this.buffers.Count < 2 && (!this.FinalBlock || ignoreFinalBlock))
        this.inputRequired = this.lastBufferSize - this.index < length;
      return !this.inputRequired;
    }

    public void SetBuffer(byte[] inputBuffer, int length)
    {
      if (length <= 0)
        return;
      this.lastBufferSize = length;
      this.buffers.Add(inputBuffer);
      if (this.buffers.Count == 1)
      {
        this.buffer = this.buffers[0];
        this.index = 0;
      }
      this.inputRequired = false;
    }

    public uint DecodeDirectBits(int numTotalBits)
    {
      uint num1 = 0;
      for (int index = numTotalBits; index > 0; --index)
      {
        this.range >>= 1;
        uint num2 = this.code - this.range >> 31;
        this.code -= this.range & num2 - 1U;
        num1 = (uint) ((int) num1 << 1 | 1 - (int) num2);
        if (this.range < 16777216U)
        {
          this.code = this.code << 8 | (uint) this.GetNextByte();
          this.range <<= 8;
        }
      }
      return num1;
    }

    public void MoveRange(uint newBound)
    {
      this.range -= newBound;
      this.code -= newBound;
      this.CheckRange();
    }

    public void UpdateRange(uint newBound)
    {
      this.range = newBound;
      this.CheckRange();
    }

    public void SaveState()
    {
      this.savedIndex = this.index;
      this.savedRange = this.range;
      this.savedCode = this.code;
    }

    public void RestoreState()
    {
      this.index = this.savedIndex;
      this.range = this.savedRange;
      this.code = this.savedCode;
    }

    private void CheckRange()
    {
      if (this.range >= 16777216U)
        return;
      this.code = this.code << 8 | (uint) this.GetNextByte();
      this.range <<= 8;
    }

    private byte GetNextByte()
    {
      if (this.buffers.Count == 1 && this.index >= this.lastBufferSize || this.index >= this.buffer.Length)
      {
        if (this.buffers.Count < 2)
        {
          if (this.inputRequired)
            throw new InvalidOperationException("Decoder must check for request next block.");
          this.inputRequired = true;
          return 0;
        }
        this.buffers.RemoveAt(0);
        this.buffer = this.buffers[0];
        this.index = 0;
      }
      return this.buffer[this.index++];
    }
  }
}
