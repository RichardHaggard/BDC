// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDateTimePicker
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

namespace Telerik.WinControls.UI
{
  [DefaultProperty("Value")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Editors")]
  [ToolboxItem(true)]
  [Description("Enables the user to select a date and time, and display the date and time in a specified format")]
  [DefaultBindingProperty("Value")]
  [DefaultEvent("ValueChanged")]
  public class RadDateTimePicker : RadEditorControl
  {
    private static readonly object EventKeyDown = new object();
    private static readonly object EventKeyPress = new object();
    private static readonly object EventKeyUp = new object();
    private RadDateTimePickerElement dateTimePickerElement;
    private bool entering;

    public RadDateTimePicker()
    {
      this.AutoSize = true;
      this.TabStop = false;
      this.SetStyle(ControlStyles.Selectable, true);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.dateTimePickerElement = this.CreateElement();
      parent.Children.Add((RadElement) this.dateTimePickerElement);
      this.dateTimePickerElement.FormatChanged += new EventHandler(this.dateTimePickerElement_FormatChanged);
      this.dateTimePickerElement.ValueChanged += new EventHandler(this.dateTimePickerElement_ValueChanged);
      this.dateTimePickerElement.ValueChanging += new ValueChangingEventHandler(this.dateTimePickerElement_ValueChanging);
      this.dateTimePickerElement.NullableValueChanged += new EventHandler(this.dateTimePickerElement_NullableValueChanged);
      this.dateTimePickerElement.Opening += new CancelEventHandler(this.dateTimePickerElement_Opening);
      this.dateTimePickerElement.Opened += new EventHandler(this.dateTimePickerElement_Opened);
      this.dateTimePickerElement.Closing += new RadPopupClosingEventHandler(this.dateTimePickerElement_Closing);
      this.dateTimePickerElement.Closed += new RadPopupClosedEventHandler(this.dateTimePickerElement_Closed);
      this.dateTimePickerElement.CheckedChanged += new EventHandler(this.DateTimePickerElement_CheckedChanged);
      this.dateTimePickerElement.TextBoxElement.KeyDown += new KeyEventHandler(this.OnKeyDown);
      this.dateTimePickerElement.TextBoxElement.KeyUp += new KeyEventHandler(this.OnKeyUp);
      this.dateTimePickerElement.TextBoxElement.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
      this.dateTimePickerElement.ToggleStateChanging += new StateChangingEventHandler(this.CheckBox_ToggleStateChanging);
      this.dateTimePickerElement.ToggleStateChanged += new StateChangedEventHandler(this.CheckBox_ToggleStateChanged);
    }

    protected virtual RadDateTimePickerElement CreateElement()
    {
      return new RadDateTimePickerElement();
    }

    private void DateTimePickerElement_CheckedChanged(object sender, EventArgs e)
    {
      this.OnCheckedChanged(e);
    }

    private void dateTimePickerElement_Closed(object sender, RadPopupClosedEventArgs args)
    {
      this.OnClosed(args);
    }

    private void dateTimePickerElement_Closing(object sender, RadPopupClosingEventArgs args)
    {
      this.OnClosing(args);
    }

    private void dateTimePickerElement_Opened(object sender, EventArgs e)
    {
      this.OnOpened(e);
    }

    private void dateTimePickerElement_Opening(object sender, CancelEventArgs e)
    {
      this.OnOpening(e);
    }

    private void dateTimePickerElement_NullableValueChanged(object sender, EventArgs e)
    {
      this.OnNullableValueChanged(e);
    }

    private void dateTimePickerElement_ValueChanging(object sender, ValueChangingEventArgs e)
    {
      this.OnValueChanging(e);
    }

    private void dateTimePickerElement_ValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged(e);
    }

    private void dateTimePickerElement_FormatChanged(object sender, EventArgs e)
    {
      this.OnFormatChanged(e);
    }

    private void CheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      this.OnToggleStateChanged(args);
    }

    private void CheckBox_ToggleStateChanging(object sender, StateChangingEventArgs args)
    {
      this.OnToggleStateChanging(args);
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadDateTimePickerAccessibleObject(this);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.dateTimePickerElement != null)
      {
        this.dateTimePickerElement.FormatChanged -= new EventHandler(this.dateTimePickerElement_FormatChanged);
        this.dateTimePickerElement.ValueChanged -= new EventHandler(this.dateTimePickerElement_ValueChanged);
        this.dateTimePickerElement.NullableValueChanged -= new EventHandler(this.dateTimePickerElement_NullableValueChanged);
        this.dateTimePickerElement.ValueChanging -= new ValueChangingEventHandler(this.dateTimePickerElement_ValueChanging);
        this.dateTimePickerElement.Opening -= new CancelEventHandler(this.dateTimePickerElement_Opening);
        this.dateTimePickerElement.Opened -= new EventHandler(this.dateTimePickerElement_Opened);
        this.dateTimePickerElement.Closing -= new RadPopupClosingEventHandler(this.dateTimePickerElement_Closing);
        this.dateTimePickerElement.Closed -= new RadPopupClosedEventHandler(this.dateTimePickerElement_Closed);
        this.dateTimePickerElement.CheckedChanged -= new EventHandler(this.DateTimePickerElement_CheckedChanged);
        this.dateTimePickerElement.TextBoxElement.TextBoxItem.HostedControl.KeyDown -= new KeyEventHandler(this.OnKeyDown);
        this.dateTimePickerElement.TextBoxElement.TextBoxItem.HostedControl.KeyUp -= new KeyEventHandler(this.OnKeyUp);
        this.dateTimePickerElement.TextBoxElement.TextBoxItem.HostedControl.KeyPress -= new KeyPressEventHandler(this.OnKeyPress);
        this.dateTimePickerElement.ToggleStateChanging -= new StateChangingEventHandler(this.CheckBox_ToggleStateChanging);
        this.dateTimePickerElement.ToggleStateChanged -= new StateChangedEventHandler(this.CheckBox_ToggleStateChanged);
        this.dateTimePickerElement.Dispose();
        this.dateTimePickerElement.DisposeChildren();
        this.dateTimePickerElement = (RadDateTimePickerElement) null;
      }
      base.Dispose(disposing);
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
        this.DateTimePickerElement.TextBoxElement.TextBoxItem.HostedControl.Name = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(164, 20));
      }
    }

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
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

    [Bindable(true)]
    [Localizable(true)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the text associated with this control.")]
    [SettingsBindable(true)]
    [DefaultValue("")]
    [Editor("Telerik.WinControls.UI.Design.SimpleTextUITypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public override string Text
    {
      get
      {
        return this.dateTimePickerElement.TextBoxElement.Text;
      }
      set
      {
        base.Text = value;
        this.dateTimePickerElement.TextBoxElement.Text = value;
      }
    }

    [Description("Gets or sets the culture supported by this calendar.")]
    [Category("Localization Settings")]
    [Localizable(true)]
    [NotifyParentProperty(true)]
    [TypeConverter(typeof (CultureInfoConverter))]
    [RefreshProperties(RefreshProperties.Repaint)]
    public CultureInfo Culture
    {
      get
      {
        return this.dateTimePickerElement.Culture;
      }
      set
      {
        this.dateTimePickerElement.Culture = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual RadDateTimePickerElement DateTimePickerElement
    {
      get
      {
        return this.dateTimePickerElement;
      }
      set
      {
        this.dateTimePickerElement = value;
      }
    }

    [Category("Behavior")]
    [Bindable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or sets the date/time value assigned to the control.")]
    public virtual DateTime Value
    {
      get
      {
        DateTime? nullable1 = this.dateTimePickerElement.Value;
        if (!nullable1.HasValue)
          return this.NullDate;
        DateTime? nullable2 = nullable1;
        DateTime minDate = this.MinDate;
        if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() < minDate ? 1 : 0) : 0) != 0)
          return this.MinDate;
        DateTime? nullable3 = nullable1;
        DateTime maxDate = this.MaxDate;
        if ((nullable3.HasValue ? (nullable3.GetValueOrDefault() > maxDate ? 1 : 0) : 0) != 0)
          return this.MaxDate;
        return nullable1.Value;
      }
      set
      {
        this.dateTimePickerElement.Value = new DateTime?(value);
        this.OnNotifyPropertyChanged(nameof (Value));
        this.NullableValue = new DateTime?(value);
        this.OnNotifyPropertyChanged("NullableValue");
      }
    }

    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or sets the date/time value assigned to the control.")]
    [Category("Behavior")]
    [Browsable(false)]
    public virtual DateTime? NullableValue
    {
      get
      {
        if (this.DateTimePickerElement.Value.Equals((object) this.DateTimePickerElement.NullDate))
          return new DateTime?();
        return this.dateTimePickerElement.Value;
      }
      set
      {
        this.dateTimePickerElement.Value = value;
        this.OnNotifyPropertyChanged(nameof (NullableValue));
        this.OnNotifyPropertyChanged("Value");
      }
    }

    [DefaultValue(DateTimePickerFormat.Long)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the format of the date and time displayed in the control.")]
    [Category("Appearance")]
    public virtual DateTimePickerFormat Format
    {
      get
      {
        return this.dateTimePickerElement.Format;
      }
      set
      {
        this.dateTimePickerElement.Format = value;
        this.OnNotifyPropertyChanged(nameof (Format));
      }
    }

    [DefaultValue(false)]
    [Description("Indicates whether a check box is displayed in the control. When the check box is unchecked no value is selected")]
    [Category("Appearance")]
    public bool ShowCheckBox
    {
      get
      {
        return this.dateTimePickerElement.ShowCheckBox;
      }
      set
      {
        this.dateTimePickerElement.ShowCheckBox = value;
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    [Description("Gets or sets the custom date/time format string.")]
    [Localizable(true)]
    [Category("Behavior")]
    public string CustomFormat
    {
      get
      {
        return this.dateTimePickerElement.CustomFormat;
      }
      set
      {
        this.dateTimePickerElement.CustomFormat = value;
      }
    }

    [Description("When ShowCheckBox is true, determines that the user has selected a value")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool Checked
    {
      get
      {
        return this.dateTimePickerElement.Checked;
      }
      set
      {
        if (this.dateTimePickerElement.Checked == value)
          return;
        this.dateTimePickerElement.Checked = value;
      }
    }

    [Description("Gets or sets the minimum date and time that can be selected in the control.")]
    [DefaultValue(typeof (DateTime), "")]
    [Category("Behavior")]
    public DateTime MinDate
    {
      get
      {
        return this.dateTimePickerElement.MinDate;
      }
      set
      {
        this.dateTimePickerElement.MinDate = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeMinDate()
    {
      return this.MinDate != this.Culture.Calendar.MinSupportedDateTime;
    }

    [Description("Gets or sets the maximum date and time that can be selected in the control.")]
    [Category("Behavior")]
    [DefaultValue(typeof (DateTime), "12/31/9998")]
    public DateTime MaxDate
    {
      get
      {
        return this.dateTimePickerElement.MaxDate;
      }
      set
      {
        this.dateTimePickerElement.MaxDate = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeMaxDate()
    {
      return this.MaxDate != this.Culture.Calendar.MaxSupportedDateTime;
    }

    [Category("Behavior")]
    [DefaultValue(typeof (Point), "0, 0")]
    [Browsable(false)]
    [Description("Gets or sets the location of the drop down showing the calendar")]
    public Point CalendarLocation
    {
      get
      {
        return this.dateTimePickerElement.CalendarLocation;
      }
      set
      {
        this.dateTimePickerElement.CalendarLocation = value;
      }
    }

    [Description("Gets or sets the size of the calendar in the drop down")]
    [DefaultValue(typeof (Size), "100, 156")]
    [Browsable(false)]
    [Category("Behavior")]
    public Size CalendarSize
    {
      get
      {
        return this.dateTimePickerElement.CalendarSize;
      }
      set
      {
        this.dateTimePickerElement.CalendarSize = value;
      }
    }

    [DefaultValue(typeof (DateTime), "")]
    [Description("The DateTime value assigned to the date picker when the Value is null")]
    [Bindable(false)]
    [Category("Data")]
    public DateTime NullDate
    {
      get
      {
        return this.dateTimePickerElement.NullDate;
      }
      set
      {
        this.dateTimePickerElement.NullDate = value;
      }
    }

    public void SetToNullValue()
    {
      this.dateTimePickerElement.SetToNullValue();
    }

    [Category("Appearance")]
    [Description("Indicates whether a spin box rather than a drop down calendar is displayed for editing the control's value")]
    [DefaultValue(false)]
    public bool ShowUpDown
    {
      get
      {
        return this.dateTimePickerElement.ShowUpDown;
      }
      set
      {
        this.dateTimePickerElement.ShowUpDown = value;
      }
    }

    [Category("Behavior")]
    [Localizable(true)]
    [Description("Gets or sets the text that is displayed when the DateTimePicker contains a null reference")]
    public string NullText
    {
      get
      {
        return this.dateTimePickerElement.NullText;
      }
      set
      {
        this.dateTimePickerElement.NullText = value;
      }
    }

    [Description("Gets or sets a value indicating whether RadDateTimePicker is read-only.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
      get
      {
        return this.DateTimePickerElement.ReadOnly;
      }
      set
      {
        this.DateTimePickerElement.ReadOnly = value;
      }
    }

    [Browsable(false)]
    [Description("Gets the maximum date value allowed for the DateTimePicker control.")]
    public static DateTime MaximumDateTime
    {
      get
      {
        return RadDateTimePickerElement.MaxDateTime;
      }
      set
      {
        RadDateTimePickerElement.MaxDateTime = value;
      }
    }

    [Category("Action")]
    [Description("Occurs when MaskProvider has been created This event will be fired multiple times because the provider is created when some properties changed Properties are: Mask, Culture, MaskType and more.")]
    public event EventHandler MaskProviderCreated
    {
      add
      {
        this.DateTimePickerElement.MaskProviderCreated += value;
      }
      remove
      {
        this.DateTimePickerElement.MaskProviderCreated -= value;
      }
    }

    [Description("Occurs when the value of the control has changed")]
    [Category("Action")]
    public event EventHandler FormatChanged;

    [Category("Action")]
    [Description("Occurs when the value of the control has changed")]
    public event EventHandler ValueChanged;

    [Description("Occurs when the value of the control is changing")]
    [Category("Action")]
    public event ValueChangingEventHandler ValueChanging;

    [Category("Action")]
    [Description("Occurs when the NullableValue of the control is changing")]
    public event EventHandler NullableValueChanged;

    [Description("Occurs when the drop down is opened")]
    [Category("Action")]
    public event EventHandler Opened;

    [Category("Action")]
    [Description("Occurs when the drop down is opening")]
    public event CancelEventHandler Opening;

    [Category("Action")]
    [Description("Occurs when the drop down is closing")]
    public event RadPopupClosingEventHandler Closing;

    [Description("Occurs when the drop down is closed")]
    [Category("Action")]
    public event RadPopupClosedEventHandler Closed;

    [Category("Key")]
    [Description("Occurs when the RadItem has focus and the user presses a key down")]
    public new event KeyEventHandler KeyDown
    {
      add
      {
        this.Events.AddHandler(RadDateTimePicker.EventKeyDown, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDateTimePicker.EventKeyDown, (Delegate) value);
      }
    }

    [Category("Key")]
    [Description("Occurs when the RadItem has focus and the user presses a key")]
    public new event KeyPressEventHandler KeyPress
    {
      add
      {
        this.Events.AddHandler(RadDateTimePicker.EventKeyPress, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDateTimePicker.EventKeyPress, (Delegate) value);
      }
    }

    [Category("Key")]
    [Description("Occurs when the RadItem has focus and the user releases the pressed key up")]
    public new event KeyEventHandler KeyUp
    {
      add
      {
        this.Events.AddHandler(RadDateTimePicker.EventKeyUp, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDateTimePicker.EventKeyUp, (Delegate) value);
      }
    }

    public event StateChangingEventHandler ToggleStateChanging;

    public event StateChangedEventHandler ToggleStateChanged;

    [Category("Action")]
    [Description("Occurs when the value of the checkbox in the editor is changed")]
    public event EventHandler CheckedChanged;

    public void ResetCulture()
    {
      this.dateTimePickerElement.Culture = new CultureInfo("en-US");
    }

    public void ResetNullText()
    {
      this.dateTimePickerElement.NullText = "";
    }

    public override void EndInit()
    {
      base.EndInit();
      this.dateTimePickerElement.TextBoxElement.OnMaskProviderCreated();
    }

    public virtual void OnKeyUp(object sender, KeyEventArgs e)
    {
      KeyEventHandler keyEventHandler = (KeyEventHandler) this.Events[RadDateTimePicker.EventKeyUp];
      if (keyEventHandler == null)
        return;
      keyEventHandler((object) this, e);
    }

    public virtual void OnKeyPress(object sender, KeyPressEventArgs e)
    {
      KeyPressEventHandler pressEventHandler = (KeyPressEventHandler) this.Events[RadDateTimePicker.EventKeyPress];
      if (pressEventHandler == null)
        return;
      pressEventHandler((object) this, e);
    }

    public virtual void OnKeyDown(object sender, KeyEventArgs e)
    {
      KeyEventHandler keyEventHandler = (KeyEventHandler) this.Events[RadDateTimePicker.EventKeyDown];
      if (keyEventHandler == null)
        return;
      keyEventHandler((object) this, e);
    }

    protected virtual void OnFormatChanged(EventArgs e)
    {
      if (this.FormatChanged == null)
        return;
      this.FormatChanged((object) this, e);
    }

    protected virtual void OnValueChanged(EventArgs e)
    {
      if (this.ValueChanged != null)
        this.ValueChanged((object) this, e);
      this.OnNotifyPropertyChanged("Value");
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "ValueChanged", (object) this.Text);
    }

    protected virtual void OnNullableValueChanged(EventArgs e)
    {
      if (this.NullableValueChanged != null)
        this.NullableValueChanged((object) this, e);
      this.OnNotifyPropertyChanged("NullableValue");
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    protected virtual void OnClosed(RadPopupClosedEventArgs args)
    {
      if (this.Closed == null)
        return;
      this.Closed((object) this, args);
    }

    protected virtual void OnOpened(EventArgs args)
    {
      if (this.Opened == null)
        return;
      this.Opened((object) this, args);
    }

    protected virtual void OnOpening(CancelEventArgs args)
    {
      if (this.Opening == null)
        return;
      this.Opening((object) this, args);
    }

    protected virtual void OnClosing(RadPopupClosingEventArgs args)
    {
      if (this.Closing == null)
        return;
      this.Closing((object) this, args);
    }

    protected virtual void OnCheckedChanged(EventArgs e)
    {
      if (this.CheckedChanged == null)
        return;
      this.CheckedChanged((object) this, e);
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      RadDateTimePickerCalendar currentBehavior = this.dateTimePickerElement.GetCurrentBehavior() as RadDateTimePickerCalendar;
      if (currentBehavior == null)
        return;
      currentBehavior.Calendar.ThemeName = this.ThemeName;
      currentBehavior.PopupControl.ThemeName = this.ThemeName;
      if (currentBehavior.ShowTimePicker && currentBehavior.TimePicker != null)
      {
        currentBehavior.TimePicker.ThemeName = this.ThemeName;
        if (currentBehavior.ShowFooter)
          currentBehavior.FooterPanel.ThemeName = this.ThemeName;
      }
      if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
        return;
      Size size = TelerikDpiHelper.ScaleSize(new Size(300, 390), this.RootElement.DpiScaleFactor);
      if (this.dateTimePickerElement.ShowTimePicker)
      {
        currentBehavior.Calendar.MinimumSize = TelerikDpiHelper.ScaleSize(new Size(300, 0), this.RootElement.DpiScaleFactor);
        size.Width = 590;
      }
      currentBehavior.DropDownMinSize = size;
      RadCalendarElement calendarElement = currentBehavior.Calendar.CalendarElement;
      calendarElement.DrawBorder = true;
      calendarElement.BorderBoxStyle = BorderBoxStyle.FourBorders;
      calendarElement.BorderGradientStyle = GradientStyles.Solid;
      calendarElement.BorderBottomColor = calendarElement.BorderBottomShadowColor = calendarElement.BorderRightColor = calendarElement.BorderRightShadowColor = calendarElement.BorderLeftShadowColor = calendarElement.BorderTopShadowColor = Color.Transparent;
      calendarElement.BorderLeftColor = calendarElement.BorderTopColor = Color.FromArgb(236, 236, 236);
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      RightToLeft rightToLeft = this.RightToLeft;
      if (rightToLeft == RightToLeft.Inherit)
      {
        Control parent = this.Parent;
        while (parent != null && parent.RightToLeft == RightToLeft.Inherit)
          parent = parent.Parent;
        rightToLeft = parent != null ? parent.RightToLeft : RightToLeft.No;
        if (rightToLeft == RightToLeft.Yes)
          this.dateTimePickerElement.RightToLeft = true;
        else
          this.dateTimePickerElement.RightToLeft = false;
      }
      if (rightToLeft == RightToLeft.No)
        this.dateTimePickerElement.RightToLeft = false;
      else if (rightToLeft == RightToLeft.Yes)
        this.dateTimePickerElement.RightToLeft = true;
      base.OnRightToLeftChanged(e);
    }

    protected override void OnEnter(EventArgs e)
    {
      base.OnEnter(e);
      if (this.entering)
        return;
      this.entering = true;
      this.dateTimePickerElement.TextBoxElement.TextBoxItem.TextBoxControl.Focus();
      this.OnGotFocus(e);
      this.entering = false;
    }

    protected override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      this.OnLostFocus(e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      if (this.entering)
        return;
      base.OnLostFocus(e);
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

    private void OnToggleStateChanged(StateChangedEventArgs args)
    {
      if (this.ToggleStateChanged == null)
        return;
      this.ToggleStateChanged((object) this, args);
    }

    private void OnToggleStateChanging(StateChangingEventArgs args)
    {
      if (this.ToggleStateChanging == null)
        return;
      this.ToggleStateChanging((object) this, args);
    }

    protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
      this.DateTimePickerElement.HandleArrowsInpit(e);
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      if (!this.Enabled || !this.ShowCheckBox || this.DateTimePickerElement.Checked)
        return;
      this.DateTimePickerElement.TextBoxElement.TextBoxItem.HostedControl.Enabled = false;
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 7)
        return;
      this.entering = true;
      this.dateTimePickerElement.TextBoxElement.TextBoxItem.HostedControl.Focus();
      this.dateTimePickerElement.TextBoxElement.TextBoxItem.SelectAll();
      this.entering = false;
    }

    private bool ShouldSerializeNullText()
    {
      return !string.IsNullOrEmpty(this.dateTimePickerElement.NullText);
    }

    private bool ShouldSerializeCulture()
    {
      return !CultureInfo.CurrentCulture.Equals((object) this.Culture);
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (element.GetType().Equals(typeof (RadMaskedEditBoxElement)))
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.DateTimePickerElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates1 = this.DateTimePickerElement.GetAvailableVisualStates();
      availableVisualStates1.Add("");
      foreach (string state in availableVisualStates1)
      {
        this.DateTimePickerElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.DateTimePickerElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "DateTimePickerBackGround");
      }
      this.DateTimePickerElement.TextBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates2 = this.DateTimePickerElement.TextBoxElement.GetAvailableVisualStates();
      availableVisualStates2.Add("");
      foreach (string state in availableVisualStates2)
      {
        this.DateTimePickerElement.TextBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.DateTimePickerElement.TextBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TextBoxFill");
      }
      this.DateTimePickerElement.ResumeApplyOfThemeSettings();
      this.DateTimePickerElement.TextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.DateTimePickerElement.SuspendApplyOfThemeSettings();
      this.DateTimePickerElement.TextBoxElement.SuspendApplyOfThemeSettings();
      this.DateTimePickerElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.DateTimePickerElement.TextBoxElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num1 = (int) this.DateTimePickerElement.Children[0].ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      int num2 = (int) this.DateTimePickerElement.TextBoxElement.Fill.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.DateTimePickerElement.ElementTree.ApplyThemeToElementTree();
      this.DateTimePickerElement.ResumeApplyOfThemeSettings();
      this.DateTimePickerElement.TextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.DateTimePickerElement.SuspendApplyOfThemeSettings();
      this.DateTimePickerElement.TextBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.DateTimePickerElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.DateTimePickerElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.DateTimePickerElement.TextBoxElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      }
      this.DateTimePickerElement.ResumeApplyOfThemeSettings();
      this.DateTimePickerElement.TextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.DateTimePickerElement.SuspendApplyOfThemeSettings();
      this.DateTimePickerElement.TextBoxElement.SuspendApplyOfThemeSettings();
      this.DateTimePickerElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.DateTimePickerElement.TextBoxElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.DateTimePickerElement.ElementTree.ApplyThemeToElementTree();
      this.DateTimePickerElement.ResumeApplyOfThemeSettings();
      this.DateTimePickerElement.TextBoxElement.ResumeApplyOfThemeSettings();
    }
  }
}
