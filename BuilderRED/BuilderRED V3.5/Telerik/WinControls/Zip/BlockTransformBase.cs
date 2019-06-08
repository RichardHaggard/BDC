// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.BlockTransformBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  public abstract class BlockTransformBase : IBlockTransform, IDisposable
  {
    private TransformationHeader header = new TransformationHeader();

    public abstract bool CanReuseTransform { get; }

    public abstract bool CanTransformMultipleBlocks { get; }

    public TransformationHeader Header
    {
      get
      {
        return this.header;
      }
    }

    public abstract int InputBlockSize { get; }

    public abstract int OutputBlockSize { get; }

    protected bool FixedInputBlockSize { get; set; }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public virtual void CreateHeader()
    {
    }

    public virtual void InitHeaderReading()
    {
    }

    public virtual void ProcessHeader()
    {
      this.Header.BytesToRead = 0;
    }

    public abstract int TransformBlock(
      byte[] inputBuffer,
      int inputOffset,
      int inputCount,
      byte[] outputBuffer,
      int outputOffset);

    public abstract byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);

    protected abstract void Dispose(bool disposing);

    protected void ValidateInputBufferParameters(
      byte[] inputBuffer,
      int inputOffset,
      int inputCount,
      bool validateBlockSize,
      bool allowZeroCount)
    {
      OperationStream.ValidateBufferParameters(inputBuffer, inputOffset, inputCount, allowZeroCount);
      if (this.FixedInputBlockSize && validateBlockSize && inputCount % this.InputBlockSize != 0)
        throw new ArgumentException("Invalid value.", nameof (inputCount));
    }

    protected void ValidateParameters(
      byte[] inputBuffer,
      int inputOffset,
      int inputCount,
      byte[] outputBuffer,
      int outputOffset,
      bool allowZeroCount)
    {
      this.ValidateInputBufferParameters(inputBuffer, inputOffset, inputCount, true, allowZeroCount);
      OperationStream.ValidateBufferParameters(outputBuffer, outputOffset, 0, true);
    }
  }
}
