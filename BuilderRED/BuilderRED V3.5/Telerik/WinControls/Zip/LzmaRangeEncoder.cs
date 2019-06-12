// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaRangeEncoder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;

namespace Telerik.WinControls.Zip
{
  internal class LzmaRangeEncoder : IDisposable
  {
    public const uint TopValue = 16777216;
    private byte cache;
    private uint cacheSize;
    private byte[] outputBuffer;
    private int nextOut;
    private MemoryStream pendingData;

    public LzmaRangeEncoder()
    {
      this.pendingData = new MemoryStream();
      this.Low = 0UL;
      this.Range = uint.MaxValue;
      this.cacheSize = 1U;
      this.cache = (byte) 0;
    }

    public ulong Low { get; set; }

    public uint Range { get; set; }

    public bool HasData
    {
      get
      {
        return this.pendingData.Length > 0L;
      }
    }

    public int NextOut
    {
      get
      {
        return this.nextOut;
      }
    }

    private bool CanWrite
    {
      get
      {
        return this.NextOut < this.outputBuffer.Length;
      }
    }

    public void SetOutputBuffer(byte[] outputBuffer, int index)
    {
      this.outputBuffer = outputBuffer;
      this.nextOut = index;
      this.Flush();
    }

    public void FlushData()
    {
      for (int index = 0; index < 5; ++index)
        this.ShiftLow();
    }

    public void Dispose()
    {
      if (this.pendingData == null)
        return;
      this.pendingData.Dispose();
    }

    public void ShiftLow()
    {
      if ((uint) this.Low < 4278190080U || (uint) (this.Low >> 32) == 1U)
      {
        byte num1 = this.cache;
        do
        {
          byte num2 = (byte) ((ulong) num1 + (this.Low >> 32));
          if (this.CanWrite)
            this.outputBuffer[this.nextOut++] = num2;
          else
            this.pendingData.WriteByte(num2);
          num1 = byte.MaxValue;
        }
        while (--this.cacheSize != 0U);
        this.cache = (byte) ((uint) this.Low >> 24);
      }
      ++this.cacheSize;
      this.Low = (ulong) ((uint) this.Low << 8);
    }

    public void EncodeDirectBits(uint value, int totalBits)
    {
      for (int index = totalBits - 1; index >= 0; --index)
      {
        this.Range >>= 1;
        if (((int) (value >> index) & 1) == 1)
          this.Low += (ulong) this.Range;
        if (this.Range < 16777216U)
        {
          this.Range <<= 8;
          this.ShiftLow();
        }
      }
    }

    private void Flush()
    {
      if (this.pendingData.Length <= 0L)
        return;
      if (this.pendingData.Position == this.pendingData.Length)
        this.pendingData.Position = 0L;
      this.nextOut += this.pendingData.Read(this.outputBuffer, this.NextOut, this.outputBuffer.Length - this.NextOut);
      if (this.pendingData.Position != this.pendingData.Length)
        return;
      this.pendingData.SetLength(0L);
    }
  }
}
