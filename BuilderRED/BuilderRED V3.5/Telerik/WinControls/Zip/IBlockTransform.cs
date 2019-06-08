// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.IBlockTransform
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  public interface IBlockTransform : IDisposable
  {
    bool CanReuseTransform { get; }

    bool CanTransformMultipleBlocks { get; }

    TransformationHeader Header { get; }

    int InputBlockSize { get; }

    int OutputBlockSize { get; }

    void CreateHeader();

    void InitHeaderReading();

    void ProcessHeader();

    int TransformBlock(
      byte[] inputBuffer,
      int inputOffset,
      int inputCount,
      byte[] outputBuffer,
      int outputOffset);

    byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
  }
}
