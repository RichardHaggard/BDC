// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IPMaskTextBoxProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class IPMaskTextBoxProvider : StandartMaskTextBoxProvider
  {
    private string errorMessage = "Value should be between 0 and 255";

    public IPMaskTextBoxProvider(
      CultureInfo culture,
      RadMaskedEditBoxElement owner,
      bool allowPromptAsInput,
      char promptChar,
      char passwordChar,
      bool restrictToAscii)
      : base("###.###.###.###", culture, owner, allowPromptAsInput, promptChar, passwordChar, restrictToAscii)
    {
    }

    public virtual string ErrorMessage
    {
      get
      {
        return this.errorMessage;
      }
      set
      {
        this.errorMessage = value;
      }
    }

    public override void KeyPress(object sender, KeyPressEventArgs e)
    {
      if (char.IsDigit(e.KeyChar))
      {
        int selectionStart = this.TextBoxItem.SelectionStart;
        int index = this.TextBoxItem.Text.IndexOfAny(new char[2]{ '.', ',' }, selectionStart) - 1;
        if (index < -1)
          index = this.TextBoxItem.Text.Length - 1;
        if (index < 0 && this.owner.EnableNullValueInput && this.owner.Value == null)
          ++this.TextBoxItem.SelectionStart;
        else if (this.TextBoxItem.Text[index] == ' ' && this.TextBoxItem.Text[index - 1] == ' ' && this.TextBoxItem.Text[index - 2] == ' ' && (selectionStart == 0 || ".,".IndexOf(this.TextBoxItem.Text[selectionStart - 1]) > -1))
          ++this.TextBoxItem.SelectionStart;
        else if (this.TextBoxItem.Text[index] == ' ' && this.TextBoxItem.Text[index - 1] != ' ' && this.TextBoxItem.Text[index - 2] == ' ' && (selectionStart == this.TextBoxItem.Text.Length - 1 || index + 1 < this.TextBoxItem.Text.Length && ".,".IndexOf(this.TextBoxItem.Text[index + 1]) > -1))
        {
          char ch = this.TextBoxItem.Text[index - 1];
          this.TextBoxItem.Text = this.TextBoxItem.Text.Remove(index - 1, 1);
          this.TextBoxItem.Text = this.TextBoxItem.Text.Insert(index - 2, ch.ToString());
          this.TextBoxItem.SelectionStart = index - 1;
        }
      }
      base.KeyPress(sender, e);
      int num1 = -1;
      int selectionStart1 = this.TextBoxItem.SelectionStart;
      for (int index = selectionStart1 - 1; index >= 0; --index)
      {
        if (this.TextBoxItem.Text[index] == '.' || this.TextBoxItem.Text[index] == ',')
        {
          num1 = index;
          break;
        }
      }
      if (e.KeyChar == '\b')
        return;
      string s = this.TextBoxItem.Text.Substring(num1 + 1, Math.Min(selectionStart1 - num1 + 1, 3));
      int result = 0;
      if (!int.TryParse(s, out result) || result <= (int) byte.MaxValue && result >= 0)
        return;
      string str1 = result < 0 ? "000" : "255";
      int num2 = (int) RadMessageBox.Show(this.errorMessage);
      result = (int) byte.MaxValue;
      string str2 = this.TextBoxItem.Text.Substring(0, num1 + 1) + str1 + this.TextBoxItem.Text.Substring(num1 + 4);
      this.Validate(str2);
      this.TextBoxItem.Text = str2;
      this.TextBoxItem.SelectionStart = selectionStart1;
    }

    protected virtual void AdjustAligment()
    {
      int selectionStart = this.TextBoxItem.SelectionStart;
      if (selectionStart <= 0)
        return;
      int num = (int) this.TextBoxItem.Text[selectionStart - 1];
    }

    public override string ToString()
    {
      return this.provider.ToString(false, true);
    }

    public override object Value
    {
      get
      {
        return (object) this.ToString(false, true);
      }
      set
      {
        this.SetValueCore(value);
      }
    }

    protected virtual void SetValueCore(object value)
    {
      string str = value.ToString();
      this.TextBoxItem.SelectAll();
      this.provider.Delete();
      foreach (char keyChar in str)
        this.KeyPress((object) this, new KeyPressEventArgs(keyChar));
    }
  }
}
