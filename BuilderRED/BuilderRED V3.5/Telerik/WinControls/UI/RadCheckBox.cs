// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckBox
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
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Description("Enables the user to select or clear the associated option.")]
  public class RadCheckBox : RadToggleButton
  {
    public RadCheckBox()
    {
      this.AutoSize = true;
    }

    protected override RadButtonElement CreateButtonElement()
    {
      return (RadButtonElement) new RadCheckBoxElement();
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadCheckBoxAccessibleObject(this, this.Name);
    }

    [Category("Layout")]
    [Description("Gets or sets a value indicating whether the control is automatically resized to display its entire contents.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
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

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(100, 18));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadCheckBoxElement ButtonElement
    {
      get
      {
        return (RadCheckBoxElement) base.ButtonElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets value indicating the checked state of the checkbox.")]
    [DefaultValue(false)]
    [Bindable(true)]
    public bool Checked
    {
      get
      {
        return this.ButtonElement.Checked;
      }
      set
      {
        this.ButtonElement.Checked = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsChecked
    {
      get
      {
        return this.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
      }
      set
      {
        this.ToggleState = value ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
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

    [RadDescription("CheckAlignment", typeof (RadCheckBoxElement))]
    [RadPropertyDefaultValue("CheckAlignment", typeof (RadCheckBoxElement))]
    [Category("Layout")]
    public ContentAlignment CheckAlignment
    {
      get
      {
        return this.ButtonElement.CheckAlignment;
      }
      set
      {
        this.ButtonElement.CheckAlignment = value;
      }
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      bool isInitializing = this.IsInitializing;
      this.SetIsInitializing(false);
      this.LoadElementTree();
      this.SetIsInitializing(isInitializing);
      base.OnEnabledChanged(e);
    }

    protected override void ButtonElement_ToggleStateChanging(
      object sender,
      StateChangingEventArgs args)
    {
      this.OnToggleStateChanging(args);
    }

    protected override void ButtonElement_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.OnToggleStateChanged((EventArgs) args);
      this.OnNotifyPropertyChanged("Checked");
      this.OnNotifyPropertyChanged("CheckState");
      this.OnNotifyPropertyChanged("ToggleState");
    }

    protected override void res_CheckStateChanged(object sender, EventArgs e)
    {
      this.OnCheckStateChanged(e);
    }

    protected override void res_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
    {
      this.OnCheckStateChanging(args);
    }

    protected override void res_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "IsChecked"))
        return;
      this.OnNotifyPropertyChanged("IsChecked");
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (element is RadCheckBoxElement || element is RadCheckmark)
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
        this.ButtonElement.CheckMarkPrimitive.EnableVisualStates = true;
      else
        this.ButtonElement.CheckMarkPrimitive.EnableVisualStates = false;
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
  }
}
