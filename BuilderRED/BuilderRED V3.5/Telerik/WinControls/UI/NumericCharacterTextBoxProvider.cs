// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.NumericCharacterTextBoxProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class NumericCharacterTextBoxProvider : IMaskCharacterProvider
  {
    private string value = "0";
    private char promptChar = '0';
    private NumericCharacterTextBoxProvider.RadNumericMaskFormatType numericType;
    private CultureInfo culture;
    private string mask;
    private RadTextBoxItem textBoxItem;
    private RadMaskedEditBoxElement owner;
    private double floatValue;

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

    public NumericCharacterTextBoxProvider(
      string mask,
      CultureInfo culture,
      NumericCharacterTextBoxProvider.RadNumericMaskFormatType numericType,
      RadMaskedEditBoxElement owner)
    {
      this.owner = owner;
      this.mask = mask;
      this.culture = culture;
      this.numericType = numericType;
      this.promptChar = '0';
      this.textBoxItem = owner.TextBoxItem;
    }

    protected virtual string EnsureMinusSign(string input, string parsedInput)
    {
      if (input.Length > 0 && (int) input[0] == (int) this.culture.NumberFormat.NegativeSign[0] && (parsedInput.Length > 0 && !parsedInput.Contains(this.culture.NumberFormat.NegativeSign)) && !parsedInput.StartsWith("("))
        parsedInput = this.culture.NumberFormat.NegativeSign + parsedInput;
      return parsedInput;
    }

    public virtual string ToString(bool includePromt, bool includeLiterals)
    {
      return this.ParseText(this.value);
    }

    public bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint)
    {
      resultHint = MaskedTextResultHint.Success;
      testPosition = 0;
      string textCore = this.ParseTextCore(input, out testPosition, out resultHint);
      if (resultHint != MaskedTextResultHint.Success)
        return false;
      this.value = textCore;
      this.textBoxItem.Text = textCore;
      return true;
    }

    public void KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == 46)
      {
        this.HandleDeleteByDel();
        e.Handled = true;
      }
      if (e.KeyCode == Keys.Up)
      {
        this.HandleUp();
        e.Handled = true;
      }
      if (e.KeyCode != Keys.Down)
        return;
      this.HandleDown();
      e.Handled = true;
    }

    private void HandleDown()
    {
      int selectionStart = this.textBoxItem.SelectionStart;
      if (this.textBoxItem.SelectionLength > 0)
        return;
      string text = this.textBoxItem.Text;
      this.UpdatePreviousChar(false, ref text, this.textBoxItem.SelectionStart - 1);
      this.textBoxItem.Text = text;
      this.textBoxItem.SelectionStart = selectionStart;
    }

    private void HandleUp()
    {
      int selectionStart = this.textBoxItem.SelectionStart;
      if (this.textBoxItem.SelectionLength > 0)
        return;
      string text = this.textBoxItem.Text;
      this.UpdatePreviousChar(true, ref text, this.textBoxItem.SelectionStart - 1);
      this.textBoxItem.Text = text;
      this.textBoxItem.SelectionStart = selectionStart;
    }

    private bool UpdatePreviousChar(bool up, ref string text, int pos)
    {
      for (; pos >= 0; --pos)
      {
        char c = text[pos];
        if (char.IsDigit(c))
        {
          int num1 = int.Parse(c.ToString());
          int num2;
          if (up)
          {
            num2 = num1 + 1;
            if (num2 > 9)
            {
              num2 = 0;
              this.UpdatePreviousChar(up, ref text, pos - 1);
            }
          }
          else
          {
            num2 = num1 - 1;
            if (num2 < 0)
            {
              num2 = 9;
              this.UpdatePreviousChar(up, ref text, pos - 1);
            }
          }
          text = this.ReplaceAt(ref text, pos, num2.ToString());
          this.Validate(text);
          if (double.TryParse(this.Replace(text, this.culture.NumberFormat.PercentSymbol, this.culture.NumberFormat.CurrencySymbol), out this.floatValue))
            this.floatValue /= 100.0;
          return true;
        }
      }
      return false;
    }

    private string ReplaceAt(ref string text, int pos, string newChar)
    {
      return text.Remove(pos, 1).Insert(pos, newChar);
    }

    public void KeyPress(object sender, KeyPressEventArgs e)
    {
      bool flag1 = this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency;
      if (e.KeyChar == '.')
        e.KeyChar = !flag1 ? this.culture.NumberFormat.NumberDecimalSeparator[0] : this.culture.NumberFormat.CurrencyDecimalSeparator[0];
      if (e.KeyChar == '\b')
      {
        this.HandleDeleteByBack();
        e.Handled = true;
      }
      else if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
      {
        e.Handled = true;
      }
      else
      {
        if (this.textBoxItem.SelectionLength > 0)
        {
          this.textBoxItem.ShouldTextChangedFire = false;
          this.HandleDeleteByDel();
          this.textBoxItem.ShouldTextChangedFire = true;
        }
        int selectionStart = this.textBoxItem.SelectionStart;
        int textLength = this.textBoxItem.TextLength;
        if (!this.AllowAppendCharacters())
        {
          SystemSounds.Beep.Play();
          e.Handled = true;
        }
        else
        {
          char lower = char.ToLower(e.KeyChar);
          if ((int) lower == (int) char.ToLower(this.culture.NumberFormat.CurrencyDecimalSeparator[0]) && flag1 || (int) lower == (int) char.ToLower(this.culture.NumberFormat.NumberDecimalSeparator[0]) && !flag1)
          {
            int num = this.textBoxItem.Text.ToLower().LastIndexOf(lower);
            if (num > -1)
            {
              this.textBoxItem.SelectionStart = num + 1 > this.textBoxItem.Text.Length ? num : num + 1;
              e.Handled = true;
              return;
            }
          }
          do
          {
            string text = this.textBoxItem.Text;
            int length1 = text.Length;
            char minValue = char.MinValue;
            for (; selectionStart < text.Length; ++selectionStart)
            {
              minValue = text[selectionStart];
              string str = minValue.ToString();
              if (str == this.culture.NumberFormat.CurrencyDecimalSeparator && flag1 || str == this.culture.NumberFormat.NumberDecimalSeparator && !flag1 || str == this.culture.NumberFormat.PercentSymbol && !flag1 || str == " " && (selectionStart > 0 && (int) text[selectionStart - 1] != (int) this.culture.NumberFormat.CurrencySymbol[0] || selectionStart == 0) || char.IsDigit(minValue))
                break;
            }
            string str1 = this.Replace(text, " ", this.culture.NumberFormat.CurrencySymbol, this.culture.NumberFormat.PercentSymbol);
            bool flag2 = selectionStart >= this.Replace(text, " ").Length;
            int decimalSeparator = this.GetIndexOfDecimalSeparator(text);
            bool flag3 = char.IsPunctuation(minValue) || minValue.ToString() == this.culture.NumberFormat.PercentSymbol || (int) e.KeyChar == (int) this.culture.NumberFormat.NegativeSign[0] || (int) char.ToLower(minValue) == (int) char.ToLower(this.culture.NumberFormat.NumberDecimalSeparator[0]) && !flag1 || (int) char.ToLower(minValue) == (int) char.ToLower(this.culture.NumberFormat.CurrencyDecimalSeparator[0]) && flag1;
            bool flag4 = flag2 && this.numericType != NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Standard && (this.numericType != NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Decimal && this.numericType != NumericCharacterTextBoxProvider.RadNumericMaskFormatType.FixedPoint) && this.numericType != NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency && this.numericType != NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Percent;
            bool flag5 = (selectionStart < decimalSeparator || decimalSeparator == -1 && this.numericType != NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Standard && (this.numericType != NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Percent && this.numericType != NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency)) && (int) minValue != (int) this.PromptChar && minValue != ' ';
            bool flag6 = decimalSeparator == -1 && str1 != "0" && ((this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Standard || this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Decimal || (this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.FixedPoint || this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency) || this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Percent) && (this.mask.Length == 2 && this.mask.EndsWith("0")));
            if (flag3 || flag4 || (flag5 || flag6))
            {
              if ((int) e.KeyChar == (int) this.culture.NumberFormat.NegativeSign[0])
              {
                if (!text.Contains(this.culture.NumberFormat.NegativeSign))
                {
                  MaskedTextResultHint resultHint = MaskedTextResultHint.Success;
                  int testPosition = 0;
                  text = this.ParseTextCore(this.culture.NumberFormat.NegativeSign + text, out testPosition, out resultHint);
                }
              }
              else if (selectionStart <= text.Length && (!flag2 || decimalSeparator == -1))
                text = text.Insert(selectionStart, e.KeyChar.ToString());
            }
            else
            {
              if ((selectionStart == text.Length || !char.IsDigit(text[selectionStart]) && decimalSeparator > -1) && selectionStart > 0)
                --selectionStart;
              if (text.Length == 0)
              {
                text = e.KeyChar.ToString();
                ++selectionStart;
              }
              else if (selectionStart < text.Length && text.Length > 0)
              {
                bool flag7 = this.textBoxItem.RightToLeft && ((int) text[selectionStart] == (int) this.culture.NumberFormat.CurrencySymbol[0] || (int) text[selectionStart] == (int) this.culture.NumberFormat.PercentSymbol[0]);
                if ((text[selectionStart] == ' ' || flag7) && (selectionStart < text.Length && selectionStart < str1.Length))
                  ++selectionStart;
                text = text.Remove(selectionStart, 1).Insert(selectionStart, e.KeyChar.ToString());
                ++selectionStart;
              }
            }
            bool flag8 = this.Validate(text);
            int num1 = 0;
            if (!text.Contains("(") && this.textBoxItem.Text.Contains("("))
              num1 = 1;
            int num2 = this.textBoxItem.Text.Length - num1;
            if (flag8)
            {
              int length2 = this.textBoxItem.Text.Length;
              if (length1 == 0)
              {
                this.textBoxItem.SelectionStart = this.textBoxItem.Text.Length <= 0 || char.IsDigit(this.textBoxItem.Text[0]) ? 1 : 2;
                break;
              }
              int val2 = selectionStart + (num2 - length1);
              if (val2 <= length2)
              {
                if (val2 > 0 && this.textBoxItem.Text[val2 - 1] == ' ')
                  --val2;
                this.textBoxItem.SelectionStart = Math.Max(0, val2);
                break;
              }
              this.textBoxItem.SelectionStart = this.textBoxItem.Text.Length;
              break;
            }
            ++selectionStart;
            this.textBoxItem.SelectionStart = selectionStart;
          }
          while (selectionStart < textLength);
          e.Handled = true;
        }
      }
    }

    public bool Validate(string value)
    {
      int testPosition = 0;
      MaskedTextResultHint resultHint = MaskedTextResultHint.Success;
      if (this.mask != "<>" && value != null)
      {
        this.Set(value, out testPosition, out resultHint);
        if (resultHint > MaskedTextResultHint.Unknown)
        {
          string str = this.ToString(true, true);
          ValueChangingEventArgs changingEventArgs = new ValueChangingEventArgs((object) str, (object) value);
          this.owner.CallValueChanging((CancelEventArgs) changingEventArgs);
          if (changingEventArgs.Cancel)
            return false;
          this.textBoxItem.Text = str;
          this.owner.isNullValue = false;
          this.owner.CallValueChanged(EventArgs.Empty);
          return true;
        }
      }
      return false;
    }

    public bool RemoveAt(int startPosition, int endPosition)
    {
      string text = this.textBoxItem.Text;
      int decimalSeparator = this.GetIndexOfDecimalSeparator(text);
      if ((this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Decimal || this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Percent || this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency || this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.FixedPoint && this.mask.Length == 1 || this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Standard && (this.mask.Length == 1 || this.mask.Length == 2 && this.mask.EndsWith("0"))) && decimalSeparator == -1)
      {
        if (startPosition >= text.Length)
          return false;
        this.Validate(text.Remove(startPosition, endPosition - startPosition));
        return true;
      }
      if (decimalSeparator == -1)
      {
        StringBuilder stringBuilder = new StringBuilder(text.Length);
        for (int index = 0; index < text.Length; ++index)
        {
          if (index >= startPosition && index < endPosition)
            stringBuilder.Append(this.promptChar);
          else
            stringBuilder.Append(text[index]);
        }
        this.Validate(stringBuilder.ToString());
        return true;
      }
      if (startPosition < decimalSeparator)
      {
        StringBuilder stringBuilder = new StringBuilder(text.Length);
        for (int index = 0; index <= decimalSeparator; ++index)
          stringBuilder.Append(text[index]);
        for (int index = decimalSeparator + 1; index < text.Length; ++index)
        {
          if (index >= startPosition && index < endPosition)
            stringBuilder.Append(this.promptChar);
          else
            stringBuilder.Append(text[index]);
        }
        while (!char.IsDigit(stringBuilder[startPosition]) && (int) stringBuilder[startPosition] != (int) this.Culture.NumberFormat.NegativeSign[0])
        {
          ++startPosition;
          ++endPosition;
          if (endPosition >= stringBuilder.Length - 1)
            endPosition = stringBuilder.Length - 1;
        }
        this.Validate(stringBuilder.ToString().Remove(startPosition, Math.Min(decimalSeparator, endPosition) - startPosition));
        return true;
      }
      StringBuilder stringBuilder1 = new StringBuilder(text.Length);
      for (int index = decimalSeparator + 1; index < text.Length; ++index)
      {
        if (index >= startPosition && index < endPosition)
          stringBuilder1.Append(this.promptChar);
        else
          stringBuilder1.Append(text[index]);
      }
      this.Validate(text.Substring(0, decimalSeparator + 1) + stringBuilder1.ToString());
      return true;
    }

    private void HandleDeleteByBack()
    {
      if (this.textBoxItem.SelectionLength > 0)
      {
        this.HandleDeleteByDel();
      }
      else
      {
        int selectionStart = this.textBoxItem.SelectionStart;
        int startPosition = this.textBoxItem.SelectionLength == 0 ? selectionStart - 1 : selectionStart;
        if (startPosition < 0 && this.textBoxItem.SelectionLength == 0)
          return;
        int length = this.textBoxItem.Text.Length;
        char ch = this.textBoxItem.Text[startPosition];
        if ((int) ch == (int) this.Culture.NumberFormat.NegativeSign[0])
        {
          this.Validate(this.Replace(this.textBoxItem.Text, this.Culture.NumberFormat.NegativeSign));
        }
        else
        {
          if ((int) ch == (int) this.Culture.NumberFormat.NumberGroupSeparator[0])
            --startPosition;
          this.RemoveAt(startPosition, startPosition + Math.Max(this.textBoxItem.SelectionLength, 1));
        }
        this.textBoxItem.Text = this.ToString(true, true);
        this.textBoxItem.SelectionLength = 0;
        int num = startPosition;
        if (length != this.textBoxItem.Text.Length)
          num -= length - this.textBoxItem.Text.Length - 1;
        bool flag = this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency;
        if (num <= 0)
          num = 0;
        else if (num < this.textBoxItem.Text.Length && !char.IsDigit(this.textBoxItem.Text[num - 1]) && (int) this.Culture.NumberFormat.NegativeSign[0] != (int) this.textBoxItem.Text[num - 1] && ((int) this.Culture.NumberFormat.NumberDecimalSeparator[0] != (int) this.textBoxItem.Text[num - 1] && !flag || (int) this.Culture.NumberFormat.CurrencyDecimalSeparator[0] != (int) this.textBoxItem.Text[num - 1] && flag))
          --num;
        this.textBoxItem.SelectionStart = num;
      }
    }

    private void HandleDeleteByDel()
    {
      int length = this.textBoxItem.Text.Length;
      int index = this.textBoxItem.SelectionStart;
      int startPosition = index;
      if (startPosition >= this.textBoxItem.Text.Length)
        return;
      int num1 = this.GetIndexOfDecimalSeparator(this.textBoxItem.Text);
      if (num1 < 0)
        num1 = this.textBoxItem.Text.Length;
      int selectionLength = this.textBoxItem.SelectionLength;
      int num2 = index + selectionLength;
      int num3 = num1 - num2;
      if (num3 < 0)
        num3 = 0;
      char c = this.textBoxItem.Text[startPosition];
      if ((int) c == (int) this.Culture.NumberFormat.NumberGroupSeparator[0])
      {
        if (selectionLength == 1)
          return;
        ++startPosition;
        if (selectionLength > 0)
          --this.textBoxItem.SelectionLength;
      }
      this.RemoveAt(startPosition, startPosition + Math.Max(this.textBoxItem.SelectionLength, 1));
      this.textBoxItem.Text = this.ToString(true, true);
      this.textBoxItem.SelectionLength = 0;
      if (length != this.textBoxItem.Text.Length)
      {
        if (selectionLength == 0)
        {
          index -= length - this.textBoxItem.Text.Length - 1;
          if (index < 0)
            index = 0;
          else if (index < this.textBoxItem.Text.Length && (!char.IsDigit(c) && (int) this.Culture.NumberFormat.NegativeSign[0] != (int) c || !char.IsDigit(this.textBoxItem.Text[index]) && (int) this.textBoxItem.Text[index] != (int) this.culture.NumberFormat.CurrencyDecimalSeparator[0]))
            ++index;
        }
        else
        {
          int num4 = this.GetIndexOfDecimalSeparator(this.textBoxItem.Text);
          if (num4 < 0)
            num4 = this.textBoxItem.Text.Length;
          index = num4 - num3;
          if (index < 0)
            index = 0;
        }
      }
      this.textBoxItem.SelectionStart = index;
    }

    public bool Delete()
    {
      this.HandleDeleteByDel();
      return true;
    }

    protected virtual string ParseText(string value)
    {
      int testPosition;
      MaskedTextResultHint resultHint;
      string textCore = this.ParseTextCore(value, out testPosition, out resultHint);
      if (resultHint == MaskedTextResultHint.Success)
        return textCore;
      return value;
    }

    public double PercentageFloatValue
    {
      get
      {
        return this.floatValue;
      }
    }

    protected virtual string ParseTextCore(
      string value,
      out int testPosition,
      out MaskedTextResultHint resultHint)
    {
      testPosition = 0;
      resultHint = MaskedTextResultHint.Success;
      string parsedInput = string.Empty;
      value = this.Replace(value, this.culture.NumberFormat.CurrencySymbol, this.culture.NumberFormat.PercentSymbol);
      value = value.Trim();
      if (string.IsNullOrEmpty(value) || value == this.culture.NumberFormat.NegativeSign)
        value = "0";
      value = this.ReplaceGroupSeparator(value);
      value = this.Replace(value, " ");
      try
      {
        switch (this.numericType)
        {
          case NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency:
            bool flag = value.IndexOf("(") > -1;
            value = this.Replace(value, "(", ")");
            value = this.ReplaceCurrencyDecimalSeparatorWithNumberDecimal(value);
            if (flag && value.IndexOfAny("123456789".ToCharArray()) > -1 && !value.StartsWith(this.culture.NumberFormat.NegativeSign))
              value = this.culture.NumberFormat.NegativeSign + value;
            parsedInput = string.Format((IFormatProvider) this.Culture, "{0:" + this.Mask + "}", (object) Decimal.Parse(value, (IFormatProvider) this.Culture));
            break;
          case NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Standard:
            parsedInput = string.Format((IFormatProvider) this.Culture, "{0:" + this.Mask + "}", (object) (long) Decimal.Parse(value, (IFormatProvider) this.Culture));
            break;
          case NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Percent:
            this.floatValue = double.Parse(value, (IFormatProvider) this.Culture);
            this.floatValue /= 100.0;
            parsedInput = string.Format((IFormatProvider) this.Culture, "{0:" + this.Mask + "}", (object) this.floatValue);
            break;
          case NumericCharacterTextBoxProvider.RadNumericMaskFormatType.FixedPoint:
            parsedInput = string.Format((IFormatProvider) this.Culture, "{0:" + this.Mask + "}", (object) Decimal.Parse(value, (IFormatProvider) this.Culture));
            break;
          case NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Decimal:
            parsedInput = string.Format((IFormatProvider) this.Culture, "{0:" + this.Mask + "}", (object) Decimal.Parse(value, (IFormatProvider) this.Culture));
            break;
        }
        parsedInput = this.EnsureMinusSign(value, parsedInput);
      }
      catch
      {
        resultHint = MaskedTextResultHint.UnavailableEditPosition;
      }
      return parsedInput;
    }

    protected virtual bool AllowAppendCharacters()
    {
      if (this.owner.Value == null || this.owner.Text == string.Empty)
        return true;
      if (this.textBoxItem.SelectionStart < this.textBoxItem.Text.TrimEnd(' ', this.Culture.NumberFormat.CurrencySymbol[0], this.Culture.NumberFormat.PercentSymbol[0]).Length)
        return true;
      switch (char.ToLower(this.Mask[0]))
      {
        case 'd':
          if (this.Mask.Length == 1)
            return true;
          break;
        case 'g':
          return true;
      }
      return this.Mask.Length == 2 && this.Mask[1] == '0';
    }

    private int GetIndexOfDecimalSeparator(string text)
    {
      return this.numericType != NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency ? this.GetIndexOfNumberDecimalSeparator(text) : this.GetIndexOfCurrencyDecimalSeparator(text);
    }

    private int GetIndexOfCurrencyDecimalSeparator(string text)
    {
      if (this.Culture.NumberFormat.CurrencyDecimalSeparator != this.Culture.NumberFormat.CurrencyGroupSeparator)
        return text.IndexOf(this.Culture.NumberFormat.CurrencyDecimalSeparator);
      int num1 = this.GetMaskNumber();
      if (num1 == 0)
        return -1;
      if (num1 < 0)
        num1 = 2;
      int num2 = text.LastIndexOf(this.Culture.NumberFormat.CurrencyDecimalSeparator);
      if (num2 < 0)
        return -1;
      int num3 = text.TrimEnd(' ', this.Culture.NumberFormat.CurrencySymbol[0], this.Culture.NumberFormat.PercentSymbol[0]).Length - num1 - 1;
      if (num3 != num2)
        num3 = -1;
      return num3;
    }

    private int GetIndexOfNumberDecimalSeparator(string text)
    {
      if (this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.None || this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Standard)
        return -1;
      if (this.Culture.NumberFormat.NumberDecimalSeparator != this.Culture.NumberFormat.NumberGroupSeparator)
        return text.IndexOf(this.Culture.NumberFormat.NumberDecimalSeparator);
      int num1 = text.LastIndexOf(this.Culture.NumberFormat.NumberDecimalSeparator);
      if (num1 < 0)
        return -1;
      if (this.Mask.StartsWith("G"))
        return num1;
      int num2 = this.GetMaskNumber();
      if (num2 == 0)
        return -1;
      if (num2 < 0)
        num2 = 2;
      int num3 = text.TrimEnd(' ', this.Culture.NumberFormat.CurrencySymbol[0], this.Culture.NumberFormat.PercentSymbol[0]).Length - num2 - 1;
      if (num3 != num1)
        num3 = -1;
      return num3;
    }

    private int GetMaskNumber()
    {
      bool flag = false;
      int num = 0;
      foreach (char c in this.Mask)
      {
        if (char.IsDigit(c))
        {
          flag = true;
          num = num * 10 + ((int) c - 48);
        }
      }
      if (!flag)
        num = -1;
      return num;
    }

    private string ReplaceDecimalSeparator(string text)
    {
      if (this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency)
        return this.ReplaceCurrencyDecimalSeparator(text);
      return this.ReplaceNumberDecimalSeparator(text);
    }

    private string ReplaceGroupSeparator(string text)
    {
      if (this.numericType == NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency)
        return this.ReplaceCurrencyGroupSeparator(text);
      return this.ReplaceNumberGroupSeparator(text);
    }

    private string ReplaceCurrencyDecimalSeparator(string text)
    {
      int decimalSeparator = this.GetIndexOfCurrencyDecimalSeparator(text);
      if (decimalSeparator < 0)
        return text;
      text = text.Remove(decimalSeparator, this.Culture.NumberFormat.CurrencyDecimalSeparator.Length);
      return text;
    }

    private string ReplaceCurrencyGroupSeparator(string text)
    {
      int decimalSeparator = this.GetIndexOfCurrencyDecimalSeparator(text);
      if (decimalSeparator < 0 || this.Culture.NumberFormat.CurrencyGroupSeparator != this.Culture.NumberFormat.CurrencyDecimalSeparator)
        return this.Replace(text, this.Culture.NumberFormat.CurrencyGroupSeparator);
      return this.Replace(text.Substring(0, decimalSeparator), this.Culture.NumberFormat.CurrencyGroupSeparator) + text.Substring(decimalSeparator);
    }

    private string ReplaceNumberDecimalSeparator(string text)
    {
      int decimalSeparator = this.GetIndexOfNumberDecimalSeparator(text);
      if (decimalSeparator < 0)
        return text;
      text = text.Remove(decimalSeparator, this.Culture.NumberFormat.NumberDecimalSeparator.Length);
      return text;
    }

    private string ReplaceNumberGroupSeparator(string text)
    {
      int decimalSeparator = this.GetIndexOfNumberDecimalSeparator(text);
      if (decimalSeparator < 0 || this.Culture.NumberFormat.NumberGroupSeparator != this.Culture.NumberFormat.NumberDecimalSeparator)
        return this.Replace(text, this.Culture.NumberFormat.NumberGroupSeparator);
      return this.Replace(text.Substring(0, decimalSeparator), this.Culture.NumberFormat.NumberGroupSeparator) + text.Substring(decimalSeparator);
    }

    private string Replace(string text, params string[] toDelete)
    {
      foreach (string oldValue in toDelete)
      {
        if (!string.IsNullOrEmpty(oldValue))
          text = text.Replace(oldValue, string.Empty);
      }
      return text;
    }

    private string ReplaceCurrencyDecimalSeparatorWithNumberDecimal(string text)
    {
      int decimalSeparator = this.GetIndexOfCurrencyDecimalSeparator(text);
      if (decimalSeparator < 0)
        return text;
      text = text.Remove(decimalSeparator, this.culture.NumberFormat.CurrencyDecimalSeparator.Length);
      text = text.Insert(decimalSeparator, this.culture.NumberFormat.NumberDecimalSeparator);
      return text;
    }

    public enum RadNumericMaskFormatType
    {
      None,
      Currency,
      Standard,
      Percent,
      FixedPoint,
      Decimal,
    }
  }
}
