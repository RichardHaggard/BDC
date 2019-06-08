// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDateTimePickerSpinEdit
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadDateTimePickerSpinEdit : RadDateTimePickerBehaviorDirector, IDisposable
  {
    private bool maskEditValueChanged;
    private RadMaskedEditBoxElement textBoxElement;
    private RadRepeatArrowElement upButton;
    private RadRepeatArrowElement downButton;
    private StackLayoutElement stackLayout;
    private StackLayoutElement buttonsLayout;
    private BorderPrimitive border;
    private FillPrimitive backGround;
    private RadCheckBoxElement checkBox;
    private RadDateTimePickerElement dateTimePickerElement;

    public RadDateTimePickerSpinEdit(RadDateTimePickerElement dateTimePicker)
    {
      this.dateTimePickerElement = dateTimePicker;
    }

    public override RadMaskedEditBoxElement TextBoxElement
    {
      get
      {
        return this.textBoxElement;
      }
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

    public StackLayoutElement ContentLayout
    {
      get
      {
        return this.stackLayout;
      }
    }

    public StackLayoutElement ButtonsLayout
    {
      get
      {
        return this.buttonsLayout;
      }
    }

    public override void SetDateByValue(DateTime? date, DateTimePickerFormat formatType)
    {
      CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
      this.maskEditValueChanged = false;
      Thread.CurrentThread.CurrentCulture = this.dateTimePickerElement.Culture;
      this.textBoxElement.Culture = this.dateTimePickerElement.Culture;
      DateTime? nullable = date;
      DateTime nullDate = this.dateTimePickerElement.NullDate;
      if ((!nullable.HasValue ? 1 : (nullable.GetValueOrDefault() != nullDate ? 1 : 0)) != 0)
      {
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
        this.textBoxElement.Value = (object) date;
      }
      else
      {
        if (!this.textBoxElement.Value.Equals((object) date))
          this.textBoxElement.Value = (object) date;
        this.textBoxElement.Text = this.textBoxElement.TextBoxItem.NullText;
      }
      Thread.CurrentThread.CurrentCulture = currentCulture;
      this.maskEditValueChanged = true;
    }

    public override void CreateChildren()
    {
      this.backGround = new FillPrimitive();
      this.backGround.Class = "DateTimePickerBackGround";
      int num1 = (int) this.backGround.SetDefaultValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid);
      this.dateTimePickerElement.Children.Add((RadElement) this.backGround);
      this.border = new BorderPrimitive();
      this.border.Class = "DateTimePickerBorder";
      this.dateTimePickerElement.Children.Add((RadElement) this.border);
      this.stackLayout = new StackLayoutElement();
      int num2 = (int) this.stackLayout.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) true);
      int num3 = (int) this.stackLayout.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      this.stackLayout.FitInAvailableSize = true;
      this.stackLayout.Class = "DateTimePickerSpinLayout";
      this.dateTimePickerElement.Children.Add((RadElement) this.stackLayout);
      this.checkBox = new RadCheckBoxElement();
      int num4 = (int) this.checkBox.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) false);
      int num5 = (int) this.checkBox.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      int num6 = (int) this.checkBox.SetDefaultValueOverride(RadElement.AlignmentProperty, (object) ContentAlignment.MiddleCenter);
      if (!this.dateTimePickerElement.ShowCheckBox)
        this.checkBox.Visibility = ElementVisibility.Collapsed;
      this.stackLayout.Children.Add((RadElement) this.checkBox);
      this.dateTimePickerElement.CheckBox = this.checkBox;
      this.textBoxElement = new RadMaskedEditBoxElement();
      this.textBoxElement.MaskProviderCreated += new EventHandler(this.textBoxElement_MaskProviderCreated);
      this.textBoxElement.Mask = "";
      this.textBoxElement.MaskType = MaskType.DateTime;
      this.textBoxElement.ShowBorder = false;
      this.textBoxElement.Class = "textbox";
      this.textBoxElement.ThemeRole = "DateTimePickerMaskTextBoxElement";
      int num7 = (int) this.textBoxElement.SetDefaultValueOverride(RadElement.AlignmentProperty, (object) ContentAlignment.MiddleLeft);
      int num8 = (int) this.textBoxElement.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) true);
      int num9 = (int) this.textBoxElement.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      int num10 = (int) this.textBoxElement.TextBoxItem.SetDefaultValueOverride(RadElement.AlignmentProperty, (object) ContentAlignment.MiddleLeft);
      int num11 = (int) this.textBoxElement.TextBoxItem.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) false);
      int num12 = (int) this.textBoxElement.Fill.SetDefaultValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid);
      this.textBoxElement.TextBoxItem.HostedControl.RightToLeft = this.dateTimePickerElement.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      this.stackLayout.Children.Add((RadElement) this.textBoxElement);
      this.textBoxElement.Children[this.textBoxElement.Children.Count - 1].Visibility = ElementVisibility.Collapsed;
      this.textBoxElement.KeyDown += new KeyEventHandler(this.textBoxElement_KeyDown);
      this.textBoxElement.KeyPress += new KeyPressEventHandler(this.textBoxElement_KeyPress);
      this.textBoxElement.KeyUp += new KeyEventHandler(this.textBoxElement_KeyUp);
      this.textBoxElement.ValueChanged += new EventHandler(this.maskBox_ValueChanged);
      this.textBoxElement.ValueChanging += new CancelEventHandler(this.maskBox_ValueChanging);
      this.textBoxElement.TextBoxItem.LostFocus += new EventHandler(this.maskBox_LostFocus);
      this.textBoxElement.TextBoxItem.TextChanged += new EventHandler(this.maskBox_TextChanged);
      this.textBoxElement.MouseDown += new MouseEventHandler(this.maskBox_MouseDown);
      this.buttonsLayout = new StackLayoutElement();
      int num13 = (int) this.buttonsLayout.SetDefaultValueOverride(StackLayoutElement.OrientationProperty, (object) Orientation.Vertical);
      int num14 = (int) this.buttonsLayout.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) false);
      int num15 = (int) this.buttonsLayout.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      this.buttonsLayout.FitInAvailableSize = true;
      this.buttonsLayout.Class = "DateTimePickerSpinButtonsLayout";
      this.stackLayout.Children.Add((RadElement) this.buttonsLayout);
      this.upButton = new RadRepeatArrowElement();
      this.upButton.ThemeRole = "UpButton";
      int num16 = (int) this.upButton.SetDefaultValueOverride(RadElement.PaddingProperty, (object) new Padding(3, 1, 3, 1));
      this.upButton.Click += new EventHandler(this.upButton_Click);
      this.upButton.Direction = Telerik.WinControls.ArrowDirection.Up;
      this.upButton.Arrow.AutoSize = true;
      this.upButton.CanFocus = false;
      this.buttonsLayout.Children.Add((RadElement) this.upButton);
      this.downButton = new RadRepeatArrowElement();
      this.downButton.ThemeRole = "DownButton";
      int num17 = (int) this.downButton.SetDefaultValueOverride(RadElement.PaddingProperty, (object) new Padding(3, 1, 3, 0));
      int num18 = (int) this.downButton.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      this.downButton.Click += new EventHandler(this.downButton_Click);
      this.downButton.Arrow.AutoSize = true;
      this.downButton.Direction = Telerik.WinControls.ArrowDirection.Down;
      this.downButton.CanFocus = false;
      this.buttonsLayout.Children.Add((RadElement) this.downButton);
      this.SetDateByValue(this.dateTimePickerElement.Value, this.dateTimePickerElement.Format);
    }

    private void maskBox_MouseDown(object sender, MouseEventArgs e)
    {
      if (!this.DateTimePickerElement.Value.Equals((object) this.DateTimePickerElement.NullDate))
        return;
      this.textBoxElement.TextBoxItem.Text = "";
    }

    private void maskBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.DateTimePickerElement.Value.Equals((object) this.DateTimePickerElement.NullDate) || string.IsNullOrEmpty(this.textBoxElement.Text))
        return;
      this.textBoxElement.Text = "";
    }

    private void textBoxElement_MaskProviderCreated(object sender, EventArgs e)
    {
      this.DateTimePickerElement.CallRaiseMaskProviderCreated();
    }

    private void textBoxElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.DateTimePickerElement.CallRaiseKeyUp(e);
    }

    private void textBoxElement_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.DateTimePickerElement.CallRaiseKeyPress(e);
    }

    private void textBoxElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.DateTimePickerElement.CallRaiseKeyDown(e);
    }

    private void maskBox_LostFocus(object sender, EventArgs e)
    {
      if (this.textBoxElement.Value == null)
      {
        this.textBoxElement.TextBoxItem.Text = "";
      }
      else
      {
        DateTime dateTime = (DateTime) this.textBoxElement.Value;
        if (dateTime.Equals(this.DateTimePickerElement.NullDate))
        {
          this.textBoxElement.TextBoxItem.Text = "";
        }
        else
        {
          if (dateTime != this.DateTimePickerElement.NullDate && (dateTime < this.DateTimePickerElement.MinDate || dateTime > this.DateTimePickerElement.MaxDate))
          {
            if (dateTime < this.DateTimePickerElement.MinDate)
              dateTime = this.DateTimePickerElement.MinDate;
            else if (dateTime > this.DateTimePickerElement.MaxDate)
              dateTime = this.DateTimePickerElement.MaxDate;
          }
          this.textBoxElement.Value = (object) dateTime;
        }
      }
    }

    private void downButton_Click(object sender, EventArgs e)
    {
      if (this.textBoxElement.TextBoxItem.ReadOnly)
        return;
      this.textBoxElement.Down();
    }

    private void upButton_Click(object sender, EventArgs e)
    {
      if (this.textBoxElement.TextBoxItem.ReadOnly)
        return;
      this.textBoxElement.Up();
    }

    private void maskBox_ValueChanged(object sender, EventArgs e)
    {
      if (!this.maskEditValueChanged)
        return;
      if (this.textBoxElement.Value != null && (DateTime) this.textBoxElement.Value >= this.DateTimePickerElement.MinDate && (DateTime) this.textBoxElement.Value <= this.dateTimePickerElement.MaxDate)
      {
        this.DateTimePickerElement.Value = new DateTime?((DateTime) this.textBoxElement.Value);
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

    public void Dispose()
    {
      this.textBoxElement.KeyDown -= new KeyEventHandler(this.textBoxElement_KeyDown);
      this.textBoxElement.KeyPress -= new KeyPressEventHandler(this.textBoxElement_KeyPress);
      this.textBoxElement.KeyUp -= new KeyEventHandler(this.textBoxElement_KeyUp);
      this.textBoxElement.MaskProviderCreated -= new EventHandler(this.textBoxElement_MaskProviderCreated);
      this.textBoxElement.ValueChanged -= new EventHandler(this.maskBox_ValueChanged);
      this.textBoxElement.ValueChanging -= new CancelEventHandler(this.maskBox_ValueChanging);
      this.textBoxElement.TextBoxItem.LostFocus -= new EventHandler(this.maskBox_LostFocus);
      this.textBoxElement.TextBoxItem.TextChanged -= new EventHandler(this.maskBox_TextChanged);
      this.textBoxElement.MouseDown -= new MouseEventHandler(this.maskBox_MouseDown);
      this.upButton.Click -= new EventHandler(this.upButton_Click);
      this.downButton.Click -= new EventHandler(this.downButton_Click);
    }
  }
}
