// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.IntelligentMail
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Data;
using System.Drawing;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class IntelligentMail : Symbology1D
  {
    private Size MinSize = new Size(130, 16);
    protected const string AscenderCharacterColumnName = "AC";
    protected const string AscenderBitColumnName = "AB";
    protected const string DescenderCharacterColumnName = "DC";
    protected const string DescenderBitColumnName = "DB";
    protected const char AscenderChar = 'A';
    protected const char DescenderChar = 'D';
    protected const char NoneChar = 'T';
    protected const char BothChar = 'F';
    private int[] codewordToCharacterLookup5of13;
    private int[] codewordToCharacterLookup2of13;
    private DataTable barToCharacterMapping;

    public IntelligentMail()
    {
      this.Stretch = true;
      this.TotalVerticalClearZone = 8;
      this.TotalHorizontalClearZone = 28;
    }

    public int TotalVerticalClearZone { get; set; }

    public int TotalHorizontalClearZone { get; set; }

    private int[] GetCodewordToCharacterLookupTable5of13()
    {
      if (this.codewordToCharacterLookup5of13 == null)
      {
        this.codewordToCharacterLookup5of13 = new int[1287];
        this.InitializeNof13Table(ref this.codewordToCharacterLookup5of13, 5, 1287);
      }
      return this.codewordToCharacterLookup5of13;
    }

    private int[] GetCodewordToCharacterLookupTable2of13()
    {
      if (this.codewordToCharacterLookup2of13 == null)
      {
        this.codewordToCharacterLookup2of13 = new int[78];
        this.InitializeNof13Table(ref this.codewordToCharacterLookup2of13, 2, 78);
      }
      return this.codewordToCharacterLookup2of13;
    }

    private DataTable GetBarToCharacterMapping()
    {
      if (this.barToCharacterMapping == null)
      {
        this.barToCharacterMapping = new DataTable();
        this.barToCharacterMapping.Columns.Add("DC", typeof (char));
        this.barToCharacterMapping.Columns.Add("DB", typeof (int));
        this.barToCharacterMapping.Columns.Add("AC", typeof (char));
        this.barToCharacterMapping.Columns.Add("AB", typeof (int));
        this.barToCharacterMapping.Rows.Add((object) 'H', (object) 2, (object) 'E', (object) 3);
        this.barToCharacterMapping.Rows.Add((object) 'B', (object) 10, (object) 'A', (object) 0);
        this.barToCharacterMapping.Rows.Add((object) 'J', (object) 12, (object) 'C', (object) 8);
        this.barToCharacterMapping.Rows.Add((object) 'F', (object) 5, (object) 'G', (object) 11);
        this.barToCharacterMapping.Rows.Add((object) 'I', (object) 9, (object) 'D', (object) 1);
        this.barToCharacterMapping.Rows.Add((object) 'A', (object) 1, (object) 'F', (object) 12);
        this.barToCharacterMapping.Rows.Add((object) 'C', (object) 5, (object) 'B', (object) 8);
        this.barToCharacterMapping.Rows.Add((object) 'E', (object) 4, (object) 'J', (object) 11);
        this.barToCharacterMapping.Rows.Add((object) 'G', (object) 3, (object) 'I', (object) 10);
        this.barToCharacterMapping.Rows.Add((object) 'D', (object) 9, (object) 'H', (object) 6);
        this.barToCharacterMapping.Rows.Add((object) 'F', (object) 11, (object) 'B', (object) 4);
        this.barToCharacterMapping.Rows.Add((object) 'I', (object) 5, (object) 'C', (object) 12);
        this.barToCharacterMapping.Rows.Add((object) 'J', (object) 10, (object) 'A', (object) 2);
        this.barToCharacterMapping.Rows.Add((object) 'H', (object) 1, (object) 'G', (object) 7);
        this.barToCharacterMapping.Rows.Add((object) 'D', (object) 6, (object) 'E', (object) 9);
        this.barToCharacterMapping.Rows.Add((object) 'A', (object) 3, (object) 'I', (object) 6);
        this.barToCharacterMapping.Rows.Add((object) 'G', (object) 4, (object) 'C', (object) 7);
        this.barToCharacterMapping.Rows.Add((object) 'B', (object) 1, (object) 'J', (object) 9);
        this.barToCharacterMapping.Rows.Add((object) 'H', (object) 10, (object) 'F', (object) 2);
        this.barToCharacterMapping.Rows.Add((object) 'E', (object) 0, (object) 'D', (object) 8);
        this.barToCharacterMapping.Rows.Add((object) 'G', (object) 2, (object) 'A', (object) 4);
        this.barToCharacterMapping.Rows.Add((object) 'I', (object) 11, (object) 'B', (object) 0);
        this.barToCharacterMapping.Rows.Add((object) 'J', (object) 8, (object) 'D', (object) 12);
        this.barToCharacterMapping.Rows.Add((object) 'C', (object) 6, (object) 'H', (object) 7);
        this.barToCharacterMapping.Rows.Add((object) 'F', (object) 1, (object) 'E', (object) 10);
        this.barToCharacterMapping.Rows.Add((object) 'B', (object) 12, (object) 'G', (object) 9);
        this.barToCharacterMapping.Rows.Add((object) 'H', (object) 3, (object) 'I', (object) 0);
        this.barToCharacterMapping.Rows.Add((object) 'F', (object) 8, (object) 'J', (object) 7);
        this.barToCharacterMapping.Rows.Add((object) 'E', (object) 6, (object) 'C', (object) 10);
        this.barToCharacterMapping.Rows.Add((object) 'D', (object) 4, (object) 'A', (object) 5);
        this.barToCharacterMapping.Rows.Add((object) 'I', (object) 4, (object) 'F', (object) 7);
        this.barToCharacterMapping.Rows.Add((object) 'H', (object) 11, (object) 'B', (object) 9);
        this.barToCharacterMapping.Rows.Add((object) 'G', (object) 0, (object) 'J', (object) 6);
        this.barToCharacterMapping.Rows.Add((object) 'A', (object) 6, (object) 'E', (object) 8);
        this.barToCharacterMapping.Rows.Add((object) 'C', (object) 1, (object) 'D', (object) 2);
        this.barToCharacterMapping.Rows.Add((object) 'F', (object) 9, (object) 'I', (object) 12);
        this.barToCharacterMapping.Rows.Add((object) 'E', (object) 11, (object) 'G', (object) 1);
        this.barToCharacterMapping.Rows.Add((object) 'J', (object) 5, (object) 'H', (object) 4);
        this.barToCharacterMapping.Rows.Add((object) 'D', (object) 3, (object) 'B', (object) 2);
        this.barToCharacterMapping.Rows.Add((object) 'A', (object) 7, (object) 'C', (object) 0);
        this.barToCharacterMapping.Rows.Add((object) 'B', (object) 3, (object) 'E', (object) 1);
        this.barToCharacterMapping.Rows.Add((object) 'G', (object) 10, (object) 'D', (object) 5);
        this.barToCharacterMapping.Rows.Add((object) 'I', (object) 7, (object) 'J', (object) 4);
        this.barToCharacterMapping.Rows.Add((object) 'C', (object) 11, (object) 'F', (object) 6);
        this.barToCharacterMapping.Rows.Add((object) 'A', (object) 8, (object) 'H', (object) 12);
        this.barToCharacterMapping.Rows.Add((object) 'E', (object) 2, (object) 'I', (object) 1);
        this.barToCharacterMapping.Rows.Add((object) 'F', (object) 10, (object) 'D', (object) 0);
        this.barToCharacterMapping.Rows.Add((object) 'J', (object) 3, (object) 'A', (object) 9);
        this.barToCharacterMapping.Rows.Add((object) 'G', (object) 5, (object) 'C', (object) 4);
        this.barToCharacterMapping.Rows.Add((object) 'H', (object) 8, (object) 'B', (object) 7);
        this.barToCharacterMapping.Rows.Add((object) 'F', (object) 0, (object) 'E', (object) 5);
        this.barToCharacterMapping.Rows.Add((object) 'C', (object) 3, (object) 'A', (object) 10);
        this.barToCharacterMapping.Rows.Add((object) 'G', (object) 12, (object) 'J', (object) 2);
        this.barToCharacterMapping.Rows.Add((object) 'D', (object) 11, (object) 'B', (object) 6);
        this.barToCharacterMapping.Rows.Add((object) 'I', (object) 8, (object) 'H', (object) 9);
        this.barToCharacterMapping.Rows.Add((object) 'F', (object) 4, (object) 'A', (object) 11);
        this.barToCharacterMapping.Rows.Add((object) 'B', (object) 5, (object) 'C', (object) 2);
        this.barToCharacterMapping.Rows.Add((object) 'J', (object) 1, (object) 'E', (object) 12);
        this.barToCharacterMapping.Rows.Add((object) 'I', (object) 3, (object) 'G', (object) 6);
        this.barToCharacterMapping.Rows.Add((object) 'H', (object) 0, (object) 'D', (object) 7);
        this.barToCharacterMapping.Rows.Add((object) 'E', (object) 7, (object) 'H', (object) 5);
        this.barToCharacterMapping.Rows.Add((object) 'A', (object) 12, (object) 'B', (object) 11);
        this.barToCharacterMapping.Rows.Add((object) 'C', (object) 9, (object) 'J', (object) 0);
        this.barToCharacterMapping.Rows.Add((object) 'G', (object) 8, (object) 'F', (object) 3);
        this.barToCharacterMapping.Rows.Add((object) 'D', (object) 10, (object) 'I', (object) 2);
      }
      return this.barToCharacterMapping;
    }

    private bool InitializeNof13Table(ref int[] TableNof13, int N, int TableLength)
    {
      int index1 = 0;
      int index2 = TableLength - 1;
      for (ushort input = 0; input < (ushort) 8192; ++input)
      {
        int num1 = 0;
        for (int index3 = 0; index3 < 13; ++index3)
        {
          if (((int) input & 1 << index3) != 0)
            ++num1;
        }
        if (num1 == N)
        {
          ushort num2 = (ushort) ((uint) this.ReverseUnsignedShort(input) >> 3);
          if ((int) num2 >= (int) input)
          {
            if ((int) input == (int) num2)
            {
              TableNof13[index2] = (int) input;
              --index2;
            }
            else
            {
              TableNof13[index1] = (int) input;
              int index3 = index1 + 1;
              TableNof13[index3] = (int) num2;
              index1 = index3 + 1;
            }
          }
        }
      }
      return index1 == index2 + 1;
    }

    private ushort ReverseUnsignedShort(ushort input)
    {
      ushort num = 0;
      for (int index = 0; index < 16; ++index)
      {
        num = (ushort) ((uint) (ushort) ((uint) num << 1) | (uint) (ushort) ((uint) input & 1U));
        input >>= 1;
      }
      return num;
    }

    protected override float GetLength()
    {
      return base.GetLength() * 2f;
    }

    public override SizeF MeasureContent(IMeasureContext context, SizeF size)
    {
      SizeF sizeF = base.MeasureContent(context, size);
      if (float.IsInfinity(size.Width))
        sizeF.Width = this.GetLength() + (float) this.TotalHorizontalClearZone;
      if (float.IsInfinity(size.Height))
        sizeF.Height = (float) (this.MinSize.Height * this.Module + this.TotalVerticalClearZone);
      return sizeF;
    }

    protected override void CreateBarsOverride(IElementFactory factory)
    {
      RectangleF barRect = this.barRect;
      float x = barRect.X + (float) (this.TotalHorizontalClearZone / 2);
      int num1 = (int) ((double) barRect.Width - (double) this.TotalHorizontalClearZone) / 65;
      int num2 = (int) Math.Max(1.0, (double) num1 * 0.2);
      barRect.Y += (float) (this.TotalVerticalClearZone / 2);
      barRect.Height -= (float) this.TotalVerticalClearZone;
      if (num1 <= 0 || (double) barRect.Height <= 0.0)
        return;
      foreach (char ch in this.pattern)
      {
        switch (ch)
        {
          case 'A':
            factory.CreateBarElement(new RectangleF(x, barRect.Y, (float) (num1 - num2), (float) ((double) barRect.Height * 2.0 / 3.0)));
            break;
          case 'D':
            factory.CreateBarElement(new RectangleF(x, barRect.Y + barRect.Height / 3f, (float) (num1 - num2), (float) ((double) barRect.Height * 2.0 / 3.0)));
            break;
          case 'F':
            factory.CreateBarElement(new RectangleF(x, barRect.Y, (float) (num1 - num2), barRect.Height));
            break;
          case 'T':
            factory.CreateBarElement(new RectangleF(x, barRect.Y + barRect.Height / 3f, (float) (num1 - num2), barRect.Height / 3f));
            break;
        }
        x += (float) num1;
      }
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      switch (value.Length)
      {
        case 20:
          break;
        case 25:
          break;
        case 29:
          break;
        case 31:
          break;
        default:
          throw new InvalidLengthException("The length of the provided value is incorrect. Valid lenghts are 20, 25, 29 and 31 characters.");
      }
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      IntelligentMail.BinaryData binaryData = this.BuildBinaryData(value);
      int fnc = this.GenerateFnc(binaryData);
      int[] codewords = this.ConvertBinaryDataToCodewords(binaryData);
      this.AddAdditionalInformationToCodewords(codewords, fnc);
      return this.ConvertCharactersToIntelligentMailLetterSequence(this.ConvertCodewordsToCharacters(codewords, fnc));
    }

    private IntelligentMail.BinaryData BuildBinaryData(string value)
    {
      string s = value.Substring(20);
      string str = value.Substring(0, 20);
      IntelligentMail.BinaryData binaryData1;
      switch (s.Length)
      {
        case 0:
          binaryData1 = new IntelligentMail.BinaryData();
          break;
        case 5:
          binaryData1 = new IntelligentMail.BinaryData(long.Parse(s) + 1L);
          break;
        case 9:
          binaryData1 = new IntelligentMail.BinaryData(long.Parse(s) + 100000L + 1L);
          break;
        case 11:
          binaryData1 = new IntelligentMail.BinaryData(long.Parse(s) + 1000000000L + 100000L + 1L);
          break;
        default:
          throw new ArgumentException("The provided routing code is not of the correct length. Allowed lengths are 0, 5, 9, 11.");
      }
      IntelligentMail.BinaryData binaryData2 = (binaryData1 * new IntelligentMail.BinaryData(10L) + new IntelligentMail.BinaryData((long) int.Parse(str.Substring(0, 1)))) * new IntelligentMail.BinaryData(5L) + new IntelligentMail.BinaryData((long) int.Parse(str.Substring(1, 1)));
      for (int startIndex = 2; startIndex < str.Length; ++startIndex)
        binaryData2 = binaryData2 * new IntelligentMail.BinaryData(10L) + new IntelligentMail.BinaryData((long) int.Parse(str.Substring(startIndex, 1)));
      return binaryData2;
    }

    private int GenerateFnc(IntelligentMail.BinaryData binaryData)
    {
      byte[] byteArray = binaryData.ToByteArray(13);
      int num1 = 3893;
      int num2 = 2047;
      int num3 = (int) byteArray[0] << 5;
      for (int index = 2; index < 8; ++index)
      {
        num2 = (((num2 ^ num3) & 1024) == 0 ? num2 << 1 : num2 << 1 ^ num1) & 2047;
        num3 <<= 1;
      }
      for (int index1 = 1; index1 < 13; ++index1)
      {
        int num4 = (int) byteArray[index1] << 3;
        for (int index2 = 0; index2 < 8; ++index2)
        {
          num2 = (((num2 ^ num4) & 1024) == 0 ? num2 << 1 : num2 << 1 ^ num1) & 2047;
          num4 <<= 1;
        }
      }
      return num2;
    }

    private int[] ConvertBinaryDataToCodewords(IntelligentMail.BinaryData binaryData)
    {
      int[] numArray = new int[10];
      IntelligentMail.BinaryData binaryData1 = new IntelligentMail.BinaryData(636L);
      IntelligentMail.BinaryData binaryData2 = new IntelligentMail.BinaryData(1365L);
      numArray[9] = (int) (binaryData % binaryData1);
      binaryData /= binaryData1;
      for (int index = 8; index >= 1; --index)
      {
        numArray[index] = (int) (binaryData % binaryData2);
        binaryData /= binaryData2;
      }
      numArray[0] = (int) binaryData;
      return numArray;
    }

    private void AddAdditionalInformationToCodewords(int[] codewords, int fnc)
    {
      codewords[9] *= 2;
      if ((fnc & 1024) == 0)
        return;
      codewords[0] += 659;
    }

    private int[] ConvertCodewordsToCharacters(int[] codewords, int fnc)
    {
      int[] numArray = new int[codewords.Length];
      for (int index = 0; index < codewords.Length; ++index)
      {
        int codeword = codewords[index];
        numArray[index] = codewords[index] >= 1287 ? this.GetCodewordToCharacterLookupTable2of13()[codeword - 1287] : this.GetCodewordToCharacterLookupTable5of13()[codeword];
      }
      for (int index = 0; index < 10; ++index)
      {
        if ((fnc & 1 << index) != 0)
          numArray[index] ^= 8191;
      }
      return numArray;
    }

    private string ConvertCharactersToIntelligentMailLetterSequence(int[] characters)
    {
      DataTable characterMapping = this.GetBarToCharacterMapping();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < 65; ++index1)
      {
        char ch1 = (char) characterMapping.Rows[index1]["AC"];
        int num1 = (int) characterMapping.Rows[index1]["AB"];
        char ch2 = (char) characterMapping.Rows[index1]["DC"];
        int num2 = (int) characterMapping.Rows[index1]["DB"];
        int index2 = (int) ch1 - 65;
        int index3 = (int) ch2 - 65;
        bool flag1 = (characters[index2] & 1 << num1) != 0;
        bool flag2 = (characters[index3] & 1 << num2) != 0;
        if (flag1 && flag2)
          stringBuilder.Append('F');
        else if (flag2)
          stringBuilder.Append('D');
        else if (flag1)
          stringBuilder.Append('A');
        else
          stringBuilder.Append('T');
      }
      return stringBuilder.ToString();
    }

    private class BinaryData
    {
      private const int maxLength = 4;
      private uint[] number;
      private int numberLength;

      public BinaryData()
      {
        this.number = new uint[4];
        this.numberLength = 1;
      }

      public BinaryData(long value)
      {
        this.number = new uint[4];
        for (this.numberLength = 0; value != 0L && this.numberLength < 4; ++this.numberLength)
        {
          this.number[this.numberLength] = (uint) ((ulong) value & (ulong) uint.MaxValue);
          value >>= 32;
        }
      }

      public BinaryData(uint[] value)
      {
        this.number = value;
      }

      public BinaryData(IntelligentMail.BinaryData value)
      {
        this.number = (uint[]) value.number.Clone();
        this.numberLength = value.numberLength;
      }

      public static explicit operator int(IntelligentMail.BinaryData value)
      {
        return (int) value.number[0];
      }

      public static IntelligentMail.BinaryData operator <<(
        IntelligentMail.BinaryData bi1,
        int shiftVal)
      {
        IntelligentMail.BinaryData binaryData = new IntelligentMail.BinaryData(bi1);
        binaryData.numberLength = IntelligentMail.BinaryData.ShiftLeft(binaryData.number, shiftVal);
        return binaryData;
      }

      public static IntelligentMail.BinaryData operator -(
        IntelligentMail.BinaryData operand)
      {
        if (operand.numberLength == 1 && operand.number[0] == 0U)
          return new IntelligentMail.BinaryData();
        IntelligentMail.BinaryData binaryData = new IntelligentMail.BinaryData(operand);
        for (int index = 0; index < 4; ++index)
          binaryData.number[index] = ~operand.number[index];
        long num1 = 1;
        for (int index = 0; num1 != 0L && index < 4; ++index)
        {
          long num2 = (long) binaryData.number[index] + 1L;
          binaryData.number[index] = (uint) ((ulong) num2 & (ulong) uint.MaxValue);
          num1 = num2 >> 32;
        }
        binaryData.numberLength = 4;
        while (binaryData.numberLength > 1 && binaryData.number[binaryData.numberLength - 1] == 0U)
          --binaryData.numberLength;
        return binaryData;
      }

      public static IntelligentMail.BinaryData operator +(
        IntelligentMail.BinaryData left,
        IntelligentMail.BinaryData right)
      {
        IntelligentMail.BinaryData binaryData = new IntelligentMail.BinaryData();
        binaryData.numberLength = Math.Max(left.numberLength, right.numberLength);
        long num1 = 0;
        for (int index = 0; index < binaryData.numberLength; ++index)
        {
          long num2 = (long) left.number[index] + (long) right.number[index] + num1;
          num1 = num2 >> 32;
          binaryData.number[index] = (uint) ((ulong) num2 & (ulong) uint.MaxValue);
        }
        if (num1 != 0L && binaryData.numberLength < 4)
        {
          binaryData.number[binaryData.numberLength] = (uint) num1;
          ++binaryData.numberLength;
        }
        return binaryData;
      }

      public static IntelligentMail.BinaryData operator -(
        IntelligentMail.BinaryData left,
        IntelligentMail.BinaryData right)
      {
        IntelligentMail.BinaryData binaryData = new IntelligentMail.BinaryData();
        binaryData.numberLength = Math.Max(left.numberLength, right.numberLength);
        long num1 = 0;
        for (int index = 0; index < binaryData.numberLength; ++index)
        {
          long num2 = (long) left.number[index] - (long) right.number[index] - num1;
          binaryData.number[index] = (uint) ((ulong) num2 & (ulong) uint.MaxValue);
          num1 = num2 >= 0L ? 0L : 1L;
        }
        if (num1 != 0L)
        {
          for (int numberLength = binaryData.numberLength; numberLength < 4; ++numberLength)
            binaryData.number[numberLength] = uint.MaxValue;
          binaryData.numberLength = 4;
        }
        return binaryData;
      }

      public static IntelligentMail.BinaryData operator *(
        IntelligentMail.BinaryData left,
        IntelligentMail.BinaryData right)
      {
        int index1 = 3;
        bool flag1 = false;
        bool flag2 = false;
        if (((int) left.number[index1] & int.MinValue) != 0)
        {
          flag1 = true;
          left = -left;
        }
        if (((int) right.number[index1] & int.MinValue) != 0)
        {
          flag2 = true;
          right = -right;
        }
        IntelligentMail.BinaryData binaryData = new IntelligentMail.BinaryData();
        for (int index2 = 0; index2 < left.numberLength; ++index2)
        {
          if (left.number[index2] != 0U)
          {
            ulong num1 = 0;
            int index3 = 0;
            int index4 = index2;
            while (index3 < right.numberLength)
            {
              ulong num2 = (ulong) left.number[index2] * (ulong) right.number[index3] + (ulong) binaryData.number[index4] + num1;
              binaryData.number[index4] = (uint) (num2 & (ulong) uint.MaxValue);
              num1 = num2 >> 32;
              ++index3;
              ++index4;
            }
            if (num1 != 0UL)
              binaryData.number[index2 + right.numberLength] = (uint) num1;
          }
        }
        binaryData.numberLength = left.numberLength + right.numberLength;
        if (binaryData.numberLength > 4)
          binaryData.numberLength = 4;
        while (binaryData.numberLength > 1 && binaryData.number[binaryData.numberLength - 1] == 0U)
          --binaryData.numberLength;
        if (flag1 != flag2)
          return -binaryData;
        return binaryData;
      }

      public static IntelligentMail.BinaryData operator /(
        IntelligentMail.BinaryData left,
        IntelligentMail.BinaryData right)
      {
        IntelligentMail.BinaryData outQuotient = new IntelligentMail.BinaryData();
        IntelligentMail.BinaryData outRemainder = new IntelligentMail.BinaryData();
        int index = 3;
        bool flag1 = false;
        bool flag2 = false;
        if (((int) left.number[index] & int.MinValue) != 0)
        {
          left = -left;
          flag1 = true;
        }
        if (((int) right.number[index] & int.MinValue) != 0)
        {
          right = -right;
          flag2 = true;
        }
        if (left < right)
          return outQuotient;
        if (right.numberLength == 1)
          IntelligentMail.BinaryData.SingleByteDivide(left, right, outQuotient, outRemainder);
        else
          IntelligentMail.BinaryData.MultiByteDivide(left, right, outQuotient, outRemainder);
        if (flag1 != flag2)
          return -outQuotient;
        return outQuotient;
      }

      public static IntelligentMail.BinaryData operator %(
        IntelligentMail.BinaryData left,
        IntelligentMail.BinaryData right)
      {
        IntelligentMail.BinaryData outQuotient = new IntelligentMail.BinaryData();
        IntelligentMail.BinaryData outRemainder = new IntelligentMail.BinaryData(left);
        int index = 3;
        bool flag = false;
        if (((int) left.number[index] & int.MinValue) != 0)
        {
          left = -left;
          flag = true;
        }
        if (((int) right.number[index] & int.MinValue) != 0)
          right = -right;
        if (left < right)
          return outRemainder;
        if (right.numberLength == 1)
          IntelligentMail.BinaryData.SingleByteDivide(left, right, outQuotient, outRemainder);
        else
          IntelligentMail.BinaryData.MultiByteDivide(left, right, outQuotient, outRemainder);
        if (flag)
          return -outRemainder;
        return outRemainder;
      }

      public static bool operator >(
        IntelligentMail.BinaryData left,
        IntelligentMail.BinaryData right)
      {
        int index1 = 3;
        if (((int) left.number[index1] & int.MinValue) != 0 && ((int) right.number[index1] & int.MinValue) == 0)
          return false;
        if (((int) left.number[index1] & int.MinValue) == 0 && ((int) right.number[index1] & int.MinValue) != 0)
          return true;
        int index2 = Math.Max(left.numberLength, right.numberLength) - 1;
        while (index2 >= 0 && (int) left.number[index2] == (int) right.number[index2])
          --index2;
        return index2 >= 0 && left.number[index2] > right.number[index2];
      }

      public static bool operator <(
        IntelligentMail.BinaryData left,
        IntelligentMail.BinaryData right)
      {
        int index1 = 3;
        if (((int) left.number[index1] & int.MinValue) != 0 && ((int) right.number[index1] & int.MinValue) == 0)
          return true;
        if (((int) left.number[index1] & int.MinValue) == 0 && ((int) right.number[index1] & int.MinValue) != 0)
          return false;
        int index2 = Math.Max(left.numberLength, right.numberLength) - 1;
        while (index2 >= 0 && (int) left.number[index2] == (int) right.number[index2])
          --index2;
        return index2 >= 0 && left.number[index2] < right.number[index2];
      }

      private static void SingleByteDivide(
        IntelligentMail.BinaryData left,
        IntelligentMail.BinaryData right,
        IntelligentMail.BinaryData outQuotient,
        IntelligentMail.BinaryData outRemainder)
      {
        uint[] numArray = new uint[4];
        int num1 = 0;
        for (int index = 0; index < 4; ++index)
          outRemainder.number[index] = left.number[index];
        outRemainder.numberLength = left.numberLength;
        while (outRemainder.numberLength > 1 && outRemainder.number[outRemainder.numberLength - 1] == 0U)
          --outRemainder.numberLength;
        ulong num2 = (ulong) right.number[0];
        int index1 = outRemainder.numberLength - 1;
        ulong num3 = (ulong) outRemainder.number[index1];
        if (num3 >= num2)
        {
          ulong num4 = num3 / num2;
          numArray[num1++] = (uint) num4;
          outRemainder.number[index1] = (uint) (num3 % num2);
        }
        ulong num5;
        for (int index2 = index1 - 1; index2 >= 0; outRemainder.number[index2--] = (uint) (num5 % num2))
        {
          num5 = ((ulong) outRemainder.number[index2 + 1] << 32) + (ulong) outRemainder.number[index2];
          ulong num4 = num5 / num2;
          numArray[num1++] = (uint) num4;
          outRemainder.number[index2 + 1] = 0U;
        }
        outQuotient.numberLength = num1;
        int index3 = 0;
        int index4 = outQuotient.numberLength - 1;
        while (index4 >= 0)
        {
          outQuotient.number[index3] = numArray[index4];
          --index4;
          ++index3;
        }
        for (; index3 < 4; ++index3)
          outQuotient.number[index3] = 0U;
        while (outQuotient.numberLength > 1 && outQuotient.number[outQuotient.numberLength - 1] == 0U)
          --outQuotient.numberLength;
        if (outQuotient.numberLength == 0)
          outQuotient.numberLength = 1;
        while (outRemainder.numberLength > 1 && outRemainder.number[outRemainder.numberLength - 1] == 0U)
          --outRemainder.numberLength;
      }

      private static void MultiByteDivide(
        IntelligentMail.BinaryData left,
        IntelligentMail.BinaryData right,
        IntelligentMail.BinaryData outQuotient,
        IntelligentMail.BinaryData outRemainder)
      {
        uint[] numArray1 = new uint[4];
        int length1 = left.numberLength + 1;
        uint[] number = new uint[length1];
        uint num1 = 2147483648;
        uint num2 = right.number[right.numberLength - 1];
        int numberOfPositionsToShift = 0;
        int num3 = 0;
        for (; num1 != 0U && ((int) num2 & (int) num1) == 0; num1 >>= 1)
          ++numberOfPositionsToShift;
        for (int index = 0; index < left.numberLength; ++index)
          number[index] = left.number[index];
        IntelligentMail.BinaryData.ShiftLeft(number, numberOfPositionsToShift);
        right <<= numberOfPositionsToShift;
        int num4 = length1 - right.numberLength;
        int index1 = length1 - 1;
        ulong num5 = (ulong) right.number[right.numberLength - 1];
        ulong num6 = (ulong) right.number[right.numberLength - 2];
        int length2 = right.numberLength + 1;
        uint[] numArray2 = new uint[length2];
        for (; num4 > 0; --num4)
        {
          ulong num7 = ((ulong) number[index1] << 32) + (ulong) number[index1 - 1];
          ulong num8 = num7 / num5;
          ulong num9 = num7 % num5;
          bool flag = false;
          while (!flag)
          {
            flag = true;
            if (num8 == 4294967296UL || num8 * num6 > (num9 << 32) + (ulong) number[index1 - 2])
            {
              --num8;
              num9 += num5;
              if (num9 < 4294967296UL)
                flag = false;
            }
          }
          for (int index2 = 0; index2 < length2; ++index2)
            numArray2[index2] = number[index1 - index2];
          IntelligentMail.BinaryData binaryData1 = new IntelligentMail.BinaryData(numArray2);
          IntelligentMail.BinaryData binaryData2 = right * new IntelligentMail.BinaryData((long) num8);
          while (binaryData2 > binaryData1)
          {
            --num8;
            binaryData2 -= right;
          }
          IntelligentMail.BinaryData binaryData3 = binaryData1 - binaryData2;
          for (int index2 = 0; index2 < length2; ++index2)
            number[index1 - index2] = binaryData3.number[right.numberLength - index2];
          numArray1[num3++] = (uint) num8;
          --index1;
        }
        outQuotient.numberLength = num3;
        int index3 = 0;
        int index4 = outQuotient.numberLength - 1;
        while (index4 >= 0)
        {
          outQuotient.number[index3] = numArray1[index4];
          --index4;
          ++index3;
        }
        for (; index3 < 4; ++index3)
          outQuotient.number[index3] = 0U;
        while (outQuotient.numberLength > 1 && outQuotient.number[outQuotient.numberLength - 1] == 0U)
          --outQuotient.numberLength;
        if (outQuotient.numberLength == 0)
          outQuotient.numberLength = 1;
        outRemainder.numberLength = IntelligentMail.BinaryData.ShiftRight(number, numberOfPositionsToShift);
        int index5;
        for (index5 = 0; index5 < outRemainder.numberLength; ++index5)
          outRemainder.number[index5] = number[index5];
        for (; index5 < 4; ++index5)
          outRemainder.number[index5] = 0U;
      }

      private static int ShiftLeft(uint[] number, int numberOfPositionsToShift)
      {
        int num1 = 32;
        int length = number.Length;
        while (length > 1 && number[length - 1] == 0U)
          --length;
        for (int index1 = numberOfPositionsToShift; index1 > 0; index1 -= num1)
        {
          if (index1 < num1)
            num1 = index1;
          ulong num2 = 0;
          for (int index2 = 0; index2 < length; ++index2)
          {
            ulong num3 = (ulong) number[index2] << num1 | num2;
            number[index2] = (uint) (num3 & (ulong) uint.MaxValue);
            num2 = num3 >> 32;
          }
          if (num2 != 0UL && length + 1 <= number.Length)
          {
            number[length] = (uint) num2;
            ++length;
          }
        }
        return length;
      }

      private static int ShiftRight(uint[] number, int numberOfPositionsToShift)
      {
        int num1 = 32;
        int num2 = 0;
        int length = number.Length;
        while (length > 1 && number[length - 1] == 0U)
          --length;
        for (int index1 = numberOfPositionsToShift; index1 > 0; index1 -= num1)
        {
          if (index1 < num1)
          {
            num1 = index1;
            num2 = 32 - num1;
          }
          ulong num3 = 0;
          for (int index2 = length - 1; index2 >= 0; --index2)
          {
            ulong num4 = (ulong) number[index2] >> num1 | num3;
            num3 = (ulong) number[index2] << num2;
            number[index2] = (uint) num4;
          }
        }
        while (length > 1 && number[length - 1] == 0U)
          --length;
        return length;
      }

      public byte[] ToByteArray()
      {
        while (this.numberLength > 1 && this.number[this.numberLength - 1] == 0U)
          --this.numberLength;
        uint num1 = this.number[this.numberLength - 1];
        uint num2 = 2147483648;
        int num3;
        for (num3 = 32; num3 > 0 && ((int) num1 & (int) num2) == 0; num2 >>= 1)
          --num3;
        int num4 = num3 + (this.numberLength - 1 << 5);
        int length = num4 >> 3;
        if ((num4 & 7) != 0)
          ++length;
        byte[] numArray = new byte[length];
        int index1 = 0;
        uint num5 = this.number[this.numberLength - 1];
        uint num6;
        if ((num6 = num5 >> 24 & (uint) byte.MaxValue) != 0U)
          numArray[index1++] = (byte) num6;
        uint num7;
        if ((num7 = num5 >> 16 & (uint) byte.MaxValue) != 0U)
          numArray[index1++] = (byte) num7;
        uint num8;
        if ((num8 = num5 >> 8 & (uint) byte.MaxValue) != 0U)
          numArray[index1++] = (byte) num8;
        uint num9;
        if ((num9 = num5 & (uint) byte.MaxValue) != 0U)
          numArray[index1++] = (byte) num9;
        int index2 = this.numberLength - 2;
        while (index2 >= 0)
        {
          uint num10 = this.number[index2];
          numArray[index1 + 3] = (byte) (num10 & (uint) byte.MaxValue);
          uint num11 = num10 >> 8;
          numArray[index1 + 2] = (byte) (num11 & (uint) byte.MaxValue);
          uint num12 = num11 >> 8;
          numArray[index1 + 1] = (byte) (num12 & (uint) byte.MaxValue);
          uint num13 = num12 >> 8;
          numArray[index1] = (byte) (num13 & (uint) byte.MaxValue);
          --index2;
          index1 += 4;
        }
        return numArray;
      }

      public byte[] ToByteArray(int numberOfBytes)
      {
        byte[] numArray = new byte[numberOfBytes];
        byte[] byteArray = this.ToByteArray();
        Array.Copy((Array) byteArray, 0, (Array) numArray, numArray.Length - byteArray.Length, byteArray.Length);
        return numArray;
      }
    }
  }
}
