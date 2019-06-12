// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.CompressionTransformBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.IO;

namespace Telerik.WinControls.Zip
{
  internal abstract class CompressionTransformBase : BlockTransformBase
  {
    public CompressionTransformBase()
    {
      this.FixedInputBlockSize = false;
    }

    public override bool CanReuseTransform
    {
      get
      {
        return false;
      }
    }

    public override bool CanTransformMultipleBlocks
    {
      get
      {
        return false;
      }
    }

    public override int InputBlockSize
    {
      get
      {
        return 8192;
      }
    }

    public override int OutputBlockSize
    {
      get
      {
        return 32768;
      }
    }

    protected int AvailableBytesIn { get; set; }

    protected byte[] InputBuffer { get; set; }

    protected int NextIn { get; set; }

    protected int TotalBytesIn { get; set; }

    protected int AvailableBytesOut { get; set; }

    protected byte[] OutputBuffer { get; set; }

    protected int NextOut { get; set; }

    protected int TotalBytesOut { get; set; }

    public override int TransformBlock(
      byte[] inputBuffer,
      int inputOffset,
      int inputCount,
      byte[] outputBuffer,
      int outputOffset)
    {
      this.ValidateParameters(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset, true);
      this.AvailableBytesIn = inputCount;
      this.InputBuffer = inputBuffer;
      this.AvailableBytesOut = outputBuffer.Length - outputOffset;
      this.OutputBuffer = outputBuffer;
      this.NextOut = outputOffset;
      this.NextIn = inputOffset;
      this.ProcessTransform(false);
      return this.NextOut - outputOffset;
    }

    public override byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
      this.ValidateInputBufferParameters(inputBuffer, inputOffset, inputCount, false, true);
      this.AvailableBytesIn = inputCount;
      this.InputBuffer = inputBuffer;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        bool flag;
        do
        {
          byte[] buffer = new byte[this.OutputBlockSize];
          this.AvailableBytesOut = buffer.Length;
          this.OutputBuffer = buffer;
          this.NextOut = 0;
          this.NextIn = 0;
          flag = this.ProcessTransform(true);
          this.AvailableBytesIn = 0;
          memoryStream.Write(buffer, 0, this.NextOut);
        }
        while (flag);
        return memoryStream.ToArray();
      }
    }

    protected override void Dispose(bool disposing)
    {
    }

    protected abstract bool ProcessTransform(bool finalBlock);
  }
}
