// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class RadTextBoxItem : RadHostItem
  {
    private Dictionary<string, object> disposedTextBoxValues = new Dictionary<string, object>();
    public static readonly RoutedEvent MultilineEvent = RadElement.RegisterRoutedEvent(nameof (MultilineEvent), typeof (RadTextBoxItem));
    public static RadProperty NullTextProperty = RadProperty.Register(nameof (NullText), typeof (string), typeof (RadTextBoxItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty NullTextColorProperty = RadProperty.Register(nameof (NullTextColor), typeof (Color), typeof (RadTextBoxItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.GrayText, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsNullTextProperty = RadProperty.Register("IsNullText", typeof (bool), typeof (RadTextBoxItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.None));
    private static readonly object TabStopChangedEventKey = new object();
    private static readonly object AcceptsTabChangedEventKey = new object();
    private static readonly object HideSelectionChangedEventKey = new object();
    private static readonly object ModifiedChangedEventKey = new object();
    private static readonly object MultilineChangedEventKey = new object();
    private static readonly object PreviewKeyDownEventKey = new object();
    private static readonly object ReadOnlyChangedEventKey = new object();
    private static readonly object TextAlignChangedEventKey = new object();
    internal const long ShouldTextChangedFireStateKey = 17592186044416;
    internal const long TextLockStateKey = 35184372088832;
    internal const long ControlLockStateKey = 70368744177664;
    internal const long TextLock2StateKey = 140737488355328;
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected const long RadTextBoxItemLastStateKey = 140737488355328;
    private int selectionLengthBeforeTextChange;

    public RadTextBoxItem(Control hostedControl)
      : base(hostedControl)
    {
      HostedTextBoxBase hostedTextBoxBase = hostedControl as HostedTextBoxBase;
      if (this.UseGenericBorderPaint)
        hostedTextBoxBase.BorderStyle = BorderStyle.Fixed3D;
      else
        hostedTextBoxBase.BorderStyle = BorderStyle.None;
      hostedTextBoxBase.Enter += new EventHandler(this.TextBoxControl_Enter);
      hostedTextBoxBase.Leave += new EventHandler(this.TextBoxControl_Leave);
      hostedTextBoxBase.TabStopChanged += new EventHandler(this.TextBoxControl_TabStopChanged);
      hostedTextBoxBase.AcceptsTabChanged += new EventHandler(this.TextBoxControl_AcceptsTabChanged);
      hostedTextBoxBase.HideSelectionChanged += new EventHandler(this.TextBoxControl_HideSelectionChanged);
      hostedTextBoxBase.ModifiedChanged += new EventHandler(this.TextBoxControl_ModifiedChanged);
      hostedTextBoxBase.MultilineChanged += new EventHandler(this.TextBoxControl_MultilineChanged);
      hostedTextBoxBase.ReadOnlyChanged += new EventHandler(this.TextBoxControl_ReadOnlyChanged);
      hostedTextBoxBase.TextAlignChanged += new EventHandler(this.TextBoxControl_TextAlignChanged);
      hostedTextBoxBase.TextChanged += new EventHandler(this.TextBoxControl_TextChanged);
      hostedTextBoxBase.KeyDown += new KeyEventHandler(this.TextBoxControl_KeyDown);
      hostedTextBoxBase.KeyPress += new KeyPressEventHandler(this.TextBoxControl_KeyPress);
      hostedTextBoxBase.KeyUp += new KeyEventHandler(this.TextBoxControl_KeyUp);
      hostedTextBoxBase.PreviewKeyDown += new PreviewKeyDownEventHandler(this.TextBoxControl_PreviewKeyDown);
      hostedTextBoxBase.MouseEnter += new EventHandler(this.TextBoxControl_MouseEnter);
      hostedTextBoxBase.MouseLeave += new EventHandler(this.textBoxItem_MouseLeave);
      hostedTextBoxBase.Disposed += new EventHandler(this.textBox_Disposed);
    }

    private void textBox_Disposed(object sender, EventArgs e)
    {
      HostedTextBoxBase hostedTextBoxBase = (HostedTextBoxBase) sender;
      hostedTextBoxBase.Disposed -= new EventHandler(this.textBox_Disposed);
      bool isHandleCreated = hostedTextBoxBase.IsHandleCreated;
      this.disposedTextBoxValues.Add("Text", isHandleCreated ? (object) this.TextBoxControl.Text : (object) string.Empty);
      this.disposedTextBoxValues.Add("TabStop", (object) (bool) (isHandleCreated ? (this.TextBoxControl.TabStop ? 1 : 0) : 1));
      this.disposedTextBoxValues.Add("AcceptsReturn", (object) (bool) (isHandleCreated ? (this.TextBoxControl.AcceptsReturn ? 1 : 0) : 0));
      this.disposedTextBoxValues.Add("AcceptsTab", (object) (bool) (isHandleCreated ? (this.TextBoxControl.AcceptsTab ? 1 : 0) : 1));
      this.disposedTextBoxValues.Add("CharacterCasing", (object) (CharacterCasing) (isHandleCreated ? (int) this.TextBoxControl.CharacterCasing : 0));
      this.disposedTextBoxValues.Add("HideSelection", (object) (bool) (isHandleCreated ? (this.TextBoxControl.HideSelection ? 1 : 0) : 0));
      this.disposedTextBoxValues.Add("MaxLength", (object) (isHandleCreated ? this.TextBoxControl.MaxLength : (int) ushort.MaxValue));
      this.disposedTextBoxValues.Add("Modified", (object) (bool) (isHandleCreated ? (this.TextBoxControl.Modified ? 1 : 0) : 0));
      this.disposedTextBoxValues.Add("Multiline", (object) (bool) (isHandleCreated ? (this.TextBoxControl.Multiline ? 1 : 0) : 0));
      this.disposedTextBoxValues.Add("PasswordChar", (object) (char) (isHandleCreated ? (int) this.TextBoxControl.PasswordChar : 42));
      this.disposedTextBoxValues.Add("ReadOnly", (object) (bool) (isHandleCreated ? (this.TextBoxControl.ReadOnly ? 1 : 0) : 0));
      this.disposedTextBoxValues.Add("ScrollBars", (object) (ScrollBars) (isHandleCreated ? (int) this.TextBoxControl.ScrollBars : 0));
      this.disposedTextBoxValues.Add("SelectedText", isHandleCreated ? (object) this.TextBoxControl.SelectedText : (object) string.Empty);
      this.disposedTextBoxValues.Add("SelectionLength", (object) (isHandleCreated ? this.TextBoxControl.SelectionLength : 0));
      this.disposedTextBoxValues.Add("SelectionStart", (object) (isHandleCreated ? this.TextBoxControl.SelectionStart : 0));
      this.disposedTextBoxValues.Add("ShortcutsEnabled", (object) (bool) (isHandleCreated ? (this.TextBoxControl.ShortcutsEnabled ? 1 : 0) : 1));
      this.disposedTextBoxValues.Add("TextAlign", (object) (HorizontalAlignment) (isHandleCreated ? (int) this.TextBoxControl.TextAlign : 0));
      this.disposedTextBoxValues.Add("WordWrap", (object) (bool) (isHandleCreated ? (this.TextBoxControl.WordWrap ? 1 : 0) : 0));
      this.disposedTextBoxValues.Add("UseGenericBorderPaint", (object) (bool) (isHandleCreated ? (this.TextBoxControl.UseGenericBorderPaint ? 1 : 0) : 0));
    }

    public RadTextBoxItem()
      : this((Control) new HostedTextBoxBase())
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BitState[17592186044416L] = true;
      this.StretchHorizontally = true;
      this.StretchVertically = false;
      this.Alignment = ContentAlignment.MiddleLeft;
    }

    public override string Text
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (string) this.disposedTextBoxValues[nameof (Text)];
        return this.TextBoxControl.Text;
      }
      set
      {
        this.BitState[140737488355328L] = true;
        int num = (int) this.SetValue(RadItem.TextProperty, (object) value);
        this.BitState[140737488355328L] = false;
      }
    }

    public bool TabStop
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (bool) this.disposedTextBoxValues[nameof (TabStop)];
        return this.TextBoxControl.TabStop;
      }
      set
      {
        this.TextBoxControl.TabStop = value;
      }
    }

    [RadDefaultValue("AcceptsReturn", typeof (System.Windows.Forms.TextBox))]
    public bool AcceptsReturn
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (bool) this.disposedTextBoxValues[nameof (AcceptsReturn)];
        return this.TextBoxControl.AcceptsReturn;
      }
      set
      {
        this.TextBoxControl.AcceptsReturn = value;
      }
    }

    [RadDefaultValue("AcceptsTab", typeof (System.Windows.Forms.TextBox))]
    public bool AcceptsTab
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (bool) this.disposedTextBoxValues[nameof (AcceptsTab)];
        return this.TextBoxControl.AcceptsTab;
      }
      set
      {
        this.TextBoxControl.AcceptsTab = value;
      }
    }

    [Browsable(false)]
    public bool CanUndo
    {
      get
      {
        return this.TextBoxControl.CanUndo;
      }
    }

    public CharacterCasing CharacterCasing
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (CharacterCasing) this.disposedTextBoxValues[nameof (CharacterCasing)];
        return this.TextBoxControl.CharacterCasing;
      }
      set
      {
        this.TextBoxControl.CharacterCasing = value;
      }
    }

    [RadDefaultValue("HideSelection", typeof (System.Windows.Forms.TextBox))]
    public bool HideSelection
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (bool) this.disposedTextBoxValues[nameof (HideSelection)];
        return this.TextBoxControl.HideSelection;
      }
      set
      {
        this.TextBoxControl.HideSelection = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] Lines
    {
      get
      {
        return this.TextBoxControl.Lines;
      }
      set
      {
        this.TextBoxControl.Lines = value;
      }
    }

    [RadDefaultValue("MaxLength", typeof (System.Windows.Forms.TextBox))]
    public int MaxLength
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (int) this.disposedTextBoxValues[nameof (MaxLength)];
        return this.TextBoxControl.MaxLength;
      }
      set
      {
        this.TextBoxControl.MaxLength = value;
      }
    }

    public bool Modified
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (bool) this.disposedTextBoxValues[nameof (Modified)];
        return this.TextBoxControl.Modified;
      }
      set
      {
        this.TextBoxControl.Modified = value;
      }
    }

    [RadDefaultValue("Multiline", typeof (HostedTextBoxBase))]
    public bool Multiline
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (bool) this.disposedTextBoxValues[nameof (Multiline)];
        return this.TextBoxControl.Multiline;
      }
      set
      {
        this.TextBoxControl.Multiline = value;
      }
    }

    [RadDefaultValue("PasswordChar", typeof (System.Windows.Forms.TextBox))]
    public char PasswordChar
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (char) this.disposedTextBoxValues[nameof (PasswordChar)];
        return this.TextBoxControl.PasswordChar;
      }
      set
      {
        this.TextBoxControl.PasswordChar = value;
      }
    }

    [Browsable(false)]
    public int PreferedHeght
    {
      get
      {
        return this.TextBoxControl.PreferredHeight;
      }
    }

    [RadDefaultValue("ReadOnly", typeof (System.Windows.Forms.TextBox))]
    public bool ReadOnly
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (bool) this.disposedTextBoxValues[nameof (ReadOnly)];
        return this.TextBoxControl.ReadOnly;
      }
      set
      {
        this.TextBoxControl.ReadOnly = value;
      }
    }

    public ScrollBars ScrollBars
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (ScrollBars) this.disposedTextBoxValues[nameof (ScrollBars)];
        return this.TextBoxControl.ScrollBars;
      }
      set
      {
        this.TextBoxControl.ScrollBars = value;
      }
    }

    public string SelectedText
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (string) this.disposedTextBoxValues[nameof (SelectedText)];
        return this.TextBoxControl.SelectedText;
      }
      set
      {
        this.TextBoxControl.SelectedText = value;
      }
    }

    public virtual int SelectionLength
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (int) this.disposedTextBoxValues[nameof (SelectionLength)];
        return this.TextBoxControl.SelectionLength;
      }
      set
      {
        this.TextBoxControl.SelectionLength = value;
      }
    }

    public virtual int SelectionStart
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (int) this.disposedTextBoxValues[nameof (SelectionStart)];
        return this.TextBoxControl.SelectionStart;
      }
      set
      {
        this.TextBoxControl.SelectionStart = value;
      }
    }

    [RadDefaultValue("ShortcutsEnabled", typeof (System.Windows.Forms.TextBox))]
    public bool ShortcutsEnabled
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (bool) this.disposedTextBoxValues[nameof (ShortcutsEnabled)];
        return this.TextBoxControl.ShortcutsEnabled;
      }
      set
      {
        this.TextBoxControl.ShortcutsEnabled = value;
      }
    }

    public HorizontalAlignment TextAlign
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (HorizontalAlignment) this.disposedTextBoxValues[nameof (TextAlign)];
        return this.TextBoxControl.TextAlign;
      }
      set
      {
        this.TextBoxControl.TextAlign = value;
      }
    }

    [Browsable(false)]
    public int TextLength
    {
      get
      {
        return this.TextBoxControl.TextLength;
      }
    }

    [RadDefaultValue("WordWrap", typeof (System.Windows.Forms.TextBox))]
    public bool WordWrap
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (bool) this.disposedTextBoxValues[nameof (WordWrap)];
        return this.TextBoxControl.WordWrap;
      }
      set
      {
        this.TextBoxControl.WordWrap = value;
      }
    }

    [RadPropertyDefaultValue("NullText", typeof (RadTextBoxItem))]
    public string NullText
    {
      get
      {
        return (string) this.GetValue(RadTextBoxItem.NullTextProperty);
      }
      set
      {
        if (value == null)
          value = string.Empty;
        HostedTextBoxBase textBoxControl = this.TextBoxControl;
        if (textBoxControl != null)
          textBoxControl.NullText = value;
        int num = (int) this.SetValue(RadTextBoxItem.NullTextProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("NullTextColor", typeof (RadTextBoxItem))]
    public Color NullTextColor
    {
      get
      {
        return (Color) this.GetValue(RadTextBoxItem.NullTextColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTextBoxItem.NullTextColorProperty, (object) value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool VsbVisible
    {
      get
      {
        return false;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public HostedTextBoxBase TextBoxControl
    {
      get
      {
        return (HostedTextBoxBase) this.HostedControl;
      }
    }

    [DefaultValue(false)]
    public override bool StretchVertically
    {
      get
      {
        return base.StretchVertically;
      }
      set
      {
        base.StretchVertically = value;
      }
    }

    [RadDescription("UseGenericBorderPaint", typeof (HostedTextBoxBase))]
    [RadDefaultValue("UseGenericBorderPaint", typeof (HostedTextBoxBase))]
    public bool UseGenericBorderPaint
    {
      get
      {
        if (this.TextBoxControl.IsDisposed)
          return (bool) this.disposedTextBoxValues[nameof (UseGenericBorderPaint)];
        return this.TextBoxControl.UseGenericBorderPaint;
      }
      set
      {
        this.TextBoxControl.UseGenericBorderPaint = value;
        if (value)
          this.TextBoxControl.BorderStyle = BorderStyle.Fixed3D;
        else
          this.TextBoxControl.BorderStyle = BorderStyle.None;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldTextChangedFire
    {
      get
      {
        return this.BitState[17592186044416L];
      }
      set
      {
        this.SetBitState(17592186044416L, value);
      }
    }

    [Description("Occurs when the TabStop property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler TabStopChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxItem.TabStopChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxItem.TabStopChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the AcceptsTab property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler AcceptsTabChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxItem.AcceptsTabChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxItem.AcceptsTabChangedEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Description("Occurs when the HideSelection property has changed.")]
    [Category("Property Changed")]
    public event EventHandler HideSelectionChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxItem.HideSelectionChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxItem.HideSelectionChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the Modified property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler ModifiedChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxItem.ModifiedChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxItem.ModifiedChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnModifiedChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxItem.ModifiedChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Description("Occurs when the Multiline property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler MultilineChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxItem.MultilineChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxItem.MultilineChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when a key is pressed while focus is on text box.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event PreviewKeyDownEventHandler PreviewKeyDown
    {
      add
      {
        this.Events.AddHandler(RadTextBoxItem.PreviewKeyDownEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxItem.PreviewKeyDownEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the ReadOnly property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler ReadOnlyChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxItem.ReadOnlyChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxItem.ReadOnlyChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the TextAlign property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler TextAlignChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxItem.TextAlignChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxItem.TextAlignChangedEventKey, (Delegate) value);
      }
    }

    public void AppendText(string text)
    {
      this.TextBoxControl.AppendText(text);
    }

    public void Clear()
    {
      this.TextBoxControl.Clear();
    }

    public void ClearUndo()
    {
      this.TextBoxControl.ClearUndo();
    }

    public void Copy()
    {
      this.TextBoxControl.Copy();
    }

    public void Cut()
    {
      this.TextBoxControl.Cut();
    }

    public void DeselectAll()
    {
      this.TextBoxControl.DeselectAll();
    }

    public char GetCharFromPosition(Point point)
    {
      return this.TextBoxControl.GetCharFromPosition(point);
    }

    public int GetCharIndexFromPosition(Point point)
    {
      return this.TextBoxControl.GetCharIndexFromPosition(point);
    }

    public int GetFirstCharIndexFromLine(int lineNumber)
    {
      return this.TextBoxControl.GetFirstCharIndexFromLine(lineNumber);
    }

    public int GetFirstCharIndexOfCurrentLine()
    {
      return this.TextBoxControl.GetFirstCharIndexOfCurrentLine();
    }

    public int GetLineFromCharIndex(int index)
    {
      return this.TextBoxControl.GetLineFromCharIndex(index);
    }

    public Point GetPositionFromCharIndex(int index)
    {
      return this.TextBoxControl.GetPositionFromCharIndex(index);
    }

    public void Paste()
    {
      this.TextBoxControl.Paste();
    }

    public void Paste(string text)
    {
      this.TextBoxControl.Paste(text);
    }

    public void ScrollToCaret()
    {
      this.TextBoxControl.ScrollToCaret();
    }

    public void Select(int start, int length)
    {
      this.TextBoxControl.Select(start, length);
    }

    public void SelectAll()
    {
      this.TextBoxControl.SelectAll();
    }

    public void Undo()
    {
      this.TextBoxControl.Undo();
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnTabStopChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxItem.TabStopChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnAcceptsTabChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxItem.AcceptsTabChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnHideSelectionChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxItem.HideSelectionChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMultilineChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxItem.MultilineChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
      PreviewKeyDownEventHandler downEventHandler = (PreviewKeyDownEventHandler) this.Events[RadTextBoxItem.PreviewKeyDownEventKey];
      if (downEventHandler == null)
        return;
      downEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnReadOnlyChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxItem.ReadOnlyChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnTextAlignChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxItem.TextAlignChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadItem.TextProperty)
      {
        string str = (string) e.NewValue;
        string oldValue = (string) e.OldValue;
        if (str == null)
          str = string.Empty;
        if (oldValue == str)
          return;
        if (str == string.Empty)
        {
          int num1 = (int) this.SetValue(RadTextBoxItem.IsNullTextProperty, (object) true);
        }
        else
        {
          int num2 = (int) this.SetValue(RadTextBoxItem.IsNullTextProperty, (object) false);
        }
        if (!this.GetBitState(70368744177664L) && !this.GetBitState(35184372088832L))
        {
          this.BitState[35184372088832L] = true;
          this.TextBoxControl.Text = str;
          this.BitState[35184372088832L] = false;
        }
        else if (this.GetBitState(70368744177664L) && this.GetBitState(140737488355328L))
          this.TextBoxControl.Text = str;
      }
      else if (e.Property == RadTextBoxItem.NullTextColorProperty)
      {
        HostedTextBoxBase textBoxControl = this.TextBoxControl;
        if (textBoxControl != null)
          textBoxControl.PromptForeColor = this.NullTextColor;
      }
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      this.TextBoxControl.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      this.NullText = this.NullText;
    }

    protected override void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnTunnelEvent(sender, args);
      if (this.ElementTree != null && !typeof (RadTextBox).IsAssignableFrom(this.ElementTree.Control.GetType()) || args.RoutedEvent != RootRadElement.AutoSizeChangedEvent)
        return;
      if (((AutoSizeEventArgs) args.OriginalEventArgs).AutoSize)
        this.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      else
        this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
    }

    protected override void OnTextChanging(TextChangingEventArgs e)
    {
      if (!this.GetBitState(17592186044416L))
        return;
      int selectionStart = this.TextBoxControl.SelectionStart;
      base.OnTextChanging(e);
      if (!e.Cancel)
        return;
      if (selectionStart > 0)
        --selectionStart;
      this.SetTextBoxTextSilently(e.OldValue, selectionStart);
    }

    protected void TextBoxControl_AcceptsTabChanged(object sender, EventArgs e)
    {
      this.OnAcceptsTabChanged(e);
    }

    protected void TextBoxControl_HideSelectionChanged(object sender, EventArgs e)
    {
      this.OnHideSelectionChanged(e);
    }

    protected void TextBoxControl_ModifiedChanged(object sender, EventArgs e)
    {
      this.OnModifiedChanged(e);
    }

    protected void TextBoxControl_MultilineChanged(object sender, EventArgs e)
    {
      this.StretchVertically = this.TextBoxControl.Multiline;
      this.RaiseBubbleEvent((RadElement) this, new RoutedEventArgs(e, RadTextBoxItem.MultilineEvent));
      this.OnMultilineChanged(e);
    }

    protected void TextBoxControl_ReadOnlyChanged(object sender, EventArgs e)
    {
      this.OnReadOnlyChanged(e);
    }

    protected void TextBoxControl_TextAlignChanged(object sender, EventArgs e)
    {
      this.OnTextAlignChanged(e);
    }

    protected void TextBoxControl_TextChanged(object sender, EventArgs e)
    {
      if (!this.GetBitState(17592186044416L) || this.GetBitState(70368744177664L) || this.GetBitState(35184372088832L))
        return;
      this.BitState[70368744177664L] = true;
      int num = (int) this.SetValue(RadItem.TextProperty, (object) this.TextBoxControl.Text);
      this.BitState[70368744177664L] = false;
    }

    private void TextBoxControl_Enter(object sender, EventArgs e)
    {
      this.Focus();
    }

    private void TextBoxControl_Leave(object sender, EventArgs e)
    {
      this.KillFocus();
    }

    private void TextBoxControl_MouseEnter(object sender, EventArgs e)
    {
      this.ContainsMouse = true;
      if (this.ElementTree == null)
        return;
      this.ElementTree.RootElement.ContainsMouse = true;
    }

    private void textBoxItem_MouseLeave(object sender, EventArgs e)
    {
      this.ContainsMouse = false;
      if (this.ElementTree == null)
        return;
      this.ElementTree.RootElement.ContainsMouse = false;
    }

    private void TextBoxControl_TabStopChanged(object sender, EventArgs e)
    {
      this.OnTabStopChanged(e);
    }

    private void TextBoxControl_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
      if (!this.Multiline || e.Handled || e.KeyData != (Keys.A | Keys.Control))
        return;
      this.SelectAll();
      e.Handled = e.SuppressKeyPress = true;
    }

    private void TextBoxControl_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
      if (e.KeyChar != '\r' || this.AcceptsReturn)
        return;
      e.Handled = true;
    }

    private void TextBoxControl_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void TextBoxControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
      this.selectionLengthBeforeTextChange = this.TextBoxControl.SelectionLength;
      this.OnPreviewKeyDown(e);
    }

    private void SetTextBoxTextSilently(string text, int selectionStart)
    {
      this.BitState[17592186044416L] = false;
      this.TextBoxControl.Text = text;
      this.TextBoxControl.SelectionStart = selectionStart;
      this.TextBoxControl.SelectionLength = this.selectionLengthBeforeTextChange;
      this.BitState[17592186044416L] = true;
    }
  }
}
