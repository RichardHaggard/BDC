// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTimePickerContentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadTimePickerContentElement : LightVisualElement
  {
    public static RadProperty ClockBeforeTablesProperty = RadProperty.Register(nameof (ClockBeforeTables), typeof (bool), typeof (RadTimePickerContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private int rowHeight = 29;
    private int columnsCount = 4;
    private int headerHeight = 19;
    private int buttonHeight = 35;
    private int tableWidth = 170;
    private int minute = -1;
    private int hour = -1;
    private int step = 5;
    private bool twoTablesForTime = true;
    private bool clockBeforeTables = true;
    private TimeTables timeTables = TimeTables.HoursAndMinutesInTwoTables;
    private IPickerContentElementOwner owner;
    private TimeTableElementHours containerHours;
    private TimeTableElement containerMinutes;
    private RadClockElement clockElement;
    private TimePickerDoneButtonElement buttonPanel;
    private StackLayoutElement mainStack;
    private StackLayoutElement clockAndTablesStack;
    private StackLayoutElement clockStack;
    private TimeTableStackLayoutElement twoTablesStack;
    private LightVisualElement clockHeaderElement;
    private DateTime value;
    private DateTime minValue;
    private DateTime maxValue;
    private bool readOnly;
    private ClockPosition clockPosition;

    public RadTimePickerContentElement(IPickerContentElementOwner owner)
    {
      this.owner = owner;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.minValue = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 0, 0, 0);
      this.maxValue = new DateTime(DateTime.MaxValue.Year, DateTime.MaxValue.Month, DateTime.MaxValue.Day, 23, 59, 59);
      this.containerHours = new TimeTableElementHours();
      this.containerHours.Class = "TimePickerContainerHours";
      this.containerHours.StretchVertically = false;
      this.containerHours.ContentElement.Class = "TimePickerContainerHoursContent";
      this.containerHours.ContentElement.StretchVertically = false;
      this.containerHours.TableHeader.Class = "TimePickerContainerHoursHeader";
      int num1 = (int) this.containerHours.ContentElement.BindProperty(RadElement.PaddingProperty, (RadObject) this, RadElement.PaddingProperty, PropertyBindingOptions.TwoWay);
      this.buttonPanel = new TimePickerDoneButtonElement(this);
      this.containerMinutes = new TimeTableElement();
      this.containerMinutes.Class = "TimePickerContainerMinutes";
      this.containerMinutes.StretchVertically = true;
      this.containerMinutes.ContentElement.Class = "TimePickerContainerMinutesContent";
      this.containerMinutes.TableHeader.Class = "TimePickerContainerMinutesHeader";
      int num2 = (int) this.containerMinutes.ContentElement.BindProperty(RadElement.PaddingProperty, (RadObject) this, RadElement.PaddingProperty, PropertyBindingOptions.TwoWay);
      this.twoTablesStack = new TimeTableStackLayoutElement();
      this.twoTablesStack.Class = "TimePickerHourAndMinustesTablesStack";
      this.twoTablesStack.FitInAvailableSize = true;
      this.twoTablesStack.Orientation = Orientation.Vertical;
      this.twoTablesStack.StretchVertically = true;
      this.twoTablesStack.StretchHorizontally = true;
      this.twoTablesStack.Children.Add((RadElement) this.containerHours);
      this.twoTablesStack.Children.Add((RadElement) this.containerMinutes);
      this.clockStack = new StackLayoutElement();
      this.clockStack.Orientation = Orientation.Vertical;
      this.clockStack.StretchHorizontally = false;
      this.clockStack.StretchVertically = true;
      this.clockStack.Class = "TimePickerClockStack";
      this.clockHeaderElement = new LightVisualElement();
      this.clockHeaderElement.Class = "ClockHeaderElement";
      this.clockHeaderElement.Text = " ";
      this.clockHeaderElement.StretchHorizontally = true;
      this.clockHeaderElement.StretchVertically = false;
      this.clockStack.Children.Add((RadElement) this.clockHeaderElement);
      this.clockElement = new RadClockElement();
      this.clockElement.StretchVertically = true;
      this.clockElement.ShowSystemTime = false;
      this.clockElement.Alignment = ContentAlignment.MiddleCenter;
      this.clockStack.Children.Add((RadElement) this.clockElement);
      this.clockAndTablesStack = new StackLayoutElement();
      this.clockAndTablesStack.Class = "TimePickerClockAndTablesStack";
      this.clockAndTablesStack.FitInAvailableSize = true;
      this.clockAndTablesStack.StretchHorizontally = true;
      this.clockAndTablesStack.StretchVertically = true;
      this.clockAndTablesStack.Children.Add((RadElement) this.clockStack);
      this.clockAndTablesStack.Children.Add((RadElement) this.twoTablesStack);
      this.mainStack = (StackLayoutElement) new TimeTableStackLayoutElement();
      this.mainStack.FitInAvailableSize = false;
      this.mainStack.StretchVertically = true;
      this.mainStack.StretchHorizontally = true;
      this.mainStack.Orientation = Orientation.Vertical;
      this.mainStack.Children.Add((RadElement) this.clockAndTablesStack);
      this.mainStack.Children.Add((RadElement) this.buttonPanel);
      this.Children.Add((RadElement) this.mainStack);
    }

    public TimeTableElementHours HoursTable
    {
      get
      {
        return this.containerHours;
      }
    }

    public TimeTableElement MinutesTable
    {
      get
      {
        return this.containerMinutes;
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
      }
    }

    public int TableWidth
    {
      get
      {
        return this.tableWidth;
      }
      set
      {
        this.tableWidth = value;
      }
    }

    public int ButtonPanelHeight
    {
      get
      {
        return this.buttonHeight;
      }
      set
      {
        this.buttonHeight = value;
      }
    }

    public int HeadersHeight
    {
      get
      {
        return this.headerHeight;
      }
      set
      {
        this.headerHeight = value;
      }
    }

    public int ColumnsCount
    {
      get
      {
        return this.columnsCount;
      }
      set
      {
        this.columnsCount = value;
      }
    }

    public int RowHeight
    {
      get
      {
        return this.rowHeight;
      }
      set
      {
        this.rowHeight = value;
      }
    }

    public virtual bool ReadOnly
    {
      get
      {
        return this.readOnly;
      }
      set
      {
        this.readOnly = value;
      }
    }

    public TimePickerDoneButtonElement FooterPanel
    {
      get
      {
        return this.buttonPanel;
      }
    }

    public ClockPosition ClockPosition
    {
      get
      {
        return this.clockPosition;
      }
      set
      {
        this.clockPosition = value;
        if (this.clockPosition == ClockPosition.HideClock)
        {
          this.HideClock();
        }
        else
        {
          this.ShowClock();
          this.ClockBeforeTables = value == ClockPosition.ClockBeforeTables;
        }
      }
    }

    public TimeTables TimeTables
    {
      get
      {
        return this.timeTables;
      }
      set
      {
        this.timeTables = value;
        this.TwoTablesForTime = value == TimeTables.HoursAndMinutesInTwoTables;
      }
    }

    public LightVisualElement ClockHeaderElement
    {
      get
      {
        return this.clockHeaderElement;
      }
    }

    public RadClockElement ClockElement
    {
      get
      {
        return this.clockElement;
      }
    }

    internal IPickerContentElementOwner Owner
    {
      get
      {
        return this.owner;
      }
    }

    public DateTime Value
    {
      get
      {
        return this.value;
      }
      set
      {
        if (this.readOnly)
          return;
        this.value = value;
        this.Owner.Value = (object) value;
        CultureInfo culture = this.Owner.Culture;
        if (this.IsAmPmMode())
          this.containerHours.TableHeader.SetHeaderElementText(this.value, culture);
        else
          this.containerHours.TableHeader.HeaderElement.Text = this.Owner.HourHeaderText;
      }
    }

    public DateTime MinValue
    {
      get
      {
        return this.minValue;
      }
      set
      {
        this.minValue = value;
      }
    }

    public DateTime MaxValue
    {
      get
      {
        return this.maxValue;
      }
      set
      {
        this.maxValue = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ClockBeforeTables
    {
      get
      {
        return this.clockBeforeTables;
      }
      set
      {
        this.clockBeforeTables = value;
        this.twoTablesStack.ClockBeforeTables = value;
        this.containerMinutes.TableHeader.ClockBeforeTables1 = value;
        this.containerHours.TableHeader.ClockBeforeTables1 = value;
        this.ChangeClockAndTableLocation(value);
      }
    }

    internal virtual bool TwoTablesForTime
    {
      get
      {
        return this.twoTablesForTime;
      }
      set
      {
        this.twoTablesForTime = value;
        this.PrepareContent();
      }
    }

    public event TimeCellFormattingEventHandler TimeCellFormatting;

    public virtual void ShowClock()
    {
      if (this.clockStack.Visibility != ElementVisibility.Collapsed)
        return;
      this.clockStack.Visibility = ElementVisibility.Visible;
      int num1 = (int) this.twoTablesStack.ResetValue(LightVisualElement.BorderLeftWidthProperty);
      int num2 = (int) this.twoTablesStack.SetDefaultValueOverride(LightVisualElement.BorderLeftWidthProperty, (object) 1f);
    }

    public virtual void HideClock()
    {
      if (this.clockStack.Visibility == ElementVisibility.Collapsed)
        return;
      this.clockStack.Visibility = ElementVisibility.Collapsed;
      this.ClockBeforeTables = true;
      this.twoTablesStack.BorderLeftWidth = 0.0f;
    }

    public virtual void ClearSelection()
    {
      this.ManageSelectedItems(this.containerHours.ContentElement, (TimeTableVisualElement) null);
      this.ManageSelectedItems(this.containerMinutes.ContentElement, (TimeTableVisualElement) null);
    }

    public virtual void SetSelected(DateTime? time)
    {
      if (!time.HasValue)
      {
        this.ClearSelection();
      }
      else
      {
        if (this.IsAmPmMode() && time.Value.Hour >= 12)
          time = new DateTime?(time.Value.AddHours(-12.0));
        if (!this.TwoTablesForTime)
        {
          foreach (TimeTableVisualElement child in this.HoursTable.ContentElement.Children)
          {
            if (child.Time.Hour == time.Value.Hour && child.Time.Minute == time.Value.Minute)
            {
              child.Selected = true;
              break;
            }
          }
        }
        else
        {
          foreach (TimeTableVisualElement child in this.HoursTable.ContentElement.Children)
          {
            if (child.Time.Hour == time.Value.Hour)
            {
              child.Selected = true;
              break;
            }
          }
          foreach (TimeTableVisualElement child in this.MinutesTable.ContentElement.Children)
          {
            if (child.Time.Minute == time.Value.Minute)
            {
              child.Selected = true;
              break;
            }
          }
        }
      }
    }

    public virtual void SetValueAndClose()
    {
      this.Owner.CloseOwnerPopup();
    }

    public virtual void SetValueAndClose(DateTime value)
    {
      if (!this.readOnly)
        this.Owner.Value = (object) value;
      this.Owner.CloseOwnerPopup();
    }

    public virtual void SetClockTime(DateTime time)
    {
      this.ClockElement.SetClock(time);
      this.ClockHeaderElement.Text = time.ToString(this.Owner.Format, (IFormatProvider) this.Owner.Culture);
      this.PrepareHoursTableHeader();
    }

    public virtual void PrepareContent()
    {
      if (this.TwoTablesForTime)
      {
        this.containerMinutes.Visibility = ElementVisibility.Visible;
        this.FillPopupInTwoTables();
        this.PrepareHoursTableHeader();
        this.PrepareMinutesTableHeader();
      }
      else
      {
        this.containerMinutes.Visibility = ElementVisibility.Collapsed;
        this.FillHours(this.containerHours.ContentElement, this.Step, this.ColumnsCount, this.Owner.Culture.DateTimeFormat.ShortTimePattern, this.IsAmPmMode());
        this.PrepareHoursTableHeader();
      }
    }

    public void ChangeClockAndTableLocation(bool clockBeforeTables)
    {
      this.clockAndTablesStack.Orientation = clockBeforeTables ? Orientation.Horizontal : Orientation.Vertical;
      if (!clockBeforeTables)
      {
        this.containerMinutes.ContentElement.StretchVertically = false;
        this.clockStack.StretchHorizontally = true;
        this.clockStack.StretchVertically = false;
      }
      else
      {
        this.containerMinutes.ContentElement.StretchVertically = true;
        this.clockStack.StretchHorizontally = false;
        this.clockStack.StretchVertically = true;
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.PrepareContent();
      this.FooterPanel.ButtonElement.Text = LocalizationProvider<RadTimePickerLocalizationProvider>.CurrentProvider.GetLocalizedString("CloseButtonText");
      this.ClockBeforeTables = this.clockBeforeTables;
    }

    protected virtual void OnTimeCellFormatting(TimeCellFormattingEventArgs args)
    {
      if (this.TimeCellFormatting == null)
        return;
      this.TimeCellFormatting((object) this, args);
    }

    protected virtual void element_Click(object sender, EventArgs e)
    {
      if (this.readOnly || !(sender as TimeTableVisualElement).Enabled)
        return;
      DateTime dateTime1 = ((TimeTableVisualElement) sender).Time;
      this.ManageSelectedItems(this.containerHours.ContentElement, (TimeTableVisualElement) sender);
      DateTime dateTime2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
      if (this.Owner.Value != null)
        dateTime2 = (DateTime) this.Owner.Value;
      if (this.IsAmPmMode())
      {
        if (dateTime2.Hour >= 12 && dateTime1.Hour < 12)
          dateTime1 = dateTime1.AddHours(12.0);
        else if (dateTime2.Hour < 12 && dateTime1.Hour >= 12)
          dateTime1 = dateTime1.AddHours(-12.0);
      }
      if (this.TwoTablesForTime)
      {
        int minutes = 0;
        DateTime dateTime3 = !this.ReArrangeMinutes(dateTime1.Hour, out minutes) ? new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, dateTime1.Hour, dateTime2.Minute, this.Value.Second) : new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, dateTime1.Hour, minutes, this.Value.Second);
        this.hour = dateTime1.Hour;
        this.Owner.Value = (object) dateTime3;
      }
      else
        this.Value = dateTime1;
    }

    private bool ReArrangeMinutes(int hour, out int minutes)
    {
      minutes = 0;
      bool flag1 = false;
      bool flag2 = false;
      if (hour == this.minValue.Hour)
      {
        foreach (TimeTableVisualElement child in this.containerMinutes.ContentElement.Children)
        {
          if (child.Time.Minute < this.minValue.Minute)
          {
            if (child.Selected)
            {
              child.Selected = false;
              flag2 = true;
            }
            child.Enabled = false;
          }
          else
          {
            child.Enabled = true;
            if (flag2)
            {
              child.Selected = true;
              flag2 = false;
              minutes = child.Time.Minute;
              flag1 = true;
            }
          }
        }
      }
      else if (hour == this.maxValue.Hour)
      {
        for (int index = this.containerMinutes.ContentElement.Children.Count - 1; index >= 0; --index)
        {
          TimeTableVisualElement child = this.containerMinutes.ContentElement.Children[index] as TimeTableVisualElement;
          if (child.Time.Minute > this.maxValue.Minute)
          {
            if (child.Selected)
            {
              child.Selected = false;
              flag2 = true;
            }
            child.Enabled = false;
          }
          else
          {
            child.Enabled = true;
            if (flag2)
            {
              child.Selected = true;
              flag2 = false;
              minutes = child.Time.Minute;
              flag1 = true;
            }
          }
        }
      }
      else
      {
        foreach (RadElement child in this.containerMinutes.ContentElement.Children)
          child.Enabled = true;
      }
      return flag1;
    }

    private bool ReArrangeHours(out int newHour)
    {
      newHour = 0;
      bool flag = false;
      foreach (TimeTableVisualElement child in this.containerHours.ContentElement.Children)
      {
        child.Enabled = true;
        DateTime dateTime1 = child.Time;
        DateTime dateTime2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        if (this.Owner.Value != null)
          dateTime2 = (DateTime) this.Owner.Value;
        if (this.IsAmPmMode() && (dateTime2.Hour < 12 || dateTime1.Hour >= 12) && dateTime2.Hour < 12)
          dateTime1 = dateTime1.AddHours(12.0);
        if (dateTime1.Hour < this.minValue.Hour || dateTime1.Hour > this.maxValue.Hour)
        {
          if (child.Selected)
          {
            child.Selected = false;
            flag = true;
          }
          child.Enabled = false;
        }
      }
      if (flag)
      {
        foreach (TimeTableVisualElement child in this.containerHours.ContentElement.Children)
        {
          if (child.Enabled)
          {
            child.Selected = true;
            newHour = child.Time.Hour;
            break;
          }
        }
      }
      return flag;
    }

    protected virtual void elementMinutes_Click(object sender, EventArgs e)
    {
      if (this.readOnly || !(sender as TimeTableVisualElement).Enabled)
        return;
      this.minute = ((TimeTableVisualElement) sender).Time.Minute;
      this.ManageSelectedItems(this.containerMinutes.ContentElement, (TimeTableVisualElement) sender);
      DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
      if (this.Owner.Value != null)
        dateTime = (DateTime) this.Owner.Value;
      this.Owner.Value = (object) new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, this.minute, this.Value.Second);
    }

    protected virtual void HeaderArrow_Click(object sender, EventArgs e)
    {
      if (this.readOnly || this.Owner.Value == null)
        return;
      DateTime dateTime1 = (DateTime) this.Owner.Value;
      DateTime dateTime2;
      try
      {
        if (dateTime1.Hour > 12)
        {
          dateTime2 = dateTime1.AddHours(-12.0);
          this.hour -= 12;
        }
        else
        {
          dateTime2 = dateTime1.AddHours(12.0);
          this.hour += 12;
        }
      }
      catch (ArgumentOutOfRangeException ex)
      {
        return;
      }
      int newHour = dateTime2.Hour;
      if (this.ReArrangeHours(out newHour))
      {
        if (dateTime2.Hour > 12)
          newHour += 12;
        dateTime2 = new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, newHour, ((DateTime) this.Owner.Value).Minute, dateTime2.Second);
      }
      this.containerHours.TableHeader.SwitchAmToPm(this.Owner.Culture);
      this.Owner.Value = (object) dateTime2;
      int minutes = 0;
      if (this.ReArrangeMinutes(((DateTime) this.Owner.Value).Hour, out minutes))
        this.Owner.Value = (object) new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, dateTime2.Hour, minutes, dateTime2.Second);
      if (!((DateTime) this.Owner.Value != dateTime2))
        return;
      this.containerHours.TableHeader.SwitchAmToPm(this.Owner.Culture);
    }

    protected internal virtual bool IsAmPmMode()
    {
      string str = this.value.ToString((IFormatProvider) this.Owner.Culture);
      if (string.IsNullOrEmpty(this.Owner.Culture.DateTimeFormat.AMDesignator) || string.IsNullOrEmpty(this.Owner.Culture.DateTimeFormat.PMDesignator))
        return false;
      if (!str.Contains(this.Owner.Culture.DateTimeFormat.AMDesignator))
        return str.Contains(this.Owner.Culture.DateTimeFormat.PMDesignator);
      return true;
    }

    protected virtual void PrepareMinutesTableHeader()
    {
      this.containerMinutes.TableHeader.HeaderElement.Text = this.Owner.MinutesHeaderText;
    }

    protected virtual void PrepareHoursTableHeader()
    {
      if (this.IsAmPmMode())
      {
        this.containerHours.TableHeader.AmPmMode = true;
        if (this.Owner.Value != null)
          this.containerHours.TableHeader.SetHeaderElementText((DateTime) this.Owner.Value, this.Owner.Culture);
        else
          this.containerHours.TableHeader.SetHeaderElementText(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0), this.Owner.Culture);
        this.containerHours.TableHeader.HeaderElement.Click -= new EventHandler(this.HeaderArrow_Click);
        this.containerHours.TableHeader.LeftArrow.Click -= new EventHandler(this.HeaderArrow_Click);
        this.containerHours.TableHeader.RightArrow.Click -= new EventHandler(this.HeaderArrow_Click);
        this.containerHours.TableHeader.HeaderElement.Click += new EventHandler(this.HeaderArrow_Click);
        this.containerHours.TableHeader.LeftArrow.Click += new EventHandler(this.HeaderArrow_Click);
        this.containerHours.TableHeader.RightArrow.Click += new EventHandler(this.HeaderArrow_Click);
      }
      else
      {
        this.containerHours.TableHeader.AmPmMode = false;
        this.containerHours.TableHeader.HeaderElement.Text = this.Owner.HourHeaderText;
        this.containerHours.TableHeader.HeaderElement.Click -= new EventHandler(this.HeaderArrow_Click);
        this.containerHours.TableHeader.LeftArrow.Click -= new EventHandler(this.HeaderArrow_Click);
        this.containerHours.TableHeader.RightArrow.Click -= new EventHandler(this.HeaderArrow_Click);
      }
    }

    protected virtual void FillHours(
      GridLayout container,
      int step,
      int columnsCount,
      string format,
      bool AmPmIndicator)
    {
      foreach (RadElement child in container.Children)
        child.Click -= new EventHandler(this.element_Click);
      container.Children.Clear();
      container.Columns.Clear();
      container.Rows.Clear();
      for (int index = 0; index < columnsCount; ++index)
      {
        GridLayoutColumn gridLayoutColumn = new GridLayoutColumn();
        gridLayoutColumn.SizingType = GridLayoutSizingType.Proportional;
        gridLayoutColumn.Width = -1f;
        container.Columns.Add(gridLayoutColumn);
      }
      int num1 = !AmPmIndicator ? 1440 / step : 720 / step;
      int num2 = 0;
      int num3 = 0;
      DateTime dateTime = new DateTime(this.Value.Year, this.Value.Month, this.Value.Day, 0, 0, 0);
      GridLayoutRow gridLayoutRow1 = new GridLayoutRow();
      gridLayoutRow1.SizingType = GridLayoutSizingType.Fixed;
      gridLayoutRow1.FixedHeight = (float) this.RowHeight;
      container.Rows.Add(gridLayoutRow1);
      for (int index = 0; index < num1; ++index)
      {
        if (num2 >= columnsCount)
        {
          num2 = 0;
          ++num3;
          GridLayoutRow gridLayoutRow2 = new GridLayoutRow();
          gridLayoutRow2.SizingType = GridLayoutSizingType.Fixed;
          gridLayoutRow2.FixedHeight = (float) this.RowHeight;
          container.Rows.Add(gridLayoutRow2);
        }
        TimeTableVisualElement element = new TimeTableVisualElement();
        int num4 = (int) element.SetValue(GridLayout.ColumnIndexProperty, (object) num2);
        int num5 = (int) element.SetValue(GridLayout.RowIndexProperty, (object) num3);
        element.Text = dateTime.ToString(this.Owner.Culture.DateTimeFormat.ShortTimePattern, (IFormatProvider) this.Owner.Culture);
        if (this.Owner.Culture.DateTimeFormat.AMDesignator != "")
          element.Text = element.Text.Replace(this.Owner.Culture.DateTimeFormat.AMDesignator, "");
        if (this.Owner.Culture.DateTimeFormat.PMDesignator != "")
          element.Text = element.Text.Replace(this.Owner.Culture.DateTimeFormat.PMDesignator, "");
        element.Time = dateTime;
        element.Click += new EventHandler(this.element_Click);
        dateTime = dateTime.AddMinutes((double) step);
        element.Alignment = ContentAlignment.MiddleCenter;
        element.AutoSize = true;
        element.TextAlignment = ContentAlignment.MiddleCenter;
        if (this.Owner.Value != null && this.Owner.Value is DateTime)
        {
          if (((DateTime) this.Owner.Value).Hour > 12 && AmPmIndicator)
          {
            if (element.Time.Hour + 12 < this.minValue.Hour || element.Time.Hour + 12 > this.maxValue.Hour)
              element.Enabled = false;
          }
          else if (element.Time.Hour < this.minValue.Hour || element.Time.Hour > this.maxValue.Hour)
            element.Enabled = false;
        }
        this.OnTimeCellFormatting(new TimeCellFormattingEventArgs((int) element.GetValue(GridLayout.ColumnIndexProperty), (int) element.GetValue(GridLayout.RowIndexProperty), element, false));
        container.Children.Add((RadElement) element);
        ++num2;
      }
    }

    protected virtual void FillMinutes(GridLayout container, int step, int columnsCount)
    {
      foreach (RadElement child in container.Children)
        child.Click -= new EventHandler(this.elementMinutes_Click);
      container.Children.Clear();
      container.Columns.Clear();
      container.Rows.Clear();
      for (int index = 0; index < columnsCount; ++index)
      {
        GridLayoutColumn gridLayoutColumn = new GridLayoutColumn();
        gridLayoutColumn.SizingType = GridLayoutSizingType.Proportional;
        gridLayoutColumn.Width = -1f;
        container.Columns.Add(gridLayoutColumn);
      }
      int num1 = 60 / step;
      if (60 % step != 0)
        ++num1;
      int num2 = 0;
      int num3 = 0;
      DateTime dateTime1 = new DateTime(this.Value.Year, this.Value.Month, this.Value.Day, 0, 0, 0);
      GridLayoutRow gridLayoutRow1 = new GridLayoutRow();
      gridLayoutRow1.SizingType = GridLayoutSizingType.Fixed;
      gridLayoutRow1.FixedHeight = (float) this.RowHeight;
      container.Rows.Add(gridLayoutRow1);
      for (int index = 0; index < num1; ++index)
      {
        if (num2 >= columnsCount)
        {
          num2 = 0;
          ++num3;
          GridLayoutRow gridLayoutRow2 = new GridLayoutRow();
          gridLayoutRow2.SizingType = GridLayoutSizingType.Fixed;
          gridLayoutRow2.FixedHeight = (float) this.RowHeight;
          container.Rows.Add(gridLayoutRow2);
        }
        TimeTableVisualElement element = new TimeTableVisualElement();
        int num4 = (int) element.SetValue(GridLayout.ColumnIndexProperty, (object) num2);
        int num5 = (int) element.SetValue(GridLayout.RowIndexProperty, (object) num3);
        element.Text = "  :" + dateTime1.ToString("mm");
        element.Time = dateTime1;
        element.Click += new EventHandler(this.elementMinutes_Click);
        element.Alignment = ContentAlignment.MiddleCenter;
        element.AutoSize = true;
        element.TextAlignment = ContentAlignment.MiddleCenter;
        dateTime1 = dateTime1.AddMinutes((double) step);
        if (this.Owner.Value != null && this.Owner.Value is DateTime)
        {
          DateTime dateTime2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ((DateTime) this.Owner.Value).Hour, element.Time.Minute, element.Time.Second);
          if (dateTime2.TimeOfDay < this.minValue.TimeOfDay || dateTime2.TimeOfDay > this.maxValue.TimeOfDay)
            element.Enabled = false;
        }
        this.OnTimeCellFormatting(new TimeCellFormattingEventArgs((int) element.GetValue(GridLayout.ColumnIndexProperty), (int) element.GetValue(GridLayout.RowIndexProperty), element, true));
        container.Children.Add((RadElement) element);
        ++num2;
      }
    }

    protected virtual void FillPopupInTwoTables()
    {
      string shortTimePattern = this.Owner.Culture.DateTimeFormat.ShortTimePattern;
      int length = shortTimePattern.IndexOf(this.Owner.Culture.DateTimeFormat.TimeSeparator);
      string str = string.Empty;
      if (length < 0)
      {
        length = shortTimePattern.IndexOfAny(new char[2]
        {
          ':',
          '.'
        });
        if (length < 0)
          str = "H";
      }
      this.FillHours(this.containerHours.ContentElement, 60, this.ColumnsCount, length < 0 ? str : shortTimePattern.Substring(0, length), this.IsAmPmMode());
      this.FillMinutes(this.containerMinutes.ContentElement, this.Step, this.ColumnsCount);
    }

    protected virtual void FillPopupInSingleTable()
    {
      this.FillHours(this.containerHours.ContentElement, this.Step, this.ColumnsCount, this.Owner.Culture.DateTimeFormat.ShortTimePattern, this.IsAmPmMode());
    }

    protected virtual void ManageSelectedItems(
      GridLayout layout,
      TimeTableVisualElement clickedElement)
    {
      foreach (TimeTableVisualElement child in layout.Children)
        child.Selected = child == clickedElement && !child.Selected;
    }
  }
}
