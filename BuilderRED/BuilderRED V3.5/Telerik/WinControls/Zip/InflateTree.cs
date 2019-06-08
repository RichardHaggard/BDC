// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.InflateTree
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal class InflateTree
  {
    private byte[] codeLengthArray;
    private short[] left;
    private short[] right;
    private short[] table;
    private int tableBits;
    private int tableMask;
    private int tableBitMask;

    static InflateTree()
    {
      InflateTree.StaticLiteralLengthTree = new InflateTree(InflateTree.GetStaticLiteralTreeLength());
      InflateTree.StaticDistanceTree = new InflateTree(InflateTree.GetStaticDistanceTreeLength());
    }

    public InflateTree(byte[] codeLengths)
    {
      this.codeLengthArray = codeLengths;
      this.tableBits = this.codeLengthArray.Length != 288 ? 7 : 9;
      this.tableBitMask = 1 << this.tableBits;
      this.tableMask = this.tableBitMask - 1;
      this.CreateTable();
    }

    public static InflateTree StaticDistanceTree { get; private set; }

    public static InflateTree StaticLiteralLengthTree { get; private set; }

    public int GetNextSymbol(InputBitsBuffer input)
    {
      uint num = input.Get16Bits();
      if (input.AvailableBits == 0)
        return -1;
      int index1 = (int) this.table[(long) num & (long) this.tableMask];
      if (index1 < 0)
      {
        uint tableBitMask = (uint) this.tableBitMask;
        do
        {
          int index2 = -index1;
          index1 = ((int) num & (int) tableBitMask) != 0 ? (int) this.right[index2] : (int) this.left[index2];
          tableBitMask <<= 1;
        }
        while (index1 < 0);
      }
      int codeLength = (int) this.codeLengthArray[index1];
      if (codeLength <= 0)
        InflateTree.InvalidHuffmanData();
      if (codeLength > input.AvailableBits)
        return -1;
      input.SkipBits(codeLength);
      return index1;
    }

    private static byte[] GetStaticDistanceTreeLength()
    {
      byte[] numArray = new byte[32];
      for (int index = 0; index < 32; ++index)
        numArray[index] = (byte) 5;
      return numArray;
    }

    private static byte[] GetStaticLiteralTreeLength()
    {
      byte[] numArray = new byte[288];
      for (int index = 0; index <= 143; ++index)
        numArray[index] = (byte) 8;
      for (int index = 144; index <= (int) byte.MaxValue; ++index)
        numArray[index] = (byte) 9;
      for (int index = 256; index <= 279; ++index)
        numArray[index] = (byte) 7;
      for (int index = 280; index <= 287; ++index)
        numArray[index] = (byte) 8;
      return numArray;
    }

    private static void InvalidHuffmanData()
    {
      throw new InvalidDataException("Invalid huffman data");
    }

    private uint[] CalculateHuffmanCode()
    {
      uint[] numArray1 = new uint[17];
      foreach (int codeLength in this.codeLengthArray)
        ++numArray1[codeLength];
      numArray1[0] = 0U;
      uint num = 0;
      uint[] numArray2 = new uint[17];
      for (int index = 1; index <= 16; ++index)
      {
        num = (uint) ((int) num + (int) numArray1[index - 1] << 1);
        numArray2[index] = num;
      }
      uint[] numArray3 = new uint[288];
      for (int index = 0; index < this.codeLengthArray.Length; ++index)
      {
        int codeLength = (int) this.codeLengthArray[index];
        if (codeLength > 0)
        {
          numArray3[index] = (uint) Tree.BitReverse((int) numArray2[codeLength], codeLength);
          ++numArray2[codeLength];
        }
      }
      return numArray3;
    }

    private void CreateTable()
    {
      short length1 = (short) this.codeLengthArray.Length;
      uint[] huffmanCode = this.CalculateHuffmanCode();
      int length2 = this.codeLengthArray.Length * 2;
      this.table = new short[this.tableBitMask];
      this.left = new short[length2];
      this.right = new short[length2];
      for (int index = 0; index < this.codeLengthArray.Length; ++index)
      {
        int codeLength = (int) this.codeLengthArray[index];
        if (codeLength > 0)
        {
          int start = (int) huffmanCode[index];
          short code = (short) index;
          if (codeLength > this.tableBits)
            this.BuildTree(code, start, codeLength, ref length1);
          else
            this.DuplicateCode(code, start, codeLength);
        }
      }
    }

    private void DuplicateCode(short code, int start, int length)
    {
      int num1 = 1 << length;
      if (start >= num1)
        InflateTree.InvalidHuffmanData();
      int num2 = 1 << this.tableBits - length;
      for (int index = 0; index < num2; ++index)
      {
        this.table[start] = code;
        start += num1;
      }
    }

    private void BuildTree(short code, int start, int length, ref short avail)
    {
      int num1 = length - this.tableBits;
      int tableBitMask = this.tableBitMask;
      int index = start & this.tableMask;
      short[] numArray = this.table;
      do
      {
        short num2 = numArray[index];
        if (num2 == (short) 0)
        {
          num2 = -avail;
          numArray[index] = num2;
          ++avail;
        }
        if (num2 > (short) 0)
          InflateTree.InvalidHuffmanData();
        numArray = (start & tableBitMask) == 0 ? this.left : this.right;
        tableBitMask <<= 1;
        index = (int) -num2;
        --num1;
      }
      while (num1 != 0);
      numArray[index] = code;
    }
  }
}
