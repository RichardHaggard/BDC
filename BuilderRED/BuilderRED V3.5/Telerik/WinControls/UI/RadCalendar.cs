// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalendar
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
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(true)]
  [DefaultEvent("SelectionChanged")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Description("Enables the user to select a date from a highly customizable calendar")]
  [TelerikToolboxCategory("Editors")]
  [DefaultProperty("SelectedDate")]
  public class RadCalendar : RadControl
  {
    private System.Drawing.ContentAlignment cellAlign = System.Drawing.ContentAlignment.MiddleCenter;
    private DayNameFormat dayNameFormat = DayNameFormat.FirstLetter;
    private int multiViewRows = 1;
    private int multiViewColumns = 1;
    private bool showNavigationButtons = true;
    private bool showFastNavigationButtons = true;
    private bool showColumnHeaders = true;
    private bool showOtherMonthsDays = true;
    private string rowHeaderText = "";
    private string columnHeaderText = "";
    private string viewSelectorText = "x";
    private int fastNavigationStep = 3;
    private bool showHeader = true;
    private bool allowSelect = true;
    private Padding cellPadding = Padding.Empty;
    private Padding cellMargin = Padding.Empty;
    private int cellVerticalSpacing = 1;
    private int cellHorizontalSpacing = 1;
    private static readonly object dayRenderEventKey = new object();
    private static readonly object headerCellRenderEventKey = new object();
    private static readonly object selectionChangedEventKey = new object();
    private static readonly object viewChangedEventKey = new object();
    private static readonly object selectionChangingEventKey = new object();
    private static readonly object viewChangingEventKey = new object();
    private RadCalendarElement calendarElement;
    private CultureInfo cultureInfo;
    private string cellDayFormat;
    private int? singleViewRows;
    private int? singleViewColumns;
    private bool rightToLeft;
    private MonthLayout? monthLayout;
    private DateTime? rangeMaxDate;
    private DateTime? rangeMinDate;
    private FirstDayOfWeek? firstDayOfWeek;
    private DateTime? focusedDate;
    private bool readOnly;
    private DateTimeCollection selectedDates;
    private bool? enableNavigation;
    private bool showFooter;
    private string navigationPrevText;
    private string navigationNextText;
    private string fastNavigationPrevText;
    private string fastNavigationNextText;
    private string navigationPrevToolTip;
    private string navigationNextToolTip;
    private string fastNavigationPrevToolTip;
    private string fastNavigationNextToolTip;
    private string titleFormat;
    private string cellToolTipFormat;
    private string dateRangeSeparator;
    private bool showRowHeaders;
    private bool allowViewSelector;
    private Image rowHeaderImage;
    private Image columnHeaderImage;
    private Image viewSelectorImage;
    private Orientation orientation;
    private CalendarDayCollection specialDays;
    private bool allowMultiSelect;
    private bool allowMultiView;
    private CalendarView calendarView;
    private bool allowRowHeaderSelectors;
    private bool allowColumnHeaderSelectors;
    private bool showViewSelector;
    private int currentViewColumn;
    private int currentViewRow;
    private bool showViewHeader;
    private bool? allowFastNavigation;
    private bool updating;

    public RadCalendar()
    {
      this.CellAlign = System.Drawing.ContentAlignment.MiddleCenter;
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadCalendarAccessibleObject(this);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(257, 227));
      }
    }

    [Category("General View Settings")]
    [Description("The row in the multi-view table where the focused date is positioned.")]
    [NotifyParentProperty(true)]
    [DefaultValue(0)]
    public int CurrentViewRow
    {
      get
      {
        return this.currentViewRow;
      }
      set
      {
        if (this.currentViewRow == value)
          return;
        this.currentViewRow = value;
        this.calendarView = this.multiViewColumns > 1 || this.multiViewRows > 1 ? (CalendarView) new MultiMonthView(this) : (CalendarView) new MonthView(this);
        this.calendarView.Initialize();
        this.ReInitializeCalendarElement();
        this.OnNotifyPropertyChanged(nameof (CurrentViewRow));
      }
    }

    [Description("The column in the multi-view table where the focused date is positioned.")]
    [NotifyParentProperty(true)]
    [Category("General View Settings")]
    [DefaultValue(0)]
    public int CurrentViewColumn
    {
      get
      {
        return this.currentViewColumn;
      }
      set
      {
        if (this.currentViewColumn == value)
          return;
        this.currentViewColumn = value;
        this.calendarView = this.multiViewColumns > 1 || this.multiViewRows > 1 ? (CalendarView) new MultiMonthView(this) : (CalendarView) new MonthView(this);
        this.calendarView.Initialize();
        this.ReInitializeCalendarElement();
        this.OnNotifyPropertyChanged("CurrentViewRowColumn");
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadCalendarElement CalendarElement
    {
      get
      {
        return this.calendarElement;
      }
    }

    [Browsable(false)]
    public new Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    [Description("Specifies the navigation mode that will be used when user click on header element.Zoom navigation mode is not supporting in MultipleView of RadCalendar.")]
    [DefaultValue(HeaderNavigationMode.Popup)]
    [Category("Appearance")]
    public HeaderNavigationMode HeaderNavigationMode
    {
      get
      {
        return this.CalendarElement.CalendarNavigationElement.NavigationMode;
      }
      set
      {
        if (this.CalendarElement.CalendarNavigationElement.NavigationMode == value || value == HeaderNavigationMode.Zoom && this.AllowMultipleView)
          return;
        this.CalendarElement.CalendarNavigationElement.NavigationMode = value;
      }
    }

    [DefaultValue(ZoomLevel.Days)]
    [Browsable(false)]
    public ZoomLevel ZoomLevel
    {
      get
      {
        return this.CalendarElement.ZoomLevel;
      }
      set
      {
        this.CalendarElement.ZoomLevel = value;
        this.CalendarElement.CalendarNavigationElement.SetText();
      }
    }

    public event CalendarNavigatingEventHandler Navigating;

    protected internal virtual void OnNavigating(CalendarNavigatingEventArgs args)
    {
      if (this.Navigating == null)
        return;
      this.Navigating((object) this, args);
    }

    public event CalendarNavigatedEventHandler Navigated;

    protected internal virtual void OnNavigated(CalendarNavigatedEventArgs args)
    {
      if (this.Navigated != null)
        this.Navigated((object) this, args);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Navigated");
    }

    public event CalendarZoomChangingEventHandler ZoomChanging;

    protected internal virtual void OnZoomChanging(CalendarZoomChangingEventArgs args)
    {
      if (this.ZoomChanging != null)
        this.ZoomChanging((object) this, args);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Zoom");
    }

    public event CalendarZoomChangedEventHandler ZoomChanged;

    protected internal virtual void OnZoomChanged(CalendarZoomChangedEventArgs args)
    {
      if (this.ZoomChanged != null)
        this.ZoomChanged((object) this, args);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Zoom");
    }

    public event SelectionEventHandler SelectionChanging
    {
      add
      {
        this.Events.AddHandler(RadCalendar.selectionChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCalendar.selectionChangingEventKey, (Delegate) value);
      }
    }

    internal SelectionEventArgs CallOnSelectionChanging(
      DateTimeCollection dates,
      List<DateTime> newDates)
    {
      this.ExcludeNewDatesFromDates(dates, newDates);
      return this.OnSelectionChanging(dates, newDates);
    }

    private void ExcludeNewDatesFromDates(DateTimeCollection dates, List<DateTime> newDates)
    {
      foreach (DateTime date in dates)
      {
        while (newDates.Contains(date))
          newDates.Remove(date);
      }
    }

    protected virtual SelectionEventArgs OnSelectionChanging(
      DateTimeCollection dates,
      List<DateTime> newDates)
    {
      SelectionEventHandler selectionEventHandler = (SelectionEventHandler) this.Events[RadCalendar.selectionChangingEventKey];
      SelectionEventArgs e = new SelectionEventArgs(dates, newDates);
      if (selectionEventHandler != null)
        selectionEventHandler((object) this, e);
      return e;
    }

    public event EventHandler SelectionChanged
    {
      add
      {
        this.Events.AddHandler(RadCalendar.selectionChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCalendar.selectionChangedEventKey, (Delegate) value);
      }
    }

    internal void CallOnSelectionChanged()
    {
      this.OnSelectionChanged();
    }

    protected virtual void OnSelectionChanged()
    {
      this.InvalidateCalendarSelection();
      EventHandler eventHandler = (EventHandler) this.Events[RadCalendar.selectionChangedEventKey];
      if (eventHandler != null)
        eventHandler((object) this, new EventArgs());
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "SelectionChanged");
    }

    public event RenderElementEventHandler ElementRender
    {
      add
      {
        this.Events.AddHandler(RadCalendar.dayRenderEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCalendar.dayRenderEventKey, (Delegate) value);
      }
    }

    internal void CallOnElementRender(
      LightVisualElement cell,
      RadCalendarDay day,
      CalendarView currentView)
    {
      this.OnElementRender(cell, day, currentView);
    }

    protected virtual void OnElementRender(
      LightVisualElement cell,
      RadCalendarDay day,
      CalendarView view)
    {
      RenderElementEventHandler elementEventHandler = (RenderElementEventHandler) this.Events[RadCalendar.dayRenderEventKey];
      if (elementEventHandler == null)
        return;
      elementEventHandler((object) this, new RenderElementEventArgs(cell, day, view));
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.CalendarElement.CalendarVisualElement.RefreshVisuals();
    }

    public event ViewChangingEventHandler ViewChanging
    {
      add
      {
        this.Events.AddHandler(RadCalendar.viewChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCalendar.viewChangingEventKey, (Delegate) value);
      }
    }

    internal ViewChangingEventArgs CallOnViewChanging(CalendarView view)
    {
      return this.OnViewChanging(view);
    }

    protected virtual ViewChangingEventArgs OnViewChanging(CalendarView view)
    {
      ViewChangingEventHandler changingEventHandler = (ViewChangingEventHandler) this.Events[RadCalendar.viewChangingEventKey];
      ViewChangingEventArgs e = new ViewChangingEventArgs(view);
      if (changingEventHandler != null)
        changingEventHandler((object) this, e);
      return e;
    }

    public event EventHandler ViewChanged
    {
      add
      {
        this.Events.AddHandler(RadCalendar.viewChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCalendar.viewChangedEventKey, (Delegate) value);
      }
    }

    internal void CallOnViewChanged()
    {
      this.OnViewChanged();
    }

    protected virtual void OnViewChanged()
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadCalendar.viewChangedEventKey];
      if (eventHandler != null)
        eventHandler((object) this, new EventArgs());
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "ViewChanged");
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      System.Type type = element.GetType();
      if ((object) type == (object) typeof (RadButtonElement) || (object) type == (object) typeof (RadRepeatButtonElement) || (object) type == (object) typeof (RadCalendarElement))
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    [NotifyParentProperty(true)]
    [Category("Localization Settings")]
    [DefaultValue(DayNameFormat.FirstLetter)]
    [Description("Specifies the display format for the days of the week on RadCalendar.")]
    public DayNameFormat DayNameFormat
    {
      get
      {
        if (this.dayNameFormat != DayNameFormat.FirstLetter)
          return this.dayNameFormat;
        return DayNameFormat.FirstLetter;
      }
      set
      {
        if (this.dayNameFormat == value)
          return;
        this.dayNameFormat = value;
        this.OnNotifyPropertyChanged(nameof (DayNameFormat));
      }
    }

    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Localization Settings")]
    [Description("Gets the default DateTimeFormatInfo instance as specified by the default culture.")]
    public DateTimeFormatInfo DateTimeFormat
    {
      get
      {
        return this.Culture.DateTimeFormat;
      }
    }

    [Description("Gets or sets the culture supported by this calendar.")]
    [TypeConverter(typeof (CultureInfoConverter))]
    [Category("Localization Settings")]
    [NotifyParentProperty(true)]
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

    private bool ShouldSerializeCulture()
    {
      if (!CultureInfo.CurrentCulture.Equals((object) this.Culture))
        return !this.Culture.Equals((object) new CultureInfo(""));
      return false;
    }

    [Browsable(false)]
    [Description("Gets the default System.Globalization.Calendar instance as speified by the default culture.")]
    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Localization Settings")]
    public Calendar CurrentCalendar
    {
      get
      {
        if (this.Culture.DateTimeFormat.Calendar != null)
          return this.Culture.DateTimeFormat.Calendar;
        return DateTimeFormatInfo.CurrentInfo.Calendar;
      }
    }

    [Localizable(true)]
    [Description("Gets or sets the formatting string that will be applied to the days in the calendar.")]
    [Category("Localization Settings")]
    [NotifyParentProperty(true)]
    [DefaultValue("%d")]
    public string DayCellFormat
    {
      get
      {
        if (string.IsNullOrEmpty(this.cellDayFormat))
          return "%d";
        return this.cellDayFormat;
      }
      set
      {
        if (!(this.cellDayFormat != value))
          return;
        this.cellDayFormat = value;
        this.OnNotifyPropertyChanged(nameof (DayCellFormat));
      }
    }

    [NotifyParentProperty(true)]
    [Description("Specifies the day to display as the first day of the week.")]
    [Category("Localization Settings")]
    [DefaultValue(FirstDayOfWeek.Default)]
    public FirstDayOfWeek FirstDayOfWeek
    {
      get
      {
        if (this.firstDayOfWeek.HasValue)
          return this.firstDayOfWeek.Value;
        return FirstDayOfWeek.Default;
      }
      set
      {
        switch (value)
        {
          case FirstDayOfWeek.Sunday:
          case FirstDayOfWeek.Monday:
          case FirstDayOfWeek.Tuesday:
          case FirstDayOfWeek.Wednesday:
          case FirstDayOfWeek.Thursday:
          case FirstDayOfWeek.Friday:
          case FirstDayOfWeek.Saturday:
          case FirstDayOfWeek.Default:
            FirstDayOfWeek? firstDayOfWeek1 = this.firstDayOfWeek;
            FirstDayOfWeek firstDayOfWeek2 = value;
            if ((firstDayOfWeek1.GetValueOrDefault() != firstDayOfWeek2 ? 1 : (!firstDayOfWeek1.HasValue ? 1 : 0)) == 0)
              break;
            this.firstDayOfWeek = new FirstDayOfWeek?(value);
            this.OnNotifyPropertyChanged(nameof (FirstDayOfWeek));
            break;
          default:
            throw new ArgumentOutOfRangeException("FirstDayOfWeek value");
        }
      }
    }

    [Localizable(true)]
    [Category("Title Settings")]
    [DefaultValue("MMMM yyyy")]
    [Description("Gets or sets the format string that is applied to the calendar title.")]
    [NotifyParentProperty(true)]
    public string TitleFormat
    {
      get
      {
        if (string.IsNullOrEmpty(this.titleFormat))
          return "MMMM yyyy";
        return this.titleFormat;
      }
      set
      {
        if (!(this.titleFormat != value))
          return;
        this.titleFormat = value;
        this.OnNotifyPropertyChanged(nameof (TitleFormat));
      }
    }

    [Category("Title Settings")]
    [NotifyParentProperty(true)]
    [DefaultValue("dddd, MMMM dd, yyyy")]
    [Description("Gets or sets the format string that is applied to the days cells tooltip.")]
    [Localizable(true)]
    public string CellToolTipFormat
    {
      get
      {
        if (string.IsNullOrEmpty(this.cellToolTipFormat))
          return "dddd, MMMM dd, yyyy";
        return this.cellToolTipFormat;
      }
      set
      {
        if (!(this.cellToolTipFormat != value))
          return;
        this.cellToolTipFormat = value;
        this.OnNotifyPropertyChanged(nameof (CellToolTipFormat));
      }
    }

    [Category("Title Settings")]
    [NotifyParentProperty(true)]
    [Description("Gets or sets the separator string that will be put between start and end months in a multi view title.")]
    [Localizable(true)]
    [DefaultValue(" - ")]
    public string DateRangeSeparator
    {
      get
      {
        if (string.IsNullOrEmpty(this.dateRangeSeparator))
          return " - ";
        return this.dateRangeSeparator;
      }
      set
      {
        if (!(this.dateRangeSeparator != value))
          return;
        this.dateRangeSeparator = value;
        this.OnNotifyPropertyChanged(nameof (DateRangeSeparator));
      }
    }

    [Category("General View Settings")]
    [DefaultValue(6)]
    [RefreshProperties(RefreshProperties.All)]
    [Description("The the count of rows to be displayed by a single CalendarView")]
    [NotifyParentProperty(true)]
    public int Rows
    {
      get
      {
        if (this.singleViewRows.HasValue)
          return this.singleViewRows.Value;
        return 6;
      }
      set
      {
        int? singleViewRows = this.singleViewRows;
        int num = value;
        if ((singleViewRows.GetValueOrDefault() != num ? 1 : (!singleViewRows.HasValue ? 1 : 0)) == 0 || !this.CalculateValidTableLayout(value, false))
          return;
        this.singleViewRows = new int?(value);
        this.OnNotifyPropertyChanged(nameof (Rows));
      }
    }

    [DefaultValue(7)]
    [Category("General View Settings")]
    [NotifyParentProperty(true)]
    [Description("The the count of columns to be displayed by a single CalendarView")]
    [RefreshProperties(RefreshProperties.All)]
    public int Columns
    {
      get
      {
        if (this.singleViewColumns.HasValue)
          return this.singleViewColumns.Value;
        return 7;
      }
      set
      {
        int? singleViewColumns = this.singleViewColumns;
        int num = value;
        if ((singleViewColumns.GetValueOrDefault() != num ? 1 : (!singleViewColumns.HasValue ? 1 : 0)) == 0 || !this.CalculateValidTableLayout(value, true))
          return;
        this.singleViewColumns = new int?(value);
        this.OnNotifyPropertyChanged(nameof (Columns));
      }
    }

    [NotifyParentProperty(true)]
    [Category("General View Settings")]
    [DefaultValue(7)]
    [Description("Gets the today button of the footer element")]
    public RadButtonElement TodayButton
    {
      get
      {
        return this.calendarElement.CalendarStatusElement.TodayButton;
      }
    }

    [Category("General View Settings")]
    [NotifyParentProperty(true)]
    [DefaultValue(7)]
    [Description("Gets the clear button of the footer element")]
    public RadButtonElement ClearButton
    {
      get
      {
        return this.calendarElement.CalendarStatusElement.ClearButton;
      }
    }

    [DefaultValue(17)]
    [Category("Behavior")]
    [NotifyParentProperty(true)]
    [Description("The Width applied to a Header")]
    public int HeaderWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt(this.CalendarElement.HeaderWidth, this.RootElement.DpiScaleFactor);
      }
      set
      {
        this.CalendarElement.HeaderWidth = value;
      }
    }

    [NotifyParentProperty(true)]
    [Category("Behavior")]
    [DefaultValue(17)]
    [Description("The Height applied to a Header")]
    public int HeaderHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt(this.CalendarElement.HeaderHeight, this.RootElement.DpiScaleFactor);
      }
      set
      {
        this.CalendarElement.HeaderHeight = value;
      }
    }

    [NotifyParentProperty(true)]
    [Category("General View Settings")]
    [DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
    [Description("Specifies the horizonal alignment of the day cells in the calendar")]
    public System.Drawing.ContentAlignment CellAlign
    {
      get
      {
        return this.cellAlign;
      }
      set
      {
        if (this.cellAlign == value)
          return;
        this.cellAlign = value;
        this.OnNotifyPropertyChanged(nameof (CellAlign));
      }
    }

    [Category("General View Settings")]
    [NotifyParentProperty(true)]
    [DefaultValue(1)]
    [Description("Gets or sets the number of month rows in a multi view calendar.")]
    public int MultiViewRows
    {
      get
      {
        return this.multiViewRows;
      }
      set
      {
        if (this.multiViewRows == value || !this.AllowMultipleView)
          return;
        this.multiViewRows = value;
        this.OnNotifyPropertyChanged(nameof (MultiViewRows));
        this.calendarView = this.multiViewColumns > 1 || this.multiViewRows > 1 ? (CalendarView) new MultiMonthView(this) : (CalendarView) new MonthView(this);
        this.calendarView.Initialize();
        this.ReInitializeCalendarElement();
      }
    }

    [DefaultValue(1)]
    [NotifyParentProperty(true)]
    [Category("General View Settings")]
    [Description("Gets or sets the number of month columns in a multi view calendar.")]
    public int MultiViewColumns
    {
      get
      {
        return this.multiViewColumns;
      }
      set
      {
        if (this.multiViewColumns == value || !this.AllowMultipleView)
          return;
        this.multiViewColumns = value;
        this.OnNotifyPropertyChanged(nameof (MultiViewColumns));
        this.calendarView = this.multiViewColumns > 1 || this.multiViewRows > 1 ? (CalendarView) new MultiMonthView(this) : (CalendarView) new MonthView(this);
        this.calendarView.Initialize();
        this.ReInitializeCalendarElement();
      }
    }

    [DefaultValue(typeof (DateTime), "12/30/2099")]
    [NotifyParentProperty(true)]
    [Category("Dates Management")]
    [Description("Gets or sets the maximal date valid for selection by Telerik RadCalendar. Must be interpreted as the Higher bound of the valid dates range available for selection. Telerik RadCalendar will not allow navigation or selection past this date.")]
    public DateTime RangeMaxDate
    {
      get
      {
        if (this.rangeMaxDate.HasValue)
          return this.rangeMaxDate.Value;
        return new DateTime(2099, 12, 30);
      }
      set
      {
        DateTime dateTime1 = RadCalendar.TruncateTimeComponent(value);
        DateTime? rangeMaxDate = this.rangeMaxDate;
        DateTime dateTime2 = value;
        if ((!rangeMaxDate.HasValue ? 1 : (rangeMaxDate.GetValueOrDefault() != dateTime2 ? 1 : 0)) == 0 || !(dateTime1 >= this.RangeMinDate))
          return;
        this.rangeMaxDate = new DateTime?(dateTime1);
        this.OnNotifyPropertyChanged(nameof (RangeMaxDate));
      }
    }

    [Category("Dates Management")]
    [NotifyParentProperty(true)]
    [DefaultValue(typeof (DateTime), "1/1/1900")]
    [Description("Gets or sets the minimal date valid for selection by Telerik RadCalendar. Must be interpreted as the Lower bound of the valid dates range available for selection. Telerik RadCalendar will not allow navigation or selection prior to this date.")]
    public DateTime RangeMinDate
    {
      get
      {
        if (this.rangeMinDate.HasValue)
          return this.rangeMinDate.Value;
        return new DateTime(1900, 1, 1);
      }
      set
      {
        DateTime dateTime1 = RadCalendar.TruncateTimeComponent(value);
        DateTime? rangeMinDate = this.rangeMinDate;
        DateTime dateTime2 = value;
        if ((!rangeMinDate.HasValue ? 1 : (rangeMinDate.GetValueOrDefault() != dateTime2 ? 1 : 0)) == 0 || !(dateTime1 <= this.RangeMaxDate))
          return;
        this.rangeMinDate = new DateTime?(dateTime1);
        this.OnNotifyPropertyChanged(nameof (RangeMinDate));
      }
    }

    [Description("Gets or sets a value indicating whether the calendar view is in read-only mode.")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public virtual bool ReadOnly
    {
      get
      {
        return this.readOnly;
      }
      set
      {
        if (this.readOnly == value)
          return;
        this.readOnly = value;
        this.OnNotifyPropertyChanged(nameof (ReadOnly));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Dates Management")]
    public DateTime SelectedDate
    {
      get
      {
        if (this.SelectedDates.Count > 0)
          return this.SelectedDates[0];
        return this.RangeMinDate;
      }
      set
      {
        if (!this.updating)
        {
          if (this.CallOnSelectionChanging(this.selectedDates, new List<DateTime>(1) { value }).Cancel)
            return;
        }
        this.selectedDates.BeginUpdate();
        if (!this.AllowMultipleSelect)
          this.SelectedDates.Clear();
        if (!this.SelectedDates.CanAdd(value))
        {
          this.selectedDates.EndUpdate();
        }
        else
        {
          if (!this.SelectedDates.Contains(value) && value <= this.RangeMaxDate && value >= this.RangeMinDate)
            this.SelectedDates.Insert(0, value);
          this.selectedDates.EndUpdate();
          if (!this.updating)
            this.OnSelectionChanged();
          this.OnNotifyPropertyChanged(nameof (SelectedDate));
        }
      }
    }

    [DefaultValue(typeof (DateTime), "1/1/1980")]
    [Category("Dates Management")]
    [NotifyParentProperty(true)]
    [Description("The date used by RadCalendar to determine the viewable area displayed")]
    public DateTime FocusedDate
    {
      get
      {
        DateTime dateTime = DateTime.Today;
        if (this.DesignMode)
          dateTime = new DateTime(1980, 1, 1);
        if (this.focusedDate.HasValue)
          dateTime = this.focusedDate.Value;
        if (dateTime > this.RangeMaxDate)
          dateTime = this.RangeMaxDate;
        else if (dateTime < this.RangeMinDate)
          dateTime = this.RangeMinDate;
        return dateTime;
      }
      set
      {
        if (this.ReadOnly && !this.DesignMode)
          return;
        DateTime dateTime1 = RadCalendar.TruncateTimeComponent(value);
        DateTime? focusedDate = this.focusedDate;
        DateTime dateTime2 = value;
        if ((!focusedDate.HasValue ? 1 : (focusedDate.GetValueOrDefault() != dateTime2 ? 1 : 0)) != 0)
        {
          this.focusedDate = new DateTime?(dateTime1);
          this.OnNotifyPropertyChanged(nameof (FocusedDate));
        }
        else
          this.SetFocusedDateView();
      }
    }

    internal bool HasValueFocusedData()
    {
      return this.focusedDate.HasValue;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Dates Management")]
    [NotifyParentProperty(true)]
    [Description("System.DateTime objects collection that represent the selected dates.")]
    public DateTimeCollection SelectedDates
    {
      get
      {
        if (this.selectedDates == null)
          this.selectedDates = new DateTimeCollection(this);
        return this.selectedDates;
      }
    }

    [Category("Navigation")]
    [NotifyParentProperty(true)]
    [DefaultValue(true)]
    [Description("Gets or sets whether navigating RadCalendar is allowed.")]
    public bool AllowNavigation
    {
      get
      {
        if (this.enableNavigation.HasValue)
          return this.enableNavigation.Value;
        return true;
      }
      set
      {
        bool? enableNavigation = this.enableNavigation;
        bool flag = value;
        if ((enableNavigation.GetValueOrDefault() != flag ? 1 : (!enableNavigation.HasValue ? 1 : 0)) == 0)
          return;
        this.enableNavigation = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (AllowNavigation));
      }
    }

    [DefaultValue(true)]
    [NotifyParentProperty(true)]
    [Category("Navigation")]
    [Description("Gets or sets whether the fast navigation in RadCalendar is allowed.")]
    public bool AllowFastNavigation
    {
      get
      {
        if (this.allowFastNavigation.HasValue)
          return this.allowFastNavigation.Value;
        return true;
      }
      set
      {
        bool? allowFastNavigation = this.allowFastNavigation;
        bool flag = value;
        if ((allowFastNavigation.GetValueOrDefault() != flag ? 1 : (!allowFastNavigation.HasValue ? 1 : 0)) == 0)
          return;
        this.allowFastNavigation = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (AllowFastNavigation));
      }
    }

    [DefaultValue("<")]
    [Localizable(true)]
    [Description("Gets or sets the text displayed for the previous month navigation control.")]
    [NotifyParentProperty(true)]
    [Category("Navigation Management")]
    public string NavigationPrevText
    {
      get
      {
        if (string.IsNullOrEmpty(this.navigationPrevText))
          return "<";
        return this.navigationPrevText;
      }
      set
      {
        if (!(this.navigationPrevText != value))
          return;
        this.navigationPrevText = value;
        this.OnNotifyPropertyChanged(nameof (NavigationPrevText));
        this.CalendarElement.PreviousButton.Text = value;
      }
    }

    [Category("Navigation Management")]
    [DefaultValue(">")]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    [Description("Gets or sets the text displayed for the next month navigation control.")]
    public string NavigationNextText
    {
      get
      {
        if (string.IsNullOrEmpty(this.navigationNextText))
          return ">";
        return this.navigationNextText;
      }
      set
      {
        if (!(this.navigationNextText != value))
          return;
        this.navigationNextText = value;
        this.OnNotifyPropertyChanged(nameof (NavigationNextText));
        this.CalendarElement.NextButton.Text = value;
      }
    }

    [Localizable(true)]
    [DefaultValue("<<")]
    [NotifyParentProperty(true)]
    [Category("Navigation Management")]
    [Description("Gets or sets the text displayed for the fast previous navigation control.")]
    public string FastNavigationPrevText
    {
      get
      {
        if (string.IsNullOrEmpty(this.fastNavigationPrevText))
          return "<<";
        return this.fastNavigationPrevText;
      }
      set
      {
        if (!(this.fastNavigationPrevText != value))
          return;
        this.fastNavigationPrevText = value;
        this.OnNotifyPropertyChanged(nameof (FastNavigationPrevText));
        this.CalendarElement.FastBackwardButton.Text = this.fastNavigationPrevText;
      }
    }

    [Localizable(true)]
    [DefaultValue(">>")]
    [NotifyParentProperty(true)]
    [Category("Navigation Management")]
    [Description("Gets or sets the text displayed for the fast next navigation control.")]
    public string FastNavigationNextText
    {
      get
      {
        if (string.IsNullOrEmpty(this.fastNavigationNextText))
          return ">>";
        return this.fastNavigationNextText;
      }
      set
      {
        if (!(this.fastNavigationNextText != value))
          return;
        this.fastNavigationNextText = value;
        this.OnNotifyPropertyChanged(nameof (FastNavigationNextText));
        this.CalendarElement.FastForwardButton.Text = value;
      }
    }

    [NotifyParentProperty(true)]
    [Category("Navigation Management")]
    [Description("Gets or sets the image that is displayed for the previous month navigation control.")]
    [Localizable(true)]
    [DefaultValue(null)]
    public Image NavigationPrevImage
    {
      get
      {
        return this.CalendarElement.CalendarNavigationElement.LeftButtonImage;
      }
      set
      {
        this.CalendarElement.CalendarNavigationElement.LeftButtonImage = value;
        this.OnNotifyPropertyChanged(nameof (NavigationPrevImage));
      }
    }

    [Category("Navigation Management")]
    [NotifyParentProperty(true)]
    [Description("Gets or sets the image that is displayed for the next month navigation control.")]
    [Localizable(true)]
    [DefaultValue(null)]
    public Image NavigationNextImage
    {
      get
      {
        return this.CalendarElement.CalendarNavigationElement.RightButtonImage;
      }
      set
      {
        this.CalendarElement.CalendarNavigationElement.RightButtonImage = value;
        this.OnNotifyPropertyChanged(nameof (NavigationNextImage));
      }
    }

    [Category("Navigation Management")]
    [NotifyParentProperty(true)]
    [DefaultValue(null)]
    [Description("Gets or sets the image that is displayed for the fast previous navigation control.")]
    [Localizable(true)]
    public Image FastNavigationPrevImage
    {
      get
      {
        return this.calendarElement.CalendarNavigationElement.FastLeftButtonImage;
      }
      set
      {
        this.calendarElement.CalendarNavigationElement.FastLeftButtonImage = value;
        this.OnNotifyPropertyChanged(nameof (FastNavigationPrevImage));
      }
    }

    [NotifyParentProperty(true)]
    [DefaultValue(null)]
    [Category("Navigation Management")]
    [Description("Gets or sets the image that is displayed for the fast next navigation control.")]
    [Localizable(true)]
    public Image FastNavigationNextImage
    {
      get
      {
        return this.calendarElement.CalendarNavigationElement.FastRightButtonImage;
      }
      set
      {
        this.calendarElement.CalendarNavigationElement.FastRightButtonImage = value;
        this.OnNotifyPropertyChanged(nameof (FastNavigationNextImage));
      }
    }

    [Localizable(true)]
    [NotifyParentProperty(true)]
    [Description("Gets or sets the text displayed as a tooltip for the previous month navigation control.")]
    [DefaultValue("<")]
    [Category("Navigation Management")]
    public string NavigationPrevToolTip
    {
      get
      {
        if (string.IsNullOrEmpty(this.navigationPrevToolTip))
          return "<";
        return this.navigationPrevToolTip;
      }
      set
      {
        if (!(this.navigationPrevToolTip != value))
          return;
        this.navigationPrevToolTip = value;
        this.OnNotifyPropertyChanged(nameof (NavigationPrevToolTip));
        this.CalendarElement.PreviousButton.ToolTipText = value;
      }
    }

    [NotifyParentProperty(true)]
    [Localizable(true)]
    [Category("Navigation Management")]
    [Description("Gets or sets the text displayed as a tooltip for the next month navigation control.")]
    [DefaultValue(">")]
    public string NavigationNextToolTip
    {
      get
      {
        if (string.IsNullOrEmpty(this.navigationNextToolTip))
          return ">";
        return this.navigationNextToolTip;
      }
      set
      {
        if (!(this.navigationNextToolTip != value))
          return;
        this.navigationNextToolTip = value;
        this.OnNotifyPropertyChanged(nameof (NavigationNextToolTip));
        this.CalendarElement.NextButton.ToolTipText = value;
      }
    }

    [NotifyParentProperty(true)]
    [Category("Navigation Management")]
    [DefaultValue("<<")]
    [Description("Gets or sets the text displayed as a tooltip for the fast navigation previous month control.")]
    [Localizable(true)]
    public string FastNavigationPrevToolTip
    {
      get
      {
        if (string.IsNullOrEmpty(this.fastNavigationPrevToolTip))
          return "<<";
        return this.fastNavigationPrevToolTip;
      }
      set
      {
        if (!(this.fastNavigationPrevToolTip != value))
          return;
        this.fastNavigationPrevToolTip = value;
        this.OnNotifyPropertyChanged(nameof (FastNavigationPrevToolTip));
        this.CalendarElement.FastBackwardButton.ToolTipText = value;
      }
    }

    [Description("Gets or sets the tooltip text displayed by the navigation button responsible for the fast forward navigation.")]
    [NotifyParentProperty(true)]
    [Category("Navigation Management")]
    [DefaultValue(">>")]
    [Localizable(true)]
    public string FastNavigationNextToolTip
    {
      get
      {
        if (string.IsNullOrEmpty(this.fastNavigationNextToolTip))
          return ">>";
        return this.fastNavigationNextToolTip;
      }
      set
      {
        if (!(this.fastNavigationNextToolTip != value))
          return;
        this.fastNavigationNextToolTip = value;
        this.OnNotifyPropertyChanged(nameof (FastNavigationNextToolTip));
        this.CalendarElement.FastForwardButton.ToolTipText = value;
      }
    }

    [NotifyParentProperty(true)]
    [Category("Title Settings")]
    [DefaultValue(System.Windows.Forms.VisualStyles.ContentAlignment.Center)]
    [Description("Gets or sets the horizontal alignment of the calendar title.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public System.Windows.Forms.VisualStyles.ContentAlignment TitleAlign
    {
      get
      {
        switch (this.CalendarElement.CalendarNavigationElement.TextAlignment)
        {
          case System.Drawing.ContentAlignment.TopLeft:
          case System.Drawing.ContentAlignment.MiddleLeft:
          case System.Drawing.ContentAlignment.BottomLeft:
            return System.Windows.Forms.VisualStyles.ContentAlignment.Left;
          case System.Drawing.ContentAlignment.TopCenter:
          case System.Drawing.ContentAlignment.MiddleCenter:
          case System.Drawing.ContentAlignment.BottomCenter:
            return System.Windows.Forms.VisualStyles.ContentAlignment.Center;
          case System.Drawing.ContentAlignment.TopRight:
          case System.Drawing.ContentAlignment.MiddleRight:
          case System.Drawing.ContentAlignment.BottomRight:
            return System.Windows.Forms.VisualStyles.ContentAlignment.Right;
          default:
            return System.Windows.Forms.VisualStyles.ContentAlignment.Center;
        }
      }
      set
      {
        switch (value)
        {
          case System.Windows.Forms.VisualStyles.ContentAlignment.Left:
            this.CalendarElement.CalendarNavigationElement.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            break;
          case System.Windows.Forms.VisualStyles.ContentAlignment.Center:
            this.CalendarElement.CalendarNavigationElement.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            break;
          case System.Windows.Forms.VisualStyles.ContentAlignment.Right:
            this.CalendarElement.CalendarNavigationElement.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            break;
        }
        this.OnNotifyPropertyChanged(nameof (TitleAlign));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Allows RadCalendar to render multiple months in a single view.")]
    [Bindable(false)]
    public bool AllowMultipleView
    {
      get
      {
        return this.allowMultiView;
      }
      set
      {
        if (this.allowMultiView == value)
          return;
        if (this.allowMultiView)
        {
          this.MultiViewRows = 1;
          this.MultiViewColumns = 1;
        }
        if (value && this.HeaderNavigationMode == HeaderNavigationMode.Zoom)
          this.HeaderNavigationMode = HeaderNavigationMode.Popup;
        this.allowMultiView = value;
        this.OnNotifyPropertyChanged("AllowMultiView");
      }
    }

    [Description("Allows the selection of dates. If not set, selection is forbidden, and if any dates are all ready selected, they are cleared.")]
    [NotifyParentProperty(true)]
    [Bindable(false)]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool AllowSelect
    {
      get
      {
        return this.allowSelect;
      }
      set
      {
        if (this.allowSelect == value)
          return;
        this.allowSelect = value;
        this.OnNotifyPropertyChanged(nameof (AllowSelect));
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Bindable(false)]
    [NotifyParentProperty(true)]
    [Description("Allows the selection of multiple dates. If not set, only a single date is selected, and if any dates are all ready selected, they are cleared.")]
    public bool AllowMultipleSelect
    {
      get
      {
        return this.allowMultiSelect;
      }
      set
      {
        if (this.allowMultiSelect == value)
          return;
        this.allowMultiSelect = value;
        this.OnNotifyPropertyChanged("AllowMultiSelect");
      }
    }

    [DefaultValue(true)]
    [NotifyParentProperty(true)]
    [Category("Header Settings")]
    [Description("Gets or sets whether the navigation buttons should be visible.")]
    public virtual bool ShowNavigationButtons
    {
      get
      {
        return this.showNavigationButtons;
      }
      set
      {
        if (this.showNavigationButtons == value)
          return;
        this.showNavigationButtons = value;
        this.OnNotifyPropertyChanged(nameof (ShowNavigationButtons));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Header Settings")]
    [DefaultValue(true)]
    [Description("Gets or sets whether the fast navigation buttons should be visible.")]
    public virtual bool ShowFastNavigationButtons
    {
      get
      {
        return this.showFastNavigationButtons;
      }
      set
      {
        if (this.showFastNavigationButtons == value)
          return;
        this.showFastNavigationButtons = value;
        this.OnNotifyPropertyChanged(nameof (ShowFastNavigationButtons));
      }
    }

    [DefaultValue(false)]
    [Category("Footer Settings")]
    [NotifyParentProperty(true)]
    [Description("Gets or sets whether RadCalendar will display a footer row.")]
    public virtual bool ShowFooter
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
        if (value)
        {
          this.calendarElement.CalendarStatusElement.StartTimer();
          this.calendarElement.CalendarStatusElement.LabelElement.Text = DateTime.Now.ToString(this.calendarElement.CalendarStatusElement.LabelFormat, (IFormatProvider) this.Culture);
        }
        this.OnNotifyPropertyChanged(nameof (ShowFooter));
      }
    }

    [Category("Header Settings")]
    [NotifyParentProperty(true)]
    [DefaultValue(true)]
    [Description("Gets or sets whether RadCalendar will display a header/navigation row.")]
    public virtual bool ShowHeader
    {
      get
      {
        return this.showHeader;
      }
      set
      {
        if (this.showHeader == value)
          return;
        this.showHeader = value;
        this.OnNotifyPropertyChanged(nameof (ShowHeader));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Determines whether the column headers will appear on the calendar.")]
    public bool ShowColumnHeaders
    {
      get
      {
        return this.showColumnHeaders;
      }
      set
      {
        if (this.showColumnHeaders == value)
          return;
        this.showColumnHeaders = value;
        this.OnNotifyPropertyChanged(nameof (ShowColumnHeaders));
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Determines whether the row headers will appear on the calendar.")]
    [NotifyParentProperty(true)]
    public bool ShowRowHeaders
    {
      get
      {
        return this.showRowHeaders;
      }
      set
      {
        if (this.showRowHeaders == value)
          return;
        this.showRowHeaders = value;
        this.OnNotifyPropertyChanged(nameof (ShowRowHeaders));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether a single CalendarView object will display a header.")]
    public bool ShowViewHeader
    {
      get
      {
        return this.showViewHeader;
      }
      set
      {
        if (this.showViewHeader == value)
          return;
        this.showViewHeader = value;
        this.OnNotifyPropertyChanged(nameof (ShowViewHeader));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether a single CalendarView object will display a selector.")]
    public bool ShowViewSelector
    {
      get
      {
        return this.showViewSelector;
      }
      set
      {
        if (this.showViewSelector == value)
          return;
        this.showViewSelector = value;
        this.OnNotifyPropertyChanged(nameof (ShowViewSelector));
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [NotifyParentProperty(true)]
    [Description("Gets or sets whether the view selector will be allowed to select all dates presented by the CalendarView.")]
    public bool AllowViewSelector
    {
      get
      {
        return this.allowViewSelector;
      }
      set
      {
        if (this.allowViewSelector == value)
          return;
        this.allowViewSelector = value;
        this.OnNotifyPropertyChanged(nameof (AllowViewSelector));
      }
    }

    [Description("Gets or sets the zooming factor of a cell which is handled by the zooming (fish eye) functionality.")]
    [NotifyParentProperty(true)]
    [DefaultValue(1.3f)]
    [Category("Behavior")]
    public virtual float ZoomFactor
    {
      get
      {
        return this.calendarElement.ZoomFactor;
      }
      set
      {
        this.calendarElement.ZoomFactor = value;
        this.OnNotifyPropertyChanged(nameof (ZoomFactor));
      }
    }

    [Description("Gets or sets whether the zooming functionality is enabled.")]
    [NotifyParentProperty(true)]
    [DefaultValue(false)]
    [Category("Behavior")]
    public virtual bool AllowFishEye
    {
      get
      {
        return this.calendarElement.AllowFishEye;
      }
      set
      {
        this.calendarElement.AllowFishEye = value;
        this.OnNotifyPropertyChanged(nameof (AllowFishEye));
      }
    }

    [NotifyParentProperty(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether row headers ( if displayed by a MonthView object) will act as row selectors.")]
    public bool AllowRowHeaderSelectors
    {
      get
      {
        return this.allowRowHeaderSelectors;
      }
      set
      {
        if (this.allowRowHeaderSelectors == value)
          return;
        this.allowRowHeaderSelectors = value;
        this.OnNotifyPropertyChanged(nameof (AllowRowHeaderSelectors));
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [NotifyParentProperty(true)]
    [Description("Gets or sets whether column headers ( if displayed by a MonthView object) will act as column selectors.")]
    public bool AllowColumnHeaderSelectors
    {
      get
      {
        return this.allowColumnHeaderSelectors;
      }
      set
      {
        if (this.allowColumnHeaderSelectors == value)
          return;
        this.allowColumnHeaderSelectors = value;
        this.OnNotifyPropertyChanged(nameof (AllowColumnHeaderSelectors));
      }
    }

    [Category("MonthView Specific Settings")]
    [NotifyParentProperty(true)]
    [DefaultValue(true)]
    [Description("Gets or sets whether the month matrix, when rendered will show days from other (previous or next) months or will render only blank cells.")]
    public bool ShowOtherMonthsDays
    {
      get
      {
        return this.showOtherMonthsDays;
      }
      set
      {
        if (this.showOtherMonthsDays == value)
          return;
        this.showOtherMonthsDays = value;
        this.OnNotifyPropertyChanged(nameof (ShowOtherMonthsDays));
      }
    }

    private bool CalculateValidTableLayout(int value, bool isColumn)
    {
      if (isColumn)
      {
        if (value != 7 && value != 14 && (value != 21 && value != 3) && (value != 6 && value != 2))
          return false;
        if (this.Orientation == Orientation.Vertical)
        {
          switch (value)
          {
            case 2:
              this.singleViewColumns = new int?(value);
              this.Rows = 21;
              this.monthLayout = new MonthLayout?(MonthLayout.Layout_21rows_x_2columns);
              return true;
            case 3:
              this.singleViewColumns = new int?(value);
              this.Rows = 14;
              this.monthLayout = new MonthLayout?(MonthLayout.Layout_14rows_x_3columns);
              return true;
            case 6:
              this.singleViewColumns = new int?(value);
              this.Rows = 7;
              this.monthLayout = new MonthLayout?(MonthLayout.Layout_7rows_x_6columns);
              return true;
          }
        }
        else
        {
          switch (value)
          {
            case 7:
              this.singleViewColumns = new int?(value);
              this.Rows = 6;
              this.monthLayout = new MonthLayout?(MonthLayout.Layout_7columns_x_6rows);
              return true;
            case 14:
              this.singleViewColumns = new int?(value);
              this.Rows = 3;
              this.monthLayout = new MonthLayout?(MonthLayout.Layout_14columns_x_3rows);
              return true;
            case 21:
              this.singleViewColumns = new int?(value);
              this.Rows = 2;
              this.monthLayout = new MonthLayout?(MonthLayout.Layout_21columns_x_2rows);
              return true;
          }
        }
      }
      else
      {
        if (value != 7 && value != 14 && (value != 21 && value != 3) && (value != 6 && value != 2))
          return false;
        if (this.orientation == Orientation.Vertical)
        {
          switch (value)
          {
            case 7:
              this.singleViewRows = new int?(value);
              this.Columns = 6;
              return true;
            case 14:
              this.singleViewRows = new int?(value);
              this.Columns = 3;
              return true;
            case 21:
              this.singleViewRows = new int?(value);
              this.Columns = 2;
              return true;
          }
        }
        else
        {
          switch (value)
          {
            case 2:
              this.singleViewRows = new int?(value);
              this.Columns = 21;
              return true;
            case 3:
              this.singleViewRows = new int?(value);
              this.Columns = 14;
              return true;
            case 6:
              this.singleViewRows = new int?(value);
              this.Columns = 7;
              return true;
          }
        }
      }
      return false;
    }

    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(MonthLayout.Layout_7columns_x_6rows)]
    [NotifyParentProperty(true)]
    [Category("Month View Settings")]
    [Description("This property allows using presets, regarding the layout of the calendar area. Sets or gets predefined pairs of rows and columns, so that the product of the two values is exactly 42, which guarantees valid calendar layout.")]
    public MonthLayout MonthLayout
    {
      get
      {
        if (this.monthLayout.HasValue)
          return this.monthLayout.Value;
        return MonthLayout.Layout_7columns_x_6rows;
      }
      set
      {
        MonthLayout? monthLayout1 = this.monthLayout;
        MonthLayout monthLayout2 = value;
        if ((monthLayout1.GetValueOrDefault() != monthLayout2 ? 1 : (!monthLayout1.HasValue ? 1 : 0)) == 0)
          return;
        this.monthLayout = new MonthLayout?(value);
        this.SynchronizeMonthView();
        this.OnNotifyPropertyChanged(nameof (MonthLayout));
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    [Description("Provides custom text for all row header elements.")]
    [Localizable(true)]
    [Bindable(false)]
    [Category("Header Settings")]
    [DefaultValue("")]
    [NotifyParentProperty(true)]
    public string RowHeaderText
    {
      get
      {
        return this.rowHeaderText;
      }
      set
      {
        if (!(this.rowHeaderText != value))
          return;
        this.rowHeaderText = value;
        this.OnNotifyPropertyChanged(nameof (RowHeaderText));
      }
    }

    [Description("The image displayed for all <strong>CalendarView</strong> row header elements.")]
    [Localizable(true)]
    [Category("Header Settings")]
    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    public Image RowHeaderImage
    {
      get
      {
        return this.rowHeaderImage;
      }
      set
      {
        if (this.rowHeaderImage == value)
          return;
        this.rowHeaderImage = value;
        this.OnNotifyPropertyChanged(nameof (RowHeaderImage));
      }
    }

    [DefaultValue(false)]
    internal bool RTL
    {
      get
      {
        return this.rightToLeft;
      }
      set
      {
        if (this.rightToLeft == value)
          return;
        this.rightToLeft = value;
        this.OnNotifyPropertyChanged("RightToLeft");
      }
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
        this.RTL = rightToLeft == RightToLeft.Yes;
      }
      if (rightToLeft == RightToLeft.No)
        this.RTL = false;
      else if (rightToLeft == RightToLeft.Yes)
        this.RTL = true;
      base.OnRightToLeftChanged(e);
      this.InvalidateCalendar();
    }

    [Category("Header Settings")]
    [DefaultValue("")]
    [Description("Provides custom text for all column header elements.")]
    [Localizable(true)]
    [Bindable(false)]
    [NotifyParentProperty(true)]
    public string ColumnHeaderText
    {
      get
      {
        return this.columnHeaderText;
      }
      set
      {
        if (!(this.columnHeaderText != value))
          return;
        this.columnHeaderText = value;
        this.OnNotifyPropertyChanged(nameof (ColumnHeaderText));
      }
    }

    [Description("The image displayed for all column header elements.")]
    [Bindable(false)]
    [Category("Header Settings")]
    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    public Image ColumnHeaderImage
    {
      get
      {
        return this.columnHeaderImage;
      }
      set
      {
        if (this.columnHeaderImage == value)
          return;
        this.columnHeaderImage = value;
        this.OnNotifyPropertyChanged(nameof (ColumnHeaderImage));
      }
    }

    [Bindable(false)]
    [Localizable(true)]
    [Category("Header Settings")]
    [DefaultValue("x")]
    [Description("The text displayed in the view selector element.")]
    [NotifyParentProperty(true)]
    public string ViewSelectorText
    {
      get
      {
        return this.viewSelectorText;
      }
      set
      {
        if (!(this.viewSelectorText != value))
          return;
        this.viewSelectorText = value;
        this.OnNotifyPropertyChanged(nameof (ViewSelectorText));
      }
    }

    [Description("The image displayed in the view selector element.")]
    [Bindable(false)]
    [Category("Header Settings")]
    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    public Image ViewSelectorImage
    {
      get
      {
        return this.viewSelectorImage;
      }
      set
      {
        if (this.viewSelectorImage == value)
          return;
        this.viewSelectorImage = value;
        this.OnNotifyPropertyChanged(nameof (ViewSelectorImage));
      }
    }

    [Category("Behavior")]
    [DefaultValue(Orientation.Horizontal)]
    [Description("Gets or sets the orientation (rendering direction) of the calendar component.")]
    [NotifyParentProperty(true)]
    public Orientation Orientation
    {
      get
      {
        return this.orientation;
      }
      set
      {
        if (this.orientation == value)
          return;
        this.orientation = value;
        this.OnNotifyPropertyChanged(nameof (Orientation));
      }
    }

    [DefaultValue(3)]
    [Category("Behavior")]
    [Description("Gets or sets the number of views that will be scrolled when the user clicks on a fast navigation button.")]
    [NotifyParentProperty(true)]
    public int FastNavigationStep
    {
      get
      {
        return this.fastNavigationStep;
      }
      set
      {
        if (this.fastNavigationStep == value)
          return;
        this.fastNavigationStep = value;
        this.OnNotifyPropertyChanged(nameof (FastNavigationStep));
      }
    }

    [Description("A collection of special days in the calendar to which may be applied specific formatting.")]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public CalendarDayCollection SpecialDays
    {
      get
      {
        if (this.specialDays == null)
          this.specialDays = new CalendarDayCollection(this);
        return this.specialDays;
      }
    }

    [Category("Behavior")]
    [DefaultValue(typeof (Padding), "0,0,0,0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the padding of the calendar cells.")]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public Padding CellPadding
    {
      get
      {
        return this.cellPadding;
      }
      set
      {
        this.cellPadding = value;
        this.OnNotifyPropertyChanged(nameof (CellPadding));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(1)]
    [Description("Gets or sets the vertical spacing between the calendar cells.")]
    [NotifyParentProperty(true)]
    public int CellVerticalSpacing
    {
      get
      {
        return this.cellVerticalSpacing;
      }
      set
      {
        this.cellVerticalSpacing = value;
        this.OnNotifyPropertyChanged(nameof (CellVerticalSpacing));
      }
    }

    [DefaultValue(1)]
    [Description("Gets or sets the horizontal spacing between the calendar cells.")]
    [NotifyParentProperty(true)]
    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.All)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int CellHorizontalSpacing
    {
      get
      {
        return this.cellHorizontalSpacing;
      }
      set
      {
        this.cellHorizontalSpacing = value;
        this.OnNotifyPropertyChanged(nameof (CellHorizontalSpacing));
      }
    }

    [DefaultValue(typeof (Padding), "0,0,0,0")]
    [RefreshProperties(RefreshProperties.All)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [Description("Gets or sets the margin of the calendar cells.")]
    [NotifyParentProperty(true)]
    public Padding CellMargin
    {
      get
      {
        return this.cellMargin;
      }
      set
      {
        this.cellMargin = value;
        this.OnNotifyPropertyChanged(nameof (CellMargin));
      }
    }

    [Category("Data")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Exposes the top instance of CalendarView or its derived types.")]
    public CalendarView DefaultView
    {
      get
      {
        if (this.calendarView == null)
          return this.EnsureDefaultView();
        return this.calendarView;
      }
    }

    public virtual void RemoveFocusedDate(bool removeAlsoSelectedDate)
    {
      this.focusedDate = new DateTime?();
      this.OnNotifyPropertyChanged("FocusedDate");
      if (!removeAlsoSelectedDate)
        return;
      this.SelectedDates.Clear();
    }

    protected static DateTime TruncateTimeComponent(DateTime date)
    {
      return date.Date;
    }

    protected virtual CalendarView EnsureDefaultView()
    {
      this.calendarView = this.multiViewColumns > 1 || this.multiViewRows > 1 ? (CalendarView) new MultiMonthView(this) : (CalendarView) new MonthView(this);
      this.calendarView.Initialize();
      return this.calendarView;
    }

    private void SynchronizeMonthView()
    {
      MonthLayout? monthLayout = this.monthLayout;
      ref MonthLayout? local = ref monthLayout;
      MonthLayout valueOrDefault = local.GetValueOrDefault();
      if (!local.HasValue)
        return;
      switch (valueOrDefault)
      {
        case MonthLayout.Layout_7columns_x_6rows:
          this.Rows = 6;
          this.Columns = 7;
          break;
        case MonthLayout.Layout_14columns_x_3rows:
          this.Rows = 3;
          this.Columns = 14;
          break;
        case MonthLayout.Layout_21columns_x_2rows:
          this.Rows = 2;
          this.Columns = 21;
          break;
        case MonthLayout.Layout_7rows_x_6columns:
          this.Rows = 7;
          this.Columns = 6;
          break;
        case MonthLayout.Layout_14rows_x_3columns:
          this.Rows = 14;
          this.Columns = 3;
          break;
        case MonthLayout.Layout_21rows_x_2columns:
          this.Rows = 21;
          this.Columns = 2;
          break;
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (!this.DefaultView.HandleKeyDown(keyData))
        return base.ProcessCmdKey(ref msg, keyData);
      return true;
    }

    public void InvalidateCalendar()
    {
      this.CalendarElement.RefreshVisuals(true);
      this.Invalidate();
    }

    internal void InvalidateCalendarSelection()
    {
      if (this.CalendarElement.CalendarVisualElement is MultiMonthViewElement)
      {
        foreach (MonthViewElement child in (this.CalendarElement.CalendarVisualElement as MultiMonthViewElement).Children[0].Children[1].Children)
          child.TableElement.RefreshSelectedDates();
      }
      else if (this.CalendarElement.CalendarVisualElement is MonthViewElement)
        (this.CalendarElement.CalendarVisualElement as MonthViewElement).TableElement.RefreshSelectedDates();
      if (!this.DesignMode || this.calendarElement == null)
        return;
      this.calendarElement.RefreshVisuals(true);
      this.calendarElement.Invalidate();
    }

    internal void SetFocusedDateView()
    {
      if (!this.focusedDate.HasValue || this.DefaultView.IsDateInView(this.FocusedDate))
      {
        this.CalendarElement.RefreshVisuals(true);
        this.Invalidate();
      }
      else
      {
        CalendarView view = this.DefaultView.CreateView(this.FocusedDate);
        if (this.OnViewChanging(view).Cancel)
          return;
        this.SetCalendarView(view);
        this.CalendarElement.CalendarVisualElement.View = view;
        this.OnViewChanged();
        this.CalendarElement.CalendarNavigationElement.Text = view.GetTitleContent();
      }
      this.Invalidate();
    }

    internal void SetCalendarView(CalendarView inputView)
    {
      this.calendarView = inputView;
    }

    internal CalendarView GetNewViewFromStep(int navigationStep)
    {
      bool flag = false;
      if (navigationStep < 0)
      {
        navigationStep = -navigationStep;
        flag = true;
      }
      return !flag ? this.DefaultView.GetNextView(navigationStep) : this.DefaultView.GetPreviousView(navigationStep);
    }

    private void ReInitializeCalendarElement()
    {
      this.calendarElement.ReInitializeChildren();
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "FocusedDate":
          this.SetFocusedDateView();
          break;
        case "RowHeaderText":
        case "RowHeaderImage":
        case "ColumnHeaderText":
        case "ColumnHeaderImage":
        case "DayNameFormat":
        case "CellAlign":
          this.CalendarElement.RefreshVisuals(true);
          break;
        case "FirstDayOfWeek":
          this.InvalidateCalendar();
          CalendarView view = this.DefaultView.CreateView(this.FocusedDate);
          if (this.OnViewChanging(view).Cancel)
            return;
          this.SetCalendarView(view);
          this.CalendarElement.CalendarVisualElement.View = view;
          this.OnViewChanged();
          this.CalendarElement.CalendarNavigationElement.Text = view.GetTitleContent();
          break;
        case "SelectedDate":
          this.CalendarElement.RefreshVisuals(true);
          break;
        case "MonthLayout":
          this.DefaultView.Initialize();
          this.InvalidateCalendar();
          break;
        case "Culture":
        case "CultureID":
          this.DefaultView.Initialize();
          this.InvalidateCalendar();
          break;
        case "ShowFooter":
          if (this.ShowFooter)
          {
            this.CalendarElement.CalendarStatusElement.Visibility = ElementVisibility.Visible;
            break;
          }
          this.CalendarElement.CalendarStatusElement.Visibility = ElementVisibility.Collapsed;
          break;
        case "TitleAlign":
          this.CalendarElement.RefreshVisuals(true);
          this.CalendarElement.InvalidateArrange();
          this.CalendarElement.InvalidateMeasure();
          this.CalendarElement.UpdateLayout();
          break;
        case "AllowSelect":
          this.ClearButton.Visibility = this.AllowSelect ? ElementVisibility.Visible : ElementVisibility.Collapsed;
          break;
        case "AllowMultiSelect":
          if (!this.AllowMultipleSelect)
          {
            this.BeginUpdate();
            this.SelectedDate = this.SelectedDate;
            this.EndUpdate();
            break;
          }
          break;
        case "Orientation":
          if (this.Rows == 6 && this.Columns == 7 || this.singleViewColumns.HasValue && this.singleViewRows.HasValue)
          {
            if (!this.singleViewColumns.HasValue)
              this.singleViewColumns = new int?(7);
            if (!this.singleViewRows.HasValue)
              this.singleViewRows = new int?(6);
            int num = this.singleViewRows.Value;
            this.singleViewRows = new int?(this.singleViewColumns.Value);
            this.singleViewColumns = new int?(num);
            switch (this.Rows)
            {
              case 2:
                this.monthLayout = new MonthLayout?(MonthLayout.Layout_21columns_x_2rows);
                break;
              case 3:
                this.monthLayout = new MonthLayout?(MonthLayout.Layout_14columns_x_3rows);
                break;
              case 6:
                this.monthLayout = new MonthLayout?(MonthLayout.Layout_7columns_x_6rows);
                break;
              case 7:
                this.monthLayout = new MonthLayout?(MonthLayout.Layout_7rows_x_6columns);
                break;
              case 14:
                this.monthLayout = new MonthLayout?(MonthLayout.Layout_14rows_x_3columns);
                break;
              case 21:
                this.monthLayout = new MonthLayout?(MonthLayout.Layout_21rows_x_2columns);
                break;
            }
          }
          else
            break;
      }
      base.OnNotifyPropertyChanged(e);
    }

    internal void BeginUpdate()
    {
      this.updating = true;
    }

    internal void EndUpdate()
    {
      this.updating = false;
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.calendarElement = new RadCalendarElement(this, this.DefaultView);
      parent.Children.Add((RadElement) this.calendarElement);
      this.TitleAlign = System.Windows.Forms.VisualStyles.ContentAlignment.Center;
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      if ((specified & BoundsSpecified.Size) != BoundsSpecified.Size)
        return;
      base.ScaleControl(factor, specified);
    }
  }
}
