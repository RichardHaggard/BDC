// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DefaultCryptoTransformBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Text;

namespace Telerik.WinControls.Zip
{
  internal abstract class DefaultCryptoTransformBase : BlockTransformBase
  {
    private uint[] encryptionKeys = new uint[3]{ 305419896U, 591751049U, 878082192U };
    protected const int HeaderSize = 12;

    public DefaultCryptoTransformBase()
    {
      this.FixedInputBlockSize = false;
      this.Header.Length = 12;
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
        return 12;
      }
    }

    public override int OutputBlockSize
    {
      get
      {
        return 12;
      }
    }

    protected byte EncodingByte
    {
      get
      {
        uint num = (uint) (ushort) ((int) this.encryptionKeys[2] & (int) ushort.MaxValue | 2);
        return (byte) (num * (num ^ 1U) >> 8);
      }
    }

    public override void CreateHeader()
    {
      Random random = new Random((int) DateTime.Now.Ticks);
      byte[] numArray = new byte[12];
      random.NextBytes(numArray);
      if (this.Header.InitData != null)
        numArray[11] = this.Header.InitData[0];
      byte[] outputBuffer = new byte[12];
      this.TransformBlock(numArray, 0, 12, outputBuffer, 0);
      this.Header.Buffer = outputBuffer;
    }

    public override void InitHeaderReading()
    {
      this.Header.BytesToRead = 12;
    }

    public override void ProcessHeader()
    {
      this.TransformBlock(this.Header.Buffer, 0, this.Header.Buffer.Length, new byte[12], 0);
      this.Header.BytesToRead = 0;
    }

    internal void InitKeys(string password)
    {
      foreach (byte byteValue in Encoding.UTF8.GetBytes(password))
        this.UpdateKeys(byteValue);
    }

    protected override void Dispose(bool disposing)
    {
    }

    protected void UpdateKeys(byte byteValue)
    {
      this.encryptionKeys[0] = Crc32.ComputeCrc32(this.encryptionKeys[0], byteValue);
      this.encryptionKeys[1] = this.encryptionKeys[1] + (uint) (byte) this.encryptionKeys[0];
      this.encryptionKeys[1] = (uint) ((int) this.encryptionKeys[1] * 134775813 + 1);
      this.encryptionKeys[2] = Crc32.ComputeCrc32(this.encryptionKeys[2], (byte) (this.encryptionKeys[1] >> 24));
    }
  }
}
