// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTimePicker
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [TelerikToolboxCategory("Editors")]
  public class RadTimePicker : RadEditorControl
  {
    private static readonly object EventKeyDown = new object();
    private static readonly object EventKeyPress = new object();
    private static readonly object EventKeyUp = new object();
    private static readonly object MultilineChangedEventKey = new object();
    private static readonly object TextAlignChangedEventKey = new object();
    private static readonly object TimeCellFormattingEventKey = new object();
    private string oldText = string.Empty;
    private RadTimePickerElement timePickerElement;

    public RadTimePicker()
    {
      this.AutoSize = true;
      this.TabStop = false;
      this.SetStyle(ControlStyles.Selectable, true);
      this.WireEvents();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.timePickerElement = this.CreateTimePickerElement();
      parent.Children.Add((RadElement) this.timePickerElement);
    }

    protected virtual RadTimePickerElement CreateTimePickerElement()
    {
      return new RadTimePickerElement();
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    protected internal void WireEvents()
    {
      this.timePickerElement.ValueChanged += new EventHandler(this.OnValueChanged);
      this.timePickerElement.ValueChanging += new CancelEventHandler(this.OnValueChanging);
      this.timePickerElement.TextChanged += new EventHandler(this.maskEditBoxElement_TextChanged);
      this.timePickerElement.KeyDown += new KeyEventHandler(this.OnKeyDown);
      this.timePickerElement.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
      this.timePickerElement.KeyUp += new KeyEventHandler(this.OnKeyUp);
      this.timePickerElement.MaskedEditBox.MultilineChanged += new EventHandler(this.OnMultilineChanged);
      this.timePickerElement.MaskedEditBox.TextAlignChanged += new EventHandler(this.OnTextAlignChanged);
      this.timePickerElement.MaskedEditBox.TextBoxItem.LostFocus += new EventHandler(this.TextBoxItem_LostFocus);
      this.timePickerElement.MaskedEditBox.TextBoxItem.GotFocus += new EventHandler(this.TextBoxItem_GotFocus);
      this.timePickerElement.TimeCellFormatting += new TimeCellFormattingEventHandler(this.timePickerElement_TimeCellFormatting);
    }

    protected internal void UnwireEvents()
    {
      this.timePickerElement.ValueChanged -= new EventHandler(this.OnValueChanged);
      this.timePickerElement.ValueChanging -= new CancelEventHandler(this.OnValueChanging);
      this.timePickerElement.TextChanged -= new EventHandler(this.maskEditBoxElement_TextChanged);
      this.timePickerElement.KeyDown -= new KeyEventHandler(this.OnKeyDown);
      this.timePickerElement.KeyPress -= new KeyPressEventHandler(this.OnKeyPress);
      this.timePickerElement.KeyUp -= new KeyEventHandler(this.OnKeyUp);
      this.timePickerElement.MaskedEditBox.MultilineChanged -= new EventHandler(this.OnMultilineChanged);
      this.timePickerElement.MaskedEditBox.TextAlignChanged -= new EventHandler(this.OnTextAlignChanged);
      this.timePickerElement.MaskedEditBox.TextBoxItem.LostFocus -= new EventHandler(this.TextBoxItem_LostFocus);
      this.timePickerElement.MaskedEditBox.TextBoxItem.GotFocus -= new EventHandler(this.TextBoxItem_GotFocus);
      this.timePickerElement.TimeCellFormatting -= new TimeCellFormattingEventHandler(this.timePickerElement_TimeCellFormatting);
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(100, 20));
      }
    }

    [DefaultValue(true)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
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

    [Description("Gets or sets the Text associated to the Button below TimeTables")]
    [DefaultValue("Close")]
    [Localizable(true)]
    [Category("Appearance")]
    public virtual string CloseButtonText
    {
      get
      {
        return this.TimePickerElement.PopupContentElement.FooterPanel.ButtonElement.Text;
      }
      set
      {
        this.TimePickerElement.PopupContentElement.FooterPanel.ButtonElement.Text = value;
      }
    }

    [Description("Gets or sets the current culture associated to the RadMaskBox")]
    [Category("Behavior")]
    public CultureInfo Culture
    {
      get
      {
        return this.TimePickerElement.Culture;
      }
      set
      {
        if (object.ReferenceEquals((object) value, (object) this.TimePickerElement.Culture))
          return;
        this.TimePickerElement.Culture = value;
        this.OnNotifyPropertyChanged(nameof (Culture));
      }
    }

    [Description("Gets or sets the row height in time picker popup.")]
    [Browsable(false)]
    [DefaultValue(29)]
    [Category("Appearance")]
    public int RowHeight
    {
      get
      {
        return this.timePickerElement.RowHeight;
      }
      set
      {
        this.timePickerElement.RowHeight = value;
        this.OnNotifyPropertyChanged(nameof (RowHeight));
      }
    }

    [DefaultValue(4)]
    [Browsable(false)]
    [Description("Gets or sets the columns count.")]
    [Category("Appearance")]
    public int ColumnsCount
    {
      get
      {
        return this.timePickerElement.ColumnsCount;
      }
      set
      {
        if (value < 4 || value > 12)
          throw new Exception(string.Format("'{0}' is not valid. Provide value between '4' and '12'", (object) value));
        this.timePickerElement.ColumnsCount = value;
        this.OnNotifyPropertyChanged(nameof (ColumnsCount));
      }
    }

    [DefaultValue(19)]
    [Category("Appearance")]
    [Browsable(false)]
    [Description("Gets or sets headers height.")]
    public int HeadersHeight
    {
      get
      {
        return this.timePickerElement.HeadersHeight;
      }
      set
      {
        this.timePickerElement.HeadersHeight = value;
        this.OnNotifyPropertyChanged("HeaderHeight");
      }
    }

    [Description("Gets or sets button panel height.")]
    [Browsable(false)]
    [DefaultValue(35)]
    [Category("Appearance")]
    public int ButtonPanelHeight
    {
      get
      {
        return this.timePickerElement.ButtonPanelHeight;
      }
      set
      {
        this.timePickerElement.ButtonPanelHeight = value;
        this.OnNotifyPropertyChanged("DoneButtonHeight");
      }
    }

    [DefaultValue(170)]
    [Category("Appearance")]
    [Browsable(false)]
    [Description("Gets or sets the table width.")]
    public int TableWidth
    {
      get
      {
        return this.timePickerElement.TableWidth;
      }
      set
      {
        this.timePickerElement.TableWidth = value;
        this.OnNotifyPropertyChanged(nameof (TableWidth));
      }
    }

    [Category("Appearance")]
    [DefaultValue(ClockPosition.ClockBeforeTables)]
    [Description("Set the Clock position Before Time Tables or Above Time Tables")]
    public ClockPosition ClockPosition
    {
      get
      {
        return this.timePickerElement.ClockPosition;
      }
      set
      {
        this.timePickerElement.ClockPosition = value;
        this.OnNotifyPropertyChanged(nameof (ClockPosition));
      }
    }

    [Category("Appearance")]
    [DefaultValue(TimeTables.HoursAndMinutesInTwoTables)]
    [Description("Gets or sets a value which determines how to represent the times in time picker popup.")]
    public TimeTables TimeTables
    {
      get
      {
        return this.timePickerElement.TimeTables;
      }
      set
      {
        this.timePickerElement.TimeTables = value;
        this.OnNotifyPropertyChanged(nameof (TimeTables));
      }
    }

    [DefaultValue(5)]
    [Description("Gets or sets a value indicating the time interval.")]
    public int Step
    {
      get
      {
        return this.timePickerElement.Step;
      }
      set
      {
        if (value < 1 || value > 59)
          throw new Exception(string.Format("'{0}' is not valid. Provide value between '1' and '59'", (object) value));
        this.timePickerElement.Step = value;
        this.OnNotifyPropertyChanged(nameof (Step));
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the contents of the TextBox control can be changed.")]
    public bool ReadOnly
    {
      get
      {
        return this.timePickerElement.ReadOnly;
      }
      set
      {
        this.timePickerElement.ReadOnly = value;
        this.OnNotifyPropertyChanged(nameof (ReadOnly));
      }
    }

    [Category("Behavior")]
    [RadDescription("NullText", typeof (RadTextBoxItem))]
    [Localizable(true)]
    [RadDefaultValue("NullText", typeof (RadTextBoxItem))]
    [Description("Gets or sets the text that is displayed when RadDropDownList has no text set.")]
    public string NullText
    {
      get
      {
        return this.timePickerElement.NullText;
      }
      set
      {
        this.timePickerElement.NullText = value;
        this.OnNotifyPropertyChanged(nameof (NullText));
      }
    }

    [Browsable(false)]
    public RadTimePickerElement TimePickerElement
    {
      get
      {
        return this.timePickerElement;
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Browsable(true)]
    [Description("Gets or sets the time value assigned to the control.")]
    [Category("Behavior")]
    [Bindable(true)]
    public DateTime? Value
    {
      get
      {
        if (this.timePickerElement.Value == null)
          return new DateTime?();
        return new DateTime?((DateTime) this.timePickerElement.Value);
      }
      set
      {
        this.timePickerElement.Value = (object) value;
        this.OnNotifyPropertyChanged(nameof (Value));
      }
    }

    [Description("Gets or sets the Minimal time value assigned to the control.")]
    [DefaultValue(typeof (DateTime), "1900-01-01T00:00:00")]
    [Browsable(true)]
    [Category("Behavior")]
    public DateTime MinValue
    {
      get
      {
        return this.TimePickerElement.MinValue;
      }
      set
      {
        this.TimePickerElement.MinValue = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the Maximal time value assigned to the control.")]
    [Browsable(true)]
    [DefaultValue(typeof (DateTime), "1900-01-01T23:59:59")]
    public DateTime MaxValue
    {
      get
      {
        return this.TimePickerElement.MaxValue;
      }
      set
      {
        this.TimePickerElement.MaxValue = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    [Category("Action")]
    [Description(" Occurs when the editing value is changing.")]
    public event CancelEventHandler ValueChanging;

    [Category("Action")]
    [Description("Occurs when the editing value has been changed")]
    public event EventHandler ValueChanged;

    [Description("Occurs when a cell changes its state.")]
    public event TimeCellFormattingEventHandler TimeCellFormatting
    {
      add
      {
        this.Events.AddHandler(RadTimePicker.TimeCellFormattingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTimePicker.TimeCellFormattingEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the RadItem has focus and the user pressees a key down")]
    [Category("Key")]
    public new event KeyEventHandler KeyDown
    {
      add
      {
        this.Events.AddHandler(RadTimePicker.EventKeyDown, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTimePicker.EventKeyDown, (Delegate) value);
      }
    }

    [Category("Key")]
    [Description("Occurs when the RadItem has focus and the user pressees a key")]
    public new event KeyPressEventHandler KeyPress
    {
      add
      {
        this.Events.AddHandler(RadTimePicker.EventKeyPress, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTimePicker.EventKeyPress, (Delegate) value);
      }
    }

    [Description("Occurs when the RadItem has focus and the user releases the pressed key up")]
    [Category("Key")]
    public new event KeyEventHandler KeyUp
    {
      add
      {
        this.Events.AddHandler(RadTimePicker.EventKeyUp, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTimePicker.EventKeyUp, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Description("Occurs when the Multiline property has changed.")]
    [Category("Property Changed")]
    public event EventHandler MultilineChanged
    {
      add
      {
        this.Events.AddHandler(RadTimePicker.MultilineChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTimePicker.MultilineChangedEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Description("Occurs when the TextAlign property has changed.")]
    [Category("Property Changed")]
    public event EventHandler TextAlignChanged
    {
      add
      {
        this.Events.AddHandler(RadTimePicker.TextAlignChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTimePicker.TextAlignChangedEventKey, (Delegate) value);
      }
    }

    public virtual void OnTextAlignChanged(object sender, EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTimePicker.TextAlignChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler(sender, e);
    }

    public virtual void OnMultilineChanged(object sender, EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTimePicker.MultilineChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler(sender, e);
    }

    public virtual void OnKeyUp(object sender, KeyEventArgs e)
    {
      KeyEventHandler keyEventHandler = (KeyEventHandler) this.Events[RadTimePicker.EventKeyUp];
      if (keyEventHandler == null)
        return;
      keyEventHandler(sender, e);
    }

    public virtual void OnKeyPress(object sender, KeyPressEventArgs e)
    {
      KeyPressEventHandler pressEventHandler = (KeyPressEventHandler) this.Events[RadTimePicker.EventKeyPress];
      if (pressEventHandler == null)
        return;
      pressEventHandler(sender, e);
    }

    public virtual void OnKeyDown(object sender, KeyEventArgs e)
    {
      KeyEventHandler keyEventHandler = (KeyEventHandler) this.Events[RadTimePicker.EventKeyDown];
      if (keyEventHandler == null)
        return;
      keyEventHandler(sender, e);
    }

    protected virtual void OnValueChanging(object sender, CancelEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    protected virtual void OnValueChanged(object sender, EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      this.TimePickerElement.RightToLeft = this.RightToLeft == RightToLeft.Yes;
    }

    private void timePickerElement_TimeCellFormatting(object sender, TimeCellFormattingEventArgs e)
    {
      TimeCellFormattingEventHandler formattingEventHandler = (TimeCellFormattingEventHandler) this.Events[RadTimePicker.TimeCellFormattingEventKey];
      if (formattingEventHandler == null)
        return;
      formattingEventHandler(sender, e);
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
      if (!(this.Text != this.oldText))
        return;
      this.OnTextChanged(e);
      this.oldText = this.Text;
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

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (!this.Visible)
        return;
      this.TimePickerElement.MaskedEditBox.TextBoxItem.InvalidateMeasure();
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 7)
        return;
      this.timePickerElement.MaskedEditBox.TextBoxItem.HostedControl.Focus();
    }

    private bool ShouldSerializeCulture()
    {
      if (!CultureInfo.CurrentCulture.Equals((object) this.Culture))
        return !this.Culture.Equals((object) new CultureInfo(""));
      return false;
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.TimePickerElement.SuspendApplyOfThemeSettings();
      this.TimePickerElement.MaskedEditBox.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.TimePickerElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.TimePickerElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.TimePickerElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TimePickerFill");
        this.TimePickerElement.MaskedEditBox.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.TimePickerElement.MaskedEditBox.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TextBoxFill");
      }
      this.TimePickerElement.ResumeApplyOfThemeSettings();
      this.TimePickerElement.MaskedEditBox.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.TimePickerElement.SuspendApplyOfThemeSettings();
      this.TimePickerElement.MaskedEditBox.SuspendApplyOfThemeSettings();
      this.TimePickerElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.TimePickerElement.MaskedEditBox.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.TimePickerElement.ElementTree.ApplyThemeToElementTree();
      this.TimePickerElement.ResumeApplyOfThemeSettings();
      this.TimePickerElement.MaskedEditBox.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.TimePickerElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.TimePickerElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.TimePickerElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.TimePickerElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.TimePickerElement.SuspendApplyOfThemeSettings();
      this.TimePickerElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.TimePickerElement.ElementTree.ApplyThemeToElementTree();
      this.TimePickerElement.ResumeApplyOfThemeSettings();
    }
  }
}
