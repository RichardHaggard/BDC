// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.InputBitsBuffer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class InputBitsBuffer
  {
    private uint bitBuffer;
    private int availableBits;
    private byte[] buffer;
    private int end;
    private int start;

    public int AvailableBits
    {
      get
      {
        return this.availableBits;
      }
    }

    public int AvailableBytes
    {
      get
      {
        return this.end - this.start + this.availableBits / 8;
      }
    }

    public bool InputRequired
    {
      get
      {
        return this.start == this.end;
      }
    }

    public bool CheckAvailable(int count)
    {
      if (this.availableBits < count)
      {
        if (this.InputRequired)
          return false;
        this.bitBuffer |= (uint) this.buffer[this.start++] << this.availableBits;
        this.availableBits += 8;
        if (this.availableBits < count)
        {
          if (this.InputRequired)
            return false;
          this.bitBuffer |= (uint) this.buffer[this.start++] << this.availableBits;
          this.availableBits += 8;
        }
      }
      return true;
    }

    public int GetBits(int count)
    {
      if (!this.CheckAvailable(count))
        return -1;
      int num = (int) this.bitBuffer & (int) InputBitsBuffer.GetBitMask(count);
      this.bitBuffer >>= count;
      this.availableBits -= count;
      return num;
    }

    public int Read(byte[] output, int offset, int length)
    {
      int num1 = 0;
      while (this.availableBits > 0 && length > 0)
      {
        output[offset++] = (byte) this.bitBuffer;
        this.bitBuffer >>= 8;
        this.availableBits -= 8;
        --length;
        ++num1;
      }
      if (length == 0)
        return num1;
      int num2 = this.end - this.start;
      if (length > num2)
        length = num2;
      Array.Copy((Array) this.buffer, this.start, (Array) output, offset, length);
      this.start += length;
      return num1 + length;
    }

    public void SetBuffer(byte[] buffer, int offset, int length)
    {
      this.buffer = buffer;
      this.start = offset;
      this.end = offset + length;
    }

    public void SkipBits(int count)
    {
      this.bitBuffer >>= count;
      this.availableBits -= count;
    }

    public void SkipToByteBoundary()
    {
      this.bitBuffer >>= this.availableBits % 8;
      this.availableBits -= this.availableBits % 8;
    }

    public uint Get16Bits()
    {
      if (this.availableBits < 8)
      {
        this.Get8Bits();
        this.Get8Bits();
      }
      else if (this.availableBits < 16)
        this.Get8Bits();
      return this.bitBuffer;
    }

    private static uint GetBitMask(int count)
    {
      return (uint) ((1 << count) - 1);
    }

    private void Get8Bits()
    {
      if (this.start >= this.end)
        return;
      this.bitBuffer |= (uint) this.buffer[this.start++] << this.availableBits;
      this.availableBits += 8;
    }
  }
}
