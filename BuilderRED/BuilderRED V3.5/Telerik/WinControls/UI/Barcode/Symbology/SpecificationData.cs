// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.SpecificationData
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public static class SpecificationData
  {
    internal const int AlphaLatch = 1001;
    internal const int MixedLatch = 1002;
    internal const int AlphaShift = 1003;
    internal const int PunctuationLatch = 1004;
    internal const int LowerLatch = 1005;
    internal const int PunctuationShift = 1006;
    internal const int MaxCodeWords = 928;
    internal const int MaxDataCodeWords = 925;
    internal const int RowIndicatorsCount = 2;
    internal const int AlphaIndexPosition = 0;
    internal const int LowerIndexPosition = 1;
    internal const int MixedIndexPosition = 2;
    internal const int PunctuationIndexPosition = 3;
    internal const int StartLength = 17;
    internal const int StopLength = 18;
    internal const int ClusterLength = 17;
    internal const int QuietZoneLength = 2;
    internal const int NumericListLength = 10;
    internal const int TextCompactionModeLatch = 900;
    internal const int FirstByteCompactionModeLatch = 901;
    internal const int ByteCompactionModeShift = 913;
    internal const int NumericComactionModeLatch = 902;
    internal const int SecondByteCompactionModeLatch = 924;
    internal const int BeginMacroPDF417ControlBlock = 928;
    internal const int BeginMacroPDF417OptionalField = 923;
    internal const int MacroPDF417Terminator = 922;
    internal const int ReaderInitialisation = 921;
    internal const int LengthIndicatorCount = 1;
    internal const int MaxTotalDataCount = 928;
    internal const int MaxDataOnlyCount = 925;
    internal const int MaxColumns = 30;
    internal const int MaxRows = 90;
    public static List<List<int>> BarSpaceSequence;
    public static List<List<int>> ErrorCorrectionLevels;
    public static List<TextModeDefinitionEntry> TextSubmodes;
    public static List<int> ByteModeValues;
    public static List<int> ECNumberPerLevel;
    public static List<Cluster> Start;
    public static List<Cluster> Stop;

    static SpecificationData()
    {
      SpecificationData.InitializeBarSpaceSequence();
      SpecificationData.ErrorCorrectionLevels = new List<List<int>>();
      SpecificationData.InitializeLevelZeroErrorCorrection();
      SpecificationData.InitializeLevelOneErrorCorrection();
      SpecificationData.InitializeLevelTwoErrorCorrection();
      SpecificationData.InitializeLevelThreeErrorCorrection();
      SpecificationData.InitializeLevelFourErrorCorrection();
      SpecificationData.InitializeLevelFiveErrorCorrection();
      SpecificationData.InitializeLevelSixErrorCorrection();
      SpecificationData.InitializeLevelSevenErrorCorrection();
      SpecificationData.InitializeLevelEightErrorCorrection();
      SpecificationData.InitializeTextSubmodes();
      SpecificationData.InitializeByteModeValues();
      SpecificationData.InitializeStartStopSequence();
      SpecificationData.InitializeECNumberPerLevel();
    }

    private static void InitializeStartStopSequence()
    {
      SpecificationData.Start = new List<Cluster>();
      SpecificationData.Stop = new List<Cluster>();
      SpecificationData.Start.Add(new Cluster(0, true, 8));
      SpecificationData.Start.Add(new Cluster(1, false, 1));
      SpecificationData.Start.Add(new Cluster(2, true, 1));
      SpecificationData.Start.Add(new Cluster(3, false, 1));
      SpecificationData.Start.Add(new Cluster(4, true, 1));
      SpecificationData.Start.Add(new Cluster(5, false, 1));
      SpecificationData.Start.Add(new Cluster(6, true, 1));
      SpecificationData.Start.Add(new Cluster(7, false, 3));
      SpecificationData.Stop.Add(new Cluster(0, true, 7));
      SpecificationData.Stop.Add(new Cluster(1, false, 1));
      SpecificationData.Stop.Add(new Cluster(2, true, 1));
      SpecificationData.Stop.Add(new Cluster(3, false, 3));
      SpecificationData.Stop.Add(new Cluster(4, true, 1));
      SpecificationData.Stop.Add(new Cluster(5, false, 1));
      SpecificationData.Stop.Add(new Cluster(6, true, 1));
      SpecificationData.Stop.Add(new Cluster(7, false, 2));
      SpecificationData.Stop.Add(new Cluster(8, true, 1));
    }

    private static void InitializeECNumberPerLevel()
    {
      SpecificationData.ECNumberPerLevel = new List<int>();
      for (int index = 0; index < 9; ++index)
      {
        if (index == 0)
          SpecificationData.ECNumberPerLevel.Add(2);
        else
          SpecificationData.ECNumberPerLevel.Add(SpecificationData.ECNumberPerLevel[index - 1] * 2);
      }
    }

    private static void InitializeByteModeValues()
    {
      SpecificationData.ByteModeValues = new List<int>();
      for (int index = 0; index < (int) byte.MaxValue; ++index)
        SpecificationData.ByteModeValues.Add(index);
    }

    private static void InitializeLevelZeroErrorCorrection()
    {
      List<int> intList = new List<int>() { 27, 917 };
      SpecificationData.ErrorCorrectionLevels.Add(intList);
    }

    private static void InitializeTextSubmodes()
    {
      SpecificationData.TextSubmodes = new List<TextModeDefinitionEntry>();
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        65,
        97,
        48,
        59
      }, 0);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        66,
        98,
        49,
        60
      }, 1);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        67,
        99,
        50,
        62
      }, 2);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        68,
        100,
        51,
        64
      }, 3);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        69,
        101,
        52,
        91
      }, 4);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        70,
        102,
        53,
        92
      }, 5);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        71,
        103,
        54,
        93
      }, 6);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        72,
        104,
        55,
        95
      }, 7);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        73,
        105,
        56,
        96
      }, 8);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        74,
        106,
        57,
        126
      }, 9);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        75,
        107,
        38,
        33
      }, 10);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        76,
        108,
        13,
        13
      }, 11);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        77,
        109,
        9,
        9
      }, 12);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        78,
        110,
        44,
        44
      }, 13);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        79,
        111,
        58,
        58
      }, 14);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        80,
        112,
        35,
        10
      }, 15);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        81,
        113,
        45,
        45
      }, 16);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        82,
        114,
        46,
        46
      }, 17);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        83,
        115,
        36,
        36
      }, 18);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        84,
        116,
        47,
        47
      }, 19);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        85,
        117,
        43,
        34
      }, 20);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        86,
        118,
        37,
        124
      }, 21);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        87,
        119,
        42,
        42
      }, 22);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        88,
        120,
        61,
        40
      }, 23);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        89,
        121,
        94,
        41
      }, 24);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        90,
        122,
        1004,
        63
      }, 25);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        32,
        32,
        32,
        123
      }, 26);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        1005,
        1003,
        1005,
        125
      }, 27);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        1002,
        1002,
        1001,
        39
      }, 28);
      SpecificationData.TextSubmodesAddRow(new List<int>()
      {
        1006,
        1006,
        1004,
        1001
      }, 29);
    }

    private static void TextSubmodesAddRow(List<int> rowValues, int rowIndex)
    {
      int group = 0;
      foreach (int rowValue in rowValues)
      {
        SpecificationData.TextSubmodes.Add(new TextModeDefinitionEntry(rowValue, group, rowIndex));
        ++group;
      }
    }

    private static void InitializeLevelOneErrorCorrection()
    {
      List<int> csValues = BarcodeResources.GetCSValues("LevelOneEC.txt");
      SpecificationData.ErrorCorrectionLevels.Add(csValues);
    }

    private static void InitializeLevelTwoErrorCorrection()
    {
      List<int> csValues = BarcodeResources.GetCSValues("LevelTwoEC.txt");
      SpecificationData.ErrorCorrectionLevels.Add(csValues);
    }

    private static void InitializeLevelThreeErrorCorrection()
    {
      List<int> csValues = BarcodeResources.GetCSValues("LevelThreeEC.txt");
      SpecificationData.ErrorCorrectionLevels.Add(csValues);
    }

    private static void InitializeLevelFourErrorCorrection()
    {
      List<int> csValues = BarcodeResources.GetCSValues("LevelFourEC.txt");
      SpecificationData.ErrorCorrectionLevels.Add(csValues);
    }

    private static void InitializeLevelFiveErrorCorrection()
    {
      List<int> csValues = BarcodeResources.GetCSValues("LevelFiveEC.txt");
      SpecificationData.ErrorCorrectionLevels.Add(csValues);
    }

    private static void InitializeLevelSixErrorCorrection()
    {
      List<int> csValues = BarcodeResources.GetCSValues("LevelSixEC.txt");
      SpecificationData.ErrorCorrectionLevels.Add(csValues);
    }

    private static void InitializeLevelSevenErrorCorrection()
    {
      List<int> csValues = BarcodeResources.GetCSValues("LevelSevenEC.txt");
      SpecificationData.ErrorCorrectionLevels.Add(csValues);
    }

    private static void InitializeLevelEightErrorCorrection()
    {
      List<int> csValues = BarcodeResources.GetCSValues("LevelEightEC.txt");
      SpecificationData.ErrorCorrectionLevels.Add(csValues);
    }

    private static void InitializeBarSpaceSequence()
    {
      SpecificationData.BarSpaceSequence = new List<List<int>>();
      SpecificationData.BarSpaceSequence = BarcodeResources.GetBarSpaceSequence("BarSpaceSequence.txt", 8);
    }
  }
}
