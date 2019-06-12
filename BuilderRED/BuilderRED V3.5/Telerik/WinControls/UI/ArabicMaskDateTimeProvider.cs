// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ArabicMaskDateTimeProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ArabicMaskDateTimeProvider : MaskDateTimeProvider
  {
    private readonly DateTime minTime = DateTime.MinValue;
    private readonly DateTime maxTime = DateTime.MaxValue;
    private readonly bool isFarsi;

    public ArabicMaskDateTimeProvider(
      string mask,
      CultureInfo culture,
      RadMaskedEditBoxElement owner)
      : base(mask, culture, owner)
    {
      this.minTime = this.Culture.Calendar.MinSupportedDateTime;
      this.maxTime = this.Culture.Calendar.MaxSupportedDateTime;
      this.isFarsi = culture.ThreeLetterISOLanguageName == "fas" || culture.Calendar is HijriCalendar;
    }

    protected override DateTime MaxSupportedDateTime
    {
      get
      {
        if (!this.isFarsi)
          return DateTime.MaxValue;
        return this.maxTime;
      }
    }

    protected override DateTime MinSupportedDateTime
    {
      get
      {
        if (!this.isFarsi)
          return DateTime.MinValue;
        return this.minTime;
      }
    }

    public override void KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.Handled)
        return;
      MaskPart part = this.List[this.SelectedItemIndex];
      if ("\\/-:,.".IndexOf(e.KeyChar) > -1)
      {
        int num = this.TextBoxItem.Text.IndexOf(e.KeyChar, this.TextBoxItem.SelectionStart);
        if (num == -1)
          num = this.TextBoxItem.Text.IndexOf(e.KeyChar, 0);
        if (num == -1)
        {
          e.Handled = true;
        }
        else
        {
          if (this.EnableKeyNavigation)
          {
            this.TextBoxItem.SelectionStart = num;
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
        if (this.ResetOnDelPartValue(part, (int) e.KeyChar))
          e.Handled = true;
        switch (part.type)
        {
          case PartTypes.Day:
            int num1 = part.value;
            part = this.SetDayMaxValue(this.List);
            part.value = num1;
            this.HandleKeyPressDay(part, e);
            part.Validate();
            if (part.value == 0)
              part.value = 1;
            int num2 = 29;
            try
            {
              num2 = this.Culture.Calendar.GetDaysInMonth(this.Culture.Calendar.GetYear(this.value), this.value.Month);
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
            if (part.value > num2)
              part.value = num2;
            this.Value = (object) this.Culture.Calendar.ToDateTime(this.Culture.Calendar.GetYear(this.value), this.Culture.Calendar.GetMonth(this.value), part.value, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
            if (char.IsLetterOrDigit(e.KeyChar))
            {
              ++this.keyCounter;
              break;
            }
            break;
          case PartTypes.MiliSeconds:
            this.HandleKeyPress(part, e);
            part.Validate();
            int num3 = (int) ((double) part.value * Math.Pow(10.0, (double) (3 - part.len)));
            this.Value = (object) new DateTime(this.value.Year, this.value.Month, this.value.Day, this.value.Hour, this.value.Minute, this.value.Second, num3 > part.max ? part.value : num3);
            if (char.IsDigit(e.KeyChar))
            {
              ++this.keyCounter;
              break;
            }
            break;
          case PartTypes.h12:
          case PartTypes.h24:
            this.HandleKeyPressHour(part, e);
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
            MaskPart maskPart1 = this.SetDayMaxValue(this.List);
            int day1 = this.value.Day;
            if (maskPart1 != null)
              day1 = maskPart1.value;
            DateTime? nullable = new DateTime?();
            string str1 = part.value < 10 ? string.Format("0{0}", (object) part.value.ToString()) : part.value.ToString();
            for (int index = 1; index < 12; ++index)
            {
              string str2 = (string) null;
              try
              {
                nullable = new DateTime?(this.Culture.Calendar.ToDateTime(this.Culture.Calendar.GetYear(this.value), part.value, day1, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond));
                if (nullable.HasValue)
                  str2 = nullable.Value.ToString("MM", (IFormatProvider) this.Culture);
              }
              catch (ArgumentOutOfRangeException ex)
              {
              }
              if (str1 == str2)
              {
                part.value = index;
                break;
              }
            }
            if (nullable.HasValue)
              this.value = nullable.Value;
            this.Value = (object) this.Culture.Calendar.ToDateTime(this.Culture.Calendar.GetYear(this.value), part.value, day1, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
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
            if (!this.HandleKeyPress(part, e))
              return;
            this.keyCounter = 0;
            part.Validate();
            MaskPart maskPart2 = this.SetDayMaxValue(this.List);
            int day2 = this.value.Day;
            if (maskPart2 != null)
            {
              int num4 = maskPart2.value;
            }
            try
            {
              this.Value = (object) this.Culture.Calendar.ToDateTime(part.value, this.Culture.Calendar.GetMonth(this.value), this.Culture.Calendar.GetDayOfMonth(this.value), this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
              break;
            }
            catch (ArgumentOutOfRangeException ex)
            {
              break;
            }
        }
        e.Handled = true;
        this.FillCollectionWithValues(this.List, this.value, this.Mask);
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
            this.SelectNextEditableItem();
            this.keyCounter = 0;
          }
          else
          {
            this.SelectNextEditableItem();
            this.keyCounter = 0;
          }
        }
      }
    }

    protected override void ResetMonth(MaskPart part)
    {
      part.Validate();
      MaskPart maskPart = this.SetDayMaxValue(this.List);
      int day = this.value.Day;
      if (maskPart != null)
        day = maskPart.value;
      DateTime? nullable = new DateTime?();
      string str1 = "01";
      for (int index = 1; index < 12; ++index)
      {
        string str2 = (string) null;
        try
        {
          nullable = new DateTime?(this.Culture.Calendar.ToDateTime(this.Culture.Calendar.GetYear(this.value), part.value, day, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond));
          if (nullable.HasValue)
            str2 = nullable.Value.ToString("MM", (IFormatProvider) this.Culture);
        }
        catch (ArgumentOutOfRangeException ex)
        {
        }
        if (str1 == str2)
        {
          part.value = index;
          break;
        }
      }
      if (nullable.HasValue)
        this.value = nullable.Value;
      this.Value = (object) this.Culture.Calendar.ToDateTime(this.Culture.Calendar.GetYear(this.value), part.value, day, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
    }

    protected override void ResetDay(MaskPart part)
    {
      part.value = 1;
      part.Validate();
      if (part.value == 0)
        part.value = 1;
      int num = 29;
      try
      {
        num = this.Culture.Calendar.GetDaysInMonth(this.Culture.Calendar.GetYear(this.value), this.value.Month);
      }
      catch (ArgumentOutOfRangeException ex)
      {
      }
      if (part.value > num)
        part.value = num;
      this.Value = (object) this.Culture.Calendar.ToDateTime(this.Culture.Calendar.GetYear(this.value), this.Culture.Calendar.GetMonth(this.value), part.value, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
    }

    protected override void ResetYear(MaskPart part)
    {
      int year = this.isFarsi ? this.Culture.Calendar.MinSupportedDateTime.Year : this.Culture.Calendar.GetYear(this.Culture.Calendar.MinSupportedDateTime);
      try
      {
        this.Value = (object) this.Culture.Calendar.ToDateTime(year, this.Culture.Calendar.GetMonth(this.value), this.Culture.Calendar.GetDayOfMonth(this.value), this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
      }
      catch (ArgumentOutOfRangeException ex)
      {
      }
    }

    public override void ResetToMinDate()
    {
      this.Value = (object) this.Culture.Calendar.ToDateTime(this.isFarsi ? this.Culture.Calendar.MinSupportedDateTime.Year : this.Culture.Calendar.GetYear(this.Culture.Calendar.MinSupportedDateTime), this.isFarsi ? this.Culture.Calendar.MinSupportedDateTime.Month : this.Culture.Calendar.GetMonth(this.Culture.Calendar.MinSupportedDateTime), this.isFarsi ? this.Culture.Calendar.MinSupportedDateTime.Day : this.Culture.Calendar.GetDayOfMonth(this.Culture.Calendar.MinSupportedDateTime), 0, 0, 0, 0);
    }

    internal override void ValidateRange()
    {
      this.keyCounter = 0;
      int year1 = this.Culture.Calendar.GetYear(this.value);
      MaskPart maskPart = (MaskPart) null;
      for (int index = 0; index < this.List.Count; ++index)
      {
        if (this.List[index].type == PartTypes.Year)
        {
          maskPart = this.List[index];
          break;
        }
      }
      if (maskPart != null)
        year1 = maskPart.value;
      int year2 = this.isFarsi ? this.Culture.Calendar.MinSupportedDateTime.Year : this.Culture.Calendar.GetYear(this.Culture.Calendar.MinSupportedDateTime);
      int month1 = this.isFarsi ? this.Culture.Calendar.MinSupportedDateTime.Month : this.Culture.Calendar.GetMonth(this.Culture.Calendar.MinSupportedDateTime);
      int day1 = this.isFarsi ? this.Culture.Calendar.MinSupportedDateTime.Day : this.Culture.Calendar.GetDayOfMonth(this.Culture.Calendar.MinSupportedDateTime);
      if (year1 < year2)
      {
        this.Value = (object) this.Culture.Calendar.ToDateTime(year2, month1, day1, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
      }
      else
      {
        int year3 = this.isFarsi ? this.Culture.Calendar.MaxSupportedDateTime.Year : this.Culture.Calendar.GetYear(this.Culture.Calendar.MaxSupportedDateTime);
        int month2 = this.isFarsi ? this.Culture.Calendar.MaxSupportedDateTime.Month : this.Culture.Calendar.GetMonth(this.Culture.Calendar.MaxSupportedDateTime);
        int day2 = this.isFarsi ? this.Culture.Calendar.MaxSupportedDateTime.Day : this.Culture.Calendar.GetDayOfMonth(this.Culture.Calendar.MaxSupportedDateTime);
        if (year1 > year3)
          this.Value = (object) this.Culture.Calendar.ToDateTime(year3, month2, day2, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
        else
          base.ValidateRange();
      }
    }

    protected override bool HandleKeyPress(MaskPart part, KeyPressEventArgs e)
    {
      if (e.Handled)
        return true;
      if (e.KeyChar == '\r')
      {
        try
        {
          if (this.isFarsi && part.value < this.MinSupportedDateTime.Year)
            part.value = this.MinSupportedDateTime.Year;
          if (this.isFarsi && part.value > this.MaxSupportedDateTime.Year)
            part.value = this.MaxSupportedDateTime.Year;
          this.Culture.Calendar.ToDateTime(part.value, 1, 1, 1, 1, 1, 1);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          int year1 = this.Culture.Calendar.GetYear(this.minTime);
          int year2 = this.Culture.Calendar.GetYear(this.maxTime);
          if (part.value < year1)
            part.value = year1;
          if (part.value > year1)
            part.value = year2;
        }
        return true;
      }
      if (this.TextBoxItem.SelectedText == this.TextBoxItem.Text)
        this.SelectFirstEditableItem();
      int result = 0;
      if (!int.TryParse(e.KeyChar.ToString(), out result))
        return false;
      string empty1 = string.Empty;
      if (part.type == PartTypes.MiliSeconds)
      {
        int num = (int) ((double) part.value / Math.Pow(10.0, (double) (3 - part.len))) * 10 + result;
        part.value = num % (int) Math.Pow(10.0, (double) part.len);
      }
      else
      {
        ++this.keyCounter;
        int selectionStart = this.TextBoxItem.SelectionStart;
        int selectionLength = this.TextBoxItem.SelectionLength;
        if (this.keyCounter == 1)
        {
          part.value = result;
          this.TextBoxItem.Text = this.TextBoxItem.Text.Remove(selectionStart, selectionLength);
          string str = new string('0', part.len - 1);
          this.TextBoxItem.Text = this.TextBoxItem.Text.Insert(selectionStart, string.Format("{0}{1}", (object) str, (object) e.KeyChar.ToString()));
          e.Handled = true;
        }
        else
        {
          part.value = part.value * 10 + result;
          int startIndex = selectionStart;
          this.TextBoxItem.Text = this.TextBoxItem.Text.Remove(startIndex, 1);
          this.TextBoxItem.Text = this.TextBoxItem.Text.Insert(startIndex + selectionLength - 1, e.KeyChar.ToString());
          e.Handled = true;
        }
        this.TextBoxItem.SelectionStart = selectionStart;
        this.TextBoxItem.SelectionLength = selectionLength;
        if (this.keyCounter == part.len)
        {
          string empty2 = part.value.ToString();
          if (this.AutoCompleteYear && empty2.Length == 1)
            empty2 = string.Empty;
          this.keyCounter = 0;
          part.value = int.Parse(empty2);
          try
          {
            int year1 = this.isFarsi ? this.Culture.Calendar.MinSupportedDateTime.Year : this.Culture.Calendar.GetYear(this.Culture.Calendar.MinSupportedDateTime);
            int month1 = this.isFarsi ? this.Culture.Calendar.MinSupportedDateTime.Month : this.Culture.Calendar.GetMonth(this.Culture.Calendar.MinSupportedDateTime);
            int day1 = this.isFarsi ? this.Culture.Calendar.MinSupportedDateTime.Day : this.Culture.Calendar.GetDayOfMonth(this.Culture.Calendar.MinSupportedDateTime);
            int num = part.value;
            if (num < year1)
            {
              this.Value = (object) this.Culture.Calendar.ToDateTime(year1, month1, day1, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
              return true;
            }
            int year2 = this.isFarsi ? this.Culture.Calendar.MaxSupportedDateTime.Year : this.Culture.Calendar.GetYear(this.Culture.Calendar.MaxSupportedDateTime);
            int month2 = this.isFarsi ? this.Culture.Calendar.MaxSupportedDateTime.Month : this.Culture.Calendar.GetMonth(this.Culture.Calendar.MaxSupportedDateTime);
            int day2 = this.isFarsi ? this.Culture.Calendar.MaxSupportedDateTime.Day : this.Culture.Calendar.GetDayOfMonth(this.Culture.Calendar.MaxSupportedDateTime);
            if (num > year2)
            {
              this.Value = (object) this.Culture.Calendar.ToDateTime(year2, month2, day2, this.value.Hour, this.value.Minute, this.value.Second, this.value.Millisecond);
              return true;
            }
            this.Culture.Calendar.ToDateTime(part.value, 1, 1, 1, 1, 1, 1);
          }
          catch (ArgumentOutOfRangeException ex)
          {
            int year1 = this.Culture.Calendar.GetYear(this.Culture.Calendar.MinSupportedDateTime);
            int year2 = this.Culture.Calendar.GetYear(this.Culture.Calendar.MaxSupportedDateTime);
            if (part.value < year1)
              part.value = year1;
            if (part.value > year1)
              part.value = year2;
          }
          return true;
        }
      }
      if (part.type == PartTypes.Year && part.maskPart.Length == 2)
      {
        int length = part.value.ToString().Length;
        part.value = length <= 1 ? int.Parse(string.Format("{0}{1}", (object) this.Culture.Calendar.GetYear(DateTime.Now).ToString().Substring(0, 2), (object) part.value.ToString())) : int.Parse(string.Format("{0}{1}", (object) this.Culture.Calendar.GetYear(DateTime.Now).ToString().Substring(0, 2), (object) part.value.ToString().Substring(length - 2, 2)));
      }
      return false;
    }
  }
}
