// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMaskedEditBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadMaskEditBoxDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [DefaultBindingProperty("Text")]
  [Description("Uses a mask to distinguish between proper and improper user input")]
  [DefaultProperty("Mask")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Editors")]
  [ToolboxItem(true)]
  public class RadMaskedEditBox : RadEditorControl
  {
    private static readonly object EventKeyDown = new object();
    private static readonly object EventKeyPress = new object();
    private static readonly object EventKeyUp = new object();
    private static readonly object MultilineChangedEventKey = new object();
    private static readonly object TextAlignChangedEventKey = new object();
    private string cachedMask = string.Empty;
    private string oldText = string.Empty;
    private RadMaskedEditBoxElement maskEditBoxElement;

    public RadMaskedEditBox()
    {
      this.AutoSize = true;
      this.TabStop = false;
      this.SetStyle(ControlStyles.Selectable, true);
      this.WireEvents();
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.maskEditBoxElement = this.CreateElement();
      this.RootElement.Children.Add((RadElement) this.maskEditBoxElement);
    }

    protected virtual RadMaskedEditBoxElement CreateElement()
    {
      return new RadMaskedEditBoxElement();
    }

    protected internal void WireEvents()
    {
      this.maskEditBoxElement.ValueChanged += new EventHandler(this.OnValueChanged);
      this.maskEditBoxElement.ValueChanging += new CancelEventHandler(this.OnValueChanging);
      this.maskEditBoxElement.TextChanged += new EventHandler(this.maskEditBoxElement_TextChanged);
      this.maskEditBoxElement.KeyDown += new KeyEventHandler(this.OnKeyDown);
      this.maskEditBoxElement.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
      this.maskEditBoxElement.KeyUp += new KeyEventHandler(this.OnKeyUp);
      this.maskEditBoxElement.MultilineChanged += new EventHandler(this.OnMultilineChanged);
      this.maskEditBoxElement.TextAlignChanged += new EventHandler(this.OnTextAlignChanged);
      this.maskEditBoxElement.TextBoxItem.LostFocus += new EventHandler(this.TextBoxItem_LostFocus);
      this.maskEditBoxElement.TextBoxItem.GotFocus += new EventHandler(this.TextBoxItem_GotFocus);
      this.maskEditBoxElement.TextBoxItem.PreviewKeyDown += new PreviewKeyDownEventHandler(this.TextBoxItem_PreviewKeyDown);
    }

    protected internal void UnwireEvents()
    {
      this.maskEditBoxElement.ValueChanged -= new EventHandler(this.OnValueChanged);
      this.maskEditBoxElement.ValueChanging -= new CancelEventHandler(this.OnValueChanging);
      this.maskEditBoxElement.TextChanged -= new EventHandler(this.maskEditBoxElement_TextChanged);
      this.maskEditBoxElement.KeyDown -= new KeyEventHandler(this.OnKeyDown);
      this.maskEditBoxElement.KeyPress -= new KeyPressEventHandler(this.OnKeyPress);
      this.maskEditBoxElement.KeyUp -= new KeyEventHandler(this.OnKeyUp);
      this.maskEditBoxElement.MultilineChanged -= new EventHandler(this.OnMultilineChanged);
      this.maskEditBoxElement.TextAlignChanged -= new EventHandler(this.OnTextAlignChanged);
      this.maskEditBoxElement.TextBoxItem.LostFocus -= new EventHandler(this.TextBoxItem_LostFocus);
      this.maskEditBoxElement.TextBoxItem.GotFocus -= new EventHandler(this.TextBoxItem_GotFocus);
      this.maskEditBoxElement.TextBoxItem.PreviewKeyDown -= new PreviewKeyDownEventHandler(this.TextBoxItem_PreviewKeyDown);
    }

    [DefaultValue(true)]
    [Category("Layout")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(125, 20));
      }
    }

    [DefaultValue("")]
    [SettingsBindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Editor("Telerik.WinControls.UI.Design.SimpleTextUITypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Browsable(true)]
    [Localizable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the text associated with this control.")]
    [Bindable(true)]
    public override string Text
    {
      get
      {
        return this.maskEditBoxElement.Text;
      }
      set
      {
        this.maskEditBoxElement.Text = value;
        this.OnNotifyPropertyChanged(nameof (Text));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual RadMaskedEditBoxElement MaskedEditBoxElement
    {
      get
      {
        return this.maskEditBoxElement;
      }
      set
      {
        this.maskEditBoxElement = value;
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Category("Behavior")]
    [DefaultValue("")]
    [Localizable(true)]
    [Description("Gets or sets a mask expression.")]
    public string Mask
    {
      get
      {
        if (this.cachedMask != string.Empty)
          return this.maskEditBoxElement.Mask;
        return this.cachedMask;
      }
      set
      {
        if (!(this.maskEditBoxElement.Mask != value))
          return;
        this.cachedMask = value;
        this.maskEditBoxElement.Mask = value;
        this.OnNotifyPropertyChanged(nameof (Mask));
      }
    }

    [Category("Behavior")]
    [DefaultValue(MaskType.None)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or sets the mask type.")]
    public MaskType MaskType
    {
      get
      {
        return this.maskEditBoxElement.MaskType;
      }
      set
      {
        if (value == this.maskEditBoxElement.MaskType)
          return;
        this.maskEditBoxElement.MaskType = value;
        this.OnNotifyPropertyChanged(nameof (MaskType));
      }
    }

    [Bindable(true)]
    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets the value associated to the mask edit box")]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object Value
    {
      get
      {
        return this.maskEditBoxElement.Value;
      }
      set
      {
        this.maskEditBoxElement.Value = value;
        this.OnNotifyPropertyChanged(nameof (Value));
      }
    }

    [DefaultValue(MaskFormat.IncludePromptAndLiterals)]
    [Description("Gets or sets a value that determines whether literals and prompt characters are included in the Value")]
    [Browsable(true)]
    [Category("Behavior")]
    public MaskFormat TextMaskFormat
    {
      get
      {
        return this.maskEditBoxElement.TextMaskFormat;
      }
      set
      {
        this.maskEditBoxElement.TextMaskFormat = value;
        this.OnNotifyPropertyChanged(nameof (TextMaskFormat));
      }
    }

    [Category("Appearance")]
    [RadDefaultValue("UseGenericBorderPaint", typeof (RadTextBoxElement))]
    [RadDescription("UseGenericBorderPaint", typeof (RadTextBoxElement))]
    public bool UseGenericBorderPaint
    {
      get
      {
        return this.maskEditBoxElement.UseGenericBorderPaint;
      }
      set
      {
        this.maskEditBoxElement.UseGenericBorderPaint = value;
      }
    }

    [DefaultValue(HorizontalAlignment.Left)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the horizontal alignment of the text.")]
    public HorizontalAlignment TextAlign
    {
      get
      {
        return this.maskEditBoxElement.TextAlign;
      }
      set
      {
        this.maskEditBoxElement.TextAlign = value;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    public bool ShortcutsEnabled
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.ShortcutsEnabled;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.ShortcutsEnabled = value;
      }
    }

    [DefaultValue(0)]
    public int SelectionStart
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.SelectionStart;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.SelectionStart = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    public bool Modified
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.Modified;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.Modified = value;
      }
    }

    [RadDefaultValue("Multiline", typeof (RadTextBoxItem))]
    [Category("Behavior")]
    public bool Multiline
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.Multiline;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.Multiline = value;
      }
    }

    [Localizable(true)]
    [Category("Behavior")]
    [RadDefaultValue("NullText", typeof (RadTextBoxItem))]
    public string NullText
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.NullText;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.NullText = value;
        this.OnNotifyPropertyChanged(nameof (NullText));
      }
    }

    [RadDefaultValue("PasswordChar", typeof (RadTextBoxItem))]
    [Category("Behavior")]
    public char PasswordChar
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.PasswordChar;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.PasswordChar = value;
        this.OnNotifyPropertyChanged(nameof (PasswordChar));
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.ReadOnly;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.ReadOnly = value;
      }
    }

    [DefaultValue(ScrollBars.None)]
    [Category("Behavior")]
    public ScrollBars ScrollBars
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.ScrollBars;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.ScrollBars = value;
      }
    }

    [DefaultValue("")]
    [Category("Appearance")]
    public string SelectedText
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.SelectedText;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.SelectedText = value;
      }
    }

    [DefaultValue(0)]
    public int SelectionLength
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.SelectionLength;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.SelectionLength = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    public bool HideSelection
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.HideSelection;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.HideSelection = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    public string[] Lines
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.Lines;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.Lines = value;
      }
    }

    [Category("Behavior")]
    [RadDefaultValue("AcceptsReturn", typeof (RadTextBoxItem))]
    public bool AcceptsReturn
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.AcceptsReturn;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.AcceptsReturn = value;
      }
    }

    [RadDefaultValue("AcceptsTab", typeof (RadTextBoxItem))]
    [Category("Behavior")]
    public bool AcceptsTab
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.AcceptsTab;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.AcceptsTab = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(CharacterCasing.Normal)]
    public CharacterCasing CharacterCasing
    {
      get
      {
        return this.maskEditBoxElement.TextBoxItem.CharacterCasing;
      }
      set
      {
        this.maskEditBoxElement.TextBoxItem.CharacterCasing = value;
      }
    }

    [Description("Gets or sets the current culture associated to the RadMaskBox")]
    [Category("Behavior")]
    public CultureInfo Culture
    {
      get
      {
        return this.maskEditBoxElement.Culture;
      }
      set
      {
        if (object.ReferenceEquals((object) value, (object) this.maskEditBoxElement.Culture))
          return;
        this.maskEditBoxElement.Culture = value;
        this.OnNotifyPropertyChanged(nameof (Culture));
      }
    }

    [Description("MaskedTextBox Prompt Char")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue('_')]
    [Category("Appearance")]
    [Localizable(true)]
    public char PromptChar
    {
      get
      {
        return this.maskEditBoxElement.PromptChar;
      }
      set
      {
        if (value == char.MinValue)
          throw new ArgumentException("Specified Character is not valid for this property!");
        this.maskEditBoxElement.PromptChar = value;
      }
    }

    [Description("MaskedTextBox Allow Prompt As Input")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool AllowPromptAsInput
    {
      get
      {
        return this.maskEditBoxElement.AllowPromptAsInput;
      }
      set
      {
        this.maskEditBoxElement.AllowPromptAsInput = value;
      }
    }

    [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    [Browsable(true)]
    [RadDefaultValue("AutoCompleteCustomSource", typeof (HostedTextBoxBase))]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description("Gets or sets a custom StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
    public AutoCompleteStringCollection AutoCompleteCustomSource
    {
      get
      {
        return ((System.Windows.Forms.TextBox) this.maskEditBoxElement.TextBoxItem.HostedControl).AutoCompleteCustomSource;
      }
      set
      {
        ((System.Windows.Forms.TextBox) this.maskEditBoxElement.TextBoxItem.HostedControl).AutoCompleteCustomSource = value;
      }
    }

    [Description("Gets or sets an option that controls how automatic completion works for the TextBox.")]
    [RadDefaultValue("AutoCompleteMode", typeof (HostedTextBoxBase))]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return ((System.Windows.Forms.TextBox) this.maskEditBoxElement.TextBoxItem.HostedControl).AutoCompleteMode;
      }
      set
      {
        ((System.Windows.Forms.TextBox) this.maskEditBoxElement.TextBoxItem.HostedControl).AutoCompleteMode = value;
      }
    }

    [RadDefaultValue("AutoCompleteSource", typeof (HostedTextBoxBase))]
    [Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
    [TypeConverter(typeof (TextBoxAutoCompleteSourceConverter))]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public AutoCompleteSource AutoCompleteSource
    {
      get
      {
        return ((System.Windows.Forms.TextBox) this.maskEditBoxElement.TextBoxItem.HostedControl).AutoCompleteSource;
      }
      set
      {
        ((System.Windows.Forms.TextBox) this.maskEditBoxElement.TextBoxItem.HostedControl).AutoCompleteSource = value;
      }
    }

    public new string Name
    {
      get
      {
        return base.Name;
      }
      set
      {
        base.Name = value;
        this.maskEditBoxElement.TextBoxItem.HostedControl.Name = value;
      }
    }

    [DefaultValue(false)]
    public virtual bool EnableNullValueInput
    {
      get
      {
        return this.maskEditBoxElement.EnableNullValueInput;
      }
      set
      {
        this.maskEditBoxElement.EnableNullValueInput = value;
      }
    }

    [Category("Action")]
    [Description("Occurs when the editing value has been changed")]
    public event EventHandler ValueChanged;

    [Category("Action")]
    [Description(" Occurs when the editing value is changing.")]
    public event CancelEventHandler ValueChanging;

    [Description("Occurs when the RadItem has focus and the user pressees a key down")]
    [Category("Key")]
    public new event KeyEventHandler KeyDown
    {
      add
      {
        this.Events.AddHandler(RadMaskedEditBox.EventKeyDown, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMaskedEditBox.EventKeyDown, (Delegate) value);
      }
    }

    [Category("Key")]
    [Description("Occurs when the RadItem has focus and the user pressees a key")]
    public new event KeyPressEventHandler KeyPress
    {
      add
      {
        this.Events.AddHandler(RadMaskedEditBox.EventKeyPress, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMaskedEditBox.EventKeyPress, (Delegate) value);
      }
    }

    [Description("Occurs when the RadItem has focus and the user releases the pressed key up")]
    [Category("Key")]
    public new event KeyEventHandler KeyUp
    {
      add
      {
        this.Events.AddHandler(RadMaskedEditBox.EventKeyUp, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMaskedEditBox.EventKeyUp, (Delegate) value);
      }
    }

    [Description("Occurs when the Multiline property has changed.")]
    [Category("Property Changed")]
    [Browsable(true)]
    public event EventHandler MultilineChanged
    {
      add
      {
        this.Events.AddHandler(RadMaskedEditBox.MultilineChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMaskedEditBox.MultilineChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the TextAlign property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler TextAlignChanged
    {
      add
      {
        this.Events.AddHandler(RadMaskedEditBox.TextAlignChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMaskedEditBox.TextAlignChangedEventKey, (Delegate) value);
      }
    }

    public void Focus()
    {
      this.MaskedEditBoxElement.TextBoxItem.HostedControl.Focus();
    }

    public new void Select()
    {
      this.MaskedEditBoxElement.TextBoxItem.HostedControl.Select();
    }

    public void Clear()
    {
      this.maskEditBoxElement.Value = (object) null;
      this.maskEditBoxElement.TextBoxItem.Clear();
    }

    public void ClearUndo()
    {
      this.maskEditBoxElement.TextBoxItem.ClearUndo();
    }

    public void SelectAll()
    {
      this.maskEditBoxElement.TextBoxItem.SelectAll();
    }

    public virtual void OnTextAlignChanged(object sender, EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadMaskedEditBox.TextAlignChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler(sender, e);
    }

    public virtual void OnMultilineChanged(object sender, EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadMaskedEditBox.MultilineChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    public virtual void OnKeyUp(object sender, KeyEventArgs e)
    {
      KeyEventHandler keyEventHandler = (KeyEventHandler) this.Events[RadMaskedEditBox.EventKeyUp];
      if (keyEventHandler == null)
        return;
      keyEventHandler((object) this, e);
    }

    public virtual void OnKeyPress(object sender, KeyPressEventArgs e)
    {
      KeyPressEventHandler pressEventHandler = (KeyPressEventHandler) this.Events[RadMaskedEditBox.EventKeyPress];
      if (pressEventHandler == null)
        return;
      pressEventHandler((object) this, e);
    }

    public virtual void OnKeyDown(object sender, KeyEventArgs e)
    {
      KeyEventHandler keyEventHandler = (KeyEventHandler) this.Events[RadMaskedEditBox.EventKeyDown];
      if (keyEventHandler == null)
        return;
      keyEventHandler((object) this, e);
    }

    protected virtual void OnValueChanged(object sender, EventArgs e)
    {
      if (this.ValueChanged != null)
        this.ValueChanged((object) this, e);
      this.OnNotifyPropertyChanged("Value");
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "ValueChanged", (object) this.Text);
    }

    protected virtual void OnValueChanging(object sender, CancelEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    private void TextBoxItem_GotFocus(object sender, EventArgs e)
    {
      this.OnGotFocus(e);
    }

    private void TextBoxItem_LostFocus(object sender, EventArgs e)
    {
      this.OnLostFocus(e);
    }

    private void maskEditBoxElement_TextChanged(object sender, EventArgs e)
    {
      if (!(this.Text != this.oldText) || this.EnableCodedUITests)
        return;
      this.OnTextChanged(e);
      this.oldText = this.Text;
    }

    private void TextBoxItem_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
      this.OnPreviewKeyDown(e);
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
      if (value)
      {
        this.RootElement.StretchHorizontally = true;
        this.RootElement.StretchVertically = false;
      }
      else
      {
        this.RootElement.StretchHorizontally = true;
        this.RootElement.StretchVertically = true;
      }
      this.RootElement.SaveCurrentStretchModeAsDefault();
    }

    private bool ShouldSerializeCulture()
    {
      if (!CultureInfo.CurrentCulture.Equals((object) this.Culture))
        return !this.Culture.Equals((object) new CultureInfo(""));
      return false;
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 7)
        return;
      this.MaskedEditBoxElement.TextBoxItem.HostedControl.Focus();
      this.MaskedEditBoxElement.TextBoxItem.SelectAll();
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.MaskedEditBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.MaskedEditBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.MaskedEditBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.MaskedEditBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TextBoxFill");
        this.MaskedEditBoxElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, state, "TextBoxFill");
      }
      this.MaskedEditBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.MaskedEditBoxElement.SuspendApplyOfThemeSettings();
      this.MaskedEditBoxElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.MaskedEditBoxElement.ResetThemeValueOverride(FillPrimitive.GradientStyleProperty);
      this.MaskedEditBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.MaskedEditBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.MaskedEditBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.MaskedEditBoxElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.MaskedEditBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.MaskedEditBoxElement.SuspendApplyOfThemeSettings();
      this.MaskedEditBoxElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.MaskedEditBoxElement.ElementTree.ApplyThemeToElementTree();
      this.MaskedEditBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadMaskedEditBoxAccessibleObject(this, this.Name);
    }
  }
}
