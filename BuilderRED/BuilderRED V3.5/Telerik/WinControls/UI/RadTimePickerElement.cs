// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTimePickerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Design;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadTimePickerElement : PopupEditorBaseElement, IPickerContentElementOwner
  {
    public static RadProperty IsDropDownShownProperty = RadProperty.Register("IsDropDownShown", typeof (bool), typeof (RadTimePickerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty PopupMinSizeProperty = RadProperty.Register(nameof (PopupMinSize), typeof (Size), typeof (RadTimePickerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(0, 0), ElementPropertyOptions.None));
    private string hourText = "Hours";
    private string minutesText = "Minutes";
    private string format = "t";
    private int step = 5;
    private int oldStep = 5;
    private bool showSpinButtons = true;
    private RadTimePickerPopup popup;
    private RadMaskedEditBoxElement editBox;
    private RadTimeDropDownButtonElement dropDownButton;
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;
    private StackLayoutElement layout;
    private StackLayoutElement spinButtonsStackLayout;
    private RadRepeatArrowElement buttonUp;
    private RadRepeatArrowElement buttonDown;
    private Size oldPopupSize;
    private CultureInfo culture;

    public RadTimePickerElement()
    {
      this.WireEvents();
      this.DropDownAnimationEnabled = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.fillPrimitive = new FillPrimitive();
      int num = (int) this.fillPrimitive.BindProperty(RadElement.AutoSizeModeProperty, (RadObject) this, RadElement.AutoSizeModeProperty, PropertyBindingOptions.TwoWay);
      this.fillPrimitive.Class = "TimePickerFill";
      this.fillPrimitive.ZIndex = -1;
      this.Children.Add((RadElement) this.fillPrimitive);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "TimePickerBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.layout = new StackLayoutElement();
      this.layout.StretchHorizontally = true;
      this.layout.StretchVertically = true;
      this.layout.Class = "TimePickerLayout";
      this.layout.FitInAvailableSize = true;
      this.Children.Add((RadElement) this.layout);
      this.CreateTextEditorElement();
      this.CreateArrowButtonElement();
      this.CreateSpinButtons();
    }

    protected override RadPopupControlBase CreatePopupForm()
    {
      this.popup = new RadTimePickerPopup(this);
      this.popup.VerticalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
      this.popup.SizingMode = SizingMode.UpDownAndRightBottom;
      this.popup.MinimumSize = this.PopupMinSize;
      this.WirePopupFormEvents((RadPopupControlBase) this.popup);
      return (RadPopupControlBase) this.popup;
    }

    protected virtual RadRepeatArrowElement CreateUpButton()
    {
      return (RadRepeatArrowElement) new RadTimeElementUpButton();
    }

    protected virtual RadRepeatArrowElement CreateDownButton()
    {
      return (RadRepeatArrowElement) new RadTimeElementDownButton();
    }

    protected virtual void CreateTextEditorElement()
    {
      this.editBox = (RadMaskedEditBoxElement) new RadTimeMaskedEditBoxElement();
      this.editBox.MaskType = MaskType.DateTime;
      this.editBox.Mask = this.Format;
      this.layout.Children.Add((RadElement) this.editBox);
      if (this.Value != null)
      {
        DateTime dateTime = (DateTime) this.Value;
        this.editBox.MaxDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, this.MaxValue.Hour, this.MaxValue.Minute, this.MaxValue.Second);
        this.editBox.MinDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, this.MinValue.Hour, this.MinValue.Minute, this.MinValue.Second);
      }
      else
      {
        this.editBox.MaxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, this.MaxValue.Hour, this.MaxValue.Minute, this.MaxValue.Second);
        this.editBox.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, this.MinValue.Hour, this.MinValue.Minute, this.MinValue.Second);
      }
    }

    protected virtual void CreateArrowButtonElement()
    {
      this.dropDownButton = new RadTimeDropDownButtonElement();
      this.dropDownButton.CanFocus = false;
      this.dropDownButton.ClickMode = ClickMode.Press;
      this.dropDownButton.Click += new EventHandler(this.arrowButton_Click);
      this.dropDownButton.ZIndex = 1;
      this.dropDownButton.StretchHorizontally = false;
      this.dropDownButton.StretchVertically = true;
      this.layout.Children.Add((RadElement) this.dropDownButton);
    }

    protected virtual void CreateSpinButtons()
    {
      this.spinButtonsStackLayout = new StackLayoutElement();
      this.spinButtonsStackLayout.Orientation = Orientation.Vertical;
      this.spinButtonsStackLayout.StretchVertically = true;
      this.spinButtonsStackLayout.StretchHorizontally = false;
      this.spinButtonsStackLayout.FitInAvailableSize = true;
      this.spinButtonsStackLayout.Class = "ButtonsLayout";
      this.layout.Children.Add((RadElement) this.spinButtonsStackLayout);
      this.buttonUp = this.CreateUpButton();
      this.buttonUp.Click += new EventHandler(this.buttonUp_Click);
      this.buttonUp.DoubleClick += new EventHandler(this.buttonUp_Click);
      this.buttonUp.StretchHorizontally = false;
      this.buttonUp.StretchVertically = true;
      this.buttonUp.Direction = Telerik.WinControls.ArrowDirection.Up;
      this.spinButtonsStackLayout.Children.Add((RadElement) this.buttonUp);
      this.buttonDown = this.CreateDownButton();
      this.buttonDown.Click += new EventHandler(this.buttonDown_Click);
      this.buttonDown.DoubleClick += new EventHandler(this.buttonDown_Click);
      this.buttonDown.StretchVertically = true;
      this.buttonDown.StretchHorizontally = false;
      this.buttonDown.Direction = Telerik.WinControls.ArrowDirection.Down;
      this.spinButtonsStackLayout.Children.Add((RadElement) this.buttonDown);
    }

    protected virtual void WireEvents()
    {
      this.PopupOpened += new EventHandler(this.OnPopupOpened);
      this.PopupClosed += new RadPopupClosedEventHandler(this.OnPopupClosed);
      this.PopupClosing += new RadPopupClosingEventHandler(this.OnPopupClosing);
      this.editBox.KeyPress += new KeyPressEventHandler(this.MaskEditBox_KeyPress);
      this.editBox.KeyDown += new KeyEventHandler(this.MaskEditBox_KeyDown);
      this.editBox.KeyUp += new KeyEventHandler(this.MaskEditBox_KeyUp);
      this.editBox.Click += new EventHandler(this.MaskEditBox_Click);
      this.editBox.MouseWheel += new MouseEventHandler(this.MaskEditBox_MouseWheel);
      this.editBox.MouseUp += new MouseEventHandler(this.MaskEditBox_MouseUp);
      this.editBox.ValueChanging += new CancelEventHandler(this.OnValueChanging);
      this.editBox.ValueChanged += new EventHandler(this.OnValueChanged);
      this.PopupContentElement.TimeCellFormatting += new TimeCellFormattingEventHandler(this.PopupContentElement_TimeCellFormatting);
      LocalizationProvider<RadTimePickerLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.RadTimePickerLocalizationProvider_CurrentProviderChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.PopupOpened -= new EventHandler(this.OnPopupOpened);
      this.PopupClosed -= new RadPopupClosedEventHandler(this.OnPopupClosed);
      this.PopupClosing -= new RadPopupClosingEventHandler(this.OnPopupClosing);
      this.editBox.KeyPress -= new KeyPressEventHandler(this.MaskEditBox_KeyPress);
      this.editBox.KeyDown -= new KeyEventHandler(this.MaskEditBox_KeyDown);
      this.editBox.KeyUp -= new KeyEventHandler(this.MaskEditBox_KeyUp);
      this.editBox.Click -= new EventHandler(this.MaskEditBox_Click);
      this.editBox.MouseWheel -= new MouseEventHandler(this.MaskEditBox_MouseWheel);
      this.editBox.MouseUp -= new MouseEventHandler(this.MaskEditBox_MouseUp);
      this.editBox.ValueChanging -= new CancelEventHandler(this.OnValueChanging);
      this.editBox.ValueChanged -= new EventHandler(this.OnValueChanged);
      this.PopupContentElement.TimeCellFormatting -= new TimeCellFormattingEventHandler(this.PopupContentElement_TimeCellFormatting);
      LocalizationProvider<RadTimePickerLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.RadTimePickerLocalizationProvider_CurrentProviderChanged);
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      base.DisposeManagedResources();
    }

    public virtual RadMaskedEditBoxElement MaskedEditBox
    {
      get
      {
        return this.editBox;
      }
    }

    public virtual RadTimePickerContentElement PopupContentElement
    {
      get
      {
        return this.PopupForm.ContentElement;
      }
    }

    public RadTimeDropDownButtonElement DropDownButton
    {
      get
      {
        return this.dropDownButton;
      }
    }

    public RadRepeatArrowElement UpButton
    {
      get
      {
        return this.buttonUp;
      }
    }

    public RadRepeatArrowElement DownButton
    {
      get
      {
        return this.buttonDown;
      }
    }

    public RadTimePickerPopup PopupForm
    {
      get
      {
        return (RadTimePickerPopup) base.PopupForm;
      }
    }

    public StackLayoutElement SpinButtonsStackLayout
    {
      get
      {
        return this.spinButtonsStackLayout;
      }
    }

    public Size PopupMinSize
    {
      get
      {
        return (Size) this.GetValue(RadTimePickerElement.PopupMinSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTimePickerElement.PopupMinSizeProperty, (object) value);
      }
    }

    public string Format
    {
      get
      {
        return this.format;
      }
      set
      {
        this.format = value;
        this.MaskedEditBox.Mask = this.format;
      }
    }

    public int RowHeight
    {
      get
      {
        return this.PopupContentElement.RowHeight;
      }
      set
      {
        this.PopupContentElement.RowHeight = value;
      }
    }

    public int ColumnsCount
    {
      get
      {
        return this.PopupContentElement.ColumnsCount;
      }
      set
      {
        this.PopupContentElement.ColumnsCount = value;
      }
    }

    public int HeadersHeight
    {
      get
      {
        return this.PopupContentElement.HeadersHeight;
      }
      set
      {
        this.PopupContentElement.HeadersHeight = value;
      }
    }

    public int ButtonPanelHeight
    {
      get
      {
        return this.PopupContentElement.ButtonPanelHeight;
      }
      set
      {
        this.PopupContentElement.ButtonPanelHeight = value;
      }
    }

    public int TableWidth
    {
      get
      {
        return this.PopupContentElement.TableWidth;
      }
      set
      {
        this.PopupContentElement.TableWidth = value;
      }
    }

    public TimeTables TimeTables
    {
      get
      {
        return this.PopupContentElement.TimeTables;
      }
      set
      {
        this.oldPopupSize = Size.Empty;
        if (value == TimeTables.HoursAndMinutesInOneTable)
        {
          this.PopupContentElement.Step = 30;
          this.oldStep = this.step;
          this.step = 30;
        }
        else
        {
          this.step = this.oldStep;
          this.PopupContentElement.Step = this.Step;
        }
        this.PopupContentElement.TimeTables = value;
      }
    }

    public bool ShowSpinButtons
    {
      get
      {
        return this.showSpinButtons;
      }
      set
      {
        this.showSpinButtons = value;
        if (!value)
          this.spinButtonsStackLayout.Visibility = ElementVisibility.Collapsed;
        else
          this.spinButtonsStackLayout.Visibility = ElementVisibility.Visible;
      }
    }

    public ClockPosition ClockPosition
    {
      get
      {
        return this.PopupContentElement.ClockPosition;
      }
      set
      {
        this.oldPopupSize = Size.Empty;
        this.PopupContentElement.ClockPosition = value;
        if (value == ClockPosition.HideClock)
        {
          this.ClockBeforeTables = true;
          this.PopupContentElement.HideClock();
        }
        else
        {
          this.PopupContentElement.ShowClock();
          this.ClockBeforeTables = value == ClockPosition.ClockBeforeTables;
        }
      }
    }

    public CultureInfo Culture
    {
      get
      {
        if (this.culture == null)
          this.culture = CultureInfo.CurrentCulture;
        return this.culture;
      }
      set
      {
        this.culture = value;
        object obj = this.MaskedEditBox.Value;
        this.MaskedEditBox.Culture = value;
        this.MaskedEditBox.Value = obj;
      }
    }

    public int Step
    {
      get
      {
        return this.step;
      }
      set
      {
        this.step = value;
        this.PopupContentElement.Step = value;
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool ReadOnly
    {
      get
      {
        return this.MaskedEditBox.TextBoxItem.ReadOnly;
      }
      set
      {
        this.MaskedEditBox.TextBoxItem.ReadOnly = value;
        this.PopupContentElement.ReadOnly = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the RadDropDownList will be animated when displaying.")]
    public bool DropDownAnimationEnabled
    {
      get
      {
        if (this.PopupForm != null)
          return this.PopupForm.AnimationEnabled;
        return false;
      }
      set
      {
        this.PopupForm.AnimationEnabled = value;
      }
    }

    [RadDescription("NullText", typeof (RadTextBoxItem))]
    [Localizable(true)]
    [RadDefaultValue("NullText", typeof (RadTextBoxItem))]
    [Category("Behavior")]
    public string NullText
    {
      get
      {
        return this.MaskedEditBox.TextBoxItem.TextBoxControl.NullText;
      }
      set
      {
        this.MaskedEditBox.TextBoxItem.TextBoxControl.NullText = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ClockBeforeTables
    {
      get
      {
        return this.PopupContentElement.ClockBeforeTables;
      }
      set
      {
        this.oldPopupSize = Size.Empty;
        this.PopupContentElement.ClockBeforeTables = value;
        this.PopupContentElement.ChangeClockAndTableLocation(value);
      }
    }

    [Localizable(true)]
    [DefaultValue("Minutes")]
    public virtual string MinutesHeaderText
    {
      get
      {
        return LocalizationProvider<RadTimePickerLocalizationProvider>.CurrentProvider.GetLocalizedString(nameof (MinutesHeaderText));
      }
      set
      {
        this.minutesText = value;
      }
    }

    [DefaultValue("Hours")]
    [Localizable(true)]
    public string HourHeaderText
    {
      get
      {
        return LocalizationProvider<RadTimePickerLocalizationProvider>.CurrentProvider.GetLocalizedString(nameof (HourHeaderText));
      }
      set
      {
        this.hourText = value;
      }
    }

    public override object Value
    {
      get
      {
        DateTime result = DateTime.Now;
        if (this.editBox.Value == null)
          return (object) null;
        if (this.editBox.Value is DateTime)
          return this.editBox.Value;
        if (DateTime.TryParse(this.editBox.Value.ToString(), out result))
          return (object) result;
        return (object) result;
      }
      set
      {
        DateTime? time1 = this.GetTime(value);
        DateTime? time2 = this.GetTime(this.editBox.Value);
        if ((time1.HasValue != time2.HasValue ? 0 : (!time1.HasValue ? 1 : (time1.GetValueOrDefault() == time2.GetValueOrDefault() ? 1 : 0))) != 0)
          return;
        this.editBox.Value = value;
        if (value != null && value != DBNull.Value)
        {
          DateTime dateTime = (DateTime) this.Value;
          this.editBox.MaxDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, this.MaxValue.Hour, this.MaxValue.Minute, this.MaxValue.Second);
          this.editBox.MinDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, this.MinValue.Hour, this.MinValue.Minute, this.MinValue.Second);
        }
        else if (value != null && value != DBNull.Value)
        {
          this.PopupContentElement.SetClockTime((DateTime) value);
        }
        else
        {
          this.PopupContentElement.SetClockTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
          this.MaskedEditBox.TextBoxItem.Text = "";
        }
      }
    }

    public bool TwoTablesForTime
    {
      get
      {
        return this.PopupContentElement.TwoTablesForTime;
      }
      set
      {
        this.oldPopupSize = Size.Empty;
        this.PopupContentElement.TwoTablesForTime = value;
      }
    }

    public DateTime MinValue
    {
      get
      {
        return this.PopupContentElement.MinValue;
      }
      set
      {
        this.PopupContentElement.MinValue = value;
        this.editBox.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, value.Hour, value.Minute, value.Second);
      }
    }

    public DateTime MaxValue
    {
      get
      {
        return this.PopupContentElement.MaxValue;
      }
      set
      {
        this.PopupContentElement.MaxValue = value;
        this.editBox.MaxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, value.Hour, value.Minute, value.Second);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override RadItem EditorElement
    {
      get
      {
        return base.EditorElement;
      }
      set
      {
        base.EditorElement = value;
      }
    }

    public event TimeCellFormattingEventHandler TimeCellFormatting;

    [Category("Action")]
    [Description(" Occurs when the editing value is changing.")]
    public event CancelEventHandler ValueChanging;

    [Category("Action")]
    [Description("Occurs when the editing value has been changed")]
    public new event EventHandler ValueChanged;

    public new event MouseEventHandler MouseUp;

    public new event MouseEventHandler MouseWheel;

    public new event EventHandler Click;

    public new event KeyEventHandler KeyDown;

    public new event KeyPressEventHandler KeyPress;

    public new event KeyEventHandler KeyUp;

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.PopupContentElement.ChangeClockAndTableLocation(this.ClockBeforeTables);
    }

    protected virtual void OnPopupClosing(object sender, RadPopupClosingEventArgs args)
    {
      args.Cancel = args.CloseReason == RadPopupCloseReason.Mouse && this.dropDownButton.IsMouseOver;
    }

    protected virtual void OnPopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      int num1 = (int) this.dropDownButton.SetValue(RadTimePickerElement.IsDropDownShownProperty, (object) false);
      int num2 = (int) this.SetValue(RadTimePickerElement.IsDropDownShownProperty, (object) false);
      this.oldPopupSize = this.PopupForm.Size;
    }

    protected virtual void OnPopupOpened(object sender, EventArgs e)
    {
      this.PreparePopupContentForOpen();
      int num1 = (int) this.dropDownButton.SetValue(RadTimePickerElement.IsDropDownShownProperty, (object) true);
      int num2 = (int) this.SetValue(RadTimePickerElement.IsDropDownShownProperty, (object) true);
      if (this.PopupContentElement == null || this.PopupContentElement.FooterPanel == null || this.PopupContentElement.FooterPanel.Visibility == ElementVisibility.Collapsed)
        return;
      RadControl radControl = this.ElementTree == null || this.ElementTree.Control == null ? (RadControl) null : this.ElementTree.Control as RadControl;
      if (radControl == null || !TelerikHelper.IsMaterialTheme(radControl.ThemeName))
        return;
      this.PopupContentElement.FooterPanel.Padding = new Padding(0, 0, 0, 5);
    }

    protected virtual void OnValueChanging(object sender, CancelEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    protected virtual void OnValueChanged(object sender, EventArgs e)
    {
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "ValueChanged", this.Value);
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      bool newValue = (bool) e.NewValue;
      if (this.Shape != null)
        this.Shape.IsRightToLeft = newValue;
      if (this.DropDownButton != null && this.DropDownButton.Shape != null)
        this.DropDownButton.Shape.IsRightToLeft = newValue;
      if (this.DownButton != null && this.DownButton.Shape != null)
        this.DownButton.Shape.IsRightToLeft = newValue;
      if (this.UpButton != null && this.UpButton.Shape != null)
        this.UpButton.Shape.IsRightToLeft = newValue;
      if (this.SpinButtonsStackLayout == null || this.SpinButtonsStackLayout.Shape == null)
        return;
      this.SpinButtonsStackLayout.Shape.IsRightToLeft = newValue;
    }

    protected virtual void MaskEditBox_MouseUp(object sender, MouseEventArgs e)
    {
      if (this.MouseUp == null)
        return;
      this.MouseUp((object) this, e);
    }

    protected virtual void MaskEditBox_MouseWheel(object sender, MouseEventArgs e)
    {
      if (this.MouseWheel == null)
        return;
      this.MouseWheel((object) this, e);
    }

    protected virtual void MaskEditBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.KeyDown == null)
        return;
      this.KeyDown((object) this, e);
    }

    protected virtual void MaskEditBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (this.KeyPress == null)
        return;
      this.KeyPress((object) this, e);
    }

    private void MaskEditBox_KeyUp(object sender, KeyEventArgs e)
    {
      if (this.KeyUp == null)
        return;
      this.KeyUp((object) this, e);
    }

    private void RadTimePickerLocalizationProvider_CurrentProviderChanged(
      object sender,
      EventArgs e)
    {
      this.UpdateHeadersText();
    }

    private void PopupContentElement_TimeCellFormatting(
      object sender,
      TimeCellFormattingEventArgs e)
    {
      if (this.TimeCellFormatting == null)
        return;
      this.TimeCellFormatting(sender, e);
    }

    protected virtual void MaskEditBox_Click(object sender, EventArgs e)
    {
      if (this.Click == null)
        return;
      this.Click((object) this, e);
    }

    protected virtual void arrowButton_Click(object sender, EventArgs e)
    {
      if (this.ReadOnly)
        return;
      this.TooglePopupState();
    }

    private void buttonDown_Click(object sender, EventArgs e)
    {
      if (this.editBox.TextBoxItem.ReadOnly)
        return;
      this.editBox.Focus();
      this.editBox.Down();
    }

    private void buttonUp_Click(object sender, EventArgs e)
    {
      if (this.editBox.TextBoxItem.ReadOnly)
        return;
      this.editBox.Focus();
      this.editBox.Up();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(this.GetClientRectangle(availableSize).Size);
      sizeF.Width = Math.Min(sizeF.Width, availableSize.Width);
      sizeF.Height = Math.Min(sizeF.Height, availableSize.Height);
      return sizeF;
    }

    protected virtual void UpdateHeadersText()
    {
      this.PopupContentElement.FooterPanel.ButtonElement.Text = LocalizationProvider<RadTimePickerLocalizationProvider>.CurrentProvider.GetLocalizedString("CloseButtonText");
      this.PopupContentElement.PrepareContent();
    }

    private void PreparePopupContentForOpen()
    {
      (this.MaskedEditBox.Provider as MaskDateTimeProvider).ValidateRange();
      if (this.MaskedEditBox.Value != null)
        this.PopupContentElement.SetClockTime((DateTime) this.MaskedEditBox.Value);
      else
        this.PopupContentElement.SetClockTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
      this.PopupContentElement.ClearSelection();
      this.PopupContentElement.PrepareContent();
      this.PopupContentElement.SetSelected(this.GetTime(this.MaskedEditBox.Value));
    }

    private DateTime? GetTime(object value)
    {
      if (value == null || value == DBNull.Value)
        return new DateTime?();
      return new DateTime?((DateTime) value);
    }

    protected override Size GetInitialPopupSize()
    {
      return new Size(1000, 1000);
    }

    protected override Size GetPopupSize(RadPopupControlBase popup, bool measure)
    {
      Size empty = Size.Empty;
      Size size;
      if ((SizeF) this.oldPopupSize != SizeF.Empty)
      {
        size = this.oldPopupSize;
      }
      else
      {
        this.PopupContentElement.InvalidateMeasure();
        this.PopupContentElement.Measure(new SizeF(float.PositiveInfinity, float.PositiveInfinity));
        size = this.PopupContentElement.DesiredSize.ToSize();
        if (this.TwoTablesForTime && !this.PopupContentElement.IsAmPmMode())
          size.Height += this.PopupContentElement.RowHeight * this.PopupContentElement.MinutesTable.ContentElement.Rows.Count;
      }
      size.Width = Math.Max(this.PopupMinSize.Width, size.Width);
      size.Height = Math.Max(this.PopupMinSize.Height, size.Height);
      return size;
    }

    public void CloseOwnerPopup()
    {
      this.ClosePopup();
    }
  }
}
