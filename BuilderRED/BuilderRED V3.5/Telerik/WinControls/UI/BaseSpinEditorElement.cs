// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseSpinEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Globalization;

namespace Telerik.WinControls.UI
{
  public class BaseSpinEditorElement : RadSpinElement
  {
    private bool setSpinValue;
    private bool enableValueChangingOnTextChanging;
    private bool valueChangingCancel;
    private int oldSelectionStart;

    public BaseSpinEditorElement()
    {
      this.DefaultSize = new Size(50, 20);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = true;
      this.NotifyParentOnMouseInput = false;
      this.Alignment = ContentAlignment.MiddleCenter;
    }

    internal bool EnableValueChangingOnTextChanging
    {
      get
      {
        return this.enableValueChangingOnTextChanging;
      }
      set
      {
        if (value == this.enableValueChangingOnTextChanging)
          return;
        this.UnbindTextBoxItem();
        if (value)
          this.BindTextBoxItem();
        this.enableValueChangingOnTextChanging = value;
      }
    }

    private void BindTextBoxItem()
    {
      this.TextBoxItem.TextChanging += new TextChangingEventHandler(this.TextBoxItem_TextChanging);
      this.TextBoxItem.HostedControl.TextChanged += new EventHandler(this.HostedControl_TextChanged);
    }

    private void UnbindTextBoxItem()
    {
      this.TextBoxItem.TextChanging -= new TextChangingEventHandler(this.TextBoxItem_TextChanging);
      this.TextBoxItem.HostedControl.TextChanged -= new EventHandler(this.HostedControl_TextChanged);
    }

    private void TextBoxItem_TextChanging(object sender, TextChangingEventArgs e)
    {
      this.oldSelectionStart = this.TextBoxItem.SelectionStart - 1;
      this.BitState[17592186044416L] = true;
      this.EndTextEdit();
      e.Cancel = this.valueChangingCancel;
    }

    private void HostedControl_TextChanged(object sender, EventArgs e)
    {
      if (this.valueChangingCancel && this.oldSelectionStart >= 0)
        this.TextBoxItem.SelectionStart = this.oldSelectionStart;
      this.valueChangingCancel = false;
      if (!string.IsNullOrEmpty(this.TextBoxItem.HostedControl.Text))
        return;
      this.internalValue = new Decimal(-1, -1, -1, true, (byte) 0);
      this.OnValueChanged(e);
    }

    protected override void OnValueChanging(ValueChangingEventArgs e)
    {
      base.OnValueChanging(e);
      this.valueChangingCancel = e.Cancel;
    }

    public override void PerformStep(Decimal step)
    {
      if (this.internalValue == new Decimal(-1, -1, -1, true, (byte) 0))
        this.internalValue = new Decimal(0);
      Decimal num = this.GetValueFromText();
      if (num == new Decimal(-1, -1, -1, true, (byte) 0))
        num = new Decimal(0);
      try
      {
        num += step;
      }
      catch (OverflowException ex)
      {
      }
      this.Value = this.Constrain(num);
      this.Validate();
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadSpinElement);
      }
    }

    protected override void SetSpinValue(Decimal value, bool fromValue)
    {
      if (this.setSpinValue)
        return;
      this.setSpinValue = true;
      Decimal num = this.Constrain(value);
      bool flag = this.Value != num;
      if (this.Value != num)
      {
        ValueChangingEventArgs e = new ValueChangingEventArgs((object) num, (object) this.Value);
        this.OnValueChanging(e);
        if (e.Cancel)
        {
          this.setSpinValue = false;
          return;
        }
        this.internalValue = num;
      }
      if (fromValue)
      {
        this.BitState[8796093022208L] = true;
        this.BitState[17592186044416L] = false;
        if (this.TextBoxItem.HostedControl != null)
          this.Text = this.GetNumberText(this.Value);
        this.BitState[8796093022208L] = false;
      }
      if (flag)
      {
        this.OnNotifyPropertyChanged("Value");
        this.OnValueChanged(EventArgs.Empty);
      }
      this.setSpinValue = false;
    }

    protected override Decimal GetValueFromText()
    {
      if (string.IsNullOrEmpty(this.Text))
      {
        if (this.EnableNullValueInput)
          return this.MinValue;
        return new Decimal(-1, -1, -1, true, (byte) 0);
      }
      try
      {
        if (string.IsNullOrEmpty(this.Text) || this.Text.Length == 1 && !(this.Text != "-"))
          return this.DefaultValue;
        Decimal num = new Decimal(0);
        return !this.Hexadecimal ? this.Constrain(Decimal.Parse(this.Text, (IFormatProvider) CultureInfo.CurrentCulture)) : this.Constrain(Convert.ToDecimal(Convert.ToInt64(this.Text, 16)));
      }
      catch
      {
        return this.DefaultValue;
      }
    }
  }
}
