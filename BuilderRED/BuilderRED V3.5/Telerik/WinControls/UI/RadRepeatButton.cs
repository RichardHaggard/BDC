// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRepeatButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Description("Offers click-and-hold functionality built directly within the ButtonClick event")]
  [ToolboxItem(true)]
  [DefaultEvent("Click")]
  [DefaultProperty("Text")]
  public class RadRepeatButton : RadButton
  {
    protected override RadButtonElement CreateButtonElement()
    {
      return (RadButtonElement) new RadRepeatButtonElement();
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(110, 24));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadRepeatButtonElement ButtonElement
    {
      get
      {
        return (RadRepeatButtonElement) base.ButtonElement;
      }
    }

    [Category("Appearance")]
    [Description("Determines whether the button can be clicked by using mnemonic characters.")]
    [DefaultValue(true)]
    public new bool UseMnemonic
    {
      get
      {
        return this.ButtonElement.TextElement.UseMnemonic;
      }
      set
      {
        this.ButtonElement.TextElement.UseMnemonic = value;
      }
    }

    [Bindable(true)]
    [Category("Behavior")]
    [RadDefaultValue("Delay", typeof (RadRepeatButtonElement))]
    [Description("Gets or sets the amount of time, in milliseconds, the Repeat button element waits while it is pressed before it starts repeating. The value must be non-negative.")]
    public int Delay
    {
      get
      {
        return this.ButtonElement.Delay;
      }
      set
      {
        this.ButtonElement.Delay = value;
      }
    }

    [Bindable(true)]
    [Category("Behavior")]
    [RadDefaultValue("Interval", typeof (RadRepeatButtonElement))]
    [Description("Gets or sets the amount of time, in milliseconds, between repeats once repeating starts. The value must be non-negative.")]
    public int Interval
    {
      get
      {
        return this.ButtonElement.Interval;
      }
      set
      {
        this.ButtonElement.Interval = value;
      }
    }

    [Description("Propagates internal element click.")]
    [Category("Action")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(true)]
    public event EventHandler ButtonClick;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnButtonClick(EventArgs e)
    {
      EventHandler buttonClick = this.ButtonClick;
      if (buttonClick == null)
        return;
      buttonClick((object) this, e);
    }

    internal virtual void PerformButtonClick()
    {
      this.OnButtonClick(EventArgs.Empty);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.ButtonElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ButtonElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.ButtonElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.ButtonElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "ButtonFill");
        this.ButtonElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, state, "ButtonFill");
      }
      this.ButtonElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.ButtonElement.SuspendApplyOfThemeSettings();
      this.ButtonElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.ButtonElement.ResetThemeValueOverride(FillPrimitive.GradientStyleProperty);
      this.ButtonElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.ButtonElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ButtonElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.ButtonElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.ButtonElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state, typeof (TextPrimitive));
      }
      this.ButtonElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.ButtonElement.SuspendApplyOfThemeSettings();
      this.ButtonElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num = (int) this.ButtonElement.TextElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.ButtonElement.ElementTree.ApplyThemeToElementTree();
      this.ButtonElement.ResumeApplyOfThemeSettings();
    }
  }
}
