// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.StoreTransformBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal class StoreTransformBase : BlockTransformBase
  {
    public StoreTransformBase()
    {
      this.FixedInputBlockSize = false;
    }

    public override bool CanReuseTransform
    {
      get
      {
        return true;
      }
    }

    public override bool CanTransformMultipleBlocks
    {
      get
      {
        return true;
      }
    }

    public override int InputBlockSize
    {
      get
      {
        return 4096;
      }
    }

    public override int OutputBlockSize
    {
      get
      {
        return 4096;
      }
    }

    public override int TransformBlock(
      byte[] inputBuffer,
      int inputOffset,
      int inputCount,
      byte[] outputBuffer,
      int outputOffset)
    {
      this.ValidateParameters(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset, true);
      for (int index = 0; index < inputCount; ++index)
        outputBuffer[outputOffset + index] = inputBuffer[inputOffset + index];
      return inputCount;
    }

    public override byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
      this.ValidateInputBufferParameters(inputBuffer, inputOffset, inputCount, false, true);
      byte[] outputBuffer = new byte[inputCount];
      this.TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, 0);
      return outputBuffer;
    }

    protected override void Dispose(bool disposing)
    {
    }
  }
}
