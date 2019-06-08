// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.NumericMaskTextBoxProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class NumericMaskTextBoxProvider : IMaskProvider
  {
    private string mask = "<>";
    protected IMaskCharacterProvider provider;
    private RadTextBoxItem textBoxItem;
    private int hintPos;
    private MaskedTextResultHint hint;
    private NumericCharacterTextBoxProvider.RadNumericMaskFormatType numericType;
    private CultureInfo culture;
    private bool includePrompt;
    private char promptChar;
    private RadMaskedEditBoxElement owner;

    public bool Click()
    {
      return false;
    }

    public RadTextBoxItem TextBoxItem
    {
      get
      {
        return this.textBoxItem;
      }
    }

    internal NumericCharacterTextBoxProvider.RadNumericMaskFormatType NumericType
    {
      get
      {
        return this.numericType;
      }
    }

    public NumericMaskTextBoxProvider(
      string mask,
      CultureInfo culture,
      RadMaskedEditBoxElement owner)
    {
      if (mask.ToLower() == "d")
        mask += "0";
      this.owner = owner;
      this.numericType = NumericMaskTextBoxProvider.GetFormat(mask, culture);
      this.mask = mask;
      this.culture = culture;
      this.provider = this.CreateNumericCharacterTextBoxProvider(mask, culture, this.numericType, owner);
      this.promptChar = this.provider.PromptChar;
      this.textBoxItem = owner.TextBoxItem;
    }

    protected virtual IMaskCharacterProvider CreateNumericCharacterTextBoxProvider(
      string mask,
      CultureInfo culture,
      NumericCharacterTextBoxProvider.RadNumericMaskFormatType radNumericMaskFormatType,
      RadMaskedEditBoxElement owner)
    {
      return (IMaskCharacterProvider) new NumericCharacterTextBoxProvider(mask, culture, radNumericMaskFormatType, owner);
    }

    public string ToString(bool includePromt, bool includeLiterals)
    {
      return this.provider.ToString(includePromt, includeLiterals);
    }

    public IMaskProvider Clone()
    {
      return (IMaskProvider) new NumericMaskTextBoxProvider(this.Mask, this.Culture, this.owner);
    }

    public bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint)
    {
      return this.provider.Set(input, out testPosition, out resultHint);
    }

    public CultureInfo Culture
    {
      get
      {
        return this.culture;
      }
    }

    public string Mask
    {
      get
      {
        return this.mask;
      }
    }

    public bool IncludePrompt
    {
      get
      {
        return this.includePrompt;
      }
      set
      {
        this.includePrompt = value;
      }
    }

    public char PromptChar
    {
      get
      {
        return this.promptChar;
      }
      set
      {
        this.promptChar = value;
      }
    }

    public void KeyPress(object sender, KeyPressEventArgs e)
    {
      this.provider.KeyPress(sender, e);
    }

    public void KeyDown(object sender, KeyEventArgs e)
    {
      this.provider.KeyDown(sender, e);
    }

    private void HandleDeleteByBack()
    {
      int selectionStart = this.TextBoxItem.SelectionStart;
      int startPosition = this.TextBoxItem.SelectionLength == 0 ? selectionStart - 1 : selectionStart;
      if (startPosition < 0 && this.TextBoxItem.SelectionLength == 0)
        return;
      this.provider.RemoveAt(startPosition, startPosition + this.TextBoxItem.SelectionLength);
      this.TextBoxItem.Text = this.provider.ToString(true, true);
      this.TextBoxItem.SelectionLength = 0;
      this.TextBoxItem.SelectionStart = startPosition;
    }

    public bool Validate(string value)
    {
      CancelEventArgs e = new CancelEventArgs();
      this.owner.CallValueChanging(e);
      if (e.Cancel || !(this.Mask != "<>") || value == null)
        return false;
      this.provider.Set(value, out this.hintPos, out this.hint);
      if (this.hint <= MaskedTextResultHint.Unknown)
        return false;
      this.owner.isNullValue = false;
      this.owner.CallValueChanged(EventArgs.Empty);
      return true;
    }

    public object Value
    {
      get
      {
        if (this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Percent)
          return (object) ((NumericCharacterTextBoxProvider) this.provider).PercentageFloatValue;
        return (object) this.provider.ToString(true, true);
      }
      set
      {
        if (this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Percent)
        {
          string s = value.ToString().Replace("%", "");
          double result = 0.0;
          if (double.TryParse(s, out result))
          {
            this.Validate((double.Parse(value.ToString().Replace("%", "")) * 100.0).ToString());
            return;
          }
        }
        this.Validate(Convert.ToString(value, (IFormatProvider) this.culture));
      }
    }

    public static NumericCharacterTextBoxProvider.RadNumericMaskFormatType GetFormat(
      string formatString,
      CultureInfo culture)
    {
      NumericCharacterTextBoxProvider.RadNumericMaskFormatType numericMaskFormatType = NumericCharacterTextBoxProvider.RadNumericMaskFormatType.None;
      if (Regex.IsMatch(formatString, "^[cCdDgGfFnNpP][0-9]{0,2}$"))
      {
        switch (formatString[0])
        {
          case 'C':
          case 'c':
            numericMaskFormatType = NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency;
            break;
          case 'D':
          case 'd':
            numericMaskFormatType = NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Standard;
            break;
          case 'F':
          case 'G':
          case 'f':
          case 'g':
            numericMaskFormatType = NumericCharacterTextBoxProvider.RadNumericMaskFormatType.FixedPoint;
            break;
          case 'N':
          case 'n':
            numericMaskFormatType = NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Decimal;
            break;
          case 'P':
          case 'p':
            numericMaskFormatType = NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Percent;
            break;
          default:
            numericMaskFormatType = NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Decimal;
            break;
        }
      }
      return numericMaskFormatType;
    }

    public bool Delete()
    {
      this.provider.Delete();
      return true;
    }
  }
}
