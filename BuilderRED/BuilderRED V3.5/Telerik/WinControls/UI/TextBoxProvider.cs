// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TextBoxProvider : IMaskProvider
  {
    protected RadMaskedEditBoxElement owner;

    public TextBoxProvider(RadMaskedEditBoxElement owner)
    {
      this.owner = owner;
    }

    public void KeyDown(object sender, KeyEventArgs e)
    {
      if (!(this.owner.Text == string.Empty))
        return;
      e.Handled = true;
    }

    public void KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    public bool Validate(string value)
    {
      this.owner.TextBoxItem.Text = value;
      return true;
    }

    public bool Click()
    {
      return true;
    }

    public RadTextBoxItem TextBoxItem
    {
      get
      {
        return this.owner.TextBoxItem;
      }
    }

    public string ToString(bool includePromt, bool includeLiterals)
    {
      return this.owner.Text;
    }

    public IMaskProvider Clone()
    {
      return (IMaskProvider) new TextBoxProvider(this.owner);
    }

    public CultureInfo Culture
    {
      get
      {
        return (CultureInfo) null;
      }
    }

    public string Mask
    {
      get
      {
        return "";
      }
    }

    public bool IncludePrompt
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public char PromptChar
    {
      get
      {
        return ' ';
      }
      set
      {
      }
    }

    public object Value
    {
      get
      {
        return (object) this.owner.Text;
      }
      set
      {
        this.owner.Text = value.ToString();
      }
    }

    public bool Delete()
    {
      this.TextBoxItem.Cut();
      return true;
    }
  }
}
