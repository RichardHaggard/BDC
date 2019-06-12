// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSpinEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [DefaultBindingProperty("Value")]
  [Description("Displays a single numeric value that the user can increment and decrement by clicking the up and down buttons on the control")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Editors")]
  [ToolboxItem(true)]
  [DefaultProperty("Value")]
  [DefaultEvent("ValueChanged")]
  public class RadSpinEditor : RadEditorControl, ISupportInitialize
  {
    private RadSpinElement spinElement;
    private bool entering;

    public RadSpinEditor()
    {
      this.AutoSize = true;
      base.TabStop = false;
      this.SetStyle(ControlStyles.Selectable, true);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.spinElement = this.CreateSpinElement();
      this.spinElement.RightToLeft = this.RightToLeft == RightToLeft.Yes;
      this.RootElement.Children.Add((RadElement) this.spinElement);
      this.spinElement.ValueChanging += new ValueChangingEventHandler(this.spinElement_ValueChanging);
      this.spinElement.ValueChanged += new EventHandler(this.spinElement_ValueChanged);
      this.spinElement.TextChanged += new EventHandler(this.spinElement_TextChanged);
      this.spinElement.KeyDown += new KeyEventHandler(this.OnSpinElementKeyDown);
      this.spinElement.KeyPress += new KeyPressEventHandler(this.OnSpinElementKeyPress);
      this.spinElement.KeyUp += new KeyEventHandler(this.OnSpinElementKeyUp);
    }

    protected virtual RadSpinElement CreateSpinElement()
    {
      return new RadSpinElement();
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadSpinEditorAccessibleObject(this, this.Name);
    }

    [DefaultValue(true)]
    public new bool TabStop
    {
      get
      {
        if (this.SpinElement.TextBoxItem != null)
          return this.SpinElement.TextBoxItem.TabStop;
        return base.TabStop;
      }
      set
      {
        if (this.SpinElement.TextBoxItem != null)
        {
          base.TabStop = false;
          this.SpinElement.TextBoxItem.TabStop = value;
        }
        else
          base.TabStop = value;
      }
    }

    public new string Name
    {
      get
      {
        return base.Name;
      }
      set
      {
        base.Name = value;
        this.SpinElement.TextBoxItem.HostedControl.Name = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(100, 20));
      }
    }

    [Category("Layout")]
    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadSpinElement SpinElement
    {
      get
      {
        return this.spinElement;
      }
    }

    [DefaultValue(typeof (Decimal), "0")]
    [Description("Minimum")]
    [Category("Data")]
    public Decimal Minimum
    {
      get
      {
        return this.SpinElement.MinValue;
      }
      set
      {
        this.SpinElement.MinValue = value;
      }
    }

    [Description("Maximum")]
    [DefaultValue(typeof (Decimal), "100")]
    [Category("Data")]
    public Decimal Maximum
    {
      get
      {
        return this.SpinElement.MaxValue;
      }
      set
      {
        this.SpinElement.MaxValue = value;
      }
    }

    [Description("Gets or sets the whether RadSpinEditor will be used as a numeric textbox.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool ShowUpDownButtons
    {
      get
      {
        return this.SpinElement.ShowUpDownButtons;
      }
      set
      {
        this.SpinElement.ShowUpDownButtons = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets whether by right-mouse clicking the up/down button you reset the value to the Maximum/Minimum value respectively.")]
    [Category("Behavior")]
    public bool RightMouseButtonReset
    {
      get
      {
        return this.spinElement.RightMouseButtonReset;
      }
      set
      {
        this.spinElement.RightMouseButtonReset = value;
      }
    }

    [DefaultValue(true)]
    [Description("ShowBorder")]
    [Category("Appearance")]
    public bool ShowBorder
    {
      get
      {
        return this.SpinElement.ShowBorder;
      }
      set
      {
        this.SpinElement.ShowBorder = value;
      }
    }

    protected override void OnValidated(EventArgs e)
    {
      this.SpinElement.Validate();
      base.OnValidated(e);
    }

    [Category("Data")]
    [Description("Step")]
    [DefaultValue(typeof (Decimal), "1")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Decimal Step
    {
      get
      {
        return this.SpinElement.Step;
      }
      set
      {
        this.SpinElement.Step = value;
      }
    }

    [Description("Increment")]
    [DefaultValue(typeof (Decimal), "1")]
    [Category("Data")]
    public Decimal Increment
    {
      get
      {
        return this.SpinElement.Step;
      }
      set
      {
        this.SpinElement.Step = value;
      }
    }

    [Description("Gets or sets a value indicating that value will revert to minimum value after reaching maximum and to maximum after reaching minimum")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool Wrap
    {
      get
      {
        return this.SpinElement.Wrap;
      }
      set
      {
        this.SpinElement.Wrap = value;
      }
    }

    [Category("Data")]
    [DefaultValue(typeof (Decimal), "0")]
    [Bindable(true)]
    public Decimal Value
    {
      get
      {
        return this.SpinElement.Value;
      }
      set
      {
        this.SpinElement.Value = value;
      }
    }

    [DefaultValue(typeof (Decimal?), "0")]
    [Category("Data")]
    [Bindable(true)]
    public Decimal? NullableValue
    {
      get
      {
        return this.SpinElement.NullableValue;
      }
      set
      {
        this.SpinElement.NullableValue = value;
        this.OnNotifyPropertyChanged(nameof (NullableValue));
      }
    }

    [DefaultValue(false)]
    public virtual bool EnableNullValueInput
    {
      get
      {
        return this.spinElement.EnableNullValueInput;
      }
      set
      {
        this.spinElement.EnableNullValueInput = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string Text
    {
      get
      {
        return this.spinElement.Text;
      }
      set
      {
        this.spinElement.Text = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the user can use the UP ARROW and DOWN ARROW keys to select values.")]
    [DefaultValue(true)]
    public bool InterceptArrowKeys
    {
      get
      {
        return this.spinElement.InterceptArrowKeys;
      }
      set
      {
        this.spinElement.InterceptArrowKeys = value;
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the text can be changed by the use of the up or down buttons only.")]
    public bool ReadOnly
    {
      get
      {
        return this.spinElement.ReadOnly;
      }
      set
      {
        this.spinElement.ReadOnly = value;
      }
    }

    [Category("Data")]
    [DefaultValue(false)]
    [Localizable(true)]
    [Description("Gets or sets a value indicating whether a thousands separator is displayed in the RadSpinEditor")]
    public bool ThousandsSeparator
    {
      get
      {
        return this.spinElement.ThousandsSeparator;
      }
      set
      {
        this.spinElement.ThousandsSeparator = value;
      }
    }

    [Localizable(true)]
    [Category("Data")]
    [DefaultValue(0)]
    [Description("Gets or sets the number of decimal places to display in the RadSpinEditor")]
    public int DecimalPlaces
    {
      get
      {
        return this.spinElement.DecimalPlaces;
      }
      set
      {
        this.spinElement.DecimalPlaces = value;
      }
    }

    [Description("Gets or sets a value indicating whether the RadSpinEditor should display the value it contains in hexadecimal format.")]
    [Category("Appearance")]
    [DefaultValue(false)]
    public bool Hexadecimal
    {
      get
      {
        return this.spinElement.Hexadecimal;
      }
      set
      {
        this.spinElement.Hexadecimal = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(HorizontalAlignment.Left)]
    [Description("Gets or sets the text alignment of RadSpinEditor")]
    public virtual HorizontalAlignment TextAlignment
    {
      get
      {
        return this.spinElement.TextAlignment;
      }
      set
      {
        this.spinElement.TextAlignment = value;
      }
    }

    [DefaultValue(true)]
    public new bool CausesValidation
    {
      get
      {
        return base.CausesValidation;
      }
      set
      {
        base.CausesValidation = value;
        this.SpinElement.TextBoxControl.CausesValidation = base.CausesValidation;
      }
    }

    [Category("Behavior")]
    public event EventHandler ValueChanged;

    [Category("Behavior")]
    public event ValueChangingEventHandler ValueChanging;

    [Category("Behavior")]
    public event EventHandler NullableValueChanged
    {
      add
      {
        this.spinElement.NullableValueChanged += value;
      }
      remove
      {
        this.spinElement.NullableValueChanged += value;
      }
    }

    public void PerformStep(Decimal step)
    {
      this.SpinElement.PerformStep(step);
    }

    protected override void OnEnter(EventArgs e)
    {
      base.OnEnter(e);
      if (this.entering)
        return;
      this.entering = true;
      this.spinElement.TextBoxControl.Focus();
      this.OnGotFocus(e);
      this.entering = false;
    }

    protected override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      this.OnLostFocus(e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      if (!this.entering)
        base.OnLostFocus(e);
      this.spinElement.Validate();
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs args)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, args);
    }

    protected virtual void OnValueChanged(EventArgs args)
    {
      if (this.ValueChanged != null)
        this.ValueChanged((object) this, args);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "ValueChanged", (object) this.Text);
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
      }
      if (rightToLeft == RightToLeft.No)
        this.SpinElement.RightToLeft = false;
      else if (rightToLeft == RightToLeft.Yes)
        this.SpinElement.RightToLeft = true;
      base.OnRightToLeftChanged(e);
    }

    private void OnSpinElementKeyDown(object sender, KeyEventArgs e)
    {
      this.CallBaseOnKeyDown(e);
    }

    private void OnSpinElementKeyUp(object sender, KeyEventArgs e)
    {
      this.CallBaseOnKeyUp(e);
    }

    private void OnSpinElementKeyPress(object sender, KeyPressEventArgs e)
    {
      this.CallBaseOnKeyPress(e);
    }

    private void spinElement_ValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged(e);
    }

    private void spinElement_ValueChanging(object sender, ValueChangingEventArgs e)
    {
      this.OnValueChanging(e);
    }

    private void spinElement_TextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
      if (value)
      {
        this.RootElement.StretchHorizontally = true;
        this.RootElement.StretchVertically = false;
      }
      else
      {
        this.RootElement.StretchHorizontally = true;
        this.RootElement.StretchVertically = true;
      }
      this.RootElement.SaveCurrentStretchModeAsDefault();
    }

    protected override void Select(bool directed, bool forward)
    {
      this.spinElement.TextBoxControl.Select();
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if ((object) element.GetType() == (object) typeof (RadRepeatArrowElement))
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.SpinElement.SuspendApplyOfThemeSettings();
      this.SpinElement.TextBoxItem.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.SpinElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.SpinElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.SpinElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "SpinElementFill");
        this.SpinElement.TextBoxItem.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
      }
      this.SpinElement.ResumeApplyOfThemeSettings();
      this.SpinElement.TextBoxItem.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.SpinElement.SuspendApplyOfThemeSettings();
      this.SpinElement.TextBoxItem.SuspendApplyOfThemeSettings();
      this.SpinElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num = (int) this.SpinElement.Children[0].ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.SpinElement.TextBoxItem.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.SpinElement.ElementTree.ApplyThemeToElementTree();
      this.SpinElement.ResumeApplyOfThemeSettings();
      this.SpinElement.TextBoxItem.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.SpinElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.SpinElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.SpinElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.SpinElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.SpinElement.SuspendApplyOfThemeSettings();
      this.SpinElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.SpinElement.ElementTree.ApplyThemeToElementTree();
      this.SpinElement.ResumeApplyOfThemeSettings();
    }
  }
}
