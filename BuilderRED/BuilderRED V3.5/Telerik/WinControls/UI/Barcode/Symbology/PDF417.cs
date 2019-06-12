// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.PDF417
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class PDF417 : ISymbology, INotifyPropertyChanged
  {
    private int module = 1;
    private bool[,] dataMatrix;
    private List<long> eCCodeWords;
    private List<long> encodedRawData;
    private EncodingMode encodingMode;
    private int xRatio;
    private int yRatio;
    private int columns;
    private int rows;
    private int totalRows;
    private int totalColumns;
    private int eCCount;
    private int eCLevel;

    [DefaultValue(1)]
    public int ECLevel
    {
      get
      {
        return this.eCLevel;
      }
      set
      {
        if (this.eCLevel == value)
          return;
        this.eCLevel = value;
        this.OnPropertyChanged(nameof (ECLevel));
      }
    }

    [DefaultValue(3)]
    public int Rows
    {
      get
      {
        return this.rows;
      }
      set
      {
        if (this.rows == value)
          return;
        this.rows = value;
        this.OnPropertyChanged(nameof (Rows));
      }
    }

    [DefaultValue(3)]
    public int Columns
    {
      get
      {
        return this.columns;
      }
      set
      {
        if (this.columns == value)
          return;
        this.columns = value;
        this.OnPropertyChanged(nameof (Columns));
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

    [DefaultValue(EncodingMode.Auto)]
    public EncodingMode EncodingMode
    {
      get
      {
        return this.encodingMode;
      }
      set
      {
        if (this.encodingMode == value)
          return;
        this.encodingMode = value;
        this.OnPropertyChanged(nameof (EncodingMode));
      }
    }

    [Browsable(false)]
    public int MaxAvailableDataCount
    {
      get
      {
        return this.rows * this.columns - this.eCCount - 1;
      }
    }

    [Browsable(false)]
    public bool[,] DataMatrix
    {
      get
      {
        return this.dataMatrix;
      }
    }

    private int RawDataCount
    {
      get
      {
        return this.columns * this.rows - this.eCCount;
      }
    }

    public PDF417()
      : this(3, 3, 1)
    {
    }

    public PDF417(int columns, int rows, int errorCorrectionLevel)
    {
      this.columns = columns;
      this.rows = rows;
      this.eCLevel = errorCorrectionLevel;
    }

    private int GetValidValue(int value, int min, int max)
    {
      return Math.Min(Math.Max(value, min), max);
    }

    public void PopulateMatrix(
      string text,
      int errorCorrectionLevel,
      EncodingMode mode,
      int columns,
      int rows)
    {
      this.encodedRawData = new List<long>();
      this.xRatio = this.GetValidValue(columns, 3, 30);
      this.yRatio = this.GetValidValue(rows, 2, 90);
      this.eCLevel = errorCorrectionLevel;
      this.eCCount = SpecificationData.ErrorCorrectionLevels[this.eCLevel].Count;
      switch (mode)
      {
        case EncodingMode.Auto:
          text = PDF417.ValidateTextModeNone(text);
          int dataIndex = 0;
          while (dataIndex < text.Length)
          {
            int digitsAtPosition = PDF417.GetNumberOfDigitsAtPosition(text, dataIndex);
            if (digitsAtPosition >= 13)
              this.EncodeNumeric(text, ref dataIndex, digitsAtPosition);
            else if (digitsAtPosition < 13)
            {
              int ofCharsAtPosition = PDF417.GetNumberOfCharsAtPosition(text, dataIndex);
              if (ofCharsAtPosition >= 5)
                this.EncodeText(text, ref dataIndex, ofCharsAtPosition);
              else if (ofCharsAtPosition < 5)
              {
                int ofBytesAtPosition = PDF417.GetNumberOfBytesAtPosition(text, dataIndex);
                this.EncodeByte(text, ref dataIndex, ofBytesAtPosition);
              }
            }
          }
          break;
        case EncodingMode.Text:
          this.EncodeTextCompleteString(text);
          break;
        case EncodingMode.Numeric:
          this.EncodeNumericCompleteString(text);
          break;
        default:
          this.EncodeByteCompleteString(text);
          break;
      }
      this.SetSmallestSizeOfMatrix();
      this.VerifyDataLength();
      this.PadData();
      this.SetErrorCorrection();
      this.FillMatrixWithData();
    }

    public static int CalculateLeftRowIndicator(
      int clusterIndex,
      int rowNumber,
      int rows,
      int columns,
      int eclevel)
    {
      int num;
      switch (clusterIndex)
      {
        case 0:
          num = 30 * ((rowNumber - 1) / 3) + (rows - 1) / 3;
          break;
        case 1:
          num = 30 * ((rowNumber - 1) / 3) + eclevel * 3 + (rows - 1) % 3;
          break;
        default:
          num = 30 * ((rowNumber - 1) / 3) + (columns - 1);
          break;
      }
      return num;
    }

    public static int CalculateRightRowIndicator(
      int clusterIndex,
      int rowNumber,
      int rows,
      int columns,
      int eclevel)
    {
      int num;
      switch (clusterIndex)
      {
        case 0:
          num = 30 * ((rowNumber - 1) / 3) + (columns - 1);
          break;
        case 1:
          num = 30 * ((rowNumber - 1) / 3) + (rows - 1) / 3;
          break;
        default:
          num = 30 * ((rowNumber - 1) / 3) + eclevel * 3 + (rows - 1) % 3;
          break;
      }
      return num;
    }

    private static string ValidateTextModeNone(string text)
    {
      string empty = string.Empty;
      for (int index = 0; index < text.Length; ++index)
      {
        if (PDF417.IsCharValid(text[index]))
          empty += (string) (object) text[index];
      }
      return empty;
    }

    private static string ValidateText(string text)
    {
      string empty = string.Empty;
      for (int index = 0; index < text.Length; ++index)
      {
        if (TextMode.FindCharacterInTable((int) text[index]) != null)
          empty += (string) (object) text[index];
      }
      return empty;
    }

    private static string ValidateByte(string text)
    {
      string empty = string.Empty;
      for (int index = 0; index < text.Length; ++index)
      {
        if (SpecificationData.ByteModeValues.Contains((int) text[index]))
          empty += (string) (object) text[index];
      }
      return empty;
    }

    private static string ValidateDigits(string text)
    {
      string empty = string.Empty;
      for (int index = 0; index < text.Length; ++index)
      {
        if (char.IsDigit(text[index]))
          empty += (string) (object) text[index];
      }
      return empty;
    }

    private static bool IsCharValid(char character)
    {
      return PDF417.GetNumberOfBytesAtPosition(character.ToString(), 0) + PDF417.GetNumberOfCharsAtPosition(character.ToString(), 0) + PDF417.GetNumberOfDigitsAtPosition(character.ToString(), 0) > 0;
    }

    private static int GetNumberOfBytesAtPosition(string text, int currentPosition)
    {
      int num = 0;
      while (currentPosition <= text.Length - 1 && PDF417.GetNumberOfDigitsAtPosition(text, currentPosition) < 13 && (PDF417.GetNumberOfCharsAtPosition(text, currentPosition) < 5 && SpecificationData.ByteModeValues.Contains((int) text[currentPosition])))
      {
        ++currentPosition;
        ++num;
      }
      return num;
    }

    private static int GetNumberOfCharsAtPosition(string text, int currentPosition)
    {
      int num = 0;
      while (currentPosition <= text.Length - 1 && PDF417.GetNumberOfDigitsAtPosition(text, currentPosition) < 13 && TextMode.FindCharacterInTable((int) text[currentPosition]) != null)
      {
        ++currentPosition;
        ++num;
      }
      return num;
    }

    private static int GetNumberOfDigitsAtPosition(string text, int currentPosition)
    {
      int num = 0;
      while (currentPosition <= text.Length - 1 && char.IsDigit(text[currentPosition]))
      {
        ++currentPosition;
        ++num;
      }
      return num;
    }

    private static int CalculateClusterIndex(int rowIndex)
    {
      switch (rowIndex % 3 * 3)
      {
        case 0:
          return 0;
        case 3:
          return 1;
        default:
          return 2;
      }
    }

    private static void DetermineNextMode(string text, ref bool shouldApplyNonLatchData)
    {
      if (PDF417.GetNumberOfCharsAtPosition(text, 0) >= 5 || PDF417.GetNumberOfBytesAtPosition(text, 0) == 1)
        return;
      shouldApplyNonLatchData = true;
    }

    private void EncodeNumeric(string text, ref int dataIndex, int numberOfDigitsAtPosition)
    {
      this.encodedRawData.Add(902L);
      this.encodedRawData.AddRange((IEnumerable<long>) NumericMode.EncodeData(text.Substring(dataIndex, numberOfDigitsAtPosition)));
      dataIndex += numberOfDigitsAtPosition;
    }

    private void EncodeNumericCompleteString(string text)
    {
      text = PDF417.ValidateDigits(text);
      this.encodedRawData.Add(902L);
      this.encodedRawData.AddRange((IEnumerable<long>) NumericMode.EncodeData(text));
    }

    private void EncodeText(string text, ref int dataIndex, int numberOfTexModeCharsAtPosition)
    {
      TextMode textMode = new TextMode();
      int startIndex = dataIndex + numberOfTexModeCharsAtPosition;
      bool shouldApplyNonLatchData = false;
      if (startIndex < text.Length - 1)
        PDF417.DetermineNextMode(text.Substring(startIndex), ref shouldApplyNonLatchData);
      this.encodedRawData.Add(900L);
      this.encodedRawData.AddRange((IEnumerable<long>) textMode.EncodeData(text.Substring(dataIndex, numberOfTexModeCharsAtPosition), shouldApplyNonLatchData));
      dataIndex += numberOfTexModeCharsAtPosition;
    }

    private void EncodeTextCompleteString(string text)
    {
      TextMode textMode = new TextMode();
      text = PDF417.ValidateText(text);
      this.encodedRawData.AddRange((IEnumerable<long>) textMode.EncodeData(text, false));
    }

    private void EncodeByte(string text, ref int dataIndex, int numberOfBytesAtIndex)
    {
      this.encodedRawData.Add(913L);
      this.encodedRawData.AddRange((IEnumerable<long>) ByteMode.EncodeText(text.Substring(dataIndex, numberOfBytesAtIndex)));
      dataIndex += numberOfBytesAtIndex;
    }

    private void EncodeByteCompleteString(string text)
    {
      text = PDF417.ValidateByte(text);
      this.encodedRawData.AddRange((IEnumerable<long>) ByteMode.EncodeText(text));
    }

    private void SetSmallestSizeOfMatrix()
    {
      this.rows = 3;
      this.columns = 2;
      while (this.columns * this.rows < this.encodedRawData.Count + this.eCCount + 1)
      {
        if ((double) this.columns / (double) this.rows < (double) this.xRatio / (double) this.yRatio && this.columns <= 30)
        {
          if ((this.columns + 1) * this.rows > 928)
            break;
          if (this.columns + 1 < 30)
            ++this.columns;
          else if (this.rows + 1 < 90)
            ++this.rows;
        }
        else
        {
          if ((this.rows + 1) * this.columns > 928)
            break;
          if (this.rows + 1 < 90)
            ++this.rows;
          else if (this.columns + 1 < 90)
            ++this.columns;
        }
      }
    }

    private void PadData()
    {
      List<long> longList = new List<long>();
      int rawDataCount = this.RawDataCount;
      longList.Add((long) rawDataCount);
      longList.AddRange((IEnumerable<long>) this.encodedRawData);
      while (longList.Count < rawDataCount)
        longList.Add(900L);
      this.encodedRawData = longList;
    }

    private void SetErrorCorrection()
    {
      this.eCCodeWords = new List<long>();
      this.eCCodeWords.AddRange((IEnumerable<long>) ErrorCorrectionGenerator.GenerateErrorCorrectionSequence(this.encodedRawData, this.eCLevel));
    }

    private void VerifyDataLength()
    {
      while (this.encodedRawData.Count > this.MaxAvailableDataCount)
        this.encodedRawData.RemoveAt(this.encodedRawData.Count - 1);
    }

    private void FillMatrixWithData()
    {
      this.totalColumns = 35 + this.columns * 17 + 34;
      this.totalRows = this.rows;
      this.dataMatrix = new bool[this.totalRows, this.totalColumns];
      int dataPointer = 0;
      int ecpointer = 0;
      for (int rowIndex = 0; rowIndex < this.totalRows; ++rowIndex)
      {
        int columnIndex = 0;
        while (columnIndex < this.totalColumns)
        {
          switch (columnIndex)
          {
            case 0:
              this.AddStartClusterToRow(rowIndex);
              columnIndex += 17;
              continue;
            case 17:
              this.AddLeftRowIndicatorCluster(rowIndex, columnIndex);
              columnIndex += 17;
              continue;
            default:
              if (columnIndex == this.totalColumns - 18 - 17)
              {
                this.AddRightRowIndicatorCluster(rowIndex, columnIndex);
                columnIndex += 17;
                continue;
              }
              if (columnIndex == this.totalColumns - 18)
              {
                this.AddStopClusterToRow(rowIndex);
                columnIndex += 18;
                continue;
              }
              if (dataPointer < this.encodedRawData.Count && columnIndex + 17 < this.totalColumns)
              {
                this.AddDataCluster(rowIndex, columnIndex, dataPointer);
                columnIndex += 17;
                ++dataPointer;
                continue;
              }
              this.AddErrorCorrectionCluster(rowIndex, columnIndex, ecpointer);
              columnIndex += 17;
              ++ecpointer;
              continue;
          }
        }
      }
    }

    private void AddLeftRowIndicatorCluster(int rowIndex, int columnIndex)
    {
      int clusterIndex = PDF417.CalculateClusterIndex(rowIndex);
      int leftRowIndicator = PDF417.CalculateLeftRowIndicator(clusterIndex, rowIndex, this.rows, this.columns, this.eCLevel);
      string str = SpecificationData.BarSpaceSequence[leftRowIndicator][clusterIndex].ToString();
      for (int index1 = 0; index1 < str.Length; ++index1)
      {
        bool flag = index1 % 2 == 0;
        for (int index2 = 0; index2 < int.Parse(str[index1].ToString()); ++index2)
        {
          this.dataMatrix[rowIndex, columnIndex] = flag;
          ++columnIndex;
        }
      }
    }

    private void AddRightRowIndicatorCluster(int rowIndex, int columnIndex)
    {
      int clusterIndex = PDF417.CalculateClusterIndex(rowIndex);
      int rightRowIndicator = PDF417.CalculateRightRowIndicator(clusterIndex, rowIndex, this.rows, this.columns, this.eCLevel);
      string str = SpecificationData.BarSpaceSequence[rightRowIndicator][clusterIndex].ToString();
      for (int index1 = 0; index1 < str.Length; ++index1)
      {
        bool flag = index1 % 2 == 0;
        for (int index2 = 0; index2 < int.Parse(str[index1].ToString()); ++index2)
        {
          this.dataMatrix[rowIndex, columnIndex] = flag;
          ++columnIndex;
        }
      }
    }

    private void AddErrorCorrectionCluster(int rowIndex, int columnIndex, int ecpointer)
    {
      int clusterIndex = PDF417.CalculateClusterIndex(rowIndex);
      string str = SpecificationData.BarSpaceSequence[(int) this.eCCodeWords[ecpointer]][clusterIndex].ToString();
      for (int index1 = 0; index1 < str.Length; ++index1)
      {
        bool flag = index1 % 2 == 0;
        for (int index2 = 0; index2 < int.Parse(str[index1].ToString()); ++index2)
        {
          this.dataMatrix[rowIndex, columnIndex] = flag;
          ++columnIndex;
        }
      }
    }

    private void AddDataCluster(int rowIndex, int columnIndex, int dataPointer)
    {
      int clusterIndex = PDF417.CalculateClusterIndex(rowIndex);
      string str = SpecificationData.BarSpaceSequence[(int) this.encodedRawData[dataPointer]][clusterIndex].ToString();
      for (int index1 = 0; index1 < str.Length; ++index1)
      {
        bool flag = index1 % 2 == 0;
        for (int index2 = 0; index2 < int.Parse(str[index1].ToString()); ++index2)
        {
          this.dataMatrix[rowIndex, columnIndex] = flag;
          ++columnIndex;
        }
      }
    }

    private void AddStopClusterToRow(int rowIndex)
    {
      int index1 = this.totalColumns - 18;
      foreach (Cluster cluster in SpecificationData.Stop)
      {
        for (int index2 = 0; index2 < cluster.NumberOfModulesAtPosition; ++index2)
        {
          this.dataMatrix[rowIndex, index1] = cluster.ValueOfModule;
          ++index1;
        }
      }
    }

    private void AddStartClusterToRow(int rowIndex)
    {
      int index1 = 0;
      foreach (Cluster cluster in SpecificationData.Start)
      {
        for (int index2 = 0; index2 < cluster.NumberOfModulesAtPosition; ++index2)
        {
          this.dataMatrix[rowIndex, index1] = cluster.ValueOfModule;
          ++index1;
        }
      }
    }

    public void CreateElements(IElementFactory factory, Rectangle bounds)
    {
      int length1 = this.dataMatrix.GetLength(0);
      int length2 = this.dataMatrix.GetLength(1);
      int left = bounds.Left;
      int top = bounds.Top;
      int width = bounds.Width / this.dataMatrix.GetLength(1);
      int height = bounds.Height / this.dataMatrix.GetLength(0);
      List<Rectangle> rectangleList = new List<Rectangle>();
      for (int index1 = 0; index1 < length1; ++index1)
      {
        for (int index2 = 0; index2 < length2; ++index2)
        {
          if (this.dataMatrix[index1, index2])
          {
            Rectangle rectangle = new Rectangle(left + index2 * width, top + index1 * height, width, height);
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
  }
}
