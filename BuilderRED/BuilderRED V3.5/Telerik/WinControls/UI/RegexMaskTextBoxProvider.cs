// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RegexMaskTextBoxProvider
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
  public class RegexMaskTextBoxProvider : IMaskProvider
  {
    private string errorMessage = "Input is not valid";
    private bool allowEmptyString = true;
    private string mask;
    private CultureInfo culture;
    private RadMaskedEditBoxElement owner;
    private RadTextBoxItem textBoxItem;
    protected Regex regex;
    private ErrorProvider errorProvider;

    public RegexMaskTextBoxProvider(
      string mask,
      CultureInfo culture,
      RadMaskedEditBoxElement owner)
    {
      this.mask = mask;
      this.culture = culture;
      this.owner = owner;
      this.textBoxItem = owner.TextBoxItem;
      this.WireEvents();
      this.errorProvider = new ErrorProvider();
      this.errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
    }

    public void WireEvents()
    {
      this.textBoxItem.HostedControl.Leave += new EventHandler(this.HostedControl_Leave);
    }

    public void UnwireEvents()
    {
      this.textBoxItem.HostedControl.Leave -= new EventHandler(this.HostedControl_Leave);
    }

    public ErrorProvider ErrorProvider
    {
      get
      {
        return this.errorProvider;
      }
      set
      {
        this.errorProvider = value;
      }
    }

    public bool IsValid
    {
      get
      {
        if (new Regex(this.mask).IsMatch(this.textBoxItem.Text) || string.IsNullOrEmpty(this.textBoxItem.Text) && this.allowEmptyString)
        {
          if (this.CanSetError)
            this.errorProvider.SetError(this.owner.ElementTree.Control, "");
          return true;
        }
        if (this.CanSetError)
          this.errorProvider.SetError(this.owner.ElementTree.Control, this.errorMessage);
        return false;
      }
    }

    public bool AllowEmptyString
    {
      get
      {
        return this.allowEmptyString;
      }
      set
      {
        this.allowEmptyString = value;
        int num = this.IsValid ? 1 : 0;
      }
    }

    public string ErrorMessage
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

    private void HostedControl_Leave(object sender, EventArgs e)
    {
      if (this.IsValid || !this.CanSetError)
        return;
      this.errorProvider.SetError(this.owner.ElementTree.Control, this.errorMessage);
    }

    public void KeyDown(object sender, KeyEventArgs e)
    {
      if (!this.CanSetError)
        return;
      this.errorProvider.SetError(this.owner.ElementTree.Control, "");
    }

    public void KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!this.CanSetError)
        return;
      this.errorProvider.SetError(this.owner.ElementTree.Control, "");
    }

    private bool CanSetError
    {
      get
      {
        if (this.owner.ElementTree == null)
          return false;
        Control control = this.owner.ElementTree.Control;
        return control != null && !(control.GetType().Name == "RadGridView");
      }
    }

    public bool Validate(string value)
    {
      CancelEventArgs e = new CancelEventArgs();
      this.owner.CallValueChanging(e);
      if (e.Cancel)
        return false;
      this.textBoxItem.Text = value;
      this.regex = new Regex(this.mask);
      if (this.regex.IsMatch(this.textBoxItem.Text) || string.IsNullOrEmpty(this.textBoxItem.Text))
      {
        if (this.CanSetError)
          this.errorProvider.SetError(this.owner.ElementTree.Control, "");
        this.owner.isNullValue = false;
        this.owner.CallValueChanged((EventArgs) e);
        return true;
      }
      if (this.CanSetError)
        this.errorProvider.SetError(this.owner.ElementTree.Control, this.errorMessage);
      return false;
    }

    public bool Click()
    {
      return true;
    }

    public RadTextBoxItem TextBoxItem
    {
      get
      {
        return this.textBoxItem;
      }
    }

    public string ToString(bool includePromt, bool includeLiterals)
    {
      return this.textBoxItem.Text;
    }

    public IMaskProvider Clone()
    {
      return (IMaskProvider) new RegexMaskTextBoxProvider(this.mask, this.culture, this.owner);
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
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public char PromptChar
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public object Value
    {
      get
      {
        return (object) this.textBoxItem.Text;
      }
      set
      {
        this.textBoxItem.Text = value.ToString();
      }
    }

    public bool Delete()
    {
      return true;
    }
  }
}
