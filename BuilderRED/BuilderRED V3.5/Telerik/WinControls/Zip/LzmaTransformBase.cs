// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.LzmaTransformBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal abstract class LzmaTransformBase : CompressionTransformBase
  {
    public const uint RepeaterDistancesNumber = 4;
    public const uint StatesNumber = 12;
    public const int PosSlotBitsNumber = 6;
    public const int DictionaryLogSizeMin = 0;
    public const int LengthToPosStatesBits = 2;
    public const uint LengthToPosStates = 4;
    public const uint MatchMinLength = 2;
    public const int AlignBitsNumber = 4;
    public const uint AlignTableSize = 16;
    public const uint AlignMask = 15;
    public const uint StartPosModelIndex = 4;
    public const uint EndPosModelIndex = 14;
    public const uint PosModelsNumber = 10;
    public const uint FullDistancesNumber = 128;
    public const uint LiteralPosStatesBitsEncodingMax = 4;
    public const int LiteralContextBitsMax = 8;
    public const int PosStatesBitsMax = 4;
    public const uint PosStatesMaxNumber = 16;
    public const int PosStatesBitsEncodingMax = 4;
    public const uint PosStatesEncodingMax = 16;
    public const int LowLengthBitsNumber = 3;
    public const int MiddleLengthBitsNumber = 3;
    public const int HighLengthBitsNumber = 8;
    public const uint LowLengthSymbolsNumber = 8;
    public const uint MiddleLengthSymbolsNumber = 8;
    public const uint LengthSymbolsNumber = 272;
    public const uint MatchMaxLen = 273;

    public LzmaTransformBase(LzmaSettings settings)
    {
      this.Settings = settings;
    }

    protected LzmaSettings Settings { get; private set; }
  }
}
