// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaOutputWindow
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class LzmaOutputWindow
  {
    private byte[] window;
    private uint end;
    private uint windowSize;
    private uint start;
    private byte[] outputBuffer;
    private int outputOffset;

    public LzmaOutputWindow(uint blockSize)
    {
      this.windowSize = blockSize;
      this.window = new byte[(IntPtr) blockSize];
    }

    public int AvailableBytes
    {
      get
      {
        int num = (int) this.end - (int) this.start;
        if (num < 0)
          num = (int) this.end + (int) this.windowSize - (int) this.start;
        return num;
      }
    }

    public int Copied
    {
      get
      {
        return this.outputOffset;
      }
    }

    public bool Full
    {
      get
      {
        return this.outputBuffer.Length - this.outputOffset < 1;
      }
    }

    public void SetOutputBuffer(byte[] buffer)
    {
      this.outputBuffer = buffer;
      this.outputOffset = 0;
    }

    public void Flush()
    {
      int length = this.outputBuffer.Length - this.outputOffset;
      if (length <= 0)
        return;
      int availableBytes = this.AvailableBytes;
      if (availableBytes <= 0)
        return;
      if (length > availableBytes)
        length = availableBytes;
      this.Flush(length);
    }

    public void CopyBlock(uint distance, uint length)
    {
      uint num = (uint) ((int) this.end - (int) distance - 1);
      if (num >= this.windowSize)
        num += this.windowSize;
      for (; length > 0U; --length)
      {
        if (num >= this.windowSize)
          num = 0U;
        this.window[(IntPtr) this.end++] = this.window[(IntPtr) num++];
        if (this.end >= this.windowSize)
        {
          this.Flush();
          this.end = 0U;
        }
      }
    }

    public void PutByte(byte value)
    {
      this.window[(IntPtr) this.end++] = value;
      if (this.end < this.windowSize)
        return;
      this.Flush();
      this.end = 0U;
    }

    public byte GetByte(uint distance)
    {
      uint num = (uint) ((int) this.end - (int) distance - 1);
      if (num >= this.windowSize)
        num += this.windowSize;
      return this.window[(IntPtr) num];
    }

    private void Flush(int length)
    {
      if ((long) ((int) this.start + length) > (long) this.windowSize)
      {
        int length1 = (int) this.windowSize - (int) this.start;
        Array.Copy((Array) this.window, (int) this.start, (Array) this.outputBuffer, this.outputOffset, length1);
        this.outputOffset += length1;
        length -= length1;
        this.UpdatePosition(length1);
      }
      Array.Copy((Array) this.window, (int) this.start, (Array) this.outputBuffer, this.outputOffset, length);
      this.outputOffset += length;
      this.UpdatePosition(length);
    }

    private void UpdatePosition(int length)
    {
      if (this.end - this.start == 0U)
        return;
      this.start += (uint) length;
      if (this.start < this.windowSize)
        return;
      this.start -= this.windowSize;
    }
  }
}
