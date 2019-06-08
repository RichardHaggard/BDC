// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StandartCharacterMaskEditBoxProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class StandartCharacterMaskEditBoxProvider : IMaskCharacterProvider
  {
    private static readonly object ISOVERWRITEMODECHANGEDKEY = new object();
    private string oldValue = string.Empty;
    protected MaskedTextProvider provider;
    protected RadTextBoxItem textBoxItem;
    private readonly RadMaskedEditBoxElement owner;
    private readonly bool allowPromptAsInput;
    private readonly char promptChar;
    private readonly char passwordChar;
    private readonly bool restrictToAscii;
    private readonly string mask;
    private readonly CultureInfo culture;
    private InsertKeyMode insertMode;
    private bool isInsertToggled;

    public StandartCharacterMaskEditBoxProvider(
      string mask,
      CultureInfo culture,
      RadMaskedEditBoxElement owner,
      bool allowPromptAsInput,
      char promptChar,
      char passwordChar,
      bool restrictToAscii)
    {
      this.owner = owner;
      this.allowPromptAsInput = allowPromptAsInput;
      this.promptChar = promptChar;
      this.passwordChar = passwordChar;
      this.restrictToAscii = restrictToAscii;
      this.mask = mask;
      this.culture = culture;
      this.provider = new MaskedTextProvider(mask, culture, allowPromptAsInput, promptChar, passwordChar, restrictToAscii);
      this.textBoxItem = owner.TextBoxItem;
    }

    [Browsable(false)]
    public bool MaskCompleted
    {
      get
      {
        return this.provider.MaskCompleted;
      }
    }

    [Browsable(false)]
    public bool MaskFull
    {
      get
      {
        return this.provider.MaskFull;
      }
    }

    [DefaultValue(InsertKeyMode.Default)]
    [Category("Behavior")]
    [Description("Gets or sets the text insertion mode of the masked text box control.")]
    public InsertKeyMode InsertKeyMode
    {
      get
      {
        return this.insertMode;
      }
      set
      {
        if (!Telerik.WinControls.ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 2))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (InsertKeyMode));
        if (this.insertMode == value)
          return;
        bool isOverwriteMode = this.IsOverwriteMode;
        this.insertMode = value;
        if (isOverwriteMode == this.IsOverwriteMode)
          return;
        this.OnIsOverwriteModeChanged(EventArgs.Empty);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnIsOverwriteModeChanged(EventArgs e)
    {
      EventHandler overwriteModeChanged = this.IsOverwriteModeChanged;
      if (overwriteModeChanged == null)
        return;
      overwriteModeChanged((object) this, e);
    }

    [Category("PropertyChanged")]
    [Description("Occurs after the insert mode has changed.")]
    public event EventHandler IsOverwriteModeChanged;

    [Browsable(false)]
    public bool IsOverwriteMode
    {
      get
      {
        switch (this.insertMode)
        {
          case InsertKeyMode.Default:
            return this.isInsertToggled;
          case InsertKeyMode.Insert:
            return false;
          case InsertKeyMode.Overwrite:
            return true;
          default:
            return false;
        }
      }
    }

    public char PromptChar
    {
      get
      {
        return this.provider.PromptChar;
      }
      set
      {
        this.provider.PromptChar = value;
        this.provider = new MaskedTextProvider(this.mask, this.culture, this.allowPromptAsInput, this.promptChar, this.passwordChar, this.restrictToAscii);
      }
    }

    protected internal MaskedTextProvider InternalProvider
    {
      get
      {
        return this.provider;
      }
    }

    public string ToString(bool includePromt, bool includeLiterals)
    {
      return this.provider.ToString(includePromt, includeLiterals);
    }

    public bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint)
    {
      int testPosition1 = 0;
      testPosition = 0;
      resultHint = MaskedTextResultHint.NoEffect;
      foreach (char input1 in input)
      {
        if (!this.IsOverwriteMode && this.owner.MaskType != MaskType.IP)
          this.provider.InsertAt(input1, testPosition1, out testPosition1, out resultHint);
        else
          this.provider.Replace(input1, testPosition1, out testPosition1, out resultHint);
        if (resultHint > MaskedTextResultHint.Unknown)
          ++testPosition1;
      }
      if (testPosition1 > 0)
        resultHint = MaskedTextResultHint.Success;
      return true;
    }

    public bool RemoveAt(int startPosition, int endPosition)
    {
      return this.provider.RemoveAt(startPosition, endPosition);
    }

    public void KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == 46)
      {
        string text = this.textBoxItem.Text;
        this.HandleDeleteByDel();
        e.Handled = true;
        if (text != this.textBoxItem.Text)
        {
          this.owner.isNullValue = false;
          this.owner.CallValueChanged(EventArgs.Empty);
        }
      }
      if (e.KeyCode != Keys.Insert || e.Modifiers != Keys.None || this.insertMode != InsertKeyMode.Default)
        return;
      this.isInsertToggled = !this.isInsertToggled;
      this.OnIsOverwriteModeChanged(EventArgs.Empty);
    }

    public void KeyPress(object sender, KeyPressEventArgs e)
    {
      int selectionStart = this.textBoxItem.SelectionStart;
      int num1 = this.textBoxItem.TextLength;
      if (e.KeyChar == '\b')
      {
        string text = this.textBoxItem.Text;
        this.HandleDeleteByBack();
        e.Handled = true;
        if (text != this.textBoxItem.Text)
        {
          this.owner.isNullValue = false;
          this.owner.CallValueChanged(EventArgs.Empty);
        }
      }
      if (num1 == 0)
      {
        for (int index = 0; index < this.mask.Length; ++index)
        {
          if ("(){}-".IndexOf(this.mask[index].ToString()) > -1)
            this.textBoxItem.Text += (string) (object) this.mask[index];
          else
            this.textBoxItem.Text += (string) (object) ' ';
        }
        num1 = this.textBoxItem.Text.Length;
      }
      if (selectionStart == num1)
      {
        e.Handled = true;
      }
      else
      {
        do
        {
          string text1 = this.textBoxItem.Text;
          int length1 = text1.Length;
          bool flag1 = this.owner.MaskType == MaskType.IP && this.textBoxItem.Text[selectionStart] == '.' && this.textBoxItem.Text.LastIndexOf('.') == selectionStart;
          string str = text1.Remove(selectionStart, 1).Insert(selectionStart, e.KeyChar.ToString());
          if (this.owner.MaskType != MaskType.IP && this.owner.MaskType != MaskType.Standard)
            str = str.Trim();
          if (!this.IsOverwriteMode && this.owner.MaskType != MaskType.IP)
          {
            if (this.textBoxItem.SelectionLength > 0)
            {
              int endPosition = selectionStart + this.textBoxItem.SelectionLength - 1;
              int testPosition = selectionStart;
              MaskedTextResultHint resultHint = MaskedTextResultHint.NoEffect;
              this.InternalProvider.Replace(e.KeyChar, selectionStart, endPosition, out testPosition, out resultHint);
              string text2 = this.textBoxItem.Text;
              this.textBoxItem.Text = this.InternalProvider.ToString(true, true);
              if (text2 != this.textBoxItem.Text)
              {
                this.owner.isNullValue = false;
                this.owner.CallValueChanged(EventArgs.Empty);
              }
              this.textBoxItem.SelectionStart = testPosition + 1;
              e.Handled = true;
              return;
            }
            int editPositionFrom = this.InternalProvider.FindEditPositionFrom(selectionStart, true);
            if (editPositionFrom > -1 && this.InternalProvider.InsertAt(e.KeyChar, selectionStart))
            {
              string text2 = this.textBoxItem.Text;
              this.textBoxItem.Text = this.InternalProvider.ToString(true, true);
              if (text2 != this.textBoxItem.Text)
              {
                this.owner.isNullValue = false;
                this.owner.CallValueChanged(EventArgs.Empty);
                this.textBoxItem.SelectionStart = editPositionFrom + 1;
              }
              else
                this.textBoxItem.SelectionStart = editPositionFrom == selectionStart ? editPositionFrom + 1 : editPositionFrom;
            }
            e.Handled = true;
            return;
          }
          bool flag2 = this.Validate(str);
          int length2 = this.textBoxItem.Text.Length;
          if (flag2)
          {
            int num2 = selectionStart + 1 + (length2 - length1);
            if (num2 < num1)
            {
              this.textBoxItem.SelectionStart = num2;
              if (flag1)
              {
                ++this.textBoxItem.SelectionStart;
                break;
              }
              break;
            }
            this.textBoxItem.SelectionStart = num1;
            break;
          }
          ++selectionStart;
        }
        while (selectionStart < num1);
        e.Handled = true;
      }
    }

    public bool Validate(string value)
    {
      CancelEventArgs e = new CancelEventArgs();
      this.owner.CallValueChanging(e);
      if (e.Cancel)
        return false;
      int testPosition = 0;
      MaskedTextResultHint resultHint = MaskedTextResultHint.Success;
      if (this.provider.Mask != "<>" && value != null)
      {
        this.provider.Set(value.TrimEnd(' '), out testPosition, out resultHint);
        if (resultHint > MaskedTextResultHint.Unknown)
        {
          this.oldValue = this.provider.ToString(true, true);
          if (this.textBoxItem.Text == this.oldValue)
            return true;
          this.textBoxItem.Text = this.oldValue;
          this.owner.isNullValue = false;
          this.owner.CallValueChanged(EventArgs.Empty);
          return true;
        }
      }
      return false;
    }

    public bool Delete()
    {
      this.HandleDeleteByDel();
      return true;
    }

    private void HandleDeleteByBack()
    {
      MaskedTextResultHint resultHint = MaskedTextResultHint.Success;
      int testPosition = 0;
      int selectionStart = this.textBoxItem.SelectionStart;
      int startPosition = this.textBoxItem.SelectionLength == 0 ? selectionStart - 1 : selectionStart;
      if (startPosition < 0 && this.textBoxItem.SelectionLength == 0)
        return;
      int endPosition = startPosition + this.textBoxItem.SelectionLength - 1;
      if (endPosition <= startPosition)
        endPosition = startPosition;
      if (endPosition >= this.textBoxItem.Text.Length)
        endPosition = this.textBoxItem.Text.Length - 1;
      this.provider.RemoveAt(startPosition, endPosition, out testPosition, out resultHint);
      this.textBoxItem.Text = this.provider.ToString(true, true);
      this.textBoxItem.SelectionLength = 0;
      this.textBoxItem.SelectionStart = startPosition;
    }

    private void HandleDeleteByDel()
    {
      MaskedTextResultHint resultHint = MaskedTextResultHint.Success;
      int selectionStart = this.textBoxItem.SelectionStart;
      int selectionLength = this.textBoxItem.SelectionLength;
      int startPosition = selectionStart;
      int testPosition = startPosition;
      if (this.owner.MaskType == MaskType.IP)
      {
        if (selectionLength == 0 || selectionLength == this.textBoxItem.Text.Length)
        {
          this.provider.Replace(' ', startPosition, startPosition + (selectionLength > 0 ? selectionLength - 1 : 0), out testPosition, out resultHint);
          ++startPosition;
        }
        else
        {
          int num = 0;
          string input = this.textBoxItem.Text;
          for (int startIndex = startPosition; startIndex < startPosition + selectionLength; ++startIndex)
          {
            input = input.Remove(startIndex, 1).Insert(startIndex, this.PromptChar.ToString());
            ++num;
          }
          startPosition += num;
          this.provider.Set(input);
        }
      }
      else
        this.provider.RemoveAt(startPosition, startPosition + (selectionLength > 0 ? selectionLength - 1 : 0), out testPosition, out resultHint);
      this.textBoxItem.Text = this.provider.ToString(true, true);
      this.textBoxItem.SelectionLength = 0;
      this.textBoxItem.SelectionStart = startPosition;
    }
  }
}
