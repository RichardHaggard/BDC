// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadToggleButton
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
  [DefaultBindingProperty("Text")]
  [DefaultEvent("ToggleStateChanged")]
  [Description("Extends the functionality of a checkbox by adding state management")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [DefaultProperty("Text")]
  public class RadToggleButton : RadButtonBase
  {
    private static readonly object StateChangingEventKey = new object();
    private static readonly object StateChangedEventKey = new object();
    private static readonly object IsCheckedChangedKey = new object();
    private static readonly object CheckStateChangedEventKey = new object();
    private static readonly object CheckStateChangingEventKey = new object();

    protected override RadButtonElement CreateButtonElement()
    {
      return (RadButtonElement) new RadToggleButtonElement();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.ButtonElement.ToggleStateChanging += new StateChangingEventHandler(this.ButtonElement_ToggleStateChanging);
      this.ButtonElement.ToggleStateChanged += new StateChangedEventHandler(this.ButtonElement_ToggleStateChanged);
      this.ButtonElement.PropertyChanged += new PropertyChangedEventHandler(this.res_PropertyChanged);
      this.ButtonElement.CheckStateChanging += new CheckStateChangingEventHandler(this.res_CheckStateChanging);
      this.ButtonElement.CheckStateChanged += new EventHandler(this.res_CheckStateChanged);
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadToggleButtonAccessibleObject(this);
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
    public RadToggleButtonElement ButtonElement
    {
      get
      {
        return (RadToggleButtonElement) base.ButtonElement;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [Bindable(true)]
    [RadDefaultValue("ToggleState", typeof (RadToggleButtonElement))]
    [Description("Gets or sets the toggle state. Toggle state enumeration defines the following values: Off, Indeterminate, and On.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Telerik.WinControls.Enumerations.ToggleState ToggleState
    {
      get
      {
        return this.ButtonElement.ToggleState;
      }
      set
      {
        this.ButtonElement.ToggleState = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [RadDefaultValue("IsChecked", typeof (RadToggleButtonElement))]
    [Category("Appearance")]
    [DefaultValue(false)]
    public virtual bool IsChecked
    {
      get
      {
        return this.ButtonElement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
      }
      set
      {
        this.ButtonElement.ToggleState = value ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      }
    }

    [DefaultValue(CheckState.Unchecked)]
    [Browsable(true)]
    [Bindable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the Check state. Check state enumeration defines the following values: Uncheck, Indeterminate, and Check.")]
    public CheckState CheckState
    {
      get
      {
        return this.ButtonElement.CheckState;
      }
      set
      {
        this.ButtonElement.CheckState = value;
      }
    }

    [Bindable(true)]
    [Browsable(true)]
    [RadDefaultValue("IsThreeState", typeof (RadToggleButtonElement))]
    [Category("Behavior")]
    public bool IsThreeState
    {
      get
      {
        return this.ButtonElement.IsThreeState;
      }
      set
      {
        this.ButtonElement.IsThreeState = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the drop down list is read only.")]
    public bool ReadOnly
    {
      get
      {
        return this.ButtonElement.ReadOnly;
      }
      set
      {
        this.ButtonElement.ReadOnly = value;
      }
    }

    [Description("Occurs before the elements's state changes.")]
    [Category("Action")]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event StateChangingEventHandler ToggleStateChanging
    {
      add
      {
        this.Events.AddHandler(RadToggleButton.StateChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadToggleButton.StateChangingEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnToggleStateChanging(StateChangingEventArgs e)
    {
      StateChangingEventHandler changingEventHandler = (StateChangingEventHandler) this.Events[RadToggleButton.StateChangingEventKey];
      if (changingEventHandler == null)
        return;
      changingEventHandler((object) this, e);
    }

    [Category("Action")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(true)]
    [Description("Occurs when the element's state changes.")]
    public event StateChangedEventHandler ToggleStateChanged
    {
      add
      {
        this.Events.AddHandler(RadToggleButton.StateChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadToggleButton.StateChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnToggleStateChanged(EventArgs e)
    {
      StateChangedEventHandler changedEventHandler = (StateChangedEventHandler) this.Events[RadToggleButton.StateChangedEventKey];
      if (changedEventHandler != null)
        changedEventHandler((object) this, (StateChangedEventArgs) e);
      this.OnNotifyPropertyChanged("ToggleState");
      this.OnToggleChanged(new EventArgs());
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(true)]
    [Category("Action")]
    [Description("Occurs before the elements's state changes.")]
    public event CheckStateChangingEventHandler CheckStateChanging
    {
      add
      {
        this.Events.AddHandler(RadToggleButton.CheckStateChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadToggleButton.CheckStateChangingEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnCheckStateChanging(CheckStateChangingEventArgs e)
    {
      CheckStateChangingEventHandler changingEventHandler = (CheckStateChangingEventHandler) this.Events[RadToggleButton.CheckStateChangingEventKey];
      if (changingEventHandler == null)
        return;
      changingEventHandler((object) this, e);
    }

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category("Action")]
    [Description("Occurs when the element's state changes.")]
    public event EventHandler CheckStateChanged
    {
      add
      {
        this.Events.AddHandler(RadToggleButton.CheckStateChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadToggleButton.CheckStateChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnCheckStateChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadToggleButton.CheckStateChangedEventKey];
      if (eventHandler != null)
        eventHandler((object) this, e);
      this.OnNotifyPropertyChanged("CheckState");
      this.OnToggleChanged(e);
    }

    protected virtual void OnToggleChanged(EventArgs e)
    {
    }

    protected virtual void res_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "IsChecked"))
        return;
      this.OnNotifyPropertyChanged("IsChecked");
    }

    protected virtual void ButtonElement_ToggleStateChanging(
      object sender,
      StateChangingEventArgs args)
    {
      this.OnToggleStateChanging(args);
    }

    protected virtual void ButtonElement_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.OnToggleStateChanged((EventArgs) args);
    }

    protected virtual void res_CheckStateChanged(object sender, EventArgs e)
    {
      this.OnCheckStateChanged(e);
    }

    protected virtual void res_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
    {
      this.OnCheckStateChanging(args);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.ButtonElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ButtonElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.ButtonElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.ButtonElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, typeof (FillPrimitive));
        this.ButtonElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, state, typeof (FillPrimitive));
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
