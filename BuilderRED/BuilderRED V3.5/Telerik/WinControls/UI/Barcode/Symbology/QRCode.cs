// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.QRCode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class QRCode : ISymbology, INotifyPropertyChanged
  {
    private static List<bool> version7 = new List<bool>(18) { false, false, true, false, true, false, false, true, false, false, true, true, true, true, true, false, false, false };
    private static List<bool> version8 = new List<bool>(18) { false, false, true, true, true, true, false, true, true, false, true, false, false, false, false, true, false, false };
    private static List<bool> version9 = new List<bool>(18) { true, false, false, true, true, false, false, true, false, true, false, true, true, false, false, true, false, false };
    private static List<bool> version10 = new List<bool>(18) { true, true, false, false, true, false, true, true, false, false, true, false, false, true, false, true, false, false };
    private static List<bool> version11 = new List<bool>(18) { false, true, true, false, true, true, true, true, true, true, false, true, true, true, false, true, false, false };
    private static List<bool> version12 = new List<bool>(18) { false, true, false, false, false, true, true, false, true, true, true, false, false, false, true, true, false, false };
    private static List<bool> version13 = new List<bool>(18) { true, true, true, false, false, false, true, false, false, false, false, true, true, false, true, true, false, false };
    private static List<bool> version14 = new List<bool>(18) { true, false, true, true, false, false, false, false, false, true, true, false, false, true, true, true, false, false };
    private static List<bool> version15 = new List<bool>(18) { false, false, false, true, false, true, false, false, true, false, false, true, true, true, true, true, false, false };
    private static List<bool> version16 = new List<bool>(18) { false, false, false, true, true, true, true, false, true, true, false, true, false, false, false, false, true, false };
    private static List<bool> version17 = new List<bool>(18) { true, false, true, true, true, false, true, false, false, false, true, false, true, false, false, false, true, false };
    private static List<bool> version18 = new List<bool>(18) { true, true, true, false, true, false, false, false, false, true, false, true, false, true, false, false, true, false };
    private static List<bool> version19 = new List<bool>(18) { false, true, false, false, true, true, false, false, true, false, true, false, true, true, false, false, true, false };
    private static List<bool> version20 = new List<bool>(18) { false, true, true, false, false, true, false, true, true, false, false, true, false, false, true, false, true, false };
    private static List<bool> version21 = new List<bool>(18) { true, true, false, false, false, false, false, true, false, true, true, false, true, false, true, false, true, false };
    private static List<bool> version22 = new List<bool>(18) { true, false, false, true, false, false, true, true, false, false, false, true, false, true, true, false, true, false };
    private static List<bool> version23 = new List<bool>(18) { false, false, true, true, false, true, true, true, true, true, true, false, true, true, true, false, true, false };
    private static List<bool> version24 = new List<bool>(18) { false, false, true, false, false, false, true, true, false, true, true, true, false, false, false, true, true, false };
    private static List<bool> version25 = new List<bool>(18) { true, false, false, false, false, true, true, true, true, false, false, false, true, false, false, true, true, false };
    private static List<bool> version26 = new List<bool>(18) { true, true, false, true, false, true, false, true, true, true, true, true, false, true, false, true, true, false };
    private static List<bool> version27 = new List<bool>(18) { false, true, true, true, false, false, false, true, false, false, false, false, true, true, false, true, true, false };
    private static List<bool> version28 = new List<bool>(18) { false, true, false, true, true, false, false, false, false, false, true, true, false, false, true, true, true, false };
    private static List<bool> version29 = new List<bool>(18) { true, true, true, true, true, true, false, false, true, true, false, false, true, false, true, true, true, false };
    private static List<bool> version30 = new List<bool>(18) { true, false, true, false, true, true, true, false, true, false, true, true, false, true, true, true, true, false };
    private static List<bool> version31 = new List<bool>(18) { false, false, false, false, true, false, true, false, false, true, false, false, true, true, true, true, true, false };
    private static List<bool> version32 = new List<bool>(18) { true, false, true, false, true, false, true, true, true, false, false, true, false, false, false, false, false, true };
    private static List<bool> version33 = new List<bool>(18) { false, false, false, false, true, true, true, true, false, true, true, false, true, false, false, false, false, true };
    private static List<bool> version34 = new List<bool>(18) { false, true, false, true, true, true, false, true, false, false, false, true, false, true, false, false, false, true };
    private static List<bool> version35 = new List<bool>(18) { true, true, true, true, true, false, false, true, true, true, true, false, true, true, false, false, false, true };
    private static List<bool> version36 = new List<bool>(18) { true, true, false, true, false, false, false, false, true, true, false, true, false, false, true, false, false, true };
    private static List<bool> version37 = new List<bool>(18) { false, true, true, true, false, true, false, false, false, false, true, false, true, false, true, false, false, true };
    private static List<bool> version38 = new List<bool>(18) { false, false, true, false, false, true, true, false, false, true, false, true, false, true, true, false, false, true };
    private static List<bool> version39 = new List<bool>(18) { true, false, false, false, false, false, true, false, true, false, true, false, true, true, true, false, false, true };
    private static List<bool> version40 = new List<bool>(18) { true, false, false, true, false, true, true, false, false, false, true, true, false, false, false, true, false, true };
    private static Dictionary<int, List<bool>> positionValues = new Dictionary<int, List<bool>>();
    private static Dictionary<DataCapacityIndexer, int> dataCapacityTable = new Dictionary<DataCapacityIndexer, int>();
    private static Dictionary<CodeMode, string> codeModeValues = new Dictionary<CodeMode, string>();
    private static Dictionary<string, CodeWordsBlockInfo> codeWordsLengthTable = new Dictionary<string, CodeWordsBlockInfo>();
    private static List<string> maskCodes = new List<string>() { "000", "001", "010", "011", "100", "101", "110", "111" };
    private ErrorCorrectionLevel errorCorrectionLevel = ErrorCorrectionLevel.L;
    private int module = 1;
    public const int QuietZone = 4;
    private const string ECIModeIndicator = "0111";
    private const string FNC1FirstPositionIndicator = "0101";
    private const string FNC1SecondPositionIndicator = "1001";
    private CodeMode codeMode;
    private int version;
    private ECIMode eciMode;
    private FNC1Mode fnc1Mode;
    private string applicationIndicator;
    private bool[,] binaryMatrix;
    private bool[,] filledValuesMatrix;
    private string encodedData;
    private CodeWordsBlockInfo info;
    private Dictionary<int, string> binaryValues;
    private Dictionary<int, string> dataResult;
    private int sizeOfMatrix;
    private int sizeOfData;
    private bool autoSetVerion;
    private static List<int> exponentsOfAlphaToValues;
    private static List<int> valuesToExponentsOfAlpha;
    private static Dictionary<int, List<int>> generatorExponentsOfAlpha;
    private static Dictionary<int, List<int>> positionAdjustmentPatternCoordinates;
    private static Dictionary<ErrorCorrectionLevel, string> errorCorrectionToMask;
    private static Dictionary<string, string> formatInformation;

    [Browsable(false)]
    public bool[,] BinaryMatrix
    {
      get
      {
        return this.binaryMatrix;
      }
    }

    [DefaultValue(1)]
    public int Module
    {
      get
      {
        return this.module;
      }
      set
      {
        if (this.module == value)
          return;
        this.module = value;
        this.OnPropertyChanged(nameof (Module));
      }
    }

    [Browsable(false)]
    public bool[,] FilledValuesMatrix
    {
      get
      {
        return this.filledValuesMatrix;
      }
    }

    [Browsable(false)]
    public int VersionDimension { get; private set; }

    [DefaultValue(0)]
    public int Version
    {
      get
      {
        return this.version;
      }
      set
      {
        if (this.version == value)
          return;
        if (value < 1 || value > 40)
        {
          value = 1;
          this.autoSetVerion = true;
        }
        else
          this.autoSetVerion = false;
        this.SetVersion(value);
        this.OnPropertyChanged(nameof (Version));
      }
    }

    [DefaultValue(CodeMode.Byte)]
    public CodeMode CodeMode
    {
      get
      {
        return this.codeMode;
      }
      set
      {
        if (this.codeMode == value)
          return;
        this.codeMode = value;
        this.OnPropertyChanged(nameof (CodeMode));
      }
    }

    [DefaultValue(ErrorCorrectionLevel.L)]
    public ErrorCorrectionLevel ErrorCorrectionLevel
    {
      get
      {
        return this.errorCorrectionLevel;
      }
      set
      {
        if (this.errorCorrectionLevel == value)
          return;
        this.errorCorrectionLevel = value;
        this.OnPropertyChanged(nameof (ErrorCorrectionLevel));
      }
    }

    [DefaultValue(ECIMode.None)]
    public ECIMode ECIMode
    {
      get
      {
        return this.eciMode;
      }
      set
      {
        if (this.eciMode == value)
          return;
        this.eciMode = value;
        this.OnPropertyChanged(nameof (ECIMode));
      }
    }

    [DefaultValue(FNC1Mode.None)]
    public FNC1Mode FNC1Mode
    {
      get
      {
        return this.fnc1Mode;
      }
      set
      {
        if (this.fnc1Mode == value)
          return;
        this.fnc1Mode = value;
        this.OnPropertyChanged(nameof (FNC1Mode));
      }
    }

    [DefaultValue(null)]
    public string ApplicationIndicator
    {
      get
      {
        return this.applicationIndicator;
      }
      set
      {
        if (!(this.applicationIndicator != value))
          return;
        this.applicationIndicator = value;
        this.OnPropertyChanged(nameof (ApplicationIndicator));
      }
    }

    public QRCode()
      : this(CodeMode.Byte, 0, ErrorCorrectionLevel.L, ECIMode.None, FNC1Mode.None, (string) null)
    {
    }

    public QRCode(
      CodeMode mode,
      int version,
      ErrorCorrectionLevel errorLevel,
      ECIMode eciMode,
      FNC1Mode fncMode,
      string appIndicator)
    {
      this.codeMode = mode;
      this.errorCorrectionLevel = errorLevel;
      this.eciMode = eciMode;
      this.fnc1Mode = fncMode;
      this.applicationIndicator = appIndicator;
      if (version < 1 || version > 40)
      {
        version = 1;
        this.autoSetVerion = true;
      }
      this.SetVersion(version);
    }

    public string EncodeData(string dataToEncode)
    {
      this.encodedData = (string) null;
      if (dataToEncode == null)
        return string.Empty;
      string str = dataToEncode;
      if (this.codeMode == CodeMode.Alphanumeric)
        str = this.EncodeAlphaNumeric(dataToEncode);
      else if (this.codeMode == CodeMode.Numeric)
        str = this.EndcodeNumeric(dataToEncode);
      else if (this.codeMode == CodeMode.Byte)
        str = this.EncodeByte(dataToEncode);
      else if (this.codeMode == CodeMode.Kanji)
        str = this.EncodeKanji(dataToEncode);
      string empty = string.Empty;
      for (int index = 0; index < this.dataResult.Count; ++index)
        empty += this.dataResult[index];
      this.AddFNC1Data();
      this.encodedData += QRCode.codeModeValues[this.codeMode];
      int num1 = this.encodedData.Length + empty.Length;
      if (this.autoSetVerion)
      {
        for (int index = 1; index < 41; ++index)
        {
          int num2 = QRCode.dataCapacityTable[new DataCapacityIndexer(index, this.errorCorrectionLevel)] - this.DetermineCountLength(index);
          if (num1 <= num2)
          {
            this.SetVersion(index);
            break;
          }
        }
      }
      int requiredLength = QRCode.dataCapacityTable[new DataCapacityIndexer(this.version, this.errorCorrectionLevel)];
      int countLength = this.DetermineCountLength();
      if (num1 > requiredLength - countLength)
        throw new ArgumentOutOfRangeException("The Text cannot be encoded with the current Version, ErrorCorrectionLevel and Mode.");
      this.encodedData += (this.codeMode != CodeMode.Byte ? Convert.ToString(str.Length, 2) : Convert.ToString(empty.Length / 8, 2)).PadLeft(countLength, '0');
      this.encodedData += empty;
      this.encodedData = this.PadLength(this.encodedData, requiredLength);
      this.encodedData = this.GenerateErrorCorrectionSequence();
      this.PopulateBinaryMatricesWithData();
      return this.encodedData;
    }

    private static void SetSingleCharacter(
      int rowIndex,
      int columnIndex,
      char value,
      bool[,] matrix,
      string maskCode)
    {
      bool flag1;
      switch (maskCode)
      {
        case "000":
          flag1 = (columnIndex + rowIndex) % 2 == 0;
          break;
        case "001":
          flag1 = rowIndex % 2 == 0;
          break;
        case "010":
          flag1 = columnIndex % 3 == 0;
          break;
        case "011":
          flag1 = (rowIndex + columnIndex) % 3 == 0;
          break;
        case "100":
          flag1 = (rowIndex / 2 + columnIndex / 3) % 2 == 0;
          break;
        case "101":
          flag1 = columnIndex * rowIndex % 2 + columnIndex * rowIndex % 3 == 0;
          break;
        case "110":
          flag1 = (columnIndex * rowIndex % 2 + columnIndex * rowIndex % 3) % 2 == 0;
          break;
        case "111":
          flag1 = ((rowIndex + columnIndex) % 2 + rowIndex * columnIndex % 3) % 2 == 0;
          break;
        default:
          flag1 = (columnIndex + rowIndex) % 2 == 0;
          break;
      }
      bool flag2 = !flag1 ? value != '0' : value == '0';
      matrix[rowIndex + 4, columnIndex + 4] = flag2;
    }

    private static List<int> GetErrorCorrectionForBlock(
      List<int> dataList,
      CodeWordsBlockInfo localInfo)
    {
      int codeWordsPerBlock = localInfo.CodeWordsPerBlock;
      int[] array = new int[codeWordsPerBlock];
      List<int> intList = new List<int>(dataList.Count + codeWordsPerBlock);
      for (int index = 0; index < dataList.Count + codeWordsPerBlock; ++index)
      {
        if (index < dataList.Count)
          intList.Add(dataList[index]);
        else
          intList.Add(0);
      }
      for (int index1 = 0; index1 < dataList.Count; ++index1)
      {
        int index2 = intList[0];
        intList.RemoveAt(0);
        if (index2 != 0)
        {
          QRCode.generatorExponentsOfAlpha[codeWordsPerBlock].CopyTo(array);
          int num1 = QRCode.valuesToExponentsOfAlpha[index2];
          for (int index3 = 0; index3 < array.Length; ++index3)
          {
            int num2 = num1 + array[index3];
            if (num2 > (int) byte.MaxValue)
              num2 %= (int) byte.MaxValue;
            array[index3] = num2;
          }
          for (int index3 = 0; index3 < array.Length; ++index3)
            array[index3] = QRCode.exponentsOfAlphaToValues[array[index3]];
          for (int index3 = 0; index3 < array.Length; ++index3)
            intList[index3] ^= array[index3];
        }
      }
      return intList;
    }

    private void SetVersion(int newVersion)
    {
      this.version = newVersion;
      int num1 = 21 + (newVersion - 1) * 4;
      int num2 = num1 + 8;
      this.VersionDimension = num1;
      this.sizeOfData = num1;
      this.sizeOfMatrix = num2;
      this.info = QRCode.codeWordsLengthTable[newVersion.ToString() + this.errorCorrectionLevel.ToString()];
      this.PopulateValueMatrix();
      if (newVersion < 7)
        return;
      this.PopulateVersionData();
    }

    private string EncodeKanji(string dataToEncodeL)
    {
      KanjiMode kanjiMode = new KanjiMode();
      dataToEncodeL = kanjiMode.ValidateData(dataToEncodeL);
      this.dataResult = kanjiMode.EncodeData(dataToEncodeL);
      return dataToEncodeL;
    }

    private static ECIBase GetEncoding(ECIMode mode)
    {
      switch (mode)
      {
        case ECIMode.ISO8859_1:
          return (ECIBase) new ISO8859_1();
        case ECIMode.CP437:
          return (ECIBase) new CP437_DOSLatinUS();
        case ECIMode.ISO8859_1En:
          return (ECIBase) new ISO8859_1Eng();
        case ECIMode.ISO8859_2:
          return (ECIBase) new ISO8859_2ECEuropean();
        case ECIMode.ISO8859_3:
          return (ECIBase) new ISO8859_3Latin();
        case ECIMode.ISO8859_4:
          return (ECIBase) new ISO8859_4Baltic();
        case ECIMode.ISO8859_5:
          return (ECIBase) new ISO8859_5Cyrillic();
        case ECIMode.ISO8859_6:
          return (ECIBase) new ISO8859_6Arabic();
        case ECIMode.ISO8859_7:
          return (ECIBase) new ISO8859_7Greek();
        case ECIMode.ISO8859_8:
          return (ECIBase) new ISO8859_8Hebrew();
        case ECIMode.ISO8859_9:
          return (ECIBase) new ISO8859_9Turkish();
        case ECIMode.ISO8859_11:
          return (ECIBase) new ISO8859_11Thai();
        case ECIMode.ISO8859_13:
          return (ECIBase) new ISO8859_13();
        case ECIMode.ISO8859_15:
          return (ECIBase) new ISO8859_15();
        case ECIMode.Windows1250:
          return (ECIBase) new Windows1250();
        case ECIMode.Windows1251:
          return (ECIBase) new Windows1251();
        case ECIMode.Windows1252:
          return (ECIBase) new Windows1252();
        case ECIMode.Windows1256:
          return (ECIBase) new Windows1256();
        case ECIMode.UTF8:
          return (ECIBase) new UTF8();
        case ECIMode.ISO646US:
          return (ECIBase) new ISO646US();
        default:
          return (ECIBase) null;
      }
    }

    internal string EncodeByte(string dataToEncodeL)
    {
      if (this.eciMode == ECIMode.None)
      {
        ByteMode2D byteMode2D = new ByteMode2D();
        dataToEncodeL = byteMode2D.ValidateData(dataToEncodeL);
        this.dataResult = byteMode2D.EncodeData(dataToEncodeL);
      }
      else
      {
        this.encodedData += "0111";
        this.encodedData += Convert.ToString((int) this.eciMode, 2).PadLeft(8, '0');
        ECIBase encoding = QRCode.GetEncoding(this.eciMode);
        if (encoding != null)
        {
          dataToEncodeL = encoding.ValidateData(dataToEncodeL);
          this.dataResult = encoding.EncodeData(dataToEncodeL);
        }
      }
      return dataToEncodeL;
    }

    private string EndcodeNumeric(string dataToEncodeL)
    {
      Numeric numeric = new Numeric();
      dataToEncodeL = numeric.ValidateData(dataToEncodeL);
      this.dataResult = numeric.EncodeData(dataToEncodeL);
      return dataToEncodeL;
    }

    private string EncodeAlphaNumeric(string dataToEncodeL)
    {
      AlphaNumeric alphaNumeric = new AlphaNumeric();
      dataToEncodeL = alphaNumeric.ValidateData(dataToEncodeL);
      this.dataResult = alphaNumeric.EncodeData(dataToEncodeL);
      return dataToEncodeL;
    }

    private void AddFNC1Data()
    {
      if (this.fnc1Mode == FNC1Mode.FirstPosition)
      {
        this.encodedData += "0101";
      }
      else
      {
        if (this.fnc1Mode != FNC1Mode.SecondPosition)
          return;
        this.encodedData += "1001";
        this.ValidateApplicationIndicatorValue();
      }
    }

    private void ValidateApplicationIndicatorValue()
    {
      if (this.applicationIndicator.Length == 2)
      {
        char c1 = this.applicationIndicator[0];
        char c2 = this.applicationIndicator[1];
        int result;
        if (!char.IsDigit(c1) || !char.IsDigit(c2) || !int.TryParse(this.applicationIndicator, out result))
          return;
        this.encodedData += Convert.ToString(result, 2).PadLeft(8, '0');
      }
      else
      {
        if (this.applicationIndicator.Length != 1)
          return;
        char c = this.applicationIndicator.ToCharArray()[0];
        if (!char.IsLetter(c))
          return;
        this.encodedData += Convert.ToString((int) c + 100, 2).PadLeft(8, '0');
      }
    }

    private string GenerateErrorCorrectionSequence()
    {
      List<int> intList1 = new List<int>();
      for (int index = 0; index < this.binaryValues.Count; ++index)
        intList1.Add(Convert.ToInt32(this.binaryValues[index], 2));
      int firstBlockCount = this.info.FirstBlockCount;
      int secondBlockCount = this.info.SecondBlockCount;
      int index1 = 0;
      List<List<int>> intListList1 = new List<List<int>>();
      List<List<int>> intListList2 = new List<List<int>>();
      List<List<int>> intListList3 = new List<List<int>>();
      List<List<int>> intListList4 = new List<List<int>>();
      List<List<int>> intListList5 = new List<List<int>>();
      List<List<int>> intListList6 = new List<List<int>>();
      List<int> intList2 = new List<int>();
      List<int> intList3 = new List<int>();
      for (; firstBlockCount > 0; --firstBlockCount)
      {
        List<int> dataList = new List<int>();
        for (int index2 = 0; index2 < this.info.FirstDataCodeWords; ++index2)
        {
          dataList.Add(intList1[index1]);
          ++index1;
        }
        intListList1.Add(dataList);
        intListList5.Add(QRCode.GetErrorCorrectionForBlock(dataList, this.info));
      }
      for (; secondBlockCount > 0; --secondBlockCount)
      {
        List<int> dataList = new List<int>();
        for (int index2 = 0; index2 < this.info.SecondBlockCodeWords; ++index2)
        {
          dataList.Add(intList1[index1]);
          ++index1;
        }
        intListList2.Add(dataList);
        intListList6.Add(QRCode.GetErrorCorrectionForBlock(dataList, this.info));
      }
      int num1 = this.info.FirstDataCodeWords * this.info.FirstBlockCount + this.info.SecondBlockCodeWords * this.info.SecondBlockCount;
      int num2 = this.info.CodeWordsPerBlock * this.info.FirstBlockCount + this.info.CodeWordsPerBlock * this.info.SecondBlockCount;
      int num3 = 0;
      int num4 = intListList1.Count + intListList2.Count;
      for (int index2 = 0; index2 < num4; ++index2)
      {
        switch (num3)
        {
          case 0:
            if (intListList1.Count > 0)
            {
              intListList3.Add(intListList1[0]);
              intListList1.RemoveAt(0);
              break;
            }
            intListList3.Add(intListList2[0]);
            ++num3;
            intListList2.RemoveAt(0);
            break;
          case 1:
            if (intListList2.Count > 0)
            {
              intListList3.Add(intListList2[0]);
              intListList2.RemoveAt(0);
              break;
            }
            break;
        }
      }
      int num5 = 0;
      int num6 = intListList5.Count + intListList6.Count;
      for (int index2 = 0; index2 < num6; ++index2)
      {
        switch (num5)
        {
          case 0:
            if (intListList5.Count > 0)
            {
              intListList4.Add(intListList5[0]);
              intListList5.RemoveAt(0);
              break;
            }
            intListList4.Add(intListList6[0]);
            ++num5;
            intListList6.RemoveAt(0);
            break;
          case 1:
            if (intListList6.Count > 0)
            {
              intListList4.Add(intListList6[0]);
              intListList6.RemoveAt(0);
              break;
            }
            break;
        }
      }
      int count1 = intListList3.Count;
      int count2 = intListList4.Count;
      int index3 = 0;
      for (int index2 = 0; index2 < num1; ++index2)
      {
        if (index3 == count1)
          index3 = 0;
        if (intListList3[index3].Count > 0)
        {
          intList2.Add(intListList3[index3][0]);
          intListList3[index3].RemoveAt(0);
          ++index3;
        }
        else
        {
          ++index3;
          --index2;
        }
      }
      int index4 = 0;
      for (int index2 = 0; index2 < num2; ++index2)
      {
        if (index4 == count2)
          index4 = 0;
        if (intListList4[index4].Count > 0)
        {
          intList3.Add(intListList4[index4][0]);
          intListList4[index4].RemoveAt(0);
          ++index4;
        }
        else
        {
          ++index4;
          --index2;
        }
      }
      StringBuilder stringBuilder = new StringBuilder();
      foreach (int num7 in intList2)
        stringBuilder.Append(Convert.ToString(num7, 2).PadLeft(8, '0'));
      foreach (int num7 in intList3)
        stringBuilder.Append(Convert.ToString(num7, 2).PadLeft(8, '0'));
      return stringBuilder.ToString();
    }

    private void PopulateBinaryValues(string valueToBreak)
    {
      int num = 0;
      for (int startIndex = 0; startIndex < valueToBreak.Length; startIndex += 8)
        this.binaryValues[num++] = startIndex + 8 > valueToBreak.Length ? valueToBreak.ToString().Substring(startIndex).PadRight(8, '0') : valueToBreak.ToString().Substring(startIndex, 8);
    }

    private string PadLength(string valueToAdjust, int requiredLength)
    {
      StringBuilder stringBuilder = new StringBuilder();
      this.binaryValues = new Dictionary<int, string>();
      if (valueToAdjust.Length > requiredLength)
      {
        stringBuilder.Append(valueToAdjust.Substring(0, requiredLength));
        this.PopulateBinaryValues(stringBuilder.ToString());
      }
      else if (valueToAdjust.Length == requiredLength)
      {
        stringBuilder.Append(valueToAdjust);
        this.PopulateBinaryValues(stringBuilder.ToString());
      }
      else
      {
        stringBuilder.Append(valueToAdjust);
        for (int index = 0; index < 4; ++index)
        {
          if (stringBuilder.Length < requiredLength)
            stringBuilder.Append('0');
        }
        this.PopulateBinaryValues(stringBuilder.ToString());
        stringBuilder = new StringBuilder();
        for (int index = 0; index < this.binaryValues.Count; ++index)
          stringBuilder.Append(this.binaryValues[index]);
        string str1 = "11101100";
        string str2 = "00010001";
        bool flag = true;
        while (stringBuilder.Length < requiredLength)
        {
          if (flag)
          {
            stringBuilder.Append(str1);
            this.binaryValues.Add(this.binaryValues.Count, str1);
          }
          else
          {
            stringBuilder.Append(str2);
            this.binaryValues.Add(this.binaryValues.Count, str2);
          }
          flag = !flag;
        }
      }
      return stringBuilder.ToString();
    }

    private void PopulateVersionData()
    {
      List<bool> positionValue = QRCode.positionValues[this.version];
      this.AddUpperVersionInformation(positionValue);
      this.AddLowerVersionInformation(positionValue);
    }

    private void AddUpperVersionInformation(List<bool> values)
    {
      int index1 = this.sizeOfMatrix - 15;
      int index2 = this.sizeOfData - 11;
      int index3 = 0;
      for (int index4 = 4; index4 < 10; ++index4)
      {
        this.BinaryMatrix[index4, index1] = values[index3];
        this.FilledValuesMatrix[index4 - 4, index2] = true;
        int index5 = index3 + 1;
        this.BinaryMatrix[index4, index1 + 1] = values[index5];
        this.FilledValuesMatrix[index4 - 4, index2 + 1] = true;
        int index6 = index5 + 1;
        this.BinaryMatrix[index4, index1 + 2] = values[index6];
        this.FilledValuesMatrix[index4 - 4, index2 + 2] = true;
        index3 = index6 + 1;
      }
    }

    private void AddLowerVersionInformation(List<bool> values)
    {
      int index1 = this.sizeOfMatrix - 13;
      int index2 = this.sizeOfData - 9;
      int index3 = 0;
      for (int index4 = 4; index4 < 10; ++index4)
      {
        this.BinaryMatrix[index1, index4] = values[index3];
        this.FilledValuesMatrix[index2, index4 - 4] = true;
        int index5 = index3 + 1;
        this.BinaryMatrix[index1 - 1, index4] = values[index5];
        this.FilledValuesMatrix[index2 - 1, index4 - 4] = true;
        int index6 = index5 + 1;
        this.BinaryMatrix[index1 - 2, index4] = values[index6];
        this.FilledValuesMatrix[index2 - 2, index4 - 4] = true;
        index3 = index6 + 1;
      }
    }

    private void SetSingleModule(
      int rowIndex,
      int columnIndex,
      int binaryIndex,
      bool[,] matrix,
      string maskCode)
    {
      if (binaryIndex < this.encodedData.Length)
        QRCode.SetSingleCharacter(rowIndex, columnIndex, this.encodedData[binaryIndex], matrix, maskCode);
      else
        QRCode.SetSingleCharacter(rowIndex, columnIndex, '0', matrix, maskCode);
    }

    private int CalculatePenalty1(bool[,] matrix)
    {
      int num1 = 0;
      for (int index1 = 0; index1 < this.sizeOfData; ++index1)
      {
        int num2 = 1;
        for (int index2 = 1; index2 < this.sizeOfData; ++index2)
        {
          if (matrix[index1 + 4, index2 + 4] == matrix[index1 + 4, index2 + 3])
          {
            ++num2;
            if (num2 == 5)
              num1 += 3;
            else if (num2 > 5)
              ++num1;
          }
          else
            num2 = 1;
        }
      }
      for (int index1 = 0; index1 < this.sizeOfData; ++index1)
      {
        int num2 = 1;
        for (int index2 = 1; index2 < this.sizeOfData; ++index2)
        {
          if (matrix[index2 + 4, index1 + 4] == matrix[index2 + 3, index1 + 4])
          {
            ++num2;
            if (num2 == 5)
              num1 += 3;
            else if (num2 > 5)
              ++num1;
          }
          else
            num2 = 1;
        }
      }
      return num1;
    }

    private int CalculatePenalty2(bool[,] matrix)
    {
      int num = 0;
      for (int index1 = 0; index1 < this.sizeOfData - 1; ++index1)
      {
        for (int index2 = 0; index2 < this.sizeOfData - 1; ++index2)
        {
          bool flag = matrix[index1 + 4, index2 + 4];
          if (flag == matrix[index1 + 4, index2 + 5] && flag == matrix[index1 + 5, index2 + 4] && flag == matrix[index1 + 5, index2 + 5])
            num += 3;
        }
      }
      return num;
    }

    private int CalculatePenalty3(bool[,] matrix)
    {
      int num = 0;
      for (int index1 = 0; index1 < this.sizeOfData; ++index1)
      {
        for (int index2 = 0; index2 <= this.sizeOfData - 7; ++index2)
        {
          if (matrix[index1 + 4, index2 + 4] && !matrix[index1 + 4, index2 + 5] && (matrix[index1 + 4, index2 + 6] && matrix[index1 + 4, index2 + 7]) && (matrix[index1 + 4, index2 + 8] && !matrix[index1 + 4, index2 + 9] && matrix[index1 + 4, index2 + 10]))
            num += 40;
        }
      }
      for (int index1 = 0; index1 < this.sizeOfData; ++index1)
      {
        for (int index2 = 0; index2 <= this.sizeOfData - 7; ++index2)
        {
          if (matrix[index2 + 4, index1 + 4] && !matrix[index2 + 5, index1 + 4] && (matrix[index2 + 6, index1 + 4] && matrix[index2 + 7, index1 + 4]) && (matrix[index2 + 8, index1 + 4] && !matrix[index2 + 9, index1 + 4] && matrix[index2 + 10, index1 + 4]))
            num += 40;
        }
      }
      return num;
    }

    private int CalculatePenalty4(bool[,] matrix)
    {
      double num1 = 0.0;
      double num2 = 0.0;
      for (int index1 = 0; index1 < this.sizeOfData; ++index1)
      {
        for (int index2 = 0; index2 < this.sizeOfData; ++index2)
        {
          if (matrix[index1 + 4, index2 + 4])
            ++num1;
          else
            ++num2;
        }
      }
      return (int) (Math.Floor(Math.Abs(num1 / (num1 + num2) * 100.0 - 50.0)) / 5.0) * 10;
    }

    private void PopulateBinaryMatricesWithData()
    {
      List<bool[,]> flagArrayList = new List<bool[,]>();
      List<int> intList = new List<int>(8);
      for (int index = 0; index < 8; ++index)
      {
        int num1 = 0;
        bool[,] binaryMatrix = this.BinaryMatrix;
        this.PopulateFormatData(QRCode.maskCodes[index]);
        this.PopulateSingleMatrix(binaryMatrix, QRCode.maskCodes[index]);
        int num2 = num1 + this.CalculatePenalty1(binaryMatrix) + this.CalculatePenalty2(binaryMatrix) + this.CalculatePenalty3(binaryMatrix) + this.CalculatePenalty4(binaryMatrix);
        intList.Add(num2);
        flagArrayList.Add(binaryMatrix);
      }
      this.PopulateBinaryValues(this.encodedData);
      int maxValue = int.MaxValue;
      int index1 = 0;
      for (int index2 = 0; index2 < intList.Count; ++index2)
      {
        if (maxValue < intList[index2])
        {
          maxValue = intList[index2];
          index1 = index2;
        }
      }
      this.PopulateFormatData(QRCode.maskCodes[index1]);
      this.PopulateSingleMatrix(this.BinaryMatrix, QRCode.maskCodes[index1]);
    }

    private void PopulateSingleMatrix(bool[,] matrix, string maskCode)
    {
      int binaryIndex = 0;
      bool flag = true;
      for (int columnIndex = this.sizeOfData - 1; columnIndex >= 0; columnIndex -= 2)
      {
        if (columnIndex == 6)
          --columnIndex;
        if (flag)
        {
          for (int rowIndex = this.sizeOfData - 1; rowIndex >= 0; --rowIndex)
          {
            if (!this.FilledValuesMatrix[rowIndex, columnIndex])
            {
              this.SetSingleModule(rowIndex, columnIndex, binaryIndex, matrix, maskCode);
              ++binaryIndex;
            }
            if (columnIndex - 1 >= 0 && !this.FilledValuesMatrix[rowIndex, columnIndex - 1])
            {
              this.SetSingleModule(rowIndex, columnIndex - 1, binaryIndex, matrix, maskCode);
              ++binaryIndex;
            }
          }
          flag = !flag;
        }
        else
        {
          for (int rowIndex = 0; rowIndex < this.sizeOfData; ++rowIndex)
          {
            if (!this.FilledValuesMatrix[rowIndex, columnIndex])
            {
              this.SetSingleModule(rowIndex, columnIndex, binaryIndex, matrix, maskCode);
              ++binaryIndex;
            }
            if (columnIndex - 1 >= 0 && !this.FilledValuesMatrix[rowIndex, columnIndex - 1])
            {
              this.SetSingleModule(rowIndex, columnIndex - 1, binaryIndex, matrix, maskCode);
              ++binaryIndex;
            }
          }
          flag = !flag;
        }
      }
    }

    private void PopulateFormatData(string maskCode)
    {
      this.PopulateBinaryMatrix(QRCode.errorCorrectionToMask[this.errorCorrectionLevel] + maskCode, this.sizeOfMatrix - 5, this.sizeOfMatrix - 5);
      this.PopulateFilledValuesMatrix();
    }

    private void PopulateFilledValuesMatrix()
    {
      this.FilledValuesMatrix[8, 0] = true;
      this.FilledValuesMatrix[8, 1] = true;
      this.FilledValuesMatrix[8, 2] = true;
      this.FilledValuesMatrix[8, 3] = true;
      this.FilledValuesMatrix[8, 4] = true;
      this.FilledValuesMatrix[8, 5] = true;
      this.FilledValuesMatrix[8, 7] = true;
      this.FilledValuesMatrix[8, 8] = true;
      this.FilledValuesMatrix[7, 8] = true;
      this.FilledValuesMatrix[5, 8] = true;
      this.FilledValuesMatrix[4, 8] = true;
      this.FilledValuesMatrix[3, 8] = true;
      this.FilledValuesMatrix[2, 8] = true;
      this.FilledValuesMatrix[1, 8] = true;
      this.FilledValuesMatrix[0, 8] = true;
      this.FilledValuesMatrix[this.sizeOfData - 1, 8] = true;
      this.FilledValuesMatrix[this.sizeOfData - 2, 8] = true;
      this.FilledValuesMatrix[this.sizeOfData - 3, 8] = true;
      this.FilledValuesMatrix[this.sizeOfData - 4, 8] = true;
      this.FilledValuesMatrix[this.sizeOfData - 5, 8] = true;
      this.FilledValuesMatrix[this.sizeOfData - 6, 8] = true;
      this.FilledValuesMatrix[this.sizeOfData - 7, 8] = true;
      this.FilledValuesMatrix[8, this.sizeOfData - 8] = true;
      this.FilledValuesMatrix[8, this.sizeOfData - 7] = true;
      this.FilledValuesMatrix[8, this.sizeOfData - 6] = true;
      this.FilledValuesMatrix[8, this.sizeOfData - 5] = true;
      this.FilledValuesMatrix[8, this.sizeOfData - 4] = true;
      this.FilledValuesMatrix[8, this.sizeOfData - 3] = true;
      this.FilledValuesMatrix[8, this.sizeOfData - 2] = true;
      this.FilledValuesMatrix[8, this.sizeOfData - 1] = true;
    }

    private void PopulateBinaryMatrix(string mask, int lastDataRowIndex, int lastDataColumnIndex)
    {
      this.PopulateBinaryMatrixBeginning(mask);
      this.PopulateBinaryMatrixEnd(mask, lastDataRowIndex, lastDataColumnIndex);
    }

    private void PopulateBinaryMatrixEnd(
      string mask,
      int lastDataRowIndex,
      int lastDataColumnIndex)
    {
      string str = QRCode.formatInformation[mask];
      this.BinaryMatrix[lastDataRowIndex, 12] = !(str[0].ToString() == "0");
      this.BinaryMatrix[lastDataRowIndex - 1, 12] = !(str[1].ToString() == "0");
      this.BinaryMatrix[lastDataRowIndex - 2, 12] = !(str[2].ToString() == "0");
      this.BinaryMatrix[lastDataRowIndex - 3, 12] = !(str[3].ToString() == "0");
      this.BinaryMatrix[lastDataRowIndex - 4, 12] = !(str[4].ToString() == "0");
      this.BinaryMatrix[lastDataRowIndex - 5, 12] = !(str[5].ToString() == "0");
      this.BinaryMatrix[lastDataRowIndex - 6, 12] = !(str[6].ToString() == "0");
      this.BinaryMatrix[12, lastDataColumnIndex - 7] = !(str[7].ToString() == "0");
      this.BinaryMatrix[12, lastDataColumnIndex - 6] = !(str[8].ToString() == "0");
      this.BinaryMatrix[12, lastDataColumnIndex - 5] = !(str[9].ToString() == "0");
      this.BinaryMatrix[12, lastDataColumnIndex - 4] = !(str[10].ToString() == "0");
      this.BinaryMatrix[12, lastDataColumnIndex - 3] = !(str[11].ToString() == "0");
      this.BinaryMatrix[12, lastDataColumnIndex - 2] = !(str[12].ToString() == "0");
      this.BinaryMatrix[12, lastDataColumnIndex - 1] = !(str[13].ToString() == "0");
      this.BinaryMatrix[12, lastDataColumnIndex] = !(str[14].ToString() == "0");
    }

    private void PopulateBinaryMatrixBeginning(string mask)
    {
      string str = QRCode.formatInformation[mask];
      this.BinaryMatrix[12, 4] = !(str[0].ToString() == "0");
      this.BinaryMatrix[12, 5] = !(str[1].ToString() == "0");
      this.BinaryMatrix[12, 6] = !(str[2].ToString() == "0");
      this.BinaryMatrix[12, 7] = !(str[3].ToString() == "0");
      this.BinaryMatrix[12, 8] = !(str[4].ToString() == "0");
      this.BinaryMatrix[12, 9] = !(str[5].ToString() == "0");
      this.BinaryMatrix[12, 11] = !(str[6].ToString() == "0");
      this.BinaryMatrix[12, 12] = !(str[7].ToString() == "0");
      this.BinaryMatrix[11, 12] = !(str[8].ToString() == "0");
      this.BinaryMatrix[9, 12] = !(str[9].ToString() == "0");
      this.BinaryMatrix[8, 12] = !(str[10].ToString() == "0");
      this.BinaryMatrix[7, 12] = !(str[11].ToString() == "0");
      this.BinaryMatrix[6, 12] = !(str[12].ToString() == "0");
      this.BinaryMatrix[5, 12] = !(str[13].ToString() == "0");
      this.BinaryMatrix[4, 12] = !(str[14].ToString() == "0");
    }

    private void PopulateValueMatrix()
    {
      this.binaryMatrix = new bool[this.sizeOfMatrix, this.sizeOfMatrix];
      this.filledValuesMatrix = new bool[this.sizeOfData, this.sizeOfData];
      this.PopulateFinderPattern(4, 4);
      this.PopulateFinderPatternFilledValues(0, 0);
      this.PopulateFinderPattern(4, this.sizeOfMatrix - 11);
      this.PopulateFinderPatternFilledValues(0, this.sizeOfData - 8);
      this.PopulateFinderPattern(this.sizeOfMatrix - 11, 4);
      this.PopulateFinderPatternFilledValues(this.sizeOfData - 8, 0);
      this.AddTimingPattern();
      this.AddSinglePixel(this.sizeOfMatrix - 12, 12);
      if (this.version <= 1)
        return;
      this.AddPositionAdjustmentPatterns(this.sizeOfMatrix);
    }

    private void AddPositionAdjustmentPatterns(int currentSizeOfMatrix)
    {
      List<int> patternCoordinate = QRCode.positionAdjustmentPatternCoordinates[this.version];
      for (int index1 = 0; index1 < patternCoordinate.Count; ++index1)
      {
        for (int index2 = 0; index2 < patternCoordinate.Count; ++index2)
        {
          int row = patternCoordinate[index1] + 4;
          int column = patternCoordinate[index2] + 4;
          if ((row != 10 || column != 10) && (row != 10 || column != currentSizeOfMatrix - 11) && (row != currentSizeOfMatrix - 11 || column != 10))
            this.AddSingleAdjustmentPattern(row, column);
        }
      }
    }

    private void AddSingleAdjustmentPattern(int row, int column)
    {
      this.BinaryMatrix[row - 2, column - 2] = true;
      this.FilledValuesMatrix[row - 6, column - 6] = true;
      this.BinaryMatrix[row - 2, column - 1] = true;
      this.FilledValuesMatrix[row - 6, column - 5] = true;
      this.BinaryMatrix[row - 2, column] = true;
      this.FilledValuesMatrix[row - 6, column - 4] = true;
      this.BinaryMatrix[row - 2, column + 1] = true;
      this.FilledValuesMatrix[row - 6, column - 3] = true;
      this.BinaryMatrix[row - 2, column + 2] = true;
      this.FilledValuesMatrix[row - 6, column - 2] = true;
      this.BinaryMatrix[row - 1, column - 2] = true;
      this.FilledValuesMatrix[row - 5, column - 6] = true;
      this.BinaryMatrix[row - 1, column - 1] = false;
      this.FilledValuesMatrix[row - 5, column - 5] = true;
      this.BinaryMatrix[row - 1, column] = false;
      this.FilledValuesMatrix[row - 5, column - 4] = true;
      this.BinaryMatrix[row - 1, column + 1] = false;
      this.FilledValuesMatrix[row - 5, column - 3] = true;
      this.BinaryMatrix[row - 1, column + 2] = true;
      this.FilledValuesMatrix[row - 5, column - 2] = true;
      this.BinaryMatrix[row, column - 2] = true;
      this.FilledValuesMatrix[row - 4, column - 6] = true;
      this.BinaryMatrix[row, column - 1] = false;
      this.FilledValuesMatrix[row - 4, column - 5] = true;
      this.BinaryMatrix[row, column] = true;
      this.FilledValuesMatrix[row - 4, column - 4] = true;
      this.BinaryMatrix[row, column + 1] = false;
      this.FilledValuesMatrix[row - 4, column - 3] = true;
      this.BinaryMatrix[row, column + 2] = true;
      this.FilledValuesMatrix[row - 4, column - 2] = true;
      this.BinaryMatrix[row + 1, column - 2] = true;
      this.FilledValuesMatrix[row - 3, column - 6] = true;
      this.BinaryMatrix[row + 1, column - 1] = false;
      this.FilledValuesMatrix[row - 3, column - 5] = true;
      this.BinaryMatrix[row + 1, column] = false;
      this.FilledValuesMatrix[row - 3, column - 4] = true;
      this.BinaryMatrix[row + 1, column + 1] = false;
      this.FilledValuesMatrix[row - 3, column - 3] = true;
      this.BinaryMatrix[row + 1, column + 2] = true;
      this.FilledValuesMatrix[row - 3, column - 2] = true;
      this.BinaryMatrix[row + 2, column - 2] = true;
      this.FilledValuesMatrix[row - 2, column - 6] = true;
      this.BinaryMatrix[row + 2, column - 1] = true;
      this.FilledValuesMatrix[row - 2, column - 5] = true;
      this.BinaryMatrix[row + 2, column] = true;
      this.FilledValuesMatrix[row - 2, column - 4] = true;
      this.BinaryMatrix[row + 2, column + 1] = true;
      this.FilledValuesMatrix[row - 2, column - 3] = true;
      this.BinaryMatrix[row + 2, column + 2] = true;
      this.FilledValuesMatrix[row - 2, column - 2] = true;
    }

    private void AddSinglePixel(int row, int column)
    {
      this.BinaryMatrix[row, column] = true;
      this.FilledValuesMatrix[row - 4, column - 4] = true;
    }

    private void PopulateFinderPatternFilledValues(int startRow, int startColumn)
    {
      int num1;
      int num2;
      if (startRow == 0 && startColumn == 0)
        num2 = num1 = 8;
      else if (startRow == 0 && startColumn > 0)
      {
        num2 = 8;
        num1 = startColumn + 8;
      }
      else
      {
        num1 = 8;
        num2 = startRow + 8;
      }
      for (int index1 = startRow; index1 < num2; ++index1)
      {
        for (int index2 = startColumn; index2 < num1; ++index2)
          this.FilledValuesMatrix[index1, index2] = true;
      }
    }

    private void PopulateFinderPattern(int initialRowIndex, int initialColumnIndex)
    {
      int index1 = initialRowIndex;
      int index2 = initialColumnIndex;
      for (int index3 = 0; index3 < 7; ++index3)
      {
        this.BinaryMatrix[index1 + index3, index2] = true;
        this.BinaryMatrix[index1, index2 + index3] = true;
        this.BinaryMatrix[index1 + index3, index2 + 6] = true;
        this.BinaryMatrix[index1 + 6, index2 + index3] = true;
      }
      int num = index1 + 2;
      int index4 = index2 + 2;
      for (int index3 = 0; index3 < 3; ++index3)
      {
        this.BinaryMatrix[num + index3, index4] = true;
        this.BinaryMatrix[num + index3, index4 + 1] = true;
        this.BinaryMatrix[num + index3, index4 + 2] = true;
      }
    }

    private void AddTimingPattern()
    {
      int index1 = 10;
      int index2 = 12;
      bool flag1 = true;
      for (; index2 < this.VersionDimension - 4; ++index2)
      {
        if (flag1)
          this.BinaryMatrix[index1, index2] = true;
        this.FilledValuesMatrix[index1 - 4, index2 - 4] = true;
        flag1 = !flag1;
      }
      int index3 = 10;
      int index4 = 12;
      bool flag2 = true;
      for (; index4 < this.VersionDimension - 4; ++index4)
      {
        if (flag2)
          this.BinaryMatrix[index4, index3] = true;
        this.FilledValuesMatrix[index4 - 4, index3 - 4] = true;
        flag2 = !flag2;
      }
    }

    private int DetermineCountLength()
    {
      return this.DetermineCountLength(this.version);
    }

    private int DetermineCountLength(int codeVersion)
    {
      if (codeVersion >= 1 && codeVersion <= 9)
      {
        if (this.codeMode == CodeMode.Numeric)
          return 10;
        return this.codeMode == CodeMode.Alphanumeric ? 9 : 8;
      }
      if (codeVersion >= 10 && codeVersion <= 26)
      {
        if (this.codeMode == CodeMode.Numeric)
          return 12;
        if (this.codeMode == CodeMode.Alphanumeric)
          return 11;
        return this.codeMode == CodeMode.Byte ? 16 : 10;
      }
      if (this.codeMode == CodeMode.Numeric)
        return 14;
      if (this.codeMode == CodeMode.Alphanumeric)
        return 13;
      return this.codeMode == CodeMode.Byte ? 16 : 12;
    }

    public void CreateElements(IElementFactory factory, Rectangle bounds)
    {
      int length1 = this.BinaryMatrix.GetLength(0);
      int length2 = this.BinaryMatrix.GetLength(1);
      int left = bounds.Left;
      int top = bounds.Top;
      int num = Math.Min(bounds.Width / this.BinaryMatrix.GetLength(1), bounds.Height / this.BinaryMatrix.GetLength(0));
      List<Rectangle> rectangleList = new List<Rectangle>();
      for (int index1 = 0; index1 < length1; ++index1)
      {
        for (int index2 = 0; index2 < length2; ++index2)
        {
          if (this.BinaryMatrix[index1, index2])
          {
            Rectangle rectangle = new Rectangle(left + index2 * num, top + index1 * num, num, num);
            rectangleList.Add(rectangle);
          }
        }
      }
      factory.ClearElements();
      foreach (Rectangle rectangle in rectangleList)
      {
        RectangleF rect = (RectangleF) rectangle;
        factory.CreateBarElement(rect);
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    static QRCode()
    {
      QRCode.PopulatePositionValues();
      QRCode.PopulateDataCapacityTable();
      QRCode.PopulateCodeModeValues();
      QRCode.PopulateCodeWordsLengthTable();
      QRCode.PopulateExponentsOfAlphaToValues();
      QRCode.PopulateValuesOfExponentsOfAlpha();
      QRCode.PopulateGeneratorExponentsOfAlpha();
      QRCode.PopulatePositionAdjustmentTable();
      QRCode.PopulateErrorCorrectionToMask();
      QRCode.PopulateFormatInformation();
    }

    private static void PopulatePositionValues()
    {
      QRCode.positionValues.Add(7, QRCode.version7);
      QRCode.positionValues.Add(8, QRCode.version8);
      QRCode.positionValues.Add(9, QRCode.version9);
      QRCode.positionValues.Add(10, QRCode.version10);
      QRCode.positionValues.Add(11, QRCode.version11);
      QRCode.positionValues.Add(12, QRCode.version12);
      QRCode.positionValues.Add(13, QRCode.version13);
      QRCode.positionValues.Add(14, QRCode.version14);
      QRCode.positionValues.Add(15, QRCode.version15);
      QRCode.positionValues.Add(16, QRCode.version16);
      QRCode.positionValues.Add(17, QRCode.version17);
      QRCode.positionValues.Add(18, QRCode.version18);
      QRCode.positionValues.Add(19, QRCode.version19);
      QRCode.positionValues.Add(20, QRCode.version20);
      QRCode.positionValues.Add(21, QRCode.version21);
      QRCode.positionValues.Add(22, QRCode.version22);
      QRCode.positionValues.Add(23, QRCode.version23);
      QRCode.positionValues.Add(24, QRCode.version24);
      QRCode.positionValues.Add(25, QRCode.version25);
      QRCode.positionValues.Add(26, QRCode.version26);
      QRCode.positionValues.Add(27, QRCode.version27);
      QRCode.positionValues.Add(28, QRCode.version28);
      QRCode.positionValues.Add(29, QRCode.version29);
      QRCode.positionValues.Add(30, QRCode.version30);
      QRCode.positionValues.Add(31, QRCode.version31);
      QRCode.positionValues.Add(32, QRCode.version32);
      QRCode.positionValues.Add(33, QRCode.version33);
      QRCode.positionValues.Add(34, QRCode.version34);
      QRCode.positionValues.Add(35, QRCode.version35);
      QRCode.positionValues.Add(36, QRCode.version36);
      QRCode.positionValues.Add(37, QRCode.version37);
      QRCode.positionValues.Add(38, QRCode.version38);
      QRCode.positionValues.Add(39, QRCode.version39);
      QRCode.positionValues.Add(40, QRCode.version40);
    }

    private static void PopulateDataCapacityTable()
    {
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(1, ErrorCorrectionLevel.L), 152);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(1, ErrorCorrectionLevel.M), 128);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(1, ErrorCorrectionLevel.Q), 104);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(1, ErrorCorrectionLevel.H), 72);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(2, ErrorCorrectionLevel.L), 272);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(2, ErrorCorrectionLevel.M), 224);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(2, ErrorCorrectionLevel.Q), 176);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(2, ErrorCorrectionLevel.H), 128);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(3, ErrorCorrectionLevel.L), 440);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(3, ErrorCorrectionLevel.M), 352);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(3, ErrorCorrectionLevel.Q), 272);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(3, ErrorCorrectionLevel.H), 208);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(4, ErrorCorrectionLevel.L), 640);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(4, ErrorCorrectionLevel.M), 512);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(4, ErrorCorrectionLevel.Q), 384);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(4, ErrorCorrectionLevel.H), 288);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(5, ErrorCorrectionLevel.L), 864);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(5, ErrorCorrectionLevel.M), 688);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(5, ErrorCorrectionLevel.Q), 496);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(5, ErrorCorrectionLevel.H), 368);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(6, ErrorCorrectionLevel.L), 1088);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(6, ErrorCorrectionLevel.M), 864);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(6, ErrorCorrectionLevel.Q), 608);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(6, ErrorCorrectionLevel.H), 480);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(7, ErrorCorrectionLevel.L), 1248);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(7, ErrorCorrectionLevel.M), 992);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(7, ErrorCorrectionLevel.Q), 704);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(7, ErrorCorrectionLevel.H), 528);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(8, ErrorCorrectionLevel.L), 1552);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(8, ErrorCorrectionLevel.M), 1232);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(8, ErrorCorrectionLevel.Q), 880);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(8, ErrorCorrectionLevel.H), 688);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(9, ErrorCorrectionLevel.L), 1856);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(9, ErrorCorrectionLevel.M), 1456);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(9, ErrorCorrectionLevel.Q), 1056);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(9, ErrorCorrectionLevel.H), 800);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(10, ErrorCorrectionLevel.L), 2192);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(10, ErrorCorrectionLevel.M), 1728);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(10, ErrorCorrectionLevel.Q), 1232);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(10, ErrorCorrectionLevel.H), 976);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(11, ErrorCorrectionLevel.L), 2592);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(11, ErrorCorrectionLevel.M), 2032);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(11, ErrorCorrectionLevel.Q), 1440);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(11, ErrorCorrectionLevel.H), 1120);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(12, ErrorCorrectionLevel.L), 2960);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(12, ErrorCorrectionLevel.M), 2320);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(12, ErrorCorrectionLevel.Q), 1648);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(12, ErrorCorrectionLevel.H), 1264);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(13, ErrorCorrectionLevel.L), 3424);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(13, ErrorCorrectionLevel.M), 2672);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(13, ErrorCorrectionLevel.Q), 1952);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(13, ErrorCorrectionLevel.H), 1440);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(14, ErrorCorrectionLevel.L), 3688);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(14, ErrorCorrectionLevel.M), 2920);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(14, ErrorCorrectionLevel.Q), 2088);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(14, ErrorCorrectionLevel.H), 1576);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(15, ErrorCorrectionLevel.L), 4184);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(15, ErrorCorrectionLevel.M), 3320);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(15, ErrorCorrectionLevel.Q), 2360);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(15, ErrorCorrectionLevel.H), 1784);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(16, ErrorCorrectionLevel.L), 4712);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(16, ErrorCorrectionLevel.M), 3624);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(16, ErrorCorrectionLevel.Q), 2600);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(16, ErrorCorrectionLevel.H), 2024);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(17, ErrorCorrectionLevel.L), 5176);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(17, ErrorCorrectionLevel.M), 4056);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(17, ErrorCorrectionLevel.Q), 2936);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(17, ErrorCorrectionLevel.H), 2264);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(18, ErrorCorrectionLevel.L), 5768);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(18, ErrorCorrectionLevel.M), 4504);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(18, ErrorCorrectionLevel.Q), 3176);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(18, ErrorCorrectionLevel.H), 2504);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(19, ErrorCorrectionLevel.L), 6360);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(19, ErrorCorrectionLevel.M), 5016);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(19, ErrorCorrectionLevel.Q), 3560);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(19, ErrorCorrectionLevel.H), 2728);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(20, ErrorCorrectionLevel.L), 6888);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(20, ErrorCorrectionLevel.M), 5352);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(20, ErrorCorrectionLevel.Q), 3880);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(20, ErrorCorrectionLevel.H), 3080);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(21, ErrorCorrectionLevel.L), 7456);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(21, ErrorCorrectionLevel.M), 5712);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(21, ErrorCorrectionLevel.Q), 4096);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(21, ErrorCorrectionLevel.H), 3248);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(22, ErrorCorrectionLevel.L), 8048);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(22, ErrorCorrectionLevel.M), 6256);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(22, ErrorCorrectionLevel.Q), 4544);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(22, ErrorCorrectionLevel.H), 3536);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(23, ErrorCorrectionLevel.L), 8752);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(23, ErrorCorrectionLevel.M), 6880);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(23, ErrorCorrectionLevel.Q), 4912);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(23, ErrorCorrectionLevel.H), 3712);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(24, ErrorCorrectionLevel.L), 9392);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(24, ErrorCorrectionLevel.M), 7312);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(24, ErrorCorrectionLevel.Q), 5312);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(24, ErrorCorrectionLevel.H), 4112);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(25, ErrorCorrectionLevel.L), 10208);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(25, ErrorCorrectionLevel.M), 8000);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(25, ErrorCorrectionLevel.Q), 5744);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(25, ErrorCorrectionLevel.H), 4304);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(26, ErrorCorrectionLevel.L), 10960);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(26, ErrorCorrectionLevel.M), 8496);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(26, ErrorCorrectionLevel.Q), 6032);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(26, ErrorCorrectionLevel.H), 4768);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(27, ErrorCorrectionLevel.L), 11744);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(27, ErrorCorrectionLevel.M), 9024);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(27, ErrorCorrectionLevel.Q), 6464);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(27, ErrorCorrectionLevel.H), 5024);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(28, ErrorCorrectionLevel.L), 12248);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(28, ErrorCorrectionLevel.M), 9544);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(28, ErrorCorrectionLevel.Q), 6968);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(28, ErrorCorrectionLevel.H), 5288);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(29, ErrorCorrectionLevel.L), 13048);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(29, ErrorCorrectionLevel.M), 10136);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(29, ErrorCorrectionLevel.Q), 7288);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(29, ErrorCorrectionLevel.H), 5608);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(30, ErrorCorrectionLevel.L), 13880);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(30, ErrorCorrectionLevel.M), 10984);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(30, ErrorCorrectionLevel.Q), 7880);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(30, ErrorCorrectionLevel.H), 5960);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(31, ErrorCorrectionLevel.L), 14744);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(31, ErrorCorrectionLevel.M), 11640);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(31, ErrorCorrectionLevel.Q), 8264);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(31, ErrorCorrectionLevel.H), 6344);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(32, ErrorCorrectionLevel.L), 15640);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(32, ErrorCorrectionLevel.M), 12328);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(32, ErrorCorrectionLevel.Q), 8920);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(32, ErrorCorrectionLevel.H), 6760);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(33, ErrorCorrectionLevel.L), 16568);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(33, ErrorCorrectionLevel.M), 13048);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(33, ErrorCorrectionLevel.Q), 9368);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(33, ErrorCorrectionLevel.H), 7208);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(34, ErrorCorrectionLevel.L), 17528);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(34, ErrorCorrectionLevel.M), 13800);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(34, ErrorCorrectionLevel.Q), 9848);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(34, ErrorCorrectionLevel.H), 7688);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(35, ErrorCorrectionLevel.L), 18448);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(35, ErrorCorrectionLevel.M), 14496);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(35, ErrorCorrectionLevel.Q), 10288);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(35, ErrorCorrectionLevel.H), 7888);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(36, ErrorCorrectionLevel.L), 19472);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(36, ErrorCorrectionLevel.M), 15312);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(36, ErrorCorrectionLevel.Q), 10832);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(36, ErrorCorrectionLevel.H), 8432);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(37, ErrorCorrectionLevel.L), 20528);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(37, ErrorCorrectionLevel.M), 15936);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(37, ErrorCorrectionLevel.Q), 11408);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(37, ErrorCorrectionLevel.H), 8768);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(38, ErrorCorrectionLevel.L), 21616);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(38, ErrorCorrectionLevel.M), 16816);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(38, ErrorCorrectionLevel.Q), 12016);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(38, ErrorCorrectionLevel.H), 9136);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(39, ErrorCorrectionLevel.L), 22496);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(39, ErrorCorrectionLevel.M), 17728);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(39, ErrorCorrectionLevel.Q), 12656);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(39, ErrorCorrectionLevel.H), 9776);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(40, ErrorCorrectionLevel.L), 23648);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(40, ErrorCorrectionLevel.M), 18672);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(40, ErrorCorrectionLevel.Q), 13328);
      QRCode.dataCapacityTable.Add(new DataCapacityIndexer(40, ErrorCorrectionLevel.H), 10208);
    }

    private static void PopulateCodeModeValues()
    {
      QRCode.codeModeValues.Add(CodeMode.Numeric, "0001");
      QRCode.codeModeValues.Add(CodeMode.Alphanumeric, "0010");
      QRCode.codeModeValues.Add(CodeMode.Byte, "0100");
      QRCode.codeModeValues.Add(CodeMode.Kanji, "1000");
    }

    private static void PopulateCodeWordsLengthTable()
    {
      List<string> stringList = new List<string>() { "L", "M", "Q", "H" };
      List<int> intList = new List<int>() { 7, 1, 19, 0, 0, 10, 1, 16, 0, 0, 13, 1, 13, 0, 0, 17, 1, 9, 0, 0, 10, 1, 34, 0, 0, 16, 1, 28, 0, 0, 22, 1, 22, 0, 0, 28, 1, 16, 0, 0, 15, 1, 55, 0, 0, 26, 1, 44, 0, 0, 18, 2, 17, 0, 0, 22, 2, 13, 0, 0, 20, 1, 80, 0, 0, 18, 2, 32, 0, 0, 26, 2, 24, 0, 0, 16, 4, 9, 0, 0, 26, 1, 108, 0, 0, 24, 2, 43, 0, 0, 18, 2, 15, 2, 16, 22, 2, 11, 2, 12, 18, 2, 68, 0, 0, 16, 4, 27, 0, 0, 24, 4, 19, 0, 0, 28, 4, 15, 0, 0, 20, 2, 78, 0, 0, 18, 4, 31, 0, 0, 18, 2, 14, 4, 15, 26, 4, 13, 1, 14, 24, 2, 97, 0, 0, 22, 2, 38, 2, 39, 22, 4, 18, 2, 19, 26, 4, 14, 2, 15, 30, 2, 116, 0, 0, 22, 3, 36, 2, 37, 20, 4, 16, 4, 17, 24, 4, 12, 4, 13, 18, 2, 68, 2, 69, 26, 4, 43, 1, 44, 24, 6, 19, 2, 20, 28, 6, 15, 2, 16, 20, 4, 81, 0, 0, 30, 1, 50, 4, 51, 28, 4, 22, 4, 23, 24, 3, 12, 8, 13, 24, 2, 92, 2, 93, 22, 6, 36, 2, 37, 26, 4, 20, 6, 21, 28, 7, 14, 4, 15, 26, 4, 107, 0, 0, 22, 8, 37, 1, 38, 24, 8, 20, 4, 21, 22, 12, 11, 4, 12, 30, 3, 115, 1, 116, 24, 4, 40, 5, 41, 20, 11, 16, 5, 17, 24, 11, 12, 5, 13, 22, 5, 87, 1, 88, 24, 5, 41, 5, 42, 30, 5, 24, 7, 25, 24, 11, 12, 7, 13, 24, 5, 98, 1, 99, 28, 7, 45, 3, 46, 24, 15, 19, 2, 20, 30, 3, 15, 13, 16, 28, 1, 107, 5, 108, 28, 10, 46, 1, 47, 28, 1, 22, 15, 23, 28, 2, 14, 17, 15, 30, 5, 120, 1, 121, 26, 9, 43, 4, 44, 28, 17, 22, 1, 23, 28, 2, 14, 19, 15, 28, 3, 113, 4, 114, 26, 3, 44, 11, 45, 26, 17, 21, 4, 22, 26, 9, 13, 16, 14, 28, 3, 107, 5, 108, 26, 3, 41, 13, 42, 30, 15, 24, 5, 25, 28, 15, 15, 10, 16, 28, 4, 116, 4, 117, 26, 17, 42, 0, 0, 28, 17, 22, 6, 23, 30, 19, 16, 6, 17, 28, 2, 111, 7, 112, 28, 17, 46, 0, 0, 30, 7, 24, 16, 25, 24, 34, 13, 0, 0, 30, 4, 121, 5, 122, 28, 4, 47, 14, 48, 30, 11, 24, 14, 25, 30, 16, 15, 14, 16, 30, 6, 117, 4, 118, 28, 6, 45, 14, 46, 30, 11, 24, 16, 25, 30, 30, 16, 2, 17, 26, 8, 106, 4, 107, 28, 8, 47, 13, 48, 30, 7, 24, 22, 25, 30, 22, 15, 13, 16, 28, 10, 114, 2, 115, 28, 19, 46, 4, 47, 28, 28, 22, 6, 23, 30, 33, 16, 4, 17, 30, 8, 122, 4, 123, 28, 22, 45, 3, 46, 30, 8, 23, 26, 24, 30, 12, 15, 28, 16, 30, 3, 117, 10, 118, 28, 3, 45, 23, 46, 30, 4, 24, 31, 25, 30, 11, 15, 31, 16, 30, 7, 116, 7, 117, 28, 21, 45, 7, 46, 30, 1, 23, 37, 24, 30, 19, 15, 26, 16, 30, 5, 115, 10, 116, 28, 19, 47, 10, 48, 30, 15, 24, 25, 25, 30, 23, 15, 25, 16, 30, 13, 115, 3, 116, 28, 2, 46, 29, 47, 30, 42, 24, 1, 25, 30, 23, 15, 28, 16, 30, 17, 115, 0, 0, 28, 10, 46, 23, 47, 30, 10, 24, 35, 25, 30, 19, 15, 35, 16, 30, 17, 115, 1, 116, 28, 14, 46, 21, 47, 30, 29, 24, 19, 25, 30, 11, 15, 46, 16, 30, 13, 115, 6, 116, 28, 14, 46, 23, 47, 30, 44, 24, 7, 25, 30, 59, 16, 1, 17, 30, 12, 121, 7, 122, 28, 12, 47, 26, 48, 30, 39, 24, 14, 25, 30, 22, 15, 41, 16, 30, 6, 121, 14, 122, 28, 6, 47, 34, 48, 30, 46, 24, 10, 25, 30, 2, 15, 64, 16, 30, 17, 122, 4, 123, 28, 29, 46, 14, 47, 30, 49, 24, 10, 25, 30, 24, 15, 46, 16, 30, 4, 122, 18, 123, 28, 13, 46, 32, 47, 30, 48, 24, 14, 25, 30, 42, 15, 32, 16, 30, 20, 117, 4, 118, 28, 40, 47, 7, 48, 30, 43, 24, 22, 25, 30, 10, 15, 67, 16, 30, 19, 118, 6, 119, 28, 18, 47, 31, 48, 30, 34, 24, 34, 25, 30, 20, 15, 61, 16 };
      int index1 = 0;
      for (int index2 = 1; index2 <= 40; ++index2)
      {
        for (int index3 = 0; index3 < stringList.Count; ++index3)
        {
          QRCode.codeWordsLengthTable.Add(index2.ToString() + stringList[index3], new CodeWordsBlockInfo(intList[index1], intList[index1 + 1], intList[index1 + 2], intList[index1 + 3], intList[index1 + 4]));
          index1 += 5;
        }
      }
    }

    private static void PopulateExponentsOfAlphaToValues()
    {
      QRCode.exponentsOfAlphaToValues = new List<int>()
      {
        1,
        2,
        4,
        8,
        16,
        32,
        64,
        128,
        29,
        58,
        116,
        232,
        205,
        135,
        19,
        38,
        76,
        152,
        45,
        90,
        180,
        117,
        234,
        201,
        143,
        3,
        6,
        12,
        24,
        48,
        96,
        192,
        157,
        39,
        78,
        156,
        37,
        74,
        148,
        53,
        106,
        212,
        181,
        119,
        238,
        193,
        159,
        35,
        70,
        140,
        5,
        10,
        20,
        40,
        80,
        160,
        93,
        186,
        105,
        210,
        185,
        111,
        222,
        161,
        95,
        190,
        97,
        194,
        153,
        47,
        94,
        188,
        101,
        202,
        137,
        15,
        30,
        60,
        120,
        240,
        253,
        231,
        211,
        187,
        107,
        214,
        177,
        (int) sbyte.MaxValue,
        254,
        225,
        223,
        163,
        91,
        182,
        113,
        226,
        217,
        175,
        67,
        134,
        17,
        34,
        68,
        136,
        13,
        26,
        52,
        104,
        208,
        189,
        103,
        206,
        129,
        31,
        62,
        124,
        248,
        237,
        199,
        147,
        59,
        118,
        236,
        197,
        151,
        51,
        102,
        204,
        133,
        23,
        46,
        92,
        184,
        109,
        218,
        169,
        79,
        158,
        33,
        66,
        132,
        21,
        42,
        84,
        168,
        77,
        154,
        41,
        82,
        164,
        85,
        170,
        73,
        146,
        57,
        114,
        228,
        213,
        183,
        115,
        230,
        209,
        191,
        99,
        198,
        145,
        63,
        126,
        252,
        229,
        215,
        179,
        123,
        246,
        241,
        (int) byte.MaxValue,
        227,
        219,
        171,
        75,
        150,
        49,
        98,
        196,
        149,
        55,
        110,
        220,
        165,
        87,
        174,
        65,
        130,
        25,
        50,
        100,
        200,
        141,
        7,
        14,
        28,
        56,
        112,
        224,
        221,
        167,
        83,
        166,
        81,
        162,
        89,
        178,
        121,
        242,
        249,
        239,
        195,
        155,
        43,
        86,
        172,
        69,
        138,
        9,
        18,
        36,
        72,
        144,
        61,
        122,
        244,
        245,
        247,
        243,
        251,
        235,
        203,
        139,
        11,
        22,
        44,
        88,
        176,
        125,
        250,
        233,
        207,
        131,
        27,
        54,
        108,
        216,
        173,
        71,
        142,
        1
      };
    }

    private static void PopulateValuesOfExponentsOfAlpha()
    {
      QRCode.valuesToExponentsOfAlpha = new List<int>()
      {
        -1,
        0,
        1,
        25,
        2,
        50,
        26,
        198,
        3,
        223,
        51,
        238,
        27,
        104,
        199,
        75,
        4,
        100,
        224,
        14,
        52,
        141,
        239,
        129,
        28,
        193,
        105,
        248,
        200,
        8,
        76,
        113,
        5,
        138,
        101,
        47,
        225,
        36,
        15,
        33,
        53,
        147,
        142,
        218,
        240,
        18,
        130,
        69,
        29,
        181,
        194,
        125,
        106,
        39,
        249,
        185,
        201,
        154,
        9,
        120,
        77,
        228,
        114,
        166,
        6,
        191,
        139,
        98,
        102,
        221,
        48,
        253,
        226,
        152,
        37,
        179,
        16,
        145,
        34,
        136,
        54,
        208,
        148,
        206,
        143,
        150,
        219,
        189,
        241,
        210,
        19,
        92,
        131,
        56,
        70,
        64,
        30,
        66,
        182,
        163,
        195,
        72,
        126,
        110,
        107,
        58,
        40,
        84,
        250,
        133,
        186,
        61,
        202,
        94,
        155,
        159,
        10,
        21,
        121,
        43,
        78,
        212,
        229,
        172,
        115,
        243,
        167,
        87,
        7,
        112,
        192,
        247,
        140,
        128,
        99,
        13,
        103,
        74,
        222,
        237,
        49,
        197,
        254,
        24,
        227,
        165,
        153,
        119,
        38,
        184,
        180,
        124,
        17,
        68,
        146,
        217,
        35,
        32,
        137,
        46,
        55,
        63,
        209,
        91,
        149,
        188,
        207,
        205,
        144,
        135,
        151,
        178,
        220,
        252,
        190,
        97,
        242,
        86,
        211,
        171,
        20,
        42,
        93,
        158,
        132,
        60,
        57,
        83,
        71,
        109,
        65,
        162,
        31,
        45,
        67,
        216,
        183,
        123,
        164,
        118,
        196,
        23,
        73,
        236,
        (int) sbyte.MaxValue,
        12,
        111,
        246,
        108,
        161,
        59,
        82,
        41,
        157,
        85,
        170,
        251,
        96,
        134,
        177,
        187,
        204,
        62,
        90,
        203,
        89,
        95,
        176,
        156,
        169,
        160,
        81,
        11,
        245,
        22,
        235,
        122,
        117,
        44,
        215,
        79,
        174,
        213,
        233,
        230,
        231,
        173,
        232,
        116,
        214,
        244,
        234,
        168,
        80,
        88,
        175
      };
    }

    private static void PopulateGeneratorExponentsOfAlpha()
    {
      QRCode.generatorExponentsOfAlpha = new Dictionary<int, List<int>>();
      QRCode.generatorExponentsOfAlpha.Add(7, new List<int>()
      {
        87,
        229,
        146,
        149,
        238,
        102,
        21
      });
      QRCode.generatorExponentsOfAlpha.Add(10, new List<int>()
      {
        251,
        67,
        46,
        61,
        118,
        70,
        64,
        94,
        32,
        45
      });
      QRCode.generatorExponentsOfAlpha.Add(13, new List<int>()
      {
        74,
        152,
        176,
        100,
        86,
        100,
        106,
        104,
        130,
        218,
        206,
        140,
        78
      });
      QRCode.generatorExponentsOfAlpha.Add(15, new List<int>()
      {
        8,
        183,
        61,
        91,
        202,
        37,
        51,
        58,
        58,
        237,
        140,
        124,
        5,
        99,
        105
      });
      QRCode.generatorExponentsOfAlpha.Add(16, new List<int>()
      {
        120,
        104,
        107,
        109,
        102,
        161,
        76,
        3,
        91,
        191,
        147,
        169,
        182,
        194,
        225,
        120
      });
      QRCode.generatorExponentsOfAlpha.Add(17, new List<int>()
      {
        43,
        139,
        206,
        78,
        43,
        239,
        123,
        206,
        214,
        147,
        24,
        99,
        150,
        39,
        243,
        163,
        136
      });
      QRCode.generatorExponentsOfAlpha.Add(18, new List<int>()
      {
        215,
        234,
        158,
        94,
        184,
        97,
        118,
        170,
        79,
        187,
        152,
        148,
        252,
        179,
        5,
        98,
        96,
        153
      });
      QRCode.generatorExponentsOfAlpha.Add(20, new List<int>()
      {
        17,
        60,
        79,
        50,
        61,
        163,
        26,
        187,
        202,
        180,
        221,
        225,
        83,
        239,
        156,
        164,
        212,
        212,
        188,
        190
      });
      QRCode.generatorExponentsOfAlpha.Add(22, new List<int>()
      {
        210,
        171,
        247,
        242,
        93,
        230,
        14,
        109,
        221,
        53,
        200,
        74,
        8,
        172,
        98,
        80,
        219,
        134,
        160,
        105,
        165,
        231
      });
      QRCode.generatorExponentsOfAlpha.Add(24, new List<int>()
      {
        229,
        121,
        135,
        48,
        211,
        117,
        251,
        126,
        159,
        180,
        169,
        152,
        192,
        226,
        228,
        218,
        111,
        0,
        117,
        232,
        87,
        96,
        227,
        21
      });
      QRCode.generatorExponentsOfAlpha.Add(26, new List<int>()
      {
        173,
        125,
        158,
        2,
        103,
        182,
        118,
        17,
        145,
        201,
        111,
        28,
        165,
        53,
        161,
        21,
        245,
        142,
        13,
        102,
        48,
        227,
        153,
        145,
        218,
        70
      });
      QRCode.generatorExponentsOfAlpha.Add(28, new List<int>()
      {
        168,
        223,
        200,
        104,
        224,
        234,
        108,
        180,
        110,
        190,
        195,
        147,
        205,
        27,
        232,
        201,
        21,
        43,
        245,
        87,
        42,
        195,
        212,
        119,
        242,
        37,
        9,
        123
      });
      QRCode.generatorExponentsOfAlpha.Add(30, new List<int>()
      {
        41,
        173,
        145,
        152,
        216,
        31,
        179,
        182,
        50,
        48,
        110,
        86,
        239,
        96,
        222,
        125,
        42,
        173,
        226,
        193,
        224,
        130,
        156,
        37,
        251,
        216,
        238,
        40,
        192,
        180
      });
    }

    private static void PopulatePositionAdjustmentTable()
    {
      QRCode.positionAdjustmentPatternCoordinates = new Dictionary<int, List<int>>();
      int num1 = 2;
      Dictionary<int, List<int>> patternCoordinates1 = QRCode.positionAdjustmentPatternCoordinates;
      int key1 = num1;
      int num2 = key1 + 1;
      List<int> intList1 = new List<int>() { 6, 18 };
      patternCoordinates1.Add(key1, intList1);
      Dictionary<int, List<int>> patternCoordinates2 = QRCode.positionAdjustmentPatternCoordinates;
      int key2 = num2;
      int num3 = key2 + 1;
      List<int> intList2 = new List<int>() { 6, 22 };
      patternCoordinates2.Add(key2, intList2);
      Dictionary<int, List<int>> patternCoordinates3 = QRCode.positionAdjustmentPatternCoordinates;
      int key3 = num3;
      int num4 = key3 + 1;
      List<int> intList3 = new List<int>() { 6, 26 };
      patternCoordinates3.Add(key3, intList3);
      Dictionary<int, List<int>> patternCoordinates4 = QRCode.positionAdjustmentPatternCoordinates;
      int key4 = num4;
      int num5 = key4 + 1;
      List<int> intList4 = new List<int>() { 6, 30 };
      patternCoordinates4.Add(key4, intList4);
      Dictionary<int, List<int>> patternCoordinates5 = QRCode.positionAdjustmentPatternCoordinates;
      int key5 = num5;
      int num6 = key5 + 1;
      List<int> intList5 = new List<int>() { 6, 34 };
      patternCoordinates5.Add(key5, intList5);
      Dictionary<int, List<int>> patternCoordinates6 = QRCode.positionAdjustmentPatternCoordinates;
      int key6 = num6;
      int num7 = key6 + 1;
      List<int> intList6 = new List<int>() { 6, 22, 38 };
      patternCoordinates6.Add(key6, intList6);
      Dictionary<int, List<int>> patternCoordinates7 = QRCode.positionAdjustmentPatternCoordinates;
      int key7 = num7;
      int num8 = key7 + 1;
      List<int> intList7 = new List<int>() { 6, 24, 42 };
      patternCoordinates7.Add(key7, intList7);
      Dictionary<int, List<int>> patternCoordinates8 = QRCode.positionAdjustmentPatternCoordinates;
      int key8 = num8;
      int num9 = key8 + 1;
      List<int> intList8 = new List<int>() { 6, 26, 46 };
      patternCoordinates8.Add(key8, intList8);
      Dictionary<int, List<int>> patternCoordinates9 = QRCode.positionAdjustmentPatternCoordinates;
      int key9 = num9;
      int num10 = key9 + 1;
      List<int> intList9 = new List<int>() { 6, 28, 50 };
      patternCoordinates9.Add(key9, intList9);
      Dictionary<int, List<int>> patternCoordinates10 = QRCode.positionAdjustmentPatternCoordinates;
      int key10 = num10;
      int num11 = key10 + 1;
      List<int> intList10 = new List<int>() { 6, 30, 54 };
      patternCoordinates10.Add(key10, intList10);
      Dictionary<int, List<int>> patternCoordinates11 = QRCode.positionAdjustmentPatternCoordinates;
      int key11 = num11;
      int num12 = key11 + 1;
      List<int> intList11 = new List<int>() { 6, 32, 58 };
      patternCoordinates11.Add(key11, intList11);
      Dictionary<int, List<int>> patternCoordinates12 = QRCode.positionAdjustmentPatternCoordinates;
      int key12 = num12;
      int num13 = key12 + 1;
      List<int> intList12 = new List<int>() { 6, 34, 62 };
      patternCoordinates12.Add(key12, intList12);
      Dictionary<int, List<int>> patternCoordinates13 = QRCode.positionAdjustmentPatternCoordinates;
      int key13 = num13;
      int num14 = key13 + 1;
      List<int> intList13 = new List<int>() { 6, 26, 46, 66 };
      patternCoordinates13.Add(key13, intList13);
      Dictionary<int, List<int>> patternCoordinates14 = QRCode.positionAdjustmentPatternCoordinates;
      int key14 = num14;
      int num15 = key14 + 1;
      List<int> intList14 = new List<int>() { 6, 26, 48, 70 };
      patternCoordinates14.Add(key14, intList14);
      Dictionary<int, List<int>> patternCoordinates15 = QRCode.positionAdjustmentPatternCoordinates;
      int key15 = num15;
      int num16 = key15 + 1;
      List<int> intList15 = new List<int>() { 6, 26, 50, 74 };
      patternCoordinates15.Add(key15, intList15);
      Dictionary<int, List<int>> patternCoordinates16 = QRCode.positionAdjustmentPatternCoordinates;
      int key16 = num16;
      int num17 = key16 + 1;
      List<int> intList16 = new List<int>() { 6, 30, 54, 78 };
      patternCoordinates16.Add(key16, intList16);
      Dictionary<int, List<int>> patternCoordinates17 = QRCode.positionAdjustmentPatternCoordinates;
      int key17 = num17;
      int num18 = key17 + 1;
      List<int> intList17 = new List<int>() { 6, 30, 56, 82 };
      patternCoordinates17.Add(key17, intList17);
      Dictionary<int, List<int>> patternCoordinates18 = QRCode.positionAdjustmentPatternCoordinates;
      int key18 = num18;
      int num19 = key18 + 1;
      List<int> intList18 = new List<int>() { 6, 30, 58, 86 };
      patternCoordinates18.Add(key18, intList18);
      Dictionary<int, List<int>> patternCoordinates19 = QRCode.positionAdjustmentPatternCoordinates;
      int key19 = num19;
      int num20 = key19 + 1;
      List<int> intList19 = new List<int>() { 6, 34, 62, 90 };
      patternCoordinates19.Add(key19, intList19);
      Dictionary<int, List<int>> patternCoordinates20 = QRCode.positionAdjustmentPatternCoordinates;
      int key20 = num20;
      int num21 = key20 + 1;
      List<int> intList20 = new List<int>() { 6, 28, 50, 72, 94 };
      patternCoordinates20.Add(key20, intList20);
      Dictionary<int, List<int>> patternCoordinates21 = QRCode.positionAdjustmentPatternCoordinates;
      int key21 = num21;
      int num22 = key21 + 1;
      List<int> intList21 = new List<int>() { 6, 26, 50, 74, 98 };
      patternCoordinates21.Add(key21, intList21);
      Dictionary<int, List<int>> patternCoordinates22 = QRCode.positionAdjustmentPatternCoordinates;
      int key22 = num22;
      int num23 = key22 + 1;
      List<int> intList22 = new List<int>() { 6, 30, 54, 78, 102 };
      patternCoordinates22.Add(key22, intList22);
      Dictionary<int, List<int>> patternCoordinates23 = QRCode.positionAdjustmentPatternCoordinates;
      int key23 = num23;
      int num24 = key23 + 1;
      List<int> intList23 = new List<int>() { 6, 28, 54, 80, 106 };
      patternCoordinates23.Add(key23, intList23);
      Dictionary<int, List<int>> patternCoordinates24 = QRCode.positionAdjustmentPatternCoordinates;
      int key24 = num24;
      int num25 = key24 + 1;
      List<int> intList24 = new List<int>() { 6, 32, 58, 84, 110 };
      patternCoordinates24.Add(key24, intList24);
      Dictionary<int, List<int>> patternCoordinates25 = QRCode.positionAdjustmentPatternCoordinates;
      int key25 = num25;
      int num26 = key25 + 1;
      List<int> intList25 = new List<int>() { 6, 30, 58, 86, 114 };
      patternCoordinates25.Add(key25, intList25);
      Dictionary<int, List<int>> patternCoordinates26 = QRCode.positionAdjustmentPatternCoordinates;
      int key26 = num26;
      int num27 = key26 + 1;
      List<int> intList26 = new List<int>() { 6, 34, 62, 90, 118 };
      patternCoordinates26.Add(key26, intList26);
      Dictionary<int, List<int>> patternCoordinates27 = QRCode.positionAdjustmentPatternCoordinates;
      int key27 = num27;
      int num28 = key27 + 1;
      List<int> intList27 = new List<int>() { 6, 26, 50, 74, 98, 122 };
      patternCoordinates27.Add(key27, intList27);
      Dictionary<int, List<int>> patternCoordinates28 = QRCode.positionAdjustmentPatternCoordinates;
      int key28 = num28;
      int num29 = key28 + 1;
      List<int> intList28 = new List<int>() { 6, 30, 54, 78, 102, 126 };
      patternCoordinates28.Add(key28, intList28);
      Dictionary<int, List<int>> patternCoordinates29 = QRCode.positionAdjustmentPatternCoordinates;
      int key29 = num29;
      int num30 = key29 + 1;
      List<int> intList29 = new List<int>() { 6, 26, 52, 78, 104, 130 };
      patternCoordinates29.Add(key29, intList29);
      Dictionary<int, List<int>> patternCoordinates30 = QRCode.positionAdjustmentPatternCoordinates;
      int key30 = num30;
      int num31 = key30 + 1;
      List<int> intList30 = new List<int>() { 6, 30, 56, 82, 108, 134 };
      patternCoordinates30.Add(key30, intList30);
      Dictionary<int, List<int>> patternCoordinates31 = QRCode.positionAdjustmentPatternCoordinates;
      int key31 = num31;
      int num32 = key31 + 1;
      List<int> intList31 = new List<int>() { 6, 34, 60, 86, 112, 138 };
      patternCoordinates31.Add(key31, intList31);
      Dictionary<int, List<int>> patternCoordinates32 = QRCode.positionAdjustmentPatternCoordinates;
      int key32 = num32;
      int num33 = key32 + 1;
      List<int> intList32 = new List<int>() { 6, 30, 58, 86, 114, 142 };
      patternCoordinates32.Add(key32, intList32);
      Dictionary<int, List<int>> patternCoordinates33 = QRCode.positionAdjustmentPatternCoordinates;
      int key33 = num33;
      int num34 = key33 + 1;
      List<int> intList33 = new List<int>() { 6, 34, 62, 90, 118, 146 };
      patternCoordinates33.Add(key33, intList33);
      Dictionary<int, List<int>> patternCoordinates34 = QRCode.positionAdjustmentPatternCoordinates;
      int key34 = num34;
      int num35 = key34 + 1;
      List<int> intList34 = new List<int>() { 6, 30, 54, 78, 102, 126, 150 };
      patternCoordinates34.Add(key34, intList34);
      Dictionary<int, List<int>> patternCoordinates35 = QRCode.positionAdjustmentPatternCoordinates;
      int key35 = num35;
      int num36 = key35 + 1;
      List<int> intList35 = new List<int>() { 6, 24, 50, 76, 102, 128, 154 };
      patternCoordinates35.Add(key35, intList35);
      Dictionary<int, List<int>> patternCoordinates36 = QRCode.positionAdjustmentPatternCoordinates;
      int key36 = num36;
      int num37 = key36 + 1;
      List<int> intList36 = new List<int>() { 6, 28, 54, 80, 106, 132, 158 };
      patternCoordinates36.Add(key36, intList36);
      Dictionary<int, List<int>> patternCoordinates37 = QRCode.positionAdjustmentPatternCoordinates;
      int key37 = num37;
      int num38 = key37 + 1;
      List<int> intList37 = new List<int>() { 6, 32, 58, 84, 110, 136, 162 };
      patternCoordinates37.Add(key37, intList37);
      Dictionary<int, List<int>> patternCoordinates38 = QRCode.positionAdjustmentPatternCoordinates;
      int key38 = num38;
      int num39 = key38 + 1;
      List<int> intList38 = new List<int>() { 6, 26, 54, 82, 110, 138, 166 };
      patternCoordinates38.Add(key38, intList38);
      Dictionary<int, List<int>> patternCoordinates39 = QRCode.positionAdjustmentPatternCoordinates;
      int key39 = num39;
      int num40 = key39 + 1;
      List<int> intList39 = new List<int>() { 6, 30, 58, 86, 114, 142, 170 };
      patternCoordinates39.Add(key39, intList39);
    }

    private static void PopulateErrorCorrectionToMask()
    {
      QRCode.errorCorrectionToMask = new Dictionary<ErrorCorrectionLevel, string>();
      QRCode.errorCorrectionToMask.Add(ErrorCorrectionLevel.L, "01");
      QRCode.errorCorrectionToMask.Add(ErrorCorrectionLevel.M, "00");
      QRCode.errorCorrectionToMask.Add(ErrorCorrectionLevel.Q, "11");
      QRCode.errorCorrectionToMask.Add(ErrorCorrectionLevel.H, "10");
    }

    private static void PopulateFormatInformation()
    {
      QRCode.formatInformation = new Dictionary<string, string>();
      QRCode.formatInformation.Add("00000", "101010000010010");
      QRCode.formatInformation.Add("00001", "101000100100101");
      QRCode.formatInformation.Add("00010", "101111001111100");
      QRCode.formatInformation.Add("00011", "101101101001011");
      QRCode.formatInformation.Add("00100", "100010111111001");
      QRCode.formatInformation.Add("00101", "100000011001110");
      QRCode.formatInformation.Add("00110", "100111110010111");
      QRCode.formatInformation.Add("00111", "100101010100000");
      QRCode.formatInformation.Add("01000", "111011111000100");
      QRCode.formatInformation.Add("01001", "111001011110011");
      QRCode.formatInformation.Add("01010", "111110110101010");
      QRCode.formatInformation.Add("01011", "111100010011101");
      QRCode.formatInformation.Add("01100", "110011000101111");
      QRCode.formatInformation.Add("01101", "110001100011000");
      QRCode.formatInformation.Add("01110", "110110001000001");
      QRCode.formatInformation.Add("01111", "110100101110110");
      QRCode.formatInformation.Add("10000", "001011010001001");
      QRCode.formatInformation.Add("10001", "001001110111110");
      QRCode.formatInformation.Add("10010", "001110011100111");
      QRCode.formatInformation.Add("10011", "001100111010000");
      QRCode.formatInformation.Add("10100", "000011101100010");
      QRCode.formatInformation.Add("10101", "000001001010101");
      QRCode.formatInformation.Add("10110", "000110100001100");
      QRCode.formatInformation.Add("10111", "000100000111011");
      QRCode.formatInformation.Add("11000", "011010101011111");
      QRCode.formatInformation.Add("11001", "011000001101000");
      QRCode.formatInformation.Add("11010", "011111100110001");
      QRCode.formatInformation.Add("11011", "011101000000110");
      QRCode.formatInformation.Add("11100", "010010010110100");
      QRCode.formatInformation.Add("11101", "010000110000011");
      QRCode.formatInformation.Add("11110", "010111011011010");
      QRCode.formatInformation.Add("11111", "010101111101101");
    }
  }
}
