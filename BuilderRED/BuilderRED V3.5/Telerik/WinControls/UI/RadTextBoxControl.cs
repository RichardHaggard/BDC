// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [Description("Enables the user to enter text, and provides multiline editing")]
  [DefaultEvent("TextChanged")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadTextBoxControlDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [DefaultProperty("Text")]
  [TelerikToolboxCategory("Editors")]
  [DefaultBindingProperty("Text")]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  [ComVisible(true)]
  [ToolboxItem(true)]
  public class RadTextBoxControl : RadControl
  {
    private Dictionary<string, object> initValues = new Dictionary<string, object>();
    private RadTextBoxControlElement textBoxElement;

    public RadTextBoxControl()
    {
      this.SetStyle(ControlStyles.UseTextForAccessibility, false);
      this.Initialized += new EventHandler(this.RadTextBoxControl_Initialized);
    }

    private void RadTextBoxControl_Initialized(object sender, EventArgs e)
    {
      this.Initialized -= new EventHandler(this.RadTextBoxControl_Initialized);
      if (this.initValues.ContainsKey("ShowClearButton"))
        this.ShowClearButton = (bool) this.initValues["ShowClearButton"];
      if (!this.initValues.ContainsKey("IsReadOnly"))
        return;
      this.IsReadOnly = (bool) this.initValues["IsReadOnly"];
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.textBoxElement = this.CreateTextBoxElement();
      this.textBoxElement.TextChanged += new EventHandler(this.OnTextBoxElementTextChanged);
      this.textBoxElement.TextChanging += new TextChangingEventHandler(this.OnTextBoxElementTextChanging);
      this.RootElement.Children.Add((RadElement) this.textBoxElement);
    }

    protected virtual RadTextBoxControlElement CreateTextBoxElement()
    {
      return new RadTextBoxControlElement();
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadTextBoxControlAccessibleObject(this);
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(125, 20));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Behavior")]
    public Size DropDownMaxSize
    {
      get
      {
        return this.TextBoxElement.DropDownMaxSize;
      }
      set
      {
        this.TextBoxElement.DropDownMaxSize = value;
      }
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Size DropDownMinSize
    {
      get
      {
        return this.TextBoxElement.DropDownMinSize;
      }
      set
      {
        this.TextBoxElement.DropDownMinSize = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(6)]
    public int MaxDropDownItemCount
    {
      get
      {
        return this.TextBoxElement.MaxDropDownItemCount;
      }
      set
      {
        this.TextBoxElement.MaxDropDownItemCount = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTextBoxControlElement TextBoxElement
    {
      get
      {
        return this.textBoxElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTextBoxListElement ListElement
    {
      get
      {
        return this.textBoxElement.ListElement;
      }
    }

    [DefaultValue(AutoCompleteMode.None)]
    [Category("Behavior")]
    [Description("Gets or sets an option that controls how automatic completion works for the TextBox.")]
    public virtual AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.textBoxElement.AutoCompleteMode;
      }
      set
      {
        this.textBoxElement.AutoCompleteMode = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the text in view should appear as the default password character.")]
    [Category("Behavior")]
    public virtual bool UseSystemPasswordChar
    {
      get
      {
        return this.textBoxElement.UseSystemPasswordChar;
      }
      set
      {
        this.textBoxElement.UseSystemPasswordChar = value;
      }
    }

    [DefaultValue('\0')]
    [Category("Behavior")]
    [Description("Gets or sets the character used to mask characters of a password in a single-line.")]
    public virtual char PasswordChar
    {
      get
      {
        return this.textBoxElement.PasswordChar;
      }
      set
      {
        this.textBoxElement.PasswordChar = value;
      }
    }

    [DefaultValue(ScrollState.AutoHide)]
    [Description("Gets or sets when the vertical scroll bar should appear in a multiline TextBox.")]
    [Category("Appearance")]
    public ScrollState VerticalScrollBarState
    {
      get
      {
        return this.textBoxElement.VerticalScrollBarState;
      }
      set
      {
        this.textBoxElement.VerticalScrollBarState = value;
      }
    }

    [Description("Gets or sets when the horizontal scroll bar should appear in a multiline TextBox.")]
    [Category("Appearance")]
    [DefaultValue(ScrollState.AutoHide)]
    public ScrollState HorizontalScrollBarState
    {
      get
      {
        return this.textBoxElement.HorizontalScrollBarState;
      }
      set
      {
        this.textBoxElement.HorizontalScrollBarState = value;
      }
    }

    [Category("Data")]
    [DefaultValue("")]
    [Editor("Telerik.WinControls.UI.Design.AutoCompleteDataMemberFieldEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Description("Gets or sets the auto complete display member.")]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Browsable(true)]
    public string AutoCompleteDisplayMember
    {
      get
      {
        return this.textBoxElement.AutoCompleteDisplayMember;
      }
      set
      {
        this.textBoxElement.AutoCompleteDisplayMember = value;
      }
    }

    [DefaultValue(null)]
    [Browsable(true)]
    [AttributeProvider(typeof (IListSource))]
    [Description("Gets or sets a value specifying the source of complete items used for automatic completion.")]
    [Category("Data")]
    public object AutoCompleteDataSource
    {
      get
      {
        return this.textBoxElement.AutoCompleteDataSource;
      }
      set
      {
        this.textBoxElement.AutoCompleteDataSource = value;
      }
    }

    [Description("Gets a value specifying the complete items used for automatic completion.")]
    [Category("Data")]
    [Browsable(true)]
    [Editor("Telerik.WinControls.UI.Design.RadListControlCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadListDataItemCollection AutoCompleteItems
    {
      get
      {
        return this.textBoxElement.AutoCompleteItems;
      }
    }

    [Description("Gets or sets a value indicating whether the selected text in the text box control remains highlighted when the element loses focus.")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool HideSelection
    {
      get
      {
        return this.textBoxElement.HideSelection;
      }
      set
      {
        this.textBoxElement.HideSelection = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the caret position.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int CaretIndex
    {
      get
      {
        return this.textBoxElement.CaretIndex;
      }
      set
      {
        this.textBoxElement.CaretIndex = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SelectionStart
    {
      get
      {
        return this.textBoxElement.SelectionStart;
      }
      set
      {
        this.textBoxElement.SelectionStart = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SelectionLength
    {
      get
      {
        return this.textBoxElement.SelectionLength;
      }
      set
      {
        this.textBoxElement.SelectionLength = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TextLength
    {
      get
      {
        return this.textBoxElement.TextLength;
      }
    }

    [Description("Gets or sets the maximum number of characters the user can type or paste into the text box element.")]
    [Localizable(true)]
    [Category("Behavior")]
    [DefaultValue(2147483647)]
    public int MaxLength
    {
      get
      {
        return this.textBoxElement.MaxLength;
      }
      set
      {
        this.textBoxElement.MaxLength = value;
      }
    }

    [Localizable(true)]
    [Bindable(true)]
    [Category("Appearance")]
    [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the current text in the text box element.")]
    public override string Text
    {
      get
      {
        return this.textBoxElement.Text;
      }
      set
      {
        this.textBoxElement.Text = value;
      }
    }

    [Localizable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the prompt text that is displayed when the text box contains no text.")]
    public string NullText
    {
      get
      {
        return this.textBoxElement.NullText;
      }
      set
      {
        this.textBoxElement.NullText = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Color NullTextColor
    {
      get
      {
        return this.textBoxElement.NullTextColor;
      }
      set
      {
        this.textBoxElement.NullTextColor = value;
      }
    }

    [Description("Gets or sets a value indicating whether the null text will be shown when the control is focused and the text is empty.")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool ShowNullText
    {
      get
      {
        return this.TextBoxElement.ShowNullText;
      }
      set
      {
        this.TextBoxElement.ShowNullText = value;
      }
    }

    [Description("Gets or sets how the text is horizontally aligned in the element.")]
    [Category("Behavior")]
    [DefaultValue(HorizontalAlignment.Left)]
    public HorizontalAlignment TextAlign
    {
      get
      {
        return this.TextBoxElement.TextAlign;
      }
      set
      {
        this.TextBoxElement.TextAlign = value;
      }
    }

    [Category("Appearance")]
    [Localizable(true)]
    [MergableProperty(false)]
    [Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] Lines
    {
      get
      {
        return this.textBoxElement.Lines;
      }
      set
      {
        this.textBoxElement.Lines = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public string SelectedText
    {
      get
      {
        return this.textBoxElement.SelectedText;
      }
      set
      {
        this.textBoxElement.SelectedText = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether pressing the TAB key in a multiline text box control types a TAB character in the control instead of moving the focus to the next element in the tab order.")]
    [Category("Behavior")]
    public bool AcceptsTab
    {
      get
      {
        return this.textBoxElement.AcceptsTab;
      }
      set
      {
        this.textBoxElement.AcceptsTab = value;
      }
    }

    [Description("Gets or sets a value indicating whether pressing ENTER in a multiline TextBox control creates a new line of text in the control or activates the default button for the form.")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool AcceptsReturn
    {
      get
      {
        return this.textBoxElement.AcceptsReturn;
      }
      set
      {
        this.textBoxElement.AcceptsReturn = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether this is a multiline text box.")]
    [DefaultValue(false)]
    public bool Multiline
    {
      get
      {
        return this.textBoxElement.Multiline;
      }
      set
      {
        this.textBoxElement.Multiline = value;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Localizable(true)]
    [Description("Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.")]
    public bool WordWrap
    {
      get
      {
        return this.textBoxElement.WordWrap;
      }
      set
      {
        this.textBoxElement.WordWrap = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SelectionColor
    {
      get
      {
        return this.textBoxElement.SelectionColor;
      }
      set
      {
        this.textBoxElement.SelectionColor = value;
      }
    }

    [Description("Gets or sets the selection opacity.")]
    [Category("Appearance")]
    [DefaultValue(100)]
    public int SelectionOpacity
    {
      get
      {
        return this.textBoxElement.SelectionOpacity;
      }
      set
      {
        this.textBoxElement.SelectionOpacity = value;
      }
    }

    [DefaultValue(CharacterCasing.Normal)]
    [Description("Gets or sets whether the TextBox control modifies the case of characters as they are typed.")]
    [Category("Behavior")]
    public CharacterCasing CharacterCasing
    {
      get
      {
        return this.textBoxElement.CharacterCasing;
      }
      set
      {
        this.textBoxElement.CharacterCasing = value;
      }
    }

    [Description("Gets or sets a value indicating whether text in the text box is read-only.")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool IsReadOnly
    {
      get
      {
        return this.textBoxElement.IsReadOnly;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (IsReadOnly)] = (object) value;
        else
          this.textBoxElement.IsReadOnly = value;
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the caret is visible in read only mode.")]
    public bool IsReadOnlyCaretVisible
    {
      get
      {
        return this.textBoxElement.IsReadOnlyCaretVisible;
      }
      set
      {
        this.textBoxElement.IsReadOnlyCaretVisible = value;
      }
    }

    [DefaultValue(null)]
    [Category("Behavior")]
    [Description("Gets or sets the shortcut menu associated to the RadTreeView.")]
    public virtual RadContextMenu RadContextMenu
    {
      get
      {
        return this.textBoxElement.ContextMenu;
      }
      set
      {
        this.textBoxElement.ContextMenu = value;
      }
    }

    [Description("Gets or sets a value indicating whether the clear button is shown.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool ShowClearButton
    {
      get
      {
        return this.textBoxElement.ShowClearButton;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (ShowClearButton)] = (object) value;
        else
          this.textBoxElement.ShowClearButton = value;
      }
    }

    public event SelectionChangingEventHandler SelectionChanging
    {
      add
      {
        this.textBoxElement.SelectionChanging += value;
      }
      remove
      {
        this.textBoxElement.SelectionChanging -= value;
      }
    }

    public event SelectionChangedEventHandler SelectionChanged
    {
      add
      {
        this.textBoxElement.SelectionChanged += value;
      }
      remove
      {
        this.textBoxElement.SelectionChanged -= value;
      }
    }

    public event TextChangingEventHandler TextChanging;

    public event TextBlockFormattingEventHandler TextBlockFormatting
    {
      add
      {
        this.textBoxElement.TextBlockFormatting += value;
      }
      remove
      {
        this.textBoxElement.TextBlockFormatting -= value;
      }
    }

    public event CreateTextBlockEventHandler CreateTextBlock
    {
      add
      {
        this.textBoxElement.CreateTextBlock += value;
      }
      remove
      {
        this.textBoxElement.CreateTextBlock -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when opening the context menu.")]
    public event TreeBoxContextMenuOpeningEventHandler ContextMenuOpening
    {
      add
      {
        this.textBoxElement.ContextMenuOpening += value;
      }
      remove
      {
        this.textBoxElement.ContextMenuOpening -= value;
      }
    }

    public event EventHandler IMECompositionStarted
    {
      add
      {
        this.TextBoxElement.IMECompositionStarted += value;
      }
      remove
      {
        this.TextBoxElement.IMECompositionStarted -= value;
      }
    }

    public event EventHandler IMECompositionEnded
    {
      add
      {
        this.TextBoxElement.IMECompositionEnded += value;
      }
      remove
      {
        this.TextBoxElement.IMECompositionEnded -= value;
      }
    }

    public event EventHandler<IMECompositionResultEventArgs> IMECompositionResult
    {
      add
      {
        this.TextBoxElement.IMECompositionResult += value;
      }
      remove
      {
        this.TextBoxElement.IMECompositionResult -= value;
      }
    }

    public void AppendText(string text)
    {
      this.textBoxElement.AppendText(text);
    }

    public void Clear()
    {
      this.textBoxElement.Clear();
    }

    public bool DeselectAll()
    {
      return this.textBoxElement.DeselectAll();
    }

    public void ScrollToCaret()
    {
      this.textBoxElement.ScrollToCaret();
    }

    public void Select(int start, int length)
    {
      this.textBoxElement.Select(start, length);
    }

    public void SelectAll()
    {
      this.textBoxElement.SelectAll();
    }

    public bool Cut()
    {
      return this.textBoxElement.Cut();
    }

    public bool Copy()
    {
      return this.textBoxElement.Copy();
    }

    public bool Paste()
    {
      return this.textBoxElement.Paste();
    }

    public bool Insert(string text)
    {
      return this.textBoxElement.Insert(text);
    }

    public bool Delete()
    {
      return this.textBoxElement.Delete();
    }

    public bool Delete(bool nextCharacter)
    {
      return this.textBoxElement.Delete(nextCharacter);
    }

    protected override bool ProcessDialogChar(char charCode)
    {
      Keys modifierKeys = Control.ModifierKeys;
      if ((modifierKeys & Keys.Alt) == Keys.Alt || (modifierKeys & Keys.Control) == Keys.Control)
        return base.ProcessDialogChar(charCode);
      return false;
    }

    protected override bool ProcessMnemonic(char charCode)
    {
      return false;
    }

    protected virtual void OnTextChanging(TextChangingEventArgs e)
    {
      TextChangingEventHandler textChanging = this.TextChanging;
      if (textChanging == null)
        return;
      textChanging((object) this, e);
    }

    private void OnTextBoxElementTextChanging(object sender, TextChangingEventArgs e)
    {
      this.OnTextChanging(e);
    }

    private void OnTextBoxElementTextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.textBoxElement.Focus();
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.textBoxElement.SetElementFocused(false);
      int num = (int) this.textBoxElement.SetValue(RadElement.ContainsFocusProperty, (object) false);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      this.CallBaseOnKeyPress(e);
      this.Behavior.OnKeyPress(e);
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
      bool flag = !value;
      this.RootElement.StretchHorizontally = flag;
      this.RootElement.StretchVertically = flag;
      this.RootElement.SaveCurrentStretchModeAsDefault();
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.TextBoxElement.SuspendApplyOfThemeSettings();
      this.TextBoxElement.ViewElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.TextBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.TextBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.TextBoxElement.ViewElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
      }
      this.TextBoxElement.ResumeApplyOfThemeSettings();
      this.TextBoxElement.ViewElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.TextBoxElement.SuspendApplyOfThemeSettings();
      this.TextBoxElement.ViewElement.SuspendApplyOfThemeSettings();
      this.TextBoxElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.TextBoxElement.ViewElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.TextBoxElement.ResumeApplyOfThemeSettings();
      this.TextBoxElement.ViewElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.TextBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.TextBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.TextBoxElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.TextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.TextBoxElement.SuspendApplyOfThemeSettings();
      this.TextBoxElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.TextBoxElement.ElementTree.ApplyThemeToElementTree();
      this.TextBoxElement.ResumeApplyOfThemeSettings();
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ImageList ImageList
    {
      get
      {
        return base.ImageList;
      }
      set
      {
        base.ImageList = value;
      }
    }
  }
}
