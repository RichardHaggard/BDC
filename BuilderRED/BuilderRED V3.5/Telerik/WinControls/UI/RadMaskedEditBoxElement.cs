// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMaskedEditBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadMaskedEditBoxElement : RadTextBoxElement
  {
    private char passwordChar = '*';
    private string mask = "";
    private char promptChar = '_';
    private MaskFormat textMasMaskFormat = MaskFormat.IncludePromptAndLiterals;
    private bool enableMouseWheel = true;
    private bool enableArrowKeys = true;
    private MaskedTextResultHint hint = MaskedTextResultHint.NoEffect;
    private string cachedMask = "";
    private DateTime minDate = DateTime.MinValue;
    private DateTime maxDate = DateTime.MaxValue;
    private const int ArabicYearMax = 1501;
    private IMaskProvider provider;
    private CultureInfo culture;
    private string unmaskedText;
    private MaskType maskType;
    private bool allowPromptAsInput;
    private bool isKeyBoard;
    private RadContextMenu contextMenu;
    private bool contextMenuEnabled;
    private int hintPos;
    private bool restrictToAscii;
    internal bool isNullValue;
    private bool enableNullValueInput;

    static RadMaskedEditBoxElement()
    {
      RadElement.CanFocusProperty.OverrideMetadata(typeof (RadMaskedEditBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    }

    public RadMaskedEditBoxElement()
    {
      this.CreateMaskProvider();
      this.TextBoxItem.KeyPress += new KeyPressEventHandler(this.TextBoxItem_KeyPress);
      this.TextBoxItem.KeyDown += new KeyEventHandler(this.TextBoxItem_KeyDown);
      this.TextBoxItem.HostedControl.Click += new EventHandler(this.TextBoxItem_Click);
      this.TextBoxItem.MouseWheel += new MouseEventHandler(this.TextBoxItem_MouseWheel);
      this.TextBoxItem.MouseUp += new MouseEventHandler(this.TextBoxItem_MouseUp);
      this.TextBoxItem.Validating += new CancelEventHandler(this.TextBoxItem_Validating);
      this.TextBoxItem.HostedControl.ContextMenu = new System.Windows.Forms.ContextMenu();
    }

    private void TextBoxItem_Validating(object sender, CancelEventArgs e)
    {
      FreeFormDateTimeProvider provider1 = this.Provider as FreeFormDateTimeProvider;
      if (provider1 != null)
      {
        provider1.TryParse();
      }
      else
      {
        MaskDateTimeProvider provider2 = this.Provider as MaskDateTimeProvider;
        if (provider2 == null || this.Value == null)
          return;
        provider2.ValidateRange();
      }
    }

    protected override void DisposeManagedResources()
    {
      this.TextBoxItem.KeyPress -= new KeyPressEventHandler(this.TextBoxItem_KeyPress);
      this.TextBoxItem.KeyDown -= new KeyEventHandler(this.TextBoxItem_KeyDown);
      this.TextBoxItem.HostedControl.Click -= new EventHandler(this.TextBoxItem_Click);
      this.TextBoxItem.MouseWheel -= new MouseEventHandler(this.TextBoxItem_MouseWheel);
      this.TextBoxItem.MouseUp -= new MouseEventHandler(this.TextBoxItem_MouseUp);
      this.TextBoxItem.Validating -= new CancelEventHandler(this.TextBoxItem_Validating);
      base.DisposeManagedResources();
    }

    protected virtual void CreateMaskProvider()
    {
      if (this.culture == null)
        this.culture = CultureInfo.CurrentCulture;
      if (this.mask == "s")
        this.mask = "";
      this.UnwireEvents();
      switch (this.maskType)
      {
        case MaskType.None:
          this.provider = (IMaskProvider) new TextBoxProvider(this);
          base.Text = "";
          break;
        case MaskType.DateTime:
          if (string.IsNullOrEmpty(this.Mask))
            this.mask = "g";
          MaskDateTimeProvider provider = this.provider as MaskDateTimeProvider;
          MaskDateTimeProvider dateTimeProvider1 = this.culture.Calendar is PersianCalendar || this.culture.Calendar is UmAlQuraCalendar || this.culture.Calendar is HijriCalendar ? (MaskDateTimeProvider) new ArabicMaskDateTimeProvider(this.mask, this.culture, this) : new MaskDateTimeProvider(this.mask, this.culture, this);
          dateTimeProvider1.MaxDate = this.maxDate;
          dateTimeProvider1.MinDate = this.minDate;
          if (provider != null)
          {
            dateTimeProvider1.value = provider.value;
            dateTimeProvider1.AutoCompleteYear = provider.AutoCompleteYear;
            dateTimeProvider1.AutoSelectNextPart = provider.AutoSelectNextPart;
          }
          this.provider = (IMaskProvider) dateTimeProvider1;
          base.Text = this.provider.ToString(true, true);
          break;
        case MaskType.Numeric:
          if (string.IsNullOrEmpty(this.Mask))
            this.mask = "n0";
          this.provider = (IMaskProvider) new NumericMaskTextBoxProvider(this.mask, this.culture, this);
          base.Text = this.provider.ToString(true, true);
          break;
        case MaskType.Standard:
          if (string.IsNullOrEmpty(this.Mask))
            this.mask = "############";
          this.provider = (IMaskProvider) new StandartMaskTextBoxProvider(this.mask, this.culture, this, this.allowPromptAsInput, this.promptChar, this.passwordChar, this.restrictToAscii);
          this.Text = this.provider.ToString(true, true);
          break;
        case MaskType.Regex:
          if (string.IsNullOrEmpty(this.Mask))
            this.mask = "[A-z]";
          this.provider = (IMaskProvider) new RegexMaskTextBoxProvider(this.mask, this.culture, this);
          base.Text = this.provider.ToString(true, true);
          break;
        case MaskType.IP:
          this.provider = (IMaskProvider) new IPMaskTextBoxProvider(CultureInfo.InvariantCulture, this, this.allowPromptAsInput, ' ', this.passwordChar, this.restrictToAscii);
          base.Text = this.provider.ToString(true, true);
          break;
        case MaskType.EMail:
          this.mask = string.Empty;
          this.provider = (IMaskProvider) new EMailMaskTextBoxProvider(this.culture, this);
          base.Text = this.provider.ToString(true, true);
          break;
        case MaskType.FreeFormDateTime:
          if (string.IsNullOrEmpty(this.Mask))
          {
            this.mask = "g";
            if (this.Parent != null && this.Parent.Parent != null)
            {
              if (this.Parent.Parent is RadDateTimePickerElement)
                this.mask = "g";
              else if (this.Parent.Parent is RadTimePickerElement)
                this.mask = "t";
            }
          }
          FreeFormDateTimeProvider dateTimeProvider2 = new FreeFormDateTimeProvider(this.mask, this.culture, this);
          dateTimeProvider2.MaxDate = this.maxDate;
          dateTimeProvider2.MinDate = this.minDate;
          this.provider = (IMaskProvider) dateTimeProvider2;
          break;
      }
      this.OnMaskProviderCreated();
    }

    private void UnwireEvents()
    {
      RegexMaskTextBoxProvider provider = this.provider as RegexMaskTextBoxProvider;
      if (provider == null)
        return;
      provider.UnwireEvents();
      provider.ErrorProvider.Dispose();
    }

    [Description("The text to display in the control")]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        this.unmaskedText = value;
        this.provider.Validate(value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableArrowKeys
    {
      get
      {
        return this.enableArrowKeys;
      }
      set
      {
        this.enableArrowKeys = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool EnableMouseWheel
    {
      get
      {
        return this.enableMouseWheel;
      }
      set
      {
        this.enableMouseWheel = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadContextMenu ContextMenu
    {
      get
      {
        if (this.contextMenu == null)
          this.contextMenu = new RadContextMenu();
        return this.contextMenu;
      }
      set
      {
        this.contextMenu = value;
      }
    }

    [DefaultValue(MaskFormat.IncludePromptAndLiterals)]
    [Category("Behavior")]
    [Description("Gets or sets a value that determines whether literals and prompt characters are included in the Value")]
    public MaskFormat TextMaskFormat
    {
      get
      {
        return this.textMasMaskFormat;
      }
      set
      {
        this.textMasMaskFormat = value;
        this.OnNotifyPropertyChanged(nameof (TextMaskFormat));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IMaskProvider Provider
    {
      get
      {
        return this.provider;
      }
      set
      {
        this.provider = value;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsKeyBoard
    {
      get
      {
        return this.isKeyBoard;
      }
      internal set
      {
        this.isKeyBoard = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("MaskedTextBox AllowPrompt As Input")]
    public bool AllowPromptAsInput
    {
      get
      {
        return this.allowPromptAsInput;
      }
      set
      {
        if (value == this.allowPromptAsInput)
          return;
        this.allowPromptAsInput = value;
        this.CreateMaskProvider();
      }
    }

    [Browsable(false)]
    public MaskedTextProvider MaskedTextProvider
    {
      get
      {
        return (MaskedTextProvider) this.provider.Clone();
      }
    }

    [Browsable(false)]
    public MaskedTextResultHint ResultHint
    {
      get
      {
        return this.hint;
      }
    }

    [Browsable(false)]
    public int HintPosition
    {
      get
      {
        return this.hintPos;
      }
    }

    [Browsable(false)]
    public string UnmaskedText
    {
      get
      {
        return this.unmaskedText;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the culture information associated with the masked label")]
    public CultureInfo Culture
    {
      get
      {
        if (this.provider.Culture == null)
          return this.culture;
        return this.provider.Culture;
      }
      set
      {
        if (object.ReferenceEquals((object) value, (object) this.culture))
          return;
        this.SetCultureCore(value);
      }
    }

    protected virtual void SetCultureCore(CultureInfo value)
    {
      object obj = (object) this.ExcludeLiterals();
      bool flag = false;
      Decimal result = new Decimal(0);
      if (this.maskType == MaskType.Numeric && Decimal.TryParse(Convert.ToString(obj, (IFormatProvider) this.culture), NumberStyles.Any, (IFormatProvider) this.culture, out result))
        flag = true;
      this.culture = value;
      this.CreateMaskProvider();
      if (flag)
        this.Value = (object) result;
      else
        this.Value = obj;
    }

    [DefaultValue("")]
    [Category("Appearance")]
    [Description("Gets or sets the mask to use for the label text")]
    public string Mask
    {
      get
      {
        if (this.cachedMask != string.Empty)
          return this.mask;
        return string.Empty;
      }
      set
      {
        if (value == null)
          value = "";
        if (!(this.mask != value))
          return;
        if (value == "h" || value == "H")
          value = " " + value;
        if (value == "yyy")
          value = "yyyy";
        this.cachedMask = value;
        this.mask = value;
        this.CreateMaskProvider();
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the prompt character for display in the label text")]
    [DefaultValue('_')]
    public char PromptChar
    {
      get
      {
        return this.promptChar;
      }
      set
      {
        this.promptChar = value;
        this.CreateMaskProvider();
      }
    }

    [Description("Gets or sets whether or not prompt characters are displayed in the label text")]
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool IncludePrompt
    {
      get
      {
        return this.provider.IncludePrompt;
      }
      set
      {
        this.provider.IncludePrompt = value;
        this.Text = this.unmaskedText;
      }
    }

    [Description("Gets or sets the mask type.")]
    [DefaultValue(MaskType.None)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Category("Behavior")]
    public virtual MaskType MaskType
    {
      get
      {
        return this.maskType;
      }
      set
      {
        if (value == this.maskType)
          return;
        this.maskType = value;
        this.Value = (object) string.Empty;
        this.CreateMaskProvider();
        this.OnNotifyPropertyChanged(nameof (MaskType));
      }
    }

    [Description("Gets or sets the edited value")]
    [DefaultValue(null)]
    [Category("Data")]
    public object Value
    {
      get
      {
        if (this.isNullValue)
          return (object) null;
        switch (this.textMasMaskFormat)
        {
          case MaskFormat.ExcludePromptAndLiterals:
            return (object) this.ExcludePromptAndLiterals();
          case MaskFormat.IncludePrompt:
            return (object) this.ExcludeLiterals();
          case MaskFormat.IncludeLiterals:
            return (object) this.ExcludePrompt();
          case MaskFormat.IncludePromptAndLiterals:
            return this.provider.Value;
          default:
            return this.provider.Value;
        }
      }
      set
      {
        if (value == null || value == DBNull.Value)
        {
          CancelEventArgs e = new CancelEventArgs();
          this.CallValueChanging(e);
          if (e.Cancel)
            return;
          this.isNullValue = true;
          this.CallValueChanged(EventArgs.Empty);
          if (this.MaskType != MaskType.Standard)
          {
            this.TextBoxItem.TextBoxControl.Text = "";
          }
          else
          {
            StandartMaskTextBoxProvider provider = this.provider as StandartMaskTextBoxProvider;
            if (provider == null)
              return;
            this.provider.TextBoxItem.SelectAll();
            provider.ValidateCore(new string(' ', this.provider.TextBoxItem.Text.Length));
            if (provider.hint <= MaskedTextResultHint.Unknown)
              return;
            provider.TextBoxItem.Text = this.provider.ToString(true, true);
          }
        }
        else
        {
          if (this.MaskType == MaskType.Standard)
          {
            this.provider.TextBoxItem.SelectAll();
            this.provider.Value = (object) new string(' ', this.provider.TextBoxItem.Text.Length);
            this.provider.TextBoxItem.Text = this.provider.Value.ToString();
          }
          this.isNullValue = false;
          this.provider.Value = value;
          this.OnNotifyPropertyChanged("MaskEditValue");
        }
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ContextMenuEnabled
    {
      get
      {
        return this.contextMenuEnabled;
      }
      set
      {
        this.contextMenuEnabled = value;
      }
    }

    [DefaultValue(false)]
    public virtual bool EnableNullValueInput
    {
      get
      {
        return this.enableNullValueInput;
      }
      set
      {
        this.enableNullValueInput = value;
        if (value || !this.isNullValue)
          return;
        this.isNullValue = false;
        this.provider.Validate(string.Empty);
      }
    }

    protected internal DateTime MinDate
    {
      get
      {
        return this.minDate;
      }
      set
      {
        this.minDate = value;
        if (!(this.provider is MaskDateTimeProvider))
          return;
        (this.provider as MaskDateTimeProvider).MinDate = value;
      }
    }

    protected internal DateTime MaxDate
    {
      get
      {
        return this.maxDate;
      }
      set
      {
        this.maxDate = value;
        if (!(this.provider is MaskDateTimeProvider))
          return;
        (this.provider as MaskDateTimeProvider).MaxDate = value;
      }
    }

    [Category("Action")]
    [Description("Occurs when MaskProvider has been created This event will be fired multiple times because the provider is created when some properties changed Properties are: Mask, Culture, MaskType and more.")]
    public event EventHandler MaskProviderCreated;

    [Category("Action")]
    [Description("Occurs when the editing value has been changed")]
    public event EventHandler ValueChanged;

    [Description(" Occurs when the editing value is changing.")]
    [Category("Action")]
    public event CancelEventHandler ValueChanging;

    public void Up()
    {
      this.isNullValue = false;
      this.provider.KeyDown((object) this, (KeyEventArgs) new KeyEventArgsWithMinMax(Keys.Up, this.minDate, this.maxDate));
    }

    public void Down()
    {
      this.isNullValue = false;
      this.provider.KeyDown((object) this, (KeyEventArgs) new KeyEventArgsWithMinMax(Keys.Down, this.minDate, this.maxDate));
    }

    public virtual void Validate(string value)
    {
      this.provider.Validate(value);
    }

    public virtual void HandleKeyPress(KeyPressEventArgs e)
    {
      bool isNullValue = this.isNullValue;
      if (!this.EnableNullValueInput)
        this.isNullValue = false;
      switch (e.KeyChar)
      {
        case '\x0003':
          this.MakeClipboardCopy();
          e.Handled = true;
          break;
        case '\x0016':
          this.MakeClipboardPaste();
          e.Handled = true;
          break;
        case '\x0018':
          this.MakeClipboardCut();
          e.Handled = true;
          break;
      }
      this.isNullValue = isNullValue && !e.Handled;
    }

    protected internal virtual void OnMaskProviderCreated()
    {
      if (this.MaskProviderCreated == null)
        return;
      this.MaskProviderCreated((object) this.provider, EventArgs.Empty);
    }

    private void TextBoxItem_Click(object sender, EventArgs e)
    {
      this.provider.Click();
    }

    private void TextBoxItem_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Handled || this.TextBoxItem.ReadOnly)
        return;
      if (e.Control && this.EnableNullValueInput && (e.KeyCode == Keys.Delete || e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0))
      {
        this.Value = (object) null;
        e.Handled = true;
      }
      else
      {
        bool isNullValue = this.isNullValue;
        if (!this.EnableNullValueInput)
          this.isNullValue = false;
        this.isKeyBoard = true;
        KeyEventArgsWithMinMax eventArgsWithMinMax = new KeyEventArgsWithMinMax(e.KeyData, this.minDate, this.maxDate);
        if (this.EnableArrowKeys || e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
          this.provider.KeyDown(sender, (KeyEventArgs) eventArgsWithMinMax);
        e.Handled = eventArgsWithMinMax.Handled;
        this.isNullValue = isNullValue && !e.Handled;
        this.isKeyBoard = false;
      }
    }

    private void TextBoxItem_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.Handled)
        return;
      bool isNullValue = this.isNullValue;
      string text = this.provider.TextBoxItem.Text;
      if (!this.EnableNullValueInput)
        this.isNullValue = false;
      this.isKeyBoard = true;
      this.HandleKeyPress(e);
      if (!e.Handled && !this.TextBoxItem.ReadOnly)
        this.provider.KeyPress(sender, e);
      this.isNullValue = isNullValue && text == this.provider.TextBoxItem.Text;
      this.isKeyBoard = false;
    }

    private void TextBoxItem_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right || !this.contextMenuEnabled)
        return;
      this.BuildAndEnableContextMenu();
      Point screen = (sender as RadTextBoxItem).PointToScreen(e.Location);
      this.contextMenu.Show(screen.X, screen.Y);
    }

    private void ContextMenuMenuItem_Click(object sender, EventArgs e)
    {
      switch (((RadElement) sender).Name.ToUpper())
      {
        case "CUT":
          this.TextBoxItem.Cut();
          break;
        case "COPY":
          this.TextBoxItem.Copy();
          break;
        case "PASTE":
          this.TextBoxItem.Paste();
          if (!this.Provider.Validate(this.Text))
          {
            this.TextBoxItem.TextBoxControl.Undo();
            break;
          }
          break;
        case "DELETE":
          this.TextBoxItem.Clear();
          break;
        case "SELECT ALL":
          this.TextBoxItem.SelectAll();
          break;
      }
      this.contextMenu.DropDown.ClosePopup(RadPopupCloseReason.Mouse);
    }

    protected virtual void TextBoxItem_MouseWheel(object sender, MouseEventArgs e)
    {
      if (!this.EnableMouseWheel || this.TextBoxItem.ReadOnly)
        return;
      this.isNullValue = false;
      if (e.Delta > 0)
        this.Up();
      else
        this.Down();
    }

    private void MakeClipboardCopy()
    {
      if (this.TextBoxItem.PasswordChar != char.MinValue)
        return;
      Clipboard.SetDataObject((object) this.TextBoxItem.SelectedText);
    }

    private void MakeClipboardPaste()
    {
      if (this.TextBoxItem.ReadOnly)
        return;
      string clipboardText = RadMaskedEditBoxElement.GetClipboardText();
      if (this.provider is TextBoxProvider || this.provider is RegexMaskTextBoxProvider || this.provider is MaskDateTimeProvider)
      {
        this.TextBoxItem.Paste();
        if (!(this.provider is RegexMaskTextBoxProvider) && !(this.provider is MaskDateTimeProvider) || this.provider is FreeFormDateTimeProvider)
          return;
        this.provider.Validate(this.TextBoxItem.Text);
      }
      else
      {
        foreach (char keyChar in clipboardText)
          this.provider.KeyPress((object) this, new KeyPressEventArgs(keyChar));
      }
    }

    private void MakeClipboardCut()
    {
      this.MakeClipboardCopy();
      this.provider.Delete();
    }

    protected internal virtual void CallValueChanged(EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    protected internal virtual void CallValueChanging(CancelEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    protected virtual string ExcludePromptAndLiterals()
    {
      return this.ExcludeLiteralsCore().Replace(this.promptChar.ToString(), "");
    }

    protected virtual string ExcludeLiterals()
    {
      return this.ExcludeLiteralsCore();
    }

    protected virtual string ExcludePrompt()
    {
      return this.provider.Value.ToString().Replace(this.promptChar.ToString(), "");
    }

    private void BuildAndEnableContextMenu()
    {
      if (this.contextMenu == null)
        this.contextMenu = new RadContextMenu();
      if (this.contextMenu.Items.Count == 0)
      {
        RadMenuItem radMenuItem1 = new RadMenuItem("Cut");
        radMenuItem1.Name = "Cut";
        this.contextMenu.Items.Add((RadItem) radMenuItem1);
        radMenuItem1.Click += new EventHandler(this.ContextMenuMenuItem_Click);
        RadMenuItem radMenuItem2 = new RadMenuItem("Copy");
        radMenuItem2.Name = "Copy";
        this.contextMenu.Items.Add((RadItem) radMenuItem2);
        radMenuItem2.Click += new EventHandler(this.ContextMenuMenuItem_Click);
        RadMenuItem radMenuItem3 = new RadMenuItem("Paste");
        radMenuItem3.Name = "Paste";
        this.contextMenu.Items.Add((RadItem) radMenuItem3);
        radMenuItem3.Click += new EventHandler(this.ContextMenuMenuItem_Click);
        RadMenuItem radMenuItem4 = new RadMenuItem("Delete");
        radMenuItem4.Name = "Delete";
        this.contextMenu.Items.Add((RadItem) radMenuItem4);
        radMenuItem4.Click += new EventHandler(this.ContextMenuMenuItem_Click);
        RadMenuItem radMenuItem5 = new RadMenuItem("Select All");
        radMenuItem5.Name = "Select All";
        this.contextMenu.Items.Add((RadItem) radMenuItem5);
        radMenuItem5.Click += new EventHandler(this.ContextMenuMenuItem_Click);
      }
      this.contextMenu.Items["Cut"].Enabled = this.TextBoxItem.SelectionLength > 0;
      this.contextMenu.Items["Copy"].Enabled = this.TextBoxItem.SelectionLength > 0;
      this.contextMenu.Items["Paste"].Enabled = Clipboard.ContainsText();
      this.contextMenu.Items["Delete"].Enabled = this.TextBoxItem.SelectionLength > 0;
    }

    private string ExcludeLiteralsCore()
    {
      bool flag = false;
      string str1 = this.RemoveNumberFormats(this.provider.Value.ToString());
      if (this.Culture.NumberFormat.NumberDecimalSeparator != "." || this.MaskType != MaskType.Numeric)
        str1 = str1.Replace(".", "");
      if (str1.StartsWith("-") && (this.MaskType == MaskType.Numeric || this.MaskType == MaskType.Standard))
        flag = true;
      string str2 = str1.Replace("-", "");
      StringBuilder stringBuilder = new StringBuilder(str2.Length);
      foreach (char c in str2)
      {
        if (!char.IsLetter(c) || this.MaskType == MaskType.Standard || (this.MaskType == MaskType.EMail || this.MaskType == MaskType.Regex))
          stringBuilder.Append(c);
      }
      string str3 = stringBuilder.ToString();
      if (flag)
        str3 = "-" + str3;
      return str3;
    }

    public string RemoveNumberFormats(string value)
    {
      if (this.ShouldAddMinusSign(value))
        value = this.Culture.NumberFormat.NegativeSign + value;
      value = value.Replace("(", "").Replace(")", "").Replace(" ", "").Replace(" ", "").Replace("/", "").Replace("\\", "").Replace(":", "");
      NumericMaskTextBoxProvider provider = this.provider as NumericMaskTextBoxProvider;
      if (provider != null)
      {
        switch (provider.NumericType)
        {
          case NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency:
            value = value.Replace(this.GetSafe(this.Culture.NumberFormat.CurrencyGroupSeparator), "").Replace(this.GetSafe(this.Culture.NumberFormat.CurrencySymbol), "");
            break;
          case NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Percent:
            value = value.Replace(this.GetSafe(this.Culture.NumberFormat.PercentGroupSeparator), "");
            break;
          default:
            value = value.Replace(this.GetSafe(this.Culture.NumberFormat.NumberGroupSeparator), "");
            break;
        }
      }
      return value;
    }

    private bool ShouldAddMinusSign(string value)
    {
      if (this.TextMaskFormat != MaskFormat.ExcludePromptAndLiterals && this.TextMaskFormat != MaskFormat.IncludePrompt)
        return false;
      NumericMaskTextBoxProvider provider = this.provider as NumericMaskTextBoxProvider;
      if (provider == null || provider.NumericType != NumericCharacterTextBoxProvider.RadNumericMaskFormatType.Currency || (value.IndexOf("(") < 0 || value.IndexOf(")") < 0))
        return false;
      switch (this.Culture.NumberFormat.CurrencyNegativePattern)
      {
        case 0:
        case 4:
        case 14:
        case 15:
          return true;
        default:
          return false;
      }
    }

    private string GetSafe(string separator)
    {
      if (!string.IsNullOrEmpty(separator))
        return separator;
      return " ";
    }

    private bool ShouldSerializeCulture()
    {
      return !CultureInfo.CurrentCulture.Equals((object) this.Culture);
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

    public static string Format(string mask, string text)
    {
      MaskedTextResultHint hint;
      int hintPosition;
      return RadMaskedEditBoxElement.Format(mask, text, char.MinValue, (CultureInfo) null, out hint, out hintPosition);
    }

    public static string Format(string mask, string text, char promptChar)
    {
      MaskedTextResultHint hint;
      int hintPosition;
      return RadMaskedEditBoxElement.Format(mask, text, promptChar, (CultureInfo) null, out hint, out hintPosition);
    }

    public static string Format(string mask, string text, char promptChar, CultureInfo culture)
    {
      MaskedTextResultHint hint;
      int hintPosition;
      return RadMaskedEditBoxElement.Format(mask, text, promptChar, culture, out hint, out hintPosition);
    }

    public static string Format(
      string mask,
      string text,
      char promptChar,
      CultureInfo culture,
      out MaskedTextResultHint hint,
      out int hintPosition)
    {
      if (text == null)
        text = string.Empty;
      if (culture == null)
        culture = CultureInfo.CurrentCulture;
      MaskedTextProvider maskedTextProvider = new MaskedTextProvider(mask, culture);
      if (promptChar != char.MinValue)
      {
        maskedTextProvider.PromptChar = promptChar;
        maskedTextProvider.IncludePrompt = true;
      }
      maskedTextProvider.Set(text, out hintPosition, out hint);
      if (hint > MaskedTextResultHint.Unknown)
        return maskedTextProvider.ToString();
      return text;
    }

    public static string GetClipboardText()
    {
      IDataObject dataObject;
      try
      {
        dataObject = Clipboard.GetDataObject();
      }
      catch
      {
        return string.Empty;
      }
      if (dataObject.GetDataPresent(DataFormats.UnicodeText))
        return (string) dataObject.GetData(DataFormats.UnicodeText);
      if (dataObject.GetDataPresent(DataFormats.Text))
        return (string) dataObject.GetData(DataFormats.Text);
      return string.Empty;
    }
  }
}
