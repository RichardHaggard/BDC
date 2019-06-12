// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StandartMaskTextBoxProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class StandartMaskTextBoxProvider : IMaskProvider
  {
    private readonly string mask = "<>";
    protected IMaskCharacterProvider provider;
    protected RadMaskedEditBoxElement owner;
    private readonly RadTextBoxItem textBoxItem;
    private readonly CultureInfo culture;
    private readonly bool allowPromptAsInput;
    private readonly char passwordChar;
    private readonly bool restrictToAscii;
    private int hintPos;
    internal MaskedTextResultHint hint;
    private bool includePrompt;
    private char promptChar;

    public StandartMaskTextBoxProvider(
      string mask,
      CultureInfo culture,
      RadMaskedEditBoxElement owner,
      bool allowPromptAsInput,
      char promptChar,
      char passwordChar,
      bool restrictToAscii)
    {
      this.allowPromptAsInput = allowPromptAsInput;
      this.promptChar = promptChar;
      this.passwordChar = passwordChar;
      this.restrictToAscii = restrictToAscii;
      this.mask = mask;
      this.culture = culture;
      this.owner = owner;
      this.provider = (IMaskCharacterProvider) new StandartCharacterMaskEditBoxProvider(mask, culture, owner, allowPromptAsInput, promptChar, passwordChar, restrictToAscii);
      this.promptChar = this.provider.PromptChar;
      this.textBoxItem = owner.TextBoxItem;
    }

    [Browsable(false)]
    public bool MaskCompleted
    {
      get
      {
        StandartCharacterMaskEditBoxProvider standardProvider = this.TryGetStandardProvider();
        if (standardProvider == null)
          return false;
        return standardProvider.MaskCompleted;
      }
    }

    [Browsable(false)]
    public bool MaskFull
    {
      get
      {
        StandartCharacterMaskEditBoxProvider standardProvider = this.TryGetStandardProvider();
        if (standardProvider == null)
          return false;
        return standardProvider.MaskFull;
      }
    }

    public RadTextBoxItem TextBoxItem
    {
      get
      {
        return this.textBoxItem;
      }
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

    public virtual object Value
    {
      get
      {
        return (object) this.provider.ToString((this.owner.TextMaskFormat & MaskFormat.IncludePrompt) == MaskFormat.IncludePrompt, (this.owner.TextMaskFormat & MaskFormat.IncludeLiterals) == MaskFormat.IncludeLiterals);
      }
      set
      {
        this.Validate(value.ToString());
      }
    }

    public bool Click()
    {
      return false;
    }

    public string ToString(bool includePromt, bool includeLiterals)
    {
      return this.provider.ToString(includePromt, includeLiterals);
    }

    public IMaskProvider Clone()
    {
      return (IMaskProvider) new StandartMaskTextBoxProvider(this.mask, this.culture, this.owner, this.allowPromptAsInput, this.promptChar, this.passwordChar, this.restrictToAscii);
    }

    public bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint)
    {
      return this.provider.Set(input, out testPosition, out resultHint);
    }

    public virtual void KeyPress(object sender, KeyPressEventArgs e)
    {
      this.provider.KeyPress(sender, e);
    }

    public virtual void KeyDown(object sender, KeyEventArgs e)
    {
      this.provider.KeyDown(sender, e);
    }

    public virtual bool Validate(string value)
    {
      CancelEventArgs e = new CancelEventArgs();
      this.owner.CallValueChanging(e);
      if (e.Cancel || !(this.Mask != "<>") || value == null)
        return false;
      this.ValidateCore(value);
      if (this.hint <= MaskedTextResultHint.Unknown)
        return false;
      this.textBoxItem.Text = this.provider.ToString(true, true);
      this.owner.isNullValue = false;
      this.owner.CallValueChanged(EventArgs.Empty);
      return true;
    }

    public virtual StandartCharacterMaskEditBoxProvider TryGetStandardProvider()
    {
      return this.provider as StandartCharacterMaskEditBoxProvider;
    }

    public bool Delete()
    {
      return this.provider.Delete();
    }

    internal void ValidateCore(string value)
    {
      StandartCharacterMaskEditBoxProvider standardProvider = this.TryGetStandardProvider();
      InsertKeyMode insertKeyMode = InsertKeyMode.Default;
      if (standardProvider != null)
      {
        insertKeyMode = standardProvider.InsertKeyMode;
        standardProvider.InsertKeyMode = InsertKeyMode.Overwrite;
      }
      this.provider.Set(value, out this.hintPos, out this.hint);
      if (standardProvider == null)
        return;
      standardProvider.InsertKeyMode = insertKeyMode;
    }
  }
}
