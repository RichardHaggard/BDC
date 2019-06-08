// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MaskDateTimeProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class MaskDateTimeProvider : IMaskProvider
  {
    private int selectedItemIndex = -1;
    internal DateTime value = DateTime.Now;
    private bool includePrompt = true;
    private char promptChar = ' ';
    private int yearResetValue = 1;
    private int minutesStep = 1;
    private int hoursStep = 1;
    private int secondsStep = 1;
    private int millisecondsStep = 1;
    private bool enableKeyNavigation = true;
    protected int oldSelectedIndex = -1;
    private string mask;
    private CultureInfo culture;
    private RadTextBoxItem textBoxItem;
    private string maskFromFormat;
    private System.Collections.Generic.List<MaskPart> list;
    private DateTimeFormatInfo dateTimeFormatInfo;
    private RadMaskedEditBoxElement owner;
    private bool autoCompleteYear;
    private object oldValue;
    private bool autoSelectNextPart;
    protected int keyCounter;
    private DateTime minDate;
    private DateTime maxDate;
    private bool isAMDesignator;

    public event EventHandler SelectedItemChanged;

    public bool AutoSelectNextPart
    {
      get
      {
        return this.autoSelectNextPart;
      }
      set
      {
        this.autoSelectNextPart = value;
      }
    }

    public bool AutoCompleteYear
    {
      get
      {
        return this.autoCompleteYear;
      }
      set
      {
        this.autoCompleteYear = value;
      }
    }

    public int YearResetValue
    {
      get
      {
        return this.yearResetValue;
      }
      set
      {
        this.yearResetValue = value;
      }
    }

    public int SelectedItemIndex
    {
      get
      {
        return this.selectedItemIndex;
      }
      set
      {
        if (this.selectedItemIndex == value)
          return;
        this.selectedItemIndex = value;
        this.oldSelectedIndex = 0;
        this.OnSelectedItemChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(true)]
    public bool EnableKeyNavigation
    {
      get
      {
        return this.enableKeyNavigation;
      }
      set
      {
        this.enableKeyNavigation = value;
      }
    }

    private void OnSelectedItemChanged(EventArgs eventArgs)
    {
      if (this.SelectedItemChanged == null)
        return;
      this.SelectedItemChanged((object) this, eventArgs);
    }

    public int MinutesStep
    {
      get
      {
        return this.minutesStep;
      }
      set
      {
        this.minutesStep = value;
      }
    }

    public int HoursStep
    {
      get
      {
        return this.hoursStep;
      }
      set
      {
        this.hoursStep = value;
      }
    }

    public int SecondsStep
    {
      get
      {
        return this.secondsStep;
      }
      set
      {
        this.secondsStep = value;
      }
    }

    public int MillisecondsStep
    {
      get
      {
        return this.millisecondsStep;
      }
      set
      {
        this.millisecondsStep = value;
      }
    }

    public virtual object Value
    {
      get
      {
        return (object) this.value;
      }
      set
      {
        if (!string.IsNullOrEmpty(this.textBoxItem.Text) && (this.oldValue == value || this.oldValue != null && this.oldValue.Equals(value)))
          return;
        ValueChangingEventArgs changingEventArgs = new ValueChangingEventArgs(value, this.Value);
        this.owner.CallValueChanging((CancelEventArgs) changingEventArgs);
        if (changingEventArgs.Cancel)
          return;
        this.oldValue = value;
        DateTime result = DateTime.Now;
        string empty = string.Empty;
        try
        {
          empty = value.ToString();
        }
        catch (ArgumentOutOfRangeException ex)
        {
        }
        if (empty == "" || !DateTime.TryParse(empty, out result))
          this.value = !(value is DateTime) ? DateTime.Now : (DateTime) value;
        else if (value is DateTime)
          this.value = (DateTime) value;
        if (this.value > this.MaxSupportedDateTime)
          this.value = this.MaxSupportedDateTime;
        if (this.value < this.MinSupportedDateTime)
          this.value = this.MinSupportedDateTime;
        try
        {
          this.maskFromFormat = MaskDateTimeProvider.GetSpecificFormat(this.mask, this.culture.DateTimeFormat);
          this.textBoxItem.Text = this.value.ToString(this.maskFromFormat, (IFormatProvider) this.culture);
          this.FillCollectionWithValues(this.list, this.value, this.mask);
          this.SelectCurrentItemWithSelectedItem();
        }
        catch (ArgumentOutOfRangeException ex)
        {
        }
        this.owner.isNullValue = false;
        this.owner.CallValueChanged(EventArgs.Empty);
      }
    }

    protected virtual DateTime MaxSupportedDateTime
    {
      get
      {
        return DateTime.MaxValue;
      }
    }

    protected virtual DateTime MinSupportedDateTime
    {
      get
      {
        return DateTime.MinValue;
      }
    }

    public string MaskFromFormat
    {
      get
      {
        return this.maskFromFormat;
      }
      set
      {
        this.maskFromFormat = value;
      }
    }

    public DateTimeFormatInfo DateTimeFormatInfo
    {
      get
      {
        return this.dateTimeFormatInfo;
      }
      set
      {
        this.dateTimeFormatInfo = value;
      }
    }

    public RadMaskedEditBoxElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public DateTime MinDate
    {
      get
      {
        return this.minDate;
      }
      set
      {
        this.minDate = value;
      }
    }

    public DateTime MaxDate
    {
      get
      {
        return this.maxDate;
      }
      set
      {
        this.maxDate = value;
      }
    }

    public MaskDateTimeProvider(string mask, CultureInfo culture, RadMaskedEditBoxElement owner)
    {
      this.owner = owner;
      this.textBoxItem = owner.TextBoxItem;
      this.mask = mask;
      this.culture = culture;
      this.textBoxItem = owner.TextBoxItem;
      this.dateTimeFormatInfo = culture.DateTimeFormat;
      this.maskFromFormat = MaskDateTimeProvider.GetSpecificFormat(mask, culture.DateTimeFormat);
      this.list = MaskDateTimeProvider.FillCollection(this.maskFromFormat, culture.DateTimeFormat);
      this.FillCollectionWithValues(this.list, this.value, this.mask);
      this.SelectedItemIndex = 0;
      this.SelectFirstEditableItem();
    }

    public virtual void KeyDown(object sender, KeyEventArgs e)
    {
      KeyEventArgsWithMinMax eventArgsWithMinMax = e as KeyEventArgsWithMinMax;
      DateTime minDate = this.MinSupportedDateTime;
      DateTime maxDate = this.MaxSupportedDateTime;
      if (e.Handled)
        return;
      if (eventArgsWithMinMax != null)
      {
        minDate = eventArgsWithMinMax.MinDate;
        maxDate = eventArgsWithMinMax.MaxDate;
      }
      if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Space)
      {
        if (this.EnableKeyNavigation)
          this.SelectNextEditableItem();
        e.Handled = true;
      }
      else if (e.KeyCode == Keys.Left)
      {
        if (this.EnableKeyNavigation)
          this.SelectPrevEditableItem();
        e.Handled = true;
      }
      else if (e.KeyCode == Keys.Up)
      {
        this.HandleSpinUp(minDate, maxDate);
        e.Handled = true;
      }
      else if (e.KeyCode == Keys.Down)
      {
        this.HandleSpinDown(minDate, maxDate);
        e.Handled = true;
      }
      else if (e.KeyValue == 46)
      {
        if (this.textBoxItem.SelectionLength != this.textBoxItem.Text.Length)
          this.ResetCurrentPartValue(sender, e);
        else
          this.ResetToMinDate();
        e.Handled = true;
      }
      else if (e.KeyCode == Keys.Home && (e.KeyData & Keys.Shift) == Keys.None)
      {
        if (this.EnableKeyNavigation)
          this.SelectFirstEditableItem();
        e.Handled = true;
      }
      else
      {
        if (e.KeyCode != Keys.End || (e.KeyData & Keys.Shift) != Keys.None)
          return;
        if (this.EnableKeyNavigation)
          this.SelectLastEditableItem();
        e.Handled = true;
      }
    }

    public virtual void ResetToMinDate()
    {
      this.Value = (object) this.MinDate;
    }

    public virtual void KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.Handled)
        return;
      MaskPart part = this.list[this.SelectedItemIndex];
      if ("\\/-:,.".IndexOf(e.KeyChar) > -1)
      {
        int num = this.textBoxItem.Text.IndexOf(e.KeyChar, this.textBoxItem.SelectionStart);
        if (num == -1)
          num = this.textBoxItem.Text.IndexOf(e.KeyChar, 0);
        if (num == -1)
        {
          e.Handled = true;
        }
        else
        {
          if (this.EnableKeyNavigation)
          {
            this.textBoxItem.SelectionStart = num;
            this.SelectNextEditableItemFromCurrentCaret();
          }
          e.Handled = true;
        }
      }
      else if (part.readOnly)
      {
        e.Handled = true;
      }
      else
      {
        if (this.SelectedItemIndex != this.oldSelectedIndex)
        {
          this.keyCounter = 0;
          this.oldSelectedIndex = this.SelectedItemIndex;
        }
        this.ResetOnDelPartValue(part, (int) e.KeyChar);
        switch (part.type)
        {
          case PartTypes.Day:
            int num1 = part.value;
            part = this.SetDayMaxValue(this.list);
            part.value = num1;
            this.HandleKeyPressDay(part, e);
            part.Validate();
            if (part.value == 0)
              part.value = 1;
            this.Value = (object) new DateTime(this.value.Year, this.value.Month, part.value, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
            if (char.IsLetterOrDigit(e.KeyChar))
            {
              ++this.keyCounter;
              break;
            }
            break;
          case PartTypes.MiliSeconds:
            this.HandleKeyPress(part, e);
            part.Validate();
            int num2 = (int) ((double) part.value * Math.Pow(10.0, (double) (3 - part.len)));
            this.Value = (object) new DateTime(this.value.Year, this.value.Month, this.value.Day, this.value.Hour, this.value.Minute, this.value.Second, num2 > part.max ? part.value : num2);
            if (char.IsDigit(e.KeyChar))
            {
              ++this.keyCounter;
              break;
            }
            break;
          case PartTypes.h12:
          case PartTypes.h24:
            this.HandleKeyPressHour(part, e);
            if (this.Value != null && this.MaskFromFormat.Contains("tt"))
            {
              string str = ((DateTime) this.Value).ToString("tt", (IFormatProvider) this.Culture);
              if (str == this.Culture.DateTimeFormat.AMDesignator && part.value > 12)
                part.value -= 12;
              else if (str == this.Culture.DateTimeFormat.PMDesignator && part.value < 12)
                part.value += 12;
            }
            this.Value = (object) new DateTime(this.value.Year, this.value.Month, this.value.Day, part.value, this.value.Minute, this.value.Second, this.value.Millisecond);
            if (char.IsLetterOrDigit(e.KeyChar))
            {
              ++this.keyCounter;
              break;
            }
            break;
          case PartTypes.Minutes:
            this.HandleKeyPress(part, e);
            part.Validate();
            this.Value = (object) new DateTime(this.value.Year, this.value.Month, this.value.Day, this.value.Hour, part.value, this.value.Second, this.value.Millisecond);
            if (char.IsDigit(e.KeyChar))
            {
              ++this.keyCounter;
              break;
            }
            break;
          case PartTypes.Month:
            this.HandleMonthKeyPress(e, part);
            part.Validate();
            MaskPart maskPart1 = this.SetDayMaxValue(this.list);
            int day1 = this.value.Day;
            if (maskPart1 != null)
              day1 = maskPart1.value;
            int day2 = Math.Min(day1, DateTime.DaysInMonth(this.value.Year, part.value));
            this.Value = (object) new DateTime(this.value.Year, part.value, day2, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
            if (char.IsDigit(e.KeyChar))
            {
              ++this.keyCounter;
              break;
            }
            break;
          case PartTypes.Seconds:
            this.HandleKeyPress(part, e);
            part.Validate();
            this.Value = (object) new DateTime(this.value.Year, this.value.Month, this.value.Day, this.value.Hour, this.value.Minute, part.value, this.value.Millisecond);
            if (char.IsLetterOrDigit(e.KeyChar))
            {
              ++this.keyCounter;
              break;
            }
            break;
          case PartTypes.AmPm:
            this.HandleAmPmKeyPress(e);
            if (char.IsLetterOrDigit(e.KeyChar))
            {
              ++this.keyCounter;
              break;
            }
            break;
          case PartTypes.Year:
            this.HandleKeyPress(part, e);
            part.Validate();
            MaskPart maskPart2 = this.SetDayMaxValue(this.list);
            int day3 = this.value.Day;
            if (maskPart2 != null)
              day3 = maskPart2.value;
            this.Value = (object) new DateTime(part.value, this.value.Month, day3, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
            if (char.IsDigit(e.KeyChar))
            {
              ++this.keyCounter;
              break;
            }
            break;
        }
        e.Handled = true;
        this.FillCollectionWithValues(this.list, this.value, this.mask);
        if (!this.AutoSelectNextPart)
        {
          this.RestoreSelectedItem();
        }
        else
        {
          if (this.keyCounter < part.len)
            return;
          if (part.type == PartTypes.Day || part.type == PartTypes.Month || (part.type == PartTypes.h12 || part.type == PartTypes.h24))
          {
            if (this.keyCounter < 2)
              return;
            int selectedItemIndex = this.SelectedItemIndex;
            this.SelectNextEditableItem();
            if (selectedItemIndex == this.SelectedItemIndex)
              this.SelectFirstEditableItem();
            this.keyCounter = 0;
          }
          else
          {
            int selectedItemIndex = this.SelectedItemIndex;
            this.SelectNextEditableItem();
            if (selectedItemIndex == this.SelectedItemIndex)
              this.SelectFirstEditableItem();
            this.keyCounter = 0;
          }
        }
      }
    }

    protected virtual void HandleMonthKeyPress(KeyPressEventArgs e, MaskPart part)
    {
      if (char.IsDigit(e.KeyChar))
        this.HandleKeyPressMonth(part, e);
      else if (part.maskPart.Length == 3)
      {
        string[] monthGenitiveNames = this.dateTimeFormatInfo.AbbreviatedMonthGenitiveNames;
        this.HandleKeyPressWithCharacters(part, e, monthGenitiveNames);
      }
      else if (part.maskPart.Length > 3)
      {
        string[] monthGenitiveNames = this.dateTimeFormatInfo.MonthGenitiveNames;
        this.HandleKeyPressWithCharacters(part, e, monthGenitiveNames);
      }
      else
        this.HandleKeyPressMonth(part, e);
    }

    protected virtual void HandleAmPmKeyPress(KeyPressEventArgs e)
    {
      char ch1 = this.culture.DateTimeFormat.AMDesignator.ToLower()[0];
      char ch2 = this.culture.DateTimeFormat.PMDesignator.ToLower()[0];
      if ((int) ch1 == (int) char.ToLower(e.KeyChar))
      {
        if (this.value.Hour <= 12)
          return;
        this.Value = (object) this.value.AddHours(-12.0);
      }
      else
      {
        if ((int) ch2 != (int) char.ToLower(e.KeyChar) || this.value.Hour >= 12)
          return;
        this.Value = (object) this.value.AddHours(12.0);
      }
    }

    public virtual void ResetCurrentPartValue(object sender, KeyEventArgs e)
    {
      MaskPart part = this.list[this.SelectedItemIndex];
      if (part.readOnly)
      {
        e.Handled = true;
      }
      else
      {
        this.ResetOnDelPartValue(part, e.KeyValue);
        switch (part.type)
        {
          case PartTypes.Day:
            this.ResetDay(part);
            break;
          case PartTypes.MiliSeconds:
            part.Validate();
            this.Value = (object) new DateTime(this.value.Year, this.value.Month, this.value.Day, this.value.Hour, this.value.Minute, this.value.Second, part.value);
            break;
          case PartTypes.h12:
          case PartTypes.h24:
            part.Validate();
            this.Value = (object) new DateTime(this.value.Year, this.value.Month, this.value.Day, part.value, this.value.Minute, this.value.Second, this.value.Millisecond);
            break;
          case PartTypes.Minutes:
            part.Validate();
            this.Value = (object) new DateTime(this.value.Year, this.value.Month, this.value.Day, this.value.Hour, part.value, this.value.Second, this.value.Millisecond);
            break;
          case PartTypes.Month:
            this.ResetMonth(part);
            break;
          case PartTypes.Seconds:
            part.Validate();
            this.Value = (object) new DateTime(this.value.Year, this.value.Month, this.value.Day, this.value.Hour, this.value.Minute, part.value, this.value.Millisecond);
            break;
          case PartTypes.Year:
            this.ResetYear(part);
            break;
        }
        e.Handled = true;
        this.FillCollectionWithValues(this.list, this.value, this.mask);
        this.RestoreSelectedItem();
      }
    }

    protected virtual void ResetMonth(MaskPart part)
    {
      part.Validate();
      MaskPart maskPart = this.SetDayMaxValue(this.list);
      int day = this.value.Day;
      if (maskPart != null)
        day = maskPart.value;
      this.Value = (object) new DateTime(this.value.Year, part.value, day, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
    }

    protected virtual void ResetDay(MaskPart part)
    {
      this.SetDayMaxValue(this.list);
      part.Validate();
      this.Value = (object) new DateTime(this.value.Year, this.value.Month, part.value, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
    }

    protected virtual void ResetYear(MaskPart part)
    {
      part.Validate();
      MaskPart maskPart = this.SetDayMaxValue(this.list);
      int day = this.value.Day;
      if (maskPart != null)
        day = maskPart.value;
      this.Value = (object) new DateTime(part.value, this.value.Month, day, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
    }

    protected virtual void HandleKeyPressWithCharacters(
      MaskPart part,
      KeyPressEventArgs e,
      string[] names)
    {
      string lower = e.KeyChar.ToString().ToLower();
      for (int index = part.value; index < names.Length; ++index)
      {
        if (names[index].ToLower().StartsWith(lower))
        {
          part.value = index + 1;
          return;
        }
      }
      for (int index = 0; index < part.value; ++index)
      {
        if (names[index].ToLower().StartsWith(lower))
        {
          part.value = index + 1;
          break;
        }
      }
    }

    private void HandleKeyPressMonth(MaskPart part, KeyPressEventArgs e)
    {
      this.HandleKeyPressDay(part, e);
    }

    protected virtual void HandleKeyPressDay(MaskPart part, KeyPressEventArgs e)
    {
      int result = 0;
      if (!int.TryParse(e.KeyChar.ToString(), out result))
        return;
      string str = part.value.ToString();
      int num1 = 0;
      int num2;
      if (part.hasZero)
      {
        if (num1 < part.min || num1 > part.max)
        {
          num2 = result;
        }
        else
        {
          if (str.Length > 1)
            str = str.Substring(1);
          num2 = int.Parse(str + (object) e.KeyChar);
        }
      }
      else
      {
        if (str.Length > 1)
          str = str.Substring(1);
        num2 = int.Parse(str + (object) e.KeyChar);
      }
      part.value = num2 < part.min || num2 > part.max ? result : num2;
      part.hasZero = result == 0;
    }

    protected virtual bool HandleKeyPress(MaskPart part, KeyPressEventArgs e)
    {
      int result = 0;
      if (!int.TryParse(e.KeyChar.ToString(), out result))
        return true;
      string empty = string.Empty;
      if (part.type == PartTypes.MiliSeconds)
      {
        int num = (int) ((double) part.value / Math.Pow(10.0, (double) (3 - part.len))) * 10 + result;
        part.value = num % (int) Math.Pow(10.0, (double) part.len);
      }
      else
      {
        string str = part.value.ToString();
        if (str.Length == part.len && str.Length >= part.maskPart.Length)
          str = str.Substring(1);
        if (this.AutoCompleteYear && str.Length == 1)
          str = string.Empty;
        part.value = int.Parse(str + (object) e.KeyChar);
      }
      if (part.type == PartTypes.Year && part.maskPart.Length == 2)
      {
        int length = part.value.ToString().Length;
        int fourDigitYear = Thread.CurrentThread.CurrentCulture.Calendar.ToFourDigitYear(int.Parse(part.value.ToString().Substring(length - 2, 2)));
        part.value = fourDigitYear;
      }
      if (this.AutoCompleteYear && part.type == PartTypes.Year)
      {
        int year = part.value;
        int length = year.ToString().Length;
        if (length > 4)
          year = int.Parse(part.value.ToString().Substring(length - 2, 2));
        part.value = Thread.CurrentThread.CurrentCulture.Calendar.ToFourDigitYear(year);
      }
      if (part.value > part.max || part.value < part.min)
        part.value = result;
      return true;
    }

    protected virtual bool IsAmPmMode()
    {
      this.isAMDesignator = !string.IsNullOrEmpty(this.owner.Culture.DateTimeFormat.AMDesignator) && this.textBoxItem.Text.Contains(this.owner.Culture.DateTimeFormat.AMDesignator);
      bool flag = !string.IsNullOrEmpty(this.owner.Culture.DateTimeFormat.PMDesignator) && this.textBoxItem.Text.Contains(this.owner.Culture.DateTimeFormat.PMDesignator);
      if (!this.isAMDesignator)
        return flag;
      return true;
    }

    protected virtual void HandleKeyPressYear(MaskPart part, KeyPressEventArgs e)
    {
      int result = 0;
      if (!int.TryParse(e.KeyChar.ToString(), out result))
        return;
      string str = part.value.ToString();
      if (str.Length == part.len && str.Length >= part.maskPart.Length)
        str = str.Substring(1);
      part.value = int.Parse(str + (object) e.KeyChar);
      if (part.type == PartTypes.Year && part.maskPart.Length == 2)
      {
        int length = part.value.ToString().Length;
        part.value = int.Parse(string.Format("{0}{1}", (object) DateTime.Now.Year.ToString().Substring(0, 2), (object) part.value.ToString().Substring(length - 2, 2)));
      }
      if (part.type == PartTypes.Year)
      {
        int length = part.value.ToString().Length;
        part.value = int.Parse(part.value.ToString().Substring(length - 2, 2));
        if (part.value >= 50 && part.value <= 99)
          part.value += 1900;
        else if (part.value >= 500 && part.value <= 999)
          part.value += 1000;
        else if (part.value < 50 || part.value > 99 && part.value < 500)
          part.value += 2000;
      }
      if (part.value <= part.max && part.value >= part.min)
        return;
      part.value = result;
    }

    protected virtual void HandleKeyPressHour(MaskPart part, KeyPressEventArgs e)
    {
      int result = 0;
      if (!int.TryParse(e.KeyChar.ToString(), out result))
        return;
      string str = part.value.ToString();
      if (str.Length == 2)
        str = str.Substring(1);
      int num = -1;
      if (this.IsAmPmMode())
        num = part.value;
      part.value = int.Parse(str + (object) e.KeyChar);
      if (part.value > part.max || part.value < part.min)
        part.value = result;
      if (num == -1)
        return;
      if (part.value >= 12 && this.isAMDesignator)
      {
        part.value -= 12;
      }
      else
      {
        if (part.value >= 12 || this.isAMDesignator)
          return;
        part.value += 12;
      }
    }

    protected virtual bool ResetOnDelPartValue(MaskPart part, int keyChar)
    {
      switch (keyChar)
      {
        case 8:
          part.value = part.type != PartTypes.Year ? part.min : this.YearResetValue;
          this.SelectPrevItem();
          return true;
        case 46:
          part.value = part.type != PartTypes.Year ? part.min : this.YearResetValue;
          return true;
        default:
          return false;
      }
    }

    public virtual void HandleSpinUp(DateTime minDate, DateTime maxDate)
    {
      if (this.textBoxItem.ContainsFocus)
        this.SelectCurrentItemFromCurrentCaret();
      this.Up(this.list[this.SelectedItemIndex], minDate, maxDate);
    }

    public virtual void HandleSpinDown(DateTime minDate, DateTime maxDate)
    {
      if (this.textBoxItem.ContainsFocus)
        this.SelectCurrentItemFromCurrentCaret();
      this.Down(this.list[this.SelectedItemIndex], minDate, maxDate);
    }

    protected virtual void RestoreSelectedItem()
    {
      try
      {
        this.textBoxItem.Text = this.value.ToString(this.mask, (IFormatProvider) this.culture);
      }
      catch (ArgumentOutOfRangeException ex)
      {
      }
      int selectedItemIndex = this.SelectedItemIndex;
      this.FillCollectionWithValues(this.list, this.value, this.mask);
      this.SelectedItemIndex = selectedItemIndex;
      this.SelectCurrentItemWithSelectedItem();
    }

    public virtual void Up(MaskPart part, DateTime minDate, DateTime maxDate)
    {
      if (part.readOnly)
        return;
      try
      {
        if (this.value < minDate)
          this.value = minDate;
        DateTime dateTime = this.Value == null ? DateTime.MaxValue : (DateTime) this.Value;
        switch (part.type)
        {
          case PartTypes.Day:
            dateTime = this.value.AddDays(1.0);
            break;
          case PartTypes.MiliSeconds:
            dateTime = this.value.AddMilliseconds((double) this.MillisecondsStep);
            break;
          case PartTypes.h12:
          case PartTypes.h24:
            dateTime = this.value.AddHours((double) this.HoursStep);
            break;
          case PartTypes.Minutes:
            dateTime = this.value.AddMinutes((double) this.MinutesStep);
            break;
          case PartTypes.Month:
            dateTime = this.culture.Calendar.AddMonths(this.value, 1);
            break;
          case PartTypes.Seconds:
            dateTime = this.value.AddSeconds((double) this.SecondsStep);
            break;
          case PartTypes.AmPm:
            if (this.value.Hour >= 12)
            {
              dateTime = this.value.AddHours(-12.0);
              break;
            }
            if (this.value.Hour < 12)
            {
              dateTime = this.value.AddHours(12.0);
              break;
            }
            break;
          case PartTypes.Year:
            dateTime = this.culture.Calendar.AddYears(this.value, 1);
            break;
        }
        if (dateTime <= maxDate)
          this.Value = (object) dateTime;
      }
      catch (ArgumentOutOfRangeException ex)
      {
      }
      this.FillCollectionWithValues(this.list, this.value, this.mask);
      this.RestoreSelectedItem();
    }

    public virtual void Down(MaskPart part, DateTime minDate, DateTime maxDate)
    {
      if (part.readOnly)
        return;
      try
      {
        DateTime dateTime = this.Value == null ? DateTime.MinValue : (DateTime) this.Value;
        if (this.value > maxDate)
          this.value = maxDate;
        switch (part.type)
        {
          case PartTypes.Day:
            dateTime = this.value.AddDays(-1.0);
            break;
          case PartTypes.MiliSeconds:
            dateTime = this.value.AddMilliseconds((double) -this.MillisecondsStep);
            break;
          case PartTypes.h12:
          case PartTypes.h24:
            dateTime = this.value.AddHours((double) -this.HoursStep);
            break;
          case PartTypes.Minutes:
            dateTime = this.value.AddMinutes((double) -this.MinutesStep);
            break;
          case PartTypes.Month:
            dateTime = this.culture.Calendar.AddMonths(this.value, -1);
            break;
          case PartTypes.Seconds:
            dateTime = this.value.AddSeconds((double) -this.SecondsStep);
            break;
          case PartTypes.AmPm:
            if (this.value.Hour > 12)
            {
              dateTime = this.value.AddHours(-12.0);
              break;
            }
            if (this.value.Hour <= 12)
            {
              dateTime = this.value.AddHours(12.0);
              break;
            }
            break;
          case PartTypes.Year:
            dateTime = this.culture.Calendar.AddYears(this.value, -1);
            break;
        }
        if (dateTime >= minDate)
          this.Value = (object) dateTime;
      }
      catch (ArgumentOutOfRangeException ex)
      {
      }
      this.FillCollectionWithValues(this.list, this.value, this.mask);
      this.RestoreSelectedItem();
    }

    public virtual void SelectCurrentItemWithSelectedItem()
    {
      for (int selectedItemIndex = this.SelectedItemIndex; selectedItemIndex < this.list.Count; ++selectedItemIndex)
      {
        MaskPart part = this.list[selectedItemIndex];
        if (this.SelectMilliseconds(selectedItemIndex, part))
          break;
        if (part.type != PartTypes.Character && !part.readOnly)
        {
          this.textBoxItem.SelectionStart = part.offset;
          this.textBoxItem.SelectionLength = part.len;
          this.SelectedItemIndex = selectedItemIndex;
          break;
        }
      }
    }

    public virtual bool SelectCurrentItemFromCurrentCaret()
    {
      int selectionStart = this.textBoxItem.SelectionStart;
      int num = 0;
      bool flag = false;
      for (int i = 0; i < this.list.Count; ++i)
      {
        MaskPart part = this.list[i];
        if (!this.SelectMilliseconds(i, part))
        {
          if (selectionStart >= part.offset && selectionStart < part.offset + part.len && (!part.readOnly && part.type != PartTypes.Character))
          {
            this.textBoxItem.SelectionStart = this.list[i].offset;
            this.textBoxItem.SelectionLength = this.list[i].len;
            this.SelectedItemIndex = i;
            flag = true;
            break;
          }
          num += part.len;
        }
        else
          break;
      }
      return flag;
    }

    private bool SelectMilliseconds(int i, MaskPart part)
    {
      if (part.type != PartTypes.MiliSeconds || this.value.Millisecond % 10 != 0 || !part.trimsZeros)
        return false;
      int len = part.len;
      for (int index = 1; index <= part.len && (double) this.value.Millisecond % Math.Pow(10.0, (double) index) == 0.0; ++index)
        --len;
      this.textBoxItem.SelectionStart = this.list[i].offset;
      this.textBoxItem.SelectionLength = len;
      this.SelectedItemIndex = i;
      return true;
    }

    public virtual bool SelectNextEditableItemFromCurrentCaret()
    {
      int selectionStart = this.textBoxItem.SelectionStart;
      int num = 0;
      bool flag = false;
      for (int index = 0; index < this.list.Count; ++index)
      {
        MaskPart maskPart = this.list[index];
        if (selectionStart >= maskPart.offset && selectionStart <= maskPart.offset + maskPart.len)
        {
          this.SelectedItemIndex = index;
          flag = true;
          this.SelectNextEditableItem();
          break;
        }
        num += maskPart.len;
      }
      this.keyCounter = 0;
      return flag;
    }

    public virtual void SelectPrevItemFromCurrentCaret()
    {
      int num1 = this.textBoxItem.SelectionStart + this.textBoxItem.SelectionLength;
      int num2 = 0;
      for (int index = 1; index < this.list.Count; ++index)
      {
        MaskPart maskPart = this.list[index];
        num2 += maskPart.len;
        if (num1 >= maskPart.offset && num1 <= maskPart.offset + maskPart.len)
        {
          this.textBoxItem.SelectionStart = this.list[index - 1].offset;
          this.textBoxItem.SelectionLength = this.list[index - 1].len;
          this.SelectedItemIndex = index - 1;
          break;
        }
      }
      this.keyCounter = 0;
    }

    public virtual void SelectNextItemFromCurrentCaret()
    {
      int num1 = this.textBoxItem.SelectionStart + this.textBoxItem.SelectionLength;
      int num2 = 0;
      for (int index = 0; index < this.list.Count - 1; ++index)
      {
        MaskPart maskPart = this.list[index];
        num2 += maskPart.len;
        if (num1 >= maskPart.offset && num1 <= maskPart.offset + maskPart.len)
        {
          this.textBoxItem.SelectionStart = this.list[index + 1].offset;
          this.textBoxItem.SelectionLength = this.list[index + 1].len;
          this.SelectedItemIndex = index + 1;
          break;
        }
      }
      this.keyCounter = 0;
    }

    public virtual void SelectPrevItem()
    {
      for (int index = this.SelectedItemIndex - 1; index >= 0; --index)
      {
        MaskPart maskPart = this.list[index];
        if (maskPart.type != PartTypes.Character && !maskPart.readOnly)
        {
          this.textBoxItem.SelectionStart = maskPart.offset;
          this.textBoxItem.SelectionLength = maskPart.len;
          this.SelectedItemIndex = index;
          break;
        }
      }
      this.keyCounter = 0;
    }

    public virtual void SelectNextItem()
    {
      for (int index = this.SelectedItemIndex + 1; index < this.list.Count; ++index)
      {
        MaskPart maskPart = this.list[index];
        if (maskPart.type != PartTypes.Character && !maskPart.readOnly)
        {
          this.textBoxItem.SelectionStart = maskPart.offset;
          this.textBoxItem.SelectionLength = maskPart.len;
          this.SelectedItemIndex = index;
          break;
        }
      }
      this.keyCounter = 0;
    }

    public virtual void SelectLastItem()
    {
      this.SelectedItemIndex = this.list.Count - 1;
      this.oldSelectedIndex = -1;
      if (this.list[this.SelectedItemIndex].type != PartTypes.Character && !this.list[this.SelectedItemIndex].readOnly)
      {
        this.textBoxItem.SelectionStart = this.list[this.SelectedItemIndex].offset;
        this.textBoxItem.SelectionLength = this.list[this.SelectedItemIndex].len;
        this.oldSelectedIndex = -1;
      }
      this.keyCounter = 0;
    }

    public virtual void SelectFirstItem()
    {
      this.SelectedItemIndex = 0;
      this.oldSelectedIndex = -1;
      if (this.list[this.SelectedItemIndex].type != PartTypes.Character && !this.list[this.SelectedItemIndex].readOnly)
      {
        this.textBoxItem.SelectionStart = this.list[this.SelectedItemIndex].offset;
        this.textBoxItem.SelectionLength = this.list[this.SelectedItemIndex].len;
        this.oldSelectedIndex = -1;
      }
      this.keyCounter = 0;
    }

    public virtual void SelectFirstEditableItem()
    {
      int num = -1;
      this.SelectFirstItem();
      while (this.list[this.SelectedItemIndex].readOnly && this.SelectedItemIndex != num)
        this.SelectNextItem();
      this.keyCounter = 0;
    }

    public virtual void SelectLastEditableItem()
    {
      int num = -1;
      this.SelectLastItem();
      while (this.list[this.SelectedItemIndex].readOnly && this.SelectedItemIndex != num)
        this.SelectPrevEditableItem();
      this.keyCounter = 0;
    }

    public virtual void SelectPrevEditableItem()
    {
      int num = -1;
      this.SelectPrevItem();
      while (this.list[this.SelectedItemIndex].readOnly && this.SelectedItemIndex != num)
      {
        if (this.list[this.SelectedItemIndex].readOnly && this.SelectedItemIndex == 0)
        {
          this.SelectNextEditableItem();
          break;
        }
        this.SelectPrevItem();
      }
      this.keyCounter = 0;
    }

    public virtual void SelectNextEditableItem()
    {
      int num = -1;
      this.SelectNextItem();
      while (this.list[this.SelectedItemIndex].readOnly && this.SelectedItemIndex != num)
      {
        if (this.list[this.SelectedItemIndex].readOnly && this.SelectedItemIndex + 1 == this.list.Count)
        {
          this.SelectPrevEditableItem();
          break;
        }
        this.SelectNextItem();
      }
      this.keyCounter = 0;
    }

    public virtual bool Click()
    {
      if (!this.SelectCurrentItemFromCurrentCaret())
        this.SelectNextEditableItemFromCurrentCaret();
      DateTime result;
      return DateTime.TryParse(this.textBoxItem.Text, (IFormatProvider) this.culture, DateTimeStyles.None, out result);
    }

    public virtual bool Validate(string stringValue)
    {
      CancelEventArgs e = new CancelEventArgs();
      this.owner.CallValueChanging(e);
      if (e.Cancel)
        return false;
      this.maskFromFormat = MaskDateTimeProvider.GetSpecificFormat(this.mask, this.culture.DateTimeFormat);
      if (DateTime.TryParse(stringValue, (IFormatProvider) this.culture, DateTimeStyles.None, out this.value))
      {
        this.textBoxItem.Text = this.value.ToString(this.maskFromFormat, (IFormatProvider) this.culture);
        this.FillCollectionWithValues(this.list, this.value, this.mask);
        this.owner.isNullValue = false;
        this.owner.CallValueChanged(EventArgs.Empty);
        return true;
      }
      this.textBoxItem.Text = DateTime.Now.ToString(this.maskFromFormat, (IFormatProvider) this.culture);
      return false;
    }

    public RadTextBoxItem TextBoxItem
    {
      get
      {
        return this.textBoxItem;
      }
    }

    public System.Collections.Generic.List<MaskPart> List
    {
      get
      {
        return this.list;
      }
      set
      {
        this.list = value;
      }
    }

    public string ToString(bool includePromt, bool includeLiterals)
    {
      return this.value.ToString(this.mask, (IFormatProvider) this.culture);
    }

    public IMaskProvider Clone()
    {
      return (IMaskProvider) new MaskDateTimeProvider(this.mask, this.culture, this.owner);
    }

    public CultureInfo Culture
    {
      get
      {
        return this.culture;
      }
    }

    public string Mask
    {
      get
      {
        return this.mask;
      }
    }

    public bool IncludePrompt
    {
      get
      {
        return this.includePrompt;
      }
      set
      {
        this.includePrompt = value;
      }
    }

    public char PromptChar
    {
      get
      {
        return this.promptChar;
      }
      set
      {
        this.promptChar = value;
      }
    }

    public static string GetSpecificFormat(string format, DateTimeFormatInfo info)
    {
      if (format == null || format.Length == 0)
        format = "G";
      if (format.Length == 1)
      {
        switch (format[0])
        {
          case 'D':
            return info.LongDatePattern;
          case 'F':
            return info.FullDateTimePattern;
          case 'G':
            return info.ShortDatePattern + (object) ' ' + info.LongTimePattern;
          case 'M':
          case 'm':
            return info.MonthDayPattern;
          case 'R':
          case 'r':
            return info.RFC1123Pattern;
          case 'T':
            return info.LongTimePattern;
          case 'Y':
          case 'y':
            return info.YearMonthPattern;
          case 'd':
            return info.ShortDatePattern;
          case 'f':
            return info.LongDatePattern + (object) ' ' + info.ShortTimePattern;
          case 'g':
            return info.ShortDatePattern + (object) ' ' + info.ShortTimePattern;
          case 's':
            return info.SortableDateTimePattern;
          case 't':
            return info.ShortTimePattern;
          case 'u':
            return info.UniversalSortableDateTimePattern;
        }
      }
      if (format.Length == 2 && format[0] == '%')
        format = format.Substring(1);
      return format;
    }

    protected static int GetGroupLengthByMask(string mask)
    {
      for (int index = 1; index < mask.Length; ++index)
      {
        if ((int) mask[index] != (int) mask[0])
          return index;
      }
      return mask.Length;
    }

    protected virtual void FillCollectionWithValues(
      System.Collections.Generic.List<MaskPart> collection,
      DateTime dateTime,
      string mask)
    {
      for (int index = 0; index < collection.Count; ++index)
      {
        MaskPart maskPart = collection[index];
        switch (maskPart.type)
        {
          case PartTypes.ReadOnly:
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
            {
              maskPart.len = maskPart.value.ToString().Length;
              break;
            }
            break;
          case PartTypes.Day:
            try
            {
              maskPart.value = this.culture.Calendar.GetDayOfMonth(dateTime);
              maskPart.len = maskPart.maskPart.Length;
              if (maskPart.len < maskPart.value.ToString().Length)
                maskPart.len = maskPart.value.ToString().Length;
              if (maskPart.maskPart.Length == 3)
              {
                maskPart.len = this.dateTimeFormatInfo.AbbreviatedDayNames[(int) dateTime.DayOfWeek].Length;
                break;
              }
              if (maskPart.maskPart.Length > 3)
              {
                maskPart.len = this.dateTimeFormatInfo.DayNames[(int) dateTime.DayOfWeek].Length;
                break;
              }
              break;
            }
            catch (ArgumentOutOfRangeException ex)
            {
              break;
            }
          case PartTypes.MiliSeconds:
            maskPart.value = (int) this.culture.Calendar.GetMilliseconds(dateTime);
            maskPart.max = 999;
            break;
          case PartTypes.h12:
            int num = this.culture.Calendar.GetHour(dateTime) % 12;
            if (num == 0)
              num = 12;
            maskPart.value = num;
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
              maskPart.len = maskPart.value.ToString().Length;
            maskPart.max = 12;
            break;
          case PartTypes.h24:
            maskPart.value = this.culture.Calendar.GetHour(dateTime);
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
              maskPart.len = maskPart.value.ToString().Length;
            maskPart.max = 23;
            break;
          case PartTypes.Minutes:
            maskPart.value = this.culture.Calendar.GetMinute(dateTime);
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
              maskPart.len = maskPart.value.ToString().Length;
            maskPart.max = 59;
            break;
          case PartTypes.Month:
            try
            {
              maskPart.value = this.culture.Calendar.GetMonth(dateTime);
              maskPart.len = maskPart.maskPart.Length;
              if (maskPart.len < maskPart.value.ToString().Length)
                maskPart.len = maskPart.value.ToString().Length;
              if (maskPart.maskPart.Length == 3)
                maskPart.len = this.dateTimeFormatInfo.AbbreviatedMonthGenitiveNames[maskPart.value - 1].Length;
              else if (maskPart.maskPart.Length > 3)
                maskPart.len = this.dateTimeFormatInfo.MonthGenitiveNames[maskPart.value - 1].Length;
              maskPart.min = 1;
              maskPart.max = 12;
              break;
            }
            catch (ArgumentOutOfRangeException ex)
            {
              break;
            }
          case PartTypes.Seconds:
            maskPart.value = this.culture.Calendar.GetSecond(dateTime);
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
              maskPart.len = maskPart.value.ToString().Length;
            maskPart.max = 59;
            break;
          case PartTypes.AmPm:
            maskPart.value = this.culture.Calendar.GetHour(dateTime) / 12;
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
            {
              maskPart.len = maskPart.value.ToString().Length;
              break;
            }
            break;
          case PartTypes.Year:
            try
            {
              maskPart.value = this.culture.Calendar.GetYear(dateTime);
              maskPart.len = maskPart.maskPart.Length;
              if (maskPart.maskPart == "y")
                maskPart.len = 2;
              maskPart.min = DateTime.MinValue.Year;
              maskPart.max = DateTime.MaxValue.Year;
              break;
            }
            catch (ArgumentOutOfRangeException ex)
            {
              break;
            }
        }
      }
      this.AdjustItemsPossitionOffset(collection);
      this.SetDayMaxValue(collection);
    }

    protected virtual void AdjustItemsPossitionOffset(System.Collections.Generic.List<MaskPart> collection)
    {
      int num = 0;
      for (int index = 0; index < collection.Count; ++index)
      {
        collection[index].offset = num;
        num += collection[index].len;
      }
    }

    protected virtual MaskPart SetDayMaxValue(System.Collections.Generic.List<MaskPart> collection)
    {
      int year = DateTime.Now.Year;
      int month = DateTime.Now.Month;
      MaskPart maskPart = (MaskPart) null;
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index].type == PartTypes.Day)
          maskPart = collection[index];
        if (collection[index].type == PartTypes.Year)
          year = collection[index].value;
        if (collection[index].type == PartTypes.Month)
          month = collection[index].value;
      }
      if (maskPart != null)
      {
        if (month == 0)
          month = 1;
        maskPart.max = DateTime.DaysInMonth(year, month);
      }
      maskPart?.Validate();
      return maskPart;
    }

    protected static System.Collections.Generic.List<MaskPart> FillCollection(
      string mask,
      DateTimeFormatInfo dateTimeFormatInfo)
    {
      System.Collections.Generic.List<MaskPart> maskPartList = new System.Collections.Generic.List<MaskPart>();
      string mask1 = mask;
      int num1 = 0;
      while (mask1.Length > 0)
      {
        int num2 = MaskDateTimeProvider.GetGroupLengthByMask(mask1);
        MaskPart maskPart = new MaskPart();
        switch (mask1[0])
        {
          case '"':
          case '\'':
            int num3 = mask1.IndexOf(mask1[0], 1);
            maskPart.type = PartTypes.Character;
            maskPart.maskPart = mask1.Substring(1, Math.Max(1, num3 - 1));
            num2 = Math.Max(1, num3 + 1);
            break;
          case '/':
          case ':':
            num2 = 1;
            maskPart.readOnly = true;
            maskPart.maskPart = mask1.Substring(0, 1);
            break;
          case 'D':
          case 'd':
            if (num2 > 2)
            {
              maskPart.readOnly = true;
              maskPart.type = PartTypes.Day;
              maskPart.maskPart = mask1.Substring(0, num2);
            }
            else
            {
              maskPart.month = true;
              maskPart.year = true;
              maskPart.day = true;
              maskPart.type = PartTypes.Day;
              maskPart.maskPart = mask1.Substring(0, num2);
            }
            maskPart.min = 1;
            break;
          case 'F':
          case 'f':
            if (num2 > 3)
              throw new ArgumentException("Masks with more than three 'F' or 'f' are not supported.");
            if (mask1[0] == 'F')
              maskPart.trimsZeros = true;
            maskPart.type = PartTypes.MiliSeconds;
            maskPart.maskPart = mask1.Substring(0, num2);
            maskPart.len = num2;
            break;
          case 'H':
            maskPart.type = PartTypes.h24;
            maskPart.maskPart = mask1.Substring(0, num2);
            break;
          case 'M':
            if (num2 > 4)
              num2 = 4;
            maskPart.type = PartTypes.Month;
            maskPart.maskPart = mask1.Substring(0, num2);
            maskPart.month = true;
            break;
          case 'S':
          case 's':
            maskPart.type = PartTypes.Seconds;
            maskPart.maskPart = mask1.Substring(0, num2);
            break;
          case 'T':
          case 't':
            maskPart.type = PartTypes.AmPm;
            maskPart.maskPart = mask1.Substring(0, num2);
            break;
          case 'Y':
          case 'y':
            maskPart.type = PartTypes.Year;
            maskPart.maskPart = mask1.Substring(0, num2);
            maskPart.year = true;
            break;
          case '\\':
            if (mask1.Length >= 2)
            {
              maskPart.type = PartTypes.Character;
              maskPart.maskPart = mask1.Substring(1, 1);
              num2 = 1;
              break;
            }
            break;
          case 'g':
            maskPart.readOnly = true;
            maskPart.maskPart = mask1.Substring(0, num2);
            break;
          case 'h':
            maskPart.type = PartTypes.h12;
            maskPart.maskPart = mask1.Substring(0, num2);
            break;
          case 'm':
            maskPart.type = PartTypes.Minutes;
            maskPart.maskPart = mask1.Substring(0, num2);
            break;
          case 'z':
            maskPart.readOnly = true;
            maskPart.maskPart = mask1.Substring(0, num2);
            break;
          default:
            num2 = 1;
            maskPart.type = PartTypes.Character;
            maskPart.maskPart = mask1.Substring(0, 1);
            break;
        }
        maskPart.offset = num1;
        num1 += maskPart.maskPart.Length;
        maskPartList.Add(maskPart);
        mask1 = mask1.Substring(num2);
        maskPart.len = maskPart.maskPart.Length;
      }
      return maskPartList;
    }

    public bool Delete()
    {
      this.ResetCurrentPartValue((object) this, new KeyEventArgs(Keys.Delete));
      return true;
    }

    internal virtual void ValidateRange()
    {
      int selectionStart = this.textBoxItem.SelectionStart;
      if (this.value < this.MinDate)
      {
        this.value = this.MinDate;
        this.textBoxItem.Text = this.value.ToString(this.maskFromFormat, (IFormatProvider) this.culture);
      }
      if (this.value > this.MaxDate)
      {
        this.value = this.MaxDate;
        this.textBoxItem.Text = this.value.ToString(this.maskFromFormat, (IFormatProvider) this.culture);
      }
      this.textBoxItem.SelectionStart = selectionStart;
      this.SelectCurrentItemFromCurrentCaret();
    }
  }
}
