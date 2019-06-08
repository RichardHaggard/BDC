// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DefaultEncryptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal class DefaultEncryptor : DefaultCryptoTransformBase
  {
    public override int TransformBlock(
      byte[] inputBuffer,
      int inputOffset,
      int inputCount,
      byte[] outputBuffer,
      int outputOffset)
    {
      this.ValidateParameters(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset, true);
      for (int index = 0; index < inputCount; ++index)
      {
        byte byteValue = inputBuffer[inputOffset + index];
        outputBuffer[outputOffset + index] = (byte) ((uint) byteValue ^ (uint) this.EncodingByte);
        this.UpdateKeys(byteValue);
      }
      return inputCount;
    }

    public override byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
      this.ValidateInputBufferParameters(inputBuffer, inputOffset, inputCount, false, true);
      byte[] outputBuffer = new byte[inputCount];
      this.TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, 0);
      return outputBuffer;
    }
  }
}
