// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadToggleSwitch
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [DefaultEvent("ValueChanged")]
  public class RadToggleSwitch : RadControl
  {
    private RadToggleSwitchElement toggleSwitchElement;

    public RadToggleSwitch()
    {
      if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        return;
      LicenseManager.Validate(this.GetType());
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.toggleSwitchElement = new RadToggleSwitchElement();
      parent.Children.Add((RadElement) this.ToggleSwitchElement);
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(50, 20));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadToggleSwitchElement ToggleSwitchElement
    {
      get
      {
        return this.toggleSwitchElement;
      }
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.EnableGesture(GestureType.Pan);
    }

    [Description("Gets the on element of RadToggleSwitch")]
    public ToggleSwitchPartElement OnElement
    {
      get
      {
        return this.ToggleSwitchElement.OnElement;
      }
    }

    [Description("Gets the off element of RadToggleSwitch")]
    public ToggleSwitchPartElement OffElement
    {
      get
      {
        return this.ToggleSwitchElement.OffElement;
      }
    }

    [Description("Gets the thumb of RadToggleSwitch")]
    public ToggleSwitchThumbElement Thumb
    {
      get
      {
        return this.ToggleSwitchElement.Thumb;
      }
    }

    [DefaultValue("ON")]
    [Description("Gets or sets the text displayed when the state is On.")]
    public string OnText
    {
      get
      {
        return this.ToggleSwitchElement.OnText;
      }
      set
      {
        this.ToggleSwitchElement.OnText = value;
      }
    }

    [Description("Gets or sets the text displayed when the state is Off.")]
    [DefaultValue("OFF")]
    public string OffText
    {
      get
      {
        return this.ToggleSwitchElement.OffText;
      }
      set
      {
        this.ToggleSwitchElement.OffText = value;
      }
    }

    [DefaultValue(20)]
    [Description("Gets or sets width of the thumb.")]
    [VsbBrowsable(true)]
    public int ThumbTickness
    {
      get
      {
        return this.ToggleSwitchElement.ThumbTickness;
      }
      set
      {
        this.ToggleSwitchElement.ThumbTickness = value;
      }
    }

    [Description("Determines how far the switch needs to be dragged before it snaps to the opposite side.")]
    [DefaultValue(0.5)]
    public double SwitchElasticity
    {
      get
      {
        return this.ToggleSwitchElement.SwitchElasticity;
      }
      set
      {
        this.ToggleSwitchElement.SwitchElasticity = value;
      }
    }

    [Description("Gets or sets the value.")]
    [DefaultValue(true)]
    public bool Value
    {
      get
      {
        return this.ToggleSwitchElement.Value;
      }
      set
      {
        this.ToggleSwitchElement.Value = value;
        this.OnNotifyPropertyChanged(nameof (Value));
      }
    }

    [Description("Gets or sets a value indicating whether to use animation when changing its state.")]
    [DefaultValue(true)]
    public bool AllowAnimation
    {
      get
      {
        return this.ToggleSwitchElement.AllowAnimation;
      }
      set
      {
        this.ToggleSwitchElement.AllowAnimation = value;
      }
    }

    [DefaultValue(10)]
    [Description("Gets or sets the animation interval.")]
    public int AnimationInterval
    {
      get
      {
        return this.ToggleSwitchElement.AnimationInterval;
      }
      set
      {
        this.ToggleSwitchElement.AnimationInterval = value;
      }
    }

    [Description("Gets or sets the animation frames.")]
    [DefaultValue(20)]
    public int AnimationFrames
    {
      get
      {
        return this.ToggleSwitchElement.AnimationFrames;
      }
      set
      {
        this.ToggleSwitchElement.AnimationFrames = value;
      }
    }

    [Browsable(false)]
    [Description("Gets a value indicating whether the control is currently animating.")]
    [DefaultValue(false)]
    public bool IsAnimating
    {
      get
      {
        return this.ToggleSwitchElement.IsAnimating;
      }
    }

    [DefaultValue(ToggleStateMode.ClickAndDrag)]
    [Description("Determines how ToggleSwitch button should handle mouse click and drag.")]
    public ToggleStateMode ToggleStateMode
    {
      get
      {
        return this.ToggleSwitchElement.ToggleStateMode;
      }
      set
      {
        this.ToggleSwitchElement.ToggleStateMode = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the value could be changed.")]
    public bool ReadOnly
    {
      get
      {
        return this.ToggleSwitchElement.ReadOnly;
      }
      set
      {
        this.ToggleSwitchElement.ReadOnly = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    public void Toggle()
    {
      this.ToggleSwitchElement.Toggle();
    }

    public void Toggle(bool animate)
    {
      this.ToggleSwitchElement.Toggle(animate);
    }

    public void SetToggleState(bool newValue)
    {
      this.ToggleSwitchElement.SetToggleState(newValue);
    }

    public void SetToggleState(bool newValue, bool animate)
    {
      this.ToggleSwitchElement.SetToggleState(newValue, animate);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.ToggleSwitchElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ToggleSwitchElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.ToggleSwitchElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
      this.ToggleSwitchElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.ToggleSwitchElement.SuspendApplyOfThemeSettings();
      this.ToggleSwitchElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.ToggleSwitchElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.ToggleSwitchElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ToggleSwitchElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.ToggleSwitchElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.ToggleSwitchElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.ToggleSwitchElement.SuspendApplyOfThemeSettings();
      this.ToggleSwitchElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.ToggleSwitchElement.ResumeApplyOfThemeSettings();
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.ToggleSwitchElement.Focus();
    }

    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }

    public event ValueChangingEventHandler ValueChanging
    {
      add
      {
        this.ToggleSwitchElement.ValueChanging += value;
      }
      remove
      {
        this.ToggleSwitchElement.ValueChanging -= value;
      }
    }

    public event EventHandler ValueChanged
    {
      add
      {
        this.ToggleSwitchElement.ValueChanged += value;
      }
      remove
      {
        this.ToggleSwitchElement.ValueChanged -= value;
      }
    }

    public event AnimationStartedEventHandler AnimationStarted
    {
      add
      {
        this.ToggleSwitchElement.AnimationStarted += value;
      }
      remove
      {
        this.ToggleSwitchElement.AnimationStarted -= value;
      }
    }

    public event AnimationFinishedEventHandler AnimationFinished
    {
      add
      {
        this.ToggleSwitchElement.AnimationFinished += value;
      }
      remove
      {
        this.ToggleSwitchElement.AnimationFinished -= value;
      }
    }
  }
}
