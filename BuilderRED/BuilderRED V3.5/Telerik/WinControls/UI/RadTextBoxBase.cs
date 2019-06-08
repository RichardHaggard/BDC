// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [DefaultBindingProperty("Text")]
  public abstract class RadTextBoxBase : RadEditorControl
  {
    private static readonly object AcceptsTabChangedEventKey = new object();
    private static readonly object HideSelectionChangedEventKey = new object();
    private static readonly object ModifiedChangedEventKey = new object();
    private static readonly object MultilineChangedEventKey = new object();
    private static readonly object ReadOnlyChangedEventKey = new object();
    private static readonly object TextAlignChangedEventKey = new object();
    private static readonly object TextChangingEventKey = new object();
    private static readonly object PropertyChangedEventKey = new object();
    private bool entering;

    public RadTextBoxBase()
    {
      this.AutoSize = true;
      this.SetStyle(ControlStyles.Selectable, true);
      base.TabStop = false;
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireHostEvents();
      base.Dispose(disposing);
    }

    protected internal abstract void InitializeTextElement();

    protected internal virtual void WireHostEvents()
    {
      this.TextBoxItem.AcceptsTabChanged += new EventHandler(this.textBoxItem_AcceptsTabChanged);
      this.TextBoxItem.HideSelectionChanged += new EventHandler(this.textBoxItem_HideSelectionChanged);
      this.TextBoxItem.ModifiedChanged += new EventHandler(this.textBoxItem_ModifiedChanged);
      this.TextBoxItem.MultilineChanged += new EventHandler(this.textBoxItem_MultilineChanged);
      this.TextBoxItem.ReadOnlyChanged += new EventHandler(this.textBoxItem_ReadOnlyChanged);
      this.TextBoxItem.TextAlignChanged += new EventHandler(this.textBoxItem_TextAlignChanged);
      this.TextBoxItem.TextChanged += new EventHandler(this.textBoxItem_TextChanged);
      this.TextBoxItem.TextChanging += new TextChangingEventHandler(this.textBoxItem_TextChanging);
      this.TextBoxItem.HostedControl.GotFocus += new EventHandler(this.HostedControl_GotFocus);
      this.TextBoxItem.HostedControl.LostFocus += new EventHandler(this.HostedControl_LostFocus);
    }

    protected internal virtual void UnwireHostEvents()
    {
      this.TextBoxItem.AcceptsTabChanged -= new EventHandler(this.textBoxItem_AcceptsTabChanged);
      this.TextBoxItem.HideSelectionChanged -= new EventHandler(this.textBoxItem_HideSelectionChanged);
      this.TextBoxItem.ModifiedChanged -= new EventHandler(this.textBoxItem_ModifiedChanged);
      this.TextBoxItem.MultilineChanged -= new EventHandler(this.textBoxItem_MultilineChanged);
      this.TextBoxItem.ReadOnlyChanged -= new EventHandler(this.textBoxItem_ReadOnlyChanged);
      this.TextBoxItem.TextAlignChanged -= new EventHandler(this.textBoxItem_TextAlignChanged);
      this.TextBoxItem.TextChanged -= new EventHandler(this.textBoxItem_TextChanged);
      this.TextBoxItem.TextChanging -= new TextChangingEventHandler(this.textBoxItem_TextChanging);
      this.TextBoxItem.HostedControl.GotFocus -= new EventHandler(this.HostedControl_GotFocus);
      this.TextBoxItem.HostedControl.LostFocus -= new EventHandler(this.HostedControl_LostFocus);
    }

    [DefaultValue(true)]
    public new bool TabStop
    {
      get
      {
        if (this.TextBoxItem != null)
          return this.TextBoxItem.TabStop;
        return base.TabStop;
      }
      set
      {
        if (this.TextBoxItem != null)
        {
          base.TabStop = false;
          this.TextBoxItem.TabStop = value;
        }
        else
          base.TabStop = value;
      }
    }

    internal abstract RadTextBoxItem TextBoxItem { get; }

    [DefaultValue(true)]
    [Category("Layout")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
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
        return RadControl.GetDpiScaledSize(new Size(100, 20));
      }
    }

    [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Appearance")]
    public override string Text
    {
      get
      {
        return this.TextBoxItem.Text;
      }
      set
      {
        if (!(this.TextBoxItem.Text != value))
          return;
        this.TextBoxItem.Text = value;
      }
    }

    [Category("Appearance")]
    public override Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        if (value == null)
        {
          int num = (int) this.TextBoxItem.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        }
        else
          this.TextBoxItem.Font = value;
        base.Font = value;
      }
    }

    [Localizable(true)]
    [Browsable(true)]
    [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets a custom StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [RadDefaultValue("AutoCompleteCustomSource", typeof (HostedTextBoxBase))]
    public AutoCompleteStringCollection AutoCompleteCustomSource
    {
      get
      {
        return this.TextBoxItem.TextBoxControl.AutoCompleteCustomSource;
      }
      set
      {
        this.TextBoxItem.TextBoxControl.AutoCompleteCustomSource = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description("Gets or sets an option that controls how automatic completion works for the TextBox.")]
    [Browsable(true)]
    [RadDefaultValue("AutoCompleteMode", typeof (HostedTextBoxBase))]
    public AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.TextBoxItem.TextBoxControl.AutoCompleteMode;
      }
      set
      {
        this.TextBoxItem.TextBoxControl.AutoCompleteMode = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [TypeConverter(typeof (TextBoxAutoCompleteSourceConverter))]
    [Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
    [RadDefaultValue("AutoCompleteSource", typeof (HostedTextBoxBase))]
    public AutoCompleteSource AutoCompleteSource
    {
      get
      {
        return this.TextBoxItem.TextBoxControl.AutoCompleteSource;
      }
      set
      {
        this.TextBoxItem.TextBoxControl.AutoCompleteSource = value;
      }
    }

    [Category("Behavior")]
    [RadDefaultValue("AcceptsReturn", typeof (RadTextBoxItem))]
    public bool AcceptsReturn
    {
      get
      {
        return this.TextBoxItem.AcceptsReturn;
      }
      set
      {
        this.TextBoxItem.AcceptsReturn = value;
      }
    }

    [RadDefaultValue("AcceptsTab", typeof (RadTextBoxItem))]
    [Category("Behavior")]
    public bool AcceptsTab
    {
      get
      {
        return this.TextBoxItem.AcceptsTab;
      }
      set
      {
        this.TextBoxItem.AcceptsTab = value;
      }
    }

    [Category("Behavior")]
    public bool CanUndo
    {
      get
      {
        return this.TextBoxItem.CanUndo;
      }
    }

    [DefaultValue(CharacterCasing.Normal)]
    [Category("Behavior")]
    public CharacterCasing CharacterCasing
    {
      get
      {
        return this.TextBoxItem.CharacterCasing;
      }
      set
      {
        this.TextBoxItem.CharacterCasing = value;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    public bool HideSelection
    {
      get
      {
        return this.TextBoxItem.HideSelection;
      }
      set
      {
        this.TextBoxItem.HideSelection = value;
      }
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] Lines
    {
      get
      {
        return this.TextBoxItem.Lines;
      }
      set
      {
        this.TextBoxItem.Lines = value;
      }
    }

    [DefaultValue(32767)]
    [Category("Behavior")]
    public int MaxLength
    {
      get
      {
        return this.TextBoxItem.MaxLength;
      }
      set
      {
        this.TextBoxItem.MaxLength = value;
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    public bool Modified
    {
      get
      {
        return this.TextBoxItem.Modified;
      }
      set
      {
        this.TextBoxItem.Modified = value;
      }
    }

    [RadDefaultValue("Multiline", typeof (RadTextBoxItem))]
    [Category("Behavior")]
    public bool Multiline
    {
      get
      {
        return this.TextBoxItem.Multiline;
      }
      set
      {
        this.TextBoxItem.Multiline = value;
      }
    }

    [Category("Behavior")]
    [Localizable(true)]
    [RadDefaultValue("NullText", typeof (RadTextBoxItem))]
    public string NullText
    {
      get
      {
        return this.TextBoxItem.NullText;
      }
      set
      {
        this.TextBoxItem.NullText = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the null text will be shown when the control is focused and the text is empty.")]
    public bool ShowNullText
    {
      get
      {
        return this.TextBoxItem.TextBoxControl.ShowNullText;
      }
      set
      {
        this.TextBoxItem.TextBoxControl.ShowNullText = value;
      }
    }

    [Category("Behavior")]
    [RadDefaultValue("PasswordChar", typeof (RadTextBoxItem))]
    public char PasswordChar
    {
      get
      {
        return this.TextBoxItem.PasswordChar;
      }
      set
      {
        this.TextBoxItem.PasswordChar = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
      get
      {
        return this.TextBoxItem.ReadOnly;
      }
      set
      {
        this.TextBoxItem.ReadOnly = value;
      }
    }

    [DefaultValue(ScrollBars.None)]
    [Category("Behavior")]
    public ScrollBars ScrollBars
    {
      get
      {
        return this.TextBoxItem.ScrollBars;
      }
      set
      {
        this.TextBoxItem.ScrollBars = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue("")]
    public string SelectedText
    {
      get
      {
        return this.TextBoxItem.SelectedText;
      }
      set
      {
        this.TextBoxItem.SelectedText = value;
      }
    }

    [DefaultValue(0)]
    public int SelectionLength
    {
      get
      {
        return this.TextBoxItem.SelectionLength;
      }
      set
      {
        this.TextBoxItem.SelectionLength = value;
      }
    }

    [DefaultValue(0)]
    public int SelectionStart
    {
      get
      {
        return this.TextBoxItem.SelectionStart;
      }
      set
      {
        this.TextBoxItem.SelectionStart = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    public bool ShortcutsEnabled
    {
      get
      {
        return this.TextBoxItem.ShortcutsEnabled;
      }
      set
      {
        this.TextBoxItem.ShortcutsEnabled = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(HorizontalAlignment.Left)]
    public virtual HorizontalAlignment TextAlign
    {
      get
      {
        return this.TextBoxItem.TextAlign;
      }
      set
      {
        this.TextBoxItem.TextAlign = value;
      }
    }

    [Category("Appearance")]
    public int TextLength
    {
      get
      {
        return this.TextBoxItem.TextLength;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    public bool WordWrap
    {
      get
      {
        return this.TextBoxItem.WordWrap;
      }
      set
      {
        this.TextBoxItem.WordWrap = value;
      }
    }

    [Description("Occurs when the AcceptsTab property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler AcceptsTabChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxBase.AcceptsTabChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxBase.AcceptsTabChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the HideSelection property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler HideSelectionChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxBase.HideSelectionChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxBase.HideSelectionChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the Modified property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler ModifiedChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxBase.ModifiedChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxBase.ModifiedChangedEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Category("Property Changed")]
    [Description("Occurs when the Multiline property has changed.")]
    public event EventHandler MultilineChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxBase.MultilineChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxBase.MultilineChangedEventKey, (Delegate) value);
      }
    }

    [Category("Property Changed")]
    [Browsable(true)]
    [Description("Occurs when the ReadOnly property has changed.")]
    public event EventHandler ReadOnlyChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxBase.ReadOnlyChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxBase.ReadOnlyChangedEventKey, (Delegate) value);
      }
    }

    [Category("Property Changed")]
    [Browsable(true)]
    [Description("Occurs when the TextAlign property has changed.")]
    public event EventHandler TextAlignChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxBase.TextAlignChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxBase.TextAlignChangedEventKey, (Delegate) value);
      }
    }

    [Category("Property Changed")]
    [Description("Occurs before the Text property changes.")]
    [Browsable(true)]
    public event TextChangingEventHandler TextChanging
    {
      add
      {
        this.Events.AddHandler(RadTextBoxBase.TextChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxBase.TextChangingEventKey, (Delegate) value);
      }
    }

    public void AppendText(string text)
    {
      this.TextBoxItem.AppendText(text);
    }

    public void Clear()
    {
      this.TextBoxItem.Clear();
    }

    public void ClearUndo()
    {
      this.TextBoxItem.ClearUndo();
    }

    public void Copy()
    {
      this.TextBoxItem.Copy();
    }

    public void Cut()
    {
      this.TextBoxItem.Cut();
    }

    public void DeselectAll()
    {
      this.TextBoxItem.DeselectAll();
    }

    public char GetCharFromPosition(Point point)
    {
      return this.TextBoxItem.GetCharFromPosition(point);
    }

    public int GetCharIndexFromPosition(Point point)
    {
      return this.TextBoxItem.GetCharIndexFromPosition(point);
    }

    public int GetFirstCharIndexFromLine(int lineNumber)
    {
      return this.TextBoxItem.GetFirstCharIndexFromLine(lineNumber);
    }

    public int GetFirstCharIndexOfCurrentLine()
    {
      return this.TextBoxItem.GetFirstCharIndexOfCurrentLine();
    }

    public int GetLineFromCharIndex(int index)
    {
      return this.TextBoxItem.GetLineFromCharIndex(index);
    }

    public Point GetPositionFromCharIndex(int index)
    {
      return this.TextBoxItem.GetPositionFromCharIndex(index);
    }

    public void Paste()
    {
      this.TextBoxItem.Paste();
    }

    public void Paste(string text)
    {
      this.TextBoxItem.Paste(text);
    }

    public void ScrollToCaret()
    {
      this.TextBoxItem.ScrollToCaret();
    }

    public void Select(int start, int length)
    {
      this.TextBoxItem.Select(start, length);
    }

    public void SelectAll()
    {
      this.TextBoxItem.SelectAll();
    }

    public void Undo()
    {
      this.TextBoxItem.Undo();
    }

    public new bool Focus()
    {
      return this.TextBoxItem.HostedControl.Focus();
    }

    public new void Select()
    {
      this.TextBoxItem.HostedControl.Select();
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnAcceptsTabChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxBase.AcceptsTabChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnHideSelectionChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxBase.HideSelectionChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnModifiedChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxBase.ModifiedChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMultilineChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxBase.MultilineChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnReadOnlyChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxBase.ReadOnlyChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnTextAlignChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxBase.TextAlignChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnTextChanging(TextChangingEventArgs e)
    {
      TextChangingEventHandler changingEventHandler = (TextChangingEventHandler) this.Events[RadTextBoxBase.TextChangingEventKey];
      if (changingEventHandler == null)
        return;
      changingEventHandler((object) this, e);
    }

    private void HostedControl_GotFocus(object sender, EventArgs e)
    {
      this.OnGotFocus(e);
    }

    private void HostedControl_LostFocus(object sender, EventArgs e)
    {
      this.OnLostFocus(e);
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 7)
        return;
      this.entering = true;
      this.TextBoxItem.TextBoxControl.Focus();
      this.entering = false;
    }

    protected override void OnLostFocus(EventArgs e)
    {
      if (this.entering)
        return;
      base.OnLostFocus(e);
    }

    private void textBoxItem_AcceptsTabChanged(object sender, EventArgs e)
    {
      this.OnAcceptsTabChanged(e);
    }

    private void textBoxItem_HideSelectionChanged(object sender, EventArgs e)
    {
      this.OnHideSelectionChanged(e);
    }

    private void textBoxItem_ModifiedChanged(object sender, EventArgs e)
    {
      this.OnModifiedChanged(e);
    }

    private void textBoxItem_MultilineChanged(object sender, EventArgs e)
    {
      this.OnMultilineChanged(e);
    }

    private void textBoxItem_ReadOnlyChanged(object sender, EventArgs e)
    {
      this.OnReadOnlyChanged(e);
    }

    private void textBoxItem_TextAlignChanged(object sender, EventArgs e)
    {
      this.OnTextAlignChanged(e);
    }

    private void textBoxItem_TextChanging(object sender, TextChangingEventArgs e)
    {
      this.OnTextChanging(e);
    }

    private void textBoxItem_TextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
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
  }
}
