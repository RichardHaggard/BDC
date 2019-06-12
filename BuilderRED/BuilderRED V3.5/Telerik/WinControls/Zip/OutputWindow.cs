// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.OutputWindow
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal class OutputWindow
  {
    private byte[] window = new byte[32768];
    private const int WindowMask = 32767;
    private const int WindowSize = 32768;
    private int end;
    private int availableBytes;

    public int AvailableBytes
    {
      get
      {
        return this.availableBytes;
      }
    }

    public int FreeBytes
    {
      get
      {
        return 32768 - this.availableBytes;
      }
    }

    public void AddByte(byte value)
    {
      this.window[this.end++] = value;
      this.end &= (int) short.MaxValue;
      ++this.availableBytes;
    }

    public void Copy(int length, int distance)
    {
      this.availableBytes += length;
      int sourceIndex = this.end - distance & (int) short.MaxValue;
      int num1 = 32768 - length;
      if (sourceIndex > num1 || this.end >= num1)
      {
        while (length-- > 0)
        {
          byte[] window1 = this.window;
          int index1 = this.end++;
          byte[] window2 = this.window;
          int index2 = sourceIndex;
          int num2 = index2 + 1;
          int num3 = (int) window2[index2];
          window1[index1] = (byte) num3;
          this.end &= (int) short.MaxValue;
          sourceIndex = num2 & (int) short.MaxValue;
        }
      }
      else if (length > distance)
      {
        while (length-- > 0)
          this.window[this.end++] = this.window[sourceIndex++];
      }
      else
      {
        Array.Copy((Array) this.window, sourceIndex, (Array) this.window, this.end, length);
        this.end += length;
      }
    }

    public int Read(byte[] output, int offset, int length)
    {
      int num1;
      if (length <= this.availableBytes)
      {
        num1 = this.end - this.availableBytes + length & (int) short.MaxValue;
      }
      else
      {
        num1 = this.end;
        length = this.availableBytes;
      }
      int num2 = length;
      int length1 = length - num1;
      if (length1 > 0)
      {
        Array.Copy((Array) this.window, 32768 - length1, (Array) output, offset, length1);
        offset += length1;
        length = num1;
      }
      Array.Copy((Array) this.window, num1 - length, (Array) output, offset, length);
      this.availableBytes -= num2;
      return num2;
    }

    public int ReadInput(InputBitsBuffer input, int length)
    {
      int length1 = 32768 - this.end;
      length = Math.Min(Math.Min(length, this.FreeBytes), input.AvailableBytes);
      int num;
      if (length <= length1)
      {
        num = input.Read(this.window, this.end, length);
      }
      else
      {
        num = input.Read(this.window, this.end, length1);
        if (num == length1)
          num += input.Read(this.window, 0, length - length1);
      }
      this.end = this.end + num & (int) short.MaxValue;
      this.availableBytes += num;
      return num;
    }
  }
}
