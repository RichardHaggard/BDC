// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.OperationStream
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;

namespace Telerik.WinControls.Zip
{
  public abstract class OperationStream : Stream
  {
    private Stream baseStream;
    private StreamOperationMode streamMode;
    private IBlockTransform blockTransform;
    private byte[] inputBuffer;
    private int inputBufferIndex;
    private byte[] outputBuffer;
    private int outputBufferIndex;
    private bool finalBlockTransformed;
    private long totalPlainCount;
    private long totalTransformedCount;
    private long? length;
    private long startPosition;

    internal OperationStream(Stream baseStream, StreamOperationMode mode)
    {
      if (!baseStream.CanRead && mode == StreamOperationMode.Read || !baseStream.CanWrite && mode == StreamOperationMode.Write)
        throw new ArgumentOutOfRangeException(nameof (mode));
      this.BaseStream = baseStream;
      this.streamMode = mode;
      this.IsDisposed = false;
    }

    ~OperationStream()
    {
      this.Dispose(false);
    }

    public override bool CanRead
    {
      get
      {
        if (this.baseStream != null)
          return this.streamMode == StreamOperationMode.Read;
        return false;
      }
    }

    public override bool CanSeek
    {
      get
      {
        return false;
      }
    }

    public override bool CanWrite
    {
      get
      {
        if (this.baseStream != null)
          return this.streamMode == StreamOperationMode.Write;
        return false;
      }
    }

    public bool HasFlushedFinalBlock
    {
      get
      {
        return this.finalBlockTransformed;
      }
    }

    public override long Length
    {
      get
      {
        if (this.BaseStream == null)
          throw new NotSupportedException();
        if (this.BaseStream.CanSeek)
        {
          if (this.Mode == StreamOperationMode.Read)
            return this.length.Value;
          return this.BaseStream.Length;
        }
        if (this.length.HasValue)
          return this.length.Value;
        return long.MaxValue;
      }
    }

    public override long Position
    {
      get
      {
        if (this.BaseStream == null)
          throw new NotSupportedException();
        if (!this.BaseStream.CanSeek)
          return 0;
        if (this.Mode == StreamOperationMode.Read)
          return this.BaseStream.Position - this.startPosition;
        return this.BaseStream.Position;
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    public long TotalPlainCount
    {
      get
      {
        return this.totalPlainCount;
      }
      private set
      {
        this.totalPlainCount = value;
      }
    }

    public long TotalTransformedCount
    {
      get
      {
        return this.totalTransformedCount;
      }
      private set
      {
        this.totalTransformedCount = value;
      }
    }

    internal Stream BaseStream
    {
      get
      {
        return this.baseStream;
      }
      set
      {
        this.baseStream = value;
        if (this.baseStream == null || this.Mode != StreamOperationMode.Read || !this.baseStream.CanSeek)
          return;
        this.startPosition = this.baseStream.Position;
        try
        {
          this.length = new long?(this.baseStream.Length);
        }
        catch
        {
          this.length = new long?(0L);
        }
      }
    }

    internal StreamOperationMode Mode
    {
      get
      {
        return this.streamMode;
      }
    }

    internal bool IsDisposed { get; set; }

    internal IBlockTransform Transform
    {
      get
      {
        return this.blockTransform;
      }
      set
      {
        this.blockTransform = value;
        this.InitializeTransform();
        this.InitializeBuffers();
      }
    }

    public override void Flush()
    {
      this.FlushFinalBlock();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      this.EnsureNotDisposed();
      OperationStream.ValidateBufferParameters(buffer, offset, count, true);
      if (!this.CanRead)
        throw new NotSupportedException();
      int num1 = count;
      int dstOffset = offset;
      if (this.outputBufferIndex != 0)
      {
        if (this.outputBufferIndex > count)
        {
          Buffer.BlockCopy((Array) this.outputBuffer, 0, (Array) buffer, offset, count);
          Buffer.BlockCopy((Array) this.outputBuffer, count, (Array) this.outputBuffer, 0, this.outputBufferIndex - count);
          this.outputBufferIndex -= count;
          return count;
        }
        Buffer.BlockCopy((Array) this.outputBuffer, 0, (Array) buffer, offset, this.outputBufferIndex);
        num1 -= this.outputBufferIndex;
        dstOffset += this.outputBufferIndex;
        this.outputBufferIndex = 0;
      }
      if (this.finalBlockTransformed)
        return count - num1;
      if (num1 > this.Transform.OutputBlockSize && this.Transform.CanTransformMultipleBlocks)
      {
        int length = (int) Math.Min((long) (num1 / this.Transform.OutputBlockSize * this.Transform.InputBlockSize), this.Length - this.Position);
        byte[] numArray = new byte[length];
        Buffer.BlockCopy((Array) this.inputBuffer, 0, (Array) numArray, 0, this.inputBufferIndex);
        int num2 = this.inputBufferIndex + this.baseStream.Read(numArray, this.inputBufferIndex, length - this.inputBufferIndex);
        this.TotalTransformedCount += (long) (num2 - this.inputBufferIndex);
        this.inputBufferIndex = 0;
        if (num2 > this.Transform.InputBlockSize)
        {
          int num3 = num2 / this.Transform.InputBlockSize * this.Transform.InputBlockSize;
          int count1 = num2 - num3;
          if (count1 != 0)
          {
            this.inputBufferIndex = count1;
            Buffer.BlockCopy((Array) numArray, num3, (Array) this.inputBuffer, 0, count1);
          }
          byte[] outputBuffer = new byte[num3 / this.Transform.InputBlockSize * this.Transform.OutputBlockSize];
          int count2 = this.Transform.TransformBlock(numArray, 0, num3, outputBuffer, 0);
          this.TotalPlainCount += (long) count2;
          Buffer.BlockCopy((Array) outputBuffer, 0, (Array) buffer, dstOffset, count2);
          Array.Clear((Array) numArray, 0, numArray.Length);
          Array.Clear((Array) outputBuffer, 0, outputBuffer.Length);
          num1 -= count2;
          dstOffset += count2;
        }
        else
        {
          this.inputBuffer = numArray;
          this.inputBufferIndex = num2;
        }
      }
      int count3;
      for (; num1 > 0; num1 -= count3)
      {
        int num2;
        for (; this.inputBufferIndex < this.Transform.InputBlockSize; this.inputBufferIndex += num2)
        {
          num2 = this.baseStream.Read(this.inputBuffer, this.inputBufferIndex, (int) Math.Min((long) (this.Transform.InputBlockSize - this.inputBufferIndex), this.Length - this.Position));
          this.TotalTransformedCount += (long) num2;
          if (num2 == 0)
          {
            byte[] numArray = this.Transform.TransformFinalBlock(this.inputBuffer, 0, this.inputBufferIndex);
            this.TotalPlainCount += (long) numArray.Length;
            this.outputBuffer = numArray;
            this.outputBufferIndex = numArray.Length;
            this.finalBlockTransformed = true;
            if (num1 >= this.outputBufferIndex)
            {
              Buffer.BlockCopy((Array) this.outputBuffer, 0, (Array) buffer, dstOffset, this.outputBufferIndex);
              int num3 = num1 - this.outputBufferIndex;
              this.outputBufferIndex = 0;
              return count - num3;
            }
            Buffer.BlockCopy((Array) this.outputBuffer, 0, (Array) buffer, dstOffset, num1);
            this.outputBufferIndex -= num1;
            Buffer.BlockCopy((Array) this.outputBuffer, num1, (Array) this.outputBuffer, 0, this.outputBufferIndex);
            return count;
          }
        }
        count3 = this.Transform.TransformBlock(this.inputBuffer, 0, this.Transform.InputBlockSize, this.outputBuffer, 0);
        this.TotalPlainCount += (long) count3;
        this.inputBufferIndex = 0;
        if (num1 < count3)
        {
          Buffer.BlockCopy((Array) this.outputBuffer, 0, (Array) buffer, dstOffset, num1);
          this.outputBufferIndex = count3 - num1;
          Buffer.BlockCopy((Array) this.outputBuffer, num1, (Array) this.outputBuffer, 0, this.outputBufferIndex);
          return count;
        }
        Buffer.BlockCopy((Array) this.outputBuffer, 0, (Array) buffer, dstOffset, count3);
        dstOffset += count3;
      }
      return count;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
      if (this.Mode != StreamOperationMode.Read)
        throw new NotSupportedException();
      this.length = new long?(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      this.EnsureNotDisposed();
      OperationStream.ValidateBufferParameters(buffer, offset, count, true);
      if (!this.CanWrite)
        throw new NotSupportedException();
      this.TotalPlainCount += (long) count;
      int count1 = count;
      int num1 = offset;
      if (this.inputBufferIndex > 0)
      {
        if (count < this.Transform.InputBlockSize - this.inputBufferIndex)
        {
          Buffer.BlockCopy((Array) buffer, offset, (Array) this.inputBuffer, this.inputBufferIndex, count);
          this.inputBufferIndex += count;
          return;
        }
        Buffer.BlockCopy((Array) buffer, offset, (Array) this.inputBuffer, this.inputBufferIndex, this.Transform.InputBlockSize - this.inputBufferIndex);
        num1 += this.Transform.InputBlockSize - this.inputBufferIndex;
        count1 -= this.Transform.InputBlockSize - this.inputBufferIndex;
        this.inputBufferIndex = this.Transform.InputBlockSize;
      }
      if (this.outputBufferIndex > 0)
      {
        this.baseStream.Write(this.outputBuffer, 0, this.outputBufferIndex);
        this.outputBufferIndex = 0;
      }
      if (this.inputBufferIndex == this.Transform.InputBlockSize)
      {
        int count2 = this.Transform.TransformBlock(this.inputBuffer, 0, this.Transform.InputBlockSize, this.outputBuffer, 0);
        this.TotalTransformedCount += (long) count2;
        this.baseStream.Write(this.outputBuffer, 0, count2);
        this.inputBufferIndex = 0;
      }
      while (count1 > 0)
      {
        if (count1 < this.Transform.InputBlockSize)
        {
          Buffer.BlockCopy((Array) buffer, num1, (Array) this.inputBuffer, 0, count1);
          this.inputBufferIndex += count1;
          break;
        }
        if (!this.Transform.CanTransformMultipleBlocks)
        {
          int count2 = this.Transform.TransformBlock(buffer, num1, this.Transform.InputBlockSize, this.outputBuffer, 0);
          this.TotalTransformedCount += (long) count2;
          this.baseStream.Write(this.outputBuffer, 0, count2);
          num1 += this.Transform.InputBlockSize;
          count1 -= this.Transform.InputBlockSize;
        }
        else
        {
          int num2 = count1 / this.Transform.InputBlockSize;
          int inputCount = num2 * this.Transform.InputBlockSize;
          byte[] numArray = new byte[num2 * this.Transform.OutputBlockSize];
          int count2 = this.Transform.TransformBlock(buffer, num1, inputCount, numArray, 0);
          this.TotalTransformedCount += (long) count2;
          this.baseStream.Write(numArray, 0, count2);
          num1 += inputCount;
          count1 -= inputCount;
        }
      }
    }

    internal static void ValidateBufferParameters(
      byte[] buffer,
      int offset,
      int count,
      bool allowZeroCount)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (count < 0 || count == 0 && !allowZeroCount)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (buffer.Length - offset < count)
        throw new ArgumentException("InvalidArgumentOffsetCount");
    }

    internal virtual void FlushFinalBlock()
    {
      if (this.finalBlockTransformed)
        throw new NotSupportedException("Can't flush final block twice");
      byte[] buffer = new byte[0];
      if (this.inputBuffer != null)
        buffer = this.Transform.TransformFinalBlock(this.inputBuffer, 0, this.inputBufferIndex);
      this.finalBlockTransformed = true;
      this.TotalTransformedCount += (long) buffer.Length;
      if (this.baseStream != null && this.CanWrite)
      {
        if (this.outputBufferIndex > 0)
        {
          this.baseStream.Write(this.outputBuffer, 0, this.outputBufferIndex);
          this.outputBufferIndex = 0;
        }
        this.baseStream.Write(buffer, 0, buffer.Length);
      }
      OperationStream baseStream = this.baseStream as OperationStream;
      if (baseStream == null)
      {
        try
        {
          if (this.baseStream.CanRead)
            this.baseStream.Flush();
        }
        catch (NotSupportedException ex)
        {
        }
      }
      else if (!baseStream.HasFlushedFinalBlock && baseStream.CanWrite)
        baseStream.FlushFinalBlock();
      if (this.inputBuffer != null)
        Array.Clear((Array) this.inputBuffer, 0, this.inputBuffer.Length);
      if (this.outputBuffer == null)
        return;
      Array.Clear((Array) this.outputBuffer, 0, this.outputBuffer.Length);
    }

    protected void EnsureNotDisposed()
    {
      if (this.baseStream == null)
        throw new ObjectDisposedException((string) null, "Stream closed");
    }

    protected override void Dispose(bool disposing)
    {
      if (this.IsDisposed)
        return;
      try
      {
        if (!disposing)
          return;
        if (!this.finalBlockTransformed && this.Transform != null)
          this.FlushFinalBlock();
        (this.baseStream as OperationStream)?.Dispose();
      }
      finally
      {
        try
        {
          this.finalBlockTransformed = true;
          if (this.Transform != null)
            this.Transform.Dispose();
          if (this.inputBuffer != null)
            Array.Clear((Array) this.inputBuffer, 0, this.inputBuffer.Length);
          if (this.outputBuffer != null)
            Array.Clear((Array) this.outputBuffer, 0, this.outputBuffer.Length);
          this.inputBuffer = (byte[]) null;
          this.outputBuffer = (byte[]) null;
          this.blockTransform = (IBlockTransform) null;
          this.baseStream = (Stream) null;
        }
        finally
        {
          this.IsDisposed = true;
          base.Dispose(disposing);
        }
      }
    }

    private void InitializeBuffers()
    {
      if (this.blockTransform == null)
        return;
      this.inputBuffer = new byte[this.blockTransform.InputBlockSize];
      this.outputBuffer = new byte[this.blockTransform.OutputBlockSize];
    }

    private void InitializeTransform()
    {
      switch (this.Mode)
      {
        case StreamOperationMode.Read:
          this.ReadTransformationHeader();
          break;
        case StreamOperationMode.Write:
          this.WriteTransformationHeader();
          break;
      }
    }

    private void ReadTransformationHeader()
    {
      this.Transform.InitHeaderReading();
      while (this.Transform.Header.BytesToRead > 0)
      {
        byte[] buffer = new byte[this.Transform.Header.Buffer != null ? this.Transform.Header.Buffer.Length + this.Transform.Header.BytesToRead : this.Transform.Header.BytesToRead];
        int offset = 0;
        if (this.Transform.Header.Buffer != null)
        {
          Array.Copy((Array) this.Transform.Header.Buffer, (Array) buffer, this.Transform.Header.Buffer.Length);
          offset = this.Transform.Header.Buffer.Length;
        }
        this.BaseStream.Read(buffer, offset, this.Transform.Header.BytesToRead);
        this.Transform.Header.Buffer = buffer;
        this.Transform.ProcessHeader();
      }
    }

    private void WriteTransformationHeader()
    {
      this.Transform.CreateHeader();
      if (this.Transform.Header.Buffer == null)
        return;
      this.BaseStream.Write(this.Transform.Header.Buffer, 0, this.Transform.Header.Buffer.Length);
    }
  }
}
