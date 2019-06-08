// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDateTimePickerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadDateTimePickerElement : RadEditorElement
  {
    private string nullText = string.Empty;
    private DateTimePickerFormat format = DateTimePickerFormat.Long;
    public static RadProperty IsDropDownShownProperty = RadProperty.Register(nameof (IsDropDownShown), typeof (bool), typeof (RadDateTimePickerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty CalendarSizeProperty = RadProperty.Register(nameof (CalendarSize), typeof (Size), typeof (RadDateTimePickerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(100, 156), ElementPropertyOptions.None));
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("Gets the minimum date value allowed for the DateTimePicker control.")]
    [Browsable(false)]
    public static DateTime MinDateTime = new DateTime(1753, 1, 1);
    [Description("Gets the maximum date value allowed for the DateTimePicker control.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static DateTime MaxDateTime = new DateTime(9998, 12, 31);
    internal const long ShowCheckBoxStateKey = 8796093022208;
    internal const long ShowUpDownStateKey = 17592186044416;
    internal const long CheckStateStateKey = 35184372088832;
    internal const long ValidTimeStateKey = 70368744177664;
    internal const long UserHasSetValueStateKey = 140737488355328;
    internal const long ShowCurrentTimeStateKey = 281474976710656;
    private DateTime creationTime;
    private string customFormat;
    private DateTime min;
    private DateTime max;
    private RadDateTimePickerBehaviorDirector defaultDirector;
    private CultureInfo cultureInfo;
    private RadDateTimePickerBehaviorDirector currentBehavior;
    private Point calendarLocation;
    private bool internalValueSet;
    private DateTime? _value;
    private RadCheckBoxElement checkBox;
    private DateTime _nullDate;
    private NullValueCheckMode includeTimeInNullDateCheck;

    public RadDateTimePickerElement()
    {
    }

    public RadDateTimePickerElement(RadDateTimePickerBehaviorDirector behaviorDirector)
    {
      this.defaultDirector = behaviorDirector;
    }

    protected override void DisposeManagedResources()
    {
      if (this.currentBehavior is RadDateTimePickerCalendar)
      {
        RadDateTimePickerCalendar currentBehavior = this.currentBehavior as RadDateTimePickerCalendar;
        if (currentBehavior.PopupControl != null)
        {
          currentBehavior.PopupControl.Opened -= new EventHandler(this.PopupControl_Opened);
          currentBehavior.PopupControl.Opening -= new CancelEventHandler(this.PopupControl_Opening);
          currentBehavior.PopupControl.Closing -= new RadPopupClosingEventHandler(this.PopupControl_Closing);
          currentBehavior.PopupControl.Closed -= new RadPopupClosedEventHandler(this.PopupControl_Closed);
          currentBehavior.Calendar.ClearButton.Click -= new EventHandler(this.ClearButton_Click);
        }
        RadTextBoxElement textBoxElement = (RadTextBoxElement) this.TextBoxElement;
        if (textBoxElement != null)
        {
          RadTextBoxItem textBoxItem = textBoxElement.TextBoxItem;
          if (textBoxItem != null && textBoxItem.HostedControl != null)
            this.currentBehavior.TextBoxElement.ValueChanged += new EventHandler(this.RadDateTimePickerElement_ValueChanged);
        }
      }
      if (this.checkBox != null)
        this.checkBox.ToggleStateChanged -= new StateChangedEventHandler(this.checkBox_ToggleStateChanged);
      IDisposable defaultDirector = this.defaultDirector as IDisposable;
      IDisposable disposable;
      if (defaultDirector != null)
      {
        defaultDirector.Dispose();
        disposable = (IDisposable) null;
      }
      IDisposable currentBehavior1 = this.currentBehavior as IDisposable;
      if (currentBehavior1 != null)
      {
        currentBehavior1.Dispose();
        disposable = (IDisposable) null;
      }
      this.currentBehavior = (RadDateTimePickerBehaviorDirector) null;
      this.defaultDirector = (RadDateTimePickerBehaviorDirector) null;
      base.DisposeManagedResources();
    }

    protected override void CreateChildElements()
    {
      this.SetBehavior(this.CreateDefaultBehavior());
      this._value = new DateTime?(DateTime.Now);
      this.creationTime = DateTime.Now;
      this.GetCurrentBehavior().SetDateByValue(this._value, this.format);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BitState[70368744177664L] = true;
      this._value = new DateTime?(DateTime.Now);
      this.creationTime = DateTime.Now;
      this.max = DateTime.MaxValue;
      this.min = DateTime.MinValue;
      this.format = DateTimePickerFormat.Long;
      this._nullDate = this.DefaultNullDate;
      this.CanFocus = false;
    }

    protected virtual RadDateTimePickerBehaviorDirector CreateDefaultBehavior()
    {
      return (RadDateTimePickerBehaviorDirector) new RadDateTimePickerCalendar(this);
    }

    [Browsable(false)]
    public RadMaskedEditBoxElement TextBoxElement
    {
      get
      {
        return this.CurrentBehavior.TextBoxElement;
      }
    }

    public RadArrowButtonElement ArrowButton
    {
      get
      {
        if (this.GetCurrentBehavior() is RadDateTimePickerCalendar)
          return (this.GetCurrentBehavior() as RadDateTimePickerCalendar).ArrowButton;
        return (RadArrowButtonElement) null;
      }
    }

    public RadCheckBoxElement CheckBox
    {
      get
      {
        return this.checkBox;
      }
      internal set
      {
        this.checkBox = value;
      }
    }

    public RadDateTimePickerBehaviorDirector CurrentBehavior
    {
      get
      {
        return this.currentBehavior;
      }
      set
      {
        this.currentBehavior = value;
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether RadDateTimePicker is read-only.")]
    public bool ReadOnly
    {
      get
      {
        return this.CurrentBehavior.TextBoxElement.TextBoxItem.ReadOnly;
      }
      set
      {
        this.CurrentBehavior.TextBoxElement.TextBoxItem.ReadOnly = value;
        RadDateTimePickerCalendar currentBehavior = this.CurrentBehavior as RadDateTimePickerCalendar;
        if (currentBehavior != null)
        {
          currentBehavior.Calendar.ReadOnly = value;
          if (currentBehavior.ShowTimePicker)
            currentBehavior.TimePicker.ReadOnly = value;
        }
        this.checkBox.ReadOnly = value;
      }
    }

    [Category("Appearance")]
    [Description("Indicates whether a spin box rather than a drop down calendar is displayed for editing the control's value")]
    [DefaultValue(false)]
    public bool ShowUpDown
    {
      get
      {
        return this.GetBitState(17592186044416L);
      }
      set
      {
        this.SetBitState(17592186044416L, value);
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [TypeConverter(typeof (CultureInfoConverter))]
    [NotifyParentProperty(true)]
    [Description("Gets or sets the culture supported by this calendar.")]
    [Localizable(true)]
    [Category("Localization Settings")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CultureInfo Culture
    {
      get
      {
        if (this.cultureInfo != null)
          return this.cultureInfo;
        return CultureInfo.CurrentCulture;
      }
      set
      {
        if (this.cultureInfo == value)
          return;
        this.cultureInfo = value;
        this.OnNotifyPropertyChanged(nameof (Culture));
      }
    }

    protected virtual DateTime DefaultNullDate
    {
      get
      {
        return DateTime.MinValue;
      }
    }

    [Bindable(false)]
    [Category("Data")]
    [Description("The DateTime value assigned to the date picker when the Value is null")]
    public DateTime NullDate
    {
      get
      {
        if (this._nullDate != this.DefaultNullDate)
          return this._nullDate;
        return this.DefaultNullDate;
      }
      set
      {
        if (DateTime.Equals(this._nullDate.Date, value.Date))
          return;
        this._nullDate = value;
        if (this.CurrentBehavior.DateTimePickerElement.Value.HasValue && this.CurrentBehavior.DateTimePickerElement.Value.Value.Date.Equals(value.Date))
        {
          this.CurrentBehavior.DateTimePickerElement.TextBoxElement.TextBoxItem.Text = string.Empty;
        }
        else
        {
          if (!this.Value.HasValue)
            this.GetCurrentBehavior().SetDateByValue(new DateTime?(DateTime.Now), this.format);
          else
            this.GetCurrentBehavior().SetDateByValue(new DateTime?(this.Value.Value.AddDays(1.0)), this.format);
          this.GetCurrentBehavior().SetDateByValue(this.Value, this.format);
        }
      }
    }

    public bool IsDropDownShown
    {
      get
      {
        return (bool) this.GetValue(RadDateTimePickerElement.IsDropDownShownProperty);
      }
      internal set
      {
        int num = (int) this.SetValue(RadDateTimePickerElement.IsDropDownShownProperty, (object) value);
      }
    }

    [DefaultValue(false)]
    [Description("When ShowCheckBox is true, determines that the user has selected a value")]
    [Category("Behavior")]
    public bool Checked
    {
      get
      {
        return this.GetBitState(35184372088832L);
      }
      set
      {
        this.SetBitState(35184372088832L, value);
        this.OnNotifyPropertyChanged(nameof (Checked));
        this.OnCheckedChanged(EventArgs.Empty);
      }
    }

    [Localizable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the custom date/time format string.")]
    [DefaultValue(null)]
    [Category("Behavior")]
    public string CustomFormat
    {
      get
      {
        return this.customFormat;
      }
      set
      {
        if ((value == null || value.Equals(this.customFormat)) && (value != null || this.customFormat == null))
          return;
        this.customFormat = value;
        this.OnNotifyPropertyChanged(nameof (CustomFormat));
      }
    }

    [Description("Gets or sets the format of the date and time displayed in the control.")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(typeof (DateTimePickerFormat), "DateTimePickerFormat.Long")]
    public DateTimePickerFormat Format
    {
      get
      {
        return this.format;
      }
      set
      {
        if (!Telerik.WinControls.ClientUtils.IsEnumValid((Enum) value, (int) value, 1, 8, 1))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (DateTimePickerFormat));
        if (this.format == value)
          return;
        this.format = value;
        this.OnFormatChanged(EventArgs.Empty);
        this.OnNotifyPropertyChanged(nameof (Format));
      }
    }

    [Browsable(false)]
    [Category("Behavior")]
    [DefaultValue(typeof (Point), "0, 0")]
    [Description("Gets or sets the location of the drop down showing the calendar")]
    public Point CalendarLocation
    {
      get
      {
        return this.calendarLocation;
      }
      set
      {
        if (!(this.calendarLocation != value))
          return;
        this.calendarLocation = value;
        this.OnNotifyPropertyChanged(nameof (CalendarLocation));
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the size of the calendar in the drop down")]
    [Category("Behavior")]
    [DefaultValue(typeof (Size), "100, 156")]
    public Size CalendarSize
    {
      get
      {
        return (Size) this.GetValue(RadDateTimePickerElement.CalendarSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDateTimePickerElement.CalendarSizeProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Indicates whether a check box is displayed in the control. When the check box is unchecked no value is selected")]
    [DefaultValue(false)]
    public bool ShowCheckBox
    {
      get
      {
        return this.GetBitState(8796093022208L);
      }
      set
      {
        this.SetBitState(8796093022208L, value);
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Bindable(true)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether the current time is shown")]
    public bool ShowCurrentTime
    {
      get
      {
        return this.GetBitState(281474976710656L);
      }
      set
      {
        this.SetBitState(281474976710656L, value);
      }
    }

    [DefaultValue(NullValueCheckMode.Date)]
    public virtual NullValueCheckMode NullValueCheckMode
    {
      get
      {
        return this.includeTimeInNullDateCheck;
      }
      set
      {
        this.includeTimeInNullDateCheck = value;
      }
    }

    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.All)]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the date/time value assigned to the control.")]
    public DateTime? Value
    {
      get
      {
        if (!this.GetBitState(140737488355328L) && this.GetBitState(70368744177664L))
          return new DateTime?(this.creationTime);
        return this._value;
      }
      set
      {
        if (this.internalValueSet)
          return;
        bool flag = !object.Equals((object) this.Value, (object) value);
        if (this.CheckIsEqualToNullDate(value) || !value.HasValue)
        {
          this.internalValueSet = true;
          this.CurrentBehavior.DateTimePickerElement.TextBoxElement.Value = (object) null;
          this.CurrentBehavior.DateTimePickerElement.Text = string.Empty;
          this.internalValueSet = false;
          this.CurrentBehavior.DateTimePickerElement.TextBoxElement.TextBoxItem.Text = string.Empty;
          this._value = new DateTime?();
          this.SetBitState(140737488355328L, true);
          this.OnValueChanged(EventArgs.Empty);
          this.OnNullableValueChanged(EventArgs.Empty);
        }
        else
        {
          if (!flag)
          {
            DateTime? nullable = value;
            DateTime nullDate = this.NullDate;
            if ((!nullable.HasValue ? 0 : (nullable.GetValueOrDefault() == nullDate ? 1 : 0)) == 0)
              return;
          }
          DateTime? nullable1 = value;
          DateTime nullDate1 = this.NullDate;
          if ((!nullable1.HasValue ? 1 : (nullable1.GetValueOrDefault() != nullDate1 ? 1 : 0)) != 0)
          {
            DateTime? nullable2 = value;
            DateTime minDate = this.MinDate;
            if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() < minDate ? 1 : 0) : 0) != 0)
            {
              value = new DateTime?(this.MinDate);
            }
            else
            {
              DateTime? nullable3 = value;
              DateTime maxDate = this.MaxDate;
              if ((nullable3.HasValue ? (nullable3.GetValueOrDefault() > maxDate ? 1 : 0) : 0) != 0)
                value = new DateTime?(this.MaxDate);
            }
          }
          string text = this.Text;
          this._value = value;
          this.CurrentBehavior.SetDateByValue(value, this.format);
          if (!value.HasValue)
            (this.currentBehavior as RadDateTimePickerCalendar)?.Calendar.SelectedDates.Clear();
          this.BitState[140737488355328L] = true;
          if (flag)
          {
            this.OnValueChanged(EventArgs.Empty);
            this.OnNullableValueChanged(EventArgs.Empty);
          }
          if (!text.Equals(this.Text))
            this.OnTextChanged(EventArgs.Empty);
          this.OnNotifyPropertyChanged("NullableValue");
          this.OnNotifyPropertyChanged(nameof (Value));
        }
      }
    }

    private bool CheckIsEqualToNullDate(DateTime? value)
    {
      bool flag = value.HasValue;
      if (flag)
      {
        switch (this.NullValueCheckMode)
        {
          case NullValueCheckMode.Date:
            flag = value.Value.Date.Equals(this.NullDate.Date);
            break;
          case NullValueCheckMode.Time:
            flag = value.Value.TimeOfDay.Equals(this.NullDate.TimeOfDay);
            break;
          case NullValueCheckMode.DateTime:
            flag = value.Value.Equals(this.NullDate);
            break;
        }
      }
      return flag;
    }

    [Localizable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the text that is displayed when the DateTimePicker contains a null reference")]
    public string NullText
    {
      get
      {
        return this.nullText;
      }
      set
      {
        if (!(this.nullText != value))
          return;
        this.nullText = value;
        this.OnNotifyPropertyChanged(nameof (NullText));
      }
    }

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

    [Description("Gets the minimum date value allowed for the DateTimePicker control.")]
    public static DateTime MinimumDateTime
    {
      get
      {
        return CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime;
      }
      set
      {
        RadDateTimePickerElement.MinimumDateTime = value;
      }
    }

    [Description("Gets or sets the minimum date and time that can be selected in the control.")]
    [Category("Behavior")]
    public DateTime MinDate
    {
      get
      {
        return this.EffectiveMinDate(this.min);
      }
      set
      {
        if (!(value != this.min))
          return;
        if (value > this.EffectiveMaxDate(this.max))
          throw new Exception("value is higher than the maximum available value");
        if (value < RadDateTimePickerElement.MinimumDateTime)
          throw new Exception("value is lower than the minimum available value");
        this.min = value;
        DateTime? nullable = this.Value;
        DateTime min = this.min;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() < min ? 1 : 0) : 0) != 0)
          this.Value = new DateTime?(this.min);
        this.OnNotifyPropertyChanged(nameof (MinDate));
        this.currentBehavior.DateTimePickerElement.TextBoxElement.MinDate = this.min;
      }
    }

    [Description("Gets or sets the maximum date and time that can be selected in the control.")]
    [Category("Behavior")]
    public DateTime MaxDate
    {
      get
      {
        return this.EffectiveMaxDate(this.max);
      }
      set
      {
        if (!(value != this.max))
          return;
        if (value < this.EffectiveMinDate(this.min))
          throw new ArgumentOutOfRangeException("MaxDate cannot be lower than the min date");
        if (value > RadDateTimePickerElement.MaximumDateTime)
          throw new ArgumentOutOfRangeException("MaxDate cannot be higher than the max date");
        this.max = value;
        DateTime? nullable = this.Value;
        DateTime max = this.max;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() > max ? 1 : 0) : 0) != 0)
          this.Value = new DateTime?(this.max);
        this.OnNotifyPropertyChanged(nameof (MaxDate));
        this.currentBehavior.DateTimePickerElement.TextBoxElement.MaxDate = this.max;
      }
    }

    [Category("Action")]
    [Description("Occurs when MaskProvider has been created This event will be fired multiple times because the provider is created when some properties changed Properties are: Mask, Culture, MaskType and more.")]
    public event EventHandler MaskProviderCreated;

    [Description("Occurs when the value of the control has changed")]
    [Category("Action")]
    public event EventHandler ValueChanged;

    [Category("Action")]
    [Description("Occurs when the NullableValue of the control has changed")]
    public event EventHandler NullableValueChanged;

    [Category("Action")]
    [Description("Occurs when the value of the control has changed")]
    public event EventHandler FormatChanged;

    [Description("Occurs when the value of the control is changing")]
    [Category("Action")]
    public event ValueChangingEventHandler ValueChanging;

    [Category("Action")]
    [Description("Occurs when the drop down is opened")]
    public event EventHandler Opened;

    [Description("Occurs when the drop down is opening")]
    [Category("Action")]
    public event CancelEventHandler Opening;

    [Description("Occurs when the drop down is closing")]
    [Category("Action")]
    public event RadPopupClosingEventHandler Closing;

    [Description("Occurs when the drop down is closed")]
    [Category("Action")]
    public event RadPopupClosedEventHandler Closed;

    public event StateChangingEventHandler ToggleStateChanging;

    public event StateChangedEventHandler ToggleStateChanged;

    public event EventHandler CheckedChanged;

    public void SetValueOnly(DateTime? value)
    {
      this._value = value;
      this.SetBitState(140737488355328L, true);
      this.OnValueChanged(EventArgs.Empty);
      this.OnNullableValueChanged(EventArgs.Empty);
      this.OnNotifyPropertyChanged("NullableValue");
      this.OnNotifyPropertyChanged("Value");
    }

    public override string ToString()
    {
      if (this.Value.HasValue)
        return base.ToString() + ", Value: " + this.FormatDateTime(this.Value.Value);
      return base.ToString() + ", Value: NULL";
    }

    public void CallOnNullableValueChanged(EventArgs e)
    {
      this.OnNullableValueChanged(e);
    }

    public void CallOnValueChanged(EventArgs e)
    {
      this.OnValueChanged(e);
    }

    public void CallOnValueChanging(ValueChangingEventArgs e)
    {
      this.OnValueChanging(e);
    }

    public bool ShouldSerializeNullText()
    {
      return !string.IsNullOrEmpty(this.nullText);
    }

    public void ResetNullText()
    {
      this.nullText = string.Empty;
    }

    public void ResetValue()
    {
      this._value = new DateTime?(DateTime.Now);
      this.BitState[140737488355328L] = false;
      this.Checked = false;
      this.GetCurrentBehavior().SetDateByValue(this._value, this.Format);
      this.OnValueChanged(EventArgs.Empty);
      this.OnTextChanged(EventArgs.Empty);
    }

    public RadDateTimePickerBehaviorDirector GetCurrentBehavior()
    {
      return this.currentBehavior;
    }

    public void SetToNullValue()
    {
      this.Value = new DateTime?();
      (this.currentBehavior as RadDateTimePickerCalendar)?.Calendar.SelectedDates.Clear();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadCalendar Calendar
    {
      get
      {
        return (this.GetCurrentBehavior() as RadDateTimePickerCalendar)?.Calendar;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowTimePicker
    {
      get
      {
        RadDateTimePickerCalendar currentBehavior = this.GetCurrentBehavior() as RadDateTimePickerCalendar;
        if (currentBehavior == null)
          return false;
        return currentBehavior.ShowTimePicker;
      }
      set
      {
        RadDateTimePickerCalendar currentBehavior = this.GetCurrentBehavior() as RadDateTimePickerCalendar;
        if (currentBehavior == null)
          return;
        this.UnwirePopupEvents(currentBehavior);
        currentBehavior.ShowTimePicker = value;
        this.WirePopupEvents(currentBehavior);
      }
    }

    protected virtual void WirePopupEvents(RadDateTimePickerCalendar calendarBehavior)
    {
      calendarBehavior.PopupControl.Opened += new EventHandler(this.PopupControl_Opened);
      calendarBehavior.PopupControl.Opening += new CancelEventHandler(this.PopupControl_Opening);
      calendarBehavior.PopupControl.Closing += new RadPopupClosingEventHandler(this.PopupControl_Closing);
      calendarBehavior.PopupControl.Closed += new RadPopupClosedEventHandler(this.PopupControl_Closed);
      calendarBehavior.TextBoxElement.ValueChanged += new EventHandler(this.RadDateTimePickerElement_ValueChanged);
    }

    protected virtual void UnwirePopupEvents(RadDateTimePickerCalendar calendarBehavior)
    {
      calendarBehavior.PopupControl.Opened -= new EventHandler(this.PopupControl_Opened);
      calendarBehavior.PopupControl.Opening -= new CancelEventHandler(this.PopupControl_Opening);
      calendarBehavior.PopupControl.Closing -= new RadPopupClosingEventHandler(this.PopupControl_Closing);
      calendarBehavior.PopupControl.Closed -= new RadPopupClosedEventHandler(this.PopupControl_Closed);
      calendarBehavior.TextBoxElement.ValueChanged -= new EventHandler(this.RadDateTimePickerElement_ValueChanged);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      string text = this.TextBoxElement.Text;
      this.TextBoxElement.TextBoxItem.Text = string.Empty;
      this.TextBoxElement.TextBoxItem.Text = text;
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

    protected virtual void OnFormatChanged(EventArgs e)
    {
      if (this.FormatChanged == null)
        return;
      this.FormatChanged((object) this, e);
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    protected virtual void OnValueChanged(EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    protected virtual void OnNullableValueChanged(EventArgs e)
    {
      if (this.NullableValueChanged == null)
        return;
      this.NullableValueChanged((object) this, e);
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "MinDate":
          RadDateTimePickerCalendar currentBehavior1 = this.GetCurrentBehavior() as RadDateTimePickerCalendar;
          if (currentBehavior1 != null)
          {
            currentBehavior1.Calendar.RangeMinDate = this.MinDate;
            break;
          }
          break;
        case "MaxDate":
          RadDateTimePickerCalendar currentBehavior2 = this.GetCurrentBehavior() as RadDateTimePickerCalendar;
          if (currentBehavior2 != null)
          {
            currentBehavior2.Calendar.RangeMaxDate = this.MaxDate;
            break;
          }
          break;
        case "Checked":
          if (this.ShowCheckBox)
            this.TextBoxElement.TextBoxItem.HostedControl.Enabled = this.Checked;
          this.checkBox.ToggleState = this.Checked ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
          break;
        case "Format":
        case "CustomFormat":
          this.GetCurrentBehavior().SetDateByValue(this._value, this.format);
          break;
        case "ShowCheckBox":
          if (this.ShowCheckBox)
          {
            this.checkBox.Visibility = ElementVisibility.Visible;
            this.TextBoxElement.TextBoxItem.HostedControl.Enabled = this.Checked;
            break;
          }
          this.checkBox.Visibility = ElementVisibility.Collapsed;
          this.TextBoxElement.TextBoxItem.HostedControl.Enabled = true;
          break;
        case "Culture":
          RadDateTimePickerCalendar currentBehavior3 = this.GetCurrentBehavior() as RadDateTimePickerCalendar;
          if (currentBehavior3 != null)
          {
            currentBehavior3.Calendar.Culture = this.Culture;
            if (currentBehavior3.TimePicker != null)
              currentBehavior3.TimePicker.Culture = this.Culture;
          }
          if (this._value.HasValue)
          {
            this.GetCurrentBehavior().SetDateByValue(this._value, this.format);
            break;
          }
          break;
        case "ShowUpDown":
          MaskDateTimeProvider provider1 = (MaskDateTimeProvider) this.CurrentBehavior.DateTimePickerElement.TextBoxElement.Provider;
          int hoursStep = provider1.HoursStep;
          int minutesStep = provider1.MinutesStep;
          bool flag = this.ReadOnly;
          if (!this.ShowUpDown)
            this.SetBehavior((RadDateTimePickerBehaviorDirector) new RadDateTimePickerCalendar(this));
          else
            this.SetBehavior((RadDateTimePickerBehaviorDirector) new RadDateTimePickerSpinEdit(this));
          this.CurrentBehavior.DateTimePickerElement.TextBoxElement.TextBoxItem.NullText = this.nullText;
          MaskDateTimeProvider provider2 = (MaskDateTimeProvider) this.CurrentBehavior.DateTimePickerElement.TextBoxElement.Provider;
          provider2.HoursStep = hoursStep;
          provider2.MinutesStep = minutesStep;
          this.CurrentBehavior.TextBoxElement.TextBoxItem.ReadOnly = flag;
          if (string.IsNullOrEmpty(ThemeResolutionService.ApplicationThemeName))
          {
            this.ElementTree.EnableApplicationThemeName = true;
            break;
          }
          this.ElementTree.ApplyThemeToElement((RadObject) this);
          break;
        case "NullText":
          this.CurrentBehavior.DateTimePickerElement.TextBoxElement.TextBoxItem.NullText = this.nullText;
          break;
      }
      base.OnNotifyPropertyChanged(e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadDateTimePickerElement.IsDropDownShownProperty)
      {
        int num = (int) ((RadDateTimePickerCalendar) this.GetCurrentBehavior()).ArrowButton.SetValue(RadDateTimePickerElement.IsDropDownShownProperty, e.NewValue);
      }
      else
      {
        if (e.Property != RadElement.RightToLeftProperty)
          return;
        bool newValue = (bool) e.NewValue;
        if (this.Shape != null)
          this.Shape.IsRightToLeft = newValue;
        if (this.ArrowButton != null && this.ArrowButton.Shape != null)
          this.ArrowButton.Shape.IsRightToLeft = newValue;
        RadDateTimePickerSpinEdit currentBehavior = this.CurrentBehavior as RadDateTimePickerSpinEdit;
        if (currentBehavior == null || currentBehavior.ButtonsLayout == null)
          return;
        if (currentBehavior.ButtonsLayout.Shape != null)
          currentBehavior.ButtonsLayout.Shape.IsRightToLeft = newValue;
        if (currentBehavior.ButtonsLayout.Children[0].Shape != null)
          currentBehavior.ButtonsLayout.Children[0].Shape.IsRightToLeft = newValue;
        if (currentBehavior.ButtonsLayout.Children[1].Shape == null)
          return;
        currentBehavior.ButtonsLayout.Children[1].Shape.IsRightToLeft = newValue;
      }
    }

    private void checkBox_ToggleStateChanging(object sender, StateChangingEventArgs args)
    {
      this.OnToggleStateChanging(args);
    }

    private void checkBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      this.Checked = this.checkBox.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
      this.OnToggleStateChanged(args);
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

    private void OnCheckedChanged(EventArgs e)
    {
      if (this.CheckedChanged == null)
        return;
      this.CheckedChanged((object) this, e);
    }

    private void RadDateTimePickerElement_ValueChanged(object sender, EventArgs e)
    {
      RadMaskedEditBoxElement textBoxElement = this.TextBoxElement;
      if (textBoxElement.IsKeyBoard)
        return;
      DateTime result;
      if (textBoxElement.Value != null && DateTime.TryParse(textBoxElement.Value.ToString(), out result) && DateTime.Compare(result, this.MaxDate) > 0)
      {
        if (!this.Value.Equals((object) this.MaxDate))
          this.Value = new DateTime?(this.MaxDate);
        else
          textBoxElement.Value = (object) this.MaxDate;
      }
      else
      {
        if (textBoxElement.Value == null || !DateTime.TryParse(textBoxElement.Value.ToString(), out result) || DateTime.Compare(result, this.MinDate) >= 0)
          return;
        DateTime? nullable = this.Value;
        DateTime nullDate = this.NullDate;
        if ((!nullable.HasValue ? 1 : (nullable.GetValueOrDefault() != nullDate ? 1 : 0)) == 0)
          return;
        textBoxElement.Value = (object) this.MinDate;
      }
    }

    private void PopupControl_Closed(object sender, RadPopupClosedEventArgs args)
    {
      this.OnClosed(args);
    }

    private void PopupControl_Closing(object sender, RadPopupClosingEventArgs args)
    {
      this.OnClosing(args);
    }

    private void PopupControl_Opening(object sender, CancelEventArgs args)
    {
      if (this.IsInValidState(true))
      {
        string themeName = this.ElementTree.ThemeName;
        RadCalendar calendar = (this.GetCurrentBehavior() as RadDateTimePickerCalendar).Calendar;
        if (calendar.ThemeName != themeName)
          calendar.ThemeName = themeName;
        RadSizablePopupControl popupControl = (RadSizablePopupControl) (this.GetCurrentBehavior() as RadDateTimePickerCalendar).PopupControl;
        if (popupControl.ThemeName != themeName)
          popupControl.ThemeName = themeName;
      }
      this.OnOpening(args);
    }

    private void PopupControl_Opened(object sender, EventArgs args)
    {
      this.OnOpened(args);
    }

    private bool ShouldSerializeCulture()
    {
      return this.cultureInfo != null && !string.IsNullOrEmpty(this.cultureInfo.ToString()) && !(this.cultureInfo.ToString() == "en-US");
    }

    private void ResetCulture()
    {
      this.cultureInfo = new CultureInfo("en-US");
    }

    private DateTime EffectiveMaxDate(DateTime maxDate)
    {
      DateTime maximumDateTime = RadDateTimePickerElement.MaximumDateTime;
      if (maxDate > maximumDateTime)
        return maximumDateTime;
      return maxDate;
    }

    private DateTime EffectiveMinDate(DateTime minDate)
    {
      DateTime minimumDateTime = RadDateTimePickerElement.MinimumDateTime;
      if (minDate < minimumDateTime)
        return minimumDateTime;
      return minDate;
    }

    private string FormatDateTime(DateTime value)
    {
      return value.ToString("G", (IFormatProvider) CultureInfo.CurrentCulture);
    }

    protected internal virtual void SetBehavior(RadDateTimePickerBehaviorDirector childrenDirector)
    {
      RadDateTimePickerBehaviorDirector currentBehavior1 = this.GetCurrentBehavior();
      string str = string.Empty;
      if (currentBehavior1 != null)
        str = this.currentBehavior.DateTimePickerElement.TextBoxElement.TextBoxItem.NullText;
      RadDateTimePickerCalendar timePickerCalendar = currentBehavior1 as RadDateTimePickerCalendar;
      if (timePickerCalendar != null)
      {
        timePickerCalendar.PopupControl.Opened -= new EventHandler(this.PopupControl_Opened);
        timePickerCalendar.PopupControl.Opening -= new CancelEventHandler(this.PopupControl_Opening);
        timePickerCalendar.PopupControl.Closing -= new RadPopupClosingEventHandler(this.PopupControl_Closing);
        timePickerCalendar.PopupControl.Closed -= new RadPopupClosedEventHandler(this.PopupControl_Closed);
        timePickerCalendar.Calendar.ClearButton.Click -= new EventHandler(this.ClearButton_Click);
        timePickerCalendar.Dispose();
        if (this.TextBoxElement != null)
          this.TextBoxElement.ValueChanged -= new EventHandler(this.RadDateTimePickerElement_ValueChanged);
      }
      if (this.ElementTree != null)
        this.ElementTree.Control.Controls.Clear();
      bool? nullable = new bool?();
      if (this.checkBox != null)
      {
        this.checkBox.ToggleStateChanging -= new StateChangingEventHandler(this.checkBox_ToggleStateChanging);
        this.checkBox.ToggleStateChanged -= new StateChangedEventHandler(this.checkBox_ToggleStateChanged);
        nullable = new bool?(this.Checked);
      }
      this.Children.Clear();
      this.currentBehavior = childrenDirector;
      childrenDirector.CreateChildren();
      RadDateTimePickerCalendar currentBehavior2 = this.currentBehavior as RadDateTimePickerCalendar;
      if (currentBehavior2 != null)
      {
        currentBehavior2.PopupControl.Opened += new EventHandler(this.PopupControl_Opened);
        currentBehavior2.PopupControl.Opening += new CancelEventHandler(this.PopupControl_Opening);
        currentBehavior2.PopupControl.Closing += new RadPopupClosingEventHandler(this.PopupControl_Closing);
        currentBehavior2.PopupControl.Closed += new RadPopupClosedEventHandler(this.PopupControl_Closed);
        currentBehavior2.TextBoxElement.ValueChanged += new EventHandler(this.RadDateTimePickerElement_ValueChanged);
        currentBehavior2.Calendar.ClearButton.Click += new EventHandler(this.ClearButton_Click);
      }
      if (this.checkBox != null)
      {
        this.checkBox.ToggleStateChanging += new StateChangingEventHandler(this.checkBox_ToggleStateChanging);
        this.checkBox.ToggleStateChanged += new StateChangedEventHandler(this.checkBox_ToggleStateChanged);
        if (nullable.HasValue)
          this.Checked = nullable.Value;
      }
      if (this.currentBehavior.TextBoxElement == null)
        return;
      this.currentBehavior.TextBoxElement.TextBoxItem.NullText = str;
      this.currentBehavior.TextBoxElement.MaxDate = this.MaxDate;
      this.currentBehavior.TextBoxElement.MinDate = this.MinDate;
    }

    internal void CallKeyDown(KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      MaskDateTimeProvider provider = this.TextBoxElement.Provider as MaskDateTimeProvider;
      int selectedItemIndex = provider.SelectedItemIndex;
      base.OnKeyDown(e);
      if (this.ShowCheckBox)
      {
        if (e.KeyCode == Keys.Left && selectedItemIndex == provider.SelectedItemIndex)
          this.checkBox.Focus();
        if (e.KeyCode == Keys.Right && provider.List != null && provider.SelectedItemIndex == provider.List.Count - 1)
          this.checkBox.Focus();
      }
      if (e.Alt && e.KeyCode == Keys.Up)
        e.Handled = true;
      if (e.Alt && this.IsDropDownShown)
      {
        this.ToogleDropDownState();
        e.Handled = true;
      }
      if (e.KeyCode != Keys.F4 && (!e.Alt || e.KeyCode != Keys.Down))
        return;
      this.ToogleDropDownState();
      this.checkBox.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.Checked = true;
      e.Handled = true;
    }

    internal void HandleArrowsInpit(PreviewKeyDownEventArgs e)
    {
      if (!this.ShowCheckBox || !this.Checked || e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
        return;
      MaskDateTimeProvider provider = this.TextBoxElement.Provider as MaskDateTimeProvider;
      provider.TextBoxItem.HostedControl.Select();
      if (e.KeyCode == Keys.Left)
        provider.SelectLastEditableItem();
      if (e.KeyCode != Keys.Right)
        return;
      provider.SelectFirstEditableItem();
    }

    protected virtual void ToogleDropDownState()
    {
      RadDateTimePickerCalendar currentBehavior = this.CurrentBehavior as RadDateTimePickerCalendar;
      if (currentBehavior == null)
        return;
      if (currentBehavior.IsDropDownShow)
        currentBehavior.PopupControl.HideControl();
      else
        currentBehavior.ShowDropDown();
    }

    internal void CallKeyUp(KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    internal void CallKeyPress(KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    internal void CallRaiseMaskProviderCreated()
    {
      if (this.MaskProviderCreated == null)
        return;
      this.MaskProviderCreated((object) this.TextBoxElement.Provider, EventArgs.Empty);
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
      this.SetToNullValue();
    }

    protected override void OnBitStateChanged(long key, bool oldValue, bool newValue)
    {
      base.OnBitStateChanged(key, oldValue, newValue);
      switch (key)
      {
        case 8796093022208:
          this.OnNotifyPropertyChanged("ShowCheckBox");
          break;
        case 17592186044416:
          this.OnNotifyPropertyChanged("ShowUpDown");
          break;
        case 35184372088832:
          this.OnNotifyPropertyChanged("Checked");
          break;
        case 281474976710656:
          this.OnNotifyPropertyChanged("ShowCurrentTime");
          break;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      if ((double) clientRectangle.Width < 2.0)
        return SizeF.Empty;
      SizeF sizeF = base.MeasureOverride(clientRectangle.Size);
      sizeF.Width = Math.Min(sizeF.Width, availableSize.Width);
      sizeF.Height = Math.Min(sizeF.Height, availableSize.Height);
      return sizeF;
    }
  }
}
