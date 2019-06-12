// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalculatorDropDown
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [DefaultEvent("CalculatorValueChanged")]
  [Description("Enables the user to use a calculator control popup.")]
  [Designer("Telerik.WinControls.UI.Design.RadCalculatorDropDownDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [DefaultProperty("Value")]
  [TelerikToolboxCategory("Editors")]
  public class RadCalculatorDropDown : RadEditorControl
  {
    private bool fireFocusEvents;
    private RadCalculatorDropDownElement calculatorElement;

    public RadCalculatorDropDown()
    {
      this.AutoSize = true;
      this.TabStop = false;
      this.SetStyle(ControlStyles.Selectable, true);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.calculatorElement = this.CreateCalculatorDropDownElement();
      parent.Children.Add((RadElement) this.calculatorElement);
      this.calculatorElement.EditorContentElement.TextBoxItem.HostedControl.LostFocus += new EventHandler(this.HostedControl_LostFocus);
      this.calculatorElement.EditorContentElement.TextBoxItem.HostedControl.GotFocus += new EventHandler(this.HostedControl_GotFocus);
    }

    private void HostedControl_GotFocus(object sender, EventArgs e)
    {
      this.fireFocusEvents = true;
      this.OnGotFocus(e);
      this.fireFocusEvents = false;
    }

    private void HostedControl_LostFocus(object sender, EventArgs e)
    {
      this.fireFocusEvents = true;
      this.OnLostFocus(e);
      this.fireFocusEvents = false;
    }

    protected virtual RadCalculatorDropDownElement CreateCalculatorDropDownElement()
    {
      return new RadCalculatorDropDownElement();
    }

    protected override void OnGotFocus(EventArgs e)
    {
      if (!this.fireFocusEvents)
        return;
      base.OnGotFocus(e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      if (!this.fireFocusEvents)
        return;
      base.OnLostFocus(e);
    }

    [Browsable(true)]
    [Category("Layout")]
    [DefaultValue(true)]
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
    public RadCalculatorDropDownElement CalculatorElement
    {
      get
      {
        return this.calculatorElement;
      }
    }

    [Description("Gets or sets the calculator value.")]
    [DefaultValue(null)]
    public object Value
    {
      get
      {
        return this.calculatorElement.Value;
      }
      set
      {
        this.calculatorElement.Value = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(125, 20));
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the calculator drop down is read only.")]
    [Browsable(true)]
    [Category("Behavior")]
    public bool ReadOnly
    {
      get
      {
        return this.calculatorElement.ReadOnly;
      }
      set
      {
        this.calculatorElement.ReadOnly = value;
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

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (!this.Visible)
        return;
      this.CalculatorElement.EditorContentElement.TextBoxItem.InvalidateMeasure();
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

    protected override void SetBackColorThemeOverrides()
    {
      this.CalculatorElement.SuspendApplyOfThemeSettings();
      this.CalculatorElement.EditorContentElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates1 = this.CalculatorElement.GetAvailableVisualStates();
      availableVisualStates1.Add("");
      foreach (string state in availableVisualStates1)
      {
        this.CalculatorElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.CalculatorElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "CalculatorFill");
      }
      List<string> availableVisualStates2 = this.CalculatorElement.EditorContentElement.GetAvailableVisualStates();
      availableVisualStates2.Add("");
      foreach (string state in availableVisualStates2)
      {
        this.CalculatorElement.EditorContentElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.CalculatorElement.EditorContentElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TextBoxFill");
        this.CalculatorElement.EditorContentElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, state, "TextBoxFill");
      }
      this.CalculatorElement.ResumeApplyOfThemeSettings();
      this.CalculatorElement.EditorContentElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.CalculatorElement.SuspendApplyOfThemeSettings();
      this.CalculatorElement.EditorContentElement.SuspendApplyOfThemeSettings();
      this.CalculatorElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.CalculatorElement.EditorContentElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.CalculatorElement.EditorContentElement.ResetThemeValueOverride(FillPrimitive.GradientStyleProperty);
      int num = (int) this.CalculatorElement.EditorContentElement.Fill.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.CalculatorElement.ElementTree.ApplyThemeToElementTree();
      this.CalculatorElement.ResumeApplyOfThemeSettings();
      this.CalculatorElement.EditorContentElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.CalculatorElement.SuspendApplyOfThemeSettings();
      this.CalculatorElement.EditorContentElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.CalculatorElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.CalculatorElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.CalculatorElement.EditorContentElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      }
      this.CalculatorElement.ResumeApplyOfThemeSettings();
      this.CalculatorElement.EditorContentElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.CalculatorElement.SuspendApplyOfThemeSettings();
      this.CalculatorElement.EditorContentElement.SuspendApplyOfThemeSettings();
      this.CalculatorElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.CalculatorElement.EditorContentElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.CalculatorElement.ElementTree.ApplyThemeToElementTree();
      this.CalculatorElement.ResumeApplyOfThemeSettings();
      this.CalculatorElement.EditorContentElement.ResumeApplyOfThemeSettings();
    }

    public event ValueChangingEventHandler CalculatorValueChanging
    {
      add
      {
        this.CalculatorElement.CalculatorValueChanging += value;
      }
      remove
      {
        this.CalculatorElement.CalculatorValueChanging -= value;
      }
    }

    public event EventHandler CalculatorValueChanged
    {
      add
      {
        this.CalculatorElement.CalculatorValueChanged += value;
      }
      remove
      {
        this.CalculatorElement.CalculatorValueChanged -= value;
      }
    }

    public event EventHandler ValueChanged
    {
      add
      {
        this.CalculatorElement.CalculatorValueChanged += value;
      }
      remove
      {
        this.CalculatorElement.CalculatorValueChanged -= value;
      }
    }
  }
}
