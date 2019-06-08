// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.TextMode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  internal class TextMode
  {
    private readonly Dictionary<int, TextSubMode> textSubModes;
    private List<long> formattedDataInt;
    private List<long> codeWordsDataInt;
    private TextSubMode currentSymbolSubmode;
    private int currentSubmodeSwitch;

    private int PreviousSubmodeSwitch { get; set; }

    private int CurrentSubmodeSwitch
    {
      get
      {
        return this.currentSubmodeSwitch;
      }
      set
      {
        if (this.currentSubmodeSwitch != 29)
          this.PreviousSubmodeSwitch = this.currentSubmodeSwitch;
        this.currentSubmodeSwitch = value;
      }
    }

    private TextSubMode? PreviousSymbolSubmode { get; set; }

    private TextSubMode CurrentSymbolSubmode
    {
      get
      {
        return this.currentSymbolSubmode;
      }
      set
      {
        if (this.currentSymbolSubmode != TextSubMode.Punctuation)
          this.PreviousSymbolSubmode = new TextSubMode?(this.currentSymbolSubmode);
        this.currentSymbolSubmode = value;
      }
    }

    internal List<long> FormattedDataInt
    {
      get
      {
        return this.formattedDataInt;
      }
    }

    public TextMode()
    {
      this.textSubModes = new Dictionary<int, TextSubMode>()
      {
        {
          0,
          TextSubMode.Upper
        },
        {
          1,
          TextSubMode.Lower
        },
        {
          2,
          TextSubMode.Mixed
        },
        {
          3,
          TextSubMode.Punctuation
        }
      };
    }

    internal static TextModeDefinitionEntry FindCharacterInTable(int value)
    {
      foreach (TextModeDefinitionEntry textSubmode in SpecificationData.TextSubmodes)
      {
        if (textSubmode.EntryValue == value)
          return textSubmode;
      }
      return (TextModeDefinitionEntry) null;
    }

    internal List<long> EncodeData(string textToEncode, bool shouldApplyNonLatchData)
    {
      this.InitializeFormatData(textToEncode);
      if (this.formattedDataInt.Count % 2 != 0)
        this.PadData(shouldApplyNonLatchData);
      this.InitializeCodeWordsDataInt();
      return this.codeWordsDataInt;
    }

    internal void PadData(bool shouldApplyNonLatchData)
    {
      if (shouldApplyNonLatchData)
      {
        if (this.CurrentSymbolSubmode != TextSubMode.Punctuation)
          this.formattedDataInt.Add((long) TextMode.FindCharacterInTable(1006).RowIndex);
        else
          this.formattedDataInt.Add((long) TextMode.FindCharacterInTable(1001).RowIndex);
      }
      else
        this.formattedDataInt.Add((long) TextMode.FindCharacterInTable(1005).RowIndex);
    }

    internal void InitializeCodeWordsDataInt()
    {
      this.codeWordsDataInt = new List<long>();
      for (int index = 0; index < this.formattedDataInt.Count; index += 2)
        this.codeWordsDataInt.Add(this.formattedDataInt[index] * 30L + this.formattedDataInt[index + 1]);
    }

    internal void InitializeFormatData(string data)
    {
      this.formattedDataInt = new List<long>();
      int num1 = 0;
      while (num1 < data.Length)
      {
        TextModeDefinitionEntry nextValidCharacter = this.GetNextValidCharacter(data.Substring(num1));
        if (nextValidCharacter != null)
        {
          this.CurrentSymbolSubmode = this.GetSymbolSubmode(nextValidCharacter);
          if (num1 == 0)
          {
            if (nextValidCharacter.TypeIndex == 1)
            {
              this.formattedDataInt.Add(27L);
              this.CurrentSubmodeSwitch = 27;
            }
            else if (nextValidCharacter.TypeIndex == 2)
            {
              this.formattedDataInt.Add(28L);
              this.CurrentSubmodeSwitch = 28;
            }
            else if (nextValidCharacter.TypeIndex == 3)
            {
              this.formattedDataInt.Add(29L);
              this.CurrentSubmodeSwitch = 29;
            }
            if (this.IsStringIdentical(data, nextValidCharacter.TypeIndex))
            {
              this.AddRangeToFormattedDataInt(data);
              int num2 = num1 + data.Length;
              break;
            }
            int firstSegmentLength = this.GetFirstSegmentLength(data);
            num1 += firstSegmentLength;
            this.AddRangeToFormattedDataInt(data.Substring(0, firstSegmentLength));
            this.SetCurrentSwitch(this.GetNextValidCharacter(data.Substring(num1 - 1)), data, num1 - 1);
          }
          else
          {
            this.formattedDataInt.Add((long) nextValidCharacter.RowIndex);
            if (num1 != data.Length - 1)
              this.SetCurrentSwitch(nextValidCharacter, data, num1);
            ++num1;
          }
        }
      }
    }

    internal TextModeDefinitionEntry GetNextValidCharacter(
      string remainingData)
    {
      TextModeDefinitionEntry characterInTable = TextMode.FindCharacterInTable((int) remainingData[0]);
      if (characterInTable != null || remainingData.Length <= 1)
        return characterInTable;
      remainingData = remainingData.Substring(1);
      return this.GetNextValidCharacter(remainingData);
    }

    private void SetCurrentSwitch(TextModeDefinitionEntry currentEntry, string data, int dataIndex)
    {
      TextModeDefinitionEntry nextValidCharacter = this.GetNextValidCharacter(data.Substring(dataIndex + 1));
      if (nextValidCharacter == null)
        return;
      if (currentEntry.TypeIndex != 3)
      {
        this.SetCurrentSwitchCore(currentEntry.TypeIndex, nextValidCharacter.TypeIndex);
      }
      else
      {
        TextSubMode? previousSymbolSubmode = this.PreviousSymbolSubmode;
        TextSubMode symbolSubmode = this.GetSymbolSubmode(nextValidCharacter);
        if ((previousSymbolSubmode.GetValueOrDefault() != symbolSubmode ? 0 : (previousSymbolSubmode.HasValue ? 1 : 0)) != 0)
          this.CurrentSubmodeSwitch = this.PreviousSubmodeSwitch;
        else if (this.PreviousSymbolSubmode.HasValue)
          this.SetCurrentSwitchCore((int) this.PreviousSymbolSubmode.Value, nextValidCharacter.TypeIndex);
        else if (nextValidCharacter.TypeIndex == 1)
        {
          this.formattedDataInt.Add(27L);
          this.CurrentSubmodeSwitch = 27;
        }
        else if (nextValidCharacter.TypeIndex == 2 && this.CurrentSubmodeSwitch != 28)
        {
          this.formattedDataInt.Add(28L);
          this.CurrentSubmodeSwitch = 28;
        }
        else
        {
          if (nextValidCharacter.TypeIndex != 3)
            return;
          this.formattedDataInt.Add(29L);
          this.CurrentSubmodeSwitch = 29;
        }
      }
    }

    private void SetCurrentSwitchCore(int currentEntryMode, int nextEntryMode)
    {
      switch (currentEntryMode)
      {
        case 0:
          if (nextEntryMode == 0 && this.CurrentSubmodeSwitch == 27)
          {
            this.formattedDataInt.Add(27L);
            this.CurrentSubmodeSwitch = 27;
            break;
          }
          if (nextEntryMode == 1 && this.CurrentSubmodeSwitch == 0)
          {
            this.formattedDataInt.Add(27L);
            this.CurrentSubmodeSwitch = 27;
            break;
          }
          if (nextEntryMode == 1 && this.CurrentSubmodeSwitch == 29)
          {
            this.formattedDataInt.Add(27L);
            this.CurrentSubmodeSwitch = 27;
            break;
          }
          if (nextEntryMode == 2)
          {
            this.formattedDataInt.Add(28L);
            this.CurrentSubmodeSwitch = 28;
            break;
          }
          if (nextEntryMode != 3)
            break;
          this.formattedDataInt.Add(29L);
          this.CurrentSubmodeSwitch = 29;
          break;
        case 1:
          switch (nextEntryMode)
          {
            case 0:
              this.formattedDataInt.Add(27L);
              this.CurrentSubmodeSwitch = 27;
              return;
            case 2:
              this.formattedDataInt.Add(28L);
              this.CurrentSubmodeSwitch = 28;
              return;
            case 3:
              this.formattedDataInt.Add(29L);
              this.CurrentSubmodeSwitch = 29;
              return;
            default:
              return;
          }
        case 2:
          switch (nextEntryMode)
          {
            case 0:
              this.formattedDataInt.Add(28L);
              this.CurrentSubmodeSwitch = 28;
              return;
            case 1:
              this.formattedDataInt.Add(27L);
              this.CurrentSubmodeSwitch = 27;
              return;
            case 3:
              this.formattedDataInt.Add(29L);
              this.CurrentSubmodeSwitch = 29;
              return;
            default:
              return;
          }
      }
    }

    private int GetFirstSegmentLength(string data)
    {
      int num = data.Length <= 0 ? 0 : 1;
      for (int startIndex = 0; startIndex < data.Length; ++startIndex)
      {
        TextModeDefinitionEntry nextValidCharacter1 = this.GetNextValidCharacter(data.Substring(startIndex));
        TextModeDefinitionEntry nextValidCharacter2 = this.GetNextValidCharacter(data.Substring(startIndex + 1));
        if (nextValidCharacter1 != null && nextValidCharacter2 != null && nextValidCharacter1.TypeIndex == nextValidCharacter2.TypeIndex)
          ++num;
        else
          break;
      }
      return num;
    }

    private void AddRangeToFormattedDataInt(string data)
    {
      for (int startIndex = 0; startIndex < data.Length; ++startIndex)
      {
        this.formattedDataInt.Add((long) this.GetNextValidCharacter(data.Substring(startIndex)).RowIndex);
        if (this.CurrentSubmodeSwitch == 29 && startIndex < data.Length - 1)
          this.formattedDataInt.Add(29L);
      }
    }

    private bool IsStringIdentical(string data, int typeIndex)
    {
      bool flag = true;
      for (int startIndex = 0; startIndex < data.Length - 1; ++startIndex)
      {
        TextModeDefinitionEntry nextValidCharacter1 = this.GetNextValidCharacter(data.Substring(startIndex));
        TextModeDefinitionEntry nextValidCharacter2 = this.GetNextValidCharacter(data.Substring(startIndex + 1));
        if (nextValidCharacter1.TypeIndex != nextValidCharacter2.TypeIndex || nextValidCharacter1.TypeIndex != typeIndex || nextValidCharacter2.TypeIndex != typeIndex)
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    private TextSubMode GetSymbolSubmode(TextModeDefinitionEntry entry)
    {
      TextSubMode textSubMode;
      if (!this.textSubModes.TryGetValue(entry.TypeIndex, out textSubMode))
        return TextSubMode.Punctuation;
      return textSubMode;
    }
  }
}
