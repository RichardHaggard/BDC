// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDateTimePickerCalendar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadDateTimePickerCalendar : RadDateTimePickerBehaviorDirector, IDisposable
  {
    private Size dropDownMinSize = Size.Empty;
    private Size dropDownMaxSize = Size.Empty;
    private int dropDownHeight = 156;
    private Size cachedSize = Size.Empty;
    private bool showFooter = true;
    private SizeF lastShowDpiScale = new SizeF(1f, 1f);
    private RadDateTimePickerElement dateTimePickerElement;
    private RadMaskedEditBoxElement textBoxElement;
    private StackLayoutElement stackLayout;
    private BorderPrimitive border;
    private FillPrimitive backGround;
    private RadCheckBoxElement checkBox;
    private RadArrowButtonElement arrowButton;
    private RadDateTimePickerDropDown popupControl;
    private RadCalendar calendar;
    private bool isDropDownShown;
    private bool maskEditValueChanged;
    private bool showTimePicker;
    private RadPanel panel;
    private RadTimePickerContent timePicker;
    private TimePickerDoneButtonContent footerPanel;

    public RadDateTimePickerCalendar(RadDateTimePickerElement dateTimePicker)
    {
      this.dateTimePickerElement = dateTimePicker;
      this.calendar = new RadCalendar();
      this.calendar.Focusable = false;
      this.CreateElements();
      this.calendar.AllowMultipleSelect = false;
      this.calendar.SelectionChanged += new EventHandler(this.calendar_SelectionChanged);
    }

    private void CreateElements()
    {
      if (this.timePicker != null)
      {
        this.timePicker.CloseButtonClicked -= new EventHandler(this.timePicker_CloseButtonClicked);
        this.timePicker.ValueChanged -= new EventHandler(this.timePicker_ValueChanged);
      }
      if (this.popupControl != null)
      {
        this.popupControl.Opened -= new EventHandler(this.popupControl_Opened);
        this.popupControl.Closing -= new RadPopupClosingEventHandler(this.popupControl_Closing);
        this.popupControl.Closed -= new RadPopupClosedEventHandler(this.popupControl_Closed);
      }
      if (this.ShowTimePicker)
      {
        this.panel = new RadPanel();
        this.calendar.Size = new Size(180, 150);
        this.calendar.Dock = DockStyle.Left;
        this.timePicker = new RadTimePickerContent();
        this.timePicker.ReadOnly = this.Calendar.ReadOnly;
        this.timePicker.Culture = this.textBoxElement.Culture;
        this.timePicker.CloseButtonClicked += new EventHandler(this.timePicker_CloseButtonClicked);
        this.timePicker.ValueChanged += new EventHandler(this.timePicker_ValueChanged);
        this.timePicker.Dock = DockStyle.Fill;
        if (this.showFooter)
        {
          this.footerPanel = new TimePickerDoneButtonContent(this.timePicker.TimePickerElement);
          this.footerPanel.Dock = DockStyle.Bottom;
        }
        this.panel.Controls.Add((Control) this.timePicker);
        this.panel.Controls.Add((Control) this.calendar);
        if (this.showFooter)
          this.panel.Controls.Add((Control) this.footerPanel);
      }
      this.popupControl = new RadDateTimePickerDropDown((RadItem) this.dateTimePickerElement);
      this.popupControl.SizingMode = SizingMode.UpDownAndRightBottom;
      this.popupControl.Opened += new EventHandler(this.popupControl_Opened);
      this.popupControl.Closing += new RadPopupClosingEventHandler(this.popupControl_Closing);
      this.popupControl.Closed += new RadPopupClosedEventHandler(this.popupControl_Closed);
      this.popupControl.HostedControl = !this.ShowTimePicker ? (RadControl) this.calendar : (RadControl) this.panel;
      string themeName = this.Calendar.ThemeName;
      this.popupControl.ThemeName = themeName;
      if (this.ShowTimePicker && this.TimePicker != null)
      {
        this.panel.ThemeName = themeName;
        this.TimePicker.ThemeName = themeName;
        if (this.ShowFooter)
          this.FooterPanel.ThemeName = themeName;
      }
      this.popupControl.LoadElementTree();
    }

    private void timePicker_ValueChanged(object sender, EventArgs e)
    {
      this.SetDate(true);
    }

    private void timePicker_CloseButtonClicked(object sender, EventArgs e)
    {
      this.popupControl.HideControl();
    }

    public override void CreateChildren()
    {
      this.backGround = new FillPrimitive();
      this.backGround.Class = "DateTimePickerBackGround";
      int num1 = (int) this.backGround.SetDefaultValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid);
      this.dateTimePickerElement.Children.Add((RadElement) this.backGround);
      this.border = new BorderPrimitive();
      this.border.Class = "DateTimePickerBorder";
      this.border.Visibility = ElementVisibility.Visible;
      this.dateTimePickerElement.Children.Add((RadElement) this.border);
      this.stackLayout = new StackLayoutElement();
      int num2 = (int) this.stackLayout.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) true);
      int num3 = (int) this.stackLayout.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      this.stackLayout.Class = "DateTimePickerCalendarLayout";
      this.stackLayout.FitInAvailableSize = true;
      this.stackLayout.CanFocus = false;
      this.dateTimePickerElement.Children.Add((RadElement) this.stackLayout);
      this.checkBox = new RadCheckBoxElement();
      int num4 = (int) this.checkBox.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) false);
      int num5 = (int) this.checkBox.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      int num6 = (int) this.checkBox.SetDefaultValueOverride(RadElement.AlignmentProperty, (object) ContentAlignment.MiddleCenter);
      this.stackLayout.Children.Add((RadElement) this.checkBox);
      this.dateTimePickerElement.CheckBox = this.checkBox;
      if (!this.dateTimePickerElement.ShowCheckBox)
        this.checkBox.Visibility = ElementVisibility.Collapsed;
      this.textBoxElement = new RadMaskedEditBoxElement();
      this.textBoxElement.MaskProviderCreated += new EventHandler(this.textBoxElement_MaskProviderCreated);
      this.textBoxElement.Mask = "";
      this.textBoxElement.ShowBorder = false;
      this.textBoxElement.Class = "textbox";
      this.textBoxElement.ThemeRole = "DateTimePickerMaskTextBoxElement";
      int num7 = (int) this.textBoxElement.SetDefaultValueOverride(RadElement.AlignmentProperty, (object) ContentAlignment.MiddleLeft);
      int num8 = (int) this.textBoxElement.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) true);
      int num9 = (int) this.textBoxElement.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      int num10 = (int) this.textBoxElement.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Hidden);
      int num11 = (int) this.textBoxElement.Border.SetDefaultValueOverride(VisualElement.ForeColorProperty, (object) Color.Transparent);
      int num12 = (int) this.textBoxElement.Fill.SetDefaultValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid);
      int num13 = (int) this.textBoxElement.TextBoxItem.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) false);
      int num14 = (int) this.textBoxElement.TextBoxItem.SetDefaultValueOverride(RadElement.AlignmentProperty, (object) ContentAlignment.MiddleLeft);
      this.stackLayout.Children.Add((RadElement) this.textBoxElement);
      int num15 = (int) this.textBoxElement.Children[this.TextBoxElement.Children.Count - 1].SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      this.textBoxElement.MaskType = MaskType.DateTime;
      this.textBoxElement.ValueChanged += new EventHandler(this.maskBox_ValueChanged);
      this.textBoxElement.ValueChanging += new CancelEventHandler(this.maskBox_ValueChanging);
      this.textBoxElement.TextBoxItem.LostFocus += new EventHandler(this.MaskBox_LostFocus);
      this.textBoxElement.TextBoxItem.MouseDown += new MouseEventHandler(this.maskBox_MouseDown);
      this.textBoxElement.KeyDown += new KeyEventHandler(this.textBoxElement_KeyDown);
      this.textBoxElement.KeyPress += new KeyPressEventHandler(this.textBoxElement_KeyPress);
      this.textBoxElement.KeyUp += new KeyEventHandler(this.textBoxElement_KeyUp);
      this.arrowButton = (RadArrowButtonElement) new RadDateTimePickerArrowButtonElement();
      this.arrowButton.Class = "ArrowButton";
      int num16 = (int) this.arrowButton.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) false);
      int num17 = (int) this.arrowButton.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      int num18 = (int) this.arrowButton.Arrow.SetDefaultValueOverride(RadElement.AutoSizeProperty, (object) true);
      int num19 = (int) this.arrowButton.Arrow.SetDefaultValueOverride(RadElement.AlignmentProperty, (object) ContentAlignment.MiddleCenter);
      int num20 = (int) this.arrowButton.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(17, 6));
      this.stackLayout.Children.Add((RadElement) this.arrowButton);
      this.arrowButton.MouseDown -= new MouseEventHandler(this.arrowButton_MouseDown);
      this.arrowButton.MouseDown += new MouseEventHandler(this.arrowButton_MouseDown);
      if (this.dateTimePickerElement.RightToLeft)
        this.calendar.RightToLeft = RightToLeft.Yes;
      else
        this.calendar.RightToLeft = RightToLeft.No;
      this.SetDateByValue(this.dateTimePickerElement.Value, this.dateTimePickerElement.Format);
      this.calendar.RangeMinDate = this.DateTimePickerElement.MinDate;
      this.calendar.RangeMaxDate = this.DateTimePickerElement.MaxDate;
    }

    private void textBoxElement_MaskProviderCreated(object sender, EventArgs e)
    {
      this.dateTimePickerElement.CallRaiseMaskProviderCreated();
    }

    public void Dispose()
    {
      if (this.textBoxElement != null)
      {
        this.textBoxElement.ValueChanged -= new EventHandler(this.maskBox_ValueChanged);
        this.textBoxElement.ValueChanging -= new CancelEventHandler(this.maskBox_ValueChanging);
        this.textBoxElement.MaskProviderCreated -= new EventHandler(this.textBoxElement_MaskProviderCreated);
        this.textBoxElement.TextBoxItem.LostFocus -= new EventHandler(this.MaskBox_LostFocus);
        this.textBoxElement.TextBoxItem.MouseDown -= new MouseEventHandler(this.maskBox_MouseDown);
        this.textBoxElement.KeyDown -= new KeyEventHandler(this.textBoxElement_KeyDown);
        this.textBoxElement.KeyPress -= new KeyPressEventHandler(this.textBoxElement_KeyPress);
        this.textBoxElement.KeyUp -= new KeyEventHandler(this.textBoxElement_KeyUp);
      }
      if (this.calendar != null)
      {
        this.calendar.SelectionChanged -= new EventHandler(this.calendar_SelectionChanged);
        this.calendar.Dispose();
        this.calendar = (RadCalendar) null;
      }
      if (this.timePicker != null)
      {
        this.timePicker.ValueChanged -= new EventHandler(this.timePicker_ValueChanged);
        this.timePicker.CloseButtonClicked -= new EventHandler(this.timePicker_CloseButtonClicked);
      }
      if (this.popupControl != null)
      {
        this.popupControl.Opened -= new EventHandler(this.popupControl_Opened);
        this.popupControl.Closing -= new RadPopupClosingEventHandler(this.popupControl_Closing);
        this.popupControl.Closed -= new RadPopupClosedEventHandler(this.popupControl_Closed);
        this.popupControl.Dispose();
        this.popupControl = (RadDateTimePickerDropDown) null;
      }
      if (this.textBoxElement != null)
      {
        this.textBoxElement.Dispose();
        this.textBoxElement.DisposeChildren();
        this.textBoxElement = (RadMaskedEditBoxElement) null;
      }
      if (this.checkBox != null)
      {
        this.checkBox.Dispose();
        this.checkBox = (RadCheckBoxElement) null;
      }
      if (this.stackLayout != null)
      {
        this.stackLayout.Dispose();
        this.stackLayout = (StackLayoutElement) null;
      }
      if (this.border != null)
      {
        this.border.Dispose();
        this.border = (BorderPrimitive) null;
      }
      if (this.backGround != null)
      {
        this.backGround.Dispose();
        this.backGround = (FillPrimitive) null;
      }
      if (this.arrowButton == null)
        return;
      this.arrowButton.Dispose();
      this.arrowButton = (RadArrowButtonElement) null;
    }

    [Browsable(false)]
    [Description("Gets the instance of RadDateTimePickerElement associated to the control")]
    [Category("Behavior")]
    public override RadDateTimePickerElement DateTimePickerElement
    {
      get
      {
        return this.dateTimePickerElement;
      }
    }

    public override RadMaskedEditBoxElement TextBoxElement
    {
      get
      {
        return this.textBoxElement;
      }
    }

    public RadArrowButtonElement ArrowButton
    {
      get
      {
        return this.arrowButton;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the calendar control which is shown when the pop up control is shown")]
    [DefaultValue(SizingMode.None)]
    public RadCalendar Calendar
    {
      get
      {
        return this.calendar;
      }
      set
      {
        this.calendar = value;
        if (this.popupControl == null)
          return;
        this.popupControl.HostedControl = (RadControl) this.calendar;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the drop down control which is shown when the user clicks on the arrow button")]
    [DefaultValue(SizingMode.None)]
    public RadDateTimePickerDropDown PopupControl
    {
      get
      {
        return this.popupControl;
      }
      set
      {
        this.popupControl = value;
      }
    }

    [Description("Gets or sets the drop down sizing mode. The mode can be: horizontal, vertical or a combination of them.")]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(SizingMode.None)]
    public SizingMode DropDownSizingMode
    {
      get
      {
        return this.popupControl.SizingMode;
      }
      set
      {
        this.popupControl.SizingMode = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the drop down minimum size.")]
    [DefaultValue(typeof (Size), "0,0")]
    public Size DropDownMinSize
    {
      get
      {
        return this.dropDownMinSize;
      }
      set
      {
        this.dropDownMinSize = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the drop down maximum size.")]
    [DefaultValue(typeof (Size), "0,0")]
    public Size DropDownMaxSize
    {
      get
      {
        return this.dropDownMaxSize;
      }
      set
      {
        this.dropDownMaxSize = value;
      }
    }

    [Description("Gets a value representing whether the drop down is shown")]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Browsable(false)]
    public bool IsDropDownShow
    {
      get
      {
        return PopupManager.Default.ContainsPopup((IPopupControl) this.popupControl);
      }
    }

    public bool ShowTimePicker
    {
      get
      {
        return this.showTimePicker;
      }
      set
      {
        if (this.showTimePicker == value)
          return;
        this.showTimePicker = value;
        this.CreateElements();
      }
    }

    public bool ShowFooter
    {
      get
      {
        return this.showFooter;
      }
      set
      {
        if (this.showFooter == value)
          return;
        this.showFooter = value;
        this.CreateElements();
      }
    }

    public RadTimePickerContent TimePicker
    {
      get
      {
        return this.timePicker;
      }
    }

    public TimePickerDoneButtonContent FooterPanel
    {
      get
      {
        return this.footerPanel;
      }
    }

    public SizeF LastShowDpiScale
    {
      get
      {
        return this.lastShowDpiScale;
      }
      set
      {
        this.lastShowDpiScale = value;
      }
    }

    private void textBoxElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.DateTimePickerElement.CallKeyUp(e);
      if (!this.DateTimePickerElement.Value.Equals((object) this.DateTimePickerElement.NullDate) || e.KeyValue != 46 && e.KeyValue != 8)
        return;
      this.textBoxElement.TextBoxItem.Text = "";
    }

    private void textBoxElement_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.DateTimePickerElement.CallKeyPress(e);
      if (!this.DateTimePickerElement.Value.Equals((object) this.DateTimePickerElement.NullDate) || e.KeyChar != '\b')
        return;
      this.textBoxElement.TextBoxItem.Text = "";
    }

    private void textBoxElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.DateTimePickerElement.CallKeyDown(e);
      if (!this.DateTimePickerElement.Value.Equals((object) this.DateTimePickerElement.NullDate) || e.KeyValue != 46 && e.KeyValue != 8)
        return;
      this.textBoxElement.TextBoxItem.Text = "";
    }

    private void arrowButton_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        this.popupControl.HideControl();
      }
      else
      {
        if (!this.IsDropDownShow)
        {
          DateTime selectedDate = this.Calendar.SelectedDate;
          DateTime? nullable = this.DateTimePickerElement.Value;
          if ((!nullable.HasValue ? 1 : (selectedDate != nullable.GetValueOrDefault() ? 1 : 0)) != 0 && this.DateTimePickerElement.Value.HasValue && this.dateTimePickerElement.Value.Value.Date.Equals(DateTime.MinValue.Date))
          {
            this.calendar.SelectedDate = this.DateTimePickerElement.Value.Value;
            this.Calendar.FocusedDate = this.Calendar.SelectedDate;
            if (this.ShowTimePicker && this.TimePicker != null)
              this.TimePicker.Value = (object) this.DateTimePickerElement.Value.Value.Date;
          }
        }
        if (!this.IsDropDownShow)
          this.ShowDropDown();
        else
          this.popupControl.ClosePopup(RadPopupCloseReason.Keyboard);
      }
    }

    private void maskBox_ValueChanged(object sender, EventArgs e)
    {
      if (!this.maskEditValueChanged)
        return;
      if (this.textBoxElement.Value != null && this.textBoxElement.TextBoxItem.Text != "")
      {
        if (this.textBoxElement.Value is DateTime)
        {
          this.DateTimePickerElement.SetValueOnly(new DateTime?((DateTime) this.textBoxElement.Value));
        }
        else
        {
          DateTime result;
          if (!DateTime.TryParse(this.textBoxElement.Value.ToString(), out result))
            return;
          this.DateTimePickerElement.SetValueOnly(new DateTime?(result));
        }
      }
      else
      {
        if (this.textBoxElement.Value != null)
          return;
        this.DateTimePickerElement.Value = new DateTime?();
      }
    }

    private void maskBox_ValueChanging(object sender, CancelEventArgs e)
    {
      ValueChangingEventArgs e1 = e as ValueChangingEventArgs ?? new ValueChangingEventArgs((object) null, (object) null);
      this.dateTimePickerElement.CallOnValueChanging(e1);
      e.Cancel = e1.Cancel;
    }

    private void popupControl_Closed(object sender, RadPopupClosedEventArgs args)
    {
      this.isDropDownShown = false;
      this.dateTimePickerElement.IsDropDownShown = false;
    }

    private void popupControl_Closing(object sender, RadPopupClosingEventArgs args)
    {
      this.cachedSize = this.popupControl.Size;
      Form form = this.dateTimePickerElement.ElementTree.Control.FindForm();
      if (form == null)
        return;
      form.FormClosed -= new FormClosedEventHandler(this.frm_FormClosed);
    }

    private void popupControl_Opened(object sender, EventArgs e)
    {
      this.isDropDownShown = true;
      this.dateTimePickerElement.IsDropDownShown = true;
      Form form = this.dateTimePickerElement.ElementTree.Control.FindForm();
      if (form == null)
        return;
      form.FormClosed += new FormClosedEventHandler(this.frm_FormClosed);
    }

    private void frm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (this.PopupControl == null)
        return;
      this.PopupControl.HideControl();
    }

    private void maskBox_MouseDown(object sender, MouseEventArgs e)
    {
      if (!this.DateTimePickerElement.Value.Equals((object) this.DateTimePickerElement.NullDate))
        return;
      this.textBoxElement.TextBoxItem.Text = "";
    }

    private void MaskBox_LostFocus(object sender, EventArgs e)
    {
      if (!this.dateTimePickerElement.Value.HasValue)
        return;
      if (this.textBoxElement.Provider is MaskDateTimeProvider)
        ((MaskDateTimeProvider) this.TextBoxElement.Provider).ValidateRange();
      if (this.textBoxElement.Value == null && !this.dateTimePickerElement.Value.HasValue)
        return;
      DateTime? nullable = this.dateTimePickerElement.Value;
      DateTime dateTime1 = (DateTime) this.textBoxElement.Value;
      if ((!nullable.HasValue ? 0 : (nullable.GetValueOrDefault() == dateTime1 ? 1 : 0)) != 0)
        return;
      this.dateTimePickerElement.TextBoxElement.Validate(this.dateTimePickerElement.TextBoxElement.Text);
      DateTime dateTime2 = this.dateTimePickerElement.Value ?? this.dateTimePickerElement.NullDate;
      if (dateTime2 != this.DateTimePickerElement.NullDate && (dateTime2 < this.DateTimePickerElement.MinDate || dateTime2 > this.DateTimePickerElement.MaxDate))
      {
        if (dateTime2 < this.DateTimePickerElement.MinDate)
          dateTime2 = this.DateTimePickerElement.MinDate;
        else if (dateTime2 > this.DateTimePickerElement.MaxDate)
          dateTime2 = this.DateTimePickerElement.MaxDate;
      }
      if (dateTime2.Date.Equals(this.DateTimePickerElement.NullDate.Date))
        this.textBoxElement.TextBoxItem.Text = "";
      else
        this.textBoxElement.Value = (object) dateTime2;
    }

    private void calendar_SelectionChanged(object sender, EventArgs e)
    {
      this.SetDate(false);
      if (this.showTimePicker)
        return;
      this.PopupControl.HideControl();
    }

    public virtual void ShowDropDown()
    {
      if (this.dateTimePickerElement.Visibility != ElementVisibility.Visible)
        return;
      RadControl control = this.dateTimePickerElement.ElementTree.Control as RadControl;
      if (this.DateTimePickerElement.Value.HasValue)
      {
        this.calendar.FocusedDate = this.DateTimePickerElement.Value.Value;
        if (this.showTimePicker && this.timePicker != null)
          this.timePicker.Value = (object) this.DateTimePickerElement.Value.Value;
      }
      this.popupControl.OwnerControl = control;
      if (this.isDropDownShown)
        return;
      this.SetDropDownMinMaxSize();
      this.popupControl.Size = this.GetDropDownSize();
      if (this.dateTimePickerElement.ElementTree == null || this.dateTimePickerElement.ElementTree == null)
        return;
      if (this.popupControl.HostedControl != null)
        this.popupControl.HostedControl.Size = new Size(this.popupControl.Size.Width, this.popupControl.Size.Height - (int) this.popupControl.SizingGrip.DesiredSize.Height);
      this.Calendar.SelectedDates.Clear();
      Point point = this.popupControl.ShowControl(RadDirection.Down, 0);
      if (this.ShowTimePicker && this.TimePicker != null && this.DateTimePickerElement.Value.HasValue)
      {
        this.TimePicker.TimePickerElement.ClearSelection();
        this.TimePicker.TimePickerElement.PrepareContent();
        this.TimePicker.TimePickerElement.SetSelected(new DateTime?(this.DateTimePickerElement.Value.Value));
      }
      this.textBoxElement.TextBoxItem.Focus();
      if (!this.dateTimePickerElement.RightToLeft)
      {
        this.popupControl.Location = point;
      }
      else
      {
        int num = this.PopupControl.Size.Width - this.dateTimePickerElement.ControlBoundingRectangle.Width;
        point = new Point(point.X - num, point.Y);
        this.popupControl.Location = point;
      }
      if (!(this.DateTimePickerElement.CalendarLocation != Point.Empty))
        return;
      this.popupControl.Location = this.DateTimePickerElement.CalendarLocation;
    }

    public override void SetDateByValue(DateTime? date, DateTimePickerFormat formatType)
    {
      CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
      this.maskEditValueChanged = false;
      Thread.CurrentThread.CurrentCulture = this.Calendar.Culture;
      this.textBoxElement.Culture = this.Calendar.Culture;
      DateTime? nullable1 = date;
      DateTime nullDate = this.DateTimePickerElement.NullDate;
      if ((!nullable1.HasValue ? 1 : (nullable1.GetValueOrDefault() != nullDate ? 1 : 0)) != 0)
      {
        DateTime? nullable2 = date;
        DateTime focusedDate = this.calendar.FocusedDate;
        if ((!nullable2.HasValue ? 1 : (nullable2.GetValueOrDefault() != focusedDate ? 1 : 0)) != 0 && date.HasValue)
        {
          this.calendar.FocusedDate = date.Value;
          if (this.showTimePicker && this.timePicker != null)
            this.timePicker.Value = (object) date.Value;
        }
        bool flag = false;
        MaskDateTimeProvider provider1 = this.textBoxElement.Provider as MaskDateTimeProvider;
        if (provider1 != null)
          flag = provider1.AutoSelectNextPart;
        switch (formatType)
        {
          case DateTimePickerFormat.Long:
            this.textBoxElement.Mask = "D";
            break;
          case DateTimePickerFormat.Short:
            this.textBoxElement.Mask = "d";
            break;
          case DateTimePickerFormat.Time:
            if (this.dateTimePickerElement.ShowCurrentTime)
              date = date.HasValue ? new DateTime?(new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, date.Value.Kind)) : new DateTime?(DateTime.Now);
            this.textBoxElement.Mask = "T";
            break;
          case DateTimePickerFormat.Custom:
            this.textBoxElement.Mask = this.dateTimePickerElement.CustomFormat;
            break;
        }
        MaskDateTimeProvider provider2 = this.textBoxElement.Provider as MaskDateTimeProvider;
        if (provider2 != null)
          provider2.AutoSelectNextPart = flag;
        if (this.textBoxElement.Value == null)
          this.textBoxElement.Value = (object) date;
        if (!this.textBoxElement.IsKeyBoard)
          this.textBoxElement.Value = (object) date;
      }
      else if (!this.textBoxElement.IsKeyBoard)
      {
        if (this.textBoxElement.Value != null && !this.textBoxElement.Value.Equals((object) date))
          this.textBoxElement.Value = (object) date;
        this.textBoxElement.TextBoxItem.Text = "";
      }
      Thread.CurrentThread.CurrentCulture = currentCulture;
      this.maskEditValueChanged = true;
    }

    private void SetDate(bool updateTime)
    {
      if (updateTime)
      {
        DateTime dateTime1 = this.TextBoxElement.Value == null || !(this.TextBoxElement.Value is DateTime) ? DateTime.Now : (DateTime) this.TextBoxElement.Value;
        if (this.ShowTimePicker && this.TimePicker != null && (this.TimePicker.Value != null && this.TimePicker.Value is DateTime))
        {
          DateTime dateTime2 = (DateTime) this.TimePicker.Value;
          dateTime1 = new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day, dateTime2.Hour, dateTime2.Minute, dateTime2.Second, dateTime2.Millisecond);
        }
        this.TextBoxElement.IsKeyBoard = true;
        this.TextBoxElement.Value = (object) dateTime1;
        this.TextBoxElement.IsKeyBoard = false;
      }
      else
      {
        if (this.Calendar.SelectedDates.Count <= 0 || !(this.Calendar.SelectedDate.Date >= this.DateTimePickerElement.MinDate.Date) || !(this.Calendar.SelectedDate.Date <= this.DateTimePickerElement.MaxDate.Date))
          return;
        DateTime dateTime = !this.ShowTimePicker || this.TimePicker == null || (this.TimePicker.Value == null || !(this.TimePicker.Value is DateTime)) ? this.Calendar.SelectedDate : (DateTime) this.TimePicker.Value;
        this.TextBoxElement.IsKeyBoard = true;
        this.TextBoxElement.Value = (object) new DateTime(this.Calendar.SelectedDate.Year, this.Calendar.SelectedDate.Month, this.Calendar.SelectedDate.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        this.TextBoxElement.IsKeyBoard = false;
      }
    }

    private Size GetDropDownSize()
    {
      Size size = Size.Empty;
      SizeF scaleFactor1 = this.PopupControl == null || this.PopupControl.OwnerElement == null ? new SizeF(1f, 1f) : this.PopupControl.OwnerElement.DpiScaleFactor;
      int val1 = TelerikDpiHelper.ScaleInt(185, scaleFactor1);
      size = new Size(Math.Max(val1, this.dateTimePickerElement.ControlBoundingRectangle.Width), Math.Max(val1, TelerikDpiHelper.ScaleInt(this.dropDownHeight, scaleFactor1)));
      if (this.dateTimePickerElement.ElementTree != null)
      {
        if (size.Width == -1)
          size.Width = this.dateTimePickerElement.ElementTree.Control.Width;
        size.Height = TelerikDpiHelper.ScaleInt(this.dropDownHeight, scaleFactor1) + (this.DropDownSizingMode != SizingMode.None ? 10 : 0);
        if (this.dateTimePickerElement.CalendarSize != new Size(100, 156))
          size = this.dateTimePickerElement.CalendarSize;
        size = new Size(size.Width, size.Height);
        if (this.cachedSize != Size.Empty)
          size = this.cachedSize;
        if (this.LastShowDpiScale != scaleFactor1)
        {
          SizeF scaleFactor2 = new SizeF(this.DateTimePickerElement.DpiScaleFactor.Width / this.LastShowDpiScale.Width, this.DateTimePickerElement.DpiScaleFactor.Height / this.LastShowDpiScale.Height);
          this.LastShowDpiScale = this.DateTimePickerElement.DpiScaleFactor;
          size = TelerikDpiHelper.ScaleSize(size, scaleFactor2);
        }
      }
      return size;
    }

    private void SetDropDownMinMaxSize()
    {
      if (this.DropDownSizingMode != SizingMode.None)
      {
        this.popupControl.MinimumSize = LayoutUtils.UnionSizes(this.dropDownMinSize, new Size(0, this.dropDownMaxSize.Height + 10));
        if (!(this.dropDownMaxSize != Size.Empty))
          return;
        this.popupControl.MaximumSize = new Size(this.dropDownMaxSize.Width, this.dropDownMaxSize.Height + 10);
      }
      else
      {
        this.popupControl.MinimumSize = Size.Empty;
        this.popupControl.MaximumSize = Size.Empty;
      }
    }

    public static Size GetDpiScaledSize(Size dpi96Size)
    {
      Point systemDpi = Telerik.WinControls.NativeMethods.GetSystemDpi();
      return new Size((int) ((double) dpi96Size.Width * (double) systemDpi.X / 96.0), (int) ((double) dpi96Size.Height * (double) systemDpi.Y / 96.0));
    }
  }
}
