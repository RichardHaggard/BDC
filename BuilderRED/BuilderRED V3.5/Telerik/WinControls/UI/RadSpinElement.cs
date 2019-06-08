// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSpinElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadSpinElement : RadEditorElement
  {
    private Decimal maxValue = new Decimal(100);
    private Decimal step = new Decimal(1);
    private string errorDescription = string.Empty;
    private DateTime previosWheel = DateTime.Now;
    internal const long SuppressEditorStateKey = 8796093022208;
    internal const long TextValueChangedStateKey = 17592186044416;
    internal const long InterceptArrowKeysStateKey = 35184372088832;
    internal const long ThousandsSeparatorStateKey = 70368744177664;
    internal const long HexadecimalStateKey = 140737488355328;
    internal const long ShowUpDownButtonsStateKey = 281474976710656;
    private const string InvalidValueMessage = "Value of '{0}' is not valid for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'.";
    private RadTextBoxItem textItem;
    private RadRepeatArrowElement buttonUp;
    private RadRepeatArrowElement buttonDown;
    private int validateCount;
    private BorderPrimitive border;
    private FillPrimitive textBoxFillPrimitive;
    private StackLayoutElement layout;
    private StackLayoutElement buttonsLayout;
    protected Decimal internalValue;
    private Decimal defaultValue;
    private Decimal minValue;
    private int decimalPlaces;
    private bool wrap;
    private bool rightMouseButtonReset;
    private bool enableMouseWheel;
    private bool isNullableValue;
    private bool enableNullValueInput;
    private bool isNull;
    public EventHandler NullableValueChanged;

    protected override void CreateChildElements()
    {
      this.buttonUp = this.CreateUpButton();
      this.buttonDown = this.CreateDownButton();
      this.textItem = new RadTextBoxItem();
      this.textItem.Alignment = ContentAlignment.MiddleLeft;
      this.textItem.StretchHorizontally = true;
      this.textItem.StretchVertically = false;
      int num1 = (int) this.textItem.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.textItem.BindProperty(RadElement.ContainsMouseProperty, (RadObject) this, RadElement.ContainsMouseProperty, PropertyBindingOptions.TwoWay);
      this.textItem.Multiline = false;
      this.textItem.RouteMessages = false;
      this.layout = new StackLayoutElement();
      this.layout.Class = "SpinElementLayout";
      this.layout.StretchHorizontally = true;
      this.layout.StretchVertically = true;
      this.layout.FitInAvailableSize = true;
      this.textBoxFillPrimitive = new FillPrimitive();
      this.textBoxFillPrimitive.Class = "SpinElementFill";
      this.textBoxFillPrimitive.NumberOfColors = 1;
      this.border = new BorderPrimitive();
      this.border.Class = "SpinElementBorder";
      this.buttonsLayout = new StackLayoutElement();
      this.buttonsLayout.Orientation = Orientation.Vertical;
      this.buttonsLayout.StretchHorizontally = false;
      this.buttonsLayout.StretchVertically = true;
      this.buttonsLayout.FitInAvailableSize = true;
      this.buttonsLayout.Class = "ButtonsLayout";
      this.buttonsLayout.Children.Add((RadElement) this.buttonUp);
      this.buttonsLayout.Children.Add((RadElement) this.buttonDown);
      this.layout.Children.Add((RadElement) this.textItem);
      this.layout.Children.Add((RadElement) this.buttonsLayout);
      this.Children.Add((RadElement) this.textBoxFillPrimitive);
      this.Children.Add((RadElement) this.border);
      this.Children.Add((RadElement) this.layout);
      this.SetSpinValue(this.internalValue, true);
      this.WireEvents();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.enableMouseWheel = true;
      this.BitState[35184372088832L] = true;
      this.BitState[281474976710656L] = true;
    }

    protected virtual RadRepeatArrowElement CreateUpButton()
    {
      RadSpinElementUpButton spinElementUpButton = new RadSpinElementUpButton();
      spinElementUpButton.Class = "UpButton";
      spinElementUpButton.Arrow.Direction = Telerik.WinControls.ArrowDirection.Up;
      return (RadRepeatArrowElement) spinElementUpButton;
    }

    protected virtual RadRepeatArrowElement CreateDownButton()
    {
      RadSpinElementDownButton elementDownButton = new RadSpinElementDownButton();
      elementDownButton.Class = "DownButton";
      elementDownButton.Arrow.Direction = Telerik.WinControls.ArrowDirection.Down;
      return (RadRepeatArrowElement) elementDownButton;
    }

    protected virtual void WireEvents()
    {
      this.textItem.TextChanging += new TextChangingEventHandler(this.textItem_TextChanging);
      this.textItem.TextChanged += new EventHandler(this.textItem_TextChanged);
      this.textItem.GotFocus += new EventHandler(this.textItem_GotFocus);
      this.textItem.LostFocus += new EventHandler(this.textItem_LostFocus);
      this.textItem.Validated += new EventHandler(this.textItem_Validated);
      this.textItem.KeyPress += new KeyPressEventHandler(this.textItem_KeyPress);
      this.textItem.KeyDown += new KeyEventHandler(this.textItem_KeyDown);
      this.textItem.KeyUp += new KeyEventHandler(this.textItem_KeyUp);
      this.textItem.HostedControl.MouseWheel += new MouseEventHandler(this.textItem_MouseWheel);
      this.textItem.HostedControl.MouseUp += new MouseEventHandler(this.HostedControl_MouseUp);
      this.textItem.TextBoxControl.MouseHover += new EventHandler(this.textItem_MouseHover);
      this.textItem.TextBoxControl.MouseLeave += new EventHandler(this.TextBoxControl_MouseLeave);
      this.buttonUp.Click += new EventHandler(this.ButtonUp_Click);
      this.buttonUp.DoubleClick += new EventHandler(this.ButtonUp_Click);
      this.buttonDown.Click += new EventHandler(this.ButtonDown_Click);
      this.buttonDown.DoubleClick += new EventHandler(this.ButtonDown_Click);
      this.textBoxFillPrimitive.MouseDown += (MouseEventHandler) ((param0, param1) => this.textItem.Focus());
    }

    protected virtual void UnwireEvents()
    {
      this.textItem.TextChanging -= new TextChangingEventHandler(this.textItem_TextChanging);
      this.textItem.TextChanged -= new EventHandler(this.textItem_TextChanged);
      this.textItem.GotFocus -= new EventHandler(this.textItem_GotFocus);
      this.textItem.LostFocus -= new EventHandler(this.textItem_LostFocus);
      this.textItem.Validated -= new EventHandler(this.textItem_Validated);
      this.textItem.KeyPress -= new KeyPressEventHandler(this.textItem_KeyPress);
      this.textItem.KeyDown -= new KeyEventHandler(this.textItem_KeyDown);
      this.textItem.KeyUp -= new KeyEventHandler(this.textItem_KeyUp);
      this.textItem.HostedControl.MouseWheel -= new MouseEventHandler(this.textItem_MouseWheel);
      this.textItem.HostedControl.MouseUp -= new MouseEventHandler(this.HostedControl_MouseUp);
      this.textItem.TextBoxControl.MouseHover -= new EventHandler(this.textItem_MouseHover);
      this.textItem.TextBoxControl.MouseLeave -= new EventHandler(this.TextBoxControl_MouseLeave);
      this.buttonUp.Click -= new EventHandler(this.ButtonUp_Click);
      this.buttonUp.DoubleClick -= new EventHandler(this.ButtonUp_Click);
      this.buttonDown.Click -= new EventHandler(this.ButtonDown_Click);
      this.buttonDown.DoubleClick -= new EventHandler(this.ButtonDown_Click);
    }

    protected override void DisposeManagedResources()
    {
      int num1 = (int) this.textItem.UnbindProperty(TextPrimitive.TextProperty);
      int num2 = (int) this.textItem.UnbindProperty(RadElement.ContainsMouseProperty);
      base.DisposeManagedResources();
      this.UnwireEvents();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual Decimal? NullableValue
    {
      get
      {
        if (this.isNullableValue)
          return new Decimal?();
        return new Decimal?(this.internalValue);
      }
      set
      {
        if (value.HasValue)
        {
          if (value.Value < this.minValue || value.Value > this.maxValue)
            throw new ArgumentOutOfRangeException("Value", string.Format("Value of '{0}' is not valid for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'.", (object) value));
          this.internalValue = this.Constrain(value.Value);
          this.textItem.Text = this.GetNumberText(this.internalValue);
          this.isNullableValue = false;
        }
        else
        {
          this.internalValue = this.MinValue;
          this.textItem.Text = string.Empty;
          this.isNullableValue = true;
        }
        this.Validate();
        if (this.isNull)
          return;
        this.OnNullableValueChanged();
      }
    }

    [System.ComponentModel.DefaultValue(false)]
    [Browsable(false)]
    public virtual bool EnableNullValueInput
    {
      get
      {
        return this.enableNullValueInput;
      }
      set
      {
        this.enableNullValueInput = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadRepeatArrowElement ButtonDown
    {
      get
      {
        return this.buttonDown;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadRepeatArrowElement ButtonUp
    {
      get
      {
        return this.buttonUp;
      }
    }

    [Description("Gets or sets the number of decimal places to display in the RadSpinEdit")]
    [System.ComponentModel.DefaultValue(0)]
    [Localizable(true)]
    [Category("Data")]
    public int DecimalPlaces
    {
      get
      {
        return this.decimalPlaces;
      }
      set
      {
        this.decimalPlaces = value;
        this.SetSpinValue();
      }
    }

    [System.ComponentModel.DefaultValue(0)]
    protected Decimal DefaultValue
    {
      get
      {
        return this.defaultValue;
      }
      set
      {
        if (value >= this.MinValue && value <= this.MaxValue)
          this.defaultValue = value;
        else if (value < this.minValue)
        {
          this.internalValue = this.minValue;
          this.textItem.Text = this.minValue.ToString();
        }
        else
        {
          if (!(value > this.maxValue))
            return;
          this.internalValue = this.maxValue;
          this.textItem.Text = this.maxValue.ToString();
        }
      }
    }

    [Description("Gets or sets a value indicating whether the RadSpinEdit should display the value it contains in hexadecimal format.")]
    [Category("Appearance")]
    [System.ComponentModel.DefaultValue(false)]
    public bool Hexadecimal
    {
      get
      {
        return this.GetBitState(140737488355328L);
      }
      set
      {
        this.SetBitState(140737488355328L, value);
        this.SetSpinValue(this.internalValue, true);
      }
    }

    [Category("Behavior")]
    [System.ComponentModel.DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the user can use the UP ARROW and DOWN ARROW keys to select values.")]
    public bool InterceptArrowKeys
    {
      get
      {
        return this.GetBitState(35184372088832L);
      }
      set
      {
        this.SetBitState(35184372088832L, value);
      }
    }

    [System.ComponentModel.DefaultValue(false)]
    public bool ReadOnly
    {
      get
      {
        return this.textItem.ReadOnly;
      }
      set
      {
        this.textItem.ReadOnly = value;
      }
    }

    [Localizable(true)]
    [Description("Gets or sets a value indicating whether a thousands separator is displayed in the RadSpinEdit")]
    [System.ComponentModel.DefaultValue(false)]
    [Category("Data")]
    public bool ThousandsSeparator
    {
      get
      {
        return this.GetBitState(70368744177664L);
      }
      set
      {
        this.SetBitState(70368744177664L, value);
        this.SetSpinValue();
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public System.Windows.Forms.TextBox TextBoxControl
    {
      get
      {
        return (System.Windows.Forms.TextBox) this.textItem.TextBoxControl;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
      get
      {
        return this.textItem.Text;
      }
      set
      {
        this.textItem.Text = value;
        this.SetSpinValue();
      }
    }

    public virtual RadTextBoxItem TextBoxItem
    {
      get
      {
        return this.textItem;
      }
    }

    [System.ComponentModel.DefaultValue(HorizontalAlignment.Left)]
    [Description("Gets or sets the text alignment of RadSpinEditor")]
    [Category("Appearance")]
    public virtual HorizontalAlignment TextAlignment
    {
      get
      {
        return this.TextBoxItem.TextAlign;
      }
      set
      {
        this.TextBoxItem.TextAlign = value;
      }
    }

    [System.ComponentModel.DefaultValue(false)]
    public override bool StretchVertically
    {
      get
      {
        return base.StretchVertically;
      }
      set
      {
        base.StretchVertically = value;
      }
    }

    [System.ComponentModel.DefaultValue(typeof (Decimal), "0")]
    public Decimal Value
    {
      get
      {
        this.Validate();
        return this.internalValue;
      }
      set
      {
        if (value < this.minValue || value > this.maxValue)
          throw new ArgumentOutOfRangeException(nameof (Value), string.Format("Value of '{0}' is not valid for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'.", (object) value));
        this.SetSpinValue(this.Constrain(value), true);
      }
    }

    [Category("Appearance")]
    [Description("Step")]
    [System.ComponentModel.DefaultValue(typeof (Decimal), "0")]
    public Decimal Step
    {
      get
      {
        return this.step;
      }
      set
      {
        if (!(this.step != value))
          return;
        this.step = value;
        this.OnNotifyPropertyChanged(nameof (Step));
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the minimum value that could be set in the spin editor.")]
    public Decimal MinValue
    {
      get
      {
        return this.minValue;
      }
      set
      {
        this.minValue = value;
        this.OnNotifyPropertyChanged(nameof (MinValue));
        if (this.minValue > this.maxValue)
          this.maxValue = value;
        this.SetSpinValue();
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets whether RadSpinEditor will be used as a numeric textbox.")]
    [System.ComponentModel.DefaultValue(true)]
    public bool ShowUpDownButtons
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

    [Description("Gets or sets whether by right-mouse clicking the up/down button you reset the value to the Maximum/Minimum value respectively.")]
    [System.ComponentModel.DefaultValue(false)]
    [Category("Behavior")]
    public bool RightMouseButtonReset
    {
      get
      {
        return this.rightMouseButtonReset;
      }
      set
      {
        this.rightMouseButtonReset = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the maximum value that could be set in the spin editor.")]
    public Decimal MaxValue
    {
      get
      {
        return this.maxValue;
      }
      set
      {
        this.maxValue = value;
        this.OnNotifyPropertyChanged(nameof (MaxValue));
        if (this.minValue > this.maxValue)
          this.minValue = this.maxValue;
        this.SetSpinValue();
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the border is shown.")]
    [Browsable(true)]
    public bool ShowBorder
    {
      get
      {
        if (this.border == null)
          return false;
        return this.border.Visibility == ElementVisibility.Visible;
      }
      set
      {
        if (this.border == null)
          return;
        this.border.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Hidden;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating that value will revert to minimum value after reaching maximum and to maximum after reaching minimum")]
    [System.ComponentModel.DefaultValue(false)]
    public bool Wrap
    {
      get
      {
        return this.wrap;
      }
      set
      {
        this.wrap = value;
      }
    }

    [System.ComponentModel.DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets or sets a value indicating whether the user can change the value with mouse wheel.")]
    public bool EnableMouseWheel
    {
      get
      {
        return this.enableMouseWheel;
      }
      set
      {
        this.enableMouseWheel = value;
      }
    }

    [Category("Action")]
    [Browsable(true)]
    [Description("Occurs when the editor finished the value editing.")]
    public event EventHandler ValueChanged;

    protected virtual void OnValueChanged(EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    [Category("Action")]
    [Description("Occurs when the editor is changing the value during the editing process.")]
    public event ValueChangingEventHandler ValueChanging;

    [Description("Occurs when the user presses a key.")]
    [Category("Action")]
    public new event KeyEventHandler KeyDown;

    public virtual bool Validate()
    {
      if (this.validateCount > 0)
        return false;
      ++this.validateCount;
      this.EndTextEdit();
      this.ValidateCore();
      if (this.GetValueFromText() != this.internalValue)
        this.textItem.Text = this.GetNumberText();
      --this.validateCount;
      return true;
    }

    public virtual void PerformStep(Decimal step)
    {
      Decimal valueFromText = this.GetValueFromText();
      try
      {
        valueFromText += step;
      }
      catch (OverflowException ex)
      {
      }
      this.Value = this.Constrain(valueFromText);
      this.Validate();
    }

    protected void OnTextBoxKeyPress(KeyPressEventArgs e)
    {
      NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
      string decimalSeparator = numberFormat.NumberDecimalSeparator;
      string numberGroupSeparator = numberFormat.NumberGroupSeparator;
      string negativeSign = numberFormat.NegativeSign;
      if (e.KeyChar == '.')
        e.KeyChar = decimalSeparator[0];
      string str = e.KeyChar.ToString();
      if (char.IsDigit(e.KeyChar) || str.Equals(decimalSeparator) || (str.Equals(numberGroupSeparator) || str.Equals(negativeSign)) || e.KeyChar == '\b' || this.Hexadecimal && (e.KeyChar >= 'a' && e.KeyChar <= 'f' || e.KeyChar >= 'A' && e.KeyChar <= 'F') || (Control.ModifierKeys & (Keys.Control | Keys.Alt)) != Keys.None)
        return;
      e.Handled = true;
      Telerik.WinControls.NativeMethods.MessageBeep(0);
    }

    protected virtual void EndTextEdit()
    {
      if (this.enableNullValueInput && string.IsNullOrEmpty(this.TextBoxItem.Text))
      {
        this.isNullableValue = true;
        if (this.isNull)
          return;
        this.isNull = true;
        this.OnNullableValueChanged();
      }
      else
      {
        this.isNull = false;
        if (this.GetBitState(17592186044416L))
          this.SetSpinValue(this.GetValueFromText(), false);
        this.isNullableValue = false;
      }
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    protected override void OnTextChanged(EventArgs e)
    {
      this.BitState[17592186044416L] |= !this.GetBitState(8796093022208L);
      base.OnTextChanged(e);
    }

    protected override void OnBitStateChanged(long key, bool oldValue, bool newValue)
    {
      base.OnBitStateChanged(key, oldValue, newValue);
      if (key != 281474976710656L)
        return;
      if (newValue)
        this.buttonsLayout.Visibility = ElementVisibility.Visible;
      else
        this.buttonsLayout.Visibility = ElementVisibility.Collapsed;
      this.OnNotifyPropertyChanged("ShowUpDownButtons");
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      bool newValue = (bool) e.NewValue;
      if (this.Shape != null)
        this.Shape.IsRightToLeft = newValue;
      if (this.ButtonDown != null && this.ButtonDown.Shape != null)
        this.ButtonDown.Shape.IsRightToLeft = newValue;
      if (this.ButtonUp != null && this.ButtonUp.Shape != null)
        this.ButtonUp.Shape.IsRightToLeft = newValue;
      if (this.buttonsLayout == null || this.buttonsLayout.Shape == null)
        return;
      this.buttonsLayout.Shape.IsRightToLeft = newValue;
    }

    protected virtual void OnNullableValueChanged()
    {
      if (this.NullableValueChanged == null)
        return;
      this.NullableValueChanged((object) this, EventArgs.Empty);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (!this.ReadOnly && this.GetBitState(35184372088832L))
      {
        switch (e.KeyCode)
        {
          case Keys.Up:
            this.PerformStep(this.Step);
            e.Handled = true;
            break;
          case Keys.Down:
            this.PerformStep(-this.Step);
            e.Handled = true;
            break;
        }
      }
      if (this.KeyDown == null)
        return;
      this.KeyDown((object) this, e);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      this.ValidateOnKeyPress(e);
      base.OnKeyPress(e);
      this.OnTextBoxKeyPress(e);
    }

    protected internal virtual void ValidateOnKeyPress(KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      this.Validate();
      ((TextBoxBase) this.TextBoxItem.HostedControl).SelectAll();
    }

    private void textItem_TextChanging(object sender, TextChangingEventArgs e)
    {
      this.OnTextChanging(e);
    }

    private void textItem_TextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }

    private void HostedControl_MouseUp(object sender, MouseEventArgs e)
    {
      this.CallDoMouseUp(e);
    }

    private void textItem_MouseWheel(object sender, MouseEventArgs e)
    {
      this.ProcessMouseWheel(e);
      if (this.ReadOnly || !this.enableMouseWheel)
        return;
      HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
      if (handledMouseEventArgs != null)
      {
        if (handledMouseEventArgs.Handled)
          return;
        handledMouseEventArgs.Handled = true;
      }
      int num = 1;
      if (DateTime.Now < this.previosWheel.AddMilliseconds(15.0))
        num = 10;
      this.previosWheel = DateTime.Now;
      this.PerformStep(e.Delta > 0 ? this.Step * (Decimal) num : -this.Step * (Decimal) num);
    }

    protected virtual void ProcessMouseWheel(MouseEventArgs e)
    {
      if (!this.IsInValidState(false) || this.ElementTree == null || this.ElementTree.Control == null)
        return;
      (this.ElementTree.Control as RadControl).CallOnMouseWheel(e);
    }

    private void textItem_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void textItem_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void textItem_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    protected internal virtual void textItem_GotFocus(object sender, EventArgs e)
    {
      this.UpdateFocusBorder(true);
    }

    protected internal virtual void textItem_LostFocus(object sender, EventArgs e)
    {
      this.UpdateFocusBorder(false);
    }

    private void textItem_Validated(object sender, EventArgs e)
    {
      this.Validate();
    }

    private void ButtonDown_Click(object sender, EventArgs e)
    {
      if (this.ReadOnly)
        return;
      if (this.rightMouseButtonReset)
      {
        MouseEventArgs mouseEventArgs = e as MouseEventArgs;
        if (mouseEventArgs != null && mouseEventArgs.Button == MouseButtons.Right)
        {
          this.Value = this.minValue;
          return;
        }
      }
      this.PerformStep(-this.Step);
    }

    private void ButtonUp_Click(object sender, EventArgs e)
    {
      if (this.ReadOnly)
        return;
      if (this.rightMouseButtonReset)
      {
        MouseEventArgs mouseEventArgs = e as MouseEventArgs;
        if (mouseEventArgs != null && mouseEventArgs.Button == MouseButtons.Right)
        {
          this.Value = this.maxValue;
          return;
        }
      }
      this.PerformStep(this.Step);
    }

    private void textItem_MouseHover(object sender, EventArgs e)
    {
      this.textItem.GetType().GetMethod("DoMouseHover", BindingFlags.Instance | BindingFlags.NonPublic).Invoke((object) this.textItem, new object[1]
      {
        (object) e
      });
    }

    private void TextBoxControl_MouseLeave(object sender, EventArgs e)
    {
      this.textItem.GetType().GetMethod("DoMouseLeave", BindingFlags.Instance | BindingFlags.NonPublic).Invoke((object) this.textItem, new object[1]
      {
        (object) e
      });
    }

    private void SetSpinValue()
    {
      if (this.GetBitState(17592186044416L))
        this.SetSpinValue(this.GetValueFromText(), false);
      else
        this.SetSpinValue(this.internalValue, true);
    }

    private static string GetNumberText(Decimal num, bool hex, bool thousands, int decimalPlaces)
    {
      if (hex)
        return string.Format("{0:X}", (object) (long) num);
      return num.ToString((thousands ? "N" : "F") + decimalPlaces.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    private string GetNumberText()
    {
      return RadSpinElement.GetNumberText(this.Value, this.Hexadecimal, this.ThousandsSeparator, this.DecimalPlaces);
    }

    protected virtual void SetSpinValue(Decimal value, bool fromValue)
    {
      Decimal num = this.Constrain(value);
      bool flag = this.internalValue != num;
      this.textItem.Text = this.GetNumberText(this.internalValue);
      if (this.internalValue != num)
      {
        ValueChangingEventArgs e = new ValueChangingEventArgs((object) num, (object) this.internalValue);
        this.OnValueChanging(e);
        if (e.Cancel)
          return;
        this.internalValue = num;
        if (fromValue)
        {
          this.BitState[8796093022208L] = true;
          this.BitState[17592186044416L] = false;
          if (this.textItem != null)
            this.textItem.Text = this.GetNumberText(this.internalValue);
          this.BitState[8796093022208L] = false;
          this.isNullableValue = false;
        }
        this.isNullableValue = false;
        this.OnNullableValueChanged();
        this.OnValueChanged(EventArgs.Empty);
      }
      if (!flag)
        return;
      this.OnNotifyPropertyChanged("Value");
    }

    protected virtual Decimal Constrain(Decimal value)
    {
      if (!this.wrap)
        return Math.Max(this.minValue, Math.Min(this.maxValue, value));
      if (value > this.maxValue)
        value = this.minValue;
      else if (value < this.minValue)
        value = this.maxValue;
      return value;
    }

    protected virtual void ValidateCore()
    {
      Decimal num = this.Constrain(Math.Round(this.internalValue, this.decimalPlaces, MidpointRounding.AwayFromZero));
      if (!(num != this.internalValue))
        return;
      this.internalValue = num;
      this.textItem.Text = this.GetNumberText();
    }

    protected virtual string GetNumberText(Decimal num)
    {
      return RadSpinElement.GetNumberText(num, this.Hexadecimal, this.ThousandsSeparator, this.DecimalPlaces);
    }

    protected virtual Decimal GetValueFromText()
    {
      int num1 = this.Hexadecimal ? 1 : 0;
      try
      {
        if (string.IsNullOrEmpty(this.Text) || this.Text.Length == 1 && !(this.Text != "-"))
          return this.internalValue;
        Decimal num2 = new Decimal(0);
        return !this.Hexadecimal ? this.Constrain(Decimal.Parse(this.Text, (IFormatProvider) CultureInfo.CurrentCulture)) : this.Constrain(Convert.ToDecimal(Convert.ToInt64(this.Text, 16)));
      }
      catch
      {
        return this.internalValue;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetTextValueChanged(bool newState)
    {
      this.BitState[17592186044416L] = newState;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetSuppresEditorState(bool newState)
    {
      this.BitState[8796093022208L] = newState;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(this.GetClientRectangle(availableSize).Size);
      sizeF.Width = Math.Min(sizeF.Width, availableSize.Width);
      sizeF.Height = Math.Min(sizeF.Height, availableSize.Height);
      return sizeF;
    }
  }
}
