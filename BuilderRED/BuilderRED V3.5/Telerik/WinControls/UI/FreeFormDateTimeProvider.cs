// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FreeFormDateTimeProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class FreeFormDateTimeProvider : MaskDateTimeProvider
  {
    private DateInput parser;

    public FreeFormDateTimeProvider(
      string mask,
      CultureInfo culture,
      RadMaskedEditBoxElement owner)
      : base(mask, culture, owner)
    {
      this.Parser = new DateInput(this.Culture);
      this.Parser.DateFormat = this.Mask;
    }

    public DateInput Parser
    {
      get
      {
        if (this.parser == null)
          this.parser = new DateInput(this.Culture);
        return this.parser;
      }
      set
      {
        this.parser = value;
      }
    }

    public event EventHandler ParsedDateTime;

    public event ParsingDateTimeEventHandler ParsingDateTime;

    public override void KeyDown(object sender, KeyEventArgs e)
    {
      if (this.List == null && e.KeyValue == 13)
      {
        this.TryParse();
        e.Handled = true;
      }
      if (this.List == null && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up)
        return;
      KeyEventArgsWithMinMax eventArgsWithMinMax = e as KeyEventArgsWithMinMax;
      DateTime minDate = DateTime.MinValue;
      DateTime maxDate = DateTime.MaxValue;
      if (e.Handled)
        return;
      if (eventArgsWithMinMax != null)
      {
        minDate = eventArgsWithMinMax.MinDate;
        maxDate = eventArgsWithMinMax.MaxDate;
      }
      if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Space)
      {
        this.SelectNextEditableItem();
        e.Handled = true;
      }
      else if (e.KeyCode == Keys.Left)
      {
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
        if (this.TextBoxItem.SelectionLength != this.TextBoxItem.Text.Length)
          this.ResetCurrentPartValue(sender, e);
        else
          this.Value = (object) minDate;
        e.Handled = true;
      }
      else if (e.KeyCode == Keys.Home && (e.KeyData & Keys.Shift) == Keys.None)
      {
        this.SelectFirstEditableItem();
        e.Handled = true;
      }
      else
      {
        if (e.KeyCode != Keys.End || (e.KeyData & Keys.Shift) != Keys.None)
          return;
        this.SelectLastEditableItem();
        e.Handled = true;
      }
    }

    public override void KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.Handled)
        return;
      this.List = (List<MaskPart>) null;
    }

    public virtual void OnParsingDateTime(ParsingDateTimeEventArgs e)
    {
      ParsingDateTimeEventHandler parsingDateTime = this.ParsingDateTime;
      if (parsingDateTime == null)
        return;
      parsingDateTime((object) this, e);
    }

    public virtual void OnParsedDateTime()
    {
      EventHandler parsedDateTime = this.ParsedDateTime;
      if (parsedDateTime == null)
        return;
      parsedDateTime((object) this, new EventArgs());
    }

    protected override bool ResetOnDelPartValue(MaskPart part, int keyChar)
    {
      if (keyChar == 8)
      {
        this.List = (List<MaskPart>) null;
        return false;
      }
      if (keyChar == 8)
      {
        part.value = part.type != PartTypes.Year ? part.min : this.YearResetValue;
        this.SelectPrevItem();
        return true;
      }
      if (keyChar != 46)
        return false;
      part.value = part.type != PartTypes.Year ? part.min : this.YearResetValue;
      return true;
    }

    public override void HandleSpinUp(DateTime minDate, DateTime maxDate)
    {
      this.TryParse();
      if (this.List == null)
        return;
      this.SelectCurrentItemFromCurrentCaret();
      if (this.SelectedItemIndex == -1)
        this.SelectedItemIndex = 0;
      this.Up(this.List[this.SelectedItemIndex], minDate, maxDate);
    }

    public override void HandleSpinDown(DateTime minDate, DateTime maxDate)
    {
      this.TryParse();
      if (this.List == null)
        return;
      this.SelectCurrentItemFromCurrentCaret();
      if (this.SelectedItemIndex == -1)
        this.SelectedItemIndex = 0;
      this.Down(this.List[this.SelectedItemIndex], minDate, maxDate);
    }

    public override void SelectCurrentItemWithSelectedItem()
    {
      if (this.SelectedItemIndex == -1 || this.List == null)
        return;
      for (int selectedItemIndex = this.SelectedItemIndex; selectedItemIndex < this.List.Count; ++selectedItemIndex)
      {
        MaskPart maskPart = this.List[selectedItemIndex];
        if (maskPart.type != PartTypes.Character && !maskPart.readOnly)
        {
          this.TextBoxItem.SelectionStart = maskPart.offset;
          this.TextBoxItem.SelectionLength = maskPart.len;
          this.SelectedItemIndex = selectedItemIndex;
          break;
        }
      }
    }

    public override bool SelectCurrentItemFromCurrentCaret()
    {
      if (this.List == null)
        return false;
      int selectionStart = this.TextBoxItem.SelectionStart;
      int num = 0;
      bool flag = false;
      for (int index = 0; index < this.List.Count; ++index)
      {
        MaskPart maskPart = this.List[index];
        if (selectionStart >= maskPart.offset && selectionStart <= maskPart.offset + maskPart.len && (!maskPart.readOnly && maskPart.type != PartTypes.Character))
        {
          this.TextBoxItem.SelectionStart = this.List[index].offset;
          this.TextBoxItem.SelectionLength = this.List[index].len;
          this.SelectedItemIndex = index;
          flag = true;
          break;
        }
        num += maskPart.len;
      }
      return flag;
    }

    public override bool SelectNextEditableItemFromCurrentCaret()
    {
      if (this.List == null)
        return false;
      int selectionStart = this.TextBoxItem.SelectionStart;
      int num = 0;
      bool flag = false;
      for (int index = 0; index < this.List.Count; ++index)
      {
        MaskPart maskPart = this.List[index];
        if (selectionStart >= maskPart.offset && selectionStart <= maskPart.offset + maskPart.len)
        {
          this.SelectedItemIndex = index;
          flag = true;
          this.SelectNextEditableItem();
          break;
        }
        num += maskPart.len;
      }
      return flag;
    }

    public override bool Validate(string stringValue)
    {
      CancelEventArgs e = new CancelEventArgs();
      this.Owner.CallValueChanging(e);
      if (e.Cancel)
        return false;
      this.MaskFromFormat = MaskDateTimeProvider.GetSpecificFormat(this.Mask, this.Culture.DateTimeFormat);
      DateTime? date = this.Parser.ParseDate(stringValue, new DateTime?());
      if (date.HasValue)
      {
        this.TextBoxItem.Text = date.Value.ToString(this.MaskFromFormat, (IFormatProvider) this.Culture);
        this.Value = (object) date.Value;
        this.List = MaskDateTimeProvider.FillCollection(this.MaskFromFormat, this.Culture.DateTimeFormat);
        this.FillCollectionWithValues(this.List, this.value, this.Mask);
        this.SelectCurrentItemWithSelectedItem();
      }
      else
        this.Owner.Value = (object) null;
      return false;
    }

    protected override void FillCollectionWithValues(
      List<MaskPart> collection,
      DateTime dateTime,
      string mask)
    {
      if (collection == null)
        return;
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
            maskPart.value = this.Culture.Calendar.GetDayOfMonth(dateTime);
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
              maskPart.len = maskPart.value.ToString().Length;
            if (maskPart.maskPart.Length == 3)
            {
              maskPart.len = this.DateTimeFormatInfo.AbbreviatedDayNames[(int) dateTime.DayOfWeek].Length;
              break;
            }
            if (maskPart.maskPart.Length > 3)
            {
              maskPart.len = this.DateTimeFormatInfo.DayNames[(int) dateTime.DayOfWeek].Length;
              break;
            }
            break;
          case PartTypes.MiliSeconds:
            maskPart.value = (int) this.Culture.Calendar.GetMilliseconds(dateTime);
            maskPart.len = maskPart.value.ToString().Length;
            maskPart.max = 59;
            break;
          case PartTypes.h12:
            int num = this.Culture.Calendar.GetHour(dateTime) % 12;
            if (num == 0)
              num = 12;
            maskPart.value = num;
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
              maskPart.len = maskPart.value.ToString().Length;
            maskPart.max = 12;
            break;
          case PartTypes.h24:
            maskPart.value = this.Culture.Calendar.GetHour(dateTime);
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
              maskPart.len = maskPart.value.ToString().Length;
            maskPart.max = 23;
            break;
          case PartTypes.Minutes:
            maskPart.value = this.Culture.Calendar.GetMinute(dateTime);
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
              maskPart.len = maskPart.value.ToString().Length;
            maskPart.max = 59;
            break;
          case PartTypes.Month:
            maskPart.value = this.Culture.Calendar.GetMonth(dateTime);
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
              maskPart.len = maskPart.value.ToString().Length;
            if (maskPart.maskPart.Length == 3)
              maskPart.len = this.DateTimeFormatInfo.AbbreviatedMonthNames[maskPart.value - 1].Length;
            else if (maskPart.maskPart.Length > 3)
              maskPart.len = this.DateTimeFormatInfo.MonthNames[maskPart.value - 1].Length;
            maskPart.min = 1;
            maskPart.max = 12;
            break;
          case PartTypes.Seconds:
            maskPart.value = this.Culture.Calendar.GetSecond(dateTime);
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
              maskPart.len = maskPart.value.ToString().Length;
            maskPart.max = 59;
            break;
          case PartTypes.AmPm:
            maskPart.value = this.Culture.Calendar.GetHour(dateTime) / 12;
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.len < maskPart.value.ToString().Length)
            {
              maskPart.len = maskPart.value.ToString().Length;
              break;
            }
            break;
          case PartTypes.Year:
            maskPart.value = this.Culture.Calendar.GetYear(dateTime);
            maskPart.len = maskPart.maskPart.Length;
            if (maskPart.maskPart == "y")
              maskPart.len = 2;
            maskPart.min = DateTime.MinValue.Year;
            maskPart.max = DateTime.MaxValue.Year;
            break;
        }
      }
      this.AdjustItemsPossitionOffset(collection);
      this.SetDayMaxValue(collection);
    }

    public void TryParse()
    {
      ParsingDateTimeEventArgs e = new ParsingDateTimeEventArgs(this.TextBoxItem.Text, this.Parser.ParseDate(this.TextBoxItem.Text, new DateTime?()));
      this.OnParsingDateTime(e);
      if (e.Cancel)
        return;
      DateTime? nullable1 = e.Result;
      DateTime? nullable2 = nullable1;
      DateTime minDate = this.MinDate;
      if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() < minDate ? 1 : 0) : 0) != 0)
      {
        nullable1 = new DateTime?(this.MinDate);
      }
      else
      {
        DateTime? nullable3 = nullable1;
        DateTime maxDate = this.MaxDate;
        if ((nullable3.HasValue ? (nullable3.GetValueOrDefault() > maxDate ? 1 : 0) : 0) != 0)
          nullable1 = new DateTime?(this.MaxDate);
      }
      if (nullable1.HasValue)
      {
        this.TextBoxItem.Text = nullable1.Value.ToString(this.MaskFromFormat, (IFormatProvider) this.Culture);
        this.value = nullable1.Value;
        this.Owner.Value = (object) nullable1.Value;
        this.List = MaskDateTimeProvider.FillCollection(this.MaskFromFormat, this.Culture.DateTimeFormat);
        this.FillCollectionWithValues(this.List, this.value, this.Mask);
        this.SelectCurrentItemWithSelectedItem();
      }
      else
        this.Owner.Value = (object) null;
      this.OnParsedDateTime();
    }
  }
}
