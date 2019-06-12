// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.Tree
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  internal sealed class Tree
  {
    internal static readonly int[] ExtraLengthBits = new int[29]{ 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0 };
    internal static readonly int[] ExtraDistanceBits = new int[30]{ 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13 };
    internal static readonly int[] ExtraBits = new int[19]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 7 };
    internal static readonly sbyte[] BitLengthOrder = new sbyte[19]{ (sbyte) 16, (sbyte) 17, (sbyte) 18, (sbyte) 0, (sbyte) 8, (sbyte) 7, (sbyte) 9, (sbyte) 6, (sbyte) 10, (sbyte) 5, (sbyte) 11, (sbyte) 4, (sbyte) 12, (sbyte) 3, (sbyte) 13, (sbyte) 2, (sbyte) 14, (sbyte) 1, (sbyte) 15 };
    internal static readonly sbyte[] LengthCode = new sbyte[256]{ (sbyte) 0, (sbyte) 1, (sbyte) 2, (sbyte) 3, (sbyte) 4, (sbyte) 5, (sbyte) 6, (sbyte) 7, (sbyte) 8, (sbyte) 8, (sbyte) 9, (sbyte) 9, (sbyte) 10, (sbyte) 10, (sbyte) 11, (sbyte) 11, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 16, (sbyte) 16, (sbyte) 16, (sbyte) 16, (sbyte) 16, (sbyte) 16, (sbyte) 16, (sbyte) 16, (sbyte) 17, (sbyte) 17, (sbyte) 17, (sbyte) 17, (sbyte) 17, (sbyte) 17, (sbyte) 17, (sbyte) 17, (sbyte) 18, (sbyte) 18, (sbyte) 18, (sbyte) 18, (sbyte) 18, (sbyte) 18, (sbyte) 18, (sbyte) 18, (sbyte) 19, (sbyte) 19, (sbyte) 19, (sbyte) 19, (sbyte) 19, (sbyte) 19, (sbyte) 19, (sbyte) 19, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 28 };
    internal static readonly int[] LengthBase = new int[29]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 12, 14, 16, 20, 24, 28, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 0 };
    internal static readonly int[] DistanceBase = new int[30]{ 0, 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 64, 96, 128, 192, 256, 384, 512, 768, 1024, 1536, 2048, 3072, 4096, 6144, 8192, 12288, 16384, 24576 };
    private static readonly int HeapSize = 573;
    private static readonly sbyte[] DistanceCode = new sbyte[512]{ (sbyte) 0, (sbyte) 1, (sbyte) 2, (sbyte) 3, (sbyte) 4, (sbyte) 4, (sbyte) 5, (sbyte) 5, (sbyte) 6, (sbyte) 6, (sbyte) 6, (sbyte) 6, (sbyte) 7, (sbyte) 7, (sbyte) 7, (sbyte) 7, (sbyte) 8, (sbyte) 8, (sbyte) 8, (sbyte) 8, (sbyte) 8, (sbyte) 8, (sbyte) 8, (sbyte) 8, (sbyte) 9, (sbyte) 9, (sbyte) 9, (sbyte) 9, (sbyte) 9, (sbyte) 9, (sbyte) 9, (sbyte) 9, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 10, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 11, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 12, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 13, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 14, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 15, (sbyte) 0, (sbyte) 0, (sbyte) 16, (sbyte) 17, (sbyte) 18, (sbyte) 18, (sbyte) 19, (sbyte) 19, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 20, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 21, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 22, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 23, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 24, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 25, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 26, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 27, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 28, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29, (sbyte) 29 };

    internal short[] DynamicTree { get; set; }

    internal int MaxCode { get; set; }

    internal StaticTree StaticTree { get; set; }

    internal static int BitReverse(int code, int length)
    {
      int num1 = 0;
      do
      {
        int num2 = num1 | code & 1;
        code >>= 1;
        num1 = num2 << 1;
      }
      while (--length > 0);
      return num1 >> 1;
    }

    internal static int GetDistanceCode(int distance)
    {
      if (distance >= 256)
        return (int) Tree.DistanceCode[256 + (int) ((uint) distance >> 7)];
      return (int) Tree.DistanceCode[distance];
    }

    internal void BuildTree(DeflateCompressor manager)
    {
      short[] dynamicTree = this.DynamicTree;
      short[] treeCodes = this.StaticTree.TreeCodes;
      int elements = this.StaticTree.Elements;
      int maxCode = -1;
      manager.HeapLength = 0;
      manager.HeapMax = Tree.HeapSize;
      for (int index = 0; index < elements; ++index)
      {
        if (dynamicTree[index * 2] != (short) 0)
        {
          manager.Heap[++manager.HeapLength] = maxCode = index;
          manager.Depth[index] = (sbyte) 0;
        }
        else
          dynamicTree[index * 2 + 1] = (short) 0;
      }
      while (manager.HeapLength < 2)
      {
        int[] heap = manager.Heap;
        int index1 = ++manager.HeapLength;
        int num1;
        if (maxCode >= 2)
          num1 = 0;
        else
          maxCode = num1 = maxCode + 1;
        int num2 = num1;
        heap[index1] = num1;
        int index2 = num2;
        dynamicTree[index2 * 2] = (short) 1;
        manager.Depth[index2] = (sbyte) 0;
        --manager.OptimalLength;
        if (treeCodes != null)
          manager.StaticLength -= (int) treeCodes[index2 * 2 + 1];
      }
      this.MaxCode = maxCode;
      for (int nodeIndex = manager.HeapLength / 2; nodeIndex >= 1; --nodeIndex)
        manager.DownHeap(dynamicTree, nodeIndex);
      int index3 = elements;
      do
      {
        int index1 = manager.Heap[1];
        manager.Heap[1] = manager.Heap[manager.HeapLength--];
        manager.DownHeap(dynamicTree, 1);
        int index2 = manager.Heap[1];
        manager.Heap[--manager.HeapMax] = index1;
        manager.Heap[--manager.HeapMax] = index2;
        dynamicTree[index3 * 2] = (short) ((int) dynamicTree[index1 * 2] + (int) dynamicTree[index2 * 2]);
        manager.Depth[index3] = (sbyte) ((int) Math.Max((byte) manager.Depth[index1], (byte) manager.Depth[index2]) + 1);
        dynamicTree[index1 * 2 + 1] = dynamicTree[index2 * 2 + 1] = (short) index3;
        manager.Heap[1] = index3++;
        manager.DownHeap(dynamicTree, 1);
      }
      while (manager.HeapLength >= 2);
      manager.Heap[--manager.HeapMax] = manager.Heap[1];
      this.GenerateBitLengths(manager);
      Tree.GenerateCodes(dynamicTree, maxCode, manager.BitLengthCount);
    }

    private static void GenerateCodes(short[] tree, int maxCode, short[] bitLengthCount)
    {
      short[] numArray = new short[16];
      short num = 0;
      for (int index = 1; index <= 15; ++index)
        numArray[index] = num = (short) ((int) num + (int) bitLengthCount[index - 1] << 1);
      for (int index = 0; index <= maxCode; ++index)
      {
        int length = (int) tree[index * 2 + 1];
        if (length != 0)
          tree[index * 2] = (short) Tree.BitReverse((int) numArray[length]++, length);
      }
    }

    private void GenerateBitLengths(DeflateCompressor manager)
    {
      short[] dynamicTree = this.DynamicTree;
      short[] treeCodes = this.StaticTree.TreeCodes;
      int[] extraBits = this.StaticTree.ExtraBits;
      int extraBase = this.StaticTree.ExtraBase;
      int maxLength = this.StaticTree.MaxLength;
      int num1 = 0;
      for (int index = 0; index <= 15; ++index)
        manager.BitLengthCount[index] = (short) 0;
      dynamicTree[manager.Heap[manager.HeapMax] * 2 + 1] = (short) 0;
      int index1;
      for (index1 = manager.HeapMax + 1; index1 < Tree.HeapSize; ++index1)
      {
        int num2 = manager.Heap[index1];
        int index2 = (int) dynamicTree[(int) dynamicTree[num2 * 2 + 1] * 2 + 1] + 1;
        if (index2 > maxLength)
        {
          index2 = maxLength;
          ++num1;
        }
        dynamicTree[num2 * 2 + 1] = (short) index2;
        if (num2 <= this.MaxCode)
        {
          ++manager.BitLengthCount[index2];
          int num3 = 0;
          if (num2 >= extraBase)
            num3 = extraBits[num2 - extraBase];
          short num4 = dynamicTree[num2 * 2];
          manager.OptimalLength += (int) num4 * (index2 + num3);
          if (treeCodes != null)
            manager.StaticLength += (int) num4 * ((int) treeCodes[num2 * 2 + 1] + num3);
        }
      }
      if (num1 == 0)
        return;
      do
      {
        int index2 = maxLength - 1;
        while (manager.BitLengthCount[index2] == (short) 0)
          --index2;
        --manager.BitLengthCount[index2];
        manager.BitLengthCount[index2 + 1] = (short) ((int) manager.BitLengthCount[index2 + 1] + 2);
        --manager.BitLengthCount[maxLength];
        num1 -= 2;
      }
      while (num1 > 0);
      for (int index2 = maxLength; index2 != 0; --index2)
      {
        int num2 = (int) manager.BitLengthCount[index2];
        while (num2 != 0)
        {
          int num3 = manager.Heap[--index1];
          if (num3 <= this.MaxCode)
          {
            int index3 = num3 * 2;
            int index4 = index3 + 1;
            if ((int) dynamicTree[index4] != index2)
            {
              manager.OptimalLength = (int) ((long) manager.OptimalLength + ((long) index2 - (long) dynamicTree[index4]) * (long) dynamicTree[index3]);
              dynamicTree[index4] = (short) index2;
            }
            --num2;
          }
        }
      }
    }
  }
}
